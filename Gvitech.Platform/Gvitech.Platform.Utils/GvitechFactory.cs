using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using System;

namespace Mmc.Platform.Utils
{
	public class GvitechFactory
	{
		public static ISpatialFilter CreateSpatialFilter(IGeometry geometry, gviSpatialRel spatialRel = gviSpatialRel.gviSpatialRelIntersects, string geometryField = "footprint")
		{
			return new SpatialFilter
			{
				Geometry = geometry,
				SpatialRel = spatialRel,
				GeometryField = geometryField
			};
		}
	}
}
