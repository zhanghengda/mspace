using System;
using System.Runtime.InteropServices;

namespace Mmc.Windows.Utils
{
	public class WindowsApi
	{
		public const int WM_SYSCOMMAND = 274;

		public const uint SC_CLOSE = 61536u;

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegisterWindowMessage(string lpString);

		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int Index);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hwnd);
	}
}
