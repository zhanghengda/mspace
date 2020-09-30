using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class PostAccountNew
    {
        //"marker_id": 1,
        //"title": "标题",
        //"problem_time": "2019-02-10",
        //"img": "dsa.jpg,dsadqw.png",
        //"img_num": 2,
        //"is_show": 1,
        //"video": "dsadas.mp4\n * "

        public string marker_id { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string problem_time { get; set; }
        public string img { get; set; }
        public string img_num { get; set; }
        public string is_show { get; set; }
        public string video { get; set; }
    }
}
