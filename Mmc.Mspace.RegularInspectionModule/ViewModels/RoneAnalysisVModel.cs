using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.RegularInspectionModule.Views;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Resource;
using Mmc.Mspace.Const.ConstDataBase;
using System.Drawing;
using Gvitech.Windows.Utils;
using System;
using Mmc.Mspace.Const.ConstPath;
using System.Net;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Utils;
using Mmc.Mspace.Theme.Pop;
using System.Collections.ObjectModel;
using Mmc.Mspace.RegularInspectionModule.model;
using Mmc.Windows.Services;
using Mmc.DataSourceAccess;
using Mmc.Mspace.Services.DataSourceServices;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class RoneAnalysisVModel: CheckedToolItemModel
    {
        // private string drwa_station="";
       
        int num11;
        List<string> NameList = new List<string>();
        List<string> NumList = new List<string>();
        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();
        
        IPolygon polygon = null;
        private string _geom;
        
        public override void Initialize()
        {

            
            _Nowdays = DateTime.Now.AddDays(1);
            DateTime dt = new DateTime(2012, 1, 1, 0, 0, 0);
            _BeforeData = dt;
            base.Initialize();
            base.ViewType = (ViewType)1;
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC("WaterPolygonDrawn", DrawCustomerType.MenuCommand);
            }
            this.Polygon_Draw_Cmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                UnRegisterDraw();
                RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished += Rone_PolygonDraw_OnDrawFinished;
                var view = (RoneAnalysisView)base.View;
                view.Hide();
            });
            this.Circle_Draw_Cmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                UnRegisterDraw();
                RCDrawManager.Instance.CustomDrawCircle.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.CustomDrawCircle.OnDrawFinished += CustomDrawCircle_OnDrawFinished;
                var view = (RoneAnalysisView)base.View;
                view.Hide();
            });
            this.Rectangle_Draw_Cmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                UnRegisterDraw();
                RCDrawManager.Instance.RectangleDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.RectangleDraw.OnDrawFinished += RectangleDraw_OnDrawFinished;
                var view = (RoneAnalysisView)base.View;
                view.Hide();

            });
            this.Close_Cmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                UnRegisterDraw();
                base.OnUnchecked();
                base.IsChecked = false;
                var view = (RoneAnalysisView)base.View;
                view.Hide();
                if (_rpm_ != null)
                {
                    _rpm_.VisibleMask = gviViewportMask.gviViewNone;
                }
               // base.OnUnchecked();
            });
            this.Draw_Cmd = new Mmc.Wpf.Commands.RelayCommand(() =>//请求数据并返回查询结果
            {
                try
                {

                    var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    string _poiHost = json.poiUrl;
                    string wkt_poly = polygon.AsWKT();
                    _geom = wkt_poly;
                    var request = (HttpWebRequest)WebRequest.Create(_poiHost + "/api/marker/report?geom=" + wkt_poly + "&startTime=" + _BeforeData + "&endTime=" + _Nowdays);
                    request.Method = "GET";
                    HttpService _httpService = new HttpService();
                    _httpService.Token = HttpServiceUtil.Token;
                    SetHeaderValue(request.Headers, "token", HttpServiceUtil.Token);
                    SetHeaderValue(request.Headers, "mspace-version", "v1.5");
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    Analyzejsonstring(responseString, out num11, out NameList, out NumList);
                }
                catch { }
                if (num11 == 0) { Messages.ShowMessage("该时间段或该区域不包含标注，或该标注未设置标签"); return; }
                else if (num11 != 0)
                {

                    if (_PieCheck == true)
                    {
                        ShowResult(num11, NameList, NumList);
                    }
                    else if (_PieCheck == false)
                    {
                        ShowbarResult(num11, NameList, NumList);
                    }
                }
               
            });
            
            //this.TestCmd = new Wpf.Commands.RelayCommand(FeatureCollection);
        }

        private IRenderModelPoint _rpm_;
        [XmlIgnore]
        public ICommand Polygon_Draw_Cmd { get; set; }
        [XmlIgnore]
        public ICommand Circle_Draw_Cmd { get; set; }
        [XmlIgnore]
        public ICommand Rectangle_Draw_Cmd { get; set; }
        [XmlIgnore]
        public ICommand Close_Cmd { get; set; }
        [XmlIgnore]
        public ICommand Draw_Cmd { get; set; }

        public ICommand TestCmd { get; set; }

        public override FrameworkElement CreatedView()
        {
            return new RoneAnalysisView() { Owner = Application.Current.MainWindow };
        }
        private bool _PieCheck;
        public bool PieCheck
        {
            get { return this._PieCheck; }
            set
            {
                _PieCheck = value; NotifyPropertyChanged("PieCheck");
            }
        }


        private DateTime _Nowdays;
        public string Nowdays
        {
            get { return this._Nowdays.ToShortDateString(); }
            set
            {

                _Nowdays = Convert.ToDateTime(value); NotifyPropertyChanged("Nowdays");

            }

        }

        private DateTime _BeforeData;
        public string BeforeData
        {
            get { return this._BeforeData.ToShortDateString(); }
            set
            {

                _BeforeData = Convert.ToDateTime(value); NotifyPropertyChanged("BeforeData");

            }

        }
     /*   private DateTime _AfterData;
        public DateTime AfterData
        {
            get { return this._AfterData; }
            set
            {
                _AfterData = value; NotifyPropertyChanged("AfterData");
            }
        }*/

        RoneAnalysisView view;
        public override void OnChecked()
        {
            base.OnChecked();
            view = (RoneAnalysisView)base.View;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.DataContext = this;
            view.Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            var view = (RoneAnalysisView)base.View;
            view.Hide();
        }
        private DrawCustomerUC drawCustomer;
      
       // AnalysisiChartDisplayView analysisiChartDisplayView;
        
        private void Rone_PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                var rPolygon = result as IRenderPolygon;
                polygon  = rPolygon.GetFdeGeometry() as IPolygon;

                polygon.SpatialCRS = GviMap.SpatialCrs;

               
                var view = (RoneAnalysisView)base.View;
                view.Show();
                //   MessageBox.Show(Convert.ToString(num11));
            }
            catch { }
          
        }
        private void RectangleDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                var rPolygon = result as IRenderPolygon;
                polygon = rPolygon.GetFdeGeometry() as IPolygon;

                polygon.SpatialCRS = GviMap.SpatialCrs;
              
                var view = (RoneAnalysisView)base.View;
                view.Show();

            }
            catch { }
           
        }
        private void CustomDrawCircle_OnDrawFinished(object sender, object result)
        {
            try
            {
                var rPolygon = result as IRenderPolygon;
                polygon = rPolygon.GetFdeGeometry() as IPolygon;

                polygon.SpatialCRS = GviMap.SpatialCrs;
             
                var view = (RoneAnalysisView)base.View;
                view.Show();
            }
            catch { }
        }

        public void Analyzejsonstring(string input,out int Object_num, out List<string> _namelist, out List<string> _numlist)//序列化json并取值
        {
            List<string> namelist = new List<string>();
            List<string> numlist = new List<string>();
        
            using (Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(new StringReader(input)))
            {
                JObject o = (JObject)JToken.ReadFrom(reader);
                string a = o["status"].ToString();
                string b = o["message"].ToString();
                //  var b = o["other"];
                //   var c = b["lotaddress"];
                var d = o["data"];
                
                foreach (JObject e in d)
                {
                    var name = e["name"];
                    namelist.Add(name.ToString());
                    var num = e["num"];
                    numlist.Add(num.ToString());

                  //  MessageBox.Show(name + ":" + num);
                }
                Object_num = namelist.Count;
                _namelist = namelist;
                _numlist = numlist;
            }
        }


        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)//请求报头设置
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


        private void ShowResult(int num,List<string> namelist, List<string> numlist)//pie展示
        {
           // analysisiChartDisplayView = new AnalysisiChartDisplayView();
         //   analysisiChartDisplayView.Show();
           AnalysisiChartVModel analysisiChartVModel = new AnalysisiChartVModel(num, namelist, numlist, _geom);
            
        }
        private void ShowbarResult(int num, List<string> namelist, List<string> numlist)//bar展示
        {
            AnalysisiBarChartVModel analysisiBarChartVModel = new AnalysisiBarChartVModel(num, namelist, numlist, _geom);
        }

        private void UnRegisterDraw()
        {
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.CustomDrawCircle.OnDrawFinished -= CustomDrawCircle_OnDrawFinished;
            RCDrawManager.Instance.RectangleDraw.OnDrawFinished -= RectangleDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
            RCDrawManager.Instance.RectangleDraw.UnRegister(GviMap.AxMapControl);
            RCDrawManager.Instance.CustomDrawCircle.UnRegister(GviMap.AxMapControl);
        }


    }
}
