using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;

namespace Mmc.Mspace.KeyBuildingsModule.KeyBuildings
{
	// Token: 0x02000004 RID: 4
	public class KeyBuildinsViewModel : BarModel
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020EF File Offset: 0x000002EF
		public override void Initialize()
		{
            base.Initialize();
            base.Command = new KeyBuildingsCmd();
			(base.Command as BarCmd).CommandCompleted += delegate(object s, EventArgs e)
			{
				this.Layers = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(base.Content);
				this.OtherLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(base.Content);
				ServiceManager.GetService<IShellService>(null).ContentView.ClearValue(ContentControl.ContentProperty);
				ServiceManager.GetService<IShellService>(null).ContentView.Content = base.View;
			};
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		[XmlIgnore]
		public DataView DataView
		{
			get
			{
				return this.dataView;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<DataView>(ref this.dataView, value, "DataView");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002164 File Offset: 0x00000364
		public List<LayerItemModel> OtherLayers
		{
			get
			{
				return this.otherLayers;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.otherLayers, value, "OtherLayers");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002194 File Offset: 0x00000394
		public List<LayerItemModel> Layers
		{
			get
			{
				return this.layers;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.layers, value, "Layers");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AC File Offset: 0x000003AC
		public override FrameworkElement CreatedView()
		{
			return new KeyBuildinsView();
		}

		// Token: 0x04000004 RID: 4
		private DataView dataView;

		// Token: 0x04000005 RID: 5
		private List<LayerItemModel> otherLayers = new List<LayerItemModel>();

		// Token: 0x04000006 RID: 6
		private List<LayerItemModel> layers = new List<LayerItemModel>();
	}
}
