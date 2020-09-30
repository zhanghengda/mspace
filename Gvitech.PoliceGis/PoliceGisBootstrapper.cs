using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.FieldsFilterService;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.LayerGroupService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.Services.MovePoiService;
using Mmc.Mspace.Services.NetRouteAnalysisService;
using Mmc.Mspace.Services.ShowCaptureObjectService;
using Mmc.Mspace.Services.StatisticService;
using MMC.MSpace.Views;
using Mmc.Windows.Services;
using Mmc.Wpf.Toolkit.Utils;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Languagepack.LanguageManager;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Utils;

namespace MMC.MSpace
{
    public class PoliceGisBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return UnityContainerExtensions.Resolve<MapHost>(base.Container, new ResolverOverride[0]);
        }

        protected override void InitializeShell()
        {

            WPFLoginView view = new WPFLoginView();
            //LoginView view = new LoginView();
            if (!(bool)view.ShowDialog())
            {
                Environment.Exit(0);
                return;
            }


            WebBrowserVersionEmulation();
            DXSplashScreen.Show<SplashWindow>();
            DXSplashScreen.Progress(0.0);
            string text = System.Windows.Forms.Application.LocalUserAppDataPath + "\\logs";
            SystemLog.InitSysLog(text);
            PoliceGisBootstrapper.RegisterServices();
            SystemLog.Log("启动加载界面");
            double screenInchSize = ScreenHelper.GetScreenInchSize();
            string[] files = Directory.GetFiles("Config\\Screen\\", "*inch.xaml");
            Dictionary<double, string> dictionary = new Dictionary<double, string>();
            foreach (string text2 in files)
            {
                FileInfo fileInfo = new FileInfo(text2);
                double num = double.Parse(fileInfo.Name.Replace("inch.xaml", ""));
                double num2 = Math.Abs(screenInchSize - num);
                bool flag = dictionary.Count != 0;
                if (flag)
                {
                    bool flag2 = num2 < dictionary.FirstOrDefault<KeyValuePair<double, string>>().Key;
                    if (flag2)
                    {
                        dictionary.Clear();
                        dictionary.Add(num2, text2);
                    }
                }
                else
                {
                    dictionary.Add(num2, text2);
                }
            }
            SystemLog.Log("初始化wpf样式");
            FileStream stream = new FileStream(dictionary.FirstOrDefault<KeyValuePair<double, string>>().Value, FileMode.Open, FileAccess.Read);
            ResourceDictionary item = XamlReader.Load(stream) as ResourceDictionary;
            ResourceDictionary resourceDictionary = (ResourceDictionary)System.Windows.Application.LoadComponent(new Uri("/Mmc.Mspace.Theme;component/Styles.xaml", UriKind.Relative));
            resourceDictionary.MergedDictionaries.Insert(0, item);
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            SystemLog.Log("弹出shell窗口");
            System.Windows.Application.Current.MainWindow = (Window)base.Shell;
            System.Windows.Application.Current.MainWindow.Show();

            UserdeadLineMessage();
            var data = HttpServiceHelper.Instance.GetRequest("/api/version/index");
            if (!string.IsNullOrWhiteSpace(data))
            {
                dynamic model = JsonUtil.DeserializeFromString<dynamic>(data);

                if (model.version != CacheData.CurrentVersion)
                {
                    var isIgnoreVersion = CacheData.GetConfigAppSettingsValue("IsIgnoreVersion");
                    var version = CacheData.GetConfigAppSettingsValue("IgnoreVersion");

                    if (string.IsNullOrWhiteSpace(isIgnoreVersion))
                    {
                        ShowVersionUpdate(model.version.ToString());
                    }
                    else
                    {
                        if (isIgnoreVersion == "False")
                        {
                            ShowVersionUpdate(model.version.ToString());
                        }
                        else
                        {
                            if (version != model.version.ToString())
                            {
                                ShowVersionUpdate(model.version.ToString());
                            }
                        }
                    }
                }
            }

        }

        private void UserdeadLineMessage()
        {
            string deadLineDay = CacheData.UserInfo.life_day;
            if (Convert.ToInt32(deadLineDay) != -1&& Convert.ToInt32(deadLineDay)<=10)
            {
               Messages.ShowMessage( "  有效期还剩" + CacheData.UserInfo.life_day + "天");
            }           
        }
        private void ShowVersionUpdate(string version)
        {
            MessageBoxView msg = new MessageBoxView
            {
                Title = "版本更新提醒",
                Message = $"检测到MSpace更新版本,当前最新版本为:{version},可前往配置页面进行更新!",
                BtnCancelContent = "忽略此版本",
                Owner = Application.Current.MainWindow
            };
            msg.CancelAction = () =>
            {
                CacheData.SaveConfig("IgnoreVersion", version);
                CacheData.SaveConfig("IsIgnoreVersion", "True");
            };
            msg.Show();
        }

        private static void RegisterServices()
        {
            SystemLog.Log("开始注册服务...", 0);
            SystemLog.Log("注册服务<MaphostService>...", 0);
            ServiceManager.RegisterService<IMaphostService>(new ProvideService(MaphostService.GetDefault));
            SystemLog.Log("注册服务<LocalWsConfigService>...", 0);
            ServiceManager.RegisterService<ILocalWsConfigService>(new ProvideService(LocalWsConfigService.GetDefault));
            SystemLog.Log("注册服务<InspectionService>...", 0);
            ServiceManager.RegisterService<IInspectionService>(new ProvideService(InspectionService.GetDefault));
            //SystemLog.Log("注册服务<RouteAnalysisService>...", 0);
            //ServiceManager.RegisterService<IRouteAnalysisService>(new ProvideService(RouteAnalysisService.GetDefault));
            SystemLog.Log("注册服务<RouteBDAnalysisService>...", 0);
            ServiceManager.RegisterService<IRouteBDAnalysisService>(new ProvideService(RouteBDAnalysisService.GetDefault));
            SystemLog.Log("注册服务<DataBaseService>...", 0);
            ServiceManager.RegisterService<IDataBaseService>(new ProvideService(DataBaseService.GetDefault));
            SystemLog.Log("注册服务<FieldsFilterService>...", 0);
            ServiceManager.RegisterService<IFieldsFilterService>(new ProvideService(FieldsFilterService.GetDefault));
            SystemLog.Log("注册服务<LayerGroupService>...", 0);
            ServiceManager.RegisterService<ILayerGroupService>(new ProvideService(LayerGroupService.GetDefault));
            SystemLog.Log("注册服务<CameraInfoService>...", 0);
            ServiceManager.RegisterService<ICameraInfoService>(new ProvideService(CameraInfoService.GetDefault));
            SystemLog.Log("注册服务<QueryService>...", 0);
            ServiceManager.RegisterService<IQueryService>(new ProvideService(QueryService.GetDefault));
            SystemLog.Log("注册服务<MaphostService>...", 0);
            ServiceManager.RegisterService<IMaphostService>(new ProvideService(MaphostService.GetDefault));
            SystemLog.Log("注册服务<ShellService>...", 0);
            ServiceManager.RegisterService<IShellService>(new ProvideService(ShellService.GetDefault));
            SystemLog.Log("注册服务<ShowCaptureObjectService>...", 0);
            ServiceManager.RegisterService<IShowCaptureObjectService>(new ProvideService(ShowCaptureObjectService.GetDefault));
            SystemLog.Log("注册服务<HttpServiceConfigService>...", 0);
            ServiceManager.RegisterService<IHttpServiceConfigService>(new ProvideService(HttpServiceConfigService.GetDefault));
            SystemLog.Log("注册服务<NetWorkCheckService>...", 0);
            ServiceManager.RegisterService<INetWorkCheckService>(new ProvideService(NetWorkCheckService.GetDefault));
            SystemLog.Log("注册服务<MovePoiService>...", 0);
            ServiceManager.RegisterService<IMovePoiService>(new ProvideService(MovePoiService.GetDefault));
            SystemLog.Log("注册服务<StatisticLayerService>...", 0);
            ServiceManager.RegisterService<IStatisticLayerService>(new ProvideService(StatisticLayerService.GetDefault));
            SystemLog.Log("注册服务<PoliceHttpService>...", 0);
            ServiceManager.RegisterService<IPoliceHttpService>(new ProvideService(PoliceHttpService.GetDefault));
            SystemLog.Log("注册服务<HttpVideoService>...", 0);
            ServiceManager.RegisterService<IVideoHttpService>(new ProvideService(VideoHttpService.GetDefault));
            SystemLog.Log("注册服务<HttpVideoService>...", 0);
            ServiceManager.RegisterService<ISubjectCaseHttpService>(new ProvideService(SubjectCaseHttpService.GetDefault));
            //SystemLog.Log("注册服务<ObjectForScriptService>...", 0);
            //ServiceManager.RegisterService<IObjectForScriptService>(new ProvideService(ObjectForScriptService.GetDefault));
            SystemLog.Log("结束注册服务...", 0);
        }

        private static void WebBrowserVersionEmulation()
        {
            const string BROWSER_EMULATION_KEY =
            @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            //
            // app.exe and app.vshost.exe
            String appname = Process.GetCurrentProcess().ProcessName + ".exe";
            //
            // Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
            const int browserEmulationMode = 11001;

            RegistryKey browserEmulationKey =
                Registry.CurrentUser.OpenSubKey(BROWSER_EMULATION_KEY, RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                Registry.CurrentUser.CreateSubKey(BROWSER_EMULATION_KEY);

            if (browserEmulationKey != null)
            {
                browserEmulationKey.SetValue(appname, browserEmulationMode, RegistryValueKind.DWord);
                browserEmulationKey.Close();
            }
        }

        public void SetLanguage()
        {
            if (string.IsNullOrEmpty(CacheData.CurrentLanguage)) return;
            var language = LanguageManager.Languages.SingleOrDefault(t => t.Name == CacheData.CurrentLanguage);
            LanguageManager.ChangeLanguages(Application.Current.Resources, language);
        }
    }
}