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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PICTPWApp
{
    public enum LightType
    {
        Red,
        Yellow,
        Green,
    }

    /// <summary>
    /// TrafficLight.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficLight : Window
    {
        public TrafficLight()
        {
            InitializeComponent();
        }

        private void LoadLightStatus()
        {
           Thread lightThread = new Thread(() =>
            {
                int j = 1;
                while (true)
                {
                    if (j > 3)
                    {
                        j = 1;
                    }
                    if (j == 1)
                    {
                        SwitchLightStatus(LightType.Red);
                        for (int i = 15; i > 0; --i)
                        {
                            SwitchLight(i);
                            Thread.Sleep(1000);
                        }
                    }
                    else if (j == 2)
                    {
                        SwitchLightStatus(LightType.Yellow);
                        for (int i = 3; i > 0; --i)
                        {
                            SwitchLight(i);
                            Thread.Sleep(1000);
                        }
                    }
                    else if (j == 3)
                    {
                        SwitchLightStatus(LightType.Green);
                        for (int i = 15; i > 0; --i)
                        {
                            SwitchLight(i);
                            Thread.Sleep(1000);
                        }
                    }
                    ++j;
                }
            });

            lightThread.IsBackground = true;
            lightThread.Start();
        }

        private void SwitchLight(int i) 
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                TimeTextBlock.Content = i.ToString();
            });
        }

        private void SwitchLightStatus(LightType light)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                switch (light)
                {
                    case LightType.Red:
                        RedLight.Fill = Brushes.Red;
                        YellowLight.Fill = Brushes.Gray;
                        GreenLight.Fill = Brushes.Gray;
                        Canvas.SetTop(TimeTextBlock, 0);
                        break;
                    case LightType.Green:
                        GreenLight.Fill = Brushes.Green;
                        YellowLight.Fill = Brushes.Gray;
                        RedLight.Fill = Brushes.Gray;
                        Canvas.SetTop(TimeTextBlock, 140);
                        break;
                    case LightType.Yellow:
                        GreenLight.Fill = Brushes.Gray;
                        YellowLight.Fill = Brushes.Yellow;
                        RedLight.Fill = Brushes.Gray;
                        Canvas.SetTop(TimeTextBlock, 70);
                        break;
                }
            });
           
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            LoadLightStatus();
        }
    }
}
