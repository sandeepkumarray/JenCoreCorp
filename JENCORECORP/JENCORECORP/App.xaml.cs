using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Threading;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Library.ErrorLogPath = Environment.CurrentDirectory;
            SPscreen splashScreen = new SPscreen();

            //set basic dynamic data on splash screen
            //splashScreen.AvailablePlugins = new[] { "Plugin 1", "Plugin 2" };
            //splashScreen.Licensee = "Liz Smith";

            splashScreen.Show();

            //use during development to generate image and embed it in application
            //splashScreen.Capture(@"c:\StaticSplashScreen.png");

            var startupTask = new Task(() =>
            {
                //Load plugins in non-UI thread - may be time consuming
                for (int i = 0; i < 5; i++)
                {
                    int Percent = 0;
                    Random r;
                    switch(i)
                    {
                        case 0:
                            r = new Random();
                            Percent = r.Next(0,20);
                            break;
                        case 1:
                            r = new Random();
                            Percent = r.Next(21, 40);
                            break;
                        case 2:
                            r = new Random();
                            Percent = r.Next(41, 60);
                            break;
                        case 3:
                            r = new Random();
                            Percent = r.Next(61, 80);
                            break;
                        case 4:
                            r = new Random();
                            Percent = r.Next(81, 100);
                            break;
                    }
                    //set custom message on screen
                    splashScreen.Dispatcher.BeginInvoke(
                        (Action)(() => splashScreen.Message = "Loading:  Files " + Percent + "%"));

                    Thread.Sleep(1000);
                }
                splashScreen.Dispatcher.BeginInvoke(
                        (Action)(() => splashScreen.Message = "Executing Procedures"));
                Thread.Sleep(5000);

                splashScreen.Dispatcher.BeginInvoke(
                        (Action)(() => splashScreen.Message = "Loading:  UI Components"));
                Thread.Sleep(1000);

                splashScreen.Dispatcher.BeginInvoke(
                        (Action)(() => splashScreen.Message = "Loading:  DataBase"));
                Thread.Sleep(1000);
            });

            //when plugin loading finished, show main window
            startupTask.ContinueWith(t =>
            {
                SplashScreen SplashScreen = new SplashScreen();

                //when main windows is loaded close splash screen
                SplashScreen.Loaded += (sender, args) => splashScreen.Close();

                //set application main window;
                this.MainWindow = SplashScreen;

                //and finally show it
                SplashScreen.Show();
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupTask.Start();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //RDCore.Logger.WriteLog(e.Exception.Message, RDCore.MessageType.Error, this.GetType(),DateTime.Now,Library.ErrorLogPath);
            RDCore.Logger.Write(e.Exception.Message, RDCore.MessageType.Error, this.GetType());
            //System.String strMessage;
            //Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry objLogEntry = null;
            //System.Exception objExcp;
            //objLogEntry = new Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry();
            //objExcp = new System.Exception();
            //objExcp = e.Exception;
            //if (objExcp.InnerException != null)
            //{
            //    strMessage = objExcp.InnerException +
            //                    "\nSource: " + objExcp.InnerException.Source +
            //                    "\nTargetsite: " + objExcp.InnerException.TargetSite +
            //                    "\nStacktrace: " + objExcp.InnerException.StackTrace;
            //}
            //else
            //{
            //    strMessage = objExcp.Message +
            //                    "\nSource: " + objExcp.Source +
            //                    "\nTargetsite: " + objExcp.TargetSite +
            //                    "\nStacktrace: " + objExcp.StackTrace;
            //}
            //objLogEntry.Message = strMessage;
            //objLogEntry.Priority = 1;
            //objLogEntry.Severity = System.Diagnostics.TraceEventType.Error;
            //objLogEntry.TimeStamp = System.DateTime.Now;
            ////writing the error and its details to the Event Log
            //Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(objLogEntry);
            e.Handled = true;
        }      
    }
}
