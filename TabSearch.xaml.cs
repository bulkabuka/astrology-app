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

            ComboBox.ItemsSource = hoursItems;
        }
        private void ApplyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                ExcelPath.excelPath = openFileDialog.FileName;
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
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
        private void ApplyBtn_OnClick1(object sender, RoutedEventArgs e)
        {
            if (DatePick.SelectedDate == null)
            {
                MessageBox.Show("Date is not selected!");
            }
            if (DatePick.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)DatePick.SelectedDate;
                //Refresh.IsEnabled = true;
                
                // Проходимся по всем строкам в DataGrid
                foreach (DataRowView row in DataGrid.Items)
                {
                    DateTime rowDate = (DateTime)row["Дата"];

                    // Если дата строки соответствует выбранной дате, то отображаем строку
                    if (rowDate.Date == selectedDate.Date)
                    {
                        row.Row.Table.DefaultView.RowFilter = $"[Дата] = '{selectedDate.Date.ToString("yyyy-MM-dd")}'";
                        // Если в таблице используется фильтрация, то нужно сбросить ее, чтобы отобразить все строки
                        DataGrid.Items.Refresh();
                        return;
                    }

                    /*if (ComboBox.SelectedItem != null)
                    {
                        var selectedTime = ComboBox.SelectedValue as string;
                        var SelectedItem = new TimeSpan(0,Convert.ToInt32(selectedTime),0,0);
                        foreach (DataRowView row1 in DataGrid.Items)
                        {
                            var rowTime = (TimeSpan)row["Время"];
                            if (rowTime.Hours == SelectedItem.Hours)
                            {
                                row.Row.Table.DefaultView.RowFilter = $"[Время] = '{rowTime:HH:mm:ss}'";
                                DataGrid.Items.Refresh();
                                return;
                            }
                        }
                    }*/
                }
                
                DataGrid.ItemsSource = null;
            }
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            /*using (var stream = File.Open(ExcelPath.excelPath, FileMode.Open, FileAccess.Read))
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
            }*/
        }
    }
}