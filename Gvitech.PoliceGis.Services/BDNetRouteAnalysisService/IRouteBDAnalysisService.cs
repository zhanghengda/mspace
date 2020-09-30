using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    public interface IRouteBDAnalysisService
    {
        Tuple<string, IRenderPolyline> RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs);
    }
}