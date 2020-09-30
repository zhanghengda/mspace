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
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.CoreModule;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.RoutePlanning.Design
{
    /// <summary>
    /// RouteDesignView.xaml 的交互逻辑
    /// </summary>
    public partial class RouteDesignView : Window
    {
        public RouteDesignView()
        {
            InitializeComponent();
        }

        private void webCtrl_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                //mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webBrowser.Document; //定义HTML
                //dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
                //dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void Navigate(string url)
        {
            Uri uri = new Uri(url);
            // WebBrowser控件显示的网页路径
            this.webBrowser.Navigate(uri);
            webBrowser.SuppressScriptErrors(true);
        }
    }
}
