using System;
using System.Xml.Serialization;

namespace Mmc.Mspace.Common.Models
{
	// Token: 0x02000007 RID: 7
	public class BarModel : ToolItemModel
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002154 File Offset: 0x00000354
		[XmlAttribute]
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
			set
			{
				this._rowIndex = value;
				base.SetAndNotifyPropertyChanged<int>(ref this._rowIndex, value, "RowIndex");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002174 File Offset: 0x00000374
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000218C File Offset: 0x0000038C
		[XmlAttribute]
		public int ColumnIndex
		{
			get
			{
				return this._columnIndex;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<int>(ref this._columnIndex, value, "ColumnIndex");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000021A4 File Offset: 0x000003A4
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000021BC File Offset: 0x000003BC
		[XmlAttribute]
		public int ColumnSpan
		{
			get
			{
				return this._columnSpan;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<int>(ref this._columnSpan, value, "ColumnSpan");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000021EC File Offset: 0x000003EC
		[XmlAttribute]
		public int RowSpan
		{
			get
			{
				return this._rowSpan;
			}
			set
			{
				this._rowSpan = value;
				base.SetAndNotifyPropertyChanged<int>(ref this._rowSpan, value, "RowSpan");
			}
		}

		// Token: 0x0400000C RID: 12
		private int _rowIndex;

		// Token: 0x0400000D RID: 13
		private int _columnIndex;

		// Token: 0x0400000E RID: 14
		private int _columnSpan = 1;

		// Token: 0x0400000F RID: 15
		private int _rowSpan = 1;
	}
}
