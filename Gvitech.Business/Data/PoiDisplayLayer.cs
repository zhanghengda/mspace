using System;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;

namespace Mmc.Business.Data
{
	// Token: 0x0200000E RID: 14
	public class PoiDisplayLayer
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000032CD File Offset: 0x000014CD
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000032D5 File Offset: 0x000014D5
		public IFeatureClass Fc { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000032DE File Offset: 0x000014DE
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000032E6 File Offset: 0x000014E6
		public IFeatureLayer FeatureLayer { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000032F0 File Offset: 0x000014F0
		public string FcName
		{
			get
			{
				return (this.Fc != null) ? this.Fc.Name : string.Empty;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000331C File Offset: 0x0000151C
		public string FcAliasName
		{
			get
			{
				return (this.Fc != null) ? ((!string.IsNullOrEmpty(this.Fc.Alias)) ? this.Fc.Alias : this.Fc.Name) : string.Empty;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003368 File Offset: 0x00001568
		public string FcGuid
		{
			get
			{
				return (this.Fc != null) ? this.Fc.Guid.ToString() : string.Empty;
			}
		}
	}
}
