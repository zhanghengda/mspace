using System.Collections.Generic;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public  class FlightWay
    {
        public List<WayPoint> waypointList { get; set; }
        public string workgroup { get; set; }
        public string aircraftName { get; set; }
        public string creator { get; set; }
        public string createdTime { get; set; }
        public string company { get; set; }
        public string airRouteType { get; set; }
        public string cameraName { get; set; }
        public double towerAltitude { get; set; }
        public FlightWay()
        {
            waypointList = new List<WayPoint>();
        }
    }
}
