using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PICTPWApp
{
    /// <summary>
    /// BorderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardWindow : Window
    {
        int angelCurrent, angleNext = 0;
        public DashboardWindow()
        {
            InitializeComponent();

            this.DrawScale();

        }

        /// <summary>
        /// 画表盘的刻度
        /// </summary>
        private void DrawScale()
        {
            for (int i = 0; i <= 180; i += 5)
            {
                //添加刻度线
                Line lineScale = new Line();

                if (i % 25 == 0)
                {
                    lineScale.X1 = 200 - 160 * Math.Cos(i * Math.PI / 180);
                    lineScale.Y1 = 200 - 160 * Math.Sin(i * Math.PI / 180);
                    lineScale.Stroke = new SolidColorBrush(Color.FromRgb(0x00, 0xFF, 0));
                    lineScale.StrokeThickness = 3;

                    //添加刻度值
                    TextBlock txtScale = new TextBlock();
                    txtScale.Text = (i).ToString();
                    txtScale.FontSize = 10;
                    if (i <= 90)//对坐标值进行一定的修正
                    {
                        Canvas.SetLeft(txtScale, 200 - 155 * Math.Cos(i * Math.PI / 180));
                    } else if (175 == i)
                    {
                        Canvas.SetLeft(txtScale, 185 - 155 * Math.Cos(i * Math.PI / 180));
                    }
                    else
                    {
                        Canvas.SetLeft(txtScale, 190 - 155 * Math.Cos(i * Math.PI / 180));
                    }
                    if (0 == i || 175 == i)
                    {
                        Canvas.SetTop(txtScale, 194 - 155 * Math.Sin(i * Math.PI / 180));
                    } else
                    {
                        Canvas.SetTop(txtScale, 200 - 155 * Math.Sin(i * Math.PI / 180));
                    }
                    this.CanvasDial.Children.Add(txtScale);
                }
                else
                {
                    lineScale.X1 = 200 - 170 * Math.Cos(i * Math.PI / 180);
                    lineScale.Y1 = 200 - 170 * Math.Sin(i * Math.PI / 180);
                    lineScale.Stroke = new SolidColorBrush(Color.FromRgb(0xFF, 0x00, 0));
                    lineScale.StrokeThickness = 1;
                }

                lineScale.X2 = 200 - 180 * Math.Cos(i * Math.PI / 180);
                lineScale.Y2 = 200 - 180 * Math.Sin(i * Math.PI / 180);

                this.CanvasDial.Children.Add(lineScale);

            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void LabelClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // Resize window width (honoring minimum width)
            var desiredWidth = (int)(ActualWidth + e.HorizontalChange);
            var minWidth = (int)(MinWidth + resizeThumb.Width + resizeThumb.Margin.Right);
            Width = Math.Max(desiredWidth, minWidth);

            // Resize window height (honoring minimum height)
            var desiredHeight = (int)(ActualHeight + e.VerticalChange);
            var minHeight = (int)(MinHeight + resizeThumb.Height + resizeThumb.Margin.Bottom);
            Height = Math.Max(desiredHeight, minHeight);
        }

        private void CanvasDial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rt = new RotateTransform();
            rt.CenterX = 200;
            rt.CenterY = 200;

            this.indicatorPin.RenderTransform = rt;

            angelCurrent = angleNext;
            Random random = new Random();
            angleNext = random.Next(180);

            double timeAnimation = Math.Abs(angelCurrent - angleNext) * 8;
            DoubleAnimation da = new DoubleAnimation(angelCurrent, angleNext, new Duration(TimeSpan.FromMilliseconds(timeAnimation)));
            da.AccelerationRatio = 1;
            da.Completed += Da_Completed;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }

        private void Da_Completed(object? sender, EventArgs e)
        {
            currentValueTxtBlock.Text = angleNext.ToString() + "km/h";
        }
    }
}
