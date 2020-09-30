using Gvitech.CityMaker.RenderControl;
using System;

public static class gviViewportMaskExtension
{
	public static bool GetIsVisible(this gviViewportMask @this)
	{
		return @this > gviViewportMask.gviViewNone;
	}
}
