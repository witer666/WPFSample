using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <summary>
    /// CircleCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class CircleCanvas : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public CircleCanvas()
        {
            InitializeComponent();

            this.DrawWatch();
        }

        private void DrawWatch()
        {
            double backWidth = BackEllipse.Width;
            double radius = backWidth / 2;

            double min = 0; double max = 60;
            double step = 360.0 / (max - min);

            for (int i = 0; i < max - min; i++)
            {
                this.DrawEvent(radius, i, step);
            }

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (rotateTransform.Angle > 360)
            {
                rotateTransform.Angle = 0;
            }
            else
            {
                rotateTransform.Angle += 6;
            }
        }

        private void DrawEvent(double radius, int i, double step)
        {
            this.Dispatcher.InvokeAsync(new Action(() =>
           {
               int lineLength = ((i % 5 == 0) ? 25 : 15);
               Line lineScale = new Line
               {
                   X1 = ((radius - lineLength) * Math.Cos(i * step * Math.PI / 180)) + radius,
                   Y1 = ((radius - lineLength) * Math.Sin(i * step * Math.PI / 180)) + radius,
                   X2 = (radius * Math.Cos(i * step * Math.PI / 180)) + radius,
                   Y2 = (radius * Math.Sin(i * step * Math.PI / 180)) + radius,
                   Stroke = Brushes.Gray,
                   StrokeThickness = ((i % 5 == 0) ? 2 : 1),
               };
               MainCanvas.Children.Add(lineScale);

               //增加小时数字
               if (i % 5 == 0)
               {
                   Label lblHour = new Label();
                   lblHour.VerticalContentAlignment = VerticalAlignment.Top;
                   lblHour.HorizontalContentAlignment = HorizontalAlignment.Left;
                   int hour = (i / 5 + 3);
                   hour = hour > 12 ? (hour % 12) : hour;
                   lblHour.Content = hour.ToString();

                   //小时坐标
                   if (hour <= 3)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 12);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 15);
                   }
                   else if (hour == 4)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 15);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 15);
                   }
                   else if(hour == 5)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 18);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 12);
                   }
                   else if (hour == 6) 
                   { 
                       Canvas.SetTop(lblHour, lineScale.Y1 - 20);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 9);
                   }
                   else if (hour == 7)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 20);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 5);
                   }
                   else if (hour == 8)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 16);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 4);
                   }
                   else if (hour == 9)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 15);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 4);
                   }
                   else if (hour == 10)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 12);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 5);
                   }
                   else if (hour == 11)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 10);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 5);
                   }
                   else if (hour == 12)
                   {
                       Canvas.SetTop(lblHour, lineScale.Y1 - 5);
                       Canvas.SetLeft(lblHour, lineScale.X1 - 12);
                   }

                   MainCanvas.Children.Add(lblHour);
               }
           }));
        }
    }
}
