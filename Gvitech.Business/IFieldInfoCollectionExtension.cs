using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Mmc.Windows.Utils;

// Token: 0x02000004 RID: 4
public static class IFieldInfoCollectionExtension
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002750 File Offset: 0x00000950
	public static DataTable CreateEmptyTableWithFields(this IFieldInfoCollection fields, string[] showFields = null)
	{
		bool flag = fields == null;
		if (flag)
		{
			throw new NullReferenceException("fields");
		}
		Dictionary<IFieldInfo, Type> canBeShowFields = fields.GetCanBeShowFields();
		bool flag2 = !canBeShowFields.HasValues<IFieldInfo, Type>();
		DataTable result;
		if (flag2)
		{
			result = null;
		}
		else
		{
			List<Tuple<string, string, Type>> lst = null;
			IEnumerable<KeyValuePair<IFieldInfo, Type>> enumerable = canBeShowFields;
			bool flag3 = showFields.HasValues<string>();
			if (flag3)
			{
				enumerable = from kvp in canBeShowFields
				where showFields.Contains(kvp.Key.Name.ToLower()) || showFields.Contains(kvp.Key.Alias.ToLower())
				select kvp;
			}
			bool flag4 = enumerable != null;
			if (flag4)
			{
				lst = (from kvp in enumerable
				select new Tuple<string, string, Type>(string.IsNullOrEmpty(kvp.Key.Alias) ? kvp.Key.Name : kvp.Key.Alias, kvp.Key.Alias, kvp.Value)).ToList<Tuple<string, string, Type>>();
			}
			result = DataTableFactory.CreateDataTable(lst);
		}
		return result;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002804 File Offset: 0x00000A04
	public static Dictionary<IFieldInfo, Type> GetCanBeShowFields(this IFieldInfoCollection fields)
	{
		bool flag = fields == null;
		if (flag)
		{
			throw new NullReferenceException("fields");
		}
		int count;
		bool flag2 = (count = fields.Count) < 1;
		Dictionary<IFieldInfo, Type> result;
		if (flag2)
		{
			result = null;
		}
		else
		{
			Dictionary<IFieldInfo, Type> dictionary = new Dictionary<IFieldInfo, Type>();
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				IFieldInfo fieldInfo = fields.Get(i);
				bool flag3 = fieldInfo.CanBeShow();
				if (flag3)
				{
					dictionary.Add(fieldInfo, fieldInfo.GetSystemType());
				}
				num = i;
			}
			result = dictionary;
		}
		return result;
	}
}
