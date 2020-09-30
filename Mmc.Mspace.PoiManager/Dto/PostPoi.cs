using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PostPoi
    {
        public marker marker { get; set; }
        public List<PoiPositon> points { get; set; }
    }

    public class marker
    {
        public int marker_id { get; set; }
        public string title { get; set; }
        /// <summary>
        /// 上传至服务器的图片地址
        /// </summary>
        public string img_server_url { get; set; }
        public string detail { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 风格样式
        /// </summary>
        public string style { get; set; }

        public int cat_id { get; set; }

        /// <summary>
        /// 0:点，1:线，2:面
        /// </summary>
        public int type { get; set; }
    }
}
