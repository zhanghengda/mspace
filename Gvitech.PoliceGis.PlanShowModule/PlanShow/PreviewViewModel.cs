using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.PlanShowService;
using Mmc.Mspace.Services.PlanShowService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.PlanShowModule.PlanShow
{
	// Token: 0x02000005 RID: 5
	public class PreviewViewModel : CheckedToolItemModel
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000219F File Offset: 0x0000039F
		public override void Initialize()
		{
			base.Initialize();
			base.ViewType = (ViewType)1;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B1 File Offset: 0x000003B1
		public override void Reset()
		{
			base.Reset();
			base.IsChecked = false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C4 File Offset: 0x000003C4
		public override void OnChecked()
		{
			base.OnChecked();
			ServiceManager.RegisterService<IPlanShowService>(new ProvideService(PlanShowService.GetDefault));
			ServiceManager.GetService<IPlanShowService>(null).LoadData();
			List<Preview> planShow = ServiceManager.GetService<IPlanShowService>(null).GetPlanShow();
			ObservableCollection<PreviewWrapper> observableCollection = new ObservableCollection<PreviewWrapper>();
			foreach (Preview preview in planShow)
			{
				observableCollection.Add(new PreviewWrapper(preview));
			}
			this.Parameter = observableCollection;
			//ServiceManager.GetService<IShellService>(null).BottomView.Content = base.BottomView;
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Collapsed;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Collapsed;
        }

		// Token: 0x0600000B RID: 11 RVA: 0x00002274 File Offset: 0x00000474
		public override void OnUnchecked()
		{
			base.OnUnchecked();
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
            bool flag = ServiceManager.HasService<IPlanShowService>();
			if (flag)
			{
				ServiceManager.GetService<IPlanShowService>(null).RemovePlanShow();
			}
			//ContentControl bottomView = ServiceManager.GetService<IShellService>(null).BottomView;
			//bool flag2 = bottomView.Content != null;
			//if (flag2)
			//{
			//	bottomView.ClearValue(ContentControl.ContentProperty);
			//}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C4 File Offset: 0x000004C4
		public override FrameworkElement CreatedBottomView()
		{
			return new PreviewView();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022DC File Offset: 0x000004DC
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022F4 File Offset: 0x000004F4
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

		// Token: 0x04000003 RID: 3
		private object parameter;
	}
}
