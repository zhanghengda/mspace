using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class RenderGeometryStyle
    {
        public gviHeightStyle HeightStyle { get; set; }

        public string GeoSymbolXml { get; set; }

        public string TextSymbolXml { get; set; }
    }
}
