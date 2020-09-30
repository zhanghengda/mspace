using System;
using System.Collections.Generic;
using System.Data;
using Gvitech.CityMaker.FdeCore;
using Mmc.Windows.Utils;

// Token: 0x02000007 RID: 7
public static class IRowBufferExtension
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002A14 File Offset: 0x00000C14
	public static DataTable ToDataTable(this IRowBuffer row)
	{
		bool flag = row == null;
		if (flag)
		{
			throw new NullReferenceException("row");
		}
		string queryFilter = "属性";
		string cursor = "值";
		DataTable dataTable = DataTableFactory.CreateDataTable(new List<Tuple<string, string, Type>>(2)
		{
			new Tuple<string, string, Type>(queryFilter, queryFilter, typeof(string)),
			new Tuple<string, string, Type>(cursor, queryFilter, typeof(string))
		});
		IFieldInfoCollection fields = row.Fields;
		int num;
		for (int i = 0; i < row.FieldCount; i = num + 1)
		{
			IFieldInfo fieldInfo;
			bool flag2 = !(fieldInfo = fields.Get(i)).CanBeShow();
			if (!flag2)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow.ItemArray = new object[]
				{
					string.IsNullOrEmpty(fieldInfo.Alias) ? fieldInfo.Name : fieldInfo.Alias,
					row.GetValue(i)
				};
				dataTable.Rows.Add(dataRow);
			}
			num = i;
		}
		return dataTable;
	}
}
