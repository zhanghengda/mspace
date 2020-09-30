using System;
using System.Xml.Serialization;
using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Models.MovePoi
{
	// Token: 0x02000017 RID: 23
	public class RecevieMessage
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000044EA File Offset: 0x000026EA
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000044F2 File Offset: 0x000026F2
		[XmlAttribute]
		[Alias("信息类型")]
		public int msgType { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000044FB File Offset: 0x000026FB
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004503 File Offset: 0x00002703
		[XmlAttribute]
		[Alias("设备编号")]
		public string deviceId { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000450C File Offset: 0x0000270C
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004514 File Offset: 0x00002714
		[XmlAttribute]
		[Alias("经度")]
		public float longti { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000451D File Offset: 0x0000271D
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004525 File Offset: 0x00002725
		[XmlAttribute]
		[Alias("维度")]
		public float lati { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000452E File Offset: 0x0000272E
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004536 File Offset: 0x00002736
		[XmlAttribute]
		[Alias("速度")]
		public float speed { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000453F File Offset: 0x0000273F
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00004547 File Offset: 0x00002747
		[XmlAttribute]
		[Alias("方向")]
		public int direction { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004550 File Offset: 0x00002750
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004558 File Offset: 0x00002758
		[XmlAttribute]
		[Alias("日期")]
		public string dateTime { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004561 File Offset: 0x00002761
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004569 File Offset: 0x00002769
		[XmlAttribute]
		[Alias("设备类型")]
		public string deviceType { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004572 File Offset: 0x00002772
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000457A File Offset: 0x0000277A
		[XmlAttribute]
		[Alias("呼号")]
		public string PDT_NO { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004583 File Offset: 0x00002783
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000458B File Offset: 0x0000278B
		[XmlAttribute]
		[Alias("警员编号")]
		public string Police_ID { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000459C File Offset: 0x0000279C
		[XmlAttribute]
		[Alias("单位id")]
		public string unit_id { get; set; }
	}
}
