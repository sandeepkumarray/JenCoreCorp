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

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for UNBrowser.xaml
    /// </summary>
    public partial class UNBrowser : UserControlClass
    {
        string URIData = string.Empty;
        public UNBrowser()
        {
            InitializeComponent();
        }

        public UNBrowser(String Data)
        {
            InitializeComponent();
            URIData = Data;
            Loaded += new RoutedEventHandler(UNBrowser_Loaded);
        }

        ~UNBrowser()
        {
            this.Dispose();
        }

        void UNBrowser_Loaded(object sender, RoutedEventArgs e)
        {
            jeenbrowser.Source = new Uri(URIData, UriKind.RelativeOrAbsolute);
        }

        public void Dispose()
        {
            //this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
