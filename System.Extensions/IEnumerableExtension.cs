using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
	public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
	{
		foreach (T current in @this)
		{
			action(current);
		}
	}

	public static bool HasValues<T>(this IEnumerable<T> @this)
	{
		return @this != null && @this.Any<T>();
	}
}
