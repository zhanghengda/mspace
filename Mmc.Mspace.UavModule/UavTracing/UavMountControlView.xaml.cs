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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mmc.Mspace.UavModule.UavTracing
{
    /// <summary>
    /// UavMountControlView.xaml 的交互逻辑
    /// </summary>
    public partial class UavMountControlView
    {
        public UavMountControlView()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var vm = this.DataContext as UavMountControlViewModel;
            if (vm != null)
                vm.releaseWindow();
        }

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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GasSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void RadioCamLock_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
