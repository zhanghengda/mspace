using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

public static class PropertyInfoArrayExtension
{
	public static DataTable CreateEmptyDataTable(this PropertyInfo[] @this, string[] showFields = null)
	{
		bool flag = !@this.HasValues<PropertyInfo>();
		DataTable result;
		if (flag)
		{
			result = null;
		}
		else
		{
			bool flag2 = showFields.HasValues<string>();
			string alisaName = string.Empty;
			List<Tuple<string, string, Type>> lst = new List<Tuple<string, string, Type>>();
			bool flag3 = !flag2;
			if (flag3)
			{
				@this.ToList<PropertyInfo>().ForEach(delegate(PropertyInfo p)
				{
					alisaName = p.GetAliasName();
					lst.AddEx(new Tuple<string, string, Type>(string.IsNullOrEmpty(alisaName) ? p.Name : alisaName, alisaName, p.PropertyType));
				});
			}
			else
			{
				@this.ToList<PropertyInfo>().ForEach(delegate(PropertyInfo p)
				{
					alisaName = p.GetAliasName();
					bool flag4 = !string.IsNullOrEmpty(alisaName);
					if (flag4)
					{
						bool flag5 = showFields.Contains(alisaName.ToLower());
						if (flag5)
						{
							lst.AddEx(new Tuple<string, string, Type>(alisaName, alisaName, p.PropertyType));
						}
						else
						{
							bool flag6 = showFields.Contains(p.Name.ToLower());
							if (flag6)
							{
								lst.AddEx(new Tuple<string, string, Type>(p.Name, alisaName, p.PropertyType));
							}
						}
					}
				});
			}
			result = DataTableFactory.CreateDataTable(lst);
		}
		return result;
	}
}
