using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.Human
{
	// Token: 0x02000019 RID: 25
	public class HttpPopulationInfos
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000045E9 File Offset: 0x000027E9
		public HttpPopulationInfos()
		{
			this.InfoRKXXs = new List<PopulationInfo>();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000045FF File Offset: 0x000027FF
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00004607 File Offset: 0x00002807
		[Alias("infoRKXXs")]
		public List<PopulationInfo> InfoRKXXs { get; set; }
	}
}
