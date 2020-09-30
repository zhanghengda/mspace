using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.BDNetRouteAnalysisService;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    /// <summary>
    /// 百度的路径规划服务
    /// </summary>
    public class HttpBDRouteAnalysisService : Singleton<HttpBDRouteAnalysisService>, IRouteBDAnalysisService
    {
        private ISpatialCRS _wgs84Crs;

        public Tuple<string, IRenderPolyline> RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs)
        {
            ICRSFactory icrsfactory = new CRSFactory();
            _wgs84Crs = (icrsfactory.CreateFromWKT(WKTString.WGS_84_WKT) as ISpatialCRS);
            //string url = ConfigurationManager.AppSettings["RouteService"];
            List<IVector3> points2 = HttpBDRouteAnalysisService.ConvertCRS(points, crs, _wgs84Crs);
            string postDataStr = CreatePointsStr(points2);
            string url = "http://api.map.baidu.com/direction/v2/driving";

            // string url = string.Format(@"http://api.map.baidu.com/direction/v2/driving?coord_type=wgs84&ret_coordtype=wgs84&origin={0}&destination={1}&ak=0Dc8b4e7ca5df5ad69e6848a09a3ee83", originPt, destinationPt);

            string httpResponse = this.HttpGet(url, postDataStr);
            var result = HttpBDRouteAnalysisService.RosolveRoutePath(httpResponse);
            if (result == null || result.Item2 == null)
                return null;
            IRenderPolyline renderPolyline = HttpBDRouteAnalysisService.CreateRoute(om, result.Item2, _wgs84Crs, crs);
            renderPolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
            renderPolyline.MaxVisibleDistance = 100000.0;
            renderPolyline.MinVisiblePixels = 1f;
            return new Tuple<string, IRenderPolyline>(result.Item1, renderPolyline);
        }

        public static IRouteBDAnalysisService GetDefault(object args = null)
        {
            return Singleton<HttpBDRouteAnalysisService>.Instance;
        }

        public string HttpGet(string url, string postDataStr)
        {
            string url1 = url + (string.IsNullOrEmpty(postDataStr) ? "" : "?") + postDataStr;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url1);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();
            bool flag = responseStream == null;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebResponse.Close();
                result = text;
            }
            return result;
        }

        private static string CreatePointsStr(IReadOnlyList<IVector3> points)
        {
            bool flag = !IEnumerableExtension.HasValues<IVector3>(points);
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                string originPt = string.Format("{0:N6},{1:N6}", points[0].Y, points[0].X);
                string destinationPt = string.Format("{0:N6},{1:N6}", points[1].Y, points[1].X);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("coord_type=wgs84&ret_coordtype=gcj02&origin={0}&destination={1}&ak=0Dc8b4e7ca5df5ad69e6848a09a3ee83", originPt, destinationPt);
                result = stringBuilder.ToString();
            }
            return result;
        }

        private static Tuple<string, List<IVector3>> RosolveRoutePath(string httpResponse)
        {
            Tuple<string, List<IVector3>> result;
            List<IVector3> pts;
            string duration = string.Empty;
            var jobject = JsonConvert.DeserializeObject<dynamic>(httpResponse);
            if (jobject == null)
                return null;
            var routes = jobject.result.routes;
            if (routes.Count == 0)
                return null;
            pts = new List<IVector3>();
            foreach (var route in routes)
            {
                var tag = route.tag;//路线类型
                var distance = route.distance; //距离
                duration = route.duration;//时间
                var steps = route.steps;//路线
                foreach (var step in steps)
                {
                    var start_location = step.start_location;
                    var startPt = CoorConveter.gcj02towgs84((double)start_location.lng, (double)start_location.lat);
                    pts.Add(new Vector3
                    {
                        //X = (double)start_location.lng,
                        //Y = (double)start_location.lat,
                        X = startPt[0],
                        Y = startPt[1],
                        Z = 20.0
                    });
                    var end_location = step.end_location;
                    var endPt = CoorConveter.gcj02towgs84((double)end_location.lng, (double)end_location.lat);
                    pts.Add(new Vector3
                    {
                        //X = (double)end_location.lng,
                        //Y = (double)end_location.lat,
                        X = endPt[0],
                        Y = endPt[1],
                        Z = 20.0
                    });
                }
                break;
            }
            var useTime = TimeStamp.Instance.DurationToHMS(duration.ParseTo<float>());
            return result = new Tuple<string, List<IVector3>>(useTime, pts);
        }

        private static IRenderPolyline CreateRoute(IObjectManager om, List<IVector3> path, ISpatialCRS crs, ISpatialCRS convertCrs = null)
        {
            bool flag = !IEnumerableExtension.HasValues<IVector3>(path);
            IRenderPolyline result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = om == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    IPolyline polyline = IGeometryFactoryExtension.CreatePolyline(new GeometryFactory(), path, crs);
                    bool flag3 = polyline == null;
                    if (flag3)
                    {
                        result = null;
                    }
                    else
                    {
                        bool flag4 = crs != null && convertCrs != null && !crs.IsPrecisionEqual(convertCrs);
                        if (flag4)
                        {
                            IGemetryExtension.ProjectEx(polyline, convertCrs.AsWKT());
                        }
                        result = ObjectManagerExtension.CreateRenderPolyline(om, polyline, new CurveSymbol
                        {
                            Color = ColorConvert.UintToColor(4294901760u),
                            Width = 5f
                        });
                    }
                }
            }
            return result;
        }

        private bool ConvertCRS(IGeometry geo, ISpatialCRS crs)
        {
            bool flag = geo == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = crs == null;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = geo.SpatialCRS.Equals(crs);
                    result = (flag3 || geo.Project(crs));
                }
            }
            return result;
        }

        private static List<IVector3> ConvertCRS(List<IVector3> vect, ISpatialCRS sourceCrs, ISpatialCRS destCrs)
        {
            bool flag = !IEnumerableExtension.HasValues<IVector3>(vect);
            List<IVector3> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = sourceCrs == null || destCrs == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    IPoint geo = null;
                    List<IVector3> cvs = new List<IVector3>();
                    try
                    {
                        geo = (HttpBDRouteAnalysisService.GeoFac.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as IPoint);
                        IVector3 cp = null;
                        vect.ForEach(delegate (IVector3 p)
                        {
                            cp = p.Clone();
                            bool flag4 = geo != null;
                            if (flag4)
                            {
                                geo.SpatialCRS = sourceCrs;
                                IPointExtension.SetPostion(geo, cp);
                                bool flag5 = !geo.SpatialCRS.Equals(destCrs);
                                if (flag5)
                                {
                                    bool flag6 = geo.Project(destCrs);
                                    if (flag6)
                                    {
                                        cp.Set(geo.X, geo.Y, geo.Z);
                                    }
                                }
                            }
                            cvs.Add(cp);
                        });
                    }
                    finally
                    {
                        bool flag3 = geo != null;
                        if (flag3)
                        {
                            FdeGeometryRelease.ReleaseComObject(geo);
                        }
                    }
                    result = cvs;
                }
            }
            return result;
        }

        private static readonly ISpatialCRS Crs = ((ICRSFactory)new CRSFactory()).CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;

        private static readonly IGeometryFactory GeoFac = new GeometryFactory();
    }
}