using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gvitech.CityMaker.FdeCore;

// Token: 0x02000002 RID: 2
public static class IFdeCursorExtension
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static DataTable ToDataTable(this IFdeCursor cursor, string[] showFields = null)
	{
		bool flag = cursor == null;
		DataTable result;
		if (flag)
		{
			result = null;
		}
		else
		{
			IFieldInfoCollection fields = cursor.GetFields();
			bool flag2 = fields == null || fields.Count < 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				bool flag3 = showFields.HasValues<string>();
				if (flag3)
				{
					List<string> list = showFields.ToList<string>();
					list.Add("oid");
					showFields = list.ToArray();
				}
				DataTable dataTable = fields.CreateEmptyTableWithFields(showFields);
				int count = dataTable.Columns.Count;
				IRowBuffer rowBuffer;
				while ((rowBuffer = cursor.NextRow()) != null)
				{
					DataRow dataRow = dataTable.NewRow();
					int num;
					for (int i = 0; i < count; i = num + 1)
					{
						dataRow[i] = rowBuffer.GetValue(fields.FieldIndexOf(dataTable.Columns[i].ColumnName));
						num = i;
					}
					dataTable.Rows.Add(dataRow);
				}
				result = dataTable;
			}
		}
		return result;
	}
}
