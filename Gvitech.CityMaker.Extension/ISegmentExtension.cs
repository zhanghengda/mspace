using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using System;

public static class ISegmentExtension
{
	public static double StandardDistance(this ISegment @this)
	{
		IPoint startPoint = @this.StartPoint;
		IPoint endPoint = @this.EndPoint;
		return Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2.0) + Math.Pow(startPoint.Y - endPoint.Y, 2.0));
	}

	public static IVector3 MiddlePoint(this ISegment @this)
	{
		IPoint startPoint = @this.StartPoint;
		IPoint endPoint = @this.EndPoint;
		Vector3 vector = new Vector3();
		vector.Set((startPoint.X + endPoint.X) / 2.0, (startPoint.Y + endPoint.Y) / 2.0, (startPoint.Z + endPoint.Z) / 2.0);
		return vector;
	}

    public static IPolyline ToPolyline(this ISegment @this, IGeometryFactory geometryFactory, ISpatialCRS spatialCRS)
    {
       return geometryFactory.CreatePolyline(@this.StartPoint, @this.EndPoint, spatialCRS);
    }
}
