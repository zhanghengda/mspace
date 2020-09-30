using Mmc.Mspace.Common.Helpers;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Mmc.Mspace.Services.HttpService;

namespace Mmc.Mspace.IotModule.Views
{
    /// <summary>
    /// StatisticsView.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsView
    {
        public StatisticsView()
        {
            InitializeComponent();
            syncRoutePlanView();
        }

        public void syncRoutePlanView()
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var _RouteHost = json.poiUrl;
            string url = $"{_RouteHost}/echarts.html?token={HttpServiceUtil.Token}";

            JsScriptBasic jsEvent = new JsScriptBasic();
            jsEvent.window = this;
            this.webBrowser.ObjectForScripting = jsEvent;
            WebBrowserZoomInvoker.AddZoomInvoker(this.webBrowser);
            jsEvent.window.webBrowser.Navigate(url);
        }
        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webBrowser.Document; //定义HTML
                dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
                dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
                webBrowser.SuppressScriptErrors(true);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
        public IJsScriptInvokerService JsScriptInvoker { get; }

        public void toDo(string msg)
        {


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
    [System.Runtime.InteropServices.ComVisible(true)] // 将该类设置为com可访问

    public class JsScriptBasic
    {

        public StatisticsView window { get; set; }

        public void toDo(string msg)
        {
            window.toDo(msg);
        }


    }
}
