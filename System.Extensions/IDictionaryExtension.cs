using System;
using System.Collections.Generic;
using System.Linq;

public static class IDictionaryExtension
{
	public static bool HasValues<TKey, TValue>(this IDictionary<TKey, TValue> @this)
	{
		return @this != null && @this.Any<KeyValuePair<TKey, TValue>>();
	}

	public static bool AddUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
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
					@this[key] = value;
					result = true;
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
}
