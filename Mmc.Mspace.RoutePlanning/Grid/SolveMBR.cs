using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    public class SolveMBR
    {
        public SolveMBR()
        { }
        public static List<Point> Solve(List<Point> arrPoi)
        {
            int stIndex = 0;
            for (int i = 0; i < arrPoi.Count;i++)
            {
                Point point1,point2;

                if (i != arrPoi.Count - 1)
                {
                    point1 = arrPoi[i];
                    point2 = arrPoi[i + 1];
                    var k = (arrPoi[i].Y - arrPoi[i + 1].Y) / (arrPoi[i].X - arrPoi[i + 1].X);
                    var b = arrPoi[i].Y - arrPoi[i].X * k;

                }
                else
                {
                    point1 = arrPoi[i];
                    point2 = arrPoi[0];
                    var k = (arrPoi[i].Y - arrPoi[0].Y) / (arrPoi[i].X - arrPoi[0].X);
                    var b = arrPoi[i].Y - arrPoi[i].X * k;
                }
                double maxDis = 0;
                for (int j = 0; j < arrPoi.Count; j++)
                {
                    if (i == j) continue;
                    maxDis = Math.Max(maxDis, GetPointToLineDistance(point1, point2, arrPoi[j]));
                    if (maxDis < GetPointToLineDistance(point1, point2, arrPoi[j]))
                    {
                        stIndex = j;
                    }
                }
            }
            return new List<Point>();
        }
        /// <summary>
        /// 点到直线的距离
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <param name="pt3"></param>
        /// <returns></returns>
        public static double GetPointToLineDistance(Point pt1, Point pt2, Point pt3)
        {
            double dis = 0;
            if (pt1.X == pt2.X)
            {
                dis = Math.Abs(pt3.X - pt1.X);
                return dis;
            }
            double lineK = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);
            double lineC = (pt2.X * pt1.Y - pt1.X * pt2.Y) / (pt2.X - pt1.X);
            dis = Math.Abs(lineK * pt3.X - pt3.Y + lineC) / (Math.Sqrt(lineK * lineK + 1));
            return dis;

        }
        /// <summary>
        /// 直线到直线的距离
        /// </summary>
        /// <param name="k"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static double GetLineToLineDistance(double k, double b1, double b2)
        {
            double dis = 0;
            return dis;
        }

    }
}
