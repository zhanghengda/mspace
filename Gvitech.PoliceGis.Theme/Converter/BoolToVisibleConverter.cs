using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mmc.Mspace.Theme.Converter
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as bool?;
            var vis = v.HasValue && v.Value ? "Visible" : "Collapsed";
            return vis;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Cannot convert back from BoolToEnableConverter");
        }
    }
}
