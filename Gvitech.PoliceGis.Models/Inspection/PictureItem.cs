using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.Inspection
{
    public class PictureItem : InspectItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string RouteName { get; set; }
        public bool IsTroublePoi { get; set; }
    }
}
