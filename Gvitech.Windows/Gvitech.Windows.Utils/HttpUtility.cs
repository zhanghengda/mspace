using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mmc.Windows.Utils
{
	public class HttpUtility
	{
		private static readonly char[] HtmlEntityEndingChars = new char[]
		{
			';',
			'&'
		};

		public static string UrlEncode(string str)
		{
			bool flag = str == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Encoding uTF = Encoding.UTF8;
				result = Encoding.ASCII.GetString(HttpUtility.UrlEncodeToBytes(str, uTF));
			}
			return result;
		}

		public static byte[] UrlEncodeToBytes(string str, Encoding e)
		{
			bool flag = str == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] bytes = e.GetBytes(str);
				result = HttpUtility.UrlEncode(bytes, 0, bytes.Length);
			}
			return result;
		}

		private static byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			int num = 0;
			int num2 = 0;
			int num3;
			for (int i = 0; i < count; i = num3 + 1)
			{
				char c = (char)bytes[offset + i];
				bool flag = c == ' ';
				if (flag)
				{
					num3 = num;
					num = num3 + 1;
				}
				else
				{
					bool flag2 = !HttpUtility.IsUrlSafeChar(c);
					if (flag2)
					{
						num3 = num2;
						num2 = num3 + 1;
					}
				}
				num3 = i;
			}
			bool flag3 = num == 0 && num2 == 0;
			byte[] result;
			if (flag3)
			{
				result = bytes;
			}
			else
			{
				byte[] array = new byte[count + num2 * 2];
				int num4 = 0;
				for (int j = 0; j < count; j = num3 + 1)
				{
					byte b = bytes[offset + j];
					char c2 = (char)b;
					bool flag4 = HttpUtility.IsUrlSafeChar(c2);
					if (flag4)
					{
						byte[] arg_B1_0 = array;
						num3 = num4;
						num4 = num3 + 1;
						arg_B1_0[num3] = b;
					}
					else
					{
						bool flag5 = c2 == ' ';
						if (flag5)
						{
							byte[] arg_CF_0 = array;
							num3 = num4;
							num4 = num3 + 1;
							arg_CF_0[num3] = 43;
						}
						else
						{
							byte[] arg_E1_0 = array;
							num3 = num4;
							num4 = num3 + 1;
							arg_E1_0[num3] = 37;
							byte[] arg_FA_0 = array;
							num3 = num4;
							num4 = num3 + 1;
							arg_FA_0[num3] = (byte)HttpUtility.IntToHex(b >> 4 & 15);
							byte[] arg_111_0 = array;
							num3 = num4;
							num4 = num3 + 1;
							arg_111_0[num3] = (byte)HttpUtility.IntToHex((int)(b & 15));
						}
					}
					num3 = j;
				}
				result = array;
			}
			return result;
		}

		public static bool IsUrlSafeChar(char ch)
		{
			bool flag = (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9');
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				if (ch != '!')
				{
					switch (ch)
					{
					case '(':
					case ')':
					case '*':
					case '-':
					case '.':
						goto IL_68;
					case '+':
					case ',':
						break;
					default:
						if (ch == '_')
						{
							goto IL_68;
						}
						break;
					}
					result = false;
					return result;
				}
				IL_68:
				result = true;
			}
			return result;
		}

		public static char IntToHex(int n)
		{
			bool flag = n <= 9;
			char result;
			if (flag)
			{
				result = (char)(n + 48);
			}
			else
			{
				result = (char)(n - 10 + 97);
			}
			return result;
		}

		public static string UrlDecode(string value)
		{
			bool flag = value == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Encoding uTF = Encoding.UTF8;
				int length = value.Length;
				UrlDecoder urlDecoder = new UrlDecoder(length, uTF);
				int i = 0;
				while (i < length)
				{
					char c = value[i];
					bool flag2 = c == '+';
					if (flag2)
					{
						c = ' ';
						goto IL_172;
					}
					bool flag3 = c == '%' && i < length - 2;
					if (!flag3)
					{
						goto IL_172;
					}
					bool flag4 = value[i + 1] == 'u' && i < length - 5;
					if (flag4)
					{
						int num = HttpUtility.HexToInt(value[i + 2]);
						int num2 = HttpUtility.HexToInt(value[i + 3]);
						int num3 = HttpUtility.HexToInt(value[i + 4]);
						int num4 = HttpUtility.HexToInt(value[i + 5]);
						bool flag5 = num < 0 || num2 < 0 || num3 < 0 || num4 < 0;
						if (flag5)
						{
							goto IL_172;
						}
						c = (char)(num << 12 | num2 << 8 | num3 << 4 | num4);
						i += 5;
						urlDecoder.AddChar(c);
					}
					else
					{
						int num5 = HttpUtility.HexToInt(value[i + 1]);
						int num6 = HttpUtility.HexToInt(value[i + 2]);
						bool flag6 = num5 >= 0 && num6 >= 0;
						if (!flag6)
						{
							goto IL_172;
						}
						byte b = (byte)(num5 << 4 | num6);
						i += 2;
						urlDecoder.AddByte(b);
					}
					IL_19E:
					int num7 = i;
					i = num7 + 1;
					continue;
					IL_172:
					bool flag7 = (c & 'ﾀ') == '\0';
					if (flag7)
					{
						urlDecoder.AddByte((byte)c);
					}
					else
					{
						urlDecoder.AddChar(c);
					}
					goto IL_19E;
				}
				result = urlDecoder.GetString();
			}
			return result;
		}

		public static int HexToInt(char h)
		{
			bool flag = h >= '0' && h <= '9';
			int result;
			if (flag)
			{
				result = (int)(h - '0');
			}
			else
			{
				bool flag2 = h >= 'a' && h <= 'f';
				if (flag2)
				{
					result = (int)(h - 'a' + '\n');
				}
				else
				{
					bool flag3 = h >= 'A' && h <= 'F';
					if (flag3)
					{
						result = (int)(h - 'A' + '\n');
					}
					else
					{
						result = -1;
					}
				}
			}
			return result;
		}

		public static string HtmlEncode(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = value;
			}
			else
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				HttpUtility.HtmlEncode(value, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		public unsafe static void HtmlEncode(string value, TextWriter output)
		{
			bool flag = value != null;
			if (flag)
			{
				bool flag2 = output == null;
				if (flag2)
				{
					throw new ArgumentNullException("output");
				}
				int num = HttpUtility.IndexOfHtmlEncodingChars(value, 0);
				bool flag3 = num == -1;
				if (flag3)
				{
					output.Write(value);
				}
				else
				{
					int num2 = value.Length - num;
					fixed (char* ptr = value.ToCharArray())
					{
						char* ptr2 = ptr;
						char* ptr3 = ptr2;
						while (true)
						{
							int num3 = num;
							num = num3 - 1;
							if (num3 <= 0)
							{
								break;
							}
							char* ptr4 = ptr3;
							ptr3 = ptr4 + 1;
							output.Write(*ptr3);
						}
						while (true)
						{
							int num3 = num2;
							num2 = num3 - 1;
							if (num3 <= 0)
							{
								break;
							}
							char* ptr4 = ptr3;
							ptr3 = ptr4 + 1;
							char c = *ptr3;
							bool flag4 = c <= '>';
							if (flag4)
							{
								char c2 = c;
								if (c2 <= '&')
								{
									if (c2 == '"')
									{
										output.Write("&quot;");
										continue;
									}
									if (c2 == '&')
									{
										output.Write("&amp;");
										continue;
									}
								}
								else
								{
									if (c2 == '\'')
									{
										output.Write("'");
										continue;
									}
									if (c2 == '<')
									{
										output.Write("&lt;");
										continue;
									}
									if (c2 == '>')
									{
										output.Write("&gt;");
										continue;
									}
								}
								output.Write(c);
							}
							else
							{
								bool flag5 = c >= '\u00a0' && c < 'Ā';
								if (flag5)
								{
									output.Write("&#");
									output.Write(c.ToString(NumberFormatInfo.InvariantInfo));
									output.Write(';');
								}
								else
								{
									output.Write(c);
								}
							}
						}
					}
				}
			}
		}

		public static string HtmlDecode(string value)
		{
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = value;
			}
			else
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				HttpUtility.HtmlDecode(value, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		public static void HtmlDecode(string value, TextWriter output)
		{
			bool flag = value != null;
			if (flag)
			{
				bool flag2 = output == null;
				if (flag2)
				{
					throw new ArgumentNullException("output");
				}
				bool flag3 = value.IndexOf('&') < 0;
				if (flag3)
				{
					output.Write(value);
				}
				else
				{
					int length = value.Length;
					int i = 0;
					while (i < length)
					{
						char c = value[i];
						bool flag4 = c == '&';
						if (flag4)
						{
							int num = value.IndexOfAny(HttpUtility.HtmlEntityEndingChars, i + 1);
							bool flag5 = num > 0 && value[num] == ';';
							if (flag5)
							{
								string text = value.Substring(i + 1, num - i - 1);
								bool flag6 = text.Length > 1 && text[0] == '#';
								if (flag6)
								{
									bool flag7 = text[1] == 'x' || text[1] == 'X';
									ushort num2;
									if (flag7)
									{
										ushort.TryParse(text.Substring(2), NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out num2);
									}
									else
									{
										ushort.TryParse(text.Substring(1), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2);
									}
									bool flag8 = num2 > 0;
									if (flag8)
									{
										c = (char)num2;
										i = num;
									}
								}
								else
								{
									i = num;
									char c2 = HtmlEntities.Lookup(text);
									bool flag9 = c2 > '\0';
									if (!flag9)
									{
										output.Write('&');
										output.Write(text);
										output.Write(';');
										goto IL_186;
									}
									c = c2;
								}
							}
							goto IL_17D;
						}
						goto IL_17D;
						IL_186:
						int num3 = i;
						i = num3 + 1;
						continue;
						IL_17D:
						output.Write(c);
						goto IL_186;
					}
				}
			}
		}

		private unsafe static int IndexOfHtmlEncodingChars(string s, int startPos)
		{
			int i = s.Length - startPos;
			int result;
			fixed (char* ptr = s.ToCharArray())
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + startPos;
				while (i > 0)
				{
					char c = *ptr3;
					bool flag = c <= '>';
					if (flag)
					{
						char c2 = c;
						if (c2 <= '&')
						{
							if (c2 != '"' && c2 != '&')
							{
								goto IL_95;
							}
						}
						else if (c2 != '\'')
						{
							switch (c2)
							{
							case '<':
							case '>':
								break;
							case '=':
								goto IL_C0;
							default:
								goto IL_95;
							}
						}
						result = s.Length - i;
						return result;
						IL_95:;
					}
					else
					{
						bool flag2 = c >= '\u00a0' && c < 'Ā';
						if (flag2)
						{
							result = s.Length - i;
							return result;
						}
					}
					IL_C0:
					char* ptr4 = ptr3;
					ptr3 = ptr4 + 1;
					int num = i;
					i = num - 1;
				}
			}
			result = -1;
			return result;
		}
	}
}
