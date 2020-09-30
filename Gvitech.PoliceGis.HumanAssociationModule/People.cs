using System;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.HumanAssociationModule
{
	// Token: 0x02000006 RID: 6
	public class People : BindableBase
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000298E File Offset: 0x00000B8E
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002996 File Offset: 0x00000B96
		public string Name { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000299F File Offset: 0x00000B9F
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000029A7 File Offset: 0x00000BA7
		public string ID { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000029B0 File Offset: 0x00000BB0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000029B8 File Offset: 0x00000BB8
		public string Sex { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000029C1 File Offset: 0x00000BC1
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000029C9 File Offset: 0x00000BC9
		public string Tel { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000029D2 File Offset: 0x00000BD2
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000029DA File Offset: 0x00000BDA
		public string Photo { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000029E3 File Offset: 0x00000BE3
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000029EB File Offset: 0x00000BEB
		public string Address { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000029F4 File Offset: 0x00000BF4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000029FC File Offset: 0x00000BFC
		public string OwnerName { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A05 File Offset: 0x00000C05
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002A0D File Offset: 0x00000C0D
		public string PopulationType { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A16 File Offset: 0x00000C16
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002A1E File Offset: 0x00000C1E
		public string BirthDate { get; set; }
	}
}
