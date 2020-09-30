using System;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace Mmc.LayerSymbol
{
	public class LayerStyleHelper
	{
		public static uint ColorParse(string strColor)
		{
			uint result = 4294967295u;
			try
			{
				result = uint.Parse(strColor);
			}
			catch
			{
				string value = "0x" + strColor;
				result = Convert.ToUInt32(value, 16);
			}
			return result;
		}

		public static XmlNode GetNode(XmlDocument xmlDoc, string path)
		{
			XmlNode result;
			try
			{
				XmlNode xmlNode = xmlDoc.SelectSingleNode(path);
				bool flag = xmlNode == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = xmlNode;
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("GetNode Function Error", innerException);
			}
			return result;
		}

		public static string ColorToHexNumber(Color color)
		{
			string result = "";
			try
			{
				int a = (int)color.A;
				int r = (int)color.R;
				int g = (int)color.G;
				int b = (int)color.B;
				result = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
				{
					a,
					b,
					g,
					r
				});
			}
			catch
			{
				result = "FFFFFFFF";
			}
			return result;
		}

		public static uint HexNumberToUint(string value)
		{
			uint result;
			try
			{
				int num;
				int.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, null, out num);
				int num2;
				int.TryParse(value.Substring(2, 2), NumberStyles.HexNumber, null, out num2);
				int num3;
				int.TryParse(value.Substring(4, 2), NumberStyles.HexNumber, null, out num3);
				int num4;
				int.TryParse(value.Substring(6, 2), NumberStyles.HexNumber, null, out num4);
				bool flag = num == 0 && num4 == 0 && num3 == 0 && num2 == 0;
				if (flag)
				{
					result = 4294967295u;
				}
				else
				{
					result = LayerStyleHelper.ARGBToUint((byte)num, (byte)num4, (byte)num3, (byte)num2);
				}
			}
			catch
			{
				result = 4294967295u;
			}
			return result;
		}

		public static uint ARGBToUint(byte a, byte r, byte g, byte b)
		{
			return (uint)((int)b | (int)g << 8 | (int)r << 16 | (int)a << 24);
		}

		public static string ColorToHexNumber(Color color, byte alpha)
		{
			string result = "";
			try
			{
				int r = (int)color.R;
				int g = (int)color.G;
				int b = (int)color.B;
				result = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
				{
					(int)alpha,
					b,
					g,
					r
				});
			}
			catch
			{
				result = "FFFFFFFF";
			}
			return result;
		}
	}
}
