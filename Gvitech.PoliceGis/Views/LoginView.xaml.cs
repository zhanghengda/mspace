using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Languagepack.LanguageManager;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Toolkit.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window, IWebView
    {
        public LoginView()
        {
            InitializeComponent();

            string configFile = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig;

            if(!File.Exists(configFile))
            {
                MessageBox.Show(Helpers.ResourceHelper.FindKey("ConfigFileMiss"));
                SystemLog.Log(string.Format("找不到配置文件:{0}", configFile));
                return;
            }

            var config = JsonUtil.DeserializeFromFile<dynamic>(configFile);

            if (config.poiUrl == null || config.mspaceVersion ==null)
            {
                MessageBox.Show(Helpers.ResourceHelper.FindKey("ConfigItemError"));
                SystemLog.Log(string.Format("配置项不能为空:poiUrl or mspaceVersion"));
                return;
            }

            HttpServiceUtil.MspaceHostUrl = config.poiUrl;
            HttpServiceUtil.MspaceVersion = config.mspaceVersion;

            WebConfig.MspaceHostUrl = config.poiUrl;
            WebConfig.MspaceVersion = config.mspaceVersion;
            WebConfig.UavSocketUri = config.uavSocketUri;
            double TracePoiDistance = 5000;
            double LabelMaxDistance = 1000;
            if (config.tracePoiMaxDistance != null && double.TryParse(config.tracePoiMaxDistance.ToString(), out TracePoiDistance))
                WebConfig.TracePoiMaxDistance = TracePoiDistance;
            if (config.labelMaxDistance != null && double.TryParse(config.labelMaxDistance.ToString(), out LabelMaxDistance))
                WebConfig.LabelMaxDistance = LabelMaxDistance;

            string version = "&mspace_version=" + WebConfig.MspaceVersion;
            string url = WebConfig.MspaceHostUrl + @"/site/login?appid=Ydsad1DA3dsa1EQ21sa32EWAd1fwqdsa" + version;
            Uri uri = new Uri(url);
            JsScriptHelper helper = new JsScriptHelper();
            helper.Window = this;
            this.webBrowser.ObjectForScripting = helper;
            // WebBrowser控件显示的网页路径
            WebBrowserZoomInvoker.AddZoomInvoker(this.webBrowser);
            helper.LanguageSwitching(CacheData.CurrentLanguage);
            helper.Window.webBrowser.Navigate(uri);
        }
                           
        public IJsScriptInvokerService JsScriptInvoker { get; }

        public void InvokeScript(string methodName, params object[] obj)
        {
            this.webBrowser.InvokeScript(methodName, obj);
        }

        public void InvokeScript(string methodName)
        {
            this.webBrowser.InvokeScript(methodName);
        }

        public void RequestUrl(string url)
        {
            this.webBrowser.Navigate(new Uri(url));
        }

        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webBrowser.Document; //定义HTML
                dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
                dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
                //var doc = (HTMLDocumentEvents2_Event)webBrowser.Document;
                //doc.oncontextmenu += obj => false;
                this.webBrowser.SuppressScriptErrors(true);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//将该类设置为com可访问
    public class JsScriptHelper
    {
        public LoginView Window { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="isLogin">是否登录成功</param>
        /// <param name="token">用户token</param>
        public void isLogin(bool isLogin,string token)
        {
            HttpServiceUtil.Token = token;
            Window.DialogResult = isLogin;
            if (isLogin)
                GetUserInfo();
        }


        public void GetUserInfo()
        {
            try
            {
                string userApi = UserCfgInterface.UserCng;
                var resutl = HttpServiceHelper.Instance.GetRequestAsync(userApi);
                CacheData.UserInfo = JsonUtil.DeserializeFromString<UserInfo>(resutl);
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LoginView.GetUserInfo", ex);
            }
        }

        public void LanguageSwitching(string lang)
        {
            try
            {
                if (string.IsNullOrEmpty(lang)) return;
                var language = LanguageManager.Languages.SingleOrDefault(t => t.Name == lang);
                LanguageManager.ChangeLanguages(Application.Current.Resources, language);
                CacheData.SaveLanguage(lang);
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LoginView.LanguageSwitching", ex);
            }   
        }
    }
}
