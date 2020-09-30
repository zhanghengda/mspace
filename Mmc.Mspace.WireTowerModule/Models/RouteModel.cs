using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.WireTowerModule.Models
{
   public class RouteModel :IUserInfo
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public string RouteName { get; set; }
        public string Serial { get; set; }
        public string Pid { get; set; }
        public double Distance { get; set; }
        public int RouteNode { get; set; }
        public string TowerList { get; set; }
        public string LeftLines { get; set; }
        public string RightLines { get; set; }
    }
}
