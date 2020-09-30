using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Dto
{
    public class MspaceConfig
    {
        public string id { get; set; }
        public string department_position_name { get; set; }
        public string is_administrator { get; set; }
        public List<UserConfig> configs { get; set; }

    }
}
