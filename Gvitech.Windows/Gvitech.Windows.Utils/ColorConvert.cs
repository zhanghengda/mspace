using System;
using System.Drawing;
using System.Globalization;

namespace Mmc.Windows.Utils
{
    public class ColorConvert
    {
        public static uint ColorToAbgr(Color c)
        {
            int num = (int)c.R << 16;
            int num2 = (int)c.G << 8;
            int b = (int)c.B;
            int num3 = (int)((c.A == 0) ? 255 : c.A) << 24;
            return (uint)(b | num2 | num | num3);
        }

        public static uint Rgba(int r, int g, int b, int a)
        {
            return (uint)(b | g << 8 | r << 16 | a << 24);
        }

        public static Color Argb(int a, int r, int g, int b)
        {
            return Color.FromArgb(a, r, g, b);
        }




        public static uint Rgb(int r, int g, int b)
        {
            return (uint)(b | g << 8 | r << 16);
        }

        public static uint ColorStringToUInt(string value)
        {
            string[] array = value.Split(new char[]
            {
                ','
            });
            int r = 0;
            int g = 0;
            int b = 0;
            bool flag = array.Length == 3 && int.TryParse(array[0], out r) && int.TryParse(array[1], out g) && int.TryParse(array[2], out b);
            uint result;
            if (flag)
            {
                result = ColorConvert.Rgba(r, g, b, 255);
            }
            else
            {
                int a = 0;
                bool flag2 = array.Length == 4 && int.TryParse(array[0], out r) && int.TryParse(array[1], out g) && int.TryParse(array[2], out b) && int.TryParse(array[3], out a);
                if (!flag2)
                {
                    throw new ArgumentException();
                }
                result = ColorConvert.Rgba(r, g, b, a);
            }
            return result;
        }

        public static uint StringToUInt(string value)
        {
            string s = string.Format("{0:X2}", value);
            uint num = uint.TryParse(s, NumberStyles.HexNumber, null, out num) ? num : 4294967040u;
            return num;
        }

        public static string UIntToString(uint value)
        {
            return string.Format("{0:X2}", value);
        }

        public static Color Abgr2Color(int v)
        {
            Color color = Color.FromArgb(v);
            int b = (int)color.B;
            int g = (int)color.G;
            int r = (int)color.R;
            return Color.FromArgb(255, b, g, r);
        }

        public static Color Abgr2Argb2(int v)
        {
            Color color = Color.FromArgb(v);
            int a = (int)color.A;
            int b = (int)color.B;
            int g = (int)color.G;
            int r = (int)color.R;
            return Color.FromArgb(a, b, g, r);
        }

        public static Color HexNumberToColor(string value)
        {
            Color result;
            try
            {
                int num;
                int.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, null, out num);
                int num2;
                int.TryParse(value.Substring(6, 2), NumberStyles.HexNumber, null, out num2);
                int num3;
                int.TryParse(value.Substring(4, 2), NumberStyles.HexNumber, null, out num3);
                int num4;
                int.TryParse(value.Substring(2, 2), NumberStyles.HexNumber, null, out num4);
                bool flag = num == 0 && num2 == 0 && num3 == 0 && num4 == 0;
                if (flag)
                {
                    result = Color.White;
                }
                else
                {
                    result = Color.FromArgb(num, num2, num3, num4);
                }
            }
            catch
            {
                result = Color.White;
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
                    result = ColorConvert.ArgbToUint((byte)num, (byte)num4, (byte)num3, (byte)num2);
                }
            }
            catch
            {
                result = 4294967295u;
            }
            return result;
        }

        public static Color HexNumberToColor2(string value)
        {
            Color result;
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
                bool flag = num == 0 && num2 == 0 && num3 == 0 && num4 == 0;
                if (flag)
                {
                    result = Color.White;
                }
                else
                {
                    result = Color.FromArgb(num, num2, num3, num4);
                }
            }
            catch
            {
                result = Color.White;
            }
            return result;
        }

        public static Color HexNumberToColorWithTransparent(string value)
        {
            Color result;
            try
            {
                int alpha;
                int.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, null, out alpha);
                int red;
                int.TryParse(value.Substring(2, 2), NumberStyles.HexNumber, null, out red);
                int green;
                int.TryParse(value.Substring(4, 2), NumberStyles.HexNumber, null, out green);
                int blue;
                int.TryParse(value.Substring(6, 2), NumberStyles.HexNumber, null, out blue);
                result = Color.FromArgb(alpha, red, green, blue);
            }
            catch
            {
                result = Color.White;
            }
            return result;
        }

        public static Color HexNumberToColorWithoutAlpha(string value)
        {
            Color result;
            try
            {
                int num;
                int.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, null, out num);
                int num2;
                int.TryParse(value.Substring(6, 2), NumberStyles.HexNumber, null, out num2);
                int num3;
                int.TryParse(value.Substring(4, 2), NumberStyles.HexNumber, null, out num3);
                int num4;
                int.TryParse(value.Substring(2, 2), NumberStyles.HexNumber, null, out num4);
                bool flag = num == 0 && num2 == 0 && num3 == 0 && num4 == 0;
                if (flag)
                {
                    result = Color.White;
                }
                else
                {
                    result = Color.FromArgb(255, num2, num3, num4);
                }
            }
            catch
            {
                result = Color.White;
            }
            return result;
        }

        public static int GetAlphaFromHexNumber(string value)
        {
            int result;
            try
            {
                int num;
                int.TryParse(value.Substring(0, 2), NumberStyles.HexNumber, null, out num);
                result = num;
            }
            catch
            {
                result = 255;
            }
            return result;
        }

        public static string ReplaceAlphaInHexNumber(string value, double alpha)
        {
            string result;
            try
            {
                int num = (int)Math.Floor(alpha * 255.0 / 100.0);
                string text = string.Format("{0:X2}{1}", num, value.Substring(2));
                result = text;
            }
            catch
            {
                result = "FFFFFFFF";
            }
            return result;
        }

        public static Color UintToColor(uint colorUint)
        {
            byte[] array = new byte[4];
            int num;
            for (int i = 0; i < 4; i = num + 1)
            {
                array[i] = (byte)((colorUint & 255u) % 256u);
                colorUint >>= 8;
                num = i;
            }
            return Color.FromArgb((int)array[3], (int)array[2], (int)array[1], (int)array[0]);
        }

        public static uint AlphaUintToUint(byte alpha, uint colorUint)
        {
            return (colorUint & 16777215u) | (uint)((uint)alpha << 24);
        }

        public static uint AlphaUintToUint(uint alpha, uint colorUint)
        {
            return (colorUint & 16777215u) | alpha << 24;
        }

        public static string ColorToHexNumber(Color color)
        {
            string result;
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

        public static string ColorToHexNumber2(Color color)
        {
            string result;
            try
            {
                int a = (int)color.A;
                int r = (int)color.R;
                int g = (int)color.G;
                int b = (int)color.B;
                result = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[]
                {
                    a,
                    r,
                    g,
                    b
                });
            }
            catch
            {
                result = "FFFFFFFF";
            }
            return result;
        }

        public static string ColorToHexNumber(Color color, byte alpha)
        {
            string result;
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

        public static uint ArgbToUint(byte a, byte r, byte g, byte b)
        {
            return (uint)((int)b | (int)g << 8 | (int)r << 16 | (int)a << 24);
        }



        public static uint GetRandomUint()
        {
            Color randomArgb = ColorConvert.GetRandomArgb();
            return ColorConvert.ColorToAbgr(randomArgb);
        }

        public static Color GetRandomArgb()
        {
            Random random = new Random();
            int red = random.Next(0, 255);
            int green = random.Next(0, 255);
            int blue = random.Next(0, 255);
            return Color.FromArgb(255, red, green, blue);
        }

        public static int Apla2Percent(int apla)
        {
            return (int)Math.Round((double)(apla * 100) / 255.0);
        }

        public static int Percent2Apla(int percent)
        {
            return (int)Math.Round((double)percent * 255.0 / 100.0);
        }

        public static Color Threshold2Color(int iThreshold)
        {
            int num = iThreshold * 255 / 100;
            return Color.FromArgb(255, num, num, num);
        }

        public static uint Threshold2Uint(int iThreshold)
        {
            Color c = ColorConvert.Threshold2Color(iThreshold);
            return ColorConvert.ColorToAbgr(c);
        }

        public static uint ColorParse(string strColor)
        {
            uint result;
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

        public static Color UintColorParse(string strColor)
        {
            var res = ColorParse(strColor);
            return UintToColor(res);
        }
    }
}
