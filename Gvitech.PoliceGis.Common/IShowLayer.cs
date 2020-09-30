using System;
using System.Data;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.Mspace.Common
{
	// Token: 0x02000002 RID: 2
	public interface IShowLayer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string AliasName { get; }

		// Token: 0x06000002 RID: 2
		bool FlyToFeature(string id, ICamera camera);

		// Token: 0x06000003 RID: 3
		DataTable FuzzySearch(string key, string fields = null, IGeometry geo = null);

		// Token: 0x06000004 RID: 4
		bool HighLightFeature(string id, IFeatureManager fm = null, uint color = 4294901760u);

		// Token: 0x06000005 RID: 5
		bool HighLightFeatures(string[] ids, IFeatureManager fm = null, uint color = 4294901760u);

		// Token: 0x06000006 RID: 6
		bool UnHighLightFeatures(string[] ids, IFeatureManager fm = null);

		// Token: 0x06000007 RID: 7
		bool UnHighLightFeature(string id, IFeatureManager fm = null);

		// Token: 0x06000008 RID: 8
		bool UnHighLightFeatureClass(IFeatureManager fm = null);

		// Token: 0x06000009 RID: 9
		bool SetVisibleMask(bool visible, gviViewportMask vmask = gviViewportMask.gviViewAllNormalView);

		// Token: 0x0600000A RID: 10
		bool ContainObject(string id);

		// Token: 0x0600000B RID: 11
		DataTable GetInfoTable(string id);

        string GetFid();
	}
}
