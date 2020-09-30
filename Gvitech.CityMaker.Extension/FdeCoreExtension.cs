using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Mmc.Windows.Utils;
using System;

public static class FdeCoreExtension
{
	public static bool ReleaseComObject(this IDataSourceFactory @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IDataSource @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IFdeCursor @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IQueryFilter @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IBinaryBuffer @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IRowBuffer @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IFieldInfoCollection @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IFieldInfo @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IObjectClass @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}
}
