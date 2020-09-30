using Mmc.Mspace.IotModule.Models;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.IotModule.Models
{
   public class Patrolman 
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int status { get; set; }
        public List<PatroledData> inspector_list { get; set; }
        public string LastSignInTime { get; set; }
    }
}
