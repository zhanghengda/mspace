using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.WireTowerModule.Models
{
   public class WayPoint
    {
        public double gimbalPitch { get; set; }
        public double aircraftLocationLongitude { get; set; }
        public double aircraftLocationLatitude { get; set; }
        public double aircraftLocationAltitude { get; set; }
        public double aircraftYaw { get; set; }
        public int waypointType { get; set; }
        public List<PhotoPosition> photoPositionList { get; set; }
        public WayPoint()
        {
            photoPositionList = new List<PhotoPosition>();
        }
    }
}
