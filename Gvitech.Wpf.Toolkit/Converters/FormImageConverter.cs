using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Mmc.Wpf.Toolkit.Converters
{
	// Token: 0x0200000F RID: 15
	public class FormImageConverter : CoreConverter<FormImageConverter>, IValueConverter
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000027EC File Offset: 0x000009EC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Bitmap image = value as Bitmap;
			return this.GetBitmapSourceFromBitmap(image);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026FB File Offset: 0x000008FB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000280C File Offset: 0x00000A0C
		public BitmapSource GetBitmapSourceFromBitmap(Image image)
		{
			BitmapSource result;
			try
			{
                Bitmap bitmap = new Bitmap(image);
                result = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			}
			catch (Exception ex)
			{
				result = null;
			}
			return result;
		}
	}
}
