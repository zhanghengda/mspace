using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Dto
{
    public class RouteInfo
    {
        //类型  字段名          是否必填 备注
        //int    testing_area_type  否 检测区域类型(0:条形测区 1: 矩形正射测区 ,2矩形倾斜测区 , 3多边形正射测区 , 4多边形倾斜测区) 默认0
        //int    voyage_type        否 航线类型(0:手动航线 1:自动航线 ) 默认 0
        //string    name               是 航线名称
        //string voyage_point_num   否 航点数量  默认1
        //float  area               否 面积(单位米) 默认0
        //int    voyage_time        否 飞行预计时间 默认0
        //int    voyage             否 预计航航程 默认null
        //string flight_course_json 是 航线json
        public string id { get; set; }
        public string addtime { get; set; }
        public int testing_area_type { get; set; }
        public int voyage_type { get; set; }
        public string name { get; set; }
        public int voyage_point_num { get; set; }
        public float area { get; set; }
        public float voyage_time { get; set; }
        public int voyage { get; set; }
        public dynamic flight_course_json { get; set; }
    }

}
