using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Models
{
    public class Flypoints
    {
        public string autoContinue { get; set; }
        public string command { get; set; }
        public JArray coordinate { get; set; }
        public string frame { get; set; }
        public string id { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param3 { get; set; }
        public string param4 { get; set; }
        public string type { get; set; }
    }
}
