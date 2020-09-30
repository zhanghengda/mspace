using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class ScreenCompareVModel : CheckedToolItemModel
    {
        private ScreenHintVModel activeVModel;
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
        }
        public override void OnChecked()
        {
            base.OnChecked();
            //开启双屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
            GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;
            ServiceManager.GetService<IShellService>(null).IsCompareView = true;
            Messenger.Messengers.Notify("LeftMenuEnum", CommonContract.LeftMenuEnum.LeftManagementView);

            Messenger.Messengers.Notify("BottomMenuEnum", IsChecked);
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.SaveStatus.ToString());
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.AllHide.ToString());

            RegInsDataRenderManager.Instance.SaveRenderLayersStatus();
            RegInsDataRenderManager.Instance.HideRenderLayer();

            ShowActiveHint();
            GviMap.AxMapControl.RcLButtonUp -= AxMapControl_RcLButtonUp_ForActiveScreen;
            GviMap.AxMapControl.RcLButtonUp += AxMapControl_RcLButtonUp_ForActiveScreen;
        }

        private bool AxMapControl_RcLButtonUp_ForActiveScreen(uint Flags, int X, int Y)
        {
            ShowActiveHint();
            return false;
        }


        private void ShowActiveHint()
        {
            if (activeVModel == null)
                activeVModel = new ScreenHintVModel();

            var viewNum = GviMap.MapControl.Viewport.ActiveView;
            if (viewNum == 0)
            {
                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                double width = mapView.Width / 2;
                double top = mapView.Top + 48;
                double left = mapView.Left;

                activeVModel.ShowViewOfActiveScreen(width, top, left);
            }
            else if (viewNum == 1)
            {
                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                double width = mapView.Width / 2;
                double top = mapView.Top + 48;
                double left = width + mapView.Left;

                activeVModel.ShowViewOfActiveScreen(width, top, left);
            }
        }

        private void HideActiveHint()
        {
            if (activeVModel == null)
                activeVModel = new ScreenHintVModel();
            activeVModel.HideView();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            //退出恢复单屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
            ServiceManager.GetService<IShellService>(null).IsCompareView = false;
            Messenger.Messengers.Notify("LeftMenuEnum", CommonContract.LeftMenuEnum.RegularInspectionView);

            Messenger.Messengers.Notify("BottomMenuEnum", IsChecked);
            Messenger.Messengers.Notify(CommonContract.RenderLayerStatus.RecoveryRenderStatus.ToString());
            GviMap.AxMapControl.RcLButtonUp -= AxMapControl_RcLButtonUp_ForActiveScreen;

            RegInsDataRenderManager.Instance.RecoverRenderLayer();
            RegInsDataRenderManager.Instance.CloseHintView();
            HideActiveHint();
        }
    }
}
