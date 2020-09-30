using System;

namespace Mmc.Mspace.Models.Video
{
	// Token: 0x02000004 RID: 4
	public class VideoParam
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002108 File Offset: 0x00000308
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002110 File Offset: 0x00000310
		public string Sysname { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002121 File Offset: 0x00000321
		public string Citizenid { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002132 File Offset: 0x00000332
		public string Password { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000213B File Offset: 0x0000033B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002143 File Offset: 0x00000343
		public string DeviceID { get; set; }
	}
}
