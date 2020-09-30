using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gvitech.CityMaker.FdeGeometry;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    public class SolveConvexHull
    {
        public static List<Point> Solve(IPolygon polygon)
        {
            List<Point> arrPoint = new List<Point>();
            var len = polygon.ExteriorRing.PointCount;
            for (int i = 0; i < len; i++)
            {
                var poi = polygon.ExteriorRing.GetPoint(i);
                Point tPoi = new Point(poi.X, poi.Y);
                arrPoint.Add(tPoi);
            }

            return ConvexHull(arrPoint);
        }
        private static List<Point> ConvexHull(List<Point> arrPoint)
        {
            List<Point> result = new List<Point>();
            arrPoint = sortPoint(arrPoint);
            if (arrPoint.Count <= 3)
            {
                return arrPoint;
            }
            int top = 0;
            int i;
            for (i = 0; i < 2; i++)
            {
                result.Add(arrPoint[i]);
                top++;
            }
            while (i < arrPoint.Count)
            {
                if (CrossMul(result[top - 2], result[top - 1], arrPoint[i]) <= 0)
                {
                    top--;
                }
                else
                {
                    result.Add(arrPoint[i++]);
                }
            }
            while (CrossMul(result[top - 2], result[top - 1], arrPoint[0]) <= 0)
            {
                top--;
            }
            result[top++] = arrPoint[0];
            return result;
        }
        private static List<Point> sortPoint(List<Point> arrPoint)
        {
            for (int i = 1; i < arrPoint.Count; i++)
            {
                if (arrPoint[i].Y<arrPoint[0].Y ||(arrPoint[i].Y==arrPoint[0].Y && arrPoint[i].X < arrPoint[0].X))
                {
                    arrPoint = swap(arrPoint, 0, i);
                }
            }
            qsortPoint(arrPoint, arrPoint[0], 1, arrPoint.Count);
            sortstartedge(arrPoint);
            return arrPoint;
        }

        private static List<Point> sortstartedge(List<Point> arrPoint)
        {
            int i, j;
            for (i = 2; i < arrPoint.Count; i++)
            {
                if (CrossMul(arrPoint[0], arrPoint[1], arrPoint[i]) != 0)
                    break;
            }
            for (j = 1; j < (i + 1) / 2; j++)
            {
                arrPoint = swap(arrPoint, i, i - j);
            }
            return arrPoint;
        }

        private static List<Point> qsortPoint(List<Point> arrPoint, Point basePoint, int start, int end)
        {
            if (start >= end)
                return null;
            Point partition = arrPoint[end - 1];
            int i = start - 1,j = end - 1;
            while (++j < arrPoint.Count)
            {
                if (isLeftorNearer(basePoint, arrPoint[j], partition))
                {
                    arrPoint = swap(arrPoint, ++i, j);
                }
            }
            arrPoint = swap(arrPoint, ++i, end - 1);
            qsortPoint(arrPoint, basePoint, start, i);
            qsortPoint(arrPoint, basePoint, i + 1, end);
            return arrPoint;
        }

        private static List<Point> swap(List<Point> arrPoint, int i, int j)
        {
            Point temp = arrPoint[i];
            arrPoint[i] = arrPoint[j];
            arrPoint[j] = temp;
            return arrPoint;
        }

        private static bool isLeftorNearer(Point b, Point i, Point j)
        {
            try {
                double m = CrossMul(b, i, j);
                if (m > 0)
                    return true;
                if (m == 0 && betweenCmp(b, i, j))
                    return true;
            }
            catch 
            {

            }
            return false;
        }

        private static bool betweenCmp(Point a, Point b, Point c)
        {
            if (doubleCmp(DotMul(a, b, c)) <= 0)
                return true;
            return false;
        }

        private static int doubleCmp(double d)
        {
            if (d < 10e-6)
                return 0;
            return d > 0 ? 1 : -1;
        }

        private static double CrossMul(Point a, Point b, Point c)
        {
            double x1 = b.X - a.X, x2 = c.X - a.X, y1 = b.Y - a.Y, y2 = c.Y - a.Y;
            double r = x1 * y2 - x2 * y1;
            return r;
        }

        private static double DotMul(Point a, Point b, Point c)
        {
            double x1 = b.X - a.X, x2 = c.X - a.X, y1 = b.Y - a.Y, y2 = c.Y - a.Y;
            return x1 * y2 + x2 * y1;
        }

    }
}
