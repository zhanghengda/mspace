using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.DataSourceAccess
{
    public interface IImageLayer:IRenderLayer
    {
        IImageryLayer Layer { get; set; }
        
    }

    public class ImageLayer : RenderLayer,IImageLayer
    {
        public ImageLayer(IImageryLayer layer)
        {
            this.Renderable=layer;
            this.LayerType = RenderLayerType.ImageLayer;
            this.Layer = layer;
        }

        [Newtonsoft.Json.JsonIgnore()]
        public IImageryLayer Layer { get; set; }
    }
}
