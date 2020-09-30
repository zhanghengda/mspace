using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using System;

public static class IGemetryExtension
{
	public static bool ProjectEx(this IGeometry @this, string wkt)
	{
		bool flag = @this == null || @this.SpatialCRS == null || string.IsNullOrEmpty(wkt);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			ICRSFactory iCRSFactory = new CRSFactory();
			ISpatialCRS sRSTarget = iCRSFactory.CreateFromWKT(wkt) as ISpatialCRS;
			iCRSFactory.ReleaseComObject();
			result = @this.Project(sRSTarget);
		}
		return result;
	}
       
    public static void SetPostion(IPoint point,IVector3 v)
    {

    }
}
