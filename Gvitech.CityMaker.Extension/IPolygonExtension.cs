using Gvitech.CityMaker.FdeGeometry;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;

public static class IPolygonExtension
{
	public static IPolygon CreatePolygonWithZ(this IPolygon @this, double height)
	{
		bool flag = @this == null;
		IPolygon result;
		if (flag)
		{
			result = null;
		}
		else
		{
			IPolygon polygon = (IPolygon)@this.Clone2(gviVertexAttribute.gviVertexAttributeZ );
			IGeometryFactory this2 = new GeometryFactory();
			IRing exteriorRing = polygon.ExteriorRing;
			int num;
			for (int i = 0; i < exteriorRing.PointCount; i = num + 1)
			{
				IPoint point = exteriorRing.GetPoint(i);
				IPoint pointValue = this2.CreatePoint(point.X, point.Y, height, null);
				exteriorRing.UpdatePoint(i, pointValue);
				num = i;
			}
			for (int j = 0; j < polygon.InteriorRingCount; j = num + 1)
			{
				IRing interiorRing = polygon.GetInteriorRing(j);
				for (int k = 0; k < interiorRing.PointCount; k = num + 1)
				{
					IPoint point2 = interiorRing.GetPoint(k);
					IPoint pointValue2 = this2.CreatePoint(point2.X, point2.Y, height, null);
					interiorRing.UpdatePoint(k, pointValue2);
					num = k;
				}
				num = j;
			}
			result = polygon;
		}
		return result;
	}

	public static List<IPolygon> CreateHeightControlBox(this IPolygon @this, double baseHeight, double limitHeight)
	{
		bool flag = @this == null || @this.ExteriorRing == null;
		List<IPolygon> result;
		if (flag)
		{
			result = null;
		}
		else
		{
			List<IPolygon> list = new List<IPolygon>();
			double z = baseHeight + limitHeight;
			IGeometryFactory geometryFactory = new GeometryFactory();
			IRing exteriorRing = @this.ExteriorRing;
			IPolygon polygon = (IPolygon)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ );
			IPoint point = (IPoint)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ );
			IPoint point2 = (IPoint)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ );
			IPoint point3 = (IPoint)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ );
			IPoint point4 = (IPoint)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ );
			int num;
			for (int i = 0; i < exteriorRing.PointCount; i = num + 1)
			{
				IPoint point5 = exteriorRing.GetPoint(i);
				point.SetPostion(point5.X, point5.Y, z);
				polygon.ExteriorRing.AppendPoint(point);
				bool flag2 = i == exteriorRing.PointCount - 1;
				if (flag2)
				{
					break;
				}
				IPoint point6 = exteriorRing.GetPoint(i + 1);
				point2.SetPostion(point6.X, point6.Y, z);
				point3.SetPostion(point6.X, point6.Y, baseHeight);
				point4.SetPostion(point5.X, point5.Y, baseHeight);
				IPolygon polygon2 = (IPolygon)geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ );
				polygon2.ExteriorRing.AppendPoint(point);
				polygon2.ExteriorRing.AppendPoint(point2);
				polygon2.ExteriorRing.AppendPoint(point3);
				polygon2.ExteriorRing.AppendPoint(point4);
				polygon2.ExteriorRing.AppendPoint(point);
				list.Add(polygon2);
				num = i;
			}
			list.Add(polygon);
			result = list;
		}
		return result;
	}

    public static double CalAreaOfPolygon(this IPolygon @this, string prjWkt)
    {
        try
        {
            double area = 0;
            if (@this == null || string.IsNullOrEmpty(prjWkt)) return area;
            var newpolygon = @this.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolygon;

            if ((bool)newpolygon?.ProjectEx(prjWkt))
            {
                area = newpolygon.Area();
            }
            return area;
        }
        catch (Exception e)
        {
            SystemLog.Log(e);
            return 0;
        }

    }
}
