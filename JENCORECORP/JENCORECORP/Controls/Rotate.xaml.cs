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
    /// Interaction logic for Rotate.xaml
    /// </summary>
    public partial class Rotate : UserControlClass
    {
        int count = 0;
        int rnd;
        DispatcherTimer timer = new DispatcherTimer();
        public Rotate()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new System.EventHandler(timer_Tick);
            Loaded += new RoutedEventHandler(Rotate_Loaded);
        }

        private static DependencyProperty RotationTypeProperty
       = DependencyProperty.Register("RotationType", typeof(string), typeof(Rotate), new PropertyMetadata(""));

        public string RotationType
        {
            get { return (string)GetValue(RotationTypeProperty); }
            set { SetValue(RotationTypeProperty, value); }
        }

        void Rotate_Loaded(object sender, RoutedEventArgs e)
        {
            rnd = new Random().Next(10, 20);
            Storyboard sb;
            if (RotationType == "AC")
                sb = (Storyboard)logo.FindResource("spinAC");
            else
                sb = (Storyboard)logo.FindResource("spin");
            logo.DataContext = new Duration(new TimeSpan(0, 0, 10));
            sb.Begin();
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            count++;
            Storyboard sb;
            if (RotationType == "AC")
                sb = (Storyboard)logo.FindResource("spinAC");
            else
                sb = (Storyboard)logo.FindResource("spin");
            if (count >= rnd / 4 * 3)
            {
                sb.Pause();
                logo.DataContext = new Duration(new TimeSpan(0, 0, 20));
                sb.Begin();
            }
            else if (count >= rnd / 2)
            {
                sb.Pause();
                logo.DataContext = new Duration(new TimeSpan(0, 0, 15));
                sb.Begin();
            }
            else if (count >= rnd / 4)
            {
                sb.Pause();
                logo.DataContext = new Duration(new TimeSpan(0, 0, 12));
                sb.Begin();
            }
            if (count == rnd)
            {
                count = 0;
                //Storyboard sb = (Storyboard)logo.FindResource("spin");
                //sb.Pause();
                //timer.Stop();
            }
        }      
    }
}
