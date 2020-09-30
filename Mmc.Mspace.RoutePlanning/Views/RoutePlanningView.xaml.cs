using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.ViewModels;
using Mmc.Windows.Utils;
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

namespace Mmc.Mspace.RoutePlanning.Views
{
    public delegate void ConveyJson(string[] arrString);
    /// <summary>
    /// RoutePlanView.xaml 的交互逻辑
    /// </summary>
    public partial class RoutePlanningView 
    {
        public ConveyJson conveyJson2;

        public RoutePlanningView()
        {
            InitializeComponent();
            syncRoutePlanView();
        }

        public void syncRoutePlanView()
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var _RouteHost = json.poiUrl;
            string url = string.Format(@"{0}/flight-course/manual-list", _RouteHost);

            JsScriptBasic jsEvent = new JsScriptBasic();
            jsEvent.window = this;
            jsEvent.conveyJson = new ConveyJson(GetMissionJson);
            //this.webBrowser.ObjectForScripting = jsEvent;
            //jsEvent.window.webBrowser.Navigate(url);
        }
        private void GetMissionJson(string[] arrString)
        {
            conveyJson2(arrString);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var vm = this.DataContext as RoutePlanningViewModel;
            vm?.OnReleaseWindow();
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

        public void ReadMissionJson(string name, string strJson)
        {
        }
        public void toDo(string msg)
        {
            MessageBox.Show(msg, "---------title-------", MessageBoxButton.OKCancel);
        }
    }

    [System.Runtime.InteropServices.ComVisible(true)] // 将该类设置为com可访问

    public class JsScriptBasic
    {

        public RoutePlanningView window { get; set; }
        public ConveyJson conveyJson;
        public void toDo(string msg)
        {
            window.toDo(msg);
        }
        public void ReadMissionJson(string name, string strJson)
        {
            string[] arrString = { name, strJson };
            conveyJson(arrString);
        }

    }
}
