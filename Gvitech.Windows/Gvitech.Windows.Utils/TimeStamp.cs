using Mmc.Windows.Design;
using System;

namespace Mmc.Windows.Utils
{
    public class TimeStamp : Singleton<TimeStamp>
    {
        public DateTime TimeStampToDate(long timeStamp)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            long ticks = timeStamp * 10000000L;
            TimeSpan value = new TimeSpan(ticks);
            return dateTime.Add(value);
        }

        public long DateToTimeStamp(DateTime dateTime)
        {
            DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)(dateTime - d).TotalSeconds;
        }

        public long DateToMilSecTimeStamp(DateTime dateTime)
        {
            DateTime d = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)((dateTime - d).TotalMilliseconds * 1000.0);
        }

        /// <summary>
        /// 秒转时分秒
        /// </summary>
        /// <returns></returns>
        public string DurationToHMS(float duration)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(duration));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "小时 " + ts.Minutes.ToString() + "分钟 " + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "分钟 " + ts.Seconds + "秒";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "秒";
            }
            return str;
        }
    }
}
