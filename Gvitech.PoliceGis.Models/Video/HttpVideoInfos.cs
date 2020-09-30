using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.Video
{
	// Token: 0x02000003 RID: 3
	public class HttpVideoInfos
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000020E1 File Offset: 0x000002E1
		public HttpVideoInfos()
		{
			this.InfoSPJKs = new List<VideoInfo>();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020FF File Offset: 0x000002FF
		[Alias("infoSPJKs")]
		public List<VideoInfo> InfoSPJKs { get; set; }
	}
}
