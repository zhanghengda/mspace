using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    public interface IRouteAnalysisService
    {
        IRenderPolyline RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs);
    }
}