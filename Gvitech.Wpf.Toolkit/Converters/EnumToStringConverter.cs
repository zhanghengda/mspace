using System;
using System.Globalization;
using System.Windows.Data;

namespace Mmc.Wpf.Toolkit.Converters
{
	/// <summary>
	/// 将枚举类型转换为string[]
	/// </summary>
	// Token: 0x0200000E RID: 14
	public class EnumToStringConverter : IValueConverter
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027B8 File Offset: 0x000009B8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = value == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = Enum.GetNames(value.GetType());
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027E1 File Offset: 0x000009E1
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
