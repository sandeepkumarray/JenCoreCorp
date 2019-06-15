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
using System.Windows.Shapes;
using System.Threading;
using System.Reflection;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for SPscreen.xaml
    /// </summary>
    public partial class SPscreen : Window
    {
        public static readonly DependencyProperty AvailablePluginsProperty =
            DependencyProperty.Register("AvailablePlugins", typeof(IEnumerable<string>), typeof(SPscreen),
                                        new UIPropertyMetadata(null));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(SPscreen),
                                        new UIPropertyMetadata(null, OnMessageChanged));


        public static readonly DependencyProperty LicenseeProperty =
            DependencyProperty.Register("Licensee", typeof(string), typeof(SPscreen), new UIPropertyMetadata(null));

        public static readonly DependencyProperty VersionProperty =
    DependencyProperty.Register("Version", typeof(string), typeof(SPscreen), new UIPropertyMetadata(null));

        public SPscreen()
        {
            InitializeComponent();

            Thread.Sleep(5000);
        }

        public string Version
        {
            get { return (string)this.GetValue(VersionProperty); }
            set { this.SetValue(VersionProperty, value); }
        }

        public string Licensee
        {
            get { return (string)this.GetValue(LicenseeProperty); }
            set { this.SetValue(LicenseeProperty, value); }
        }


        public IEnumerable<string> AvailablePlugins
        {
            get { return (IEnumerable<string>)this.GetValue(AvailablePluginsProperty); }
            set { this.SetValue(AvailablePluginsProperty, value); }
        }


        public string Message
        {
            get { return (string)this.GetValue(MessageProperty); }
            set { this.SetValue(MessageProperty, value); }
        }


        public event System.EventHandler MessageChanged;

        private void RaiseMessageChanged(EventArgs e)
        {
            System.EventHandler handler = this.MessageChanged;
            if (handler != null) handler(this, e);
        }

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SPscreen splashScreen = (SPscreen)d;
            splashScreen.MessageChanged += new System.EventHandler(splashScreen_MessageChanged);
            splashScreen.RaiseMessageChanged(EventArgs.Empty);
            //splashScreen.messageTextBlock.Text = splashScreen.Message;
            //if(splashScreen.Message == "Executing Stored Procedures")
            //    ExecuteStoredProcedures(splashScreen);
        }

        static void splashScreen_MessageChanged(object sender, EventArgs e)
        {
            SPscreen splashScreen = (SPscreen)sender;
            string Message = splashScreen.Message;
            switch (Message)
            {
                case "Executing Procedures":
                    ExecuteStoredProcedures(splashScreen);
                    break;
                default:
                    break;
            }
        }

        private static void ExecuteStoredProcedures(SPscreen splashScreen)
        {
            //Thread.Sleep(5000);
            Assembly t=Assembly.GetExecutingAssembly();
            splashScreen.Licensee = "Licensed to: Cedrik John Engen";
            splashScreen.Version = t.FullName.Split(',')[1].Replace("=", " ");// "Version 2.1";
            Library.Version = splashScreen.Version;
        }
    }
}
