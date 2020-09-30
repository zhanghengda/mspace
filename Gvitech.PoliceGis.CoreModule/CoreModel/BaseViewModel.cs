using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.CoreModule.PoliceCommon;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System.Threading.Tasks;

namespace Mmc.Mspace.CoreModule.CoreModel
{
	// Token: 0x02000004 RID: 4
	public class BaseViewModel : BarModel
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020B8 File Offset: 0x000002B8
		public override void Initialize()
		{
            base.Initialize();
            base.Command = new BaseBarCmd();
			(base.Command as BaseBarCmd).CommandCompleted += delegate(object s, EventArgs e)
			{
				this.ToolsMenuChanged();
				this.Layers = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(base.Content);
				this.OtherLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(base.Content);
				//ServiceManager.GetService<IShellService>(null).BottomView.ClearValue(ContentControl.ContentProperty);
				//ServiceManager.GetService<IShellService>(null).BottomView.Content = base.BottomView;
                //ServiceManager.GetService<IShellService>(null).RightView.ClearValue(ContentControl.ContentProperty);
                //ServiceManager.GetService<IShellService>(null).RightView.Content = base.RightView;
                ServiceManager.GetService<IShellService>(null).ContentView.ClearValue(ContentControl.ContentProperty);
				ServiceManager.GetService<IShellService>(null).ContentView.Content = base.View;
                ServiceManager.GetService<IShellService>(null).ShowMenuView(base.GroupName);
                this.OnCommandCompleted();
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000002E4
		public override FrameworkElement CreatedView()
		{
			return new PoliceCommonView();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		[XmlIgnore]
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<DataView>(ref this._dataView, value, "DataView");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002144 File Offset: 0x00000344
		public List<LayerItemModel> OtherLayers
		{
			get
			{
				return this._otherLayers;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this._otherLayers, value, "OtherLayers");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000215C File Offset: 0x0000035C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002174 File Offset: 0x00000374
		public List<LayerItemModel> Layers
		{
			get
			{
				return this._layers;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this._layers, value, "Layers");
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218A File Offset: 0x0000038A
		public virtual void OnCommandCompleted()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		public virtual void ToolsMenuChanged()
		{
            if (CheckedToolItemModel.AllCheckedCmds.Count > 0)
                foreach (var item in CheckedToolItemModel.AllCheckedCmds)
                {
                    if (string.IsNullOrEmpty(item.IsConflictFlag))
                    {
                        item.IsChecked = false;
                    }
                }
                    
            bool flag = ServiceManager.GetService<IShellService>(null).BottomToolMenu == null || string.IsNullOrEmpty(base.GroupName);
			if (!flag)
			{
				FrameworkElement bottomToolMenu = ServiceManager.GetService<IShellService>(null).RightToolMenu;
				ItemsControl itemsControl = (ItemsControl)bottomToolMenu.FindName("tools");
				bool flag2 = itemsControl == null;
				if (!flag2)
				{
                    //显示组内
                    ObservableCollection<ToolItemModel> source = (ObservableCollection<ToolItemModel>)itemsControl.ItemsSource;
					foreach (ToolItemModel toolItemModel in from toolItem in source
					where !string.IsNullOrEmpty(toolItem.GroupName) && toolItem.GroupName.Contains(base.GroupName)
					select toolItem)
					{
						toolItemModel.Visible = true;
					}

                    ////隐藏组外按钮
                    foreach (ToolItemModel toolItemModel in from toolItem in source
                                                            where !string.IsNullOrEmpty(toolItem.GroupName) && !toolItem.GroupName.Contains(base.GroupName)
                                                            select toolItem)
                    {
                        toolItemModel.Visible = false;
                    }
                }
			}
		}

		// Token: 0x04000002 RID: 2
		private DataView _dataView;

		// Token: 0x04000003 RID: 3
		private List<LayerItemModel> _otherLayers = new List<LayerItemModel>();

		// Token: 0x04000004 RID: 4
		private List<LayerItemModel> _layers = new List<LayerItemModel>();
	}
}
