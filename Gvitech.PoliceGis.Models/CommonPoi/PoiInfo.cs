using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.CommonPoi
{
 public   class PoiInfo
    {
        public string title { get; set; }
        public string detail { get; set; }
        /// <summary>
        /// 标注类型
        /// </summary>
        public string markerType { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string image { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public double alt { get; set; }
        /// <summary>
        /// 水平方向角
        /// </summary>
        public double heading { get; set; }
        public double pitch { get; set; }
        public double roll { get; set; }
        public string num { get; set; }
        /// <summary>
        /// 1=点，0=巡航线区间，2=巡航线终点
        /// </summary>
        public string camType { get; set; }

    }
}
