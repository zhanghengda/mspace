using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System.Collections.Generic;
using System.Windows;

namespace FireControlModule
{
    public class HiddenDangerViewModel : CheckedToolItemModel
    {
        private HiddenDangerStatics statis;
        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();

        private StatisticLegened statisticLegened;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            statis = new HiddenDangerStatics();
            this._layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            statis.ShowSupervisoryReviewStatics();
            ThreeColorItemData thData = ThreeColorItemData.GetSupervisoryReviewTestItems();
            statisticLegened = new StatisticLegened
            {
                Owner = Application.Current.MainWindow,
                DataContext = thData
            };
            statisticLegened.Show();
            ServiceManager.GetService<IShellService>(null).LeftWindow.Hide();
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
        }

        public override void OnUnchecked()
        {
            statis.CloseStatics();
            statisticLegened.Close();
            ServiceManager.GetService<IShellService>(null).LeftWindow.Show();
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
        }
    }
}