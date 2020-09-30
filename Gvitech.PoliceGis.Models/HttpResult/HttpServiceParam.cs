using System;
using System.Xml.Serialization;

namespace Mmc.Mspace.Models.HttpResult
{
	// Token: 0x0200001C RID: 28
	public class HttpServiceParam
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000047CD File Offset: 0x000029CD
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000047D5 File Offset: 0x000029D5
		[XmlAttribute]
		public string Name { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000047DE File Offset: 0x000029DE
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000047E6 File Offset: 0x000029E6
		[XmlAttribute]
		public string Url { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000047EF File Offset: 0x000029EF
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000047F7 File Offset: 0x000029F7
		[XmlAttribute]
		public string JsonFile { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004800 File Offset: 0x00002A00
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004808 File Offset: 0x00002A08
		[XmlAttribute]
		public bool Unobstructed { get; set; }
	}
}
