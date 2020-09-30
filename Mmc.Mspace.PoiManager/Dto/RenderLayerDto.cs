using Mmc.DataSourceAccess;
using Mmc.Mspace.PoiManagerModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public  class RenderLayerDto
    {

        public static BaseRenderLayer RenderLayerConvert(DisplayLayer idisplayLayer)
        {
            BaseRenderLayer baseRenderLayer = new BaseRenderLayer();
            if(idisplayLayer!=null)
            {
                baseRenderLayer.Name = idisplayLayer.Name;
                baseRenderLayer.AliasName = idisplayLayer.AliasName;
                baseRenderLayer.Guid = idisplayLayer.Guid;
                baseRenderLayer.IsLocal = idisplayLayer.IsLocal;
                baseRenderLayer.LayerType = idisplayLayer.LayerType;
                baseRenderLayer.Renderable = idisplayLayer.Renderable;
            }
            return baseRenderLayer;
        }
    }
}
