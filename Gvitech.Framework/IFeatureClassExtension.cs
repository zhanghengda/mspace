using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using System;
using System.Collections.Generic;

public static class IFeatureClassExtension
{
	public static void SetVisibleMask(this IFeatureClass @this, bool isVisible)
	{
		bool flag = @this == null;
		if (!flag)
		{
			List<IFeatureLayer> list = GviMap.FeatureLayers[@this.Guid.ToString()];
			bool flag2 = list.HasValues<IFeatureLayer>();
			if (flag2)
			{
				list.ForEach(delegate(IFeatureLayer item)
				{
					bool flag3 = item != null;
					if (flag3)
					{
						item.VisibleMask = (isVisible ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone);
					}
				});
			}
		}
	}

	public static void SetVisibleMask(this IFeatureClass @this, gviViewportMask gviViewportMask)
	{
		bool flag = @this == null;
		if (!flag)
		{
			List<IFeatureLayer> list = GviMap.FeatureLayers[@this.Guid.ToString()];
			bool flag2 = list.HasValues<IFeatureLayer>();
			if (flag2)
			{
				list.ForEach(delegate(IFeatureLayer item)
				{
					bool flag3 = item != null;
					if (flag3)
					{
						item.VisibleMask = gviViewportMask;
					}
				});
			}
		}
	}
}
