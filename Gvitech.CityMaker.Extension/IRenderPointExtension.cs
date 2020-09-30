using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using System;

public static class IRenderPointExtension
{
	public static bool SetPosition(this IRenderPoint @this, double x, double y, double z = 0.0)
	{
		bool flag = @this == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			IGeometryFactory geometryFactory = new GeometryFactory();
			IPoint point = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.X = x;
			point.Y = y;
			point.Z = z;
			@this.SetFdeGeometry(point);
			geometryFactory.ReleaseComObject();
			result = true;
		}
		return result;
	}

	public static bool SetPosition(this IRenderPoint @this, IGeometryFactory geoFactory, double x, double y, double z = 0.0)
	{
		bool flag = @this == null || geoFactory == null;
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			IPoint point = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.X = x;
			point.Y = y;
			point.Z = z;
			@this.SetFdeGeometry(point);
			result = true;
		}
		return result;
	}
}
