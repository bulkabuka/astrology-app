using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AstrologyApp
{
    public partial class TabSearch : Page
    {
        public TabSearch()
        {
            InitializeComponent();
            var hoursItems = new Collection<ComboBoxItem>();
            for (int i = 0; i < 24; i++)
            {
                var item = new ComboBoxItem
                {
                    Content = i.ToString()
                };
                hoursItems.Add(item);
            }

            ComboBox.ItemsSource = hoursItems;
        }
    }
}