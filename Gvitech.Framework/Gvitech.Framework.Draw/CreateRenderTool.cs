using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Framework.Draw
{
    public static class CreateRenderTool
    {
        /// <summary>
        ///     绘制Label
        /// </summary>
        /// <param name="posX">X坐标</param>
        /// <param name="posY">Y坐标</param>
        /// <param name="posZ">Z坐标</param>
        /// <param name="msg">lable的显示内容</param>
        /// <param name="offsetH">水平偏移量（X方向）</param>
        /// <param name="offsetV">垂直偏移量（Y方向）</param>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        public static ILabel DrawLabel(double posX, double posY, double posZ, string msg, int offsetH, int offsetV,
            uint color = 0xffffff00)
        {
            var label = GviMap.ObjectManager.CreateLabel();
            label.Text = msg;
            ITextSymbol textSym = new TextSymbol();
            textSym.TextAttribute.TextColor = ColorConvert.UintToColor(color);
            textSym.MarginWidth = offsetH;
            textSym.MarginHeight = offsetV;
            label.TextSymbol = textSym;
            var gpnt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            gpnt.SetCoords(posX, posY, posZ, 0, 0);
            label.Position = gpnt;
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            label.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            label.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            return label;
        }

        /// <summary>
        ///     绘制标签
        /// </summary>
        /// <param name="posX">X坐标</param>
        /// <param name="posY">Y坐标</param>
        /// <param name="posZ">Z坐标</param>
        /// <param name="msg">lable的显示内容</param>
        /// <param name="color">Color</param>
        /// <returns></returns>
        public static ILabel DrawLabel(double posX, double posY, double posZ, string msg, uint color = 0xffffff00)
        {
            var label = GviMap.ObjectManager.CreateLabel();
            label.Text = msg;
            ITextSymbol textSym = new TextSymbol();
            textSym.TextAttribute.TextColor = ColorConvert.UintToColor(color);
            label.TextSymbol = textSym;
            var gpnt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            gpnt.SetCoords(posX, posY, posZ, 0, 0);
            label.Position = gpnt;
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            label.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            label.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            return label;
        }

        public static ILabel CreateTextLabel(IPoint point, string msg, uint textColor, int textSize, string fontName,
            bool isBold = false, double maxDistance = 5000)
        {
            var label = GviMap.ObjectManager.CreateLabel(point);
            label.Text = msg;
            ITextSymbol txtSymbol = new TextSymbol();
            TextAttribute attribute = new TextAttribute();
            attribute.Font = fontName;
            attribute.TextSize = textSize;
            attribute.TextColor = ColorConvert.UintToColor(textColor);
            attribute.Bold = isBold;
            txtSymbol.TextAttribute = attribute;
            txtSymbol.PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter;
            label.TextSymbol = txtSymbol;
            label.MaxVisibleDistance = maxDistance;
            label.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
            return label;
        }

        /// <summary>
        ///     添加Lable
        /// </summary>
        /// <param name="point"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ILabel CreateTextLabel(IPoint point, string text)
        {
            var label = DrawLabel(point.X, point.Y, point.Z + 0.1, text);
            label.MouseSelectMask = 0;
            return label;
        }

        /// <summary>
        ///     动态标注
        /// </summary>
        /// <param name="title">标签标题</param>
        /// <param name="dic">表字段：key为列名，value为值</param>
        /// <param name="position">定位位置</param>
        /// <returns>返回标注</returns>
        public static ITableLabel DrawTableLabel(string title, Dictionary<string, string> dic, IVector3 position)
        {
            ITableLabel label = null;
            try
            {
                if (position == null)
                    return null;
                if (dic == null)
                {
                    label = GviMap.ObjectManager.CreateTableLabel(0, 0);
                }
                else
                {
                    //创建标注
                    var nColumn = 2;
                    var nRow = dic.Count;
                    label = GviMap.ObjectManager.CreateTableLabel(nRow, nColumn);
                    if (label == null)
                        return null;
                    label.TitleText = title;

                    //显示内容 
                    var i = 0;
                    foreach (var kv in dic)
                    {
                        label.SetRecord(i, 0, kv.Key);
                        label.SetRecord(i, 1, kv.Value);
                        i++;
                    }
                }
                //显示位置              
                var gpnt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                gpnt.Position = position;
                label.Position = gpnt;
                //Label Table样式设置
                //Table整体样式
                label.TableBackgroundColor = ColorConvert.Argb(150, 174, 221, 129); //表的背景色
                // Table Title样式
                var capitalTextAttribute = new TextAttribute();
                capitalTextAttribute.TextColor = ColorConvert.UintToColor(0xffffffff);
                capitalTextAttribute.OutlineColor = ColorConvert.UintToColor(4279834905);
                capitalTextAttribute.Font = "微软雅黑";
                capitalTextAttribute.TextSize = 12;
                capitalTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
                capitalTextAttribute.Bold = false;
                label.TitleTextAttribute = capitalTextAttribute;

                label.TitleBackgroundColor = ColorConvert.Argb(200, 107, 194, 53); //表头背景色           
                label.BorderColor = ColorConvert.Argb(200, 6, 128, 67); //表的边框颜色
                label.BorderWidth = 1; //表的边框的宽度

                //属性名称样式
                label.SetColumnWidth(0, 30);
                var headerTextAttribute = new TextAttribute();
                headerTextAttribute.TextColor = ColorConvert.Argb(100, 0, 0, 0);
                headerTextAttribute.Font = "微软雅黑";
                headerTextAttribute.TextSize = 12;
                headerTextAttribute.Bold = true;
                headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                label.SetColumnTextAttribute(0, headerTextAttribute);

                //属性值样式
                label.SetColumnWidth(1, 40);
                var contentTextAttribute = new TextAttribute();
                contentTextAttribute.TextColor = ColorConvert.Argb(100, 0, 0, 0);
                ;
                contentTextAttribute.Font = "微软雅黑";
                contentTextAttribute.Bold = false;
                contentTextAttribute.TextSize = 12;
                contentTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineLeft;
                label.SetColumnTextAttribute(1, contentTextAttribute);

                label.MouseSelectMask = 0;
                label.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                return label;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                //删除对象
                if (label != null)
                    GviMap.ObjectManager.ReleaseRenderObject(label);
                return null;
            }
        }

        /// <summary>
        ///     绘制盒子，包括一个顶面和N个侧面的
        /// </summary>
        /// <param name="geo">底部多边形，无Z值</param>
        /// <param name="surColor">盒子的颜色</param>
        /// <param name="absoluteHeight">盒子底部高度</param>
        /// <param name="height">盒子的高度</param>
        /// <returns></returns>
        public static List<IRenderPolygon> DrawPoly(IPolygon geo, uint surColor, double absoluteHeight, double height)
        {
            try
            {
                var renderpolys = new List<IRenderPolygon>();
                ISurfaceSymbol surSymbol = new SurfaceSymbol();
                surSymbol.Color = ColorConvert.UintToColor(surColor); // 0x00f0f0f0;
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = ColorConvert.UintToColor(0xffffff00);
                surSymbol.BoundarySymbol = cs;
                if (geo == null || geo.ExteriorRing == null) return null;
                var dz = absoluteHeight + height;
                var ring = geo.ExteriorRing;
                //绘制顶面
                var top_polygon =
                    GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                        gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                var point1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point2 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point3 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point4 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var sidePolys = new List<IPolygon>();

                for (var j = 0; j < ring.PointCount; j++)
                {
                    //绘制顶面环
                    var origpoint = ring.GetPoint(j);
                    point1.X = origpoint.X;
                    point1.Y = origpoint.Y;
                    point1.Z = dz;
                    top_polygon.ExteriorRing.AppendPoint(point1);
                    //绘制侧面
                    if (j == (ring.PointCount - 1))
                    {
                        break;
                    }
                    var origpoint2 = ring.GetPoint(j + 1);
                    point2.X = origpoint2.X;
                    point2.Y = origpoint2.Y;
                    point2.Z = dz; // (origpoint2.Z > absoluteHeight ? origpoint2.Z : absoluteHeight) + height;

                    point3.X = origpoint2.X;
                    point3.Y = origpoint2.Y;
                    point3.Z = absoluteHeight; //origpoint2.Z > absoluteHeight ? origpoint2.Z : absoluteHeight;

                    point4.X = origpoint.X;
                    point4.Y = origpoint.Y;
                    point4.Z = absoluteHeight; //origpoint2.Z > absoluteHeight ? origpoint2.Z : absoluteHeight;
                    var sidePoly =
                        GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                            gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                    sidePoly.ExteriorRing.AppendPoint(point1);
                    sidePoly.ExteriorRing.AppendPoint(point2);
                    sidePoly.ExteriorRing.AppendPoint(point3);
                    sidePoly.ExteriorRing.AppendPoint(point4);
                    sidePoly.ExteriorRing.AppendPoint(point1);

                    sidePolys.Add(sidePoly);
                }
                var topPolygon = GviMap.ObjectManager.CreateRenderPolygon(top_polygon, surSymbol);
                topPolygon.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
                topPolygon.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
                renderpolys.Add(topPolygon);
                foreach (var side in sidePolys)
                {
                    var sidePolygon = GviMap.ObjectManager.CreateRenderPolygon(side, surSymbol);
                    sidePolygon.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
                    sidePolygon.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
                    renderpolys.Add(sidePolygon);
                }
                return renderpolys;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static List<IRenderPolygon> DrawPoly(IPolygon geo, double height)
        {
            try
            {
                var renderpolys = new List<IRenderPolygon>();
                ISurfaceSymbol surSymbol = new SurfaceSymbol();
                surSymbol.Color = ColorConvert.UintToColor(0xaaf0f0f0);
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = ColorConvert.UintToColor(0xffffff00);
                surSymbol.BoundarySymbol = cs;
                if (geo == null || geo.ExteriorRing == null) return null;
                var ring = geo.ExteriorRing;
                //绘制顶面
                var topPolygon =
                    GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                        gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                var point1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point2 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point3 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var point4 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                var sidePolys = new List<IPolygon>();
                for (var j = 0; j < ring.PointCount; j++)
                {
                    //绘制顶面环
                    var origpoint = ring.GetPoint(j);
                    point1.X = origpoint.X;
                    point1.Y = origpoint.Y;
                    point1.Z = origpoint.Z + height;
                    topPolygon.ExteriorRing.AppendPoint(point1);
                    //绘制侧面
                    IPoint origpoint2;
                    if (j == (ring.PointCount - 1))
                    {
                        break;
                    }
                    origpoint2 = ring.GetPoint(j + 1);
                    point2.X = origpoint2.X;
                    point2.Y = origpoint2.Y;
                    point2.Z = origpoint2.Z + height;

                    point3.X = origpoint2.X;
                    point3.Y = origpoint2.Y;
                    point3.Z = origpoint2.Z;

                    point4.X = origpoint.X;
                    point4.Y = origpoint.Y;
                    point4.Z = origpoint2.Z;
                    var sidePoly =
                        GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                            gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                    sidePoly.ExteriorRing.AppendPoint(point1);
                    sidePoly.ExteriorRing.AppendPoint(point2);
                    sidePoly.ExteriorRing.AppendPoint(point3);
                    sidePoly.ExteriorRing.AppendPoint(point4);
                    sidePoly.ExteriorRing.AppendPoint(point1);

                    sidePolys.Add(sidePoly);
                }

                renderpolys.Add(GviMap.ObjectManager.CreateRenderPolygon(topPolygon, surSymbol));
                renderpolys.AddRange(sidePolys.Select(side => GviMap.ObjectManager.CreateRenderPolygon(side, surSymbol)));
                return renderpolys;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        public static List<IRenderPolygon> DrawMultiPoly(IMultiPolygon multiGeo, uint surColor,
            double absoluteHeight, double height)
        {
            if (multiGeo == null || multiGeo.IsEmpty
                || !multiGeo.IsValid || multiGeo.GeometryCount < 1)
                return null;
            try
            {
                var renderpolys = new List<IRenderPolygon>();
                ISurfaceSymbol surSymbol = new SurfaceSymbol();
                surSymbol.Color = ColorConvert.UintToColor(0xaaf0f0f0);
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = ColorConvert.UintToColor(0xffffff00);
                surSymbol.BoundarySymbol = cs;
                for (var i = 0; i < multiGeo.GeometryCount; i++)
                {
                    var geo = multiGeo.GetGeometry(i) as IPolygon;
                    if (geo == null || geo.IsEmpty
                        || !geo.IsValid || geo.ExteriorRing == null)
                        continue;
                    var lst = DrawPoly(geo, surColor, absoluteHeight, height);
                    if (lst == null || lst.Count < 1)
                        continue;
                    renderpolys.AddRange(lst);
                }
                return renderpolys;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        ///     绘制MultiPolygon
        /// </summary>
        /// <param name="multiGeo"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static List<IRenderPolygon> DrawMultiPoly(IMultiPolygon multiGeo, double height)
        {
            if (multiGeo == null || multiGeo.IsEmpty
                || !multiGeo.IsValid || multiGeo.GeometryCount < 1)
                return null;
            try
            {
                var renderpolys = new List<IRenderPolygon>();
                ISurfaceSymbol surSymbol = new SurfaceSymbol();
                surSymbol.Color = ColorConvert.UintToColor(0xaaf0f0f0);
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = ColorConvert.UintToColor(0xffffff00);
                surSymbol.BoundarySymbol = cs;
                for (var i = 0; i < multiGeo.GeometryCount; i++)
                {
                    var geo = multiGeo.GetGeometry(i) as IPolygon;
                    if (geo == null || geo.IsEmpty
                        || !geo.IsValid || geo.ExteriorRing == null)
                        continue;
                    var lst = DrawPoly(geo, height);
                    if (lst == null || lst.Count < 1)
                        continue;
                    renderpolys.AddRange(lst);
                }
                return renderpolys;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        ///     绘制buffer
        /// </summary>
        /// <param name="geo">生成缓冲的对象</param>
        /// <param name="bufDis">缓冲的距离,可多个缓冲距离</param>
        /// <param name="bufPolys">生成的缓冲polygon</param>
        /// <returns></returns>
        public static List<IRenderPolygon> DrawBuffer(IGeometry geo, List<double> bufDis,
            out List<IPolygon> bufPolys, double dz)
        {
            bufPolys = new List<IPolygon>();
            try
            {
                var color = 0xaaff0000;
                //绘制样式
                ISurfaceSymbol surSymbol = new SurfaceSymbol();
                surSymbol.Color = ColorConvert.UintToColor(color);
                ICurveSymbol cs = new CurveSymbol();
                cs.Color = ColorConvert.UintToColor(0xffff0000);
                surSymbol.BoundarySymbol = cs;
                //结果集
                var renderpolys = new List<IRenderPolygon>();
                //内环
                IRing inRing;
                //外环
                IRing outRing;
                IPolygon polyClone = null;
                bufDis.Sort(); //从小到大排序
                //拓扑分析
                var topo = geo as ITopologicalOperator2D;
                if (topo == null) return null;
                for (var i = 0; i < bufDis.Count; i++)
                {
                    if (i == 0) //第一个不需要去掉内环
                    {
                        bufPolys.Add((IPolygon)topo.Buffer2D(bufDis[i], gviBufferStyle.gviBufferCapround));
                        if (bufPolys[i] != null)
                        {
                            var gpolygon = CreateGeometryTool.CreatePolygonWithZ(bufPolys[i], dz);
                            renderpolys.Add(GviMap.ObjectManager.CreateRenderPolygon(gpolygon, surSymbol));
                        }
                    }
                    else
                    {
                        bufPolys.Add((IPolygon)topo.Buffer2D(bufDis[i], gviBufferStyle.gviBufferCapround));
                        if (bufPolys[i] == null) continue;
                        polyClone = (IPolygon)bufPolys[i].Clone();
                        if (bufPolys[i - 1] != null)
                        {
                            //扣去外环
                            outRing = bufPolys[i - 1].ExteriorRing;
                            outRing.ReverseOrientation();
                            polyClone.AddInteriorRing(outRing);
                            //扣去内环
                            for (var j = 0; j < bufPolys[i - 1].InteriorRingCount; j++)
                            {
                                inRing = bufPolys[i - 1].GetInteriorRing(j);
                                inRing.ReverseOrientation();
                                polyClone.AddInteriorRing(inRing);
                            }
                        }
                        surSymbol.Color = ColorConvert.UintToColor(color);
                        if (bufPolys[i] != null)
                        {
                            var gpolygon = CreateGeometryTool.CreatePolygonWithZ(polyClone, dz);
                            renderpolys.Add(GviMap.ObjectManager.CreateRenderPolygon(gpolygon, surSymbol));
                        }
                    }
                    color += 16384; //颜色值增量
                }
                if (polyClone != null)
                {
                    polyClone.ReleaseComObject();
                    polyClone = null;
                }
                return renderpolys;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        /// <summary>
        ///     根据两个点坐标创建RenderPolyline
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="beginLineSymbol"></param>
        /// <returns></returns>
        public static IRenderPolyline CreateRenderPolylineByTwoXyz(double x0, double y0, double z0, double x1, double y1,
            double z1, ICurveSymbol beginLineSymbol)
        {
            var line =
                GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                    gviVertexAttribute.gviVertexAttributeZ) as
                    IPolyline;
            if (line == null)
                return null;
            var pw0 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            if (pw0 == null)
                return null;
            pw0.SetCoords(x0, y0, z0, 0, 0);
            var pw1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            if (pw1 == null)
                return null;
            pw1.SetCoords(x1, y1, z1, 0, 0);
            line.AppendPoint(pw0);
            line.AppendPoint(pw1);
            var pline = GviMap.ObjectManager.CreateRenderPolyline(line, beginLineSymbol);
            pline.VisibleMask = gviViewportMask.gviViewAllNormalView;
            pline.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            pline.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            return pline;
        }

        /// <summary>
        ///     根据两个点创建RenderPolyline
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="beginLineSymbol"></param>
        /// <returns></returns>
        public static IRenderPolyline CreateRenderPolylineByPoints(IPoint start, IPoint end,
            ICurveSymbol beginLineSymbol)
        {
            var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline.AppendPoint(start);
            polyline.AppendPoint(end);
            var rpolyline = GviMap.ObjectManager.CreateRenderPolyline(polyline, beginLineSymbol);
            return rpolyline;
        }

        /// <summary>
        ///     通过xyz坐标值创建RenderPoint
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="beginPointSymbol"></param>
        /// <returns></returns>
        public static IRenderPoint CreateRenderPointByXyz(double x, double y, double z,
            ISimplePointSymbol beginPointSymbol)
        {
            var pw = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pw.SetCoords(x, y, z, 0, 0);
            var renderPoint = GviMap.ObjectManager.CreateRenderPoint(pw,
                beginPointSymbol);
            renderPoint.VisibleMask = gviViewportMask.gviViewAllNormalView;
            renderPoint.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            renderPoint.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            return renderPoint;
        }

        /// <summary>
        ///     设置RenderPoint的位置
        /// </summary>
        /// <param name="point"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetRenderPointPosition(IRenderPoint point, double x, double y, double z)
        {
            try
            {
                var p = GviMap.GeoFactory.CreatePoint(x, y, z);
                point.SetFdeGeometry(p);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        /// <summary>
        ///     设置Label的位置
        /// </summary>
        /// <param name="label"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetLabelPosition(ILabel label, double x, double y, double z)
        {
            try
            {
                var gpnt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                gpnt.SetCoords(x, y, z, 0, 0);
                label.Position = gpnt;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        /// <summary>
        ///     设置RenderPolyline的位置
        /// </summary>
        /// <param name="line"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        public static void SetRenderPolylinePosition(IRenderPolyline line, double x0, double y0, double z0, double x1,
            double y1, double z1)
        {
            var pline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            if (pline == null)
                return;
            var pw0 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pw0.SetCoords(x0, y0, z0, 0, 0);
            var pw1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            pw1.SetCoords(x1, y1, z1, 0, 0);
            pline.AppendPoint(pw0);
            pline.AppendPoint(pw1);
            line.SetFdeGeometry(pline);
        }

        /// <summary>
        ///     添加IRenderPoint
        /// </summary>
        /// <param name="point"></param>
        /// <param name="pointSymbol"></param>
        /// <returns></returns>
        public static IRenderPoint CreateRenderPoint(IPoint point, IPointSymbol pointSymbol)
        {
            pointSymbol.Alignment = gviPivotAlignment.gviPivotAlignCenterCenter;
            var renderPoint = GviMap.ObjectManager.CreateRenderPoint(point, pointSymbol);
            renderPoint.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            renderPoint.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            renderPoint.MouseSelectMask = 0;
            return renderPoint;
        }

        /// <summary>
        ///     添加IRenderPoint
        /// </summary>
        /// <param name="z"></param>
        /// <param name="pointSymbol"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IRenderPoint CreateRenderPointByXyz(double x, double y, double z, IPointSymbol pointSymbol)
        {
            var point = CreateGeometryTool.CreatePoint(x, y, z);
            var renderPoint = GviMap.ObjectManager.CreateRenderPoint(point, pointSymbol);
            renderPoint.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            renderPoint.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            renderPoint.MouseSelectMask = 0;
            return renderPoint;
        }

        /// <summary>
        ///     添加IRenderPolyline
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="curveSymbol"></param>
        /// <returns></returns>
        public static IRenderPolyline CreateRenderPolyline(IPolyline polyline, ICurveSymbol curveSymbol)
        {
            var renderPolyline = GviMap.ObjectManager.CreateRenderPolyline(polyline, curveSymbol);
            renderPolyline.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
            renderPolyline.MinVisiblePixels = (float)GviMap.RoMinObserveDistance;
            renderPolyline.MouseSelectMask = 0;
            return renderPolyline;
        }
    }
}