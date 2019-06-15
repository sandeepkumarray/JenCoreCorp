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
using DragCanvas;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for WeatherTile.xaml
    /// </summary>
    public partial class WeatherTile : UserControlClass,IDragUserControl
    {
        public WeatherTile()
        {
            InitializeComponent();
        }

        #region IGadget Members
        public void OnShowOptions(OptionButtonTypes type)
        {
            if (type.Equals(OptionButtonTypes.Settings))
            {
               
            }
        }
        #endregion
    }
}
