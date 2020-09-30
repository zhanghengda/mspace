using System;
using System.Globalization;
using System.Windows.Data;

namespace Mmc.Wpf.Toolkit.Converters
{
	// Token: 0x02000010 RID: 16
	public class HexNumberToColorConverter : CoreConverter<HexNumberToColorConverter>, IValueConverter
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002868 File Offset: 0x00000A68
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint strk = (uint)value;
			return string.Format("#{0}", string.Format("{0:X2}", strk));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026FB File Offset: 0x000008FB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
