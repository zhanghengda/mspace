using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.CoreModule.CoreModel;
using Mmc.Windows.Services;

namespace Mmc.Mspace.PlanShowModule.PlanShow
{
	// Token: 0x02000003 RID: 3
	public class PlanShowViewModel : CoreModule.CoreModel.BaseViewModel
    {
		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public override void OnCommandCompleted()
		{
			base.OnCommandCompleted();
			FrameworkElement bottomToolMenu = ServiceManager.GetService<IShellService>(null).BottomToolMenu;
			ItemsControl itemsControl = (ItemsControl)bottomToolMenu.FindName("tools");
			bool flag = itemsControl == null;
			if (!flag)
			{
				ObservableCollection<ToolItemModel> observableCollection = (ObservableCollection<ToolItemModel>)itemsControl.ItemsSource;
				bool flag2 = observableCollection != null;
				if (flag2)
				{
					foreach (ToolItemModel toolItemModel in observableCollection)
					{
						bool flag3 = toolItemModel.GroupName == "预案演示";
						if (flag3)
						{
							((CheckedToolItemModel)toolItemModel).IsChecked = true;
						}
					}
				}
			}
		}
	}
}
