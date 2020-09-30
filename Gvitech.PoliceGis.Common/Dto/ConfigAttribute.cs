using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Dto
{
    public class ConfigAttribute
    {
        public string id { get; set; }
        public string config_id { get; set; }
        public string attribute_name { get; set; }
        public string attribute_value { get; set; }
        public string addtime { get; set; }
        public string edittime { get; set; }
    }
}
