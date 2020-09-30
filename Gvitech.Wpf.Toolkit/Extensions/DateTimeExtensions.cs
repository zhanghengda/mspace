using System;

namespace Mmc.Wpf.Toolkit.Extensions
{
	// Token: 0x0200000B RID: 11
	public static class DateTimeExtensions
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000023D0 File Offset: 0x000005D0
		public static string ChineseTwentyFourDay(this DateTime date1)
		{
			string[] array = new string[]
			{
				"小寒",
				"大寒",
				"立春",
				"雨水",
				"惊蛰",
				"春分",
				"清明",
				"谷雨",
				"立夏",
				"小满",
				"芒种",
				"夏至",
				"小暑",
				"大暑",
				"立秋",
				"处暑",
				"白露",
				"秋分",
				"寒露",
				"霜降",
				"立冬",
				"小雪",
				"大雪",
				"冬至"
			};
			int[] array2 = new int[]
			{
				0,
				21208,
				42467,
				63836,
				85337,
				107014,
				128867,
				150921,
				173149,
				195551,
				218072,
				240693,
				263343,
				285989,
				308563,
				331033,
				353350,
				375494,
				397447,
				419210,
				440795,
				462224,
				483532,
				504758
			};
			DateTime dateTime = new DateTime(1900, 1, 6, 2, 5, 0);
			string result = "";
			int year = date1.Year;
			int num;
			for (int i = 1; i <= 24; i = num + 1)
			{
				double value = 525948.76 * (double)(year - 1900) + (double)array2[i - 1];
				bool flag = dateTime.AddMinutes(value).DayOfYear <= date1.DayOfYear;
				if (!flag)
				{
					break;
				}
				result = array[i - 1];
				num = i;
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002560 File Offset: 0x00000760
		public static DateTime GetDateTimeOfChineseTwentyFourDay(this DateTime date1, string day24Name)
		{
			string[] array = new string[]
			{
				"小寒",
				"大寒",
				"立春",
				"雨水",
				"惊蛰",
				"春分",
				"清明",
				"谷雨",
				"立夏",
				"小满",
				"芒种",
				"夏至",
				"小暑",
				"大暑",
				"立秋",
				"处暑",
				"白露",
				"秋分",
				"寒露",
				"霜降",
				"立冬",
				"小雪",
				"大雪",
				"冬至"
			};
			int[] array2 = new int[]
			{
				0,
				21208,
				42467,
				63836,
				85337,
				107014,
				128867,
				150921,
				173149,
				195551,
				218072,
				240693,
				263343,
				285989,
				308563,
				331033,
				353350,
				375494,
				397447,
				419210,
				440795,
				462224,
				483532,
				504758
			};
			DateTime dateTime = new DateTime(1900, 1, 6, 2, 5, 0);
			DateTime result = date1;
			int year = date1.Year;
			int num;
			for (int i = 1; i <= 24; i = num + 1)
			{
				bool flag = array[i - 1] == day24Name;
				if (flag)
				{
					double value = 525948.76 * (double)(year - 1900) + (double)array2[i - 1];
					result = dateTime.AddMinutes(value);
					break;
				}
				num = i;
			}
			return result;
		}
	}
}
