using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;

namespace AstrologyApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public List<ReferenceItem> AstrologyData = new List<ReferenceItem>();

        public MainWindow()
        {
            InitializeComponent();
            SampleItems();
        }

        public void SampleItems()
        {
            var date = new DateTime(2004, 12, 29).ToShortDateString();
            var time = new TimeSpan(9, 28, 30);
            AstrologyData.Add(new ReferenceItem(date, time, 920121));
            Data.ItemsSource = AstrologyData;
        }


        private void PickerFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == true)
            {
                var excelApp = new ApplicationClass();
                excelApp.Visible = false;
                var workbook = excelApp.Workbooks.Open(openFileDialog.FileName);
                var worksheet = workbook.Sheets[1];
                Data.ItemsSource = worksheet as Collection<DataRow>;

                workbook.Close(false);
                excelApp.Quit();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PickerFile_OnClick(sender, e);
        }

        public class ReferenceItem
        {
            public ReferenceItem(string date, TimeSpan time, int conditionalUnits)
            {
                Date = date;
                Time = time;
                ConditionalUnits = conditionalUnits;
            }

            public string Date { get; set; }
            public TimeSpan Time { get; set; }
            public int ConditionalUnits { get; set; }
        }
    }
}