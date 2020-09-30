using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System.Windows;

namespace FireControlModule.HazardSource
{
    /// <summary>
    /// 重大危险源
    /// </summary>
    public class HazardSourceViewModel : CheckedToolItemModel
    {
        private IDisplayLayer _gridLayer;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            SetLayerVisible(true);
            StaticCamera.SetCamera(height:9500);
            if (_gridLayer == null)
            {
                var layers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
                layers.ForEach(p => { if (p.AliasName == "社区范围") _gridLayer = p; });
            }
            _gridLayer.SetVisibleMask(true);

            //ServiceManager.GetService<IShellService>().HideAllView();
            //ServiceManager.GetService<IShellService>().ShowBottomRight(5);

            //System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    System.Threading.Thread.Sleep(3000);
            //    ServiceManager.GetService<IShellService>().RightToolMenu.Visibility = System.Windows.Visibility.Visible;
            //    ServiceManager.GetService<IShellService>().BottomToolMenu.Visibility = System.Windows.Visibility.Visible;
            //}), DispatcherPriority.Background, new object[0]);
        }

        private static void SetLayerVisible(bool isVisilbe)
        {
            var shpLayers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
            var layerName = "重大危险源";
            shpLayers.ForEach(p =>
            {
                if (p.Fc.Name == layerName || p.Fc.Alias == layerName)
                    p.SetVisibleMask(isVisilbe);
            });
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            SetLayerVisible(false);
            if (_gridLayer != null)
                _gridLayer.SetVisibleMask(false);
            StaticCamera.RestoreCamera();
            ServiceManager.GetService<IShellService>(null).ShowAllView();
            //ServiceManager.GetService<IShellService>(null).LeftWindow.Show();
            //ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
        }
    }
}