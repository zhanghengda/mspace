using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mmc.Wpf.Toolkit.Converters
{
	// Token: 0x02000011 RID: 17
	public class NullOrEmptyStringToInvertVisibilityConverter : CoreConverter<NullOrEmptyStringToInvertVisibilityConverter>, IValueConverter
	{
		/// <summary>
		///     转换值。
		/// </summary>
		/// <param name="value">绑定源生成的值。</param>
		/// <param name="targetType">绑定目标属性的类型。</param>
		/// <param name="parameter">要使用的转换器参数。</param>
		/// <param name="culture">要用在转换器中的区域性。</param>
		/// <returns>转换后的值。 如果该方法返回 null，则使用有效的 null 值。</returns>
		// Token: 0x06000036 RID: 54 RVA: 0x000028A4 File Offset: 0x00000AA4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value == null || string.IsNullOrEmpty(value.ToString())) ? Visibility.Visible : Visibility.Collapsed;
		}

		/// <summary>
		///     转换值。
		/// </summary>
		/// <param name="value">绑定目标生成的值。</param>
		/// <param name="targetType">要转换到的类型。</param>
		/// <param name="parameter">要使用的转换器参数。</param>
		/// <param name="culture">要用在转换器中的区域性。</param>
		/// <returns>转换后的值。 如果该方法返回 null，则使用有效的 null 值。</returns>
		/// <exception cref="T:System.NotImplementedException"></exception>
		// Token: 0x06000037 RID: 55 RVA: 0x000026FB File Offset: 0x000008FB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
