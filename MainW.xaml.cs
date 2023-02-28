using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Wpf.Ui.Controls.Window;
using Wpf.Ui.TitleBar;
using Wpf.Ui.Appearance;

namespace AstrologyApp
{
    public partial class MainW : FluentWindow
    {
        public MainW()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light, WindowBackdropType.Mica, false);
            var primaryAccent = Color.FromRgb(106, 116, 79);
            var secondaryAccent = Color.FromRgb(211, 197, 168);
            var tertiaryAccent = Color.FromRgb(245, 236, 219);
            Wpf.Ui.Appearance.Accent.Apply(primaryAccent, primaryAccent, secondaryAccent, tertiaryAccent);
            var controls = new Collection<TabItem>();
            controls.Add(new TabItem() { Header = "Поиск", Content = new Frame() { Content = new TabSearch() }});
            controls.Add(new TabItem() { Header = "Показатели", Content = new Frame() { Content = new TabMinMax() }});
            Control.ItemsSource = controls;
        }
    }
}