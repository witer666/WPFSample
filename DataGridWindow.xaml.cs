using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PICTPWApp
{

    public class ItemEntity
    {
        public int Col1 { get; set; }
        public int Col2 { get; set; }
        public int Col3 { get; set; }
        public int Col4 { get; set; }
        public int Col5 { get; set; }
    }

    /// <summary>
    /// DataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridWindow : Window
    {
        public DataGridWindow()
        {
            InitializeComponent();

            ItemSourceData();
        }

        private void ItemSourceData()
        {
            List<ItemEntity> list = new List<ItemEntity>();
            for(int i = 0; i < 30; i++)
            {
                ItemEntity entity = new ItemEntity();
                entity.Col1 = i;
                entity.Col2 = i;
                entity.Col3 = i;
                entity.Col4 = i;
                entity.Col5 = i;
                list.Add(entity);
            }

            DataGridList.ItemsSource = list;
        }
    }
}
