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
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for TileItemSquare.xaml
    /// </summary>
    public partial class TileItemSquare : UserControlClass
    {
        private DispatcherTimer tiletimer;
        public TileItemSquare()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(TileItemSquare_Loaded);
        }

        private bool slide = false;
        Random rndm = new Random();
        public readonly static DependencyProperty TitleTitleProperty = DependencyProperty.Register("TitleTitle", typeof(string), typeof(TileItemSquare), new PropertyMetadata(""));
        public string TitleTitle
        {
            get { return (string)GetValue(TitleTitleProperty); }
            set
            {
                SetValue(TitleTitleProperty, value);
            }
        }

        public readonly static DependencyProperty CanSlideProperty = DependencyProperty.Register("CanSlide", typeof(bool), typeof(TileItemSquare), new PropertyMetadata(true));
        public bool CanSlide
        {
            get { return (bool)GetValue(CanSlideProperty); }
            set
            {
                SetValue(CanSlideProperty, value);
            }
        }

        void TileItemSquare_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.CanSlide)
            {
                tiletimer = new DispatcherTimer();
                tiletimer.Start();
                tiletimer.Tick += new EventHandler(tiletimer_Tick);
            }
        }

        void tiletimer_Tick(object sender, EventArgs e)
        {
            Storyboard storyboard = null;
            if (!slide)
            {
                //storyboard = Application.Current.Resources["Storyboard1"] as Storyboard;
                storyboard = this.Resources["Storyboard1"] as Storyboard;
                slide = true;
            }
            else
            {
                //storyboard = Application.Current.Resources["Storyboard2"] as Storyboard;
                storyboard = this.Resources["Storyboard2"] as Storyboard;
                slide = false;
            }
            try
            {
                storyboard.Begin();
            }
            catch (Exception ee)
            {
                string error = ee.Message;
            }
            tiletimer.Interval = new TimeSpan(0, 0, rndm.Next(3, 10));
        }

        private void tileView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void tileView_MouseEnter(object sender, MouseEventArgs e)
        {
            HoverIcon.Visibility = Visibility.Visible;
            RealIcon.Visibility = Visibility.Collapsed;
        }

        private void tileView_MouseLeave(object sender, MouseEventArgs e)
        {
            HoverIcon.Visibility = Visibility.Collapsed;
            RealIcon.Visibility = Visibility.Visible;
        }
    }
}
