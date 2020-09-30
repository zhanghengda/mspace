using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.Case
{
	// Token: 0x0200001E RID: 30
	public class HttpCaseInfos
	{
		// Token: 0x0600016C RID: 364 RVA: 0x000048CC File Offset: 0x00002ACC
		public HttpCaseInfos()
		{
			this.InfoAJLBs = new List<CaseInfo>();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000048E2 File Offset: 0x00002AE2
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000048EA File Offset: 0x00002AEA
		[Alias("infoAJLBs")]
		public List<CaseInfo> InfoAJLBs { get; set; }
	}
}
