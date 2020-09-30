using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.Grid;

namespace Mmc.Mspace.RoutePlanning.Grid
{
    public class MSpaceGeo
    {
        public const double M_PI = 3.14159;
        private const double M_DEG_TO_RAD = (M_PI / 180.0);


        private double M_RAD_TO_DEG = (180.0 / M_PI);

        private const double CONSTANTS_ONE_G = 9.80665f;      /* m/s^2		*/
        private const double CONSTANTS_AIR_DENSITY_SEA_LEVEL_15C = 1.225f;        /* kg/m^3		*/
        private const double CONSTANTS_AIR_GAS_CONST = 287.1f;        /* J/(kg * K)		*/
        private const double CONSTANTS_ABSOLUTE_NULL_CELSIUS = -273.15f;  /* °C			*/
        private const double CONSTANTS_RADIUS_OF_EARTH = 6371000;		/* meters (m)		*/


        public static PointF convertGeoToNed(IPoint coord, IPoint origin, double x, double y, double z)
        {
            PointF result = new PointF();

            double lon_rad = coord.X * M_DEG_TO_RAD;
            double lat_rad = coord.Y * M_DEG_TO_RAD;

            double ref_lon_rad = origin.X * M_DEG_TO_RAD;
            double ref_lat_rad = origin.Y * M_DEG_TO_RAD;

            double sin_lat = Math.Sin(lat_rad);
            double cos_lat = Math.Cos(lat_rad);
            double cos_d_lon = Math.Cos(lon_rad - ref_lon_rad);

            double ref_sin_lat = Math.Sin(ref_lat_rad);
            double ref_cos_lat = Math.Cos(ref_lat_rad);

            double c = Math.Acos(ref_sin_lat * sin_lat + ref_cos_lat * cos_lat * cos_d_lon);
            double k = (Math.Abs(c) < Double.Epsilon) ? 1.0 : (c / Math.Sin(c));
            Console.WriteLine("Math.Abs(c)" + Math.Abs(c) + "Double.Epsilon" + Double.Epsilon);

            result.x = k * (ref_cos_lat * sin_lat - ref_sin_lat * cos_lat * cos_d_lon) * CONSTANTS_RADIUS_OF_EARTH;
            result.y = k * cos_lat * Math.Sin(lon_rad - ref_lon_rad) * CONSTANTS_RADIUS_OF_EARTH;

            z = -(coord.Y - origin.Y);
            //Console.WriteLine("xx:" + result.x + " yy:" + result.y + " ZZ：" + z);

            return result;
            //xx: 0 yy: 0 ZZ：0
            //---- - PrintCount :5 i: 113.961456207189 22.7066528919547
            //Math.Abs(c)5.40078192828018E-06 Double.Epsilon4.94065645841247E-324
            //xx: 10.9111694338464 yy: 32.6325145453243 ZZ：-9.81262725616716E-05
            //---- - PrintCount :5 i: 113.961774335982 22.7067510182272
            //Math.Abs(c)7.62967101264814E-06Double.Epsilon4.94065645841247E-324
            //xx: -23.099417416415 yy: 42.7692742085105 ZZ：0.000207738767368681
            //---- - PrintCount :5 i: 113.961873156583 22.7064451531873
            //Math.Abs(c)5.46870585680169E-06Double.Epsilon4.94065645841247E-324
            //xx: -33.3720288669759 yy: 10.010493319266 ZZ：0.000300122149035076
            //---- - PrintCount :5 i: 113.961553797489 22.7063527698056
            //Math.Abs(c)0Double.Epsilon4.94065645841247E-324
            //xx: 0 yy: 0 ZZ：0
            //---- - PrintCount :5 i: 113.961456207189 22.7066528919547
        }


    }


}
