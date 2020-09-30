using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Mmc.Mspace.Theme.Converter
{
   public class CalerdarSelectedDateCoverter : IValueConverter
    {
        public static List<DateTime> dict = new List<DateTime>();

        //static CalerdarSelectedDateCoverter()
        //{
        //    dict.Add(DateTime.Today.AddDays(-1));
        //    dict.Add(DateTime.Today.AddDays(-5));
        //    dict.Add(DateTime.Today.AddDays(-8));
        //    dict.Add(DateTime.Today.AddDays(4));

        //    dict.Add(DateTime.Today.AddDays(6));
        //    dict.Add(DateTime.Today.AddDays(8));
        //    dict.Add(DateTime.Today.AddDays(15));
        //    dict.Add(DateTime.Today.AddDays(10));
        //    dict.Add(DateTime.Today.AddDays(19));
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = null;
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict[i] == (DateTime)value)
                {
                    text = "HaveHistory";
                }
                Console.WriteLine(dict[i]);
            }

            Console.WriteLine(DateTime.Today);

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static void Update(List<DateTime> MIList)
        {
            dict.Clear();
            dict = MIList;
        }

        public static List<string> GetRecordDateStrList( )
        {
            List<string> dateList = new List<string>();
            if (dict?.Count > 0)
                foreach (var item in dict)
                    dateList.Add(item.ToString("yyyy-MM-dd"));
          return dateList;
        }
    }
}
