using System.Collections.Generic;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Utils;
using Mmc.Framework.Services;

namespace Mmc.Framework.Draw
{
    public static class CreateGeometryTool
    {
        /// <summary>
        ///     创建带Z值得Geometry
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="dz"></param>
        /// <returns></returns>
        public static IGeometry CreateGeometryWithZ(IGeometry geo, double dz)
        {
            if (geo == null)
                return null;
            switch (geo.GeometryType)
            {
                case gviGeometryType.gviGeometryPoint:
                    return CreatePointWithZ(geo as IPoint, dz);
                case gviGeometryType.gviGeometryPolyline:
                    return CreatePolylineWithZ(geo as IPolyline, dz);
                case gviGeometryType.gviGeometryPolygon:
                    return CreatePolygonWithZ(geo as IPolygon, dz);
                default:
                    return null;
            }
        }

        /// <summary>
        ///     创建或者修改点的高度
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="dz"></param>
        /// <returns></returns>
        public static IPoint CreatePointWithZ(IPoint geo, double dz)
        {
            var point = (IPoint) geo.Clone2(gviVertexAttribute.gviVertexAttributeZ );
            if (point == null)
                return null;
            point.X = geo.X;
            point.Y = geo.Y;
            point.Z = dz;
            return point;
        }

        /// <summary>
        ///     利用二维polygon和高度值，生成一个三维的polygon
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="dz"></param>
        /// <returns></returns>
        public static IPolyline CreatePolylineWithZ(IPolyline geo, double dz)
        {
            var polyline = geo.Clone2(gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
            if (polyline == null)
                return null;
            for (var i = 0; i < polyline.PointCount; i++)
            {
                var point = polyline.GetPoint(i);
                var newpoint = CreatePointWithZ(point, dz);
                if (newpoint == null)
                    return null;
                polyline.UpdatePoint(i, newpoint);
            }
            return polyline;
        }

        /// <summary>
        ///     利用二维polygon和高度值，生成一个三维的polygon
        /// </summary>
        /// <param name="geo"></param>
        /// <param name="dz"></param>
        /// <returns></returns>
        public static IPolygon CreatePolygonWithZ(IPolygon geo, double dz)
        {
            var g2 = (IPolygon) geo.Clone2(gviVertexAttribute.gviVertexAttributeZ );
            if (g2 != null)
            {
                var ring = g2.ExteriorRing;

                for (var i = 0; i < ring.PointCount; i++)
                {
                    var gp = ring.GetPoint(i);
                    var pnt1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                    pnt1.X = gp.X;
                    pnt1.Y = gp.Y;
                    pnt1.Z = dz;
                    ring.UpdatePoint(i, pnt1);
                }
                for (var j = 0; j < g2.InteriorRingCount; j++)
                {
                    var interRing = g2.GetInteriorRing(j);
                    for (var h = 0; h < interRing.PointCount; h++)
                    {
                        var gp = interRing.GetPoint(h);
                        var pnt1 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                        pnt1.X = gp.X;
                        pnt1.Y = gp.Y;
                        pnt1.Z = dz;
                        interRing.UpdatePoint(h, pnt1);
                    }
                }
                return g2;
            }
            return null;
        }

        /// <summary>
        ///     通过两点创建IPolyline
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IPolyline CreatePolyline(IPoint begin, IPoint end)
        {
            var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
            polyline.AppendPoint(begin);
            polyline.AppendPoint(end);
            return polyline;
        }

        /// <summary>
        ///     通过SerializePoint创建IPoint
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static IPoint CreatePointByPosition(SerializePoint point)
        {
            var gviPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
            if (gviPoint == null)
                return null;
            gviPoint.SetPostion(point.X, point.Y, point.Z);
            return gviPoint;
        }

        /// <summary>
        ///     通过SerializePoint集合创建IPolyline
        /// </summary>
        /// <param name="pointList"></param>
        /// <returns></returns>
        public static IPolyline CreatePolylineByPointList(List<SerializePoint> pointList)
        {
            if (pointList == null || pointList.Count == 0)
                return null;
            var polyline =
                GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                    gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
            if (polyline == null)
                return null;
            for (var i = 0; i < pointList.Count; i++)
            {
                var point = CreatePointByPosition(pointList[i]);
                if (point == null)
                    continue;
                polyline.AppendPoint(point);
            }
            return polyline;
        }

        /// <summary>
        ///     创建点
        /// </summary>
        /// <returns></returns>
        public static IPoint CreatePoint(double x, double y, double z)
        {
            var pt = GviMap.GeoFactory.CreatePoint(x, y, z);
            return pt;
        }
    }
}