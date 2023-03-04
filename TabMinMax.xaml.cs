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
            if (ExcelPath.excelPath == null)
            {
                MessageBox.Show("Не выбран файл таблицы.", "Ошибка");
                
                return;
            }
            else
            {
                using (var stream = File.Open(ExcelPath.excelPath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Read the data from the first worksheet
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                        });
                        var dataTable = dataSet.Tables[0];

                        // Set the DataTable as the DataGrid's ItemsSource
                        DataGrid.ItemsSource = dataTable.DefaultView;
                        var column = DataGrid.Columns[1] as DataGridTextColumn;
                        if (column != null) column.Binding.StringFormat = "HH:mm";
                        var column1 = DataGrid.Columns[0] as DataGridTextColumn;
                        if (column1 != null) column1.Binding.StringFormat = "M/dd/yyyy";
                    }
                }
            }
        }
    }
}