using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PICTPWApp
{
    /// <summary>
    /// WindMill.xaml 的交互逻辑
    /// </summary>
    public partial class WindMill : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public WindMill()
        {
            InitializeComponent();

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(0.001);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (rotateTransform.Angle > 360)
            {
                rotateTransform.Angle = 0;
            } else
            {
                rotateTransform.Angle += 1;
            }

            if (rotateTransform1.Angle > 360)
            {
                rotateTransform1.Angle = 0;
            }
            else
            {
                rotateTransform1.Angle += 1;
            }

            if (rotateTransform2.Angle > 360)
            {
                rotateTransform2.Angle = 0;
            }
            else
            {
                rotateTransform2.Angle += 1;
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }
    }
}
