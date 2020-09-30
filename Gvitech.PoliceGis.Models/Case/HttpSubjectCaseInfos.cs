using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.Case
{
	// Token: 0x02000021 RID: 33
	public class HttpSubjectCaseInfos
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00004948 File Offset: 0x00002B48
		public HttpSubjectCaseInfos()
		{
			this.InfoZTAJs = new List<SubjectCaseInfo>();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000495E File Offset: 0x00002B5E
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00004966 File Offset: 0x00002B66
		[Alias("infoZTAJs")]
		public List<SubjectCaseInfo> InfoZTAJs { get; set; }
	}
}
