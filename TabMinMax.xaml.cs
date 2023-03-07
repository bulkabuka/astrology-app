using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ExcelDataReader;

namespace AstrologyApp
{
    public partial class TabMinMax : Page
    {
        public TabMinMax()
        {
            InitializeComponent();
        }

        private void MaxBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);

            // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Descending));

            // Обновляем отображение данных в DataGrid
            dataView.Refresh();
        }

        private void MinBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);

            // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Ascending));

            // Обновляем отображение данных в DataGrid
            dataView.Refresh();
        }

        private void TabMinMax_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataTable clonedTable = ExcelPath.DataTable.Clone();
            foreach (DataRow row in ExcelPath.DataTable.Rows)
            {
                clonedTable.ImportRow(row);
            }
            DataGrid.ItemsSource = clonedTable.DefaultView;
            DataGrid1.ItemsSource = clonedTable.DefaultView;
            if (DataGrid.ItemsSource != null && DataGrid1.ItemsSource != null)
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(DataGrid.ItemsSource);
                ICollectionView dataView1 = CollectionViewSource.GetDefaultView(DataGrid1.ItemsSource);
                // Добавляем сортировку по убыванию столбца (замените propertyName на имя вашего столбца)
                dataView.SortDescriptions.Clear();
                dataView1.SortDescriptions.Clear();
                dataView.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Descending));
                dataView1.SortDescriptions.Add(new SortDescription("Условные единицы", ListSortDirection.Ascending));
                // Обновляем отображение данных в DataGrid
                dataView.Refresh();
                dataView1.Refresh();
            }
        }

        private void BackAllBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.frame.Content = new TabSearch();
        }
    }
}