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