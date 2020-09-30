using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class HttpFlight
    {
        public string code { get; set; }
        public string message { get; set; }
        public FlightWay data { get; set; }
    }
}
