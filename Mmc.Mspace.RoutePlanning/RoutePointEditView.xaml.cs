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

namespace Mmc.Mspace.RoutePlanning
{
    /// <summary>
    /// RoutePointEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RoutePointEditView
    {
        public RoutePointEditView()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var vm = this.DataContext as RoutePointEditViewModel;
            vm?.releaseWindow();
        }

        private void TextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
