using GFramework.BlankWindow;
using Mmc.Mspace.PoiManagerModule.ViewModels;
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

namespace Mmc.Mspace.PoiManagerModule.Views
{
    /// <summary>
    /// TileLayerPropView.xaml 的交互逻辑
    /// </summary>
    public partial class TileLayerPropView : BlankWindow
    {
        public TileLayerPropView()
        {
            InitializeComponent();
        }


        private void ListBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as TileModifyGeoVModel;
            
        }

        private void tiledatagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var vm = this.DataContext as TileModifyGeoVModel;
            vm.OnFly(vm.SelectGeo);
        }
    }
}
