using Gvitech.CityMaker.FdeGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class PatrolmanDataForRender
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<IPoint> PointsList { get; set; }
        public List<DateTime?> PointsTime { get; set; }
        public List<int> StatusLocation { get; set; }
        public PatrolmanDataForRender()
        {
            PointsList = new List<IPoint>();
            PointsTime = new List<DateTime?>();
            StatusLocation = new List<int>();
        }
    }
}
