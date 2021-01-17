using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Enum;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Models.RoutePlanning;
using Mmc.Mspace.RoutePlanning.Models;
using Mmc.Mspace.RoutePlanning.Utils;
using Mmc.Mspace.RoutePlanning.Views;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Framework.Commands;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.RoutePlanning.ViewModels
{
    public class RoutePlanningViewModel : CheckedToolItemModel
    {
        List<IPoint> pointList;
        GeometryFactory gfactory = new GeometryFactory();
        IPoint point = null;
        public FlySimulate flySimulate = new FlySimulate();
        private RoutePlanningView _newRoutePlanView;
        public delegate void DelFlyObjectDelegate();
        
        public override void Initialize()
        {
            base.Initialize();
            this.SearchDetailCmd = new Wpf.Commands.RelayCommand<RoutePlanModel>((routePlanModel) =>OnSearchDetail(routePlanModel));
            this.FlightSimulateCmd= new Wpf.Commands.RelayCommand<RoutePlanModel>((routePlanModel) => OnFlightSimulate(routePlanModel));
            this.DeleteItemCmd = new Wpf.Commands.RelayCommand<RoutePlanModel>((routePlanModel) => OnDeleteItem(routePlanModel));
            this.AirDeclarationCommand = new Wpf.Commands.RelayCommand<RoutePlanModel>((routePlanModel) => OnAirDeclarationCommand(routePlanModel));

            this.BackToListCmd = new Wpf.Commands.RelayCommand(OnBackToList);
            this.ReleaseWindowCmd = new Wpf.Commands.RelayCommand(OnReleaseWindow);
            this.SearchRouteCmd = new Wpf.Commands.RelayCommand(OnSearchRoute);
            this.LastPageCmd = new Wpf.Commands.RelayCommand(OnLastPage);
            this.NextPageCmd = new Wpf.Commands.RelayCommand(OnNextPage);
            this.EndPageCmd = new Wpf.Commands.RelayCommand(OnEndPage);
            this.FirstPageCmd = new Wpf.Commands.RelayCommand(OnFirstPage);
            this.OverLookingCmd= new Wpf.Commands.RelayCommand<RoutePlanModel>((routePlanModel) => OnOverLooking(routePlanModel));
            RoutePlanHelper.Instance.RoutePlanCount += OnRoutePlanCount;
           
        }
        private System.Guid rootId = new System.Guid();

        private void OnOverLooking(RoutePlanModel routePlanModel)
        {

            rootId = GviMap.ObjectManager.GetProjectTree().RootID;
            List<IPoint> pointLineList = new List<IPoint>();
            if (routePlanModel != null)
            {
                var resDyn = JsonUtil.DeserializeFromString<dynamic>(routePlanModel.RouteCourseJson);
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
                    pointLineList.Add(point);

                }
            }
            GviMap.TraceLinePolyManager.Clear();

            if (gfactory == null)
                gfactory = new GeometryFactory();

         var pointLine = (IPolyline)gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            pointLine.SpatialCRS = GviMap.SpatialCrs;

            for (int i = 0; i < pointLineList.Count; i++)
            {
                var point = pointLineList[i];

                pointLine.AppendPoint(point);
            }
          var  lineSymbol = new CurveSymbol();      
            lineSymbol.Width = 0;
            var rpolyline = GviMap.ObjectManager.CreateRenderPolyline(pointLine, lineSymbol, rootId);
            var label = GviMap.ObjectManager.CreateLabel(pointLine.Midpoint);
            GviMap.TraceLinePolyManager.AddPoi(rpolyline.Guid.ToString(), 2, label, rpolyline);
            GviMap.Camera.LookAtEnvelope(rpolyline.Envelope);
        }



        private void OnFlightSimulate(RoutePlanModel routePlanModel)
        {
            OnFlightSimulating(routePlanModel.RouteID, routePlanModel.RouteCourseJson);
        }
        private void OnFlightSimulating(string id, string polylineStr)
        {
            pointList = new List<IPoint>();
            flySimulate?.HideParameterView();
            DelFlyObject();
            _newRoutePlanView.Hide();
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

        }

        public void DelFlyObject()
        {
            flySimulate.RemoveFlight();
        }

        private void OnFirstPage()
        {
            try
            {
                PageNum = 1;
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnEndPage()
        {
            try
            {
                PageNum = PageCount;
                if (PageCount == 0)
                {
                    PageNum = 1;
                }
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnNextPage()
        {
            try
            {
                PageNum ++;
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnLastPage()
        {
            try
            {
                PageNum --;
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        private void OnSortRoutePlan(int OrderBy, string OrderName)
        {
            try
            {
                PageNum = 1;
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, OrderName, SearchRouteCondition, pageSize, PageNum);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnSearchRoute()
        {
            try
            {
                var RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(1,"id",SearchRouteCondition,pageSize, 1);
                RoutePlanCollection.Clear();
                foreach (var item in RoutePlanDataList)
                {
                    RoutePlanCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }



        private void OnBackToList()
        {
            try
            {

            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 空中申报
        /// </summary>
        /// <param name="routePlanModel"></param>
        private void OnAirDeclarationCommand(RoutePlanModel routePlanModel)
        {
            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("RoutePlan_MesTip"), "是否确定空域申报" + routePlanModel.RouteName + "?"))
            {
                Messages.ShowMessage("已提交申报，等待申报审核！");
            }
        }
        private void OnDeleteItem(RoutePlanModel routePlanModel)
        {
            try
            {
                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("RoutePlan_MesTip"), Helpers.ResourceHelper.FindKey("RoutePlan_ConfirmDelete") +routePlanModel.RouteName + "?"))
                {
                    var result=RoutePlanHelper.Instance.DeleteRoutePlan(routePlanModel.RouteID);
                    if (result)
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Deletesuccess"));
                    }
                    else
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Deletefailed"));
                    }


                    if (PageCount == 0)
                    {
                        PageNum = 1;
                    }
                    List<RoutePlanModel> RoutePlanDataList = new List<RoutePlanModel>();
                    RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                    if(RoutePlanDataList.Count==0&& PageNum>1&&PageNum>=PageCount)
                    {
                        PageNum--;
                         RoutePlanDataList = RoutePlanHelper.Instance.GetRoutePlanList(OrderBy, "id", SearchRouteCondition, pageSize, PageNum);
                    }
                    RoutePlanCollection.Clear();
                    foreach (var item in RoutePlanDataList)
                    {
                        RoutePlanCollection.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnSearchDetail(RoutePlanModel routePlanModel)
        {
            try
            {

                if (_routePlanDetailVModel == null)
                {
                    _routePlanDetailVModel = new RoutePlanDetailVModel();
                }

                _routePlanDetailVModel.ShowView(routePlanModel);

            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void ShowWindow(bool STATUE)
        {
            try
            {
                if (_newRoutePlanView == null)
                {
                    _newRoutePlanView = new RoutePlanningView();
                    _newRoutePlanView.Closed += (sender, e) => { _newRoutePlanView = null; };
                }
                this.ShowDeclare = STATUE;
                _newRoutePlanView.DataContext = this;
                _newRoutePlanView.Owner = Application.Current.MainWindow;
                _newRoutePlanView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                IsSelected = true;
                _newRoutePlanView.Show();
                
                OnGetRoutePlanData();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnRoutePlanCount(int obj)
        {
            PageCount = (obj % pageSize) > 0 ? (obj / pageSize) + 1 : obj / pageSize;
            if (PageNum == PageCount || obj == 0)
            {
                NextPageBtnEnable = false;
            }
            else
            {
                NextPageBtnEnable = true;
            }
        }

        private void OnGetRoutePlanData()
        {
            try
            {
                OnFirstPage();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }


        public void OnReleaseWindow()
        {
            try
            {
                this.OnUnchecked();
                flySimulate?.HideParameterView();
                flySimulate?.RemoveFlight();
                if (_newRoutePlanView != null)
                {
                    _newRoutePlanView.Close();
                    _newRoutePlanView = null;
                    if(CloseRoutePlanning!=null)
                    {
                        CloseRoutePlanning("close");
                    }
                    if (_routePlanDetailVModel != null)
                    {
                        _routePlanDetailVModel.OnCloseDetail();
                    }
                }
                Console.WriteLine("-----------CloseWindow");
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                IsSelected = false;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                IsSelected = true;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }


        #region 属性、命令


        public int pageSize = 8;
        public RoutePlanDetailVModel _routePlanDetailVModel;

        public Action<string> CloseRoutePlanning;

        public ICommand SearchDetailCmd { get; set; }

        public ICommand DeleteItemCmd { get; set; }
        public ICommand AirDeclarationCommand { get; set; }

        public ICommand BackToListCmd { get; set; }

        public ICommand FlightSimulateCmd { get; set; }


        public ICommand ReleaseWindowCmd { get; set; }

        public ICommand SearchRouteCmd { get; set; }

        public ICommand LastPageCmd { get; set; }
        public ICommand NextPageCmd { get; set; }
        public ICommand EndPageCmd { get; set; }
        public ICommand FirstPageCmd { get; set; }

        public ICommand OverLookingCmd { get; set; }
        

        private ObservableCollection<RoutePlanModel> _routePlanCollection = new ObservableCollection<RoutePlanModel>();
        public ObservableCollection<RoutePlanModel> RoutePlanCollection
        {
            get { return _routePlanCollection; }
            set
            {
                _routePlanCollection = value;
                NotifyPropertyChanged("RoutePlanCollection");
            }
        }

        private bool _showDeclare=false;

        public bool ShowDeclare
        {
            get { return _showDeclare; }
            set { _showDeclare = value; NotifyPropertyChanged("ShowDeclare"); }
        }


        private string _searchRouteCondition;
        public string SearchRouteCondition
        {
            get { return _searchRouteCondition; }
            set
            {
                _searchRouteCondition = value;
                NotifyPropertyChanged("SearchRouteCondition");
            }
        }

        private int _pageCount=1;
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;

                NotifyPropertyChanged("PageCount");
            }
        }

        private int _pageNum = 1;
        public int PageNum
        {
            get { return _pageNum; }
            set
            {
                _pageNum = value;
                

                if (PageNum == 1)
                {
                    LastPageBtnEnable = false;
                }
                if (PageNum > 1)
                {
                    LastPageBtnEnable = true;
                }
                if (PageNum == PageCount)
                {
                    NextPageBtnEnable = false;
                }
                NotifyPropertyChanged("PageNum");
            }
        }

        private bool _lastPageBtnEnable;
        public bool LastPageBtnEnable
        {
            get { return _lastPageBtnEnable; }
            set { _lastPageBtnEnable = value; NotifyPropertyChanged("LastPageBtnEnable"); }
        }

        private bool _nextPageBtnEnable = true;
        public bool NextPageBtnEnable
        {
            get { return _nextPageBtnEnable; }
            set { _nextPageBtnEnable = value; NotifyPropertyChanged("NextPageBtnEnable"); }
        }

        private int _orderBy = 0;
        public int OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; NotifyPropertyChanged("OrderBy"); }
        }
        private string _orderName ;
        public string OrderName
        {
            get { return _orderName; }
            set { _orderName = value; NotifyPropertyChanged("OrderName"); }
        }

        #region 升降序图标显示相关属性
        private bool _iDIsChecked = true;
        public bool IDIsChecked
        {
            get { return _iDIsChecked; }
            set
            { _iDIsChecked = value;
                SortIconToCollapsed();
                if (_iDIsChecked)
                {
                    IDUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "id");
                }
                else
                {
                    IDDownVisibility = Visibility.Visible  ;
                    OnSortRoutePlan(0, "id");
                }
                NotifyPropertyChanged("IDIsChecked");
            }
        }
        private Visibility _iDDownVisibility = Visibility.Collapsed;
        public Visibility IDDownVisibility
        {
            get { return _iDDownVisibility; }
            set { _iDDownVisibility = value; NotifyPropertyChanged("IDDownVisibility"); }
        }

        private Visibility _iDUpVisibility = Visibility.Collapsed;
        public Visibility IDUpVisibility
        {
            get { return _iDUpVisibility; }
            set { _iDUpVisibility = value; NotifyPropertyChanged("IDUpVisibility"); }
        }

      

        private bool _nameIsChecked = true;
        public bool NameIsChecked
        {
            get { return _nameIsChecked; }
            set
            {
                _nameIsChecked = value;
                SortIconToCollapsed();
                if (_nameIsChecked)
                {
                    NameUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "name");
                }
                else
                {
                    NameDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "name");
                }
                NotifyPropertyChanged("NameIsChecked");
            }
        }
        private Visibility _nameDownVisibility = Visibility.Collapsed;
        public Visibility NameDownVisibility
        {
            get { return _nameDownVisibility; }
            set { _nameDownVisibility = value; NotifyPropertyChanged("NameDownVisibility"); }
        }

        private Visibility _nameUpVisibility = Visibility.Collapsed;
        public Visibility NameUpVisibility
        {
            get { return _nameUpVisibility; }
            set { _nameUpVisibility = value; NotifyPropertyChanged("NameUpVisibility"); }
        }

        private bool _timeSavedIsChecked = true;
        public bool TimeSavedIsChecked
        {
            get { return _timeSavedIsChecked; }
            set
            {
                _timeSavedIsChecked = value;
                SortIconToCollapsed();
                if (_timeSavedIsChecked)
                {
                    TimeSavedUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "addtime");
                }
                else
                {
                    TimeSavedDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "addtime");
                }
                NotifyPropertyChanged("TimeSavedIsChecked");
            }
        }
        private Visibility _timeSavedDownVisibility = Visibility.Collapsed;
        public Visibility TimeSavedDownVisibility
        {
            get { return _timeSavedDownVisibility; }
            set { _timeSavedDownVisibility = value; NotifyPropertyChanged("TimeSavedDownVisibility"); }
        }

        private Visibility _timeSavedUpVisibility = Visibility.Collapsed;
        public Visibility TimeSavedUpVisibility
        {
            get { return _timeSavedUpVisibility; }
            set { _timeSavedUpVisibility = value; NotifyPropertyChanged("TimeSavedUpVisibility"); }
        }

        private bool _routeTypeIsChecked = true;
        public bool RouteTypeIsChecked
        {
            get { return _routeTypeIsChecked; }
            set
            {
                _routeTypeIsChecked = value;
                SortIconToCollapsed();
                if (_routeTypeIsChecked)
                {
                    RouteTypeUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "voyage_type");
                }
                else
                {
                    RouteTypeDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "voyage_type");
                }
                NotifyPropertyChanged("RouteTypeIsChecked");
            }
        }
        private Visibility _routeTypeDownVisibility = Visibility.Collapsed;
        public Visibility RouteTypeDownVisibility
        {
            get { return _routeTypeDownVisibility; }
            set { _routeTypeDownVisibility = value; NotifyPropertyChanged("RouteTypeDownVisibility"); }
        }

        private Visibility _routeTypeUpVisibility = Visibility.Collapsed;
        public Visibility RouteTypeUpVisibility
        {
            get { return _routeTypeUpVisibility; }
            set { _routeTypeUpVisibility = value; NotifyPropertyChanged("RouteTypeUpVisibility"); }
        }
      

        private bool _pointNumIsChecked = true;
        public bool PointNumIsChecked
        {
            get { return _pointNumIsChecked; }
            set
            {
                _pointNumIsChecked = value;
                SortIconToCollapsed();
                if (_pointNumIsChecked)
                {
                    PointNumUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "voyage_point_num");
                }
                else
                {
                    PointNumDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "voyage_point_num");
                }
                NotifyPropertyChanged("PointNumIsChecked");
            }
        }
        private Visibility _pointNumDownVisibility = Visibility.Collapsed;
        public Visibility PointNumDownVisibility
        {
            get { return _pointNumDownVisibility; }
            set { _pointNumDownVisibility = value; NotifyPropertyChanged("PointNumDownVisibility"); }
        }

        private Visibility _pointNumUpVisibility = Visibility.Collapsed;
        public Visibility PointNumUpVisibility
        {
            get { return _pointNumUpVisibility; }
            set { _pointNumUpVisibility = value; NotifyPropertyChanged("PointNumUpVisibility"); }
        }

        private bool _workingAreaIsChecked = true;
        public bool WorkingAreaIsChecked
        {
            get { return _workingAreaIsChecked; }
            set
            {
                _workingAreaIsChecked = value;
                SortIconToCollapsed();
                if (_workingAreaIsChecked)
                {
                    WorkingAreaUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "area");
                }
                else
                {
                    WorkingAreaDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "area");
                }
                NotifyPropertyChanged("WorkingAreaIsChecked");
            }
        }
        private Visibility _workingAreaDownVisibility = Visibility.Collapsed;
        public Visibility WorkingAreaDownVisibility
        {
            get { return _workingAreaDownVisibility; }
            set { _workingAreaDownVisibility = value; NotifyPropertyChanged("WorkingAreaDownVisibility"); }
        }

        private Visibility _workingAreaUpVisibility = Visibility.Collapsed;
        public Visibility WorkingAreaUpVisibility
        {
            get { return _workingAreaUpVisibility; }
            set { _workingAreaUpVisibility = value; NotifyPropertyChanged("WorkingAreaUpVisibility"); }
        }

        private bool _estimatedTimeIsChecked = true;
        public bool EstimatedTimeIsChecked
        {
            get { return _estimatedTimeIsChecked; }
            set
            {
                _estimatedTimeIsChecked = value;
                SortIconToCollapsed();
                if (_estimatedTimeIsChecked)
                {
                    EstimatedTimeUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "voyage_time");
                }
                else
                {
                    EstimatedTimeDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "voyage_time");
                }
                NotifyPropertyChanged("EstimatedTimeIsChecked");
            }
        }
        private Visibility _estimatedTimeDownVisibility = Visibility.Collapsed;
        public Visibility EstimatedTimeDownVisibility
        {
            get { return _estimatedTimeDownVisibility; }
            set { _estimatedTimeDownVisibility = value; NotifyPropertyChanged("EstimatedTimeDownVisibility"); }
        }

        private Visibility _estimatedTimeUpVisibility = Visibility.Collapsed;
        public Visibility EstimatedTimeUpVisibility
        {
            get { return _estimatedTimeUpVisibility; }
            set { _estimatedTimeUpVisibility = value; NotifyPropertyChanged("EstimatedTimeUpVisibility"); }
        }

        private bool _estimatedRangeIsChecked = true;
        public bool EstimatedRangeIsChecked
        {
            get { return _estimatedRangeIsChecked; }
            set
            {
                _estimatedRangeIsChecked = value;
                SortIconToCollapsed();
                if (_estimatedRangeIsChecked)
                {
                    EstimatedRangeUpVisibility = Visibility.Visible;
                    OnSortRoutePlan(1, "voyage");
                }
                else
                {
                    EstimatedRangeDownVisibility = Visibility.Visible;
                    OnSortRoutePlan(0, "voyage");
                }
                NotifyPropertyChanged("EstimatedRangeIsChecked");
            }
        }
        private Visibility _estimatedRangeDownVisibility = Visibility.Collapsed;
        public Visibility EstimatedRangeDownVisibility
        {
            get { return _estimatedRangeDownVisibility; }
            set { _estimatedRangeDownVisibility = value; NotifyPropertyChanged("EstimatedRangeDownVisibility"); }
        }

        private Visibility _estimatedRangeUpVisibility = Visibility.Collapsed;
        public Visibility EstimatedRangeUpVisibility
        {
            get { return _estimatedRangeUpVisibility; }
            set { _estimatedRangeUpVisibility = value; NotifyPropertyChanged("EstimatedRangeUpVisibility"); }
        }


        private void SortIconToCollapsed()
        {
            IDDownVisibility = Visibility.Collapsed;
            IDUpVisibility = Visibility.Collapsed;
            NameDownVisibility = Visibility.Collapsed;
            NameUpVisibility = Visibility.Collapsed;
            TimeSavedDownVisibility = Visibility.Collapsed;
            TimeSavedUpVisibility = Visibility.Collapsed;
            RouteTypeUpVisibility = Visibility.Collapsed;
            RouteTypeDownVisibility = Visibility.Collapsed;
            PointNumUpVisibility = Visibility.Collapsed;
            PointNumDownVisibility = Visibility.Collapsed;
            WorkingAreaUpVisibility = Visibility.Collapsed;
            WorkingAreaDownVisibility = Visibility.Collapsed;
            EstimatedTimeDownVisibility = Visibility.Collapsed;
            EstimatedTimeUpVisibility = Visibility.Collapsed;
            EstimatedRangeDownVisibility = Visibility.Collapsed;
            EstimatedRangeUpVisibility = Visibility.Collapsed;

        }
    #endregion

    #endregion
}
}
