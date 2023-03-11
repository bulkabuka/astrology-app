using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ExcelDataReader;

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
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                    });
                    var dataTable = dataSet.Tables[0];
                    var fio = dataTable.Rows[0].ItemArray[3].ToString();
                    var date = dataTable.Rows[0].ItemArray[4] as DateTime?;
                    Dispatcher.Invoke(() =>
                        {
                            
                            Whore.Text =
                                $"Расчёт совместимости партнёров для: {fio}, {date?.ToString("dd/MM/yyyy, H")} часов";
                            ExcelPath.whore = Whore.Text;
                            LoadingRingText.Visibility = Visibility.Collapsed;
                            Wait_text1.Visibility = Visibility.Collapsed;
                            Wait_text2.Visibility = Visibility.Collapsed;
                        }
                    );
                    // Set the DataTable as the DataGrid's ItemsSource
                    Dispatcher.Invoke(() => DataGrid.ItemsSource = dataTable.DefaultView);
                    var column = DataGrid.Columns[1] as DataGridTextColumn;
                    if (column != null) column.Binding.StringFormat = "HH:mm";
                    var column1 = DataGrid.Columns[0] as DataGridTextColumn;
                    if (column1 != null) column1.Binding.StringFormat = "M/dd/yyyy";
                    Dispatcher.Invoke(() =>
                        {
                            CoolApplyButton.IsEnabled = true;
                            LoadingRing.Visibility = Visibility.Collapsed;
                            Wait_text1.Visibility = Visibility.Collapsed;
                            Wait_text2.Visibility = Visibility.Collapsed;
                            Title.Visibility = Visibility.Visible;
                        }
                    );
                    foreach (var column2 in DataGrid.Columns)
                        Dispatcher.Invoke(() =>
                        {
                            if (column2.DisplayIndex >= 3) // Установите максимальное количество колонок здесь
                            {
                                column2.MaxWidth = 0;
                                column2.Width = 0;
                                column2.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                column2.MaxWidth = double.PositiveInfinity;
                                column2.Visibility = Visibility.Visible;
                            }
                        });
                    Dispatcher.Invoke(() => { MinMaxBtn.IsEnabled = true; });
                }
            }
        }

        private void ApplyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ApplyBtn_OnClick1(object sender, RoutedEventArgs e)
        {
            // 1 января 1950 г.
            if (DatePick.SelectedDate == null)
            {
                MessageBox.Show("Дата не выбрана");
            }
            else
            {
                var selectedDate = (DateTime)DatePick.SelectedDate;
                var dataTable = (DataGrid.ItemsSource as DataView)?.Table.DefaultView;

                if (TimeBox.SelectedValue != null)
                {
                    var normalTime = (TimeBox.SelectedValue as ComboBoxItem)?.Content.ToString();
                    normalTime = "1899-12-31 " + (normalTime?.Length == 2 ? normalTime : "0" + normalTime) + ":00:00";
                    dataTable.RowFilter =
                        $"[Дата] = '{selectedDate.ToString("yyyy-MM-dd")}' AND [Время] = '{normalTime}'";
                }
                else
                {
                    dataTable.RowFilter = $"[Дата] = '{selectedDate.ToString("yyyy-MM-dd")}'";
                }


                DataGrid.ItemsSource = dataTable;

                Refresh.IsEnabled = true;
            }
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            FindExcel(ExcelPath.excelPath);
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
                Task.Run(async delegate { FindExcel(ExcelPath.excelPath); });

                // FindExcel(ExcelPath.excelPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Удостоверьтесь, что Excel файл находится в той же директории, что и приложение. Ознакомьтесь с инструкцией (кнопка) ",
                    "Ошибка считывания файла");
                throw new Exception("Ошибочка");
            }
        }
    }
}