using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Gvitech.CityMaker.RenderControl;
using System.Drawing;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    /// <summary>
    /// 航线生成算法
    /// </summary>
    public class RouteCalculate
    {

        private  string _RectanglePolygon = "RectanglePolygon";
        private  string _RoutePlanLineKey = "RoutePlanLine";


        /// <summary>
        /// 按最小面积求解的航线的入口（已废弃）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public IPolyline getRoute(IPolygon p)
        {
            if (p == null) return null;
            //转换为平面坐标
            double angle = 0;
            var index = 0;

            var ring = p.ExteriorRing;
            var minArea = double.MaxValue;
            IPolygon temp_Polygon = null;
            double finalAngle = 0;
            IPoint pt1 = null, pt2 = null;
            for (int i = 0; i < ring.PointCount; i++)//循环凸包的边
            {

                var polygon_clone = p.Clone() as IPolygon;
                polygon_clone.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                ITransform transFormGeometry = polygon_clone as ITransform;
                if (i == ring.PointCount - 1)
                {
                    pt1 = ring.GetPoint(i).Clone() as IPoint;
                    pt2 = ring.GetPoint(i).Clone() as IPoint;
                }
                else
                {
                    pt1 = ring.GetPoint(i).Clone() as IPoint;
                    pt2 = ring.GetPoint(i + 1).Clone() as IPoint;
                }
                IEulerAngle eulerAngle = GviMap.Camera.GetAimingAngles2(pt1, pt2);
                pt1.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                pt2.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                var headAngle = eulerAngle.Heading;
                if (headAngle < 90)
                {
                    angle = headAngle;
                }
                else
                {
                    angle = headAngle % 90;
                }
                double radian = angle * Math.PI / 180;//计算旋转弧度
                transFormGeometry.Rotate2D(pt1.X, pt1.Y, radian);
                IPolygon ectPolygon = getEctPolygon(polygon_clone);
                var temp_P = ectPolygon.Clone() as IPolygon;
                temp_P.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                if (temp_P.Area() < minArea && temp_P.Area() >= 0)
                {
                    minArea = ectPolygon.Area();
                    temp_Polygon = ectPolygon;//拿到最小的矩形
                    finalAngle = radian;
                    index = i;
                }
            }
            if (!GviMap.TempRObjectPool.ContainsKey(_RectanglePolygon))
            {
                var polygons = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ);
                polygons.SpatialCRS = GviMap.SpatialCrs;
                ISurfaceSymbol surfaceSymbol = CreateDefaultSurfaceSymbol();

                var rpolygon = GviMap.ObjectManager.CreateRenderPolygon(polygons as IPolygon, surfaceSymbol);
                GviMap.TempRObjectPool.Add(_RectanglePolygon, rpolygon);
            }
            ITransform tranForm = temp_Polygon as ITransform;
            temp_Polygon.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
            var pt = p.ExteriorRing.GetPoint(index);
            tranForm.Rotate2D(pt1.X, pt1.Y, -finalAngle);
            temp_Polygon = transFormGeo(temp_Polygon);
            var rpolygon1 = GviMap.TempRObjectPool[_RectanglePolygon] as IRenderPolygon;
            rpolygon1?.SetFdeGeometry(temp_Polygon);
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            return DrawNormal(temp_Polygon, p.Clone() as IPolygon);
        }


        /// <summary>
        /// 转换空间参考
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private  IPolygon transFormGeo(IPolygon p)
        {
            p.Project(GviMap.SpatialCrs);
            return p;
        }

        /// <summary>
        /// 获取外接多边形
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private IPolygon getEctPolygon(IPolygon p)
        {
            p.Project(GviMap.SpatialCrs);
            var en = p.Envelope;
            double Z = 0;
            var listPt = new List<IPoint>();
            ISpatialCRS sRSTarget = GviMap.SpatialCrs; ;
            listPt.Add(GviMap.GeoFactory.CreatePoint(en.MinX, en.MaxY, Z, GviMap.SpatialCrs));
            listPt.Add(GviMap.GeoFactory.CreatePoint(en.MaxX, en.MaxY, Z, sRSTarget));
            listPt.Add(GviMap.GeoFactory.CreatePoint(en.MaxX, en.MinY, Z, sRSTarget));
            listPt.Add(GviMap.GeoFactory.CreatePoint(en.MinX, en.MinY, Z, sRSTarget));
            listPt.Add(GviMap.GeoFactory.CreatePoint(en.MinX, en.MaxY, Z, GviMap.SpatialCrs));
            IPolygon ectPolygon = GviMap.GeoFactory.CreatePolygon(listPt);
            ectPolygon.SpatialCRS = sRSTarget;
            return ectPolygon;
        }

        private ISurfaceSymbol CreateDefaultSurfaceSymbol()
        {
            return new SurfaceSymbol
            {
                Color = Color.Transparent,
                BoundarySymbol = new CurveSymbol
                {
                    Color = Color.FromArgb(200, Color.Red),
                    Width = .5f
                }
            };
        }


        /// <summary>
        /// 获取多边形的两条平行线
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private  IPolyline[] getTwoPolyline(IPolygon p, int type)
        {
            int index0 = 0, index1 = 1, index2 = 3, index3 = 2;
            if (type == 1)
            {
                index0 = 1;
                index1 = 2;
                index2 = 4;
                index3 = 3;
            }
            var polyLine1 = GviMap.GeoFactory.CreatePolyline(p.ExteriorRing.GetPoint(index0),p.ExteriorRing.GetPoint(index1),GviMap.SpatialCrs);
            var polyLine2 = GviMap.GeoFactory.CreatePolyline(p.ExteriorRing.GetPoint(index2),p.ExteriorRing.GetPoint(index3), GviMap.SpatialCrs);
            //drawGeometry(polyLine1);
            //drawGeometry(polyLine2);
            return new IPolyline[] { polyLine1,polyLine2};
        }

        /// <summary>
        /// 渲染法线
        /// </summary>
        /// <param name="polygon1"></param>
        /// <param name="polygon2"></param>
        /// <returns></returns>
        private IPolyline DrawNormal(IPolygon polygon1, IPolygon polygon2)
        {
            var polyLine = DispaseEx(getTwoPolyline(polygon1,0), 8);
            return drawNormal(polyLine, polygon2);
        }


       /// <summary>
       /// 渲染法线
       /// </summary>
       /// <param name="polyLine"></param>
       /// <param name="polygon"></param>
       /// <returns></returns>
        private IPolyline drawNormal(IPolyline polyLine, IPolygon polygon)
        {
            polygon.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
            ITopologicalOperator2D topoOpera = polyLine as ITopologicalOperator2D;
            IMultiPolyline geo = topoOpera.Intersection2D(polygon) as IMultiPolyline;
            List<IPoint> hxPointList = new List<IPoint>();
            if (geo != null)
            {
                for (int i = 0; i < geo.GeometryCount; i++)
                {
                    IPolyline temp = geo.GetPolyline(i);
                    IPoint startPoint = temp.StartPoint;
                    IPoint endPoint = temp.EndPoint;
                    if (startPoint != null)
                    {
                        hxPointList.Add(startPoint);
                    }
                    if (endPoint != null)
                    {
                        hxPointList.Add(endPoint);
                    }
                }
                var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
                IPolyline hxPolyline = GviMap.GeoFactory.CreatePolyline(hxPointList, curSprs);
                drawGeometry(hxPolyline);
                return hxPolyline;
            }
            return null;
        }

        /// <summary>
        /// 渲染折线
        /// </summary>
        /// <param name="polyline"></param>
        private void drawGeometry(IPolyline polyline)
        {
            if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanLineKey))
            {
                var polyLines = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeNone);
                polyLines.SpatialCRS = GviMap.SpatialCrs;
                ICurveSymbol curveeSymbol = new CurveSymbol()
                {
                    Color = Color.Red,
                    Width = .5f
                };
                var rpolyLine = GviMap.ObjectManager.CreateRenderPolyline(polyline, curveeSymbol);
                GviMap.TempRObjectPool.Add(_RoutePlanLineKey, rpolyLine);
            }
            var rpolyline = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            rpolyline.SetFdeGeometry(polyline);
            rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }
        
        /// <summary>
        /// 渲染多段线
        /// </summary>
        /// <param name="p"></param>
        private void drawGeometry(IMultiPolyline p)
        {
            if (!GviMap.TempRObjectPool.ContainsKey("multipolyline"))
            {
                var polyLines = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeNone);
                polyLines.SpatialCRS = GviMap.SpatialCrs;
                ICurveSymbol curveeSymbol = new CurveSymbol()
                {
                    Color = Color.Red,
                    Width = .5f
                };
                var rpolyLine = GviMap.ObjectManager.CreateRenderMultiPolyline(p, curveeSymbol);
                GviMap.TempRObjectPool.Add("multipolyline", rpolyLine);
            }
            var rpolyline = GviMap.TempRObjectPool["multipolyline"] as IRenderMultiPolyline;
            rpolyline.SetFdeGeometry(p);
            rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        /// <summary>
        /// 渲染多边形
        /// </summary>
        /// <param name="p"></param>

        private void drawGeometry(IPolygon p)
        {
            if (!GviMap.TempRObjectPool.ContainsKey("polygon"))
            {
                var polyLines = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeNone);
                polyLines.SpatialCRS = GviMap.SpatialCrs;
                ICurveSymbol curveeSymbol = new CurveSymbol()
                {
                    Color = Color.Red,
                    Width = .5f
                };
                var rpolyLine = GviMap.ObjectManager.CreateRenderPolygon(p, GviMap.LinePolyManager.SurfaceSym);
                GviMap.TempRObjectPool.Add("polygon", rpolyLine);
            }
            var rpolyline = GviMap.TempRObjectPool["polygon"] as IRenderPolygon;
            rpolyline.SetFdeGeometry(p);
            rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        /// <summary>
        /// 本方法是将折线段集合按规则连线生成航线
        /// </summary>
        /// <param name="arrPolyLine"></param>
        /// <param name="disperLength"></param>
        /// <returns></returns>
        private IPolyline DispaseEx(IPolyline[] arrPolyLine, double disperLength)
        {

            var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPolyline _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _polyline.SpatialCRS = curSprs;
            arrPolyLine[0].Project(curSprs);
            arrPolyLine[1].Project(curSprs);
            //离散点

            var mulPt = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            mulPt.SpatialCRS = curSprs;
            IVector3 temVect = null;
            IVector3 temVect2 = null;

            for (int j = 0; j < arrPolyLine[0].SegmentCount; j++)
            {
                var seg = arrPolyLine[0].GetSegment(j);
                var seg2 = arrPolyLine[1].GetSegment(j);
                var vStart = seg.StartPoint.ToVector3();
                var vStart2 = seg2.StartPoint.ToVector3();
                var vEnd = seg.EndPoint.ToVector3();
                var vEnd2 = seg2.EndPoint.ToVector3();
                var vector = vEnd.Subtract(vStart);
                var vector2 = vEnd2.Subtract(vStart2);
                vector.Normalize();//单一化
                vector2.Normalize();


                var spitCount = Math.Floor(seg.Length / disperLength);//离散线采样点个数
                if (disperLength >= seg.Length)//采样距离大于线段距离
                {
                    mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, curSprs));
                    mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                }
                else
                {
                    for (int k = 0; k < spitCount; k++)
                    {
                        if (k % 2 == 0)
                        {
                            temVect = vStart.Add(vector.Multiply(disperLength * k));
                            if (k + 1 < spitCount)
                                temVect2 = vStart.Add(vector.Multiply(disperLength * (k + 1)));
                        }
                        else
                        {
                            temVect = vStart2.Add(vector.Multiply(disperLength * k));
                            if (k + 1 < spitCount)
                                temVect2 = vStart2.Add(vector.Multiply(disperLength * (k + 1)));
                        }
                        mulPt.AddGeometry(temVect.ToPoint(GviMap.GeoFactory, curSprs));
                        mulPt.AddGeometry(temVect2.ToPoint(GviMap.GeoFactory, curSprs));
                    }
                    if (spitCount % 2 == 0)
                        mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                    else
                        mulPt.AddGeometry(vEnd2.ToPoint(GviMap.GeoFactory, curSprs));
                }
            }
            for (int m = 0; m < mulPt.GeometryCount; m++)
            {
                var pt = mulPt.GetPoint(m);
                _polyline.AppendPoint(pt);
            }

            //_polyline.Project(GviMap.SpatialCrs);
            //drawGeometry(_polyline);
            return _polyline;

        }

        //计算航线间距
        public double getDistance(MappingCamera mappingCamera, double GroundPixel, double Side)
        {
            //地面分辨率、像幅的长、侧面百分比
            double dis =  GroundPixel / mappingCamera.Width * (1 - Side / 100);
            //dis = 10;
            return dis;
        }

        /// <summary>
        /// 测绘航线计算航线入口
        /// </summary>
        /// <param name="p">传入多边形</param>
        /// <param name="angle">旋转角度</param>
        /// <param name="mappingCamera">相机参数</param>
        /// <param name="height">高度</param>
        /// <param name="groundPixel">地面分辨率</param>
        /// <param name="side">侧面百分比</param>
        /// <returns></returns>
        public IPolyline getRoute(IPolygon p, double angle, MappingCamera mappingCamera, double height, double groundPixel, double side)
        {
            try
            {
                double distance = this.getDistance(mappingCamera, groundPixel, side);//根据相机参数、地面分辨率，侧面百分比计算航线间距
                var pMax = getEctPolygon(p);//拿到外接矩形
                //drawGeometry(pMax);
                pMax.ProjectEx(WKTString.PROJ_CGCS2000_WKT);//球面坐标转平面坐标
                var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
                IPolyline line = null;
                IMultiPolyline mPolyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolyline;
                if (angle == 0 || angle == 180)
                {
                    angle = 0;
                    line = DispaseEx(getTwoPolyline(pMax, 0), distance);
                    line = line.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                    mPolyline.AddPolyline(line);
                }
                if (angle == 90)
                {
                    line = DispaseEx(getTwoPolyline(pMax, 1), distance);
                    line = line.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline;
                    mPolyline.AddPolyline(line);
                }
                if (angle > 0 && angle < 90)//取外接矩形第2、3个点
                {
                    line = GviMap.GeoFactory.CreatePolyline(pMax.ExteriorRing.GetPoint(2), pMax.ExteriorRing.GetPoint(3), curSprs);
                    mPolyline = getMultiPolyline(line, angle, distance, pMax.ExteriorRing.GetPoint(0));
                    //drawGeometry(mPolyline);
                }
                if (angle > 90 && angle < 180)
                {
                    line = GviMap.GeoFactory.CreatePolyline(pMax.ExteriorRing.GetPoint(3), pMax.ExteriorRing.GetPoint(4), curSprs);
                    mPolyline = getMultiPolyline(line, angle, distance, pMax.ExteriorRing.GetPoint(1));
                }
                if (mPolyline != null)
                {
                    if (mPolyline.GeometryCount > 0)
                    {
                        var tempPoly = p.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;
                        tempPoly.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                        IMultiPolyline multiPolyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolyline;
                        multiPolyline.SpatialCRS = curSprs;
                        for (int i = 0; i < mPolyline.GeometryCount; i++)
                        {
                            var lineInsert = mPolyline.GetGeometry(i);
                            ITopologicalOperator2D topoOpera = lineInsert as ITopologicalOperator2D;
                            IGeometry geo = topoOpera.Intersection2D(tempPoly);//做线与面
                            if (geo == null) continue;
                            geo.ProjectEx(WKTString.PROJ_CGCS2000_WKT);
                            if (angle == 0 || angle == 90 || angle == 180)
                            {
                                if (geo is IMultiPolyline)
                                {
                                    var multiLine = geo as IMultiPolyline;
                                    multiPolyline = multiLine;
                                }
                                else
                                {
                                    
                                }
                            }
                            else
                            {
                                multiPolyline.AddPolyline(geo as IPolyline);

                            }
                        }
                        List<IPoint> ptList = new List<IPoint>();
                        for (int j = 0; j < multiPolyline.GeometryCount; j++)
                        {
                            IPoint pt1 = multiPolyline.GetPolyline(j).StartPoint;
                            pt1 = pt1.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                            pt1.Z = height;
                            IPoint pt2 = multiPolyline.GetPolyline(j).EndPoint;
                            pt2 = pt2.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                            pt2.Z = height;
                            
                            if (angle == 0 || angle == 90 || angle == 180)
                            {
                                
                                ptList.Add(pt1);
                                ptList.Add(pt2);
                            }
                            else
                            {
                                if (j % 2 != 0)
                                {
                                    ptList.Add(pt1);
                                    ptList.Add(pt2);
                                }
                                else
                                {
                                    ptList.Add(pt2);
                                    ptList.Add(pt1);
                                }
                            }
                        }
                        int ptCount = p.ExteriorRing.PointCount;
                        for (int i = 0; i < ptCount; i++)
                        {
                            IPoint tmpPt = p.ExteriorRing.GetPoint(i);
                            tmpPt = tmpPt.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                            tmpPt.Z = height;
                        }
                        if (ptList.Count > 0)
                        {
                            IPolyline polyLine = GviMap.GeoFactory.CreatePolyline(ptList, curSprs);
                            return polyLine;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex) 
                {
                throw ex;
            }
            
        }
        /// <summary>
        /// 获取线段延长线
        /// </summary>
        /// <param name="line"></param>
        /// <param name="angle"></param>
        /// <param name="range"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private  IMultiPolyline getMultiPolyline(IPolyline line, double angle, double range, IPoint pt)
        {
            double len0 = line.Length;//延长线长度
            double angle0 = angle;
            if (angle > 90)
            {
                angle0 = angle - 90;
            }
            double rad = angle0 * Math.PI / 180;
            len0 = len0 / Math.Tan(rad);
            IEulerAngle eulerAngle = new EulerAngle();
            eulerAngle.Roll = 0; eulerAngle.Tilt = 0; eulerAngle.Heading = 180;
            if (angle > 90)
            {
                eulerAngle.Heading = 270;
            }
            IPoint pt0 = GviMap.Camera.GetAimingPoint2(line.EndPoint, eulerAngle, len0);//延长线终点
            line.StartPoint = pt0.Clone() as IPoint; //重新定义线段
            line.EndPoint = pt.Clone() as IPoint;
            drawGeometry(line);
            double len1 = range / Math.Sin(rad);//离散点间距
            IPolyline line0 = DispaseEx(line as IPolyline, len1);//获取折线段
            var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IMultiPolyline mPolyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolyline;
            mPolyline.SpatialCRS = curSprs;
            for (int i = 0; i < line0.PointCount; i++)
            {
                var startPt = line0.GetPoint(i);
                eulerAngle.Heading = angle;
                IPoint endPt = GviMap.Camera.GetAimingPoint2(startPt, eulerAngle, 1000000);
                IPolyline temLine = GviMap.GeoFactory.CreatePolyline(startPt, endPt, curSprs);
                
                mPolyline.AddPolyline(temLine.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline);//拿到多段线集合
            }
            //drawGeometry(mPolyline);
            return mPolyline;
        }


       
        private  IPolyline DispaseEx(IPolyline polyLine, double disperLength)
        {

            var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPolyline _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _polyline.SpatialCRS = curSprs;
            polyLine.Project(curSprs);
            //离散点

            var mulPt = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            mulPt.SpatialCRS = curSprs;
            IVector3 temVect = null;

            for (int i = 0; i < polyLine.SegmentCount; i++)
            {
                var seg = polyLine.GetSegment(i);
                var vStart = seg.StartPoint.ToVector3();
                var vEnd = seg.EndPoint.ToVector3();
                var vector = vEnd.Subtract(vStart);
                vector.Normalize();//单一化


                var spitCount = Math.Floor(seg.Length / disperLength);//离散线采样点个数
                if (disperLength >= seg.Length)//采样距离大于线段距离
                {
                    mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, curSprs));
                    mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                }
                else
                {
                    if (i == 0)
                        mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, curSprs));
                    for (int j = 0; j < spitCount; j++)
                    {

                        temVect = vStart.Add(vector.Multiply(disperLength * (j + 1)));
                        mulPt.AddGeometry(temVect.ToPoint(GviMap.GeoFactory, curSprs));
                    }
                    mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                }
            }
            for (int i = 0; i < mulPt.GeometryCount; i++)
            {
                var pt = mulPt.GetPoint(i);
                _polyline.AppendPoint(pt);
            }

            //_polyline.Project(GviMap.SpatialCrs);
            return _polyline;
        }
    }
}