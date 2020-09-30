using System;

public static class DateTimeExtensions
{
	public static string ChineseTwentyFourDay(this DateTime date1)
	{
		string[] SolarTerm = new string[]
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
		int[] sTermInfo = new int[]
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
		DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0);
		string tempStr = "";
		int y = date1.Year;
		for (int i = 1; i <= 24; i++)
		{
			double num = 525948.76 * (double)(y - 1900) + (double)sTermInfo[i - 1];
			if (baseDateAndTime.AddMinutes(num).DayOfYear > date1.DayOfYear)
			{
				break;
			}
			tempStr = SolarTerm[i - 1];
		}
		return tempStr;
	}

	public static DateTime GetDateTimeOfChineseTwentyFourDay(this DateTime date1, string day24Name)
	{
		string[] SolarTerm = new string[]
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
		int[] sTermInfo = new int[]
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
		DateTime baseDateAndTime = new DateTime(1900, 1, 6, 2, 5, 0);
		DateTime newDate = date1;
		int y = date1.Year;
		for (int i = 1; i <= 24; i++)
		{
			if (SolarTerm[i - 1] == day24Name)
			{
				double num = 525948.76 * (double)(y - 1900) + (double)sTermInfo[i - 1];
				newDate = baseDateAndTime.AddMinutes(num);
				break;
			}
		}
		return newDate;
	}
}
