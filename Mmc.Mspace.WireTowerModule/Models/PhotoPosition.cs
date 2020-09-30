using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class PhotoPosition
    {
        public double longitude{get;set;}
        public double latitude { get; set; }
        public double altitude { get; set; }
        public string site { get; set; }
        public string name { get; set; }
        public string feederName { get; set; }
    }
}
