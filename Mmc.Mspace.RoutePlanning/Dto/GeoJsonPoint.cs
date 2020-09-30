using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Dto
{
    class GeoJsonPoint
    {
        
        public string type { get; set; }

        public GeoJsonPointGeometry geometry { get; set; }

        public GeoJsonPointProperty properties { get; set; }

    }

    class GeoJsonPointCoord
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }

    }

    class GeoJsonPointGeometry
    {
        public string type { get; set; }

        public string altitudeMode { get; set; }

        public List<double> coordinates { get; set; }

    }

    class GeoJsonPointProperty
    {
        public string camposx { get; set; }

        public string camposy { get; set; }

        public string camposz { get; set; }

        public string camheading { get; set; }

        public string camtilt { get; set; }

        public string camroll { get; set; }

        public string camcapture { get; set; }

        public string uavyaw { get; set; }

    }
}
