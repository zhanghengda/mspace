using System;
using System.Collections.Generic;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000016 RID: 22
	public class PoliceCar : BaseObject
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004436 File Offset: 0x00002636
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000443E File Offset: 0x0000263E
		private List<Policeman> PoliceMen { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004447 File Offset: 0x00002647
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000444F File Offset: 0x0000264F
		public string PoliceCarType { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004458 File Offset: 0x00002658
		[Alias("oid")]
		public string Oid
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004470 File Offset: 0x00002670
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00004488 File Offset: 0x00002688
		[Alias("车辆Id")]
		public string PoliceCarId
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004492 File Offset: 0x00002692
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000449A File Offset: 0x0000269A
		[Alias("车牌号")]
		public string PoliceCarNumber { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000044A3 File Offset: 0x000026A3
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000044AB File Offset: 0x000026AB
		[Alias("警车品牌")]
		public string PoliceCarContent { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000044B4 File Offset: 0x000026B4
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000044BC File Offset: 0x000026BC
		[Alias("所属单位")]
		public string PoliceCarUnit { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000044C5 File Offset: 0x000026C5
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000044CD File Offset: 0x000026CD
		[Alias("警车坐标")]
		public string PoliceCarCoordinate { get; set; }

		// Token: 0x0400005A RID: 90
		private string _id = string.Empty;
	}
}
