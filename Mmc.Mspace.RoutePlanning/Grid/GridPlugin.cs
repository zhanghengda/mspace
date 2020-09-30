using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.WebSockets;
using System.Threading;
using System.Diagnostics;
using Mmc.Windows.Utils;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Device.Location;
using Newtonsoft.Json;

using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.Grid;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    public delegate void CallBack(IPolygon p);
    public class GridPlugin //MissionPlanner.Plugin.Plugin
    {
        public CallBack callBack;
        private string _PolygonKey = "GridPluginPolygon";
        private string _RectanglePolygon = "RectanglePolygon";
        private string _KeyNewPolyline = "KeyNewPolyline";//绘制分割线
        private DrawCustomerUC drawCustomer = null;
        public bool isInitPolygon = false;

        //param
        double _coveredArea = 0.0;//航线规划面积
        Fact _gridAngleFact;//网格角度

        [XmlIgnore]
        public IPolygon _polygon;
        private IRenderPOI _PolygonVertexPoi;//标记点
        private IPolyline _newPolyline;//绘制分割线


        public void initPolygon()
        {
            var pools = GviMap.TempRObjectPool;
            foreach (var item in pools)
            {
                IRenderable renderable = GviMap.TempRObjectPool[item.Key] as IRenderable;
                renderable.VisibleMask = gviViewportMask.gviViewNone;
            }
            if (!GviMap.TempRObjectPool.ContainsKey(_PolygonKey))
            {
                var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ);
                polygon.SpatialCRS = GviMap.SpatialCrs;
                var rpolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon as IPolygon, GviMap.LinePolyManager.SurfaceSym);
                GviMap.TempRObjectPool.Add(_PolygonKey, rpolygon);
            }
            else
            {
                var rpolygon1 = GviMap.TempRObjectPool[_PolygonKey] as IRenderPolygon;
                rpolygon1.SetFdeGeometry(null);
            }
            UnRegisterDrawLineEvent();
            RegisterDrawLineEvent();
            isInitPolygon = true;
        }

        private void RegisterDrawLineEvent()
        {
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("Linedrawn"), DrawCustomerType.MenuCommand);
            }
            RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished;
        }

        private void PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                var rPolygon = result as IRenderPolygon;
                var polygon = rPolygon.GetFdeGeometry() as IPolygon;
                polygon.SpatialCRS = GviMap.SpatialCrs;

                //绘制面
                if (!GviMap.TempRObjectPool.ContainsKey(_PolygonKey))
                    GviMap.TempRObjectPool.Add(_PolygonKey, rPolygon);
                var rpolygon1 = GviMap.TempRObjectPool[_PolygonKey] as IRenderPolygon;

                rpolygon1?.SetFdeGeometry(polygon);
                rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

                _polygon = polygon;
                //开始计算航线
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UnRegisterDrawLineEvent();

                callBack(_polygon);
            }
        }

        public void drawGeometry(double height)
        {
            try
            {
                var count = _polygon.ExteriorRing.PointCount;
                List<IPoint> ptList = new List<IPoint>();
                for (int i = 0; i < count; i++)
                {
                    IPoint tmpPt = _polygon.ExteriorRing.GetPoint(i);
                    tmpPt.Z = height;
                    ptList.Add(tmpPt);
                }
                var tmpPolygon = GviMap.GeoFactory.CreatePolygon(ptList);
                var rpolygon1 = GviMap.TempRObjectPool[_PolygonKey] as IRenderPolygon;
                rpolygon1?.SetFdeGeometry(tmpPolygon);
                rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void GenerateGrid(IPolygon polygon)
        {
            List<System.Drawing.PointF> polygonPoints = new List<System.Drawing.PointF>();  //用来存放区域顶点
            List<System.Drawing.PointF> gridPoints = new List<System.Drawing.PointF>();     //声明一个空的数组
            _gridAngleFact = new Fact();

            //将坐标转换为Y轴向下为正方向的坐标系统
            //初始化第一个点的坐标
            IPoint tangentOrigin = polygon.ExteriorRing.GetPoint(0);
            for (int i = 0; i < polygon.ExteriorRing.PointCount - 1; i++)
            {
                //-----PrintCount :5 i: 113.96146684721 22.7066461483931
                //---- - PrintCount :5 i: 113.961778582475 22.7067599118059
                //---- - PrintCount :5 i: 113.961874125313 22.7064519527485
                //---- - PrintCount :5 i: 113.961561857218 22.7063576588414
                //---- - PrintCount :5 i: 113.96146684721 22.7066461483931

                //IVector3 vector3 = new Vector3();
                System.Drawing.PointF vector3 = new System.Drawing.PointF();

                double y = 0, x = 0, down = 0;
                PointF trance = MSpaceGeo.convertGeoToNed(polygon.ExteriorRing.GetPoint(i), tangentOrigin, y, x, down);

                Console.WriteLine("xx:" + trance.x + " yy:" + trance.y);
                vector3.X = (float)trance.x;
                vector3.Y = -(float)trance.y;
                polygonPoints.Add(vector3);
                //Console.WriteLine("-----PrintCount :" + polygon.ExteriorRing.PointCount + " i: " + polygon.ExteriorRing.GetPoint(i).X.ToString() + " " + polygon.ExteriorRing.GetPoint(i).Y.ToString());

            }

            double coveredArea = 0.0;   //覆盖面积
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                if (i != 0)
                {
                    coveredArea += polygonPoints[i - 1].X * polygonPoints[i].Y - polygonPoints[i].X * polygonPoints[i - 1].Y;
                }
                else
                {
                    coveredArea += polygonPoints[polygonPoints.Count - 1].X * polygonPoints[i].Y - polygonPoints[i].X * polygonPoints[polygonPoints.Count - 1].Y;
                }
            }

            SetCoveredArea(0.5 * Math.Abs(coveredArea)); //设置覆盖面积
            Console.WriteLine("-----_coveredArea :" + _coveredArea);

            SurveyLine surveyLine = new SurveyLine();
            SurveyLine surveyMax = new SurveyLine(); //最长线 

            surveyLine.setPoint(polygonPoints[0], polygonPoints[polygonPoints.Count - 1]);
            surveyMax.setPoint(surveyLine.point1(), surveyLine.point2());
            for (int i = 0; i <= polygonPoints.Count - 2; i++)
            {  //最长线
                surveyLine.setPoint(polygonPoints[i], polygonPoints[i + 1]);
                if (surveyLine.Length() > surveyMax.Length())
                {
                    surveyMax.setPoint(surveyLine.point1(), surveyLine.point2());
                }
            }
            Console.WriteLine("=====surveyMax.angle()====" + surveyMax.Angle());

            //绘制方格
            GridGenerator(polygonPoints, gridPoints);

        }

        private void GridGenerator(List<System.Drawing.PointF> polygonPoints, List<System.Drawing.PointF> gridPoints)
        {
            double gridAngle = 45;// _gridAngleFact.rawValue().toDouble();//旋转角度，可以先固定值45
            double gridSpacing = 17.4;// _gridSpacingFact.rawValue().toDouble(); //网格间距 17.4

            gridPoints.Clear();

            //IPolygon polygon = 

        }

        private void SetCoveredArea(double coveredArea)
        {
            //if (!qFuzzyCompare(_coveredArea, coveredArea))
            //{
            _coveredArea = coveredArea;
            //    emit coveredAreaChanged(_coveredArea);
            //}
        }

        private static readonly IPointSymbol PointSymbol = new SimplePointSymbol
        {
            FillColor = ColorConvert.UintToColor(4294967040u),
            Size = 15
        };

        private void CreateNewRpoi(IPolygon polygon)
        {
            for (int i = 0; i < polygon.ExteriorRing.PointCount; i++)
            {
                Console.WriteLine("-----PrintCount :" + polygon.ExteriorRing.PointCount + "  " + polygon.ExteriorRing.GetPoint(i).X.ToString());

                //Point
                var rPoint = GviMap.ObjectManager.CreateRenderPoint(polygon.ExteriorRing.GetPoint(i), PointSymbol);
                rPoint.MouseSelectMask = gviViewportMask.gviViewNone;
                rPoint.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;

                //POI with number
                IPOI ipoi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                string name = i.ToString();
                ipoi.SpatialCRS = GviMap.SpatialCrs;
                IPointExtension.SetPostion(ipoi, polygon.ExteriorRing.GetPoint(i).Position);
                ipoi.Name = name;
                this._PolygonVertexPoi = GviMap.MapControl.ObjectManager.CreateRenderPOI(ipoi);

            }

            //初始化 分割线段
            _newPolyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _newPolyline.AppendPoint(polygon.ExteriorRing.GetPoint(0));
            _newPolyline.AppendPoint(polygon.ExteriorRing.GetPoint(1));
            double disperLength = 17.4;//间距17.4米
            _newPolyline = BrokenStick(_newPolyline, disperLength);

            if (!GviMap.TempRObjectPool.ContainsKey(_KeyNewPolyline))
            {
                _newPolyline.SpatialCRS = GviMap.SpatialCrs;
                //var ptsym = new ICurveSymbol();
                ICurveSymbol ptsym = new CurveSymbol();
                //ptsym.Color = ColorConvert.UintToColor(0xffffff00);
                ptsym.Color = Color.Red;
                ptsym.Width = 1;

                var rline = GviMap.ObjectManager.CreateRenderPolyline(_newPolyline as IPolyline, ptsym);
                GviMap.TempRObjectPool.Add(_KeyNewPolyline, rline);
            }

        }

        public IPolyline BrokenStick(IPolyline polyLine, double disperLength)
        {

            var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPolyline _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _polyline.SpatialCRS = curSprs;
            polyLine.Project(curSprs);
            //离散点

            var mulPt = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            mulPt.SpatialCRS = curSprs;
            Console.WriteLine("---BrokenStick---SegmentCount:" + polyLine.SegmentCount);



            //SegmentCount:线段的个数
            for (int i = 0; i < polyLine.SegmentCount; i++)
            {
                var seg = polyLine.GetSegment(i);
                var vStart = seg.StartPoint.ToVector3();
                var vEnd = seg.EndPoint.ToVector3();
                var vector = vEnd.Subtract(vStart);
                vector.Normalize();

                Console.WriteLine("GetSegment---" + " vStart : " + vStart.X.ToString() + " vEnd : " + vEnd.Y.ToString());

                ST_GPS_POINT gpsPoint1 = new ST_GPS_POINT(
                    );
                ST_GPS_POINT gpsPoint2;
                gpsPoint1.sgp_lat = vStart.X;
                gpsPoint1.sgp_lon = vStart.Y;

            }
            for (int i = 0; i < mulPt.GeometryCount; i++)
            {
                var pt = mulPt.GetPoint(i);
                _polyline.AppendPoint(pt);
                Console.WriteLine("----pt---" + pt.X.ToString() + pt.Y.ToString());

            }

            _polyline.Project(GviMap.SpatialCrs);
            return _polyline;
        }

        /// <summary>
        /// 已知两点经纬度，求等分点，求据第一点的固定距离点
        /// </summary>
        /// <param name="gpsPoint1">点1</param>
        /// <param name="gpsPoint2">点2</param>
        /// <param name="len">距P1点距</param>
        /// <returns>经纬度坐标</returns>
        public ST_GPS_POINT getExcursionGpsInfo(ST_GPS_POINT gpsPoint1, ST_GPS_POINT gpsPoint2, double len)
        {//gpsPoint2->gpsPoint1

            CalcPoint calcPoint = new CalcPoint();

            ST_GPS_POINT ret = new ST_GPS_POINT();
            ST_GPSXY_POINT zb1 = new ST_GPSXY_POINT();
            ST_GPSXY_POINT zb2 = new ST_GPSXY_POINT();
            ST_GPSXY_POINT point = new ST_GPSXY_POINT();
            double rate, c = 0;
            double x1, x2, y1, y2;
            //将WGS84坐标转换为北京54平面坐标点
            zb1 = calcPoint.getScreenPoint(gpsPoint1);
            zb2 = calcPoint.getScreenPoint(gpsPoint2);
            c = calcPoint.getDistanceForPoint(zb1, zb2);//取得两个gps之间的距离
            // rate = (c + len) / c;//第三个点与第一个点的距离比，根据平行定理，计算出第三个点的坐标
            rate = len / c;//   距离/总长
            x1 = Math.Abs(zb1.x);
            x2 = Math.Abs(zb2.x);
            y1 = Math.Abs(zb1.y);
            y2 = Math.Abs(zb2.y);
            //取得第三点的平面坐标
            if (x1 < x2)//确定趋势
            {
                //point.x = zb2.x - rate * (zb2.x - zb1.x);
                point.x = zb1.x - rate * (zb1.x - zb2.x);
            }
            else
            {
                //point.x = rate * (zb1.x - zb2.x) + zb2.x;
                point.x = zb1.x + rate * (zb2.x - zb1.x);
            }
            if (y1 > y2)//确定趋势
            {
                //point.y = rate * (zb1.y - zb2.y) + zb2.y;
                point.y = zb1.y + rate * (zb2.y - zb1.y);
            }
            else
            {
                //point.y = zb2.y - rate * (zb2.y - zb1.y);
                point.y = zb1.y - rate * (zb1.y - zb2.y);
            }
            ret = calcPoint.getScreenPointToGps(point);//将平面坐标转换为经纬度
            return ret;
        }

        public void UnRegisterDrawLineEvent()
        {
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
        }


    }
}
