using FireControlModule.FireIot;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Messenger;
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class PatrolListVModel : CheckedToolItemModel
    {
        public Action<string> ShowPeopleList;
        public Action ClearPatrolPeople;
        public Action HidePatrolmanListView;
        public Action ClearRoute;
        public ICommand RefreshCmd { get; set; }
        public ICommand FoldCmd { get; set; }
        public ICommand IsOpenCmd { get; set; }
        public ICommand SelectAllCmd { get; set; }
        PatrolListView patrolListView = new PatrolListView();
        ResultsView resultsView = new ResultsView();
        AbnormalView abnormalView = new AbnormalView();
        WishView wishView = new WishView();

        public Dictionary<string, Guid> OnlinePeopleList = new Dictionary<string, Guid>();
        public Dictionary<string, Guid> OnlineTableList = new Dictionary<string, Guid>();
        public Dictionary<string, Guid> OnlinePeopleListByArea = new Dictionary<string, Guid>();
        public Dictionary<string, Guid> OnlineTableListByArea = new Dictionary<string, Guid>();
        public PatrolListVModel()
        {
            patrolListView.DataContext = this;
            this.RefreshCmd = new RelayCommand(RefreshPatrolView);
            this.FoldCmd = new RelayCommand(FoldView);
            this.IsOpenCmd = new RelayCommand<GridInfo>((gridIn) => ViewGridInfo(gridIn));
            this.SelectAllCmd = new RelayCommand(OnSelectAll);

            Messenger.Messengers.Register<bool>("ShowSummery", (t) =>
            {
                resultsView.Left = Application.Current.MainWindow.Width * 0.01;
                resultsView.Top = Application.Current.MainWindow.Height * 0.05;
                resultsView.Show();

                abnormalView.Left = Application.Current.MainWindow.Width * 0.01;
                abnormalView.Top = 300;
                abnormalView.Show();

                wishView.Left = 420;
                wishView.Top = Application.Current.MainWindow.Height * 0.05;
                wishView.Show();
            });
            Messenger.Messengers.Register<bool>("HideSummery", (t) =>
            {
                resultsView.Hide();
                abnormalView.Hide();
                wishView.Hide();
            });
        }

        private ObservableCollection<GridInfo> _patrolListCollection = new ObservableCollection<GridInfo>();
        public ObservableCollection<GridInfo> PatrolListCollection
        {
            get { return _patrolListCollection; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<GridInfo>>(ref this._patrolListCollection, value, "PatrolListCollection");
            }
        }

        private Visibility _patrolVisible;
        public Visibility PatrolVisible
        {
            get { return _patrolVisible; }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this._patrolVisible, value, "PatrolVisible");
            }
        }
        public void OpenPatrolView()
        {
            if (PatrolListCollection == null || PatrolListCollection?.Count == 0)
            {
                PatrolListCollection.Clear();//保险
                List<GridInfo> gridList = new List<GridInfo>();
                try
                {
                    string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.PatrolGridListInf);
                    gridList = JsonUtil.DeserializeFromString<List<GridInfo>>(resStr);
                }
                catch (HttpException httpEx)
                {
                    HttpException.ShowHttpExcetion(httpEx.Message);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                if (gridList != null && gridList.Count > 0)
                {
                    foreach (var item in gridList)
                    {
                        PatrolListCollection.Add(item);
                    }
                }
            }
            patrolListView.Left =Application.Current.MainWindow.Width * 0.01;
            patrolListView.Top = Application.Current.MainWindow.Height * 0.1;
            patrolListView.Show();

        }
        public void RefreshPatrolView()
        {
            ClearRoute();
            ClearOnlinePatrolList();
            ClearOnlinePatrolListByArea();
            PatrolListCollection.Clear();
            ClearPatrolPeople();
            HidePatrolmanListView();
            //patrolmanListVModel.ClearPatrolList();
            List<GridInfo> gridList = new List<GridInfo>();
                try
                {
                    string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.PatrolGridListInf);
                    gridList = JsonUtil.DeserializeFromString<List<GridInfo>>(resStr);
                }
                catch (HttpException httpEx)
                {
                    HttpException.ShowHttpExcetion(httpEx.Message);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                if (gridList != null && gridList.Count > 0)
                {
                    foreach (var item in gridList)
                    {
                        PatrolListCollection.Add(item);
                    }
                }
            
            patrolListView.Show();
            patrolListView.Left = Application.Current.MainWindow.Width * 0.01;
            patrolListView.Top = Application.Current.MainWindow.Height * 0.1;
        }
        public void ClosePatrolView()
        {
            patrolListView.Hide();
        }
        private void FoldView()
        {
            
            if (PatrolVisible == Visibility.Visible)
            {
                PatrolVisible = Visibility.Collapsed;
                patrolListView.Height = 24;
                patrolListView.FoldImage.ImageSource = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("packdown_H");
            }
            else
            {
                PatrolVisible = Visibility.Visible;
                patrolListView.Height = 858;
                patrolListView.FoldImage.ImageSource = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("packup_H");
            }
           
        }
        private void ViewGridInfo(GridInfo gridInfo)
        {
            ClearRoute();
            CancleSelectAll();
            gridInfo.Is_Open = !gridInfo.Is_Open;
           // UnCheckAllGrid();
            ClearPatrolPeople();            
            if (gridInfo.Is_Open == true)
            {
            ClearOnlinePatrolList();//清除查看的全部网格的人员数据
            ClearOnlinePatrolListByArea();
            SearchOnlinePatrolByArea(Convert.ToInt32(gridInfo.id));
            ShowPeopleList(gridInfo.id);             
            UnCheckAllGrid(gridInfo.id);
            }
            if(gridInfo.Is_Open == false)
            {
                HidePatrolmanListView();
            }
        }

        private void CheckAllGrid()
        {
            foreach(var item in PatrolListCollection)
            {
                item.Is_Open = true;
            }
            
        }
        public void UnCheckAllGrid(string id="")
        {
            if (id != "")
            {
                foreach (var item in PatrolListCollection)
                {
                    if (item.id != id)
                    {
                        item.Is_Open = false;
                    }
                    if (item.id == id)
                    {
                        item.Is_Open = true;
                    }
                }
            }
            else
            {
                foreach (var item in PatrolListCollection)
                {                    
                        item.Is_Open = false;                    
                }
            }
        }
        bool viewAllPeople = true;
        private void OnSelectAll()
        {
            HidePatrolmanListView();
            ClearRoute();
            if (viewAllPeople)
            {
                ClearOnlinePatrolListByArea();
                ClearOnlinePatrolList();
                ClearPatrolPeople();
                List<OnlinePatrolman> allOnlinePersonList = new List<OnlinePatrolman>();
                try
                {
                    string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.AllonlinePatrolmanInf);
                    allOnlinePersonList = JsonUtil.DeserializeFromString<List<OnlinePatrolman>>(resStr);
                    if (allOnlinePersonList?.Count != 0)
                    {
                        foreach (var item in allOnlinePersonList)
                        {
                            CreatePeoplePosition(item);
                        }
                    }
                }
                catch (HttpException httpEx)
                {
                    HttpException.ShowHttpExcetion(httpEx.Message);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                viewAllPeople = !viewAllPeople;
                patrolListView.ViewAll.Content = "取消查看";
            }
            else
            {
                CancleSelectAll();
            }
        }
        public void CancleSelectAll()
        {
            viewAllPeople = !viewAllPeople;
            ClearOnlinePatrolListByArea();
            ClearOnlinePatrolList();
            ClearPatrolPeople();
            patrolListView.ViewAll.Content = "查看全部";
        }
        private void CreatePeoplePosition(OnlinePatrolman onePerson)
        {
            
            string geom = onePerson.geom;
            if (geom != "")
            {
                IGeometry pos = GviMap.GeoFactory.CreateFromWKT(geom) as IGeometry;
                pos.SpatialCRS = GviMap.SpatialCrs;
                IPoint point = pos as IPoint;
                var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                poi.SetPostion(point.X, point.Y, 2);
                poi.Size = 30;
                poi.ShowName = true;
                poi.MaxVisibleDistance = 5000;
                poi.MinVisibleDistance = 100;
                poi.Name = onePerson.name;
                poi.SpatialCRS = GviMap.SpatialCrs;
                poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", "保安Warning");//Helpers.ResourceHelper.FindResourceByKey("userImg").ToString();//
                IRenderPOI rpoi = GviMap.ObjectManager.CreateRenderPOI(poi);
                rpoi.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                //rpoi.Highlight(Color.Red);
                // GviMap.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                //guidList.Add(rpoi.Guid);

                OnlinePeopleList.Add(onePerson.id, rpoi.Guid);
                var table = createTableLable(point, onePerson.name, onePerson.phone);
                table.SetRecord(0, 1, onePerson.phone);
                //table.SetRecord(1, 1, onePerson.department_name);
                if (onePerson.department_name.Length > 6)
                {
                    onePerson.department_name = onePerson.department_name.Substring(0, 6) + "...";
                    table.SetRecord(1, 1, onePerson.department_name);
                }
                else
                {
                    table.SetRecord(1, 1, onePerson.department_name);
                }
                table.VisibleMask = gviViewportMask.gviViewNone;//gviViewAllNormalView;

                OnlineTableList.Add(onePerson.id, table.Guid);
            }
            else
            {
                Messages.ShowMessage("尚未查询到该人员位置信息，有人员离线请刷新表单！");
            }                   
        }
        private void CreatePeoplePositionByArea(OnlinePatrolman onePerson)
        {

            string geom = onePerson.geom;
            if (geom != "")
            {
                IGeometry pos = GviMap.GeoFactory.CreateFromWKT(geom) as IGeometry;
                pos.SpatialCRS = GviMap.SpatialCrs;
                IPoint point = pos as IPoint;
                var poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
                poi.SetPostion(point.X, point.Y, 2);
                poi.Size = 30;
                poi.ShowName = true;
                poi.MaxVisibleDistance = 5000;
                poi.MinVisibleDistance = 100;
                poi.Name = onePerson.name;
                poi.SpatialCRS = GviMap.SpatialCrs;
                poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", "保安Warning");//Helpers.ResourceHelper.FindResourceByKey("userImg").ToString();//
                IRenderPOI rpoi = GviMap.ObjectManager.CreateRenderPOI(poi);
                rpoi.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                //rpoi.Highlight(Color.Red);
                // GviMap.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                //guidList.Add(rpoi.Guid);

                OnlinePeopleListByArea.Add(onePerson.id, rpoi.Guid);
                var table = createTableLable(point, onePerson.name, onePerson.phone);
                table.SetRecord(0, 1, onePerson.phone);
                //table.SetRecord(1, 1, onePerson.department_name);
                if (onePerson.department_name.Length > 6)
                {
                    onePerson.department_name = onePerson.department_name.Substring(0, 6) + "...";
                    table.SetRecord(1, 1, onePerson.department_name);
                }
                else
                {
                    table.SetRecord(1, 1, onePerson.department_name);
                }
                table.VisibleMask = gviViewportMask.gviViewNone;

                OnlineTableListByArea.Add(onePerson.id, table.Guid);
            }
            else
            {
                Messages.ShowMessage("尚未查询到该人员位置信息！");
            }
        }
        private ITableLabel createTableLable(IPoint pt, string _name, string _phone)
        {
            var tableLabel = TableLabelFactory.CreateWindTable(GviMap.ObjectManager, 2, 2);
            pt.Z = 2;
            tableLabel.Position = pt;
            //tableLabel.Position.Z = 2;            
            tableLabel.VisibleMask = gviViewportMask.gviViewAllNormalView;
            tableLabel.TitleText = _name;
            tableLabel.SetColumnWidth(0, 45);
            tableLabel.SetColumnWidth(1, 110);
            // 设定表格中第1行，第1列的显示文字
            tableLabel.SetRecord(0, 0, "电话:");
            // 第1行，第2列
            tableLabel.SetRecord(1, 0, "部门:");

            return tableLabel;
        }
        public void ClearOnlinePatrolList()
        {
            if (OnlinePeopleList?.Count != 0)
            {
                foreach (var item in OnlinePeopleList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            if (OnlineTableList?.Count != 0)
            {
                foreach (var item in OnlineTableList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            OnlinePeopleList = new Dictionary<string, Guid>();
            OnlineTableList = new Dictionary<string, Guid>();
        }
        private void SearchOnlinePatrolByArea(int areaNum)
        {
            ClearOnlinePatrolList();
            ClearOnlinePatrolListByArea();
            List<OnlinePatrolman> allOnlinePersonListByArea = new List<OnlinePatrolman>();
            try
            {
                string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.OnlinePatrolByAreaInf+ "?area_id="+ areaNum);
                allOnlinePersonListByArea = JsonUtil.DeserializeFromString<List<OnlinePatrolman>>(resStr);
                if (allOnlinePersonListByArea?.Count != 0)
                {
                    foreach (var item in allOnlinePersonListByArea)
                    {
                        CreatePeoplePositionByArea(item);
                    }
                }

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
        public void ClearOnlinePatrolListByArea()
        {
            if (OnlinePeopleListByArea?.Count != 0)
            {
                foreach (var item in OnlinePeopleListByArea)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            if (OnlineTableListByArea?.Count != 0)
            {
                foreach (var item in OnlineTableListByArea)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            OnlinePeopleListByArea = new Dictionary<string, Guid>();
            OnlineTableListByArea = new Dictionary<string, Guid>();
        }

    }
}
