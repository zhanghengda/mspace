using Gvitech.CityMaker.FdeGeometry;
using Mmc.Windows.Utils;
using System;

public static class FdeGeometryRelease
{
	public static bool ReleaseComObject(this IGeometry @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this IGeometryFactory @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this ICRSFactory @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this ICoordinateTransformer @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}

	public static bool ReleaseComObject(this ICoordinateReferenceSystem @this)
	{
		return ComFactory.ReleaseComObject(@this);
	}
}
