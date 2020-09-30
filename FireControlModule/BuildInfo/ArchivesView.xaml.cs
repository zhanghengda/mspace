using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule
{
    /// <summary>
    /// FindPeopleWebView.xaml 的交互逻辑
    /// </summary>
    public partial class ArchivesView : Window
    {
        public ArchivesView()
        {
            InitializeComponent();
            ObjectForScriptingHelper jsInvoker = new ObjectForScriptingHelper();
            jsInvoker.LeftWindow = this;
            this.webCtrl.ObjectForScripting = jsInvoker;
        }

        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // ((sender as WebBrowser).Document as mshtml.HTMLDocumentEvents_Event).oncontextmenu += new mshtml.HTMLDocumentEvents_oncontextmenuEventHandler(ExtendFrameControl_oncontextmenu);

            mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webCtrl.Document; //定义HTML
            dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
            dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
            this.webCtrl.SuppressScriptErrors(true);
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try { base.DragMove(); } catch (Exception) { }
        }

        public void Navigate(string url)
        {
            this.webCtrl.Navigate(new Uri(url));
        }
    }
}