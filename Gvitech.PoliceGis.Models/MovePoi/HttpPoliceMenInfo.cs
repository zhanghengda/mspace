using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000011 RID: 17
	public class HttpPoliceMenInfo
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003955 File Offset: 0x00001B55
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000395D File Offset: 0x00001B5D
		[Alias("infoJYSJs")]
		public List<PoliceManInfo> InfoJYSJs { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00003966 File Offset: 0x00001B66
		public HttpPoliceMenInfo()
		{
			this.InfoJYSJs = new List<PoliceManInfo>();
		}
	}
}
