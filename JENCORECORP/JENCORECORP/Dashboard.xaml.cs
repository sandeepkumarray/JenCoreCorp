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
using System.Windows.Interop;
using System.Windows.Threading;
using RDCore;
using System.Threading;
using System.ComponentModel;
using DragCanvas;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        Boolean flag = false;
        DispatcherTimer timer;
        string KeyWord = string.Empty;
        FolderFileViewModel objffvm;
        int i = 0;
        private readonly BackgroundWorker SearchWorker = new BackgroundWorker();
        RoutedCommand cmndSettings = new RoutedCommand(); 
        public Dashboard()
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            cmndSettings.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control,"Search"));
            CommandBindings.Add(new CommandBinding(cmndSettings, btnStart_Click2));

            //throw new Exception("Exception Happened");
            Searchbox.btnSearch.Click += new RoutedEventHandler(btnSearch_Click);
            Searchbox.txtSearchKey.KeyDown += new KeyEventHandler(txtSearchKey_KeyDown);
            JENCOREEventHandler.SearchResultFromDirectory += new EventHandler<SearchResultFromDirectory>(JENCOREEventHandler_SearchResultFromDirectory);
            btnSpecialProjects.DataContext = new ApplicationTile() { Icon = "/Images/1361558396_x.png" };
            btnDecreadesZones.DataContext = new ApplicationTile() { Icon = "/Images/1361558391_minus.png" };
            btnJenCmd.DataContext = new ApplicationTile() { Icon = "/Images/1361558363_new_window.png" };
            _modalchildWindow.Closed += new EventHandler(_modalchildWindow_Closed);

            if (Library.isCoreUser)
            {
                btnSpecialProjects.Visibility = Visibility.Visible;
            }
        }

        void _modalchildWindow_Closed(object sender, EventArgs e)
        {
            if ((_modalchildWindow.Content.GetType().BaseType).Name == "UserControlClass")
                ((UserControlClass)_modalchildWindow.Content).Dispose();// Unloaded += new RoutedEventHandler((s, re) => { ((UserControlClass)_modalchildWindow.Content).Dispose(); });
        }

        void JENCOREEventHandler_SearchResultFromDirectory(object sender, SearchResultFromDirectory e)
        {

        }

        void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                btnSearch_Click(sender, new RoutedEventArgs());
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnMaximise_Click(object sender, RoutedEventArgs e)
        {
            if (flag == false)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                btnMaximise.Template = (ControlTemplate)Application.Current.Resources["RestoreIcon"];
                flag = true;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                btnMaximise.Template = (ControlTemplate)Application.Current.Resources["MaximizeIcon"];
                flag = false;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = true;
            //HostParent hostcntrl = new HostParent();
            ////hostcntrl.ExeName = "notepad.exe";
            //hostcntrl.ExeName = @"C:/MyOwn/OOPConceptsSamples/OOPConceptsSamples/bin/Debug/OOPConceptsSamples.exe";
            //appControl.Content = hostcntrl;
            //hostcntrl.Unloaded += new RoutedEventHandler((s, re) => { hostcntrl.Dispose(); });
            //AxWMPLib.AxWindowsMediaPlayer axWmp = new AxWMPLib.AxWindowsMediaPlayer();
        }

        private void btnStart_Click2(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = true;
            Searchbox.txtSearchKey.Focus();
        }

        void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Library.IsFromResource = false;
            ppStart.IsOpen = false;
            KeyWord = Searchbox.txtSearchKey.Text;
            object obj = new SearchMethod().GetSearchResult(Searchbox.txtSearchKey.Text);
            //Library.IsFromResource = false;
            Type t = obj.GetType();

            if (t.Name == "String")
            {
                _modalchildWindow.Content = new TextViewer((string)obj);
                _modalchildWindow.Show();
            }
            else
                if (Library.IsFromResource == false)
                {
                    SearchWorker.DoWork += new DoWorkEventHandler(SearchWorker_DoWork);
                    SearchWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(SearchWorker_RunWorkerCompleted);
                    SearchWorker.ProgressChanged += new ProgressChangedEventHandler(SearchWorker_ProgressChanged);
                    SearchWorker.WorkerSupportsCancellation = true;
                    SearchWorker.WorkerReportsProgress = true;
                    SearchWorker.RunWorkerAsync();
                }

            Searchbox.txtSearchKey.Text = string.Empty;
        }

        void SearchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = "Searching";
        }

        void SearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblStatus.Text = "";
                if (objffvm.SearchResult.Count > 0)
                    _modalchildWindow.Content = new FolderFileList(objffvm);
                else
                    _modalchildWindow.Content = new TextViewer("No Items Match Your Search.");
                _modalchildWindow.Show();
            }));
        }

        void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            SearchWorker.ReportProgress(i++);
            objffvm = new FolderFileViewModel(Library.GetRootDirectory(), Library.GetRootDirectory(), null, KeyWord, false, false, false, 0);
            objffvm.GetFiles(Library.GetRootDirectory(), Library.GetRootDirectory(), null, KeyWord, false, false, false, 0);
            SearchWorker.CancelAsync();
        }

        private void btnSpecialProjects_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = false;
          
            //_modalchildWindow.Content = new TextViewer("No New Records Found");
            _modalchildWindow.Content = new WeatherDashboard();
            _modalchildWindow.Show();
        }

        private void btnDecreadesZones_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = false;
            //_modalchildWindow.Content = new TextViewer("Zoner Account is disabled for unspecified reasons. \n \rContact Administrators for counselling.");
            _modalchildWindow.Content = new DecreadesZones();
            _modalchildWindow.Show();
        }

        private void btnJenCmd_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = false;
            _modalchildWindow.Content = new JENCORECMD();
            _modalchildWindow.Show();
        }

        private void btnRadarControl_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = false;
            _modalchildWindow.Content = new RadarX();
            _modalchildWindow.Show();
        }

        private void btnMenuStrip_Click(object sender, RoutedEventArgs e)
        {
            ppStart.IsOpen = false;
            if (grdMainTile.Visibility == Visibility.Visible)
                appControl.Content = new MenuStrip();
            else
            {
                var gadgetContainer = new DragUserControlContent();
                Canvas.SetTop(gadgetContainer, 10);
                Canvas.SetLeft(gadgetContainer, 10);
                gadgetContainer.OptionButtonType = OptionButtonTypes.Settings;
                //gadgetContainer.Close += OnGadgetClose;                
                gadgetContainer.IsCloseButtonVisible = false;
                var menuStrip = new MenuStrip();
                gadgetContainer.Gadget = menuStrip;
                gadgetContainer.Width = 250;
                _SnapCanvas.Children.Add(gadgetContainer);
            }
        }

        private void btnWorkSpace_Click(object sender, RoutedEventArgs e)
        {
            grdMainTile.Visibility = Visibility.Collapsed;
            grdDragFlow.Visibility = Visibility.Visible;
        }
    }
}
