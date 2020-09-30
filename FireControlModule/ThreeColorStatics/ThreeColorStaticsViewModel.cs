using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FireControlModule
{
    public class ThreeColorStaticsViewModel : CheckedToolItemModel
    {
        private ThreeColorStatics statis;
        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();
        private HttpService _httpSrv;

        private StatisticLegened statisticLegened;

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            statis = new ThreeColorStatics();
            this._layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            _httpSrv = new HttpService();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            statis.TestShowColor();

            ThreeColorItemData thData = ThreeColorItemData.GetThreeTestItems();
            statisticLegened = new StatisticLegened
            {
                Owner = Application.Current.MainWindow,
                DataContext = thData
            };
            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}fireApplication/?pageName={1}", json1.leftViewUrl, "earlyWarning");
            webView.RequestUrl(url);
            if (webView.JsScriptInvoker.ColorVisibleEvent == null)
                webView.JsScriptInvoker.ColorVisibleEvent = new Action<string, string>(statis.SetColorVisible);
            //  statisticLegened.Show();
            //ServiceManager.GetService<IShellService>(null).LeftWindow.Hide();
            //ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
            StaticCamera.SetCamera();
        }

        public override void OnUnchecked()
        {
            statis.CloseThreeColorStatics();
            statisticLegened.Close();

            //ServiceManager.GetService<IShellService>(null).LeftWindow.Show();
            //ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;

            StaticCamera.RestoreCamera();
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            string url = json1.leftViewUrl;
            webView.RequestUrl(url);
        }
    }
}