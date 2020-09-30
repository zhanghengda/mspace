using Gvitech.CityMaker.FdeGeometry;
using System;

public static class ISpatialCRSExtentions
{
	public static bool IsSameTypeCRS(this ISpatialCRS @this, ISpatialCRS other)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new ArgumentNullException("@this");
		}
		bool flag2 = other == null;
		if (flag2)
		{
			throw new ArgumentNullException("other");
		}
		int crsType = ISpatialCRSExtentions.GetCrsType(@this);
		int crsType2 = ISpatialCRSExtentions.GetCrsType(other);
		return Convert.ToBoolean(crsType & crsType2);
	}

	private static int GetCrsType(ISpatialCRS crs)
	{
		return (crs == null) ? 0 : (crs.IsGeographic() ? 1 : (crs.IsProjected() ? 2 : 4));
	}
}
