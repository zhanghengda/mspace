using System;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000015 RID: 21
	public class Policeman : BaseObject
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000434C File Offset: 0x0000254C
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004354 File Offset: 0x00002554
		[Alias("警员类型")]
		public string PolicemanType { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004360 File Offset: 0x00002560
		[Alias("oid")]
		public string Oid
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004378 File Offset: 0x00002578
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004390 File Offset: 0x00002590
		[Alias("警员Id")]
		public string POLICE
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000439A File Offset: 0x0000259A
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000043A2 File Offset: 0x000025A2
		[Alias("警员编号")]
		public string PolicemanNumber { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000043AB File Offset: 0x000025AB
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000043B3 File Offset: 0x000025B3
		[Alias("警员名称")]
		public string PolicemanName { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000043BC File Offset: 0x000025BC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000043C4 File Offset: 0x000025C4
		[Alias("所属单位")]
		public string PolicemanUnit { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000043CD File Offset: 0x000025CD
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000043D5 File Offset: 0x000025D5
		[Alias("详细信息")]
		public string PolicemanDescription { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000043DE File Offset: 0x000025DE
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000043E6 File Offset: 0x000025E6
		[Alias(" 身份证")]
		public string PolicemanIdentification { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000043EF File Offset: 0x000025EF
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000043F7 File Offset: 0x000025F7
		[Alias("对讲机Id")]
		public string PolicemanDjId { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004400 File Offset: 0x00002600
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004408 File Offset: 0x00002608
		[Alias("警员设备")]
		public string PolicemanEquipment { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004411 File Offset: 0x00002611
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004419 File Offset: 0x00002619
		[Alias("警员坐标")]
		public string PolicemanCoordinate { get; set; }

		// Token: 0x04000050 RID: 80
		private string _id = string.Empty;
	}
}
