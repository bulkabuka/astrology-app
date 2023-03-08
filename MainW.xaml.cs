using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls.Window;

namespace AstrologyApp
{
    public partial class MainW : FluentWindow
    {
        public MainW()
        {
            InitializeComponent();
            Main.Content = new TabSearch();
            Navigator.frame = Main;
        }
    }
}