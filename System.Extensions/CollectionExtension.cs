using System;
using System.Collections.Generic;

public static class CollectionExtension
{
	public static bool AddEx<T>(this List<T> @this, T value)
	{
		bool flag = @this == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = value == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = @this.Contains(value);
				if (flag3)
				{
					result = false;
				}
				else
				{
					@this.Add(value);
					result = true;
				}
			}
		}
		return result;
	}

	public static bool AddEx<T, V>(this Dictionary<T, V> @this, T key, V value)
	{
		bool flag = @this == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			bool flag2 = key == null || value == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = @this.ContainsKey(key);
				if (flag3)
				{
					result = false;
				}
				else
				{
					@this.Add(key, value);
					result = true;
				}
			}
		}
		return result;
	}

	public static bool HasValues<T>(this T[] @this)
	{
		return @this != null && @this.Length != 0;
	}
}
