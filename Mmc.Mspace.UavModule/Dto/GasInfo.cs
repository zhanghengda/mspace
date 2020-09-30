using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.Dto
{
    public class GasInfo
    {     

        public List<string> uintMap;

        public string Name { get; set; }

        public string Value { get; set; }

        public string Unit { get; set; }
        
        //设备状态
        public int State { get; set; }

        //报警状态 0:正常  1:一级警报 2：二级警报  3.三级警报
        public int WarningState { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double AmslAlt { get; set; }

        public double Altitude { get; set; }

    }


}
