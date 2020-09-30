using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Mmc.Windows.Framework.Converters
{
	public class FormImageConverter : CoreConverter<FormImageConverter>, IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Bitmap bitmap = value as Bitmap;
			return this.GetBitmapSourceFromBitmap(bitmap);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public BitmapSource GetBitmapSourceFromBitmap(Image image)
		{
			Bitmap bitmap = new Bitmap(image);
			BitmapSource imageSource;
			try
			{
				imageSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
			}
			catch (Exception ex_27)
			{
				imageSource = null;
			}
			return imageSource;
		}
	}
}
