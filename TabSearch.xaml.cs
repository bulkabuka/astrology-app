using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.IO;
using System.Windows.Documents;
using ExcelDataReader;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using Page = System.Windows.Controls.Page;

namespace AstrologyApp
{
    public partial class TabSearch : Page
    {
        public TabSearch()
        {
            InitializeComponent();
            var hoursItems = new Collection<ComboBoxItem>();
            for (var i = 0; i < 24; i++)
            {
                var item = new ComboBoxItem
                {
                    Content = i
                };
                hoursItems.Add(item);
            }

            TimeBox.ItemsSource = hoursItems;
        }

        public void FindExcel(string excelPath)
        {
            using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
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
                    CoolApplyButton.IsEnabled = true;
                    LoadingRing.Visibility = Visibility.Collapsed;
                    Title.Visibility = Visibility.Visible;
                }
            }
        }

        private void ApplyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Title.Visibility = Visibility.Hidden;
            LoadingRing.Visibility = Visibility.Visible;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ExcelPath.excelPath = openFileDialog.FileName;
                FindExcel(ExcelPath.excelPath);
            }
        }

        private void ApplyBtn_OnClick1(object sender, RoutedEventArgs e)
        {
            // 1 января 1950 г.
            if (DatePick.SelectedDate == null)
            {
                MessageBox.Show("Date is not selected!");
            }
            else
            {
                DateTime selectedDate = (DateTime)DatePick.SelectedDate;
                var dataTable = (DataGrid.ItemsSource as DataView)?.Table.DefaultView;
                var row = dataTable.Table.Rows[1];
                
                if (TimeBox.SelectedValue != null)
                {
                    string normalTime = (TimeBox.SelectedValue as ComboBoxItem)?.Content.ToString();
                    normalTime = "1899-12-31 " + (normalTime.Length == 2 ? normalTime : "0" + normalTime) + ":00:00";
                    dataTable.RowFilter =
                        $"[Дата] = '{selectedDate.Date.ToString("yyyy-MM-dd")}' AND [Время] = '{normalTime}'";
                }
                else
                    dataTable.RowFilter = $"[Дата] = '{selectedDate.Date.ToString("yyyy-MM-dd")}'";


                DataGrid.ItemsSource = dataTable;

                Refresh.IsEnabled = true;
            }
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
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

        private void MinMaxBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ExcelPath.DataTable = ((DataView)DataGrid.ItemsSource).Table;
            if (ExcelPath.excelPath == null)
            {
                MessageBox.Show("Не выбран файл таблицы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Navigator.frame.Content = new TabMinMax();
        }

        private void TabSearch_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                ExcelPath.excelPath = Path.Combine(exePath, "AstrologyExcel.xlsx");
               FindExcel(ExcelPath.excelPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Удостоверьтесь, что excel файл находится в той же директории, что и приложение. Ознакомьтесь с инструкцией(кнопка) " +
                                "или откройте файл вручную",
                    "Ошибка считывания файла! ");
            }
        }
    }
}