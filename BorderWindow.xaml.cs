using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PICTPWApp
{
    /// <summary>
    /// BorderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BorderWindow : Window
    {
        public BorderWindow()
        {
            InitializeComponent();
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
    }
}
