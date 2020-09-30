using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Reflection;

namespace Mmc.Mspace.HumanAssociationModule.findpeople
{
    /// <summary>
    /// FindPeopleWebView.xaml 的交互逻辑
    /// </summary>
    public partial class FindPeopleWebView : Window
    {
        private string buildUrl = "http://58.60.185.51:5759/xfyhpc-ps-web/system.do?toBuildInfoMoreNoLogin&buildid=";

        public string BuidId { get; set; }
        public FindPeopleWebView()
        {
            InitializeComponent();
            // string szTmp = "http://www.baidu.com";
            // http://58.60.185.51:5759/xfyhpc-ps-web/system.do?toBuildInfoMoreNoLogin&buildid=4403060080020700066
            // string szTmp = "file:///F:/%E4%B8%9A%E5%8A%A1%E6%96%87%E6%A1%A3/backgroundStatistics/backgroundStatistics.html";

           // string szTmp = "http://58.60.185.51:5759/xfyhpc-ps-web/system.do?toBuildInfoMoreNoLogin&buildid=4403060080020700066";

            // string szTmp = "file:///F:/%E4%B8%9A%E5%8A%A1%E6%96%87%E6%A1%A3/ZHNS_JG_11_20/ZHNS_JG_11_20.html";
            //string szTmp ="file:///G:/code/Police3D/%E9%A1%B9%E7%9B%AE%E6%95%B0%E6%8D%AE/ZHNS_JG_12_8_1/ZHNS_JG_12_8_1.html";
            
        }

        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // ((sender as WebBrowser).Document as mshtml.HTMLDocumentEvents_Event).oncontextmenu += new mshtml.HTMLDocumentEvents_oncontextmenuEventHandler(ExtendFrameControl_oncontextmenu);
            mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webCtrl.Document; //定义HTML
            dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
            dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
            this.webCtrl.SuppressScriptErrors(true);
           // string buildId = "";
            //string url = buildUrl + BuidId;
            //Uri uri = new Uri(url);
            //webCtrl.Navigate(uri);
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
