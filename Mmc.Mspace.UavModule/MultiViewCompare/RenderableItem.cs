using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class RenderableItem : LayerItemModel
    {
        public RenderLayerType LayerType { get; set; }
        public override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
            var rVport = base.Parameters as RenderableViewPort;
            rVport.Renderable.SetVisibleMask(gviViewportMode.gviViewportL1R1, rVport.ViewPort, base.IsVisible);
        }
    }

    public class RenderableViewPort
    {
        public int ViewPort { get; set; }

        public IRenderable Renderable { get; set; }
    }

}
