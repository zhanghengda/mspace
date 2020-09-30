using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.Inspection
{

    /// 巡检地区
    public class InspectRegion:IUserInfo
    {
        [LiteDB.BsonIgnore]
        public List<InspectUnit> InspectUnits { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public int IsAdministrator { get; set; }
    }
}
