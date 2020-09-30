using System;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.HttpResult
{
	// Token: 0x0200001A RID: 26
	public class HttpResult<T> where T : class, new()
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004610 File Offset: 0x00002810
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004618 File Offset: 0x00002818
		[Alias("code")]
		public string Code { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004621 File Offset: 0x00002821
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004629 File Offset: 0x00002829
		[Alias("msg")]
		public string Msg { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004632 File Offset: 0x00002832
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000463A File Offset: 0x0000283A
		public T RequestResult { get; set; }
	}
}
