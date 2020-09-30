using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.UavModule.MultiViewCompare
{
    public class GroupLayerItemModel : LayerItemModel
    {
        public GroupLayerItemModel() : base()
        {
            this.LayerType = RenderLayerType.GroupLayer;
        }
        public RenderLayerType LayerType { get; set; }
        public override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            foreach (LayerItemModel layerItemModel in this.Children)
            {
                layerItemModel.IsVisible = this.IsVisible;
            }
        }
    }
}
