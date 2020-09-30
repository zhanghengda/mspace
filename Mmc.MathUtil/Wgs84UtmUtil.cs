using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.MathUtil
{
    /// <summary>
    /// utm投影处理工具
    /// </summary>
    public class Wgs84UtmUtil
    {
        public static List<WGS84UTM> UTMs { get; private set; }

        public static void Load(string path)
        {
            UTMs = JsonUtil.DeserializeFromFile<List<WGS84UTM>>(path);
        }


        private static string WGS84_UTM_STRING(double longitude)
        {

            string zone_num = Convert.ToString(Math.Floor(Math.Floor(longitude) / 6) + 31);
            string zone = "WGS_1984_UTM_Zone_" + zone_num + "N";
            return zone;//计算出投影名称
        }

        /// <summary>
        /// 通过经度获取带号的wkt
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static string GetWkt(double longitude)
        {
            var zone = WGS84_UTM_STRING(longitude);
            var utm = UTMs.Find(P => P.name == zone);
            return utm.prj;
        }
    }


    public class WGS84UTM
    {
        /// <summary>
        /// 投影参考的wkt字符串
        /// </summary>
        public string prj { get; set; }
        /// <summary>
        /// 度带号
        /// </summary>
        public string zone { get; set; }
        /// <summary>
        /// 投影带名称
        /// </summary>
        public string name { get; set; }
    }
}
