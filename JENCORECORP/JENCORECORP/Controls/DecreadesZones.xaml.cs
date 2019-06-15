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
using System.Collections.ObjectModel;
using System.IO;
using RDCore;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for DecreadesZones.xaml
    /// </summary>
    public partial class DecreadesZones : UserControlClass
    {
        object ItemsPanelItem;
        ZoneViewModel objZoneViewModel;
        ZonesDAL zoneDAL;
        
        public DecreadesZones()
        {
            InitializeComponent();
            objZoneViewModel = new ZoneViewModel();
            Loaded += new RoutedEventHandler(DecreadesZones_Loaded);
        }

        void DecreadesZones_Loaded(object sender, RoutedEventArgs e)
        {
            zoneDAL = new ZonesDAL(Library.JENDBManager);
            ReadDecreadesZone();            
        }

        private void ReadDecreadesZone()
        {
            //var fullFileName = Library.GetRootDirectory() + "\\" + Library.GetXMLDataFile();

            //if (System.IO.File.Exists(fullFileName))
            //{
            //    TextReader Treader = null;
            //    Treader = new StreamReader(fullFileName);
            //    objZoneViewModel = (ZoneViewModel)XmlUtility.FromXml<ZoneViewModel>(Treader);
            //    AllZonesItems.DataContext = null;
            //    AllZonesItems.DataContext = objZoneViewModel;
            //}
            //if (AllZonesItems.DataContext != null)
            //{
            //    AllZonesItems.Items.Clear();
            //    foreach (var it in ((ZoneViewModel)AllZonesItems.DataContext).ListZones)
            //    {
            //        Bubble bbl = new Bubble()
            //        {
            //            Height = it.Height,
            //            Width = it.Width,
            //            LabelColour = new SolidColorBrush(it.LabelColour),
            //            LabelColour2 = new SolidColorBrush(it.LabelColour2),
            //            StrokeThickness = it.StrokeThickness,
            //            ControlHeader = it.ControlHeader,
            //            ProfitPercentage = it.ProfitPercentage,
            //            Overal = it.Overal,
            //            Description = it.Description
            //        };
            //        bbl.MouseDoubleClick += new MouseButtonEventHandler(bbl_MouseDoubleClick);
            //        AllZonesItems.Items.Add(bbl);
            //    }
            //}

            List<Zones> AllZonesList = zoneDAL.GetAllJenZones();
            if (AllZonesList.Count > 0)
            {
                AllZonesItems.Items.Clear();
                foreach (var it in AllZonesList)
                {
                    Bubble bbl = new Bubble()
                    {
                        ID = it.ZoneId,
                        Height = it.Height,
                        Width = it.Width,
                        LabelColour = new SolidColorBrush(it.LabelColour),
                        LabelColour2 = new SolidColorBrush(it.LabelColour2),
                        StrokeThickness = it.StrokeThickness,
                        ControlHeader = it.ControlHeader,
                        ProfitPercentage = it.ProfitPercentage,
                        Overal = it.Overal,
                        Description = it.Description
                    };
                    bbl.MouseDoubleClick += new MouseButtonEventHandler(bbl_MouseDoubleClick);
                    AllZonesItems.Items.Add(bbl);
                }
            }
        }

        void bbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _ZoneschildWindow.Content = new AddDecreadesZone(sender);
            _ZoneschildWindow.Closed += new EventHandler(_ZoneschildWindow_Closed);
            _ZoneschildWindow.Show();
        }

        private void MnAddDecreadesZone_Click(object sender, RoutedEventArgs e)
        {
            _ZoneschildWindow.Content = new AddDecreadesZone();
            _ZoneschildWindow.Closed += new EventHandler(_ZoneschildWindow_Closed);
            _ZoneschildWindow.Show();
        }

        void _ZoneschildWindow_Closed(object sender, EventArgs e)
        {
            ReadDecreadesZone();
        }
    }
}
