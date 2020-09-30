using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    public class HttpRouteAnalysisService : Singleton<HttpRouteAnalysisService>, IRouteAnalysisService
    {
        public IRenderPolyline RouteAnalysis(List<IVector3> points, IObjectManager om, ISpatialCRS crs)
        {
            string url = ConfigurationManager.AppSettings["RouteService"];
            List<IVector3> points2 = HttpRouteAnalysisService.ConvertCRS(points, crs, HttpRouteAnalysisService.Crs);
            string postDataStr = HttpRouteAnalysisService.CreatePointsStr(points2);
            string httpResponse = this.HttpGet(url, postDataStr);
            List<IVector3> list = HttpRouteAnalysisService.RosolveRoutePath(httpResponse);
            bool flag = list == null;
            IRenderPolyline result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IRenderPolyline renderPolyline = HttpRouteAnalysisService.CreateRoute(om, list, crs, null);
                renderPolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                renderPolyline.MaxVisibleDistance = 100000.0;
                renderPolyline.MinVisiblePixels = 1f;
                result = renderPolyline;
            }
            return result;
        }

        public static IRouteAnalysisService GetDefault(object args = null)
        {
            return Singleton<HttpRouteAnalysisService>.Instance;
        }

        public string HttpGet(string url, string postDataStr)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + (string.IsNullOrEmpty(postDataStr) ? "" : "?") + postDataStr);
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
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("stops=");
                stringBuilder.Append("{\"features\":[");
                int num;
                for (int i = 0; i < points.Count; i = num + 1)
                {
                    IVector3 vector = points[i];
                    stringBuilder.Append("{\"geometry\":{\"x\":");
                    stringBuilder.Append(vector.X.ToString(CultureInfo.CurrentCulture));
                    stringBuilder.Append(",\"y\":");
                    stringBuilder.Append(vector.Y.ToString(CultureInfo.CurrentCulture));
                    stringBuilder.Append("},");
                    stringBuilder.Append((i == 0) ? "\"attributes\":{\"Name\":\"From\",\"RouteName\":\"Route A\"}}," : "\"attributes\":{\"Name\":\"To\",\"RouteName\":\"Route A\"}},");
                    num = i;
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("]}");
                stringBuilder.Append("&barriers=&polylineBarriers=&polygonBarriers=&outSR=&ignoreInvalidLocations=true&accumulateAttributeNames=&impedanceAttributeName=&restrictionAttributeNames=&attributeParameterValues=&restrictUTurns=esriNFSBAllowBacktrack&useHierarchy=false&returnDirections=false&returnRoutes=true&returnStops=false&returnBarriers=false&returnPolylineBarriers=false&returnPolygonBarriers=false&directionsLanguage=en&directionsStyleName=&outputLines=esriNAOutputLineTrueShapeWithMeasure&findBestSequence=false&preserveFirstStop=false&preserveLastStop=false&useTimeWindows=false&startTime=0&outputGeometryPrecision=&outputGeometryPrecisionUnits=esriDecimalDegrees&directionsOutputType=esriDOTComplete&directionsTimeAttributeName=&directionsLengthUnits=esriNAUMiles&returnZ=false&f=pjson");
                result = stringBuilder.ToString();
            }
            return result;
        }

        private static List<IVector3> RosolveRoutePath(string httpResponse)
        {
            JObject jobject = JsonConvert.DeserializeObject<JObject>(httpResponse);
            JObject jobject2 = (jobject != null) ? (jobject.GetValue("routes") as JObject) : null;
            JArray jarray = (jobject2 != null) ? (jobject2.GetValue("features") as JArray) : null;
            JObject jobject3 = (jarray != null && jarray.Count > 0) ? (jarray[0] as JObject) : null;
            JObject jobject4 = (jobject3 != null) ? (jobject3.GetValue("attributes") as JObject) : null;
            JObject jobject5 = (jobject3 != null) ? (jobject3.GetValue("geometry") as JObject) : null;
            JArray jarray2 = (jobject5 != null) ? (jobject5.GetValue("paths") as JArray) : null;
            bool flag = jarray2 == null;
            List<IVector3> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                List<JToken> list = jarray2.ToList<JToken>();
                int count = 0;
                bool flag2 = (count = list.Count) < 1;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    List<List<IVector3>> array = new List<List<IVector3>>(count);
                    List<IVector3> lst;
                    Action<JToken> action1 = null;
                    list.ForEach(delegate (JToken path)
                    {
                        count = path.Count<JToken>();
                        lst = new List<IVector3>(count);
                        Action<JToken> action;
                        if ((action = action1) == null)
                        {
                            action = (action1 = delegate (JToken p)
                            {
                                lst.Add(new Vector3
                                {
                                    X = (double)p[0],
                                    Y = (double)p[1],
                                    Z = 20.0
                                });
                            });
                        }
                        IEnumerableExtension.ForEach<JToken>(path, action);
                        array.Add(lst);
                    });
                    result = array.FirstOrDefault<List<IVector3>>();
                }
            }
            return result;
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
                        geo = (HttpRouteAnalysisService.GeoFac.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ ) as IPoint);
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