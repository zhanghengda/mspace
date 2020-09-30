using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PoiPositon
    {
       // public string id { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
        public double alt { get; set; }
        /// <summary>
        /// 水平方向角
        /// </summary>
        public double heading { get; set; }
        public double pitch { get; set; }
        public double roll { get; set; }

       // public string marker_id { get; set; }


        public int num { get; set; }
       // public int type { get; set; }

    }
}
