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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FireControlModule.UnitInfo
{
    /// <summary>
    /// Full3dView.xaml 的交互逻辑
    /// </summary>
    public partial class Full3dView : Window
    {
        public Full3dView()
        {
            InitializeComponent();
        }

        private void webCtrl_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument dom = (mshtml.HTMLDocument)webCtrl.Document; //定义HTML
            dom.documentElement.style.overflow = "hidden"; //隐藏浏览器的滚动条
            dom.body.setAttribute("scroll", "no"); //禁用浏览器的滚动条
            this.webCtrl.SuppressScriptErrors(true);
        }

        public void Navigate(string url)
        {
            webCtrl.Navigate(new Uri(url));
        }

       
    }
}
