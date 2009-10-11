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
using System.Windows.Shapes;

namespace CubeExercise
{
    /// <summary>
    /// Interaction logic for GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        private Group root;

        public Group SelectedGroup { get; set; }

        private GroupWindow()
        {
            InitializeComponent();
        }

        public GroupWindow(Group root, string referenceName) : this()
        {
            this.root = root;
            this.Title = string.Format("为公式{0}选择目标组", referenceName);

            TreeViewItem tempRoot = new TreeViewItem();
            this.GetSubTreeItems(this.root, tempRoot);

            // Transfer the sub items from the TreeViewItem to the TreeView.
            while (tempRoot.Items.Count > 0)
            {
                // One TreeViewItem can have only one logical parent. So the current
                // parent must remove it from its children.
                object subItem = tempRoot.Items[0];
                tempRoot.Items.RemoveAt(0);
                this.tvGroups.Items.Add(subItem);
            }
        }

        private TreeViewItem GetSubTreeItems(Group group, TreeViewItem tvItem)
        {
            {
                // Initialize the node itself.
                StackPanel panel = new StackPanel();
                panel.BeginInit();
                tvItem.Header = panel;
                tvItem.DataContext = group;
                tvItem.IsExpanded = true;
                tvItem.MouseDoubleClick += new MouseButtonEventHandler(treeViewItem_MouseDoubleClick);

                panel.HorizontalAlignment = HorizontalAlignment.Left;
                panel.VerticalAlignment = VerticalAlignment.Center;
                panel.Orientation = Orientation.Horizontal;

                TextBlock tb = new TextBlock() { VerticalAlignment = VerticalAlignment.Center };
                // Use parent(TreeViewItem)'s datacontext.
                tb.SetBinding(TextBlock.TextProperty, "Name");
                panel.Children.Add(tb);
                panel.EndInit();
            }

            if (group.Items == null || group.Items.Count() <= 0)
            {
                return tvItem;
            }

            // Initialize the subnodes.
            foreach (object item in group.Items)
            {
                if (item.GetType() == typeof(Group))
                {
                    Group g = (Group)item;

                    TreeViewItem subItem = new TreeViewItem();
                    tvItem.Items.Add(subItem);
                    this.GetSubTreeItems(g, subItem);
                }
            }

            return tvItem;
        }

        void treeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ConfirmSelection();
            e.Handled = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ConfirmSelection();
        }

        private void ConfirmSelection()
        {
            if (this.Visibility == Visibility.Hidden)
            {
                // This window has been closed.
                return;
            }

            if (this.tvGroups.SelectedItem == null)
            {
                MessageBox.Show("请选择一个公式组。", "错误！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TreeViewItem item = this.tvGroups.SelectedItem as TreeViewItem;
            if (item != null)
            {
                this.SelectedGroup = item.DataContext as Group;
            }

            this.DialogResult = true;
            this.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
