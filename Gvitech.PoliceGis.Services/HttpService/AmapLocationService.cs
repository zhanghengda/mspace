using Mmc.MathUtil;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    /// <summary>
    /// 高德位置服务
    /// </summary>
    public class AmapLocationService
    {
        private HttpService HttpService;

        public AmapLocationService()
        {
            HttpService = new HttpService();
        }

        /// <summary>
        /// 通过坐标位置获取地址信息
        /// </summary>
        public string GetAddressByCoor(double x, double y)
        {
            string address = string.Empty;
            try
            {

                //https://restapi.amap.com/v3/geocode/regeo?output=json&location=116.310003,39.991957&key=945c084635e272e8067d2082c3b6554c&radius=1000&extensions=all
                var gps = PositionUtil.gps84_To_Gcj02(y, x);
                string url = "https://restapi.amap.com/v3/geocode/regeo";
                url = string.Format("{0}?output=json&location={1},{2}&key=945c084635e272e8067d2082c3b6554c&radius=1000&extensions=all", url, gps.getWgLon(), gps.getWgLat());
                var res = HttpService.HttpRequest(url, 500);
                var resDyn = JsonUtil.DeserializeFromString<dynamic>(res);
                if (resDyn.status == 1)
                {
                    address = resDyn.regeocode.formatted_address;
                }
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("AmapLocationService.GetAddressByCoor", ex);
            }
            return address;
        }
    }
}
