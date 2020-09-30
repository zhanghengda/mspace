using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.Services.Controls
{
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            this.InitializeComponent();
            base.Loaded += this.SearchControl_Loaded;
        }

        private void SearchControl_Loaded(object sender, RoutedEventArgs e)
        {
            base.DataContext = new SearchControlViewModel();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            bool flag = "oid" == e.Column.Header.ToString().ToLower();
            if (flag)
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
    }
}