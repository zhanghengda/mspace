using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mmc.Windows.Framework.Converters
{
	public sealed class NullOrEmptyStringToVisibilityConverter : CoreConverter<NullOrEmptyStringToVisibilityConverter>, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value == null || string.IsNullOrEmpty(value.ToString())) ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
