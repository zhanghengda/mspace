using Mmc.Windows.Attributes;
using System;
using System.Linq;
using System.Reflection;

public static class PropertyInfoExtension
{
	public static string GetAliasName(this PropertyInfo @this)
	{
		object[] customAttributes = @this.GetCustomAttributes(typeof(AliasAttribute), false);
		bool flag = !customAttributes.HasValues<object>();
		string result;
		if (flag)
		{
			result = string.Empty;
		}
		else
		{
			AliasAttribute aliasAttribute = customAttributes.First<object>() as AliasAttribute;
			result = ((aliasAttribute == null) ? string.Empty : aliasAttribute.AliasName);
		}
		return result;
	}
}
