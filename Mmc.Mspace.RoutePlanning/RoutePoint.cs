using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning
{
    public class RoutePoint
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// 悬停
        /// </summary>
        public double Hover { get; set; }
        /// <summary>
        /// 触发
        /// </summary>
        public double Trigger { get; set; }
        /// <summary>
        /// 是否相机触发
        /// </summary>
        public int IsCameraTriger { get; set; }
        /// <summary>
        /// 航点构造函数
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <param name="hover"></param>
        /// <param name="trigger"></param>
        /// <param name="isCameraTrigger"></param>
        public RoutePoint(double lng, double lat,double height,double speed, double hover, double trigger, int isCameraTrigger)
        {
            Lng = lng;
            Lat = lat;
            Height = height;
            Speed = speed;
            Hover = hover;
            Trigger = trigger;
            IsCameraTriger = isCameraTrigger;
        }

    }
}
