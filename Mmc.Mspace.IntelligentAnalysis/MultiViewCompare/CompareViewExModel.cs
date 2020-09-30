using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IntelligentAnalysisModule
{
    public class CompareViewExModel : CheckedToolItemModel
    {
        bool isDoubleScreen = false;
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
        }
        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                ShowView();
                Messenger.Messengers.Notify(CommonContract.MessengerKey.Splitscreen.ToString(),true);

                //var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
                //webView.InvokeScript("openCompareView", true);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                if (this.isDoubleScreen)
                {
                    HideView();
                }
                Messenger.Messengers.Notify(CommonContract.MessengerKey.Splitscreen.ToString(), false);
                //var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
                //webView.InvokeScript("openCompareView", false);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        private void ShowView()
        {
            this.isDoubleScreen = true;
            ServiceManager.GetService<IShellService>(null).IsCompareView = true;
            //其他组建隐藏
            //var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            //var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;

            //mapView.UpdateLayout();

        }

        private void HideView()
        {
            this.isDoubleScreen = false;
            //var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            //var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            //mapView.Width = shellView.Width;
            //mapView.Left = shellView.Left;
            //mapView.UpdateLayout();
            //退出恢复单屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
            ServiceManager.GetService<IShellService>(null).IsCompareView = false;
        }
    }
}
