using DevExpress.Xpf.Charts;
using Mmc.Wpf.Commands;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.AlarmStatisticLayerController
{
    public partial class StatisticsView : Window
    {
        public StatisticsView()
        {
            this.InitializeComponent();
            base.Loaded += this.StatisticsView_Loaded;
            StatisticsView.CloseCmd = new RelayCommand(() =>
            {
                base.Visibility = Visibility.Collapsed;
            });
        }

        public static ICommand CloseCmd { get; set; }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception)
            {
            }
        }

        private void StatisticsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetWindowLocation();
            DemoValuesProvider demoValuesProvider = new DemoValuesProvider();
            Pie2DKind pie2DKind = demoValuesProvider.PredefinedPie2DKinds.ElementAt(2);
            this.Series.Model = (Pie2DModel)Activator.CreateInstance(pie2DKind.Type);
            this.pieChart.Animate();
            Bar2DKind bar2DKind = demoValuesProvider.PredefinedBar2DKinds.ElementAt(4);
            this.series.Model = (Bar2DModel)Activator.CreateInstance(bar2DKind.Type);
            this.barChart.Animate();
        }

        private void SetWindowLocation()
        {
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            base.Top = 100.0;
            base.Left = SystemParameters.WorkArea.Width - base.ActualWidth - 100.0;
        }
    }
}