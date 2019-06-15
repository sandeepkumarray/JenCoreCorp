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
    /// Interaction logic for WeatherDashboard.xaml
    /// </summary>
    public partial class WeatherDashboard : UserControlClass
    {
        public WeatherDashboard()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(WeatherDashboard_Loaded);
            
        }

        void WeatherDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            ReadWeatherDetails();
        }

        private void ReadWeatherDetails()
        {

        }

        private void AddWeatherTile()
        {
            var gadgetContainer = new DragUserControlContent();
            Canvas.SetTop(gadgetContainer, 10);
            Canvas.SetLeft(gadgetContainer, 10);
            gadgetContainer.OptionButtonType = OptionButtonTypes.Settings;
            //gadgetContainer.Close += OnGadgetClose;
            gadgetContainer.IsCloseButtonVisible = false;
            var weatherTile = new WeatherTile();
            gadgetContainer.Gadget = weatherTile;
        }
    }
}
