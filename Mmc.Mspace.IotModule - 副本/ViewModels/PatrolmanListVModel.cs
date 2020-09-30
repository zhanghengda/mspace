using FireControlModule.FireIot;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.IotModule.Dto;
using Mmc.Mspace.IotModule.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Converter;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class PatrolmanListVModel : CheckedToolItemModel
    {
        private HttpService _httpService;
        private string _poiHost;
        [XmlIgnore]
        private PatrolRecordVModel _patrolRecordVModel;
        private Thread _patrolThread;
        private bool _isTreadOpen;
        private string selectedAreaId;
        private string _selectAreaKey;
        private readonly string TAG = "Patrolman";
        private Dictionary<string, IRenderPolyline> currentRPolyline;
        private Dictionary<string, System.Drawing.Color> _personalColor;
        private Dictionary<string, ICurveSymbol> _personSymbol;
        private Dictionary<string, List<IPoint>> _patrolmanPointsList;
        private Dictionary<string, ISurfaceSymbol> _groupGridSymbol;
        private string _workStatus;
        private Dictionary<string, PatrolmanDataForRender> _patrolmanList;
        private Dictionary<string, GridInfo> _gridDic;
        private ObservableCollection<PatrolmanForClient> _patrolmanSet;
        PatrolListVModel patrolListVModel = new PatrolListVModel();
        EventTimeVModel eventTimeVModel = new EventTimeVModel();
        EventTypeVModel eventTypeVModel = new EventTypeVModel();
        //List<Guid>  = new List<Guid>();
        Dictionary<string, Guid> peopleList = new Dictionary<string, Guid>();
        Dictionary<string, Guid> guidList = new Dictionary<string, Guid>();


        MeshView meshView = new MeshView();
        string areaID = "";
        private ItoModelCovernter _itoModelCovernter;
        [XmlIgnore]
        public ObservableCollection<PatrolmanForClient> PatrolmanSet
        {
            get { return _patrolmanSet ?? (_patrolmanSet = new ObservableCollection<PatrolmanForClient>()); }
            set { _patrolmanSet = value; NotifyPropertyChanged("PatrolmanSet"); }
        }
        private Visibility _patrolManVisible;
        public Visibility PatrolManVisible
        {
            get { return _patrolManVisible; }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this._patrolManVisible, value, "PatrolVisible");
            }
        }
        private string _patrolmanTableTitle;
        [XmlIgnore]
        public string PatrolmanTableTitle
        {
            get { return _patrolmanTableTitle; }
            set { _patrolmanTableTitle = value; NotifyPropertyChanged("PatrolmanTableTitle"); }
        }
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }
        public ICommand QueryPositionCmd { get; set; }
        public ICommand QueryRouteCmd { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand FoldCmd { get; set; }
        

        private RelayCommand<MouseButtonEventArgs> _buttonDownCommand;
        [XmlIgnore]
        public RelayCommand<MouseButtonEventArgs> ButtonDownCommand
        {
            get { return _buttonDownCommand ?? (_buttonDownCommand = new RelayCommand<MouseButtonEventArgs>(OnButtonDownCommand)); }
            set { _buttonDownCommand = value; }
        }
        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; NotifyPropertyChanged("SearchText"); }
        }

        private async void OnButtonDownCommand(MouseButtonEventArgs obj)
        {
            if (obj == null) return;

            PatrolmanForClient Model = (obj.Source as FrameworkElement).DataContext as PatrolmanForClient;
            if (Model == null) return;

            if (_patrolRecordVModel != null)
            {
                _patrolRecordVModel.CloseView();
                _patrolRecordVModel = null;
            }
            _patrolRecordVModel = new PatrolRecordVModel();
            if (PatrolmanSet.Count > 0)
            {
                PatrolmanSet.ToList().ForEach(p => p.IsSelected = false);
                PatrolmanSet.ToList().Find(p => p.Phone == Model.Phone).IsSelected = true;
            }

            if (Model.Status == _workStatus) FlyToPersonalRender(Model.Phone);

            string MonthNow = DateTime.Now.ToString("yyyy-MM");

            await Task.Run(() =>
            {
                var historyDataList = GetPatrolmanRecordDatetime(Model.Phone, MonthNow);
                CalerdarSelectedDateCoverter.Update(historyDataList);
            });

            _patrolRecordVModel.currentPerson = Model;
            _patrolRecordVModel.DrawingPersonalHistoryTrace -= OnDrawingPersonalTrace;
            _patrolRecordVModel.DrawingPersonalHistoryTrace += OnDrawingPersonalTrace;
            _patrolRecordVModel.GetPersonalMonthRecord -= GetPatrolmanRecordDatetime;
            _patrolRecordVModel.GetPersonalMonthRecord += GetPatrolmanRecordDatetime;
            _patrolRecordVModel.StartThreadOfPatrol -= OnStartThreadOfPatrol;
            _patrolRecordVModel.StartThreadOfPatrol += OnStartThreadOfPatrol;

            _patrolRecordVModel.ShowView();
            _isTreadOpen = false;
        }

        private void OnStartThreadOfPatrol()
        {
            _isTreadOpen = true;
            _patrolThread = new Thread(RefleshPatrolmanTraceData);
            _patrolThread.Start();
        }

        private void OnDrawingPersonalTrace(PatrolmanForClient patrolData, string dateStr)
        {

            if (string.IsNullOrEmpty(dateStr)) return;

            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            Patrolman person = new Patrolman();
            Task.Run(() =>
            {
                person = PersonalRecordData(patrolData.Phone, dateStr);

                shell.Dispatcher.InvokeAsync(() =>
                {
                    ClearRender();
                    _patrolmanList.Clear();
                    PatrolmanDataForRender personalData = _itoModelCovernter.PatrolmanRenderConvert(person);
                    if (patrolData.Status == _workStatus && DateTime.Now.ToString("yyyy-MM-dd") == dateStr)
                    {
                        RenderPatrolmanPoints(personalData, 0, true, false);
                    }
                    else
                    {
                        RenderPatrolmanPoints(personalData);
                    }


                    RenderPatrolmanTraceEx(personalData);

                    FlyToPersonalRender(patrolData.Phone + 0);
                });
            });
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;

            _httpService = new HttpService();
            _httpService.Token = HttpServiceUtil.Token;
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _poiHost = json.poiUrl;
            this.CloseCmd = new RelayCommand(() =>
            {
                //base.IsChecked = false;
                ClearRender();
                HideView();
                ClearPatrolList();
                patrolListVModel.UnCheckAllGrid();
                patrolListVModel.CancleSelectAll();
            });
            this.QueryPositionCmd = new RelayCommand<object>((obj) => GetSinglePosition(obj));
            this.QueryRouteCmd = new RelayCommand<object>((obj) => GetSingleRoute(obj));
            this.SearchCommand = new RelayCommand(OnSearch);
            this.FoldCmd = new RelayCommand(Onfold);
            currentRPolyline = new Dictionary<string, IRenderPolyline>();
            PatrolmanSet = new ObservableCollection<PatrolmanForClient>();

            _workStatus = Helpers.ResourceHelper.FindKey("Online");
            _personalColor = new Dictionary<string, System.Drawing.Color>();
            _personSymbol = new Dictionary<string, ICurveSymbol>();
            _patrolmanPointsList = new Dictionary<string, List<IPoint>>();

            _gridDic = new Dictionary<string, GridInfo>();
            _groupGridSymbol = new Dictionary<string, ISurfaceSymbol>();
            _patrolmanList = new Dictionary<string, PatrolmanDataForRender>();
            _itoModelCovernter = new ItoModelCovernter();

            Messenger.Messengers.Register<bool>("unpatrol", (r) =>
            {
                base.IsChecked = false;
            });
        }
        public void ClearPatrolList()
        {
            if (guidList?.Count != 0)
            {
                foreach (var item in guidList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            if(peopleList?.Count!=0)
            {
                foreach(var item in peopleList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            guidList = new Dictionary<string, Guid>();
            peopleList = new Dictionary<string, Guid>();
        }
        private async void RefleshPatrolmanTraceData()
        {
            List<PatrolmanDataForRender> patrolList = new List<PatrolmanDataForRender>();
            while (true)
            {
                if (!_isTreadOpen) break;

                Thread.Sleep(60000); // 1分钟刷新一次
                patrolList.Clear();
                ClearPatrolList();
                if (string.IsNullOrEmpty(selectedAreaId)) continue;
                List<Patrolman> patrolmanListofArea = new List<Patrolman>();

                await Task.Run(() =>
                {
                    patrolmanListofArea = GetPatrolmanListOfArea(selectedAreaId);
                });

                if (patrolmanListofArea.Count > 0)
                {
                    foreach (var item in patrolmanListofArea)
                    {
                        patrolList.Add(_itoModelCovernter.PatrolmanRenderConvert(item));
                    }
                }

                ReDrawingPatrolmanTrace(patrolList, _selectAreaKey);
            }
        }

        public override FrameworkElement CreatedView()
        {
            return new PatrolmanListView() { Owner = Application.Current.MainWindow };
        }

        public override void OnChecked()
        {
            base.OnChecked();
            eventTimeVModel.eventRecordVModel.GetEventreport(DateTime.Now.Date.ToString());
            eventTimeVModel.ShowEventTimeView();
            DrawingGridOfCerrentRole();
            MapSelectEventManager(true);
            eventTypeVModel.ShowEventTypeView();
            eventTypeVModel.OnDfeipai -= ShowDfeipai;
            eventTypeVModel.OnDfeipai += ShowDfeipai;
            eventTypeVModel.OnDshenhe -= ShowDshenhe;
            eventTypeVModel.OnDshenhe += ShowDshenhe;
            eventTypeVModel.OnDshoulie -= ShowDshoulie;
            eventTypeVModel.OnDshoulie += ShowDshoulie;
            eventTypeVModel.OnDbanjie -= ShowOnDbanjie;
            eventTypeVModel.OnDbanjie += ShowOnDbanjie;
            eventTypeVModel.OnDwanjie -= ShowOnDwanjie;
            eventTypeVModel.OnDwanjie += ShowOnDwanjie;
            eventTypeVModel.OnDYguidang -= ShowOnDYguidang;
            eventTypeVModel.OnDYguidang += ShowOnDYguidang;
            eventTypeVModel.OnShowAll -= ShowAll;
            eventTypeVModel.OnShowAll += ShowAll;
            Messenger.Messengers.Notify("undeviceview", false);
            Messenger.Messengers.Notify("unAdministrative", false);
            Messenger.Messengers.Notify("ShowHiddenMenu", true);
            Messenger.Messengers.Notify("HideSummery", false);
        }
        private void ShowDfeipai()
        {           
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "0")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowDshenhe()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "1")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowDshoulie()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "4")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowOnDbanjie()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "5")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowOnDwanjie()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "6")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowOnDYguidang()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {
                if (item.Value != "7")
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewNone;
                }
                else
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

        }
        private void ShowAll()
        {
            foreach (var item in eventTimeVModel.eventRecordVModel.eventStatusguid)
            {                
                if(item.Key!=null)
                {
                    Guid rPoiguid = eventTimeVModel.eventRecordVModel.eventList[item.Key];
                    IRenderPOI rPoi = GviMap.ObjectManager.GetObjectById(rPoiguid) as IRenderPOI;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
                              
            }

        }
        
        private void MapSelectEventManager(bool ison)
        {
            if (ison)
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;

                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectGrid;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelectGrid;
            }
            else
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectGrid;
            }
        }

        private IPolygon _centerPtPolygon = null;//凹多边形前三个点构造多边形
      
        private void DrawingGridOfCerrentRole()
        {
            //patrolListVModel?.PatrolListCollection.Clear();
            patrolListVModel.OpenPatrolView();
            patrolListVModel.ShowPeopleList -= ShowPatrolmanListOfAreaFromList;
            patrolListVModel.ShowPeopleList += ShowPatrolmanListOfAreaFromList;
            patrolListVModel.ClearPatrolPeople -= ClearPatrolList;
            patrolListVModel.ClearPatrolPeople += ClearPatrolList;
            patrolListVModel.HidePatrolmanListView -= HideView;
            patrolListVModel.HidePatrolmanListView += HideView;
            patrolListVModel.ClearRoute -= ClearRender;
            patrolListVModel.ClearRoute += ClearRender;
            Task.Run(() =>
            {
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
                  
                    //patrolListVModel.PatrolListCollection.Add(gridInfo);
                    //foreach (var item in gridList)
                    //{
                    //    patrolListVModel.PatrolListCollection.Add(item);
                    //}
                    IRenderPolygon lastRpolygon = null;
                    var groupCount = gridList.GroupBy(p => p.group).ToList();
                    foreach (var item in gridList)
                    {
                        var polygon = GviMap.GeoFactory.CreatePolygon(item.geom, GviMap.SpatialCrs);
                        polygon = polygon.CreatePolygonWithZ(1);//z偏移防止闪面

                        if (polygon == null) continue;
                        var color = GetRandomColor();
                        ICurveSymbol curveSym = null;
                        ISurfaceSymbol surSym = null;

                        // 当仅有一个分组时，各元素单独颜色符号显示；多分组按分组显示，同组同颜色符号
                        if (item.group == null) item.group = "GroupForNotNull";
                        if (groupCount.Count != 1 && _groupGridSymbol.ContainsKey(item.group)) surSym = _groupGridSymbol[item.group];
                        else
                        {
                            curveSym = GviMap.GridPatrolPolyManager.CreateCurveSymbol(-2f, color);
                            surSym = GviMap.GridPatrolPolyManager.CreateSurfaceSymbol((CurveSymbol)curveSym, System.Drawing.Color.FromArgb(60, color));
                            if (!_groupGridSymbol.ContainsKey(item.group)) _groupGridSymbol.Add(item.group, surSym);
                        }

                        var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, surSym);
                        lastRpolygon = rPolygon;

                        var centerpt = polygon.Centroid;
                        if (!polygon.IsPointOnSurface(centerpt))
                        {
                            //凹多边形前三个点构造多边形
                            var pt0 = polygon.ExteriorRing.GetPoint(0);
                            var pt1 = polygon.ExteriorRing.GetPoint(1);
                            var pt2 = polygon.ExteriorRing.GetPoint(2);

                            if (_centerPtPolygon == null)
                            {
                                var listPt = new List<IPoint>();
                                listPt.Add(pt0);
                                listPt.Add(pt1);
                                listPt.Add(pt2);
                                listPt.Add(pt0);
                                _centerPtPolygon = GviMap.GeoFactory.CreatePolygon(listPt);
                            }
                            else
                            {
                                _centerPtPolygon.ExteriorRing.RemovePoints(0, 3);
                                _centerPtPolygon.ExteriorRing.AppendPoint(pt0);
                                _centerPtPolygon.ExteriorRing.AppendPoint(pt1);
                                _centerPtPolygon.ExteriorRing.AppendPoint(pt2);
                                _centerPtPolygon.ExteriorRing.AppendPoint(pt0);
                            }
                            centerpt = _centerPtPolygon.Centroid;
                            //var label = GviMap.ObjectManager.CreateLabel(polygon.Centroid);
                            //label.MaxVisibleDistance = WebConfig.TracePoiMaxDistance;
                            //label.Text = item.name;

                            //label.SetPosition(label.Position.X - 0.0008, label.Position.Y - 0.0019);
                            //string key = rPolygon.Guid.ToString();
                            //GviMap.GridPatrolPolyManager.AddPoi(key, 3, label, rPolygon);

                            //_gridDic.Add(key, item);
                        }

                        var label = GviMap.ObjectManager.CreateLabel(centerpt);
                        label.MaxVisibleDistance = WebConfig.TracePoiMaxDistance;
                        label.Text = item.name;
                        string key = rPolygon.Guid.ToString();
                        GviMap.GridPatrolPolyManager.AddPoi(key, 3, label, rPolygon);

                        _gridDic.Add(key, item);
                    }

                    if (lastRpolygon != null)
                    {
                        GviMap.Camera.LookAtEnvelope(lastRpolygon.Envelope);
                        //IVector3 vector = lastRpolygon.Envelope as IVector3;
                       
                        GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                        ////GviMap.Camera.FlyToEnvelope(point.Envelope);
                        eulerAngle.Tilt = -45;
                        eulerAngle.Heading = 210;
                        pointCamera.X = lastRpolygon.Envelope.MaxX;
                        pointCamera.Y = lastRpolygon.Envelope.MaxY;
                        pointCamera.Z = 2000;
                        GviMap.Camera.SetCamera2(pointCamera, eulerAngle, 0);
                        
                    }
                   
                }
            });
           
        }

        private void AxMapControl_RcMouseClickSelectGrid(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var pt = IntersectPoint;
            if (pt == null) return;

            if (PickResult == null) return;

            IRenderPolygonPickResult rPolyPk = null;
            try
            {
                if (PickResult is IRenderPOIPickResult)
                {
                    IRenderPOI renderPOI = PickResult as IRenderPOI;
                    IRenderPOIPickResult renderPOIResult = PickResult as IRenderPOIPickResult;
                    Guid guid = renderPOIResult.RenderPOI.Guid;
                    bool peopleExist = peopleList.ContainsValue(guid);
                   
                    var firstKey = patrolListVModel.OnlinePeopleList.FirstOrDefault(q => q.Value == guid).Key;
                    bool allOnlineExist = false;
                    if (firstKey==null)
                    {
                        
                    }
                    else
                    {
                        allOnlineExist = patrolListVModel.OnlineTableList.ContainsKey(firstKey);
                    }
                    
                    if (allOnlineExist)
                    {
                        var table = GviMap.ObjectManager.GetObjectById(patrolListVModel.OnlineTableList[firstKey]) as ITableLabel;
                        //if(table.VisibleMask == gviViewportMask.gviViewAllNormalView)
                        //{
                        //    table.VisibleMask = gviViewportMask.gviViewNone;
                        //}
                         if (table.VisibleMask == gviViewportMask.gviViewNone)
                        {
                            table.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        }

                    }
                
                
                    //bool peopleExist = peopleList.ContainsKey(guid);
                    if (peopleExist)
                    {
                        var keys = peopleList.FirstOrDefault(q => q.Value == guid);
                        string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.SinglePatrolmanInf + "?inspector_id=" + keys.Key);
                        if (resStr != null && resStr != "")
                        {
                            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                            string geom = resDyn.geom;
                            string department_name = resDyn.department_name;
                            string name = resDyn.name;
                            if (name != "" && name != null)
                            {
                                SearchText = name;
                                OnSearch();
                            }
                        }
                    }
                    bool guidByAreaExist = patrolListVModel.OnlinePeopleListByArea.ContainsValue(guid);
                    if (guidByAreaExist)
                    {
                        var keys = patrolListVModel.OnlinePeopleListByArea.FirstOrDefault(q => q.Value == guid);
                        bool tableExist = patrolListVModel.OnlineTableListByArea.ContainsKey(keys.Key);
                        if (tableExist)
                        {

                            ITableLabel table = GviMap.ObjectManager.GetObjectById(patrolListVModel.OnlineTableListByArea[keys.Key]) as ITableLabel;
                            table.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        }
                        
                        Task.Run(() =>
                        {
                            string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.SinglePatrolmanInf + "?inspector_id=" + keys.Key);
                            if (resStr != null && resStr != "")
                            {
                                var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                                string geom = resDyn.geom;
                                string department_name = resDyn.department_name;
                                string name = resDyn.name;
                                if (name != "" && name != null)
                                {
                                    SearchText = name;
                                    OnSearch();
                                }
                            }
                        });
                       
                    }
                    bool eventExist = eventTimeVModel.eventRecordVModel.eventList.ContainsValue(guid);
                    if(eventExist)
                    {
                        var keys = eventTimeVModel.eventRecordVModel.eventList.FirstOrDefault(q => q.Value == guid);
                        bool eventTableExist = eventTimeVModel.eventRecordVModel.eventTableList.ContainsKey(keys.Key);
                        if (eventTableExist)
                        {

                            ITableLabel table = GviMap.ObjectManager.GetObjectById(eventTimeVModel.eventRecordVModel.eventTableList[keys.Key]) as ITableLabel;
                            table.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        }
                    }
                }
                else
                {
                    foreach (var item in patrolListVModel.OnlineTableList)
                    {
                        var table = GviMap.ObjectManager.GetObjectById(item.Value) as ITableLabel;
                        if (table != null)
                        {
                            table.VisibleMask = gviViewportMask.gviViewNone;
                        }
                    }
                    foreach (var item in patrolListVModel.OnlineTableListByArea)
                    {
                        var table = GviMap.ObjectManager.GetObjectById(item.Value) as ITableLabel;
                        if (table != null)
                        {
                            table.VisibleMask = gviViewportMask.gviViewNone;
                        }
                    }
                    foreach (var item in eventTimeVModel.eventRecordVModel.eventTableList)
                    {
                        var table = GviMap.ObjectManager.GetObjectById(item.Value) as ITableLabel;
                        if (table != null)
                        {
                            table.VisibleMask = gviViewportMask.gviViewNone;
                        }
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
            if (PickResult is IRenderPolygonPickResult) { 
            rPolyPk = PickResult as IRenderPolygonPickResult;

            if (rPolyPk == null) return;
                string key = rPolyPk.RenderPolygon.Guid.ToString();
                try
                {
                    
                    ShowPatrolmanListOfArea(key);
                    patrolListVModel.CancleSelectAll();
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

         
            
        }

        public async void ShowPatrolmanListOfArea(string key)
        {
            if (_gridDic != null && _gridDic.Count > 0)
            {
                if (_gridDic.ContainsKey(key))
                {
                    ClearPatrolList();
                    var render = GviMap.GridPatrolPolyManager.GetPoi(key).Item3;
                    GviMap.GridPatrolPolyManager.HightLight((IRenderGeometry)render);

                    selectedAreaId = _gridDic[key].id;
                    _selectAreaKey = key;

                    List<Patrolman> patrolmanListofArea = new List<Patrolman>();
                    patrolListVModel.UnCheckAllGrid(selectedAreaId);
                    await Task.Run(() =>
                     {
                         patrolmanListofArea = GetPatrolmanListOfArea(selectedAreaId);
                     });

                    List<PatrolmanForClient> patrolList = new List<PatrolmanForClient>();
                    List<PatrolmanDataForRender> patrolRenderList = new List<PatrolmanDataForRender>();

                    if (patrolmanListofArea.Count > 0)
                    {
                        for (int i = 0; i < patrolmanListofArea.Count; i++)
                        {
                            patrolList.Add(_itoModelCovernter.PatrolmanConvert(patrolmanListofArea[i]));
                            patrolRenderList.Add(_itoModelCovernter.PatrolmanRenderConvert(patrolmanListofArea[i]));
                        }
                    }

                    PatrolmanSet = new ObservableCollection<PatrolmanForClient>(patrolList);

                    //ReDrawingPatrolmanTrace(patrolRenderList,key);

                    ShowView(_gridDic[key].name, _gridDic[key].id);
                    areaID = _gridDic[key].id;
                    // OnStartThreadOfPatrol();
                }
            }
        }
        public async void ShowPatrolmanListOfAreaFromList(string areaId)
        {
            if (areaId != null && areaId != "")
            {
                GridInfo patrolItem = null;
                foreach (var item in _gridDic.Values.ToList())
                {
                    if (item.id == areaId)
                        patrolItem = item;
                }
                    {
                        List<Patrolman> patrolmanListofArea = new List<Patrolman>();

                        await Task.Run(() =>
                        {
                            ClearPatrolList();
                            patrolmanListofArea = GetPatrolmanListOfArea(areaId);
                        });

                        List<PatrolmanForClient> patrolList = new List<PatrolmanForClient>();
                        List<PatrolmanDataForRender> patrolRenderList = new List<PatrolmanDataForRender>();

                        if (patrolmanListofArea.Count > 0)
                        {
                            for (int i = 0; i < patrolmanListofArea.Count; i++)
                            {
                                patrolList.Add(_itoModelCovernter.PatrolmanConvert(patrolmanListofArea[i]));
                                patrolRenderList.Add(_itoModelCovernter.PatrolmanRenderConvert(patrolmanListofArea[i]));
                            }
                        }

                        PatrolmanSet = new ObservableCollection<PatrolmanForClient>(patrolList);

                        //ReDrawingPatrolmanTrace(patrolRenderList,key);
                        if (patrolItem != null)
                        {
                            ShowView(patrolItem.name, patrolItem.id);
                            areaID = patrolItem.id;
                        }
                        // OnStartThreadOfPatrol();
                    }
                
            }
        }

        private void ClearRender()
        {
            GviMap.PointManager.Clear();
            GviMap.TracePoiManager.Clear();
            GviMap.TraceLinePolyManager.Clear();
        }

        private void ReDrawingPatrolmanTrace(List<PatrolmanDataForRender> patrolmanList, string keyArea)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (patrolmanList == null || patrolmanList.Count <= 0)
            {

                Messages.ShowMessage(string.Format("{0}未分配责任成员", _gridDic[keyArea].name));
            };
            try
            {
                PatrolmanDataForRender patrolmanData = new PatrolmanDataForRender();
                foreach (var item in patrolmanList.ToArray())
                {
                    var man = PatrolmanSet?.ToList().Find(p => p.Phone == item.Phone);
                    if (man == null)
                        continue;
                    string status = man.Status;
                    if (status == _workStatus)
                    {
                        int index = -1;
                        bool isFirstTime = false;
                        if (_patrolmanList.ContainsKey(item.Phone))
                        {
                            index = _patrolmanList[item.Phone].PointsList.Count - 1;
                            int count = item.PointsList.Count;
                            patrolmanData.Name = item.Name;
                            patrolmanData.Phone = item.Phone;
                            patrolmanData.PointsList = item.PointsList.GetRange(index, count - index);
                            patrolmanData.PointsTime = item.PointsTime.GetRange(index, count - index);
                            foreach (var pos in item.StatusLocation)
                            {
                                int newpos = pos - index;
                                if (newpos > 0) patrolmanData.StatusLocation.Add(newpos);
                            }
                        }
                        else
                        {
                            isFirstTime = true;
                            index = item.PointsList.Count - 1;
                            patrolmanData = item;
                            _patrolmanList.Add(item.Phone, item);
                        }

                        //使用更新策略
                        //GviMap.TracePoiManager.DeletePoi(patrolmanData.Phone + index + 1);
                        GviMap.TraceLinePolyManager.DeletePoi(patrolmanData.Phone);

                        RenderPatrolmanPoints(patrolmanData, index: index, isFirst: isFirstTime, isHistroy: false);
                        RenderPatrolmanTraceEx(item);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private List<Patrolman> GetPatrolmanListOfArea(string areaid)
        {
            if (string.IsNullOrEmpty(areaid)) return null;

            List<Patrolman> patrolmenList = new List<Patrolman>();
            string api = string.Format("{0}?area_id={1}", GridPatrolInterface.PatrolmanListOfAreaInf, areaid);
            string resStr = string.Empty;
            try
            {
                resStr = HttpServiceHelper.Instance.GetRequest(api);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

            if (!string.IsNullOrEmpty(resStr))
                patrolmenList = JsonUtil.DeserializeFromString<List<Patrolman>>(resStr);
            return patrolmenList;
        }

        private List<DateTime> GetPatrolmanRecordDatetime(string phone, string month)
        {
            if (string.IsNullOrEmpty(phone)) return null;

            List<DateTime> outList = new List<DateTime>();
            string api = string.Format("{0}?phone={1}", GridPatrolInterface.RecordDateOfPatrolmanInf, phone);
            if (!string.IsNullOrEmpty(month))
                api = string.Format("{0}&ym={1}", api, month);

            string resStr = HttpServiceHelper.Instance.GetRequestAsync(api);

            if (!string.IsNullOrEmpty(resStr))
            {
                var recordDateOfMonth = JsonUtil.DeserializeFromString<List<string>>(resStr);

                if (recordDateOfMonth != null && recordDateOfMonth.Count > 0)
                {
                    foreach (var item in recordDateOfMonth)
                    {
                        string datestr = item.ToString().Replace("-", "");

                        DateTime dt = DateTime.ParseExact(datestr, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        outList.Add(dt);
                    }
                }
            }
            return outList;
        }

        private Patrolman PersonalRecordData(string phone, string datetime)
        {
            if (string.IsNullOrEmpty(phone)) return null;

            string api = string.Format("{0}?phones={1}", GridPatrolInterface.DataListOfPatrolmanInf, phone);
            if (!string.IsNullOrEmpty(datetime))
                api = string.Format("{0}&date={1}", api, datetime);

            Patrolman patrolman = new Patrolman();

            string resStr = HttpServiceHelper.Instance.GetRequest(api);

            if (!string.IsNullOrEmpty(resStr))
            {
                patrolman = JsonUtil.DeserializeFromString<List<Patrolman>>(resStr)[0];
            }
            return patrolman;
        }

        private void RenderPatrolmanPoints(PatrolmanDataForRender patrolmanData, int index = 0, bool isFirst = true, bool isHistroy = true)
        {
            if (patrolmanData == null) return;

            if (patrolmanData.PointsList.Count <= 0) return;

            List<IPoint> pointList = patrolmanData.PointsList;

            System.Drawing.Color color = PersonalColor(patrolmanData.Phone);
            IPointSymbol ptSymbol = GviMap.PointManager.CreateSymbol(color, 9, gviSimplePointStyle.gviSimplePointCircle);
            var mulpts = new List<IPoint>(); ;
            for (int j = 0; j < pointList.Count; j++)
            {
                var item = pointList[j];

                if ((j != 0 || !isFirst) && patrolmanData.StatusLocation.Contains(j))
                {
                    //签到点
                    string img = "项目数据\\shp\\IMG_POI\\alphabet_C.png";

                    var key = patrolmanData.Phone.ToString() + index + j;
                    if (!GviMap.TracePoiManager.ContainsKey(TAG, key))
                    {
                        var poi = GviMap.TracePoiManager.CreatePoi(item.X, item.Y, item.Z, img, patrolmanData.Name + ":" + patrolmanData.PointsTime[j], size: 24);
                        poi.ShowName = false;
                        var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
                        GviMap.TracePoiManager.AddPoi(patrolmanData.Phone.ToString() + index + j, TAG, rPoi);
                    }
                    else
                    {
                        var rPoi = GviMap.TracePoiManager.GetRPOI(TAG, key);
                        var poi = rPoi.GetFdeGeometry() as IPOI;
                        poi.X = item.X;
                        poi.Y = item.Y;
                        poi.Z = item.Z;
                    }
                    if (j != pointList.Count - 1)//最後一点不处理
                    {
                        //下一点增加开始点
                        img = "项目数据\\shp\\IMG_POI\\alphabet_S.png";
                        key = patrolmanData.Phone.ToString() + index + 1 + j;
                        item = pointList[j + 1];
                        if (!GviMap.TracePoiManager.ContainsKey(TAG, key))
                        {
                            var poi = GviMap.TracePoiManager.CreatePoi(item.X, item.Y, item.Z, img, patrolmanData.Name + "开始时间：" + patrolmanData.PointsTime[j + 1], size: 30);
                            poi.ShowName = false;
                            var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
                            GviMap.TracePoiManager.AddPoi(key, TAG, rPoi);
                        }
                        else
                        {
                            var rPoi = GviMap.TracePoiManager.GetRPOI(TAG, key);
                            var poi = rPoi.GetFdeGeometry() as IPOI;
                            poi.X = item.X;
                            poi.Y = item.Y;
                            poi.Z = item.Z;
                        }
                    }
                }

                if (j == 0 && isFirst)
                {

                    string img = "项目数据\\shp\\IMG_POI\\alphabet_S.png";
                    var key = patrolmanData.Phone.ToString() + index + j;
                    if (!GviMap.TracePoiManager.ContainsKey(TAG, key))
                    {
                        var poi = GviMap.TracePoiManager.CreatePoi(item.X, item.Y, item.Z, img, patrolmanData.Name + "开始时间：" + patrolmanData.PointsTime[j], size: 30);

                        var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
                        GviMap.TracePoiManager.AddPoi(key, TAG, rPoi);
                    }
                    else
                    {
                        var rPoi = GviMap.TracePoiManager.GetRPOI(TAG, key);
                        var poi = rPoi.GetFdeGeometry() as IPOI;
                        poi.X = item.X;
                        poi.Y = item.Y;
                        poi.Z = item.Z;
                    }

                }
                else if (j == pointList.Count - 1)
                {
                    string img = string.Empty;
                    string name = string.Empty;
                    if (isHistroy)
                    {
                        img = "项目数据\\shp\\IMG_POI\\alphabet_E.png";
                        name = patrolmanData.Name + string.Format("结束时间：{0}", patrolmanData.PointsTime[j]);
                        var poi = GviMap.TracePoiManager.CreatePoi(item.X, item.Y, item.Z, img, name, size: 30);
                        var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
                        GviMap.TracePoiManager.AddPoi(patrolmanData.Phone.ToString() + index + j, TAG, rPoi);
                    }
                    else
                    {
                        img = "项目数据\\shp\\IMG_POI\\alphabet_N.png";
                        name = patrolmanData.Name + "当前位置";
                        var uid = patrolmanData.Phone.ToString() + name;
                        if (!GviMap.TracePoiManager.ContainsKey(TAG, uid))
                        {
                            var poi = GviMap.TracePoiManager.CreatePoi(item.X, item.Y, item.Z, img, name, size: 30);
                            var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
                            GviMap.TracePoiManager.AddPoi(uid, TAG, rPoi);
                        }
                        else
                        {
                            var rpoi = GviMap.TracePoiManager.GetRPOI(TAG, uid);
                            var poi = rpoi.GetFdeGeometry() as IPOI;
                            poi.X = item.X;
                            poi.Y = item.Y;
                            poi.Z = item.Z;
                            rpoi.SetFdeGeometry(poi);
                        }
                    }


                }
                else
                {
                    mulpts.Add(item);
                }
            }

            //创建多点,提高性能
            var mulpoint = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            mulpoint.SpatialCRS = GviMap.SpatialCrs;
            foreach (var item in mulpts)
            {
                mulpoint.AddGeometry(item);
            }

            var rmulPt = GviMap.ObjectManager.CreateRenderMultiPoint(mulpoint, ptSymbol);
            GviMap.TraceLinePolyManager.AddPoi(patrolmanData.Phone.ToString() + "mulPt", 0, null, rmulPt);
        }


        private void RenderPatrolmanTrace(PatrolmanDataForRender patrolmanData)
        {
            if (patrolmanData == null) return;

            List<IPoint> pointList = patrolmanData.PointsList;

            if (pointList.Count <= 1) return;

            System.Drawing.Color color = PersonalColor(patrolmanData.Phone);

            var polyLine = GviMap.GeoFactory.CreatePolyline(pointList, GviMap.SpatialCrs);

            polyLine.Smooth(0);

            ICurveSymbol curveSymbol;
            if (_personSymbol.ContainsKey(patrolmanData.Phone)) curveSymbol = _personSymbol[patrolmanData.Phone];
            else
            {
                curveSymbol = GviMap.TraceLinePolyManager.CreateCurveSymbol(0.08f, color, gviDashStyle.gviDashSmall);
                _personSymbol.Add(patrolmanData.Phone, curveSymbol);
            }

            var rLine = GviMap.ObjectManager.CreateRenderPolyline(polyLine, curveSymbol);
            //rLine.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
            GviMap.TraceLinePolyManager.AddPoi(patrolmanData.Phone, 2, null, rLine);
        }


        private void RenderPatrolmanTraceEx(PatrolmanDataForRender patrolmanData)
        {
            if (patrolmanData == null) return;

            List<IPoint> pointList = patrolmanData.PointsList;

            if (pointList.Count <= 1) return;

            System.Drawing.Color color = PersonalColor(patrolmanData.Phone);

            List<List<IPoint>> points = new List<List<IPoint>>();
            var index = 0;
            for (int i = 0; i < pointList.Count; i++)
            {
                if (i == 0)//起点
                {
                    index = 0;
                    points.Add(new List<IPoint>());
                    points[index].Add(pointList[i]);
                }
                else if (patrolmanData.StatusLocation.Contains(i))//中间签到点
                {
                    points[index].Add(pointList[i]);
                    index++;
                    points.Add(new List<IPoint>());

                }
                else
                {
                    points[index].Add(pointList[i]);
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                var item = points[i];
                if (item.Count < 2)
                    continue;
                var polyLine = GviMap.GeoFactory.CreatePolyline(item, GviMap.SpatialCrs);

                polyLine.Smooth(0);

                //ICurveSymbol curveSymbol;
                //if (_personSymbol.ContainsKey(patrolmanData.Phone)) curveSymbol = _personSymbol[patrolmanData.Phone];
                //else
                //{
                //    curveSymbol = GviMap.TraceLinePolyManager.CreateCurveSymbol(0.08f, color = GetRandomColor(), gviDashStyle.gviDashSmall);
                //    _personSymbol.Add(patrolmanData.Phone, curveSymbol);
                //}
                var curveSymbol = GviMap.TraceLinePolyManager.CreateCurveSymbol(0.08f, color = GetRandomColor(), gviDashStyle.gviDashSmall);
                var rLine = GviMap.ObjectManager.CreateRenderPolyline(polyLine, curveSymbol);
                //rLine.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                GviMap.TraceLinePolyManager.AddPoi(patrolmanData.Phone + i, 2, null, rLine);
            }

        }


        private void FlyToPersonalRender(string key)
        {
            GviMap.TraceLinePolyManager.Flyto(key);
        }

        private System.Drawing.Color PersonalColor(string key)
        {
            System.Drawing.Color color;
            if (_personalColor.ContainsKey(key))
            {
                color = _personalColor[key];
            }
            else
            {
                color = GetRandomColor();
                _personalColor.Add(key, color);
            }

            return color;
        }

        private System.Drawing.Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            // 尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        public override void OnUnchecked()
        {
            Messenger.Messengers.Notify("ShowSummery", false);

            ClearPatrolList();
            patrolListVModel.UnCheckAllGrid();
            patrolListVModel.ClearOnlinePatrolList();
            patrolListVModel.ClearOnlinePatrolListByArea();
            base.OnUnchecked();
            HideView();
            MapSelectEventManager(false);
            ClearRender();
            //Messenger.Messengers.Notify("ShowHiddenMenu", false);
            selectedAreaId = null;
            GviMap.GridPatrolPolyManager.Clear();
            _gridDic.Clear();
            _groupGridSymbol.Clear();
            eventTimeVModel.HideEventTimeView();

            patrolListVModel.ClosePatrolView();
            eventTypeVModel.HideEventTypeView();
        }

        public override void Reset()
        {
            base.Reset();
        }

        private void ShowView(string title = "",string id="")
        {
            var view = (PatrolmanListView)base.View;
            view.DataContext = this;
            view.Owner = Application.Current.MainWindow;
            view.Left = Application.Current.MainWindow.Width * 0.6;
            view.Top = Application.Current.MainWindow.Height * 0.3;
            meshView.Left = Application.Current.MainWindow.Width * 0.2;
            meshView.Top = Application.Current.MainWindow.Height * 0.1;
            
            if (!string.IsNullOrEmpty(title)) PatrolmanTableTitle = title + " 人员列表";
            else PatrolmanTableTitle = "工作人员列表";
            SearchText = "";
            view.Show();
            meshView.Show();
            meshView.reLoad(id);
        }
        private void HideView()
        {
            var view = (PatrolmanListView)base.View;
            view.Hide();
            meshView.Hide();
            if (_patrolRecordVModel != null) _patrolRecordVModel.CloseView();

            _patrolmanSet?.Clear();
            _isTreadOpen = false;
            _patrolmanList.Clear();
            patrolListVModel.ClearOnlinePatrolList();
            patrolListVModel.ClearOnlinePatrolListByArea();
        }

        // 曲线光滑
        private List<IPoint> optimizePoints(List<IPoint> inList)
        {
            int lenght = inList.Count;

            if (lenght < 5)
            {
                return inList;
            }
            else
            {
                inList[0].X = ((3.0 * inList[0].X + 2.0 * inList[1].X + inList[2].X - inList[4].X) / 5.0);
                inList[1].X = ((4.0 * inList[0].X + 3.0 * inList[1].X + 2.0 * inList[2].X + inList[3].X) / 10.0);
                inList[lenght - 2].X = ((4.0 * inList[lenght - 1].X + 3.0 * inList[lenght - 2].X + 2.0 * inList[lenght - 3].X + inList[lenght - 4].X) / 10.0);
                inList[lenght - 1].X = ((3.0 * inList[lenght - 1].X + 2.0 * inList[lenght - 2].X + inList[lenght - 3].X - inList[lenght - 5].X) / 5.0);

                //log
                inList[0].Y = ((3.0 * inList[0].Y + 2.0 * inList[1].Y + inList[2].Y - inList[4].Y) / 5.0);
                inList[1].Y = ((4.0 * inList[0].Y + 3.0 * inList[1].Y + 2.0 * inList[2].Y + inList[3].Y) / 10.0);
                inList[lenght - 2].Y = ((4.0 * inList[lenght - 1].Y + 3.0 * inList[lenght - 2].Y + 2.0 * inList[lenght - 3].Y + inList[lenght - 4].Y) / 10.0);
                inList[lenght - 1].Y = ((3.0 * inList[lenght - 1].Y + 2.0 * inList[lenght - 2].Y + inList[lenght - 3].Y - inList[lenght - 5].Y) / 5.0);

                return inList;
            }
        }
        private void GetSinglePosition(object obj)
        {
            //patrolListVModel.ClearOnlinePatrolList();
            //patrolListVModel.ClearOnlinePatrolListByArea();
            patrolListVModel.CancleSelectAll();
            ClearRender();
            _patrolRecordVModel?.HideView();
            if (obj == null) return;
            PatrolmanForClient Model = obj as PatrolmanForClient;
            bool personExist = patrolListVModel.OnlinePeopleListByArea.ContainsKey(Model.ID);
            if (personExist)
            {
                GviMap.Camera.FlyToObject(patrolListVModel.OnlinePeopleListByArea[Model.ID], gviActionCode.gviActionFlyTo);
                IRenderPOI renderPOI = GviMap.ObjectManager.GetObjectById(patrolListVModel.OnlinePeopleListByArea[Model.ID]) as IRenderPOI;
                IPoint point = renderPOI.GetFdeGeometry() as IPoint;
                point.SetPostion(point.X, point.Y,50);              
                GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                GviMap.Camera.FlyToEnvelope(point.Envelope);
                eulerAngle.Tilt = -90;
                GviMap.Camera.SetCamera2(point, eulerAngle, 0);
            }
            else
            {
                try
                {
                    string resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.SinglePatrolmanInf + "?inspector_id=" + Model.ID);
                    if (resStr != null && resStr != "")
                    {
                        var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                        string geom = resDyn.geom;
                        string department_name = resDyn.department_name;
                        if (geom != "" && geom != null)
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
                            poi.Name = Model.Name;                            
                            poi.SpatialCRS = GviMap.SpatialCrs;
                            poi.ImageName = string.Format("项目数据\\shp\\IMG_POI\\{0}.png", "保安Warning");//Helpers.ResourceHelper.FindResourceByKey("userImg").ToString();//
                            IRenderPOI rpoi = GviMap.ObjectManager.CreateRenderPOI(poi);
                            rpoi.DepthTestMode = gviDepthTestMode.gviDepthTestAdvance;
                            //rpoi.Highlight(Color.Red);
                            GviMap.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                            IRenderPOI renderPOI = GviMap.ObjectManager.GetObjectById(rpoi.Guid) as IRenderPOI;
                            IPoint flyPoint = renderPOI.GetFdeGeometry() as IPoint;
                            point.SetPostion(point.X, point.Y, 50);
                            GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                            GviMap.Camera.FlyToEnvelope(point.Envelope);
                            eulerAngle.Tilt = -90;
                            GviMap.Camera.SetCamera2(point, eulerAngle, 0);
                            //guidList.Add(rpoi.Guid);
                            if (peopleList.ContainsKey(Model.ID))
                            {
                                GviMap.ObjectManager.DeleteObject(peopleList[Model.ID]);
                                peopleList.Remove(Model.ID);
                            }
                            peopleList.Add(Model.ID, rpoi.Guid);
                            var table = createTableLable(point, Model.Name, Model.Phone);
                            table.SetRecord(0, 1, Model.Phone);
                            if (department_name.Length > 6)
                            {
                                department_name = department_name.Substring(0, 6)+"...";
                                table.SetRecord(1, 1, department_name);
                            }
                            else
                            {
                                table.SetRecord(1, 1, department_name);
                            }
                            //foreach (var item in _gridDic)
                            //{
                            //    if(item.Value.id==areaID)
                            //    {
                            //        table.SetRecord(1, 1, item.Value.name);                                   
                            //    }
                            //}                                                       
                            table.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            if (guidList.ContainsKey(Model.ID))
                            {
                                GviMap.ObjectManager.DeleteObject(guidList[Model.ID]);
                                guidList.Remove(Model.ID);
                            }
                            guidList.Add(Model.ID, table.Guid);
                        }
                    }
                    else
                    {
                        Messages.ShowMessage("尚未查询到该人员位置信息！");
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
          
        }
        private ITableLabel createTableLable(IPoint pt, string _name, string _phone)
        {
            var tableLabel = TableLabelFactory.CreateWindTable(GviMap.ObjectManager, 2, 2);
            pt.Z = 2;
            tableLabel.Position= pt;
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
        private async void GetSingleRoute(object obj)//PatrolmanForClient patrolmanForClient
        {
            patrolListVModel.CancleSelectAll();
            if (guidList?.Count!=0)
            {
                foreach(var item in guidList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
                guidList = new Dictionary<string, Guid>();
            }
            if (peopleList?.Count != 0)
            {
                foreach (var item in peopleList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
                peopleList = new Dictionary<string, Guid>();
            }
            patrolListVModel.ClearOnlinePatrolList();
            patrolListVModel.ClearOnlinePatrolListByArea();
            if (obj == null) return;
            PatrolmanForClient Model = obj as PatrolmanForClient;
            if (Model == null) return;

            if (_patrolRecordVModel != null)
            {
                _patrolRecordVModel.CloseView();
                _patrolRecordVModel = null;
            }
            _patrolRecordVModel = new PatrolRecordVModel();
            if (PatrolmanSet.Count > 0)
            {
                PatrolmanSet.ToList().ForEach(p => p.IsSelected = false);
                PatrolmanSet.ToList().Find(p => p.Phone == Model.Phone).IsSelected = true;
            }

            if (Model.Status == _workStatus) FlyToPersonalRender(Model.Phone);

            string MonthNow = DateTime.Now.ToString("yyyy-MM");

            await Task.Run(() =>
            {
                var historyDataList = GetPatrolmanRecordDatetime(Model.Phone, MonthNow);
                CalerdarSelectedDateCoverter.Update(historyDataList);
            });

            _patrolRecordVModel.currentPerson = Model;
            _patrolRecordVModel.DrawingPersonalHistoryTrace -= OnDrawingPersonalTrace;
            _patrolRecordVModel.DrawingPersonalHistoryTrace += OnDrawingPersonalTrace;
            _patrolRecordVModel.GetPersonalMonthRecord -= GetPatrolmanRecordDatetime;
            _patrolRecordVModel.GetPersonalMonthRecord += GetPatrolmanRecordDatetime;
            _patrolRecordVModel.StartThreadOfPatrol -= OnStartThreadOfPatrol;
            _patrolRecordVModel.StartThreadOfPatrol += OnStartThreadOfPatrol;
            _patrolRecordVModel.ShowView();
            _isTreadOpen = false;
        }
        private void OnSearch()
        {
            List<Patrolman> patrolmenList = new List<Patrolman>();
            string resStr = "";
            if (searchText == null)
            {
                resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.KeyWordOnlinesearch + "?keyword=" + "&area_id=" + Convert.ToInt16(areaID));
                 //resStr = string.Format("{0}?area_id={1}", GridPatrolInterface.PatrolmanListOfAreaInf, areaID);
            }
            else
            {
                 resStr = HttpServiceHelper.Instance.GetRequest(GridPatrolInterface.KeyWordOnlinesearch + "?keyword=" + searchText.ToString() + "&area_id=" + Convert.ToInt16(areaID));
            }
            if (resStr != null && resStr != "")
            {
                var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                
                if (!string.IsNullOrEmpty(resStr))
                {
                    patrolmenList = JsonUtil.DeserializeFromString<List<Patrolman>>(resStr);
                }
                        
            }
            if (patrolmenList != null)
            {
                PatrolmanSet.Clear();
                foreach(var item in patrolmenList)
                {
                    PatrolmanSet.Add(_itoModelCovernter.PatrolmanConvert(item));
                }                
             }    // KeyWordOnlinesearch
        }
        private void Onfold()
        {
            var view =(PatrolmanListView)base.View;
            if (PatrolManVisible == Visibility.Visible)
            {
                PatrolManVisible = Visibility.Collapsed;
                view.Height = 40;
                view.FoldImage.ImageSource = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("packdown_H");
            }
            else
            {
                PatrolManVisible = Visibility.Visible;
                view.Height = 480;
                view.FoldImage.ImageSource = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("packup_H");
            }
        }
        
    }
}