using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PoiDetail
    {
        public string marker_id { get; set; }
        public string title { get; set; }
        public string detail { get; set; }
        public int cat_id { get; set; }
        /// <summary>
        /// 上传至服务器的图片地址
        /// </summary>
        public string img_url { get; set; }

    }
}
