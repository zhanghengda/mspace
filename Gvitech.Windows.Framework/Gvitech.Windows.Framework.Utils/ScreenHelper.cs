using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Windows;

namespace Mmc.Windows.Framework.Utils
{
	public class ScreenHelper : Singleton<ScreenHelper>
	{
		public double GetX(double size)
		{
			double dWidth = SystemParameters.PrimaryScreenWidth;
			int width = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 4);
			return size / (double)width * dWidth;
		}

		public double GetY(double size)
		{
			double dHeight = SystemParameters.PrimaryScreenHeight;
			int height = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 6);
			return size / (double)height * dHeight;
		}

		public double GetScreenInchSize()
		{
			int width = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 4);
			int height = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 6);
			double fDiagonalLen = Math.Sqrt((double)(height * height + width * width));
			return fDiagonalLen * 1.0 / 25.4;
		}
	}
}
