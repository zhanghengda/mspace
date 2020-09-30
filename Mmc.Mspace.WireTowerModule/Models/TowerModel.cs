using Gvitech.CityMaker.Math;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class TowerModel
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public double SafeDistance { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string TowerType { get; set; }
        public List<SignModel> SignList { get; set; }
        public string CrossVotor { get; set; }
        public string  ExtendVotor { get; set; }
        public double RelativeHeight { get; set; }
    }
}
