using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class ShipTrace
    {
        public int goods_id { get; set; }
        public int cat_id { get; set; }
        public string ground_serial { get; set; }
        public string unmanned_type { get; set; }

        public string mount_type { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }

        public double altitude { get; set; }
        public double height { get; set; }

        public string roll { get; set; }
        public string pitch { get; set; }

        public string yaw { get; set; }
        public double speed { get; set; }

        public string rtmp_url { get; set; }

        public string heading { get; set; }
        public string ct { get; set; }
        public string doValue { get; set; }
        public double ph { get; set; }
        public string temp { get; set; }
        public string tur { get; set; }

        public string startTime { get; set; }
        public string flightTime { get; set; }
        public string time { get; set; }
        public string timestamp { get; set; }
        public string connect_id { get; set; }
        public string uav_type { get; set; }
    }
}
