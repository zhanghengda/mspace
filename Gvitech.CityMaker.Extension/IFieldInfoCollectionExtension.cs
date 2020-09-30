using Gvitech.CityMaker.FdeCore;
using System;

public static class IFieldInfoCollectionExtension
{
	public static int FieldIndexOf(this IFieldInfoCollection @this, string fieldName)
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
			bool flag3 = @this.Count < 0;
			if (flag3)
			{
				result = -1;
			}
			else
			{
				int num = @this.IndexOf(fieldName);
				bool flag4 = num < 0;
				if (flag4)
				{
					int num2;
					for (int i = 0; i < @this.Count; i = num2 + 1)
					{
						bool flag5 = @this.Get(i).Alias.Equals(fieldName);
						if (flag5)
						{
							num = i;
							break;
						}
						num2 = i;
					}
				}
				result = num;
			}
		}
		return result;
	}
}
