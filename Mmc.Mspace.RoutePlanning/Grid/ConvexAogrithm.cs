using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    class ConvexAogrithm

    {

        private static List<Point> nodes;

        private static Stack<Point> sortedNodes;

        public static Point[] sor_nodes;

        private static double DistanceOfNodes(Point p0, Point p1)
        {
            return Math.Sqrt((p1.X - p0.X) * (p1.X - p0.X) + (p1.Y - p0.Y) * (p1.Y - p0.Y));
        }

        public static Stack<Point> Solve(IPolygon polygon)
        {
            nodes = new List<Point>();
            sortedNodes = new Stack<Point>();
            List<Point> arrPoint = new List<Point>();
            var len = polygon.ExteriorRing.PointCount;
            for (int i = 0; i < len; i++)
            {
                var poi = polygon.ExteriorRing.GetPoint(i);
                Point tPoi = new Point(poi.X, poi.Y);
                nodes.Add(tPoi);
            }
            Point p;
            GetNodesByAngle();
            return sortedNodes;
        }

        public static void GetNodesByAngle()
        {
            LinkedList<Point> list_node = new LinkedList<Point>();
            Point p0 = GetMinYPoint();
            LinkedListNode<Point> node = new LinkedListNode<Point>(nodes[0]);
            list_node.AddFirst(node);
            for (int i = 1; i < nodes.Count; i++)
            {
                int direct = IsClockDirection(p0, node.Value, nodes[i]);
                if (direct == 1)
                {
                    list_node.AddLast(nodes[i]);
                    node = list_node.Last;
                    //node.Value = nodes[i]; 
                }
                else if (direct == -10)
                {
                    list_node.Last.Value = nodes[i];
                    //node = list_node.Last 
                    //node.Value = nodes[i]; 
                }
                else if (direct == 10)
                    continue;
                else if (direct == -1)
                {
                    LinkedListNode<Point> temp = node.Previous;
                    while (temp != null && IsClockDirection(p0, temp.Value, nodes[i]) == -1)
                    {
                        temp = temp.Previous;
                    }
                    if (temp == null)
                    {
                        list_node.AddFirst(nodes[i]);
                        continue;
                    }
                    if (IsClockDirection(p0, temp.Value, nodes[i]) == -10)
                        temp.Value = nodes[i];
                    else if (IsClockDirection(p0, temp.Value, nodes[i]) == 10)
                        continue;
                    else
                        list_node.AddAfter(temp, nodes[i]);
                }
            }
            sor_nodes = list_node.ToArray();
            sortedNodes = new Stack<Point>();
            sortedNodes.Push(p0);
            sortedNodes.Push(sor_nodes[0]);
            sortedNodes.Push(sor_nodes[1]);
            for (int i = 2; i < sor_nodes.Length; i++)
            {
                Point p2 = sor_nodes[i];
                Point p1 = sortedNodes.Pop();
                Point p0_sec = sortedNodes.Pop();
                sortedNodes.Push(p0_sec);
                sortedNodes.Push(p1);
                if (IsClockDirection1(p0_sec, p1, p2) == 1)
                {
                    sortedNodes.Push(p2);
                    continue;
                }
                while (IsClockDirection1(p0_sec, p1, p2) != 1)
                {
                    sortedNodes.Pop();
                    p1 = sortedNodes.Pop();

                    p0_sec = sortedNodes.Pop();

                    sortedNodes.Push(p0_sec);

                    sortedNodes.Push(p1);

                }
                sortedNodes.Push(p2);
            }
        }

        private static int IsClockDirection1(Point p0, Point p1, Point p2)

        {
            Point p0_p1 = new Point(p1.X - p0.X, p1.Y - p0.Y);

            Point p0_p2 = new Point(p2.X - p0.X, p2.Y - p0.Y);

            return (p0_p1.X * p0_p2.Y - p0_p2.X * p0_p1.Y) > 0 ? 1 : -1;
        }

        private static Point GetMinYPoint()

        {
            Point succNode;
            double miny = nodes.Min(r => r.Y);
            IEnumerable<Point> pminYs = nodes.Where(r => r.Y == miny);
            Point[] ps = pminYs.ToArray();
            if (pminYs.Count() > 1)
            {
                succNode = pminYs.Single(r => r.X == pminYs.Min(t => t.X));
                nodes.Remove(succNode);
                return succNode;
            }
            else
            {
                nodes.Remove(ps[0]);
                return ps[0];

            }



        }

        private static int IsClockDirection(Point p0, Point p1, Point p2)

        {

            Point p0_p1 = new Point(p1.X - p0.X, p1.Y - p0.Y);

            Point p0_p2 = new Point(p2.X - p0.X, p2.Y - p0.Y);

            if ((p0_p1.X * p0_p2.Y - p0_p2.X * p0_p1.Y) != 0)

                return (p0_p1.X * p0_p2.Y - p0_p2.X * p0_p1.Y) > 0 ? 1 : -1;

            else

                return DistanceOfNodes(p0, p1) > DistanceOfNodes(p0, p2) ? 10 : -10;



        }

        public Stack<Point> SortedNodes

        {

            get { return sortedNodes; }

        }



    }
}
