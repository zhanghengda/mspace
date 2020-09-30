using Gvitech.CityMaker.FdeGeometry;
using System;

public static class IGeometryCollectionExtension
{
	public static void AddGeometrys(this IGeometryCollection @this, IGeometryCollection geometries)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new ArgumentNullException("@this");
		}
		bool flag2 = geometries == null;
		if (flag2)
		{
			throw new ArgumentNullException("geometries");
		}
		bool flag3 = @this.GeometryType != geometries.GeometryType;
		if (flag3)
		{
			throw new Exception("GeometryType不一致，无法合并");
		}
		int num;
		for (int i = 0; i < geometries.GeometryCount; i = num + 1)
		{
			IGeometry geometry = geometries.GetGeometry(i);
			@this.AddGeometry(geometry);
			num = i;
		}
	}
}
