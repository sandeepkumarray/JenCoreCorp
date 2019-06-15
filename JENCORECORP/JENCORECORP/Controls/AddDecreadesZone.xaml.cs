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
using RDCore;
using System.Collections.ObjectModel;
using System.IO;
using System.Data;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for AddDecreadesZone.xaml
    /// </summary>
    public partial class AddDecreadesZone : UserControlClass
    {
        string XMLPath = string.Empty;
        ZoneViewModel ZonesViewModle;
        ColorConverter objColorConverter;
        bool IsItemEdit = false;
        long ZoneId;
        ZonesDAL zoneDAL = new ZonesDAL(Library.JENDBManager);

        public AddDecreadesZone()
        {
            InitializeComponent();
            ReadMasterData();
        }

        public AddDecreadesZone(object sender)
        {
            InitializeComponent();
            ReadMasterData();
            // TODO: Complete member initialization
            var ItemZone = (Bubble)sender;
            tbZnControlHeader.IsReadOnly = true;
            ZoneId = ItemZone.ID;
            IsItemEdit = true;
            btnAdd.Content = "Update";
            tbZnHeight.Text = Convert.ToString(ItemZone.Height);
            tbZnWidth.Text = Convert.ToString(ItemZone.Width);
            tbZnControlHeader.Text = ItemZone.ControlHeader;
            tbZnDescription.Text = ItemZone.Description;
            cbZnLabelColour.SelectedValue = Library.GetColorName(((SolidColorBrush)(ItemZone.LabelColour)).Color);
            cbZnLabelColour2.SelectedValue = Library.GetColorName(((SolidColorBrush)(ItemZone.LabelColour2)).Color);
            tbZnOveral.Text = ItemZone.Overal;
            tbZnProfitPercentage.Text = ItemZone.ProfitPercentage;
            tbZnStrokeThickness.Text = Convert.ToString(ItemZone.StrokeThickness);
        }        

        private void ReadMasterData()
        {
            cbZnLabelColour.ItemsSource = typeof(Colors).GetProperties();
            cbZnLabelColour.SelectedValuePath = "Name";
            cbZnLabelColour.SelectedIndex = 0;
            cbZnLabelColour2.ItemsSource = typeof(Colors).GetProperties();
            cbZnLabelColour2.SelectedValuePath = "Name";
            cbZnLabelColour2.SelectedIndex = 0;
            objColorConverter = new ColorConverter();
            ZonesViewModle = new ZoneViewModel();
            XMLPath = Library.GetRootDirectory() + "\\" + Library.GetXMLDataFile();

            if (System.IO.File.Exists(XMLPath))
            {
                TextReader Treader = null;
                Treader = new StreamReader(XMLPath);
                ZonesViewModle = (ZoneViewModel)XmlUtility.FromXml<ZoneViewModel>(Treader);
                Treader.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //ZonesViewModle = new ZoneViewModel();
            //XMLPath = Library.GetRootDirectory() + "\\" + Library.GetXMLDataFile();
            //ZonesViewModle.ListZones = new ObservableCollection<Zones>();

            Zones ItemZone = new Zones();
            {
                ItemZone.ZoneId = ZoneId;
                ItemZone.Height = Convert.ToDouble(tbZnHeight.Text);
                ItemZone.Width = Convert.ToDouble(tbZnWidth.Text);
                ItemZone.ControlHeader = tbZnControlHeader.Text;
                ItemZone.Description = tbZnDescription.Text;
                ItemZone.LabelColour = (Color)objColorConverter.ConvertFromInvariantString((cbZnLabelColour).SelectedValue.ToString());
                ItemZone.LabelColour2 = (Color)objColorConverter.ConvertFromInvariantString((cbZnLabelColour2).SelectedValue.ToString());
                ItemZone.Overal = tbZnOveral.Text;
                ItemZone.ProfitPercentage = tbZnProfitPercentage.Text;
                ItemZone.StrokeThickness = Convert.ToDouble(tbZnStrokeThickness.Text);
                ItemZone.ZoneName = tblZnName.Text;
            }
            ClearDataFromField(true);

            if (IsItemEdit)
            {
                bool Result = false;
                //var Item = ZonesViewModle.ListZones.First(p => p.ControlHeader == ItemZone.ControlHeader);
                //if (Item != null)
                //{
                //    //Item.ControlHeader = ItemZone.ControlHeader;
                //    Item.Description = ItemZone.Description;
                //    Item.LabelColour = ItemZone.LabelColour;
                //    Item.LabelColour2 = ItemZone.LabelColour2;
                //    Item.Overal = ItemZone.Overal;
                //    Item.ProfitPercentage = ItemZone.ProfitPercentage;
                //    Item.StrokeThickness = ItemZone.StrokeThickness;
                //    Item.Height = ItemZone.Height;
                //    Item.Width = ItemZone.Width;
                //}

                //var fullFileName = System.IO.Path.Combine(XMLPath);
                //System.IO.File.WriteAllText(fullFileName, XmlUtility.ToXml(ZonesViewModle));
                Result = zoneDAL.UpdateZone(ItemZone);
                if (Result)
                {
                    MessageBox.Show("Zone Details Updated Successfully");
                }
                else
                {
                    MessageBox.Show("Error While Updating Zone Details");
                }
            }
            else
            {
                bool Result = false; 
                Result = zoneDAL.SaveZone(ItemZone);
                if (Result)
                {
                    MessageBox.Show("Zone Details Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Error While Saving Zone Details");
                }
                //ZonesViewModle.ListZones.Add(ItemZone);
                //var fullFileName = System.IO.Path.Combine(XMLPath);
                //System.IO.File.WriteAllText(fullFileName, XmlUtility.ToXml(ZonesViewModle));
            }
        }

        private void ClearDataFromField(bool para)
        {
            if (para)
            {
                tbZnHeight.Text = string.Empty;
                tbZnWidth.Text = string.Empty;
                tbZnControlHeader.Text = string.Empty;
                tbZnDescription.Text = string.Empty;
                cbZnLabelColour.SelectedValue = string.Empty;
                cbZnLabelColour2.SelectedValue = string.Empty;
                tbZnOveral.Text = string.Empty;
                tbZnProfitPercentage.Text = string.Empty;
                tbZnStrokeThickness.Text = string.Empty;
            }
        }
    }
}
