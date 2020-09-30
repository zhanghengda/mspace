using Mmc.Mspace.RoutePlanning.Grid;
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
    /// RoutePlanShowPageView.xaml 的交互逻辑
    /// </summary>
    public partial class RoutePlanShowPageView 
    {
        public RoutePlanShowPageView()
        {
            InitializeComponent();
            //this.Id_WGJD.Items.Clear();
            this.Id_Camera.Items.Clear();
        }

        private void Id_DrawPolygon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
