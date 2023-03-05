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
            FontFamily = new FontFamily("Segoe UI");
            Theme.Apply(ThemeType.Light, WindowBackdropType.Mica, false);
            var primaryAccent = Color.FromRgb(103, 80, 164);
            Accent.Apply(primaryAccent, ThemeType.Light, false);
            Main.Content = new TabSearch();
            Navigator.frame = Main;
        }
    }
}