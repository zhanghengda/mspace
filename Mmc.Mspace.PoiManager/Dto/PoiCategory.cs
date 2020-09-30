using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
  public  class PoiCategory
    {
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public string keywords { get; set; }
        public string cat_desc { get; set; }
        public int parent_id { get; set; }
        public int sort_order { get; set; }
        public string cat_url{ get; set; }
    }
}
