using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Models.Case
{
	// Token: 0x02000020 RID: 32
	public class SubjectCaseInfo
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004915 File Offset: 0x00002B15
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000491D File Offset: 0x00002B1D
		public string FirstFieldValue { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004926 File Offset: 0x00002B26
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000492E File Offset: 0x00002B2E
		public int FirstCount { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004937 File Offset: 0x00002B37
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000493F File Offset: 0x00002B3F
		public List<AreaSubjectCaseInfo> PivotList { get; set; }
	}
}
