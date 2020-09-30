using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.CoreModule;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.JscriptInvokeService;
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
using Newtonsoft.Json.Linq;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.RoutePlanning.Models;

namespace Mmc.Mspace.RoutePlanning
{
    /// <summary>
    /// RoutePlanView.xaml 的交互逻辑
    /// </summary>
    /// 
    public delegate void DelFlyObjectDelegate();
    public delegate void FallEventHandler();
    public delegate void ConveyJson(string[] arrString);

    public partial class RoutePlanView
    {
        public ConveyJson conveyJson2; 
        public RoutePlanView()
        {
            InitializeComponent();
            syncRoutePlanView();
        }
        DelFlyObjectDelegate delStatic;

        public void syncRoutePlanView()
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var _RouteHost = json.poiUrl;
            string url = string.Format(@"{0}/flight-course/manual-list", _RouteHost);
          
            JsScriptBasic jsEvent = new JsScriptBasic();
            jsEvent.OnFlightSimulating -= OnFlightSimulating;
            jsEvent.OnFlightSimulating += OnFlightSimulating;
            //delStatic= jsEvent.DelFlyObject;

            jsEvent.window = this;
            jsEvent.conveyJson = new ConveyJson(GetMissionJson);
            this.webBrowser.ObjectForScripting = jsEvent;
            jsEvent.window.webBrowser.Navigate(url);
        }
        private void GetMissionJson(string[] arrString)
        {
            conveyJson2(arrString);
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            //delStatic();
            var vm = this.DataContext as RouteListViewModel;            
            vm?.releaseWindow();
            
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

        public void Navigate(string url)
        {
            Uri uri = new Uri(url);
            this.webBrowser.Navigate(uri);           
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

        public IJsScriptInvokerService JsScriptInvoker { get; }

        public void InvokeScript(string methodName, params object[] obj)
        {
            this.webBrowser.InvokeScript(methodName, obj);
        }

        public void InvokeScript(string methodName)
        {
            this.webBrowser.InvokeScript(methodName);
        }
        public void ReadMissionJson(string name, string strJson)
        {
        }
        public void toDo(string msg)
        {
            MessageBox.Show(msg, "---------title-------", MessageBoxButton.OKCancel);
        }




        public void OnFlightSimulating(string id, string polylineStr)
        {
            this.Hide();
           
            var resDyn = JsonUtil.DeserializeFromString<dynamic>(polylineStr);

            var data = resDyn.items;
            if (data == null)
                return;
            var resDataStr = JsonUtil.SerializeToString(data);
            var flypoint = JsonUtil.DeserializeFromString<List<Flypoints>>(resDataStr);
            foreach (var ele in flypoint)
            {
                point = gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                point.SpatialCRS = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(WKTString.WGS_84_WKT);
                JArray jdata = ele.coordinate;
                point.SetCoords(Convert.ToDouble(jdata[1]), Convert.ToDouble(jdata[0]), Convert.ToDouble(jdata[2]), 0, 0);
                pointList.Add(point);

            }
            flySimulate.FlyPointList = pointList;
            flySimulate.fly(pointList);
            //pointList.Clear();
           
        }

        List<IPoint> pointList = new List<IPoint>();
        GeometryFactory gfactory = new GeometryFactory();
        IPoint point = null;
        public FlySimulate flySimulate = new FlySimulate();


        public void DelFlyObject()
        {
            flySimulate.RemoveFlight();
        }
               
    }

    [System.Runtime.InteropServices.ComVisible(true)] // 将该类设置为com可访问
  
    public class JsScriptBasic
    {
        public Action<string,string> OnFlightSimulating;
        public RoutePlanView window { get; set; }
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
        List<IPoint> points = new List<IPoint>();

        GeometryFactory gfactory = new GeometryFactory();
        IPoint point = null;
        public  FlySimulate flySimulate = new FlySimulate();
       


        public void FlightSimulating(string id, string polylineStr)
        {
            OnFlightSimulating(id, polylineStr);
            
        }
    }

    
   
}
