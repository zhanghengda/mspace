using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.IotModule.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class AdministrativeViewModel : CheckedToolItemModel
    {
        public Dictionary<int, Guid> eventTableList = new Dictionary<int, Guid>();

        private AdministrativeView administrativeView = new AdministrativeView();
        private InformationTableView informationTableView = new InformationTableView();

        private GridInfoDetailView gridInfoDetailView = new GridInfoDetailView();
        private List<GridModel> _menuItems = new List<GridModel>();
        private Dictionary<string, ISurfaceSymbol> _groupGridSymbol;
        private Dictionary<string, GridModel> _gridDic;
        Dictionary<string, Guid> peopleList = new Dictionary<string, Guid>();
        Dictionary<string, Guid> guidList = new Dictionary<string, Guid>();
        public ICommand FoldCmd { get; set; }
        public ICommand FoldCmd2 { get; set; }

        private bool isFly = false;

        public List<GridModel> MenuItems
        {
            get
            {
                return this._menuItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._menuItems, value, "MenuItems");
            }
        }

        private bool _DistrictIsChecked;
         /// <summary>
         /// 片
         /// </summary>
        public bool DistrictIsChecked
        {
            get { return _DistrictIsChecked; }
          
            set { _DistrictIsChecked = value; NotifyPropertyChanged("DistrictIsChecked"); }
        }

        private bool _AreaIsChecked;
        /// <summary>
        /// 辖区
        /// </summary>
        public bool AreaIsChecked
        {
            get { return _AreaIsChecked; }
            set { _AreaIsChecked = value; NotifyPropertyChanged("AreaIsChecked"); }
        }
        private bool _TownIsChecked;
        /// <summary>
        /// 镇
        /// </summary>
        public bool TownIsChecked
        {
            get { return _TownIsChecked; }
            set { _TownIsChecked = value; NotifyPropertyChanged("TownIsChecked"); }
        }
        private bool _CountyIsChecked;
        /// <summary>
        /// 县
        /// </summary>
        public bool CountyIsChecked
        {
            get { return _CountyIsChecked; }
            set { _CountyIsChecked = value; NotifyPropertyChanged("CountyIsChecked"); }
        }
        private bool _CityIsChecked;
        /// <summary>
        /// 市
        /// </summary>
        public bool CityIsChecked
        {
            get { return _CityIsChecked; }
            set { _CityIsChecked = value; NotifyPropertyChanged("CityIsChecked"); }
        }
        private List<GridModel> _gridModels;
        public List<GridModel> GridModels
        {
            get
            {
                return _gridModels;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._gridModels, value, "GridModels");
            }
        }

        private GridInfoModel _GridInfoDetailModel ;

        public GridInfoModel GridInfoDetailModel
        {
            get { return _GridInfoDetailModel; }
            set { _GridInfoDetailModel = value; NotifyPropertyChanged("GridInfoDetailModel"); }
        }


        private List<GridInfoModel> _gridInfoModels = new List<GridInfoModel>();
        public List<GridInfoModel> GridInfoModels
        {
            get
            {
                return _gridInfoModels;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridInfoModel>>(ref this._gridInfoModels, value, "GridInfoModels");
            }
        }
        /// <summary>
        /// 市
        /// </summary>
        private List<GridModel> _models1 = new List<GridModel>();
        public List<GridModel> Models1
        {
            get
            {
                return _models1;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._models1, value, "Models1");
            }
        }
        /// <summary>
        /// 区/县
        /// </summary>
        private List<GridModel> _models2 = new List<GridModel>();
        public List<GridModel> Models2
        {
            get
            {
                return _models2;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._models2, value, "Models2");
            }
        }
        /// <summary>
        /// 镇
        /// </summary>
        private List<GridModel> _models3 = new List<GridModel>();
        public List<GridModel> Models3
        {
            get
            {
                return _models3;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._models3, value, "Models3");
            }
        }
        /// <summary>
        /// 辖区
        /// </summary>
        private List<GridModel> _models4 = new List<GridModel>();
        public List<GridModel> Models4
        {
            get
            {
                return _models4;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<GridModel>>(ref this._models4, value, "Models4");
            }
        }
        public ICommand CloseCmd
        {
            get;
            set;
        }

        public ICommand CloseDetailCmd
    {
        get;
        set;
    }
        /// <summary>
        /// 市
        /// </summary>
        public ICommand CitySelectAllCmd
        {
            get;
            set;
        }

        /// <summary>
        /// 县
        /// </summary>
        public ICommand CountySelectAllCmd
        {
            get;
            set;
        }

        /// <summary>
        /// 辖区
        /// </summary>
        public ICommand TownSelectAllCmd
        {
            get;
            set;
        }
        /// <summary>
        /// 镇
        /// </summary>
        public ICommand SelectAllCmd2
        {
            get;
            set;
        }
        /// <summary>
        /// 片
        /// </summary>
        public ICommand SelectAllCmd
        {
            get;
            set;
        }

        public ICommand CloseTableCmd
        {
            get;
            set;
        }
        public ICommand SelectTagCommand
        {
            get;
            set;
        }

      
        public ICommand UnSelectAllCmd
        {
            get;
            set;
        }

        public ICommand CheckCommand
        {
            get;
            set;
        }
        public ICommand SelectChangedCommand
        {
            get;
            set;
        }
        public ICommand ConfirmCmd
        {
            get;
            set;
        }

        public AdministrativeViewModel()
        {
            Messenger.Messengers.Register<bool>("unAdministrative", (r) =>
            {
                base.IsChecked = false;
                this.isFly = false;
            });
        }
        public override void OnChecked()
        {
            base.OnChecked();
            base.IsSelected = true;
            MapSelectEventManager(true);
            Messenger.Messengers.Notify("undeviceview", false);
            Messenger.Messengers.Notify("unpatrol", false);
            Messenger.Messengers.Notify("showMoretool", false);
            Messenger.Messengers.Notify("HideSummery", false);

            ShowView();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            base.IsSelected = false;
            this.isFly = false;
            MapSelectEventManager(false);

            Messenger.Messengers.Notify("ShowSummery", true);
            this.administrativeView.Hide();
            this.informationTableView.Hide();
            this.gridInfoDetailView.Hide();
            _gridDic.Clear();
            GviMap.GridPatrolPolyManager.Clear();
            this.GridInfoModels = new List<GridInfoModel>();
            this.Models1 = new List<GridModel>();
            this.Models2 = new List<GridModel>();
            this.Models3 = new List<GridModel>();
            this.Models4 = new List<GridModel>();
            this.MenuItems = new List<GridModel>();
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
            if (peopleList?.Count != 0)
            {
                foreach (var item in peopleList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Value);
                }
            }
            guidList = new Dictionary<string, Guid>();
            peopleList = new Dictionary<string, Guid>();
        }

        public override void Initialize()
        {
            base.ViewType = ViewType.CheckedIcon;
            base.Initialize();
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                this.Models1 = new List<GridModel>();
                this.Models2 = new List<GridModel>();
                this.Models3 = new List<GridModel>();
                this.Models4 = new List<GridModel>();
                this.MenuItems = new List<GridModel>();
            });
            this.CloseDetailCmd = new RelayCommand(() =>
            {
                this.gridInfoDetailView.Hide();
                this.GridInfoDetailModel = new GridInfoModel();
            }); 


            this.CloseTableCmd = new RelayCommand(() =>
              {
                  this.informationTableView.Hide();
                  this.GridInfoModels = new List<GridInfoModel>();
              });
            this.SelectChangedCommand = new RelayCommand<GridModel>(OnSelectChangedCommand);
            this.CheckCommand = new RelayCommand<GridModel>(OnCheckCommand);
            this.SelectAllCmd = new RelayCommand<bool>(OnSelectAll);
            this.SelectAllCmd2 = new RelayCommand<bool>(OnSelectAll2);
            this.CitySelectAllCmd = new RelayCommand<bool>(OnCitySelectAllCmd);
            this.CountySelectAllCmd= new RelayCommand<bool>(OnCountySelectAllCmd);
            this.TownSelectAllCmd= new RelayCommand<bool>(OnTownSelectAllCmd);


            this.FoldCmd = new RelayCommand(FoldView);
            this.FoldCmd2 = new RelayCommand(FoldView2);

            this.UnSelectAllCmd = new RelayCommand(new Action(this.OnUnSelectAll));
            this.ConfirmCmd = new RelayCommand(new Action(this.OnConfirmCmd));
            this.SelectTagCommand = new RelayCommand<GridModel>(this.OnSelectTagCommand);
            _groupGridSymbol = new Dictionary<string, ISurfaceSymbol>();
            _gridDic = new Dictionary<string, GridModel>();

        }
        private void OnSelectChangedCommand(GridModel obj)
        {

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
        private Visibility _patrolVisible2;
        public Visibility PatrolVisible2
        {
            get { return _patrolVisible2; }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this._patrolVisible2, value, "PatrolVisible2");
            }
        }
        private void FoldView()
        {

            if (PatrolVisible == Visibility.Visible)
            {
                PatrolVisible = Visibility.Collapsed;
                administrativeView.Height = 40;
            }
            else
            {
                PatrolVisible = Visibility.Visible;
                administrativeView.Height = 490;
            }

        }
        private void FoldView2()
        {

            if (PatrolVisible2 == Visibility.Visible)
            {
                PatrolVisible2 = Visibility.Collapsed;
                informationTableView.Height = 64;
            }
            else
            {
                PatrolVisible2 = Visibility.Visible;
                informationTableView.Height = 480;
            }

        }

        public void OnSelectAll(bool obj)
        {
            if(obj&& this.MenuItems.Count > 0)
            {
                for (int i = 0; i < this.MenuItems.Count; i++)
                {
                    this.MenuItems[i].IsChecked = true;
                    this.OnCheckCommand(this.MenuItems[i]);
                }
            }
            else
            {
                this.DistrictIsChecked = false;

                bool flag = this.MenuItems.Count > 0;
                if (flag)
                {
                    for (int i = 0; i < this.MenuItems.Count; i++)
                    {
                        this.MenuItems[i].IsChecked = false;
                        this.OnCheckCommand(this.MenuItems[i]);

                    }
                }
            }
          
        }
        public void OnSelectAll2(bool obj)
        {
            if (obj&& this.Models4.Count > 0)
            {
                for (int i = 0; i < this.Models4.Count; i++)
                {

                    this.Models4[i].IsChecked = true;
                    this.OnCheckCommand(this.Models4[i]);
                }
            }
            else
            {
                this.AreaIsChecked = false;

                bool flag = this.Models4.Count > 0;
                if (flag)
                {
                    for (int i = 0; i < this.Models4.Count; i++)
                    {
                        this.Models4[i].IsChecked = false;
                        this.OnCheckCommand(this.Models4[i]);

                    }
                }
            }

        }
        public void OnCitySelectAllCmd(bool obj)
        {
            if (obj&& this.GridModels.Count > 0)
            {
                for (int i = 0; i < this.GridModels.Count; i++)
                {
                    this.GridModels[i].IsChecked = true;
                    this.OnCheckCommand(this.GridModels[i]);
                }
            }
            else
            {
                this.CityIsChecked = false;
                bool flag = this.GridModels.Count > 0;
                if (flag)
                {
                    for (int i = 0; i < this.GridModels.Count; i++)
                    {
                        this.GridModels[i].IsChecked = false;
                        this.OnCheckCommand(this.GridModels[i]);

                    }
                }
            }

        }
        public void OnCountySelectAllCmd(bool obj)
        {
            if (obj&& this.Models2.Count > 0)
            {
                for (int i = 0; i < this.Models2.Count; i++)
                {
                    this.Models2[i].IsChecked = true;
                    this.OnCheckCommand(this.Models2[i]);
                }
            }
            else
            {
                this.CountyIsChecked = false;
                bool flag = this.Models2.Count > 0;
                if (flag)
                {
                    for (int i = 0; i < this.Models2.Count; i++)
                    {
                        this.Models2[i].IsChecked = false;
                        this.OnCheckCommand(this.Models2[i]);

                    }
                }
            }

        }
        public void OnTownSelectAllCmd(bool obj)
        {
            if (obj&& this.Models3.Count>0)
            {
                for (int i = 0; i < this.Models3.Count; i++)
                {
                    this.Models3[i].IsChecked = true;
                    this.OnCheckCommand(this.Models3[i]);
                }
            }
            else
            {
                this.TownIsChecked = false;
                bool flag = this.Models3.Count > 0;
                if (flag)
                {
                    for (int i = 0; i < this.Models3.Count; i++)
                    {
                        this.Models3[i].IsChecked = false;
                        this.OnCheckCommand(this.Models3[i]);

                    }
                }
            }

        }
        
        public void OnUnSelectAll()
        {

            bool flag = this.MenuItems.Count > 0;
            if (flag)
            {
                for (int i = 0; i < this.MenuItems.Count; i++)
                {
                    this.MenuItems[i].IsChecked = false;
                }
            }
        }
        private void OnConfirmCmd()
        {
            this.DrawGrid(this.MenuItems);
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
        private void AxMapControl_RcMouseClickSelectGrid(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var pt = IntersectPoint;
            if (pt == null) return;

            if (PickResult == null) return;

            IRenderPolygonPickResult rPolyPk = null;
         
            if (PickResult is IRenderPolygonPickResult)
            {
                rPolyPk = PickResult as IRenderPolygonPickResult;

                if (rPolyPk == null) return;
                string key = rPolyPk.RenderPolygon.Guid.ToString();
                try
                {
                    ShowPatrolmanListOfArea(key);
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

                    var aa = _gridDic[key].id;
                    GridModel gridModel = getGridModel(GridModels, aa);

                    string request = Singleton<HttpServiceHelper>.Instance.GetRequest(GridPatrolInterface.GetGridinfolist + "?grid_id=" + aa);
                    GridInfoModel gridInfoModel = JsonUtil.DeserializeFromString<GridInfoModel>(request);

                    gridInfoModel.name = remodel.name;
                    GridInfoDetailModel = gridInfoModel;


                    if (gridInfoDetailView==null)
                    {
                        gridInfoDetailView = new GridInfoDetailView();
                    }
                    this.gridInfoDetailView.DataContext = this;
                    this.gridInfoDetailView.Left = Application.Current.MainWindow.Width * 0.3;
                    this.gridInfoDetailView.Top = Application.Current.MainWindow.Height * 0.2;
                    this.gridInfoDetailView.Owner = Application.Current.MainWindow;
                    gridInfoDetailView.Show();


                    //ReDrawingPatrolmanTrace(patrolRenderList,key);

                    //ShowView(_gridDic[key].name, _gridDic[key].id);
                }
            }
        }
        GridModel remodel = new GridModel();
        private GridModel getGridModel(List<GridModel> list,string id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].id == id)
                {
                    remodel = list[i];
                }
                else
                {
                    if (list[i].list.Count > 0)
                    {
                        getGridModel(list[i].list, id);
                    }
                }
            }
            return new GridModel();
        }
        private void OnSelectTagCommand(GridModel obj)
        {
            if (obj == null) return;
            obj.list.ForEach(t => t.IsChecked = false);
            if (obj.level == "1")
            {
                GridModels.ForEach(t => t.IsSelected = false);
                this.AreaIsChecked = false;
                this.DistrictIsChecked = false;
                this.TownIsChecked = false;
                this.CountyIsChecked = false;
                foreach (var item in this.Models2)
                {
                    this.RemoveGridModels(item);
                }
                foreach (var item in this.Models3)
                {
                    this.RemoveGridModels(item);
                }
                foreach (var item in this.Models4)
                {
                    this.RemoveGridModels(item);
                }
                foreach (var item in this.MenuItems)
                {
                    this.RemoveGridModels(item);
                }
                this.Models2 = obj.list;
                this.Models3 = new List<GridModel>();
                this.Models4 = new List<GridModel>();
                this.MenuItems = new List<GridModel>();
                List<GridModel> list = new List<GridModel>();
                list.Add(obj);
                this.DrawGrid(list);
            }
            else if (obj.level == "2")
            {
                this.AreaIsChecked = false;
                this.DistrictIsChecked = false;
                this.TownIsChecked = false;
                Models2.ForEach(t => t.IsSelected = false);
                foreach (var item in this.Models3)
                {
                    this.RemoveGridModels(item);
                }
                this.Models3 = obj.list;
                foreach (var item in this.Models3)
                {
                    this.ShowView2(item);
                }
                this.Models4 = new List<GridModel>();
                this.MenuItems = new List<GridModel>();
            }
            else if (obj.level == "3")
            {
                Models3.ForEach(t => t.IsSelected = false);
                foreach (var item in this.Models4)
                {
                    this.RemoveGridModels(item);
                }
                foreach (var item in this.MenuItems)
                {
                    this.RemoveGridModels(item);
                }
                this.Models4 = obj.list;
                this.MenuItems = new List<GridModel>();
                this.DistrictIsChecked = false;
                this.AreaIsChecked = false;

            }
            else if (obj.level == "4")
            {
                this.DistrictIsChecked = false;

                Models4.ForEach(t => t.IsSelected = false);
                foreach (var item in this.MenuItems)
                {
                    this.RemoveGridModels(item);
                }
                this.MenuItems = obj.list;
            }
            obj.IsSelected = true;
        }
        private void OnCheckCommand(GridModel obj)
        {
            if (obj == null) return;
            if (obj.geom == "POLYGON((0 0,0 0,0 0,0 0,0 0))") return;
            List<GridModel> list = new List<GridModel>();
            if (obj.IsChecked)
            {
                list.Add(obj);
                this.DrawGrid(list);
            }
            else
            {
                RemoveObject(obj);
            }
            ResetIsCheck(obj);

        }

       private void  ResetIsCheck(GridModel obj)
        {
            if (obj.level == "1")
            {
                if(this.GridModels.Where(t=>t.IsChecked==true).Count()==this.GridModels.Count)
                {
                    this.CityIsChecked = true;
                }
                  else
                {
                    this.CityIsChecked = false;
                }
            }
            else if (obj.level == "2")
            {
                if (this.Models2.Where(t => t.IsChecked == true).Count() == this.Models2.Count)
                {
                    this.CountyIsChecked = true;
                }
                else
                {
                    this.CountyIsChecked = false;
                }
            }
            else if (obj.level == "3")
            {
                if (this.Models3.Where(t => t.IsChecked == true).Count() == this.Models3.Count)
                {
                    this.TownIsChecked = true;
                }
                else
                {
                    this.TownIsChecked = false;
                }
            }
            else if (obj.level == "4")
            {
                if (this.Models4.Where(t => t.IsChecked == true).Count() == this.Models4.Count)
                {
                    this.AreaIsChecked = true;
                }
                else
                {
                    this.AreaIsChecked = false;
                }
            }
            else if (obj.level == "5")
            {
                if (this.MenuItems.Where(t => t.IsChecked == true).Count() == this.MenuItems.Count)
                {
                    this.DistrictIsChecked = true;
                }
                else
                {
                    this.DistrictIsChecked = false;
                }
            }
        }
        private void RemoveObject(GridModel obj)
        {
            bool flag5 = this.eventTableList.ContainsKey(Convert.ToInt32(obj.id));
            if (flag5)
            {
                GviMap.ObjectManager.DeleteObject(this.eventTableList[Convert.ToInt32(obj.id)]);
                this.eventTableList.Remove(Convert.ToInt32(obj.id));
            }
        }
        private void RemoveGridModels(GridModel obj)
        {
            this.GridInfoModels = this.GridInfoModels.Where(t => t.grid_id != obj.id).ToList();
        }
        private IPolygon _centerPtPolygon = null;//凹多边形前三个点构造多边形

        private void DrawGrid(List<GridModel> list)
        {
            Task.Run(() =>
            {
             
                if (list != null && list.Count > 0)
                {
             
                    IRenderPolygon lastRpolygon = null;
                    foreach (var item in list)
                    {
                        bool flag5 = this.eventTableList.ContainsKey(Convert.ToInt32(item.id));
                        if (flag5)
                        {
                            return;
                        }
                        if (item.geom == "POLYGON((0 0,0 0,0 0,0 0,0 0))") continue;
                        var polygon = GviMap.GeoFactory.CreatePolygon(item.geom, GviMap.SpatialCrs);
                        polygon = polygon.CreatePolygonWithZ(1);//z偏移防止闪面

                        if (polygon == null) continue;
                        var color = GetRandomColor();
                        ICurveSymbol curveSym = null;
                        ISurfaceSymbol surSym = null;

                        // 当仅有一个分组时，各元素单独颜色符号显示；多分组按分组显示，同组同颜色符号
                        if (item.group == null) item.group = "GroupForNotNull";
                        if (list.Count != 1 && _groupGridSymbol.ContainsKey(item.group)) surSym = _groupGridSymbol[item.group];
                        else
                        {
                            curveSym = GviMap.GridPatrolPolyManager.CreateCurveSymbol(-2f, color);
                            surSym = GviMap.GridPatrolPolyManager.CreateSurfaceSymbol((CurveSymbol)curveSym, System.Drawing.Color.FromArgb(60, color));
                            if (!_groupGridSymbol.ContainsKey(item.group)) _groupGridSymbol.Add(item.group, surSym);
                        }

                        var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, surSym);
                        lastRpolygon = rPolygon;
                        lastRpolygon.Name = item.name;
                        lastRpolygon.ToolTipText= item.name;
                        this.eventTableList.Add(Convert.ToInt32(item.id), rPolygon.Guid);

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
                        }

                        var label = GviMap.ObjectManager.CreateLabel(centerpt);


                        

                        label.MaxVisibleDistance = WebConfig.TracePoiMaxDistance;
                        label.Text = item.name;
                        label.VisibleMask= gviViewportMask.gviViewAllNormalView;
                        string key = rPolygon.Guid.ToString();
                        GviMap.GridPatrolPolyManager.AddPoi(key, 3, label, rPolygon);

                        _gridDic.Add(key, item);
                    }

                    if (lastRpolygon != null&& isFly==false)
                    {
                        GviMap.Camera.LookAtEnvelope(lastRpolygon.Envelope);
                        //IVector3 vector = lastRpolygon.Envelope as IVector3;

                        GviMap.Camera.GetCamera2(out IPoint pointCamera, out IEulerAngle eulerAngle);
                        ////GviMap.Camera.FlyToEnvelope(point.Envelope);
                        eulerAngle.Tilt = -90;
                        eulerAngle.Heading = 210;
                        pointCamera.X = 112.602617;
                        pointCamera.Y =23.2301;
                        pointCamera.Z = 38600;
                        GviMap.Camera.SetCamera2(pointCamera, eulerAngle, 0);

                        isFly = true;
                        //GviMap.Camera.FlyToObject(lastRpolygon.Guid, gviActionCode.gviActionFlyTo);
                    }

                }
            });
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
        private List<GridModel> GetGridModels(GridModel gridModel)
        {
            for (int i = 0; i < GridModels.Count; i++)
            {

            }
            return this.MenuItems;
        }
        private void GetGridList()
        {
            string request = Singleton<HttpServiceHelper>.Instance.GetRequest(GridPatrolInterface.GetGridlist);
            this.GridModels = JsonUtil.DeserializeFromString<List<GridModel>>(request);
        }
        private void ShowView()
        {
            try
            {
                this.GetGridList();
                bool flag = this.administrativeView == null;
                if (flag)
                {
                    this.administrativeView = new AdministrativeView();
                }
                this.administrativeView.DataContext = this;
                this.administrativeView.Left = Application.Current.MainWindow.Width * 0.5;
                this.administrativeView.Top = Application.Current.MainWindow.Height * 0.2;
                this.administrativeView.Owner = Application.Current.MainWindow;
               
                this.administrativeView.Show();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
        private void GetGridList2(GridModel griditem)
        {
            string request = Singleton<HttpServiceHelper>.Instance.GetRequest(GridPatrolInterface.GetGridinfolist + "?grid_id="+ griditem.id);
            GridInfoModel gridInfoModel = JsonUtil.DeserializeFromString<GridInfoModel>(request);
            gridInfoModel.name = griditem.name;
            List<GridInfoModel> list = new List<GridInfoModel>();
            list.Add(gridInfoModel);
            list.AddRange(this.GridInfoModels);
            for (int i = 1; i <= list.Count; i++)
            {
                list[i - 1].Index = i;
            }
            this.GridInfoModels = list;
        }
        private void ShowView2(GridModel griditem)
        {
            try
            {
                Task.Run(() => {
                    GetGridList2(griditem);
                });
                if (this.informationTableView == null)
                {
                    this.informationTableView = new InformationTableView();
                }

                if(this.informationTableView.IsVisible==false)
                {
                    this.informationTableView.DataContext = this;
                    this.informationTableView.Left = Application.Current.MainWindow.Width * 0.2;
                    this.informationTableView.Top = Application.Current.MainWindow.Height * 0.3;
                    this.informationTableView.Owner = Application.Current.MainWindow;

                    this.informationTableView.Show();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
    }
}
