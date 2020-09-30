using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace Mmc.MathUtil
{
    public static class LineUtil
    {
        public static double GetPointDistance(double x1, double y1, double x2, double y2)
        {
            double y = y1 - y2;
            double x = x1 - x2;
            return Math.Sqrt(y * y + x * x);
        }

        /// <summary>
        /// 经纬度转为投影
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static IPoint ToProjection(IPoint point)
        {
            var prjWkt = Wgs84UtmUtil.GetWkt(point.X);
            if (!string.IsNullOrEmpty(prjWkt))
            {
                point.ProjectEx(prjWkt);
            }
            return point;
        }

        /// <summary>
        /// 求偏移后的点坐标(原直线第一个点与最后一共点)
        /// </summary>
        /// <param name="point1">第一个点</param>
        /// <param name="point2">第二个点</param>
        /// <param name="isFirst">是否是第一个点</param>
        /// <param name="_horizontalOffset">水平偏移</param>
        /// <param name="_verticalOffset">垂直偏移</param>
        /// <param name="prePoint">偏移后与此点关联的上一个点(求原直线最后一个点)</param>
        /// <returns></returns>
        private static IPoint GetOffsetPoint(IPoint point1, IPoint point2, bool isFirst, double _horizontalOffset, double _verticalOffset, IPoint prePoint = null)
        {
            IPoint result;
            if (isFirst)
            {
                result = point1.Clone() as IPoint;
            }
            else
            {
                result = point2.Clone() as IPoint;
            }

            double k = (point1.Y - point2.Y) / (point1.X - point2.X);
            double radians = Math.Atan(k);

            //double tempK = k * k;
            //double sinA = Math.Sqrt(1 / (1 + tempK));
            //double cosA = Math.Sqrt(tempK / (1 + tempK));

            double sinA = Math.Abs(Math.Sin(radians));
            double cosA = Math.Abs(Math.Cos(radians));

            if (prePoint != null)
            {
                if (k > 0)
                {
                    cosA = -cosA;
                }

                double offsetX = _horizontalOffset * sinA;
                double offsetY = _horizontalOffset * cosA;


                double b = prePoint.Y - k * prePoint.X;

                double x1 = result.X + offsetX;
                double y1 = result.Y + offsetY;
                double x2 = result.X - offsetX;
                double y2 = result.Y - offsetY;

                LineSegment line = new LineSegment(point1.X, point1.Y, point2.X, point2.Y);
                LineSegment line1 = new LineSegment(prePoint.X, prePoint.Y, x1, y1);
                LineSegment line2 = new LineSegment(prePoint.X, prePoint.Y, x2, y2);

                GeoAPI.Geometries.Coordinate cor1 = line1.Intersection(line);
                GeoAPI.Geometries.Coordinate cor2 = line2.Intersection(line);

                if (cor1 == null)
                {
                    result.X = x1;
                    result.Y = y1;
                }

                if (cor2 == null)
                {
                    result.X = x2;
                    result.Y = y2;
                }
            }
            else
            {
                if (k > 0)
                {
                    cosA = -cosA;
                }

                double offsetX = _horizontalOffset * sinA;
                double offsetY = _horizontalOffset * cosA;
                result.X += offsetX;
                result.Y += offsetY;
            }

            result.Z += _verticalOffset;

            return result;
        }

        /// <summary>
        /// 求偏移后的点坐标
        /// </summary>
        /// <param name="pointFirst">原直线第一个点</param>
        /// <param name="pointMid">原直线第二个点</param>
        /// <param name="pointLast">原直线第三个点</param>
        /// <param name="prePoint">偏移后直线与此点关联的上一个点</param>
        /// <param name="_horizontalOffset">水平偏移</param>
        /// <param name="_verticalOffset">垂直偏移</param>
        /// <returns></returns>
        private static IPoint TransformAcuteAngle(IPoint pointFirst, IPoint pointMid, IPoint pointLast, IPoint prePoint, double _horizontalOffset, double _verticalOffset)
        {
            double k1 = (pointFirst.Y - pointMid.Y) / (pointFirst.X - pointMid.X);
            double k2 = (pointMid.Y - pointLast.Y) / (pointMid.X - pointLast.X);

            double b1 = pointMid.Y - k1 * pointMid.X;
            double b2 = pointMid.Y - k2 * pointMid.X;

            double tempp = (k2 + k1) / (1 - k1 * k2);

            //temp*(1-k^2)=2k => k=(-2+-sqrt(4+4*temp^2))/(2*temp)
            double k = (-2 + Math.Sqrt(4 + 4 * tempp * tempp)) / (2 * tempp);
            double kk = (-2 - Math.Sqrt(4 + 4 * tempp * tempp)) / (2 * tempp);

            double b = pointMid.Y - k * pointMid.X;
            double b1Offset = prePoint.Y - k1 * prePoint.X;
            double b_ = pointMid.Y - kk * pointMid.X;

            double xOffset1 = (b1Offset - b) / (k - k1);
            double yOffset1 = k1 * xOffset1 + b1Offset;
            double xOffset2 = (b1Offset - b_) / (kk - k1);
            double yOffset2 = k1 * xOffset2 + b1Offset;

            IPoint point = pointMid.Clone() as IPoint;


            #region 判断是否与两直线相交

            LineSegment line1 = new LineSegment(pointFirst.X, pointFirst.Y, pointMid.X, pointMid.Y);
            LineSegment line2 = new LineSegment(pointMid.X, pointMid.Y, pointLast.X, pointLast.Y);

            LineSegment rLine1 = new LineSegment(prePoint.X, prePoint.Y, xOffset1, yOffset1);
            LineSegment rLine2 = new LineSegment(prePoint.X, prePoint.Y, xOffset2, yOffset2);

            GeoAPI.Geometries.Coordinate cor1 = rLine1.Intersection(line1);
            GeoAPI.Geometries.Coordinate cor2 = rLine2.Intersection(line1);
            GeoAPI.Geometries.Coordinate cor3 = rLine1.Intersection(line2);
            GeoAPI.Geometries.Coordinate cor4 = rLine2.Intersection(line2);

            double bb = yOffset1 - xOffset1 * k2;
            double yy = k2 * pointLast.X + bb;
            LineSegment lineTest = new LineSegment(xOffset1, yOffset1, pointLast.X, yy);
            double bb2 = yOffset2 - xOffset2 * k2;
            double yy2 = k2 * pointLast.X + bb2;
            LineSegment lineTest2 = new LineSegment(xOffset2, yOffset2, pointLast.X, yy2);

            var cor11 = line2.Intersection(lineTest);
            var cor21 = line2.Intersection(lineTest2);
            var cor12 = line1.Intersection(lineTest);
            var cor22 = line1.Intersection(lineTest2);


            bool cor1IsNull = false;
            bool cor2IsNull = false;

            if (cor11 == null && cor1 == null && cor3 == null && cor12 == null)
            {
                cor1IsNull = true;
                point.X = xOffset1;
                point.Y = yOffset1;
            }

            if (cor21 == null && cor2 == null && cor4 == null && cor22 == null)
            {
                cor2IsNull = true;
                point.X = xOffset2;
                point.Y = yOffset2;
            }

            if (cor1IsNull && cor2IsNull)
            {
                var dis1 = GetPointDistance(pointMid.X, pointMid.Y, xOffset1, yOffset1);
                var dis2 = GetPointDistance(pointMid.X, pointMid.Y, xOffset2, yOffset2);

                if (Math.Abs(dis1 - _horizontalOffset) < Math.Abs(dis2 - _horizontalOffset))
                {
                    point.X = xOffset1;
                    point.Y = yOffset1;
                }
                else
                {
                    point.X = xOffset2;
                    point.Y = yOffset2;
                }
            }

            #endregion

            point.Z += _verticalOffset;

            return point;
        }


        /*** 航线偏移使用示例 

            var polyline = _polyline.Clone() as IPolyline;

            for (int i = 0; i < _polyline.PointCount; i++)
            {
                IPoint point = _polyline.GetPoint(i);

                point = ToProjection(point);

                IPoint result;

                if (i + 1 < _polyline.PointCount && i > 0)
                {
                    IPoint pointFirst = _polyline.GetPoint(i - 1);
                    IPoint pointLast = _polyline.GetPoint(i + 1);
                    IPoint pointPre = polyline.GetPoint(i - 1);
                    pointFirst = ToProjection(pointFirst);
                    pointLast = ToProjection(pointLast);
                    pointPre = ToProjection(pointPre);

                    result = TransformAcuteAngle(pointFirst, point, pointLast, pointPre);
                }
                else if (i + 1 < _polyline.PointCount)
                {
                    IPoint nextPoint = _polyline.GetPoint(i + 1);
                    nextPoint = ToProjection(nextPoint);

                    result = GetOffsetPoint(point, nextPoint, true);
                }
                else
                {
                    IPoint pointFirst = _polyline.GetPoint(i - 1);
                    pointFirst = ToProjection(pointFirst);
                    IPoint prePoint = polyline.GetPoint(i - 1);
                    prePoint = ToProjection(prePoint);

                    result = GetOffsetPoint(pointFirst, point, false, prePoint);
                }

                result.Project(GviMap.SpatialCrs);

                polyline.UpdatePoint(i, result);
            }

        ***/
    }
}
