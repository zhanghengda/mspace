using System.Windows;
using System.Windows.Markup;

namespace FireControlModule
{
    public partial class StatisticLegened : Window, IComponentConnector
    {
        public StatisticLegened()
        {
            this.InitializeComponent();
            base.Loaded += this.StatisticLegened_Loaded;
        }

        private void StatisticLegened_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetWindowLocation();
        }

        private void SetWindowLocation()
        {
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            base.Top = SystemParameters.WorkArea.Height - base.ActualHeight - 65.0;
            base.Left = 20.0;
            // base.Left =400;
        }
    }
}