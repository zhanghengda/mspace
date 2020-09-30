using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Dto
{
    public class UserInfo
    {                 
        public string name { get; set; }
        public string username { get; set; }
        public string  addtime { get; set; }
        public string  phone { get; set; }
        public string life_day { get; set; }
        public string account_end_time { get; set; }
        public string portrait { get; set; }
        public MspaceConfig mspace_config { get; set; }
        public int report_template { get; set; }
        //public ShellItemModel 
    }
    public class UserInfo2
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Guid { get; set; }
        public string ImageName { get; set; }
        public string Camera { get; set; }
        public MspaceConfig mspace_config { get; set; }
    }
}
