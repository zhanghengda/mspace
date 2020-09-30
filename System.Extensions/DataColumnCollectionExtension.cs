using System;
using System.Data;

public static class DataColumnCollectionExtension
{
	public static DataColumn Add(this DataColumnCollection @this, string columnName, string columnCaption, Type type)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new NullReferenceException("Columns is null");
		}
		bool flag2 = string.IsNullOrEmpty(columnName);
		if (flag2)
		{
			throw new ArgumentNullException("columnName");
		}
		DataColumn dataColumn = @this.Add(columnName, type);
		dataColumn.Caption = (string.IsNullOrEmpty(columnCaption) ? columnName : columnCaption);
		return dataColumn;
	}
}
