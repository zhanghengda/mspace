using System;
using System.Collections.ObjectModel;

namespace Mmc.Mspace.Common.Models
{
	// Token: 0x0200000B RID: 11
	public class ToolItemsModel : ToolItemModel
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002923 File Offset: 0x00000B23
		public ToolItemsModel()
		{
			this._items = new ObservableCollection<ToolItemModel>();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002938 File Offset: 0x00000B38
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002950 File Offset: 0x00000B50
		public ObservableCollection<ToolItemModel> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this._items, value, "Items");
			}
		}

		// Token: 0x04000028 RID: 40
		private ObservableCollection<ToolItemModel> _items;
	}
}
