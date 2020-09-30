using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x0200000F RID: 15
	public class HttpPoliceInfo
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000038AB File Offset: 0x00001AAB
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000038B3 File Offset: 0x00001AB3
		[Alias("infoSWRYResps")]
		public List<PoliceManInfo> InfoSWRYResps { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000038BC File Offset: 0x00001ABC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000038C4 File Offset: 0x00001AC4
		[Alias("infoSWCLResps")]
		public List<PoliceCarInfo> InfoSWCLResps { get; set; }

		// Token: 0x060000C2 RID: 194 RVA: 0x000038CD File Offset: 0x00001ACD
		public HttpPoliceInfo()
		{
			this.InfoSWRYResps = new List<PoliceManInfo>();
			this.InfoSWCLResps = new List<PoliceCarInfo>();
		}
	}
}
