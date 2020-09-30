using System;
using System.Collections.Generic;
using System.Windows;
//using Gvitech.AppPd.UrbanPlan.DAL;
using Mmc.Mspace.NavigationModule.Core;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;

namespace Mmc.Mspace.NavigationModule.Locale
{
	// Token: 0x02000008 RID: 8
	public class LocaleViewModel : ScenariosViewModelBase
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002510 File Offset: 0x00000710
		public override void Initialize()
		{
			base.Initialize();
			base.ViewType = (Mmc.Mspace.Common.Models.ViewType)1;
			//List<LocationScene> locationScenes = ServiceManager.GetService<IDataBaseService>(null).GetLocationScenes();
			//List<LocationSceneWrapper> list = new List<LocationSceneWrapper>();
			//bool flag = !IEnumerableExtension.HasValues<LocationScene>(locationScenes);
			//if (!flag)
			//{
			//	foreach (LocationScene locationScene in locationScenes)
			//	{
			//		list.Add(new LocationSceneWrapper
			//		{
			//			LocationScene = locationScene
			//		});
			//	}
			//	this.Parameter = list;
			//}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025B0 File Offset: 0x000007B0
		public override FrameworkElement CreatedBottomView()
		{
			return new LocaleView();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025C8 File Offset: 0x000007C8
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000025E0 File Offset: 0x000007E0
		public object Parameter
		{
			get
			{
				return this.parameter;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<object>(ref this.parameter, value, "Parameter");
			}
		}

		// Token: 0x0400000F RID: 15
		private object parameter;
	}
}
