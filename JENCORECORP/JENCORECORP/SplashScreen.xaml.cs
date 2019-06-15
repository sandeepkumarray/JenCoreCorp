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
using System.Configuration;
using RDCore;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using JENCORECORP.App_GlobalResources;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        string UserName, PassCode;
        int Attemp = 0;
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(SplashScreen_Loaded);
        }

        void SplashScreen_Loaded(object sender, RoutedEventArgs e)
        {
            Library.UserName = Convert.ToString(ConfigurationManager.AppSettings["UserName"]);
            Library.PassCode = Convert.ToString(ConfigurationManager.AppSettings["PassCode"]);

            

            string DataFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            Library.JENDBManager = new DataBaseManager(Library.JenDBConnString, Convert.ToString(ConfigurationManager.AppSettings["JenDBProvider"]));
            Library.MTDBManager = new DataBaseManager("MTDB");

            txtUsername.Text = "admin";
            txtPassword.Password = "admin";
            btnLogin_Click(sender, e);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Attemp < 3)
            {
                UserName = Encryption.Encrypt(txtUsername.Text, "abcdefgh", "JENCOREAD");
                PassCode = Encryption.Encrypt(txtPassword.Password, "abcdefgh", "JENCOREAD");
                UserDAL DataAccessLayer = new UserDAL(Library.JENDBManager);
                
                //if ((UserName == ConfigurationManager.AppSettings["UserName"].ToString() && (PassCode == ConfigurationManager.AppSettings["PassCode"].ToString()))
                //    || (UserName == ConfigurationManager.AppSettings["AdminUserName"].ToString() && (PassCode == ConfigurationManager.AppSettings["AdminPassCode"].ToString())))
                USERS User = DataAccessLayer.CheckJenUsers(txtUsername.Text,txtPassword.Password);
                if (User.IsActive)
                {
                    Library.CurrentUser = User;
                    if (User.UserName.ToLower() == "admin" && User.RoleType == "A")
                        Library.isCoreUser = true;
                    var startupTask = new Task(() =>
                    {
                        Thread.Sleep(5000);
                    });

                    //when plugin loading finished, show main window
                    startupTask.ContinueWith(t =>
                    {
                        Dashboard dashboard = new Dashboard();

                        //when main windows is loaded close splash screen
                        dashboard.Loaded += (sender1, args) => this.Close();

                        //set application main window;
                        //this.MainWindow = dashboard;

                        //and finally show it
                        dashboard.Show();
                    }, TaskScheduler.FromCurrentSynchronizationContext());

                    startupTask.Start();
                }
                else
                {
                    MessageBoxResult MSBresult = MessageBox.Show(JenResources.WRONGPASSCODE, JenResources.JENCORECORPALERT, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.None);
                    if (MSBresult == MessageBoxResult.OK)
                    {
                        Attemp++;

                        StringBuilder log = new StringBuilder();
                        log.Append(JenResources.WRONGPASSCODELOGTEMPLATE);
                        log.Replace("[DATENOW]", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
                        log.Replace("[USERNAME]", txtUsername.Text);
                        log.Replace("[PASSWORD]", txtPassword.Password);
                        log.Replace("[ATTEMPT]", Convert.ToString(Attemp));

                        RDCore.Logger.WriteLog(log.ToString(),MessageType.Warning,this.GetType(),DateTime.Now,Library.ErrorLogPath);
                        txtUsername.Text = "";
                        txtPassword.Password = "";
                    }
                }
            }
            else
                this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
