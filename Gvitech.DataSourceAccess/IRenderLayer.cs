using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.DataSourceAccess
{
    public interface IRenderLayer
    {
        string Name { get; set; }
        string AliasName { get; set; }
        bool IsLocal { get; set; }
        string Guid { get; set; }
        RenderLayerType LayerType { get; set; }


        [Newtonsoft.Json.JsonIgnore()]
        IRenderable Renderable { get; set; }
    }

    public class RenderLayer : IRenderLayer
    {
        public string Name { get; set; }
        public string AliasName { get; set; }
        public bool IsLocal { get; set; }
        public List<RenderLayer> Rederlayers { get; set; }
        public string Guid { get; set; }
        public RenderLayerType LayerType { get; set; }

        [Newtonsoft.Json.JsonIgnore()]
        public IRenderable Renderable { get; set; }


        public static RenderLayer CreateRenderLayer(IRenderLayer renderLayer)
        {
            return new RenderLayer()
            {
                Name = renderLayer.Name,
                AliasName = renderLayer.AliasName,
                IsLocal = renderLayer.IsLocal,
                Guid = renderLayer.Guid,
                LayerType = renderLayer.LayerType,
                Renderable = renderLayer.Renderable
            };
        }
    }
}
