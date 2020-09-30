using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.CoreModule.CoreModel;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.StatisticService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.StatisticsModule
{
	// Token: 0x02000003 RID: 3
	public class StatisticsViewModelEx : CoreModule.CoreModel.BaseViewModel
    {
		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public override void Initialize()
		{
			base.Initialize();
			bool flag = ServiceManager.HasService<IStatisticLayerService>();
			if (flag)
			{
				ServiceManager.GetService<IStatisticLayerService>(null).InitService();
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000215C File Offset: 0x0000035C
		public override FrameworkElement CreatedRightView()
		{
			return base.CreatedRightView();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002174 File Offset: 0x00000374
		private void RegisterSelectXQ()
		{
			bool flag = !StatisticsViewModelEx.isRegisted;
			if (flag)
			{
				GviMap.MapControl.InteractMode = gviInteractMode.gviInteractSelect;
				GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
				GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
				GviMap.AxMapControl.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClickSelect);
				StatisticsViewModelEx.isRegisted = true;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		private void UnRegisterSelectXQ()
		{
			bool flag = StatisticsViewModelEx.isRegisted;
			if (flag)
			{
				GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
				GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
				GviMap.AxMapControl.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClickSelect);
				StatisticsViewModelEx.isRegisted = false;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002224 File Offset: 0x00000424
		private void RenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
		{
			bool flag = PickResult.Type != gviObjectType.gviObjectFeatureLayer;
			if (!flag)
			{
				IFeatureLayerPickResult featureLayerPickResult = PickResult as IFeatureLayerPickResult;
				bool flag2 = featureLayerPickResult.FeatureId < 0;
				if (!flag2)
				{
					IDisplayLayer disPlayLayerByFCGuid = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCGuid(featureLayerPickResult.FeatureLayer.FeatureClassId.ToString());
					bool flag3 = disPlayLayerByFCGuid == null || disPlayLayerByFCGuid.Fc == null;
					if (!flag3)
					{
						IRowBuffer row = disPlayLayerByFCGuid.Fc.GetRow(featureLayerPickResult.FeatureId);
						string policement = StringExtension.ParseTo<string>(row.GetValue(row.Fields.IndexOf("MC")), null);
						AlarmStatisticalChart chartData = ServiceManager.GetService<IStatisticLayerService>(null).GetChartData(policement);
						base.RightView.DataContext = chartData;
						base.RightView.Visibility = Visibility.Visible;
						FdeCoreExtension.ReleaseComObject(row);
					}
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002304 File Offset: 0x00000504
		public override void OnCommandCompleted()
		{
			FrameworkElement bottomToolMenu = ServiceManager.GetService<IShellService>(null).BottomToolMenu;
			ItemsControl itemsControl = (ItemsControl)bottomToolMenu.FindName("tools");
			ObservableCollection<ToolItemModel> source = (ObservableCollection<ToolItemModel>)itemsControl.ItemsSource;
			ToolItemModel toolItemModel = source.FirstOrDefault((ToolItemModel item) => item.Content.Equals("综合统计"));
			bool flag = toolItemModel != null;
			if (flag)
			{
				toolItemModel.Visible = true;
			}
			ServiceManager.GetService<IDataBaseService>(null).ShowPOILayers(false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002380 File Offset: 0x00000580
		public override void Reset()
		{
			base.Reset();
			ServiceManager.GetService<IDataBaseService>(null).ShowPOILayers(true);
			FrameworkElement toolMenu = ServiceManager.GetService<IShellService>(null).ToolMenu;
			ItemsControl itemsControl = (ItemsControl)toolMenu.FindName("tools");
			ObservableCollection<ToolItemModel> source = (ObservableCollection<ToolItemModel>)itemsControl.ItemsSource;
			ToolItemModel toolItemModel = source.FirstOrDefault((ToolItemModel item) => item.Content.Equals("综合统计"));
			bool flag = toolItemModel != null;
			if (flag)
			{
				toolItemModel.Reset();
				toolItemModel.Visible = false;
			}
			this.UnRegisterSelectXQ();
		}

		// Token: 0x04000003 RID: 3
		private static bool isRegisted;
	}
}
