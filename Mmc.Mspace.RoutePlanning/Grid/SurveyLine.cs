using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    class SurveyLine
    {
        private System.Drawing.PointF _qpointf1;

        private System.Drawing.PointF _qpointf2;
        public System.Drawing.PointF point1() { return _qpointf1; }        //返回点1
        public System.Drawing.PointF point2() { return _qpointf2; }        //返回点2

        public void setPoint(System.Drawing.PointF tmp1, System.Drawing.PointF tmp2)    //设置两个点
        {
            _qpointf1 = tmp1;
            _qpointf2 = tmp2;
        }

        public double Length()//返回线长
        {
            double lineLength = 0;
            lineLength = Math.Sqrt(Math.Pow(_qpointf1.X - _qpointf2.X, 2) + Math.Pow(_qpointf1.Y - _qpointf2.Y, 2));
            return lineLength;
        }

        public double Angle()//返回线的角度（与x轴夹角）
        {
            double lineAngle;
            if (_qpointf1.X == _qpointf2.X)
            {
                return 0;
            }
            lineAngle = Math.Atan((_qpointf1.Y - _qpointf2.Y) / (_qpointf1.X - _qpointf2.X));
            lineAngle = lineAngle / MSpaceGeo.M_PI * 180;
            if (lineAngle < 0)
            {
                while (lineAngle < 0)
                {
                    lineAngle += 360;
                }
            }
            else if (lineAngle > 360)
            {
                while (lineAngle > 360)
                {
                    lineAngle -= 360;
                }
            }
            return lineAngle;
        }
    }
}
