using Gvitech.CityMaker.FdeCore;
using System;

public static class IRowBufferExtension
{
	public static int IndexOf(this IRowBuffer @this, string fieldName)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new NullReferenceException("@this");
		}
		bool flag2 = string.IsNullOrEmpty(fieldName);
		int result;
		if (flag2)
		{
			result = -1;
		}
		else
		{
			result = @this.Fields.FieldIndexOf(fieldName);
		}
		return result;
	}

	public static T GetValue<T>(this IRowBuffer @this, int indx) where T : class
	{
		bool flag = indx < 0;
		T result;
		if (flag)
		{
			result = default(T);
		}
		else
		{
			T t = @this.GetValue(indx) as T;
			result = t;
		}
		return result;
	}

	public static int GetFid(this IRowBuffer @this)
	{
		string text = @this.GetValue(0).ToString();
		bool flag = text == "";
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			int num = Convert.ToInt32(text);
			result = num;
		}
		return result;
	}

	public static T GetValue<T>(this IRowBuffer @this, string fieldName) where T : class
	{
		bool flag = string.IsNullOrEmpty(fieldName);
		T result;
		if (flag)
		{
			result = default(T);
		}
		else
		{
			int indx = @this.IndexOf(fieldName);
			result = @this.GetValue(indx) as T;
		}
		return result;
	}

	public static bool SetValue(this IRowBuffer @this, string fieldName, object newValue)
	{
		bool flag = string.IsNullOrEmpty(fieldName);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			int position = @this.IndexOf(fieldName);
			@this.SetValue(position, newValue);
			result = true;
		}
		return result;
	}
}
