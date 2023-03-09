using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AstrologyApp
{
    public partial class TabMinMax : Page
    {
        public TabMinMax()
        {
            InitializeComponent();
        }

        private void VisibleColumns(DataGrid dataGrid)
        {
            foreach (var column in dataGrid.Columns)
                if (column.DisplayIndex >= 3) // Установите максимальное количество колонок здесь
                {
                    column.MaxWidth = 0;
                    column.Width = 0;
                    column.Visibility = Visibility.Collapsed;
                }
                else
                {
                    column.MaxWidth = double.PositiveInfinity;
                    column.Visibility = Visibility.Visible;
                }
        }

        private void MaxBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dataView = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);

            // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Descending));

            // Обновляем отображение данных в DataGrid
            dataView.Refresh();
        }

        private void MinBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dataView = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);

            // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Ascending));

            // Обновляем отображение данных в DataGrid
            dataView.Refresh();
        }

        private void TabMinMax_OnLoaded(object sender, RoutedEventArgs e)
        {
            var clonedTable = ExcelPath.DataTable.Clone();
            var clonedTable2 = ExcelPath.DataTable.Clone();
            foreach (DataRow row in ExcelPath.DataTable.Rows)
            {
                clonedTable.ImportRow(row);
                clonedTable2.ImportRow(row);
            }

            DataGrid.ItemsSource = clonedTable.DefaultView;
            DataGrid1.ItemsSource = clonedTable2.DefaultView;

            if (DataGrid.ItemsSource != null && DataGrid1.ItemsSource != null)
            {
                // Создаем два разных ICollectionView для каждого DataGrid
                var dataView = new CollectionViewSource { Source = DataGrid.ItemsSource }.View;
                var dataView1 = new CollectionViewSource { Source = DataGrid1.ItemsSource }.View;

                // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
                dataView.SortDescriptions.Clear();
                dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Descending));

                dataView1.SortDescriptions.Clear();
                dataView1.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Ascending));
                VisibleColumns(DataGrid);
                VisibleColumns(DataGrid1);
            }
        }

        private void BackAllBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Content = new TabSearch();
        }
    }
}