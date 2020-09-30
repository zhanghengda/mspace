using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.DataSourceAccess
{
    public interface ITileLayer:IRenderLayer
    {
        I3DTileLayer Layer { get; set; }

       
    }

    public class TileLayer :RenderLayer, ITileLayer
    {
        public TileLayer(I3DTileLayer layer)
        {
            this.LayerType = RenderLayerType.TileLayer;
            this.Renderable = layer;
            this.Layer = layer;
        }
        [Newtonsoft.Json.JsonIgnore()]
        public I3DTileLayer Layer { get; set; }
    }
}
