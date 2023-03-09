﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
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

        private void TabMinMax_OnLoaded(object sender, RoutedEventArgs e)
        {
            Whore2.Text = ExcelPath.whore;
            Task.Run(async delegate
            {
                // Группируем по дате
                var clonedTableQuery = ExcelPath.DataTable.Copy().AsEnumerable().GroupBy(r => r["Дата"]);
                // Для таблицы максимума берём максимум по дням и сортируем по убыванию
                var res1 = clonedTableQuery.Select(g => g.OrderByDescending(r => r["Условные единицы"]).First())
                    .OrderByDescending(r => r["Условные единицы"]).Take(10)
                    .CopyToDataTable().DefaultView;
                // Для таблицы минимума берём минимум по дням и сортируем по возростанию
                var res2 = clonedTableQuery.Select(g => g.OrderBy(r => r["Условные единицы"]).First())
                    .OrderBy(r => r["Условные единицы"]).Take(10).CopyToDataTable()
                    .DefaultView;

                Dispatcher.Invoke(() =>
                {
                    DataGrid.ItemsSource = res1;
                    DataGrid1.ItemsSource = res2;

                    VisibleColumns(DataGrid);
                    VisibleColumns(DataGrid1);
                    LoadingRing.Visibility = Visibility.Collapsed;
                    LoadingRing2.Visibility = Visibility.Collapsed;
                });
            });
        }

        private void BackAllBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.frame.GoBack();
        }
    }
}