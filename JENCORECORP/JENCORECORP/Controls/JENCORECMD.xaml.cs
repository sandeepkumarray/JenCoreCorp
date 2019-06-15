using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for JENCORECMD.xaml
    /// </summary>
    public partial class JENCORECMD : UserControlClass
    {
        StringBuilder sb;
        DispatcherTimer dtimer = new DispatcherTimer();
        int ctr = 0;
        List<string> Messages;
        string CurMessage;
        Random rndm = new Random();
        bool isWritable = false;
        public JENCORECMD()
        {
            InitializeComponent();
            Messages = new List<string>();
            sb = new StringBuilder();
            //Library.isCoreUser = true;
            isWritable = true;
            Loaded += new RoutedEventHandler(JENCORECMD_Loaded);
        }

        void JENCORECMD_Loaded(object sender, RoutedEventArgs e)
        {
            if (Library.isCoreUser)
            {
                txtCmd.Visibility = Visibility.Visible;
            }

            sb.AppendLine("JENCORE WORLD CORPORATION [" + Library.Version + "]");
            sb.AppendLine("Copyright (c) 2021 JENCORE WORLD CORPORATION.");
            sb.AppendLine("All rights reserved.");
            sb.Append("!=>");
            rtbConsole.AppendText(sb.ToString());
            rtbConsole.Focus();
            txtCmd.Text = "!=>";

        }

        void dtimer_Tick(object sender, EventArgs e)
        {
            isWritable = false;
            switch (ctr)
            {
                case 10:
                    rtbConsole.AppendText("Executing..." + "Removing Core Files" + "\n");
                    break;
                case 30:
                    rtbConsole.AppendText("Executing..." + "Adding Administrator Settings" + "\n");
                    break;
                case 40:
                    rtbConsole.AppendText("Executing..." + "Checking Application Pool" + "\n");
                    break;
                case 50:
                    rtbConsole.AppendText("Executing..." + "Logging Process Execution" + "\n");
                    break;
                case 80:
                    rtbConsole.AppendText("Executing..." + "Terminating Datalink" + "\n");
                    break;
                case 100:
                    dtimer.Stop();
                    dtimer.IsEnabled = false;
                    rtbConsole.AppendText("\n" + "!=>");
                    isWritable = true;
                    ctr = 0;
                    break;
                default:
                    break;
            }
            ctr = ctr + 10;
            rtbConsole.ScrollToEnd();
        }

        private void rtbConsole_KeyDown(object sender, KeyEventArgs e)
        {
            rtbConsole.ScrollToEnd();
        }

        private void rtbConsole_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(isWritable == true)
            switch (e.Key)
            {
                case Key.Enter:
                    GETEnterCommand(sender);
                    
                    break;
                case Key.Delete:
                    //rtbConsole.Undo();
                    break;
                case Key.Back:
                    rtbConsole.Undo();
                    break;
                case Key.Space:
                    rtbConsole.AppendText(" ");
                    break;
                case Key.D0:
                    rtbConsole.AppendText("0");
                    break;
                case Key.D1:
                    rtbConsole.AppendText("1");
                    break;
                case Key.D2:
                    rtbConsole.AppendText("2");
                    break;
                case Key.D3:
                    rtbConsole.AppendText("3");
                    break;
                case Key.D4:
                    rtbConsole.AppendText("4");
                    break;
                case Key.D5:
                    rtbConsole.AppendText("5");
                    break;
                case Key.D6:
                    rtbConsole.AppendText("6");
                    break;
                case Key.D7:
                    rtbConsole.AppendText("7");
                    break;
                case Key.D8:
                    rtbConsole.AppendText("8");
                    break;
                case Key.D9:
                    rtbConsole.AppendText("9");
                    break;
                case Key.NumPad0:
                    rtbConsole.AppendText("0");
                    break;
                case Key.NumPad1:
                    rtbConsole.AppendText("1");
                    break;
                case Key.NumPad2:
                    rtbConsole.AppendText("2");
                    break;
                case Key.NumPad3:
                    rtbConsole.AppendText("3");
                    break;
                case Key.NumPad4:
                    rtbConsole.AppendText("4");
                    break;
                case Key.NumPad5:
                    rtbConsole.AppendText("5");
                    break;
                case Key.NumPad6:
                    rtbConsole.AppendText("6");
                    break;
                case Key.NumPad7:
                    rtbConsole.AppendText("7");
                    break;
                case Key.NumPad8:
                    rtbConsole.AppendText("8");
                    break;
                case Key.NumPad9:
                    rtbConsole.AppendText("9");
                    break;
                default:
                    rtbConsole.AppendText(e.Key.ToString());
                    break;
            }
            rtbConsole.ScrollToEnd();
        }

        private void GETEnterCommand(object sender)
        {
            if (sender.GetType().Name == "RichTextBox")
            {
                TextRange textRange = new TextRange(rtbConsole.Document.ContentStart, rtbConsole.Document.ContentEnd);
                int lastindex = textRange.Text.LastIndexOf("!=>");
                string getCommandLine = textRange.Text.Substring(lastindex + 3).Replace("\r", "").Replace("\n", "");
                if (getCommandLine.ToLower().Contains("command"))
                    ExecuteCommand(getCommandLine);
            }
            else
            {
                int lastindex = txtCmd.Text.LastIndexOf("!=>");
                string getCommandLine = txtCmd.Text.Substring(lastindex + 3).Replace("\r", "").Replace("\n", "");
                ExecuteCommand(getCommandLine);
            }
        }

        private void ExecuteCommand(string CommandLine)
        {
            if (CommandLine.ToLower().Contains("order66"))
            {
                GoTimer();
            }

            if (CommandLine.ToLower().Contains("overrule security 1"))
            {
                rtbConsole.AppendText("\nOverruled" + "\n");
                Thread.Sleep(500);
            }
            if (CommandLine.ToLower().Contains("exit"))
            {
                rtbConsole.AppendText("\nExiting" + "\n");
                Thread.Sleep(1000);
                ((ChildWindow)(this.Parent)).Close();
            }
        }

        private void GoTimer()
        {
            dtimer.Interval = new TimeSpan(0, 0, 2);
            dtimer.IsEnabled = true;
            dtimer.Tick += new EventHandler(dtimer_Tick);
            dtimer.Start();
        }

        private void txtCmd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    GETEnterCommand(sender);
                    rtbConsole.AppendText("\n" + "!=>");
                    txtCmd.Text = "!=>";
                    break;
                default:
                    break;
            }

            rtbConsole.ScrollToEnd();
        }
    }
}
