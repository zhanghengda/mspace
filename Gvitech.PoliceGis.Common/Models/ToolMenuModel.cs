using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.Common.Models
{
	// Token: 0x0200000C RID: 12
	[XmlInclude(typeof(ToolItemModel))]
	[XmlInclude(typeof(ToolItemsModel))]
	[XmlInclude(typeof(LayerItemModel))]
	[XmlInclude(typeof(CheckedToolItemModel))]
	public class ToolMenuModel : BindableBase
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002966 File Offset: 0x00000B66
		public ToolMenuModel()
		{
			this._toolMenuItems = new ObservableCollection<ToolItemModel>();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000299D File Offset: 0x00000B9D
		public ObservableCollection<ToolItemModel> ToolMenuItems
		{
			get
			{
				return this._toolMenuItems ?? new ObservableCollection<ToolItemModel>();
			}
			set
			{
				base.SetAndNotifyPropertyChanged<ObservableCollection<ToolItemModel>>(ref this._toolMenuItems, value, "ToolMenuItems");
			}
		}

		// Token: 0x04000029 RID: 41
		private ObservableCollection<ToolItemModel> _toolMenuItems;
	}
}
