using System;
using System.Windows;
using Mmc.Windows.Utils;

namespace Mmc.Wpf.Toolkit.Utils
{
	// Token: 0x02000003 RID: 3
	public class ScreenHelper
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public static double GetX(double size)
		{
			double primaryScreenWidth = SystemParameters.PrimaryScreenWidth;
			int deviceCaps = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 4);
			return size / (double)deviceCaps * primaryScreenWidth;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		public static double GetY(double size)
		{
			double primaryScreenHeight = SystemParameters.PrimaryScreenHeight;
			int deviceCaps = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 6);
			return size / (double)deviceCaps * primaryScreenHeight;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		public static double GetScreenInchSize()
		{
			int deviceCaps = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 4);
			int deviceCaps2 = WindowsApi.GetDeviceCaps(WindowsApi.GetDC(IntPtr.Zero), 6);
			double num = Math.Sqrt((double)(deviceCaps2 * deviceCaps2 + deviceCaps * deviceCaps));
			return num * 1.0 / 25.4;
		}
	}
}
