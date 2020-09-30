using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Dto
{
    public class StationMissionJson
    {
        public int MAV_AUTOPILOT { get; set; }
        public List<StationComplexItems> complexItems { get; set; }
        public string groundStation { get; set; }
        public List<StationItems> items { get; set; }
        public StationItems plannedHomePosition { get; set; }
        public string version { get; set; }

    }

    public class StationComplexItems
    {
        public int id { get; set; }
    }

    public class StationItems
    {
        public bool autoContinue { get; set; }
        public int command { get; set; }
        public List<double> coordinate { get; set; }
        public int frame { get; set; }
        public int id { get; set; }

        //public double speed { get; set; }
        //public double hover { get; set; }
        //public double trigger { get; set; }
        //public int isCameraTrigger { get; set; }
        public double param1 { get; set; }
        public double param2 { get; set; }
        public double param3 { get; set; }
        public double param4 { get; set; }
        public string type { get; set; }
    }
}
