using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mmc.Mspace.Theme.Converter
{
    public class MapParameterConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Count() == 0)
                return null;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < values.Count(); i++)
            {
                string keyName = "value" + (i - 1).ToString();
                if (i == 0) keyName = "model";
                if (i == 1) keyName = "index";
                dic.Add(keyName, values[i]);
            }
            return dic;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
