using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    /// <summary>
    /// 百度的路径规划服务
    /// </summary>
	public class RouteBDAnalysisService : Singleton<RouteBDAnalysisService>, IRouteBDAnalysisService
    {
        public RouteBDAnalysisService()
        {
        }

        public Tuple<string, IRenderPolyline> RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs)
        {
            if (!IEnumerableExtension.HasValues<IVector3>(points))
                throw new ArgumentNullException("points");
            if (om == null)
                throw new ArgumentNullException("om");
            if (crs == null)
                crs = GviMap.SpatialCrs;
            return Singleton<HttpBDRouteAnalysisService>.Instance.RouteAnalysis(points, om, crs);
        }

        public static IRouteBDAnalysisService GetDefault(object args = null)
        {
            return Instance;
        }
    }
}