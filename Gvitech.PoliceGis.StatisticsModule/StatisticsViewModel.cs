using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using Mmc.Mspace.CoreModule.CoreModel;
using Mmc.Mspace.Services.StatisticService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.StatisticsModule
{
	// Token: 0x02000002 RID: 2
	public class StatisticsViewModel : BaseViewModel
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<string>(ref this.title, value, "Title");
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		[XmlIgnore]
		public List<StatisticalChartItem> DataSource
		{
			get
			{
				return this.datasource;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<StatisticalChartItem>>(ref this.datasource, value, "DataSource");
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020AE File Offset: 0x000002AE
		public override void Initialize()
		{
			base.Initialize();
			this.GetData();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		private void GetData()
		{
			bool flag = !ServiceManager.HasService<IStatisticLayerService>();
			if (flag)
			{
				ServiceManager.RegisterService<IStatisticLayerService>(new ProvideService(StatisticLayerService.GetDefault));
				ServiceManager.GetService<IStatisticLayerService>(null).InitService();
			}
			this.DataSource = ServiceManager.GetService<IStatisticLayerService>(null).GetAlarmCharData();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000210C File Offset: 0x0000030C
		public override FrameworkElement CreatedRightView()
		{
			return base.CreatedRightView();
		}

		// Token: 0x04000001 RID: 1
		private List<StatisticalChartItem> datasource;

		// Token: 0x04000002 RID: 2
		private string title;
	}
}
