using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.TextFormatting;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace AstrologyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public class ReferenceItem
        {
            public string Date { get; set; }
            public TimeSpan Time { get; set; }
            public int ConditionalUnits { get; set; }

            public ReferenceItem(string date, TimeSpan time, int conditionalUnits)
            {
                this.Date = date;
                this.Time = time;
                this.ConditionalUnits = conditionalUnits;
            }
        }

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
                var excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                excelApp.Visible = false;
                var workbook = excelApp.Workbooks.Open(openFileDialog.FileName);
                var worksheet = workbook.Sheets[1];
                Data.ItemsSource = worksheet as Collection<DataRow>;

                workbook.Close(false);
                excelApp.Quit();
            }
        }
    }
}