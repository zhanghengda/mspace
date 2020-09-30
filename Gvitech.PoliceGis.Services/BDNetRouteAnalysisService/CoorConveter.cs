using System;

namespace Mmc.Mspace.Services.BDNetRouteAnalysisService
{
    public class CoorConveter
    {
        //定义一些常量
        private static double x_PI = 3.14159265358979324 * 3000.0 / 180.0;

        private static double PI = 3.1415926535897932384626;
        private static double a = 6378245.0;
        private static double ee = 0.00669342162296594323;

        /**
         * 百度坐标系 (BD-09) 与 火星坐标系 (GCJ-02)的转换
         * 即 百度 转 谷歌、高德
         * @param bd_lon
         * @param bd_lat
         * @returns {*[]}
         */

        public static double[] bd09togcj02(double bd_lon, double bd_lat)
        {
            var x_pi = 3.14159265358979324 * 3000.0 / 180.0;
            var x = bd_lon - 0.0065;
            var y = bd_lat - 0.006;
            var z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
            var theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
            var gg_lng = z * Math.Cos(theta);
            var gg_lat = z * Math.Sin(theta);
            return new double[2] { gg_lng, gg_lat };
        }

        /**
         * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
         * 即谷歌、高德 转 百度
         * @param lng
         * @param lat
         * @returns {*[]}
         */

        public static double[] gcj02tobd09(double lng, double lat)
        {
            var z = Math.Sqrt(lng * lng + lat * lat) + 0.00002 * Math.Sin(lat * x_PI);
            var theta = Math.Atan2(lat, lng) + 0.000003 * Math.Cos(lng * x_PI);
            var bd_lng = z * Math.Cos(theta) + 0.0065;
            var bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[2] { bd_lng, bd_lat };
        }

        /**
         * WGS84转GCj02
         * @param lng
         * @param lat
         * @returns {*[]}
         */

        public static double[] wgs84togcj02(double lng, double lat)
        {
            if (out_of_china(lng, lat))
            {
                return new double[2] { lng, lat };
            }
            else
            {
                var dlat = transformlat(lng - 105.0, lat - 35.0);
                var dlng = transformlng(lng - 105.0, lat - 35.0);
                var radlat = lat / 180.0 * PI;
                var magic = Math.Sin(radlat);
                magic = 1 - ee * magic * magic;
                var sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * PI);
                var mglat = lat + dlat;
                var mglng = lng + dlng;
                return new double[2] { mglng, mglat };
            }
        }

        /**
         * GCJ02 转换为 WGS84
         * @param lng
         * @param lat
         * @returns {*[]}
         */

        public static double[] gcj02towgs84(double lng, double lat)
        {
            if (out_of_china(lng, lat))
            {
                return new double[2] { lng, lat };
            }
            else
            {
                var dlat = transformlat(lng - 105.0, lat - 35.0);
                var dlng = transformlng(lng - 105.0, lat - 35.0);
                var radlat = lat / 180.0 * PI;
                var magic = Math.Sin(radlat);
                magic = 1 - ee * magic * magic;
                var sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * PI);
                var mglat = lat + dlat;
                var mglng = lng + dlng;
                return new double[2] { lng * 2 - mglng, lat * 2 - mglat };
            }
        }

        public static double transformlat(double lng, double lat)
        {
            var ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * PI) + 40.0 * Math.Sin(lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * PI) + 320 * Math.Sin(lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        public static double transformlng(double lng, double lat)
        {
            var ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lng * PI) + 40.0 * Math.Sin(lng / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lng / 12.0 * PI) + 300.0 * Math.Sin(lng / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        /**
         * 判断是否在国内，不在国内则不做偏移
         * @param lng
         * @param lat
         * @returns {boolean}
         */

        public static bool out_of_china(double lng, double lat)
        {
            return (lng < 72.004 || lng > 137.8347) || ((lat < 0.8293 || lat > 55.8271) || false);
        }
    }
}