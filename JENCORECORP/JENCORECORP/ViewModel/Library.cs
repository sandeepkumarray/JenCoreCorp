using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using RDCore;
using System.Windows.Media;

namespace JENCORECORP
{
    public static class Library
    {
        public static string UserName { get; set; }
        public static string PassCode { get; set; }
        public static string ErrorLogPath { get; set; }

        public static bool isCoreUser { get; set; }
        public static bool IsFromResource { get; set; }
        public static Thread RunningThread { get; set; }

        public static RDCore.DataBaseManager JENDBManager { get; set; }
        public static RDCore.DataBaseManager MTDBManager { get; set; }

        public static USERS CurrentUser { get; set; }

        internal static string GetRootDirectory()
        {
            string RootDirectory = string.Empty;
            if (Convert.ToString(ConfigurationManager.AppSettings["RootDirectory"]) != string.Empty)
                RootDirectory = Convert.ToString(ConfigurationManager.AppSettings["RootDirectory"]);
            else
                RootDirectory = Environment.CurrentDirectory + "\\JENCOREDATA";

            return RootDirectory;
        }

        internal static string GetXMLDataFile()
        {
            string XMLDataFile = string.Empty;
            if (Convert.ToString(ConfigurationManager.AppSettings["XMLDataFile"]) != string.Empty)
                XMLDataFile = Convert.ToString(ConfigurationManager.AppSettings["XMLDataFile"]);
            else
                XMLDataFile ="JenData.xml";

            return XMLDataFile;
        }

        internal static string GetJENDataFile()
        {
            string JenDataFile = string.Empty;
            JenDataFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataFiles//JenData.jen");
            return JenDataFile;
        }

        public static String JenDBConnString = "Data Source=" + Library.GetJENDataFile() + ";Password=" +
                Encryption.Decrypt(ConfigurationManager.AppSettings["JenDBPassword"].ToString(), "abcdefgh", "JENCOREAD") +
                ";Persist Security Info=False;";

        public static string Version = "Version 5.5.5.5";

        public static string GetColorName(Color color)
        {
            string Name = string.Empty;
            var Colors = typeof(Colors).GetProperties();
            foreach (var it in Colors)
            {
                if ((Color)new ColorConverter().ConvertFromInvariantString(it.Name.ToString()) == color)
                {
                    Name = it.Name;
                }
            }
            return Name;
        }
    }
}
