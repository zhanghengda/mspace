using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using Mmc.Mspace.UavModule.UavTracing;
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

namespace Mmc.Mspace.UavModule
{
    /// <summary>
    /// StatisticsWebView.xaml 的交互逻辑
    /// </summary>
    public partial class UavVideoWebView 
    {
        public UavVideoWebView()
        {
            InitializeComponent();
            //var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

            //string url = json1.staticUrl;

          
           
            //webCtrl.Navigate(new Uri(url));
            webCtrl.SuppressScriptErrors(true);
        }
        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // ((sender as WebBrowser).Document as mshtml.HTMLDocumentEvents_Event).oncontextmenu += new mshtml.HTMLDocumentEvents_oncontextmenuEventHandler(ExtendFrameControl_oncontextmenu);
            mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webCtrl.Document; //定义HTML
            dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
            dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception)
            {
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((UavWebVideoViewModel)this.DataContext).IsChecked=false;
        }
    }
    public static class WebBrowserExtensions
    {
        public static void SuppressScriptErrors(this WebBrowser webBrowser, bool hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fiComWebBrowser == null) return;

            object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);

            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });

        }

    }
}
