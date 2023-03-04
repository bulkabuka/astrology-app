using System.Collections.ObjectModel;
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
            Theme.Apply(ThemeType.Light, WindowBackdropType.Mica, false);
            var primaryAccent = Color.FromRgb(103, 80, 164);
            Accent.Apply(primaryAccent, ThemeType.Light, false);
            var controls = new Collection<TabItem>();
            controls.Add(new TabItem { Header = "Поиск", Content = new Frame { Content = new TabSearch() } });
            controls.Add(new TabItem { Header = "Показатели", Content = new Frame { Content = new TabMinMax() } });
            Control.ItemsSource = controls;
        }
    }
}