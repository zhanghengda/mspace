using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.UavModule.UavTracing
{    
    public class UavTrace
    {
        //分类
        public int goods_id { get; set; }
        public int cat_id { get; set; }
        //属性
        public string ground_serial { get; set; }
        public string unmanned_type { get; set; }
        public string mount_type { get; set; }
        //地理信息
        public double longitude { get; set; }
        public double latitude { get; set; }
        public double altitude { get; set; }
        public double height { get; set; }
        //姿态角
        public string roll { get; set; }
        public string pitch { get; set; }
        public string yaw { get; set; }
        //速度
        public double speed { get; set; }
        //爬升率
        public string climb_rate { get; set; }
        //电压
        public string voltage { get; set; }
        //离家距离
        public string distance_to_home { get; set; }
        //视频流地址
        public string rtmp_url { get; set; }
        //本次飞行时长
        public string flight_time { get; set; }
        //电流
        public double current { get; set; }
        //剩余电压
        public double battary_remain { get; set; }
        //对地速度
        public double ground_speed { get; set; }
        //对空速度
        public double air_speed { get; set; }
        //卫星数
        public double sat_count { get; set; }
        //距离下一个点距离
        public double distance_next { get; set; }
        //飞行模式
        public string flight_mode { get; set; }
        //此次飞行架次标识
        public string flight_sortie { get; set; }
    }
}
