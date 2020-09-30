using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.UavModule.MultiViewCompare
{
    public class PoiItem : LayerItemModel
    {
        public PoiItem() : base()
        {
            this.LayerType = RenderLayerType.Poi;
        }
        public RenderLayerType LayerType { get; set; }
        public int ViewPort { get; set; }
        public string PoiId { get; set; }

        public string Tag { get; set; }

        public override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            GviMap.PoiManager.SetVisibleByViewPort(PoiId, Tag, ViewPort, base.IsVisible);
        }
    }
}
