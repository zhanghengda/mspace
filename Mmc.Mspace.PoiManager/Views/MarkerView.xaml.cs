using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Mmc.Mspace.PoiManagerModule.ViewModels;

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// MarkerView.xaml 的交互逻辑
    /// </summary>
    public partial class MarkerView
    {
        public MarkerView()
        {
            InitializeComponent();
        }
        DispatcherTimer timer;
        private int i = 0;
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) =>
            {
                timer.IsEnabled = false;
                i = 0;
            };
            timer.IsEnabled = true;
            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                var vm = this.DataContext as MarkerViewModel;

                vm.DisplayImgCommand.Execute(null);
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            var vm = this.DataContext as MarkerViewModel;
            vm.StyleColor = ColorPicker.SelectedColor;
        }

        private void OutlineColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            var vm = this.DataContext as FaceMarkerViewModel;
            vm.OutlineColor = OutlineColorPicker.SelectedColor;
        }
    }
}
