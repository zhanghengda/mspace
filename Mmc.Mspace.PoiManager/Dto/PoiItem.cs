using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PoiItem
    {
        public int id { get; set; }
        public string title { get; set; }
        /// <summary>
        /// 上传至服务器的图片地址
        /// </summary>
        public string img { get; set; }
        public string detail { get; set; }
        public int type { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 风格样式
        /// </summary>
        public string style { get; set; }
        public List<PoiPositon> points { get; set; }
        public List<TagItem> tags { get; set; }

        public PoiCategory category { get; set; }

        


    }
}
