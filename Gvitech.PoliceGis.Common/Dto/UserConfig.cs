using System.Collections.Generic;

namespace Mmc.Mspace.Common.Dto
{
    public class UserConfig
    {
        public string id { get; set; }
        public string pid { get; set; }
        public string sort { get; set; } = "1";//默认为1
        public string user_id { get; set; }
        public string config_key { get; set; }
        public string config_name { get; set; }
        public string config_value { get; set; }
        public string config_type { get; set; }
        public string guid { get; set; }
        public string description { get; set; }
        public List<ConfigAttribute> configAttributes { get; set; }
    }
}
