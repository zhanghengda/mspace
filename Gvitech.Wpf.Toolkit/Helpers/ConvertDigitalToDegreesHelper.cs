using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Wpf.Toolkit.Helpers
{
    public class ConvertDigitalToDegreesHelper
    {
        public static string ConvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = Math.Round((tmp - minute) * num,2);
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }
    }
}
