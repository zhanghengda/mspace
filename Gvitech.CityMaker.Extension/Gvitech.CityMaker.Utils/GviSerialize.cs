using Gvitech.CityMaker.FdeGeometry;
using System;

namespace Gvitech.CityMaker.Utils
{
	public static class GviSerialize
	{
		public static SerializePoint IPointToSerializePoint(IPoint gviPoint)
		{
			SerializePoint result = default(SerializePoint);
			bool flag = gviPoint != null;
			if (flag)
			{
				result.X = gviPoint.X;
				result.Y = gviPoint.Y;
				result.Z = gviPoint.Z;
			}
			return result;
		}

		public static SerializePoint CoordiateToSerializePoint(double x, double y, double z)
		{
			return new SerializePoint
			{
				X = x,
				Y = y,
				Z = z
			};
		}

		public static IPoint SerializePointToIPoint(SerializePoint sPoint, IGeometryFactory geoFactory)
		{
			bool flag = geoFactory == null;
			IPoint result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IPoint point = geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
				bool flag2 = point == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					point.X = sPoint.X;
					point.Y = sPoint.Y;
					point.Z = sPoint.Z;
					result = point;
				}
			}
			return result;
		}
	}
}
