using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    class OnlinePatrolman
    {
        public string id { get; set; }
        public string name { get; set; }
        public string department_id { get; set; }
        //public string area_id { get; set; }
        public string geom { get; set; }
        public string phone { get; set; }
        public string area_name { get; set; }
        public string department_name { get; set; }
        
    }
}
