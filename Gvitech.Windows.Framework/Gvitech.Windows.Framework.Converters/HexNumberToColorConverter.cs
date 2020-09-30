using System;
using System.Globalization;
using System.Windows.Data;

namespace Mmc.Windows.Framework.Converters
{
	public class HexNumberToColorConverter : CoreConverter<HexNumberToColorConverter>, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint colorUint = (uint)value;
			return string.Format("#{0}", string.Format("{0:X2}", colorUint));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
