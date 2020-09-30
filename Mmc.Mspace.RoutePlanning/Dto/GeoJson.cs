using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Dto
{
    class GeoJson
    {
        public string type { get; set; }

        public List<GeoJsonPoint> features { get; set; }
    }
}
