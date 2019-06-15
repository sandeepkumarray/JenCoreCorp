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
    /// Interaction logic for Bubble.xaml
    /// </summary>
    public partial class Bubble : UserControlClass
    {
        public Bubble()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        private static DependencyProperty LabelColourProperty
        = DependencyProperty.Register("LabelColour", typeof(Brush), typeof(Bubble), new PropertyMetadata(null, OnLabelColourChanged));
        private static DependencyProperty LabelColourProperty2
        = DependencyProperty.Register("LabelColour2", typeof(Brush), typeof(Bubble), new PropertyMetadata(null, OnLabelColourChanged1));
        private static DependencyProperty Borderthickness
        = DependencyProperty.Register("StrokeThickness", typeof(Double), typeof(Bubble), new PropertyMetadata(new Double(), OnStrokeThicknessChanged));
        private static DependencyProperty _ControlHeader
        = DependencyProperty.Register("ControlHeader", typeof(string), typeof(Bubble), new PropertyMetadata("", OnControlHeaderChanged));
        private static DependencyProperty _ProfitPercentage
        = DependencyProperty.Register("ProfitPercentage", typeof(string), typeof(Bubble), new PropertyMetadata("", OnProfitPercentageChanged));
        private static DependencyProperty _Overal
        = DependencyProperty.Register("Overal", typeof(string), typeof(Bubble), new PropertyMetadata("", OnOveralChanged));
        private static DependencyProperty _Description
       = DependencyProperty.Register("Description", typeof(string), typeof(Bubble), new PropertyMetadata("", OnDescriptionChanged));
        private static DependencyProperty _ID
        = DependencyProperty.Register("ID", typeof(long), typeof(Bubble), new PropertyMetadata());
        #endregion

        #region Callback Methods
        private static void OnLabelColourChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                ctl.centerel.Fill = (Brush)e.NewValue;
            }
        }

        private static void OnLabelColourChanged1(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                ctl.centerel.Stroke = (Brush)e.NewValue;
            }
        }

        private static void OnStrokeThicknessChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                ctl.centerel.StrokeThickness = (Double)e.NewValue;
            }
        }

        private static void OnControlHeaderChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                ctl.txtHeader.Text = Convert.ToString(e.NewValue);
            }
        }

        private static void OnDescriptionChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                ctl.txtDescription.Text = Convert.ToString(e.NewValue);
            }
        }

        private static void OnProfitPercentageChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                if (Convert.ToInt32(e.NewValue) < 60)
                {
                    ctl.txtProfitper.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    ctl.txtProfitper.Foreground = new SolidColorBrush(Colors.Green);
                }
                ctl.txtProfitper.Text = Convert.ToString(e.NewValue + "%");
            }
        }

        private static void OnOveralChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Bubble ctl = source as Bubble;
            if (ctl != null)
            {
                if (Convert.ToInt32(e.NewValue) < 60)
                {
                    ctl.txtOveral.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    ctl.txtOveral.Foreground = new SolidColorBrush(Colors.Green);
                }
                ctl.txtOveral.Text = Convert.ToString(e.NewValue + "%");
            }
        }
        #endregion

        #region Properties
        public long ID
        {
            get { return (long)GetValue(_ID); }
            set { SetValue(_ID, value); }
        }

        public Brush LabelColour
        {
            get { return GetValue(LabelColourProperty) as Brush; }
            set { SetValue(LabelColourProperty, value); }
        }

        public Brush LabelColour2
        {
            get { return GetValue(LabelColourProperty2) as Brush; }
            set { SetValue(LabelColourProperty2, value); }
        }

        public Double StrokeThickness
        {
            get { return (Double)GetValue(Borderthickness); }
            set { SetValue(Borderthickness, value); }
        }

        public string ProfitPercentage
        {
            get { return (string)GetValue(_ProfitPercentage); }
            set { SetValue(_ProfitPercentage, value); }
        }

        public string Overal
        {
            get { return (string)GetValue(_Overal); }
            set { SetValue(_Overal, value); }
        }

        public string ControlHeader
        {
            get { return (string)GetValue(_ControlHeader); }
            set { SetValue(_ControlHeader, value); }
        }

        public string Description
        {
            get { return (string)GetValue(_Description); }
            set { SetValue(_Description, value); }
        }
        #endregion
    }
}
