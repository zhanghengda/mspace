using Mmc.DataSourceAccess;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.PoiManagerModule;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMC.MSpace.Views
{
    /// <summary>
    /// LeftContentView.xaml 的交互逻辑
    /// 只能用浮动窗体方式
    /// </summary>
    public partial class LeftContentView : Window, IWebView
    {
        private Mmc.Mspace.Main.ObjectForScriptingHelper _scriptHelper;
        public LeftContentView(Shell shellView)
        {
            InitializeComponent();
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

            string url = json1.poiUrl + @"/leftmenu.html";
            Uri uri = new Uri(url);
            SystemLog.WriteLog("LeftContentView" + url);
            var width = int.Parse(ConfigurationManager.AppSettings["LeftViewWidth"]);
            this.Width = width;
            // WebBrowser控件显示的网页路径
            this.webBrowser.LoadCompleted += WebBrowser_LoadCompleted;
            this.webBrowser.Navigate(uri);
            // 将当前类设置为可由脚本访问
            _scriptHelper = new Mmc.Mspace.Main.ObjectForScriptingHelper();
            this.JsScriptInvoker = _scriptHelper;
            _scriptHelper.LeftWindow = this;
            this.webBrowser.ObjectForScripting = _scriptHelper;
            this.webBrowser.PreviewKeyDown += WebBrowser_PreviewKeyDown;
            this.Owner = shellView;
            this.Height = SystemParameters.WorkArea.Height - 48;

            //_scriptHelper.GetMarkerListByMapRange();
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                SystemLog.WriteLog("LeftContentView.webCtrl_LoadCompleted");
                mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webBrowser.Document; //定义HTML
                dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
                dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
                this.webBrowser.SuppressScriptErrors(true);
                SystemLog.WriteLog("start.webCtrl_LoadCompleted.UpdateLeftViewLayer");
                _scriptHelper.UpdateLeftViewLayer();
                SystemLog.WriteLog("end.webCtrl_LoadCompleted.UpdateLeftViewLayer");
                //_scriptHelper.UpdateMarkerTable();
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LeftContentView.webCtrl_LoadCompleted", ex);
            }
        }

        private void WebBrowser_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                e.Handled = true;
            }
        }

        public IJsScriptInvokerService JsScriptInvoker { get; }

        public void RequestUrl(string url)
        {
            SystemLog.WriteLog("LeftContentView.RequestUrl" + url);
            this.webBrowser.Navigate(new Uri(url));
        }

        private void webCtrl_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                SystemLog.WriteLog("LeftContentView.webCtrl_LoadCompleted");
                mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webBrowser.Document; //定义HTML
                dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
                dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
                this.webBrowser.SuppressScriptErrors(true);
                SystemLog.WriteLog("start.webCtrl_LoadCompleted.UpdateLeftViewLayer");
                _scriptHelper.UpdateLeftViewLayer();
                SystemLog.WriteLog("end.webCtrl_LoadCompleted.UpdateLeftViewLayer");
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog("LeftContentView.webCtrl_LoadCompleted", ex);
            }

        }



        public void InvokeScript(string methodName, params object[] obj)
        {
            this.webBrowser.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.webBrowser.InvokeScript(methodName, obj);
            }));
        }

        public void InvokeScript(string methodName)
        {
            this.webBrowser.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.webBrowser.InvokeScript(methodName);
            }));
        }
    }
    public static class WebBrowserExtensions
    {
        public static void SuppressScriptErrors(this System.Windows.Controls.WebBrowser webBrowser, bool hide)
        {
            FieldInfo fiComWebBrowser = typeof(System.Windows.Controls.WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }

    }
}
