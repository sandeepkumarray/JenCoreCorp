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
    /// Interaction logic for FolderFileList.xaml
    /// </summary>
    public partial class FolderFileList : UserControlClass
    {
        public FolderFileList()
        {
            InitializeComponent();
        }

        public FolderFileList(object ViewModel)
        {
            InitializeComponent();            
            listView.ItemsSource = ((FolderFileViewModel)ViewModel).SearchResult;
        }
    }
}
