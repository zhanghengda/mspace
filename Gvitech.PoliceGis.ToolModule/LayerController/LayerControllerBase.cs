using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class LayerControllerBase : ToolItemModel
    {
        [XmlIgnore]
        public ICommand MainSwitchCmd { get; set; }

        public override void Initialize()
        {
            base.ViewType = (ViewType)5;
            base.Initialize();
            this.Items.Clear();
            this.MainSwitchCmd = new RelayCommand(delegate (object p)
            {
                bool isVisible = StringExtension.ParseTo<bool>(p, false);
                foreach (LayerItemModel layerItemModel in this.Items)
                {
                    layerItemModel.IsVisible = isVisible;
                }
            });
        }

        public override FrameworkElement CreatedView()
        {
            bool flag = !IEnumerableExtension.HasValues<LayerItemModel>(this.Items);
            if (flag)
            {
                this.Items = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(base.Content);
            }
            return new LayersView();
        }

        public List<LayerItemModel> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.items, value, "Items");
            }
        }

        private List<LayerItemModel> items = new List<LayerItemModel>();
    }
}