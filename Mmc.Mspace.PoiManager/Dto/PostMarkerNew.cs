using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PostMarkerNew
    {
        public int marker_id { get; set; }
        public string title { get; set; }
        public string img { get; set; }
        public string detail { get; set; }
        public string address { get; set; }
        public string style { get; set; }
        public int cat_id { get; set; }
        public int type { get; set; }
        public string geom { get; set; }
        public string sr_proj4_text { get; set; }
        public string lp_size { get; set; }
    }
}
