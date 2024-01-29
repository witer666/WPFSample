using Irony;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ItemEntity
    {
        public string Name { get; set; } = "";
    }
    public class TreeItemEntity
    {
        public string ItemName { get; set; } = "";
        public bool IsChecked { get; set; } = false;
        public List<TreeItemEntity> Childs { get; set; } = new List<TreeItemEntity>();
    }

    /// <summary>
    /// DataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridWindow : Window
    {
        public ObservableCollection<ItemEntity> CollectItemEntity { get; set; }
        public DataGridWindow()
        {
            InitializeComponent();

            ItemSourceData();
        }

        private void ItemSourceData()
        {
            CollectItemEntity = new ObservableCollection<ItemEntity>();
            CollectItemEntity.Add(new ItemEntity { Name = "Home"});
            CollectItemEntity.Add(new ItemEntity { Name = "Media"});
            CollectItemEntity.Add(new ItemEntity { Name = "Backup"});
            CollectItemEntity.Add(new ItemEntity { Name = "Blog"});

            DataGridList.ItemsSource = CollectItemEntity;

            List<TreeItemEntity> treeItemEntities = new List<TreeItemEntity>();
            for(int j = 0; j < 5; j++)
            {
                TreeItemEntity entity = new TreeItemEntity();
                entity.ItemName = "TreeItem" + j.ToString();

                List<TreeItemEntity> treeItemChilds = new List<TreeItemEntity>();
                for (int m = 0; m < 2; m++)
                {
                    TreeItemEntity entityChild = new TreeItemEntity();
                    entityChild.ItemName = "TreeItemChild" + m.ToString();
                    treeItemChilds.Add(entityChild);
                }
                entity.Childs = treeItemChilds;

                treeItemEntities.Add(entity);
            }

            TreeViewDrop.ItemsSource = treeItemEntities;
        }

        public static readonly DependencyProperty DraggedItemProperty =
            DependencyProperty.Register("DraggedItem", typeof(ItemEntity), typeof(DataGridWindow));

        public ItemEntity DraggedItem
        {
            get { return (ItemEntity)GetValue(DraggedItemProperty); }
            set { SetValue(DraggedItemProperty, value); }
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            ItemEntity item = DataGridList.SelectedItem as ItemEntity;
            if (item == null)
            {
                return;
            }
            int index = CollectItemEntity.IndexOf(item);
            if (index == 0)
                return;
            CollectItemEntity.RemoveAt(index);
            index = index - 1;
            CollectItemEntity.Insert(index, item);
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            ItemEntity item = DataGridList.SelectedItem as ItemEntity;
            if (item == null)
            {
                return;
            }
            int index = CollectItemEntity.IndexOf(item);
            if (index == DataGridList.Items.Count - 1)
                return;
            CollectItemEntity.RemoveAt(index);
            index = index + 1;
            CollectItemEntity.Insert(index, item);
        }

        public bool IsEditing { get; set; }

        private void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
            if (IsDragging) ResetDragDrop();
        }

        private void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            IsEditing = false;
        }

        public bool IsDragging { get; set; }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsEditing) return;

            var row = UIHelpers.TryFindFromPoint<DataGridRow>((UIElement)sender, e.GetPosition(DataGridList));
            if (row == null || row.IsEditing) return;

            IsDragging = true;
            DraggedItem = (ItemEntity)row.Item;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsDragging || IsEditing)
            {
                return;
            }

            if (DraggedItem != null)
            {
                Point position = e.GetPosition(TreeViewDrop);
                TreeViewItem row = UIHelpers.TryFindFromPoint<TreeViewItem>(TreeViewDrop, position);
                if (row != null)
                {
                    TreeItemEntity treeItemEntity = row.DataContext as TreeItemEntity;
                    if (treeItemEntity != null)
                    {
                        TreeItemEntity treeItemEntitySel = new TreeItemEntity();
                        treeItemEntitySel.ItemName = DraggedItem.Name;
                        treeItemEntity.Childs.Add(treeItemEntitySel);
                        TreeViewDrop.Items.Refresh();
                    }
                }
            }

            ResetDragDrop();
        }

        private void ResetDragDrop()
        {
            IsDragging = false;
            popup1.IsOpen = false;
            DataGridList.IsReadOnly = false;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging || e.LeftButton != MouseButtonState.Pressed) return;

            if (!popup1.IsOpen)
            {
                DataGridList.IsReadOnly = true;
                popup1.IsOpen = true;
            }


            Size popupSize = new Size(popup1.ActualWidth, popup1.ActualHeight);
            popup1.PlacementRectangle = new Rect(e.GetPosition(this), popupSize);

            Point position = e.GetPosition(DataGridList);
            var row = UIHelpers.TryFindFromPoint<DataGridRow>(DataGridList, position);
            if (row != null) DataGridList.SelectedItem = row.Item;
        }
    }
}