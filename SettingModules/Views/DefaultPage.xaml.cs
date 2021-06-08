using SettingModules.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SettingModules.Views
{
    /// <summary>
    /// DefaultPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DefaultPage : UserControl
    {
        public delegate Point GetPosition(IInputElement element);
        int rowIndex = -1;

        public DefaultPage()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rowIndex = GetCurrentRowIndex(e.GetPosition);
            if (rowIndex < 0)
                return;
            MoniDataGrid.SelectedIndex = rowIndex;
            ChMoniList selectedItem = MoniDataGrid.Items[rowIndex] as ChMoniList;
            if (selectedItem == null)
                return;
            DragDropEffects dragdropeffects = DragDropEffects.Move;
            if (DragDrop.DoDragDrop(MoniDataGrid, selectedItem, dragdropeffects)
                                != DragDropEffects.None)
            {
                MoniDataGrid.SelectedItem = selectedItem;
            }
        }

        private void DataGrid_Drop(object sender, DragEventArgs e)
        {
            if (rowIndex < 0)
                return;
            int index = GetCurrentRowIndex(e.GetPosition);
            if (index < 0)
                return;
            if (index == rowIndex)
                return;
            //if (index == MoniDataGrid.Items.Count - 1)
            //{
            //    MessageBox.Show("This row-index cannot be drop");
            //    return;
            //}

            ObservableCollection<ChMoniList> collection = (ObservableCollection<ChMoniList>)MoniDataGrid.ItemsSource;
            ChMoniList moniList = collection[rowIndex];
            collection.RemoveAt(rowIndex);
            collection.Insert(index, moniList);

            for (int i = 0; i < collection.Count; i++)
            {
                ChMoniList temp = collection[i];
                temp.No = i + 1;
                collection.RemoveAt(i);
                collection.Insert(i, temp);
            }
        }

        private bool GetMouseTargetRow(Visual theTarget, GetPosition position)
        {
            Rect rect = VisualTreeHelper.GetDescendantBounds(theTarget);
            Point point = position((IInputElement)theTarget);
            return rect.Contains(point);
        }

        private DataGridRow GetRowItem(int index)
        {
            if (MoniDataGrid.ItemContainerGenerator.Status
                    != GeneratorStatus.ContainersGenerated)
                return null;
            return MoniDataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
        }

        private int GetCurrentRowIndex(GetPosition pos)
        {
            int curIndex = -1;
            for (int i = 0; i < MoniDataGrid.Items.Count; i++)
            {
                DataGridRow itm = GetRowItem(i);
                if (GetMouseTargetRow(itm, pos))
                {
                    curIndex = i;
                    break;
                }
            }
            return curIndex;
        }
    }
}
