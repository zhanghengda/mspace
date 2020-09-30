using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule
{
    public class DeviceInfoModel
    {
        //public int id { get; set; }
        public string device { get; set; }
        public string device_name { get; set; }
        public string device_type { get; set; }
        public string datainfo { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }        
        public double altitude { get; set; }

    }
}
