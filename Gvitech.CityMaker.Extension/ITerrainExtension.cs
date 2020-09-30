using Gvitech.CityMaker.RenderControl;
using System;

public static class ITerrainExtension
{
	public static void FlyTo(this ITerrain @this)
	{
		bool flag = @this == null;
		if (!flag)
		{
			@this.FlyTo(gviTerrainActionCode.gviFlyToTerrain);
		}
	}

	public static void SetVisibleMask(this ITerrain @this, bool isVisible, gviViewportMask gviViewportMask = gviViewportMask.gviViewAllNormalView)
	{
		bool flag = @this == null;
		if (!flag)
		{
			@this.VisibleMask = (isVisible ? gviViewportMask : gviViewportMask.gviViewNone);
		}
	}
}
