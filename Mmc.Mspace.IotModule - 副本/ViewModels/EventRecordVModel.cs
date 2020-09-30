using FireControlModule.FireIot;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.IotModule.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.IotModule.ViewModels
{
   public class EventRecordVModel: CheckedToolItemModel
    {
        EventRecordView EventRecordView = new EventRecordView();
        public Action<string> DateTimeChange;
        public Dictionary<string, Guid> eventList = new Dictionary<string, Guid>();
        public Dictionary<string, Guid> eventTableList = new Dictionary<string, Guid>();
        public Dictionary<string, string> eventStatusguid = new Dictionary<string, string>();
        public ICommand CloseCmd { get; set; }
        public EventRecordVModel()
        {          
            EventRecordView.DataContext = this;
            this.CloseCmd = new RelayCommand(HideEventRecordView);
        }
        
        public void ShowEventRecordView()
        {
            EventRecordView.Show();
            EventRecordView.Left = Application.Current.MainWindow.Width * 0.45;
            EventRecordView.Top = Application.Current.MainWindow.Height * 0.15;
            EventRecordView.SelectedRecordDateTimeChange -= onSelectedRecordDateTimeChange;
            EventRecordView.SelectedRecordDateTimeChange += onSelectedRecordDateTimeChange;
            //GetEventreport(DateTime.Now.Date.ToString());
        }

        public void HideEventRecordView()
        {
            EventRecordView.Hide();
        }
        private void onSelectedRecordDateTimeChange(string dateStr)
        {
            DateTime dt = Convert.ToDateTime(dateStr);
            if (dt == null ) return;
            DateTimeChange(dt.ToString("yyyy-MM-dd"));
            GetEventreport(dateStr);
        }
        public void GetEventreport(string dateStr)
        {
            ClearMapEvent();
            try
            {
                List<EventInfo> eventList = new List<EventInfo>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                if(dateStr == DateTime.Now.Date.ToString())
                {
                    dic.Add("start_time", DateTime.Now.Date.ToString());
                    dic.Add("end_time", DateTime.Now.ToString());
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(dateStr);
                    dic.Add("start_time", dt.Date.ToString("yyyy-MM-dd"));
                    DateTime yesterday = dt.AddDays(1);
                    dic.Add("end_time", yesterday.Date.ToString("yyyy-MM-dd"));
                }
               

                string json = JsonUtil.SerializeToString(dic);

                var res = HttpServiceHelper.Instance.PostRequestForData(GridPatrolInterface.GetEventreportInf, json);
                List<EventInfo> resDyn = JsonUtil.DeserializeFromString<List<EventInfo>>(res);
                if (resDyn != null&&resDyn?.Count!=0)
                {
                    foreach (var item in resDyn)
                    {
                        CreateEventPoint(item);
                    }
                }
              else
                {
                    DateTime dt = Convert.ToDateTime(dateStr);
                    if (dt == null) return;                   
                    Messages.ShowMessage(dt.ToString("D") + "暂无上报数据");
                }
                // string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.GetEventreportInf);
                // eventList = JsonUtil.DeserializeFromString<List<EventInfo>>(resStr);
            }
            catch (HttpException httpEx)
            {
                HttpException.ShowHttpExcetion(httpEx.Message);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        private void CreateEventPoint(EventInfo eventInfo)
        {
            string id = eventInfo.id;
            string geom = eventInfo.geom;
            string up_time = eventInfo.up_time;
            string event_name = eventInfo.event_name;
            string event_details = eventInfo.event_details;                  
            if (geom != "" && geom != null&&geom!= "POINT Z(0 0 0)")
            {
                string type="";
                GetStatus(eventInfo.status, out type);
                IGeometry pos = GviMap.GeoFactory.CreateFromWKT(geom) as IGeometry;
                pos.SpatialCRS = GviMap.SpatialCrs;
                IPoint point = pos as IPoint;
                var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                poi.SetPostion(point.X, point.Y, 2);
                poi.Size = 30;
                poi.ShowName = true;
                poi.MaxVisibleDistance = 5000;
                poi.MinVisibleDistance = 100;
                poi.Name = event_name;
                poi.SpatialCRS = GviMap.SpatialCrs;
                poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", type);//Helpers.ResourceHelper.FindResourceByKey("userImg").ToString();//
                IRenderPOI rpoi = GviMap.ObjectManager.CreateRenderPOI(poi);
                rpoi.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                //rpoi.Highlight(Color.Red);
                //GviMap.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                //IRenderPOI renderPOI = GviMap.ObjectManager.GetObjectById(rpoi.Guid) as IRenderPOI;
                //IPoint flyPoint = renderPOI.GetFdeGeometry() as IPoint;
                //point.SetPostion(point.X, point.Y, 50);
                //GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                //GviMap.Camera.FlyToEnvelope(point.Envelope);
                //eulerAngle.Tilt = -90;
                //GviMap.Camera.SetCamera2(point, eulerAngle, 0);
                //guidList.Add(rpoi.Guid);
                if (eventList.ContainsKey(id))
                {
                    GviMap.ObjectManager.DeleteObject(eventList[id]);
                    eventList.Remove(id);
                    //eventStatusguid.Remove();
                }
                eventList.Add(id, rpoi.Guid);
                eventStatusguid.Add(id, eventInfo.status);
                var table = createTableLable(point, event_name, type);
                DateTime dtShort = Convert.ToDateTime(up_time);               
                table.SetRecord(0, 1, dtShort.ToString("HH:mm:ss"));
                table.SetRecord(1, 1, type);
                table.VisibleMask = gviViewportMask.gviViewNone;
                if (eventTableList.ContainsKey(id))
                {
                    GviMap.ObjectManager.DeleteObject(eventTableList[id]);
                    eventTableList.Remove(id);
                }
                eventTableList.Add(id, table.Guid);
            }
            //else
            //{
            //    Messages.ShowMessage("该事件未上传位置信息！");
            //}
        }
        private ITableLabel createTableLable(IPoint pt, string event_name, string type)
        {
            var tableLabel = TableLabelFactory.CreateWindTable(GviMap.ObjectManager, 2, 2);
            pt.Z = 2;
            tableLabel.Position = pt;
            //tableLabel.Position.Z = 2;            
            tableLabel.VisibleMask = gviViewportMask.gviViewAllNormalView;
            tableLabel.TitleText = event_name;
            tableLabel.SetColumnWidth(0, 100);
            tableLabel.SetColumnWidth(1, 105);
            // 设定表格中第1行，第1列的显示文字
            tableLabel.SetRecord(0, 0, "上报时间:");
            // 第1行，第2列
            tableLabel.SetRecord(1, 0, "状态:");


            return tableLabel;
        }
        private void GetStatus (string status ,out string type)
        {

            switch (status)
            {
                case "0":
                    type = "待分派";
                    //imgType = "待分配.png";
                    break;
                case "1":
                    type = "待审核";
                   // imgType = "待审核.png";
                    break;
                case "4":
                    type = "待受理";
                  //  imgType = "待受理.png";
                    break;
                case "5":
                    type = "待办结";
                  //  imgType = "待办结.png";
                    break;
                case "6":
                    type = "待完结";
                  //  imgType = "待完结.png";
                    break;
                case "7":
                    type = "已归档";
                //    imgType = "已归档.png";
                    break;
                default:
                    type = "待分配";
                   // imgType = "待分配.png"; 
                    break;
            }
        }
        public void ClearMapEvent()
        {
            if (eventList?.Count != 0)
            {
                foreach (var item in eventList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            if (eventTableList?.Count != 0)
            {
                foreach (var item in eventTableList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }           
            eventList = new Dictionary<string, Guid>();
            eventTableList = new Dictionary<string, Guid>();
            eventStatusguid = new Dictionary<string, string>();
        }
    }
}
