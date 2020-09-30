using Mmc.Mspace.Common.Models;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LayersQueryManagementVModel: CheckedToolItemModel
    {
        LayersQueryManagementView layersQueryManagementView = new LayersQueryManagementView();
        private ObservableCollection<LayerQueryWktGroup> _layerQueryListCollection = new ObservableCollection<LayerQueryWktGroup>();
        public ObservableCollection<LayerQueryWktGroup> LayerQueryListCollection
        {
            get { return _layerQueryListCollection; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<LayerQueryWktGroup>>(ref this._layerQueryListCollection, value, "LayerQueryListCollection");
            }
        }

        public void OpenLayersQuery(string layerName)
        {
            layersQueryManagementView.DataContext = this;
            //LayerQueryListCollection.
            layersQueryManagementView.Show();
        }
        public void CloseLayersQuery()
        {
            layersQueryManagementView.Hide();
        }
    }
}
