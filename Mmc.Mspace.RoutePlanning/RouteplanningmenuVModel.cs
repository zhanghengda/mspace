using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.RoutePlanning.Grid;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Net.WebSockets;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Common.Models;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.PoiManagerModule;
using Helpers;
using Mmc.Mspace.RoutePlanning.Views;
using Application = System.Windows.Application;

namespace Mmc.Mspace.RoutePlanning
{
    public class RouteplanningmenuVModel : BindableBase
    {
        private RoutePlanView _routePlanView;
        private GridUI _gridUI;
        private DrawCustomerUC drawCustomer = null;
        private GenerateGeoJson _generateGeoJson;
        private DrawCustomerUC editCustomer1 = null;//ployEdit
        private readonly ExportProgressView progressView;//busying

        private bool _isContinueEdit = false;

        private bool _isAutoDetection = false;
        private string _RoutePlanLineKey = "RoutePlanLine";
        private string _RoutePlanPipeLineKey = "RoutePlanPipeLine";
        private string _RoutePlanMultiPtKey = "RoutePlanMultiPtKey";
        private string _RouteNoInterSetMultiLineKey = "RouteNoInterSetMultiLineKey";
        private string _RouteInterSetMultiLineKey = "RouteInterSetMultiLineKey";
        private string _RoutePlanPipeLineRedKey = "RoutePlanPipeLineRedKey";

        private List<CurRenderPoint> renderPointList;
        private List<ITableLabel> tableLabelList;
        private List<IRObject> tmpList;
        private List<IRenderPipeLine> pipeLinesInterSet;

        public List<RoutePoint> _routePtList;
        private const double _flyHeight = 20;//20米
        private const double _dispLength = 8;//8米

        private double tmpLon = 0;
        private double tmpLat = 0;

        private int selectIndex = 1;
        private IRenderPoint selectedPt;
        private int ptCount = 0;

        private IRenderPolyline rPolyline;
        private bool isRoutePointAdd = false;
        private bool isRoutePointInsert = false;
        private IPoint movingPt;
        private IPoint pickPt;
        public static string[] _missionJson;
        public string WindowTitle { get; set; }

        private ILabel _label;

        [XmlIgnore]
        public IPolyline _polyline;

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        [XmlIgnore]
        public ICommand cmdExportKml { get; set; }

        [XmlIgnore]
        public ICommand cmdExportDoublePointKml { get; set; }

        [XmlIgnore]
        public ICommand cmdExportGeoJson { get; set; }

        [XmlIgnore]
        public ICommand cmdCreateGroundRoute { get; set; }

        [XmlIgnore]
        public ICommand cmdCreatePowerRoute { get; set; }

        [XmlIgnore]
        public ICommand cmdCreateMappingRoute { get; set; }

        [XmlIgnore]
        public ICommand cmdCreateLogisticRoute { get; set; }

        [XmlIgnore]
        public ICommand cmdUploadRoute { get; set; }

        [XmlIgnore]
        public ICommand cmdKmlInput { get; set; }

        [XmlIgnore]
        public ICommand cmdMissionInput { get; set; }

        [XmlIgnore]
        public ICommand cmdCreatePolygon { get; set; }

        [XmlIgnore]
        public ICommand cmdExportStationMission { get; set; }

        [XmlIgnore]
        public ICommand cmdPolyLineEditMission { get; set; }

        [XmlIgnore]
        public ICommand cmdShowRouteListView { get; set; }

        [XmlIgnore]
        public ICommand cmdShowRoutePlanView { get; set; }

        [XmlIgnore]
        public ICommand cmdShowRoutePointEdit { get; set; }

        [XmlIgnore]
        public ICommand cmdShowUploadRouteView { get; set; }


        private RouteListViewModel _routeListModel;

        public RouteListViewModel RouteListModel
        {
            get { return _routeListModel ?? (_routeListModel = new RouteListViewModel()); }
            set { _routeListModel = value; base.NotifyPropertyChanged("RouteListModel"); }
        }


        private RoutePlanShowPageViewModel _routePlanShowPageModel;

        public RoutePlanShowPageViewModel RoutePlanShowPageModel
        {
            get { return _routePlanShowPageModel ?? (_routePlanShowPageModel = new RoutePlanShowPageViewModel()); }
            set { _routePlanShowPageModel = value; base.NotifyPropertyChanged("RoutePlanShowPageModel"); }
        }



        private RoutePointEditViewModel _routePoitEditViewModel;

        public RoutePointEditViewModel RoutePoitEditViewModel
        {
            get { return _routePoitEditViewModel ?? (_routePoitEditViewModel = new RoutePointEditViewModel()); }
            set { _routePoitEditViewModel = value; base.NotifyPropertyChanged("RoutePoitEditViewModel"); }
        }



        private UploadRouteTipsViewModel _uploadRouteTipsViewModel;

        public UploadRouteTipsViewModel UploadRouteTipsViewModel
        {
            get { return _uploadRouteTipsViewModel ?? (_uploadRouteTipsViewModel = new UploadRouteTipsViewModel()); }
            set { _uploadRouteTipsViewModel = value; base.NotifyPropertyChanged("UploadRouteTipsViewModel"); }
        }

        public RouteplanningmenuVModel()
        {
            _generateGeoJson = new GenerateGeoJson();
            _generateGeoJson.initGeoJson();
            ServiceManager.GetService<IDataBaseService>(null).OnLoadingDataSourceProcess += new Action<string>(LoadingDsProcess);
            progressView = new ExportProgressView();
          
            this.cmdCreatePowerRoute = new RelayCommand(() =>
            {
                ClearAllLines();
                _isAutoDetection = false;
                this.DrawRouteOpen = false;
                hideRoutePlanView();
                Console.WriteLine("  ---cmdCreatePowerRoute ");
            });

            this.cmdCreateGroundRoute = new RelayCommand(() =>
            {
                ClearAllLines();
                _isAutoDetection = true;
                this.DrawRouteOpen = false;
                hideRoutePlanView();
            });

            this.cmdCreateMappingRoute = new RelayCommand(() =>
            {
                ClearAllLines();
                ShowGridUI();
                hideRoutePlanView();
            });

            /*航点编辑*/
            this.cmdPolyLineEditMission = new RelayCommand(() =>
            {
                CleanRenderPointAndTransformHelper();
                CleanAllRenderPoint();
                CloseWindows();
                if (GviMap.TempRObjectPool.ContainsKey(_RoutePlanLineKey))
                {
                    _isContinueEdit = true;

                    IRenderable renderable = null;
                    renderable = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderable;
                    renderable.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    var rpolyline1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
                    RegisterEditLineEvent(rpolyline1);
                }
                else
                {
                    RoutePoitEditViewModel.IsSelected = false;
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("RouteEditWarn"));
                }

            });

            this.cmdCreateLogisticRoute = new RelayCommand(() =>
            {
                hideRoutePlanView();
            });

            this.cmdKmlInput = new RelayCommand(() =>
            {
                _polyline = null;
                ParseKml parseKml = new ParseKml();
                _polyline = parseKml.readXmlFile();
                _routePtList = parseKml._routePtList;
                if (_routePtList != null)
                {
                    this.showInputPolyline(_polyline);
                }
            });

            this.cmdMissionInput = new RelayCommand(() =>
            {
                _polyline = null;
                _routePtList = new List<RoutePoint>();
                ParseMission parseMission = new ParseMission();
                _polyline = parseMission.readMissionFile();
                _routePtList = parseMission._routePtList;
                if (_routePtList?.Count > 0)
                {
                    this.showInputPolyline(_polyline);
                }
            });

            this.cmdExportKml = new RelayCommand(() =>
            {
                if (_routePtList != null)
                {
                    this.exportKmlFile();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
            });

            this.cmdExportDoublePointKml = new RelayCommand(() =>
            {
                if (_routePtList != null)
                {
                    this.exportDoublePointKmlFile();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));

            });

            //导出GeoJson
            this.cmdExportGeoJson = new RelayCommand(() =>
            {
                if (_polyline != null)
                {
                    this.exportGeoJsonFile();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
            });

            this.cmdExportStationMission = new RelayCommand(() =>
            {

                if (_routePtList != null)
                {
                    _generateGeoJson.setMMCStationMission(_routePtList);
                    _generateGeoJson.SaveStationMissionFileDialog();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
            });

            /*航线列表展示*/
            this.cmdShowRouteListView = new RelayCommand(() =>
            {
                CloseWindows();
                CleanAllRenderPoint();
                //_routeListModel.conveyJson3 -= new ConveyJson(ReadMissionJson);
                //_routeListModel.conveyJson3 += new ConveyJson(ReadMissionJson);
                RouteListModel.conveyJson3 -= new Views.ConveyJson(ReadMissionJson);
                RouteListModel.conveyJson3 += new Views.ConveyJson(ReadMissionJson);
                RouteListModel?.OnChecked();
            });

            this.CloseCmd = new RelayCommand(() =>
            {

                _routePlanView?.Close();
                ServiceManager.GetService<IShellService>(null).ToolRouteplanningMenu.Visibility = Visibility.Collapsed;
                _gridUI?.Close();
                this.releaseWindow();
            });

            this.cmdShowRoutePlanView = new RelayCommand(() =>
            {

                _polyline = null;
                CloseWindows();
                HideAllIReaderGeometry();
                CleanAllRenderPoint();
                ClearAllLines();

                _routePlanShowPageModel?.OnChecked();
                _routePlanShowPageModel.callback = new Callback(drawGeometry);
            });

            this.cmdShowRoutePointEdit = new RelayCommand(() =>
            {
                RoutePointEditView rpeView = new RoutePointEditView();
                rpeView.Show();
            });

            this.cmdShowUploadRouteView = new RelayCommand(() =>
            {
                CloseWindows();
                //HideAllIReaderGeometry();
                //CleanAllRenderPoint();
                if (_routePtList != null)// _polyline _routePtList != null
                {
                    _uploadRouteTipsViewModel._uploadRoute -= new UploadRoute(ToUploadRoute);
                    _uploadRouteTipsViewModel._uploadRoute += new UploadRoute(ToUploadRoute);
                    _uploadRouteTipsViewModel?.OnChecked();
                }
                else
                {
                    UploadRouteTipsViewModel.IsSelected = false;
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
                }
            });

            pipeLinesInterSet = new List<IRenderPipeLine>();
        }

        /// <summary>
        /// Route Plan Busying Process
        /// </summary>
        private void BeginLoadDsProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartLoading"));
            });
        }

        /// <summary>
        /// Busying Loading
        /// </summary>
        /// <param name="msg"></param>
        private void LoadingDsProcess(string msg = "")
        {
            if (string.IsNullOrEmpty(msg))
                msg = Helpers.ResourceHelper.FindKey("Loading");
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = msg;
            });
        }

        /// <summary>
        /// Busying Finished
        /// </summary>
        private void FinishLoadProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = string.Empty;
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
            });
        }

        /// <summary>
        ///上传航线
        /// </summary>
        /// <param name="routeName"></param>
        private void ToUploadRoute(string routeName)
        {
            if (routeName != null)
            {
                CloseWindows();
                CleanAllRenderPoint();
                this.createGeoJson(routeName);
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("RouteUploadWarn"));
            }
        }

        private void HideAllIReaderGeometry()
        {
            var pools = GviMap.TempRObjectPool;
            foreach (var item in pools)
            {
                IRenderable renderable = GviMap.TempRObjectPool[item.Key] as IRenderable;
                renderable.VisibleMask = gviViewportMask.gviViewNone;
            }
        }

        public void releaseWindow()
        {
            _routePlanView = null;
            _gridUI = null;
            this.OnUnchecked();
            Console.WriteLine("-----------CloseWindow");
        }

        public void OnUnchecked()
        {
            ClearAllLines();

            UnRegisterDrawLineEvent();
            UnRegisterEditLineEvent();

            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;

            _isAutoDetection = false;
            if (_routePlanView != null)
            {
                _routePlanView?.Close();
                _routePlanView = null;
            }
        }

        private RelayCommand _inputFileCommand;

        public RelayCommand InputFileCommand
        {
            get { return _inputFileCommand ?? (_inputFileCommand = new RelayCommand(OnInputFileCommand)); }
            set { _inputFileCommand = value; }
        }

        private RelayCommand _drawRouteCommand;
        public RelayCommand DrawRouteCommand
        {
            get { return _drawRouteCommand ?? (_drawRouteCommand = new RelayCommand(OnDrawRouteCommand)); }
            set { _drawRouteCommand = value; }
        }

        private void OnDrawRouteCommand()
        {
            _polyline = null;
            CloseWindows();
            HideAllIReaderGeometry();
            this.DrawRouteOpen = true;
            CleanAllRenderPoint();
        }

        private bool _drawRouteOpen;
        public bool DrawRouteOpen
        {
            get { return _drawRouteOpen; }
            set { _drawRouteOpen = value; base.NotifyPropertyChanged("DrawRouteOpen"); }
        }

        private RelayCommand _exportCommand;
        public RelayCommand ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new RelayCommand(OnExportCommad)); }
            set { _exportCommand = value; }
        }

        private void OnExportCommad()
        {
            CloseWindows();
            CleanAllRenderPoint();
            this.ExportOpen = true;
        }

        private bool _exportOpen;
        public bool ExportOpen
        {
            get { return _exportOpen; }
            set { _exportOpen = value; base.NotifyPropertyChanged("ExportOpen"); }
        }
        private RelayCommand _editCommand;
        private RelayCommand _routePlanCommand;

        public RelayCommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand(OnEditCommand)); }
            set { _editCommand = value; }
        }


        private bool _editwaypointOpen;

        public bool EditwaypointOpen
        {
            get { return _editwaypointOpen; }
            set { _editwaypointOpen = value; base.NotifyPropertyChanged("EditwaypointOpen"); }
        }

        private bool _inputFileOpen;

        public bool InputFileOpen
        {
            get { return _inputFileOpen; }
            set { _inputFileOpen = value; base.NotifyPropertyChanged("InputFileOpen"); }
        }


        private void OnInputFileCommand()
        {
            CleanAllRenderPoint();
            CloseWindows();
            InputFileOpen = true;
        }

        public void CloseWindows()
        {
            CleanRenderPointAndTransformHelper();
            if (_routePlanShowPageModel != null)
            {
                _routePlanShowPageModel.releaseWindow();
            }
            if (_routeListModel != null)
            {
                _routeListModel.releaseWindow();
                
            }
            if (_routePoitEditViewModel != null)
            {
                _routePoitEditViewModel.releaseWindow();
            }
            if (_uploadRouteTipsViewModel != null)
            {
                _uploadRouteTipsViewModel.releaseWindow();
            }
           // GviMap.AxMapControl.RcRButtonUp -= AxMapControl_RcRButtonUp;
            GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect1;
        }


        private void OnEditCommand()
        {

        }

        /// <summary>
        /// 规划航线初始化
        /// </summary>
        public void initRoutePlan()
        {
            tmpList = new List<IRObject>();
            if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanLineKey))
            {
                var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ);
                polyline.SpatialCRS = GviMap.SpatialCrs;
                var rline = GviMap.ObjectManager.CreateRenderPolyline(polyline as IPolyline, null);
                GviMap.TempRObjectPool.Add(_RoutePlanLineKey, rline);
            }

            if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanMultiPtKey))
            {
                var mulPt = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
                mulPt.SpatialCRS = GviMap.SpatialCrs;
                var ptsym = new SimplePointSymbol();
                ptsym.FillColor = Color.Red;
                ptsym.Size = 15;
                IRenderMultiPoint rmulPt = GviMap.ObjectManager.CreateRenderMultiPoint(mulPt, ptsym);
                GviMap.TempRObjectPool.Add(_RoutePlanMultiPtKey, rmulPt);
            }

            if (!GviMap.TempRObjectPool.ContainsKey(_RouteNoInterSetMultiLineKey))
            {
                var mulLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
                mulLine.SpatialCRS = GviMap.SpatialCrs;
                var ptsym = new CurveSymbol();
                ptsym.Color = Color.Blue;
                var rmulPt = GviMap.ObjectManager.CreateRenderMultiPolyline(mulLine, ptsym);
                GviMap.TempRObjectPool.Add(_RouteNoInterSetMultiLineKey, rmulPt);
            }

            if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanPipeLineRedKey))
            {
                var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ);
                polyline.SpatialCRS = GviMap.SpatialCrs;
                var ptsym = new CurveSymbol();
                ptsym.Color = Color.Red;
                var rline = GviMap.ObjectManager.CreateRenderPolyline(polyline as IPolyline, ptsym);
                GviMap.TempRObjectPool.Add(_RoutePlanPipeLineRedKey, rline);
            }

            if (!GviMap.TempRObjectPool.ContainsKey(_RouteInterSetMultiLineKey))
            {
                var mulLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
                mulLine.SpatialCRS = GviMap.SpatialCrs;
                var ptsym = new CurveSymbol();
                ptsym.Color = Color.Red;
                var rmulPt = GviMap.ObjectManager.CreateRenderMultiPolyline(mulLine, ptsym);
                GviMap.TempRObjectPool.Add(_RouteInterSetMultiLineKey, rmulPt);
            }

            ClearPipeLine();

            RegisterDrawLineEvent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public void showRoutePlanView()
        {
            if (this._routePlanView == null)
            {
                this._routePlanView = new RoutePlanView();
                this._routePlanView.Owner = System.Windows.Application.Current.MainWindow;

                //释放窗口
                this._routePlanView.Closed += (sender, e) =>
                {
                    this._routePlanView = null;
                };
            }
            else
            {
                this._routePlanView.WindowStartupLocation = WindowStartupLocation.Manual;
                this._routePlanView.Top = 100;
                this._routePlanView.Left = 100;
                this._routePlanView.Show();
            }
        }

        public void ShowGridUI()
        {
            if (_gridUI == null)
            {
                _gridUI = new GridUI();
                _gridUI.Closed += (sender, e) => { _gridUI = null; };
            }
            _gridUI.Show();
        }

        public void hideRoutePlanView()
        {
            if (_routePlanView == null)
            {
                _routePlanView = new RoutePlanView();
                _routePlanView.Closed += (sender, e) => { _routePlanView = null; };
            }
            _routePlanView.Hide();
            initRoutePlan();
        }

        public void CleanRenderPointAndTransformHelper()
        {

            if (renderPointList?.Count > 0)
            {
                for (int i = 0; i < renderPointList.Count; i++)
                {
                    GviMap.ObjectManager.DeleteObject(renderPointList[i].Guid);
                }
            }
            if (tableLabelList?.Count > 0)
            {
                foreach (ITableLabel item in tableLabelList)
                {
                    GviMap.ObjectManager.DeleteObject(item.Guid);
                }
            }
            renderPointList?.Clear();
            IVector3 v = new Vector3();
            v.Set(0, 0, 0);
            GviMap.TransformHelper.SetPosition(v);
        }

        private void ClearPipeLine()
        {
            foreach (var item in pipeLinesInterSet)
                GviMap.ObjectManager.DeleteObject(item.Guid);
            pipeLinesInterSet.Clear();
        }

        private void ClearAllLines()
        {
            CleanRenderPointAndTransformHelper();
            if (_polyline != null)
            {
                IRenderable renderable = null;
                renderable = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderable;
                renderable.VisibleMask = gviViewportMask.gviViewNone;

                renderable = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderable;
                renderable.VisibleMask = gviViewportMask.gviViewNone;

                if (_isAutoDetection)
                {
                    renderable = GviMap.TempRObjectPool[_RouteNoInterSetMultiLineKey] as IRenderable;
                    renderable.VisibleMask = gviViewportMask.gviViewNone;

                    renderable = GviMap.TempRObjectPool[_RouteInterSetMultiLineKey] as IRenderable;
                    renderable.VisibleMask = gviViewportMask.gviViewNone;

                    renderable = GviMap.TempRObjectPool[_RoutePlanPipeLineKey] as IRenderable;
                    renderable.VisibleMask = gviViewportMask.gviViewNone;

                    renderable = GviMap.TempRObjectPool[_RoutePlanPipeLineRedKey] as IRenderable;
                    renderable.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
        }

        /// <summary>
        /// 更新渲染对象
        /// </summary>
        /// <param name="height"></param>
        private void UpdateRenderGeometry(double height)
        {
            if (_routePoitEditViewModel?.isEditing == true)
            {
                for (int i = 0; i < renderPointList.Count; i++)
                {
                    if (renderPointList[i].Guid == selectedPt?.Guid)
                    {
                        var rPoint = renderPointList[i].RenderPoint;
                        var pt = rPoint.GetFdeGeometry() as IPoint;
                        pt.Z = height;
                        rPoint.SetFdeGeometry(pt);
                        renderPointList[i].RenderPoint = rPoint;
                        IVector3 v = new Vector3();
                        v.Set(pt.X, pt.Y, pt.Z);
                        GviMap.AxMapControl.TransformHelper.SetPosition(v);
                        RedrawRenderGemometry();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 拿到总数跟位序
        /// </summary>
        private void GetLenAndIndex()
        {
            ptCount = renderPointList.Count;
            for (int i = 0; i < renderPointList.Count; i++)
            {
                if (selectedPt.Guid == renderPointList[i].Guid)
                {
                    selectIndex = i + 1;
                }
            }
        }

        private void RegisterDrawLineEvent()
        {
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("Linedrawn"), DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }
            Thread.Sleep(10);
            RCDrawManager.Instance.PolylineDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished += PolylineDraw_OnDrawFinished;
        }

        /// <summary>
        /// 航点编辑
        /// </summary>
        /// <param name="renderPolyline"></param>
        private void RegisterEditLineEvent(IRenderPolyline renderPolyline)
        {
            //画点
            if (renderPointList != null)
            {
                for (int i = 0; i < renderPointList.Count; i++)
                {
                    GviMap.ObjectManager.DeleteObject(renderPointList[i].Guid);
                }
            }
            IPolyline polyline = renderPolyline.GetFdeGeometry() as IPolyline;
            rPolyline = renderPolyline;
            PushGuidToList(polyline);
            if (renderPolyline != null)
            {
                GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractSelect;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect1;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect1;
            }
            GviMap.AxMapControl.TransformHelper.Type = gviEditorType.gviEditorMove;
            IVector3 v = new Vector3();
            var defaultPt = polyline.GetPoint(0);
            if (defaultPt == null) return;
            if (_routePtList?.Count > 0)
            {
                UpdateRouteEditView(defaultPt.X, defaultPt.Y, defaultPt.Z, _routePtList[0].Speed, _routePtList[0].Hover, _routePtList[0].Trigger);
            }
            else
            {
                UpdateRouteEditView(defaultPt.X, defaultPt.Y, defaultPt.Z, 2, 2, 0);
            }
            v.Set(defaultPt.X, defaultPt.Y, defaultPt.Z);
            GviMap.AxMapControl.TransformHelper.SetPosition(v);

            GviMap.AxMapControl.RcTransformHelperBegin -= AxMapControl_RcTransformHelperBegin1;
            GviMap.AxMapControl.RcTransformHelperMoving -= AxMapControl_RcTransformHelperMoving1;
            GviMap.AxMapControl.RcTransformHelperEnd -= AxMapControl_RcTransformHelperEnd1;

            GviMap.AxMapControl.RcTransformHelperBegin += AxMapControl_RcTransformHelperBegin1;
            GviMap.AxMapControl.RcTransformHelperMoving += AxMapControl_RcTransformHelperMoving1;
            GviMap.AxMapControl.RcTransformHelperEnd += AxMapControl_RcTransformHelperEnd1;

           // GviMap.AxMapControl.RcRButtonUp -= AxMapControl_RcRButtonUp;
          //  GviMap.AxMapControl.RcRButtonUp += AxMapControl_RcRButtonUp;

        }

        private bool AxMapControl_RcMouseMove(uint Flags, int X, int Y)
        {
            if (isRoutePointAdd || isRoutePointInsert)
            {
                if (renderPointList?.Count > 0)
                {
                    GviMap.Camera.ScreenToWorld(X, Y, out movingPt);
                    if (movingPt == null)
                        return false;
                    IPolyline polyline = null;
                    if (isRoutePointAdd)
                    {
                        List<IPoint> ptList = new List<IPoint>();
                        for (int i = 0; i < renderPointList.Count; i++)
                        {
                            var pt = renderPointList[i].RenderPoint.GetFdeGeometry() as IPoint;
                            ptList.Add(pt);
                        }
                        if (renderPointList.Count == ptCount)
                        {
                            ptList.Add(movingPt);
                        }
                        else
                        {
                            ptList[ptCount] = movingPt;
                        }
                        polyline = GviMap.GeoFactory.CreatePolyline(ptList, GviMap.SpatialCrs);
                    }
                    else
                    {
                        polyline = rPolyline.GetFdeGeometry() as IPolyline;
                        if (polyline.PointCount != renderPointList.Count)
                        {
                            polyline.RemovePoints(selectIndex, 1);
                        }
                        polyline.AddPointAfter(selectIndex - 1, movingPt);
                    }

                    var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
                    rpolygon1?.SetFdeGeometry(null);
                    rpolygon1?.SetFdeGeometry(polyline);
                    rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
            }

            return true;
        }

        //private bool AxMapControl_RcRButtonUp(uint Flags, int X, int Y)
        //{
        //    ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        //    contextMenuStrip.Items.Add("添加航点", null, OnAddRoutePt);
        //   // contextMenuStrip.Items.Add("插入航点", null, OnInsertRoutePt);
        //    contextMenuStrip.Items.Add("删除航点", null, OnDeleteRoutePt);
        //    contextMenuStrip.Show(X, Y);
        //    return false;
        //}

        private void OnInsertRoutePt()
        {
            isRoutePointInsert = true;
            GviMap.AxMapControl.RcMouseMove += AxMapControl_RcMouseMove;
        }

        private void OnDeleteRoutePt()
        {
            DeleteOrInsertRoutePt(true);
        }

        private void OnAddRoutePt()
        {
            isRoutePointAdd = true;
            GviMap.AxMapControl.RcMouseMove += AxMapControl_RcMouseMove;
        }

        private void AxMapControl_RcTransformHelperEnd1()
        {

        }

        private void AxMapControl_RcTransformHelperMoving1(IVector3 Position)
        {
            _routePoitEditViewModel.isEditing = false;
            for (int i = 0; i < renderPointList.Count; i++)
            {
                if (renderPointList[i].Guid == selectedPt?.Guid)
                {
                    var rPoint = renderPointList[i].RenderPoint;
                    var pt = rPoint.GetFdeGeometry() as IPoint;
                    pt.SetPostion(Position.X, Position.Y, Position.Z);
                    rPoint.SetFdeGeometry(pt);
                    renderPointList[i].RenderPoint = rPoint;
                    GviMap.ObjectManager.DeleteObject(tableLabelList[i].Guid);
                    UpdateRouteEditView(Position.X, Position.Y, Position.Z, -1, -1, -1);
                    RedrawRenderGemometry();
                    break;
                }
            }
            _routePoitEditViewModel.isEditing = true;
        }

        private void AxMapControl_RcTransformHelperBegin1()
        {

        }

        private void CleanAllRenderPoint()
        {
            GviMap.AxMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
            if (renderPointList == null) return;
            for (int i = 0; i < renderPointList.Count; i++)
            {
                GviMap.ObjectManager.DeleteObject(renderPointList[i].Guid);
            }
        }

        /// <summary>
        /// 保存guid
        /// </summary>
        /// <param name="polyLine"></param>
        private void PushGuidToList(IPolyline polyLine)
        {
            if (polyLine == null) return;
            if (renderPointList != null)
            {
                for (int i = 0; i < renderPointList.Count; i++)
                {
                    GviMap.ObjectManager.DeleteObject(renderPointList[i].Guid);
                }
            }
            renderPointList = new List<CurRenderPoint>();
            ISimplePointSymbol pointSymbol = new SimplePointSymbol();
            pointSymbol.Size = 15;
            pointSymbol.Color = Color.YellowGreen;
            ptCount = polyLine.PointCount;
            CleanRenderPointAndTransformHelper();
            tableLabelList = new List<ITableLabel>();

            if(!_isContinueEdit)
            { 
                _routePtList = new List<RoutePoint>();//[!1]Tips
            }

            for (int i = 0; i < polyLine.PointCount; i++)
            {
                var position = polyLine.GetPoint(i);

                if (!_isContinueEdit)
                {
                    RoutePoint routePt = new RoutePoint(position.X, position.Y, position.Z, 2, 2, 0, 0);
                    _routePtList.Add(routePt); //[!1]Tips
                }

                //画点
                var tmpRenderPt = GviMap.ObjectManager.CreateRenderPoint(polyLine.GetPoint(i), pointSymbol);
                CurRenderPoint newRenderPt = new CurRenderPoint();
                newRenderPt.Guid = tmpRenderPt.Guid;
                newRenderPt.RenderPoint = tmpRenderPt;
                renderPointList.Add(newRenderPt);
                tableLabelList.Add(CreatTableLabel(position, i));
                if (i == 0)
                {
                    selectedPt = tmpRenderPt;
                    selectIndex = 1;
                }

            }
            var tmpPt = polyLine.GetPoint(0);
            if (tmpPt == null)
            {
                _routePoitEditViewModel?.releaseWindow();
                return;
            }
            IVector3 v = new Vector3();
            v.Set(tmpPt.X, tmpPt.Y, tmpPt.Z);
            GviMap.AxMapControl.TransformHelper.SetPosition(v);
            if (_routePtList?.Count > 0)
            {
                UpdateRouteEditView(tmpPt.X, tmpPt.Y, tmpPt.Z, _routePtList[0].Speed, _routePtList[0].Hover, _routePtList[0].Trigger);
            }
            else
            {
                UpdateRouteEditView(tmpPt.X, tmpPt.Y, tmpPt.Z, -1, -1, -1);
            }
;
        }

        private ITableLabel CreatTableLabel(IPoint pt, int i)
        {
            ITableLabel tmpTableLabel = GviMap.ObjectManager.CreateTableLabel(1, 1, new System.Guid());
            tmpTableLabel.Position = pt;
            tmpTableLabel.TitleText = "       ";
            tmpTableLabel.SetRecord(0, 0, i + 1 + "");
            tmpTableLabel.BorderColor = Color.FromArgb(255, 255, 255, 255);
            tmpTableLabel.BorderWidth = 1;
            tmpTableLabel.TableBackgroundColor = Color.FromArgb(200, 255, 255, 165);
            tmpTableLabel.TitleBackgroundColor = Color.FromArgb(180, 122, 122, 122);
            TextAttribute headerTextAttribute = new TextAttribute();
            headerTextAttribute.TextColor = Color.FromArgb(120, 127, 64, 0);
            headerTextAttribute.OutlineColor = Color.Red;
            headerTextAttribute.Font = "细黑";
            headerTextAttribute.Bold = true;
            headerTextAttribute.MultilineJustification = gviMultilineJustification.gviMultilineCenter;
            tmpTableLabel.SetColumnTextAttribute(0, headerTextAttribute);
            return tmpTableLabel;
        }

        private void UpdateRouteEditView(double lng, double lat, double height, double speed, double hover, double trigger)
        {
            _routePoitEditViewModel.update -= new Update(UpdateRenderGeometry);
            _routePoitEditViewModel.flyToPoint -= new FlyToPoint(SelectRoutePointByIndex);
            _routePoitEditViewModel.deleteRoutePoint -= new DeleteRoutePoint(DeleteOrInsertRoutePt);
            _routePoitEditViewModel.updateRoutePoint -= new UpdateRoutePoint(UpdateRoutePointByType);
            _routePoitEditViewModel.update += new Update(UpdateRenderGeometry);
            _routePoitEditViewModel.flyToPoint += new FlyToPoint(SelectRoutePointByIndex);
            _routePoitEditViewModel.deleteRoutePoint += new DeleteRoutePoint(DeleteOrInsertRoutePt);
            _routePoitEditViewModel.updateRoutePoint += new UpdateRoutePoint(UpdateRoutePointByType);
            _routePoitEditViewModel.addpointIntoList -= new AddPointInToList(OnInsertRoutePt);
            _routePoitEditViewModel.addpointIntoList += new AddPointInToList(OnInsertRoutePt);
            _routePoitEditViewModel.OnCloseEvent -= CancelContinueEdit;
            _routePoitEditViewModel.OnCloseEvent += CancelContinueEdit;
            _routePoitEditViewModel?.OnChecked();
            _routePoitEditViewModel?.SetParameter(lng, lat, height, speed, hover, trigger, false);
            _routePoitEditViewModel.SelectPointCount = ptCount;
            if (_routePoitEditViewModel.SelectPointIndex != selectIndex)
            {          
                _routePoitEditViewModel.SelectPointIndex = selectIndex;
            }
        }

        private void CancelContinueEdit()
        {
            _isContinueEdit = false;

            if (_polyline != null)
            {

                if (_isAutoDetection)
                {
                    var polyLine = _polyline.Clone() as IPolyline;
                    // 飞行安全管道
                    var rLInes = CreateFlyPipe(polyLine);
                    //安全性检查:禁飞区+碰撞检测
                    if (IsTouchedFeature(rLInes, out IMultiPolyline linesInterset, out IMultiPolyline linesNoInterset,
                        out List<int> segIndexs))
                    {
                        var rMutlLine1 = GviMap.TempRObjectPool[_RouteInterSetMultiLineKey] as IRenderMultiPolyline;
                        rMutlLine1.SetFdeGeometry(linesInterset);
                        rMutlLine1.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        for (int i = 0; i < polyLine.SegmentCount; i++)
                        {
                            if (segIndexs.Contains(i))
                            {
                                var line = polyLine.GetSegment(i).ToPolyline(GviMap.GeoFactory, GviMap.SpatialCrs);
                                //var rPipeInterset = GviMap.ObjectManager.CreateRenderPipeLine(line, GviMap.ProjectTree.RootID);
                                //rPipeInterset.Color = Color.Red;
                                //rPipeInterset.Radius = 2.05f;

                                var rpolyline = GviMap.TempRObjectPool[_RoutePlanPipeLineRedKey] as IRenderPolyline;
                                rpolyline.SetFdeGeometry(line);
                                rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            }
                        }
                    }

                    var rMutlLine = GviMap.TempRObjectPool[_RouteNoInterSetMultiLineKey] as IRenderMultiPolyline;
                    rMutlLine.SetFdeGeometry(rLInes);
                    rMutlLine.VisibleMask = gviViewportMask.gviViewAllNormalView;

                    var intersetLines = IsSafeNoFlyZone(polyLine);

                    IRenderPipeLine rPipe;
                    if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanPipeLineKey))
                    {
                        rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
                        GviMap.TempRObjectPool.Add(_RoutePlanPipeLineKey, rPipe);
                        //GviMap.Camera.FlyToObject(rPipe.Guid, gviActionCode.gviActionFlyTo);
                    }
                    else
                    {
                        rPipe = GviMap.TempRObjectPool[_RoutePlanPipeLineKey] as IRenderPipeLine;
                        GviMap.ObjectManager.DeleteObject(rPipe.Guid);
                        rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
                        GviMap.TempRObjectPool[_RoutePlanPipeLineKey] = rPipe;
                    }
                }
            }
        }

        private void UpdateRoutePointByType(string type, double value)
        {
            if (selectIndex < 1 || selectIndex > _routePtList.Count || _routePtList.Count == 0) return;
            switch (type)
            {
                case "lng":
                    _routePtList[selectIndex - 1].Lng = value;
                    break;
                case "lat":
                    _routePtList[selectIndex - 1].Lat = value;
                    break;
                case "height":
                    _routePtList[selectIndex - 1].Height = value;
                    break;
                case "speed":
                    _routePtList[selectIndex - 1].Speed = value;
                    break;
                case "hover":
                    _routePtList[selectIndex - 1].Hover = value;
                    break;
                case "trigger":
                    _routePtList[selectIndex - 1].Trigger = value;
                    break;
                default:
                    break;
            }
        }

        private void DeleteOrInsertRoutePt(bool type)
        {
            for (int i = 0; i < renderPointList.Count; i++)
            {
                if (selectedPt.Guid == renderPointList[i].Guid)
                {
                    IPolyline tempPloyline = rPolyline.GetFdeGeometry() as IPolyline;
                    if (type)
                    {
                        tempPloyline.RemovePoints(i, 1);
                        _routePtList.RemoveAt(i);
                    }
                    else
                    {
                        RoutePoint routePt = new RoutePoint(pickPt.X, pickPt.Y, pickPt.Z, 2, 2, 0, 0);
                        _routePtList.Insert(i + 1, routePt);
                    }
                    RedrawRenderPolyline(tempPloyline);
                    _polyline = tempPloyline.Clone() as IPolyline;
                    PushGuidToList(tempPloyline);
                    break;
                }
            }
        }


        private void RedrawRenderGemometry()
        {
            List<IPoint> ptList = new List<IPoint>();
            for (int i = 0; i < renderPointList.Count; i++)
            {
                var pt = renderPointList[i].RenderPoint.GetFdeGeometry() as IPoint;
                ptList.Add(pt);
            }
            var polyline = GviMap.GeoFactory.CreatePolyline(ptList, GviMap.SpatialCrs);
            if (polyline == null) return;
            _polyline = polyline.Clone() as IPolyline;
            RedrawRenderPolyline(polyline);
            ReDrawTableLabel();
        }

        private void ReDrawTableLabel()
        {
            for (int i = 0; i < tableLabelList.Count; i++)
            {
                GviMap.ObjectManager.DeleteObject(tableLabelList[i].Guid);
            }
            tableLabelList = new List<ITableLabel>();
            for (int i = 0; i < renderPointList.Count; i++)
            {
                var pt = renderPointList[i].RenderPoint.GetFdeGeometry() as IPoint;
                var tmpTabelLabel = this.CreatTableLabel(pt, i);
                tableLabelList.Add(tmpTabelLabel);
            }
        }

        /// <summary>
        /// 更新航点
        /// </summary>
        /// <param name="routePt"></param>
        /// <param name="type"></param>
        /// <param name="index"></param>
        private void UpdateRoutePtList()
        {
            for (int i = 0; i < _polyline.PointCount; i++)
            { }
        }

        private void RedrawRenderPolyline(IPolyline polyline)
        {
            var rpolyline = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            rpolyline?.SetFdeGeometry(polyline);
            rPolyline = rpolyline;
            rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        public void SelectRoutePointByIndex(int index)
        {
            if (index > renderPointList.Count - 1) return;
            selectedPt = renderPointList[index].RenderPoint;
            var tempPt = selectedPt.GetFdeGeometry() as IPoint;
            PickPoint(selectedPt, tempPt);
        }


        /// <summary>
        /// 鼠标左键点击事件
        /// </summary>
        /// <param name="PickResult"></param>
        /// <param name="IntersectPoint"></param>
        /// <param name="Mask"></param>
        /// <param name="EventSender"></param>
        private void AxMapControl_RcMouseClickSelect1(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            GviMap.AxMapControl.RcMouseMove -= AxMapControl_RcMouseMove;
            if (PickResult == null) return;
            var type = PickResult.Type;
            pickPt = IntersectPoint;
            if (isRoutePointAdd == true)
            {
                ISimplePointSymbol pointSymbol = new SimplePointSymbol();
                pointSymbol.Size = 15;
                pointSymbol.Color = Color.YellowGreen;
                var tmpPt = GviMap.ObjectManager.CreateRenderPoint(IntersectPoint, pointSymbol);
                CurRenderPoint newRenderPt = new CurRenderPoint();
                newRenderPt.Guid = tmpPt.Guid;
                newRenderPt.RenderPoint = tmpPt;
                renderPointList.Add(newRenderPt);
                RedrawRenderGemometry();
                GetPickRenderPoint(renderPointList[renderPointList.Count - 1].RenderPoint);
                RoutePoint routePt = new RoutePoint(IntersectPoint.X, IntersectPoint.Y, IntersectPoint.Z, 0, 0, 0, 0);
                _routePtList.Add(routePt);
                isRoutePointAdd = false;
                return;
            }
            if (isRoutePointInsert == true)
            {
                DeleteOrInsertRoutePt(false);
                isRoutePointInsert = false;
            }
            if (PickResult is IRenderPointPickResult)
            {
                var pkPoint = PickResult as IRenderPointPickResult;
             
                if (_routePoitEditViewModel.WeatherIsChanged() && _routePoitEditViewModel.isSaved == false)
                {
                    var _alreadySave = Messages.ShowMessageDialog("保存", "尚未保存数据，是否返回修改");
                    if (_alreadySave)
                    {

                    }
                    else
                    {
                        PickPoint(pkPoint.RenderPoint, IntersectPoint);

                        _routePoitEditViewModel.SetValueAndState();
                    }
                }
                else
                {
                    PickPoint(pkPoint.RenderPoint, IntersectPoint);
                    _routePoitEditViewModel.SetValueAndState();
                }
            }
        }

        private void PickPoint(IRenderPoint PickPoint, IPoint IntersectPoint)
        {
            if (_routePoitEditViewModel != null)
            {
                _routePoitEditViewModel.isEditing = false;
            }
            if (PickPoint == null) return;


            GetPickRenderPoint(PickPoint);
            _routePoitEditViewModel.isEditing = true;
        }

        private void GetPickRenderPoint(IRenderPoint RenderPoint)
        {
            if (_routePoitEditViewModel.IsChangeAll)
            {
                _routePoitEditViewModel.IsChangeAll = false;
                int index = _routePoitEditViewModel.CurrentIndex - 1;
                for (int i = 0; i < _routePtList.Count; i++)
                {
                    _routePtList[i].Height = _routePtList[index].Height;
                    _routePtList[i].Speed = _routePtList[index].Speed;
                    _routePtList[i].Hover = _routePtList[index].Hover;
                    _routePtList[i].Trigger = _routePtList[index].Trigger;
                }

                for (int i = 0; i < renderPointList.Count; i++)
                {
                    var rPoint = renderPointList[i].RenderPoint;
                    var ptt = rPoint.GetFdeGeometry() as IPoint;
                    ptt.Z = _routePtList[index].Height;
                    rPoint.SetFdeGeometry(ptt);
                    renderPointList[i].RenderPoint = rPoint;
                }
            }
            //if (_isFirstSetDefault)
            //{
            //    _isFirstSetDefault = false;

            //    for (int i = 1; i < _routePtList.Count; i++)
            //    {
            //        _routePtList[i].Height = _routePtList[0].Height;
            //        _routePtList[i].Speed = _routePtList[0].Speed;
            //        _routePtList[i].Hover = _routePtList[0].Hover;
            //        _routePtList[i].Trigger = _routePtList[0].Trigger;
            //    }

            //    for (int i = 1; i < renderPointList.Count; i++)
            //    {
            //        var rPoint = renderPointList[i].RenderPoint;
            //        var ptt = rPoint.GetFdeGeometry() as IPoint;
            //        ptt.Z = _routePtList[0].Height;
            //        rPoint.SetFdeGeometry(ptt);
            //        renderPointList[i].RenderPoint = rPoint;
            //    }
            //}

            selectedPt = RenderPoint;
            var rptGuid = RenderPoint.Guid;
            GetLenAndIndex();
            var pt = RenderPoint.GetFdeGeometry() as IPoint;
            double lon = Math.Round(pt.X, 6);
            double lat = Math.Round(pt.Y, 6);
            double height = Math.Round(pt.Z, 6);
            IVector3 v = new Vector3();
            v.Set(pt.X, pt.Y, pt.Z);
            GviMap.AxMapControl.TransformHelper.SetPosition(v);
            double speed = 0;
            double hover = 0;
            double trigger = 0;
            if (selectIndex <= _routePtList.Count)
            {
                speed = _routePtList[selectIndex - 1].Speed;
                hover = _routePtList[selectIndex - 1].Hover;
                trigger = _routePtList[selectIndex - 1].Trigger;
            }
            UpdateRouteEditView(lon, lat, height, speed, hover, trigger);
        }


        private void AxMapControl_RcTransformHelperEnd()
        {

        }

        private void AxMapControl_RcTransformHelperMoving(IVector3 Position)
        {
        }

        private void AxMapControl_RcTransformHelperBegin()
        {

        }

        private void UnRegisterEditLineEvent()
        {
            RCDrawManager.Instance.PolylineEdit.OnDrawFinished -= PolylineEdit_OnDrawFinished;
            RCDrawManager.Instance.PolylineEdit.UnRegister(GviMap.AxMapControl);
        }

        private void PolylineEdit_OnDrawFinished(object sender, object result)
        {
            return;
        }

        private void UnRegisterDrawLineEvent()
        {
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
        }

        /// <summary>
        /// 航线绘制结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="result"></param>
        private void PolylineDraw_OnDrawFinished(object sender, object result)
        {
            if (_isAutoDetection)
            {
                RouteAdjustmentView view = new RouteAdjustmentView()
                {
                    Owner = Application.Current.MainWindow
                };
                view.OnConfirmAction = (d1, d2) =>
                {
                    GenerateRoute(result, d1, d2);
                };
                view.OnCancelAction = () =>
                {
                    GenerateRoute(result);
                };
                view.ShowDialog();
            }
            else
            {
                GenerateRoute(result);
            }
            //try
            //{
       
            //    Task.Run(() =>
            //    {
            //        BeginLoadDsProcess();
            //        rPolyline = result as IRenderPolyline;
            //        var polyLine = rPolyline.GetFdeGeometry() as IPolyline;

            //        polyLine.SpatialCRS = GviMap.SpatialCrs;

            //        if (_isAutoDetection)
            //        {
            //            //算法采样离散
            //            polyLine = DispaseEx(polyLine, _dispLength);
            //            //高度跟随与自动避让
            //            polyLine = AjustToFlyHeight(polyLine, true, _flyHeight = 20);
            //            //用户采样离散
            //            double customeLength = 30;
            //        }

            //        _routePtList = new List<RoutePoint>();
            //        for (int i = 0; i < polyLine.PointCount; i++)
            //        {
            //            IPoint tmpPt = polyLine.GetPoint(i);
            //            RoutePoint routePt = new RoutePoint(tmpPt.X, tmpPt.Y, tmpPt.Z, 2, 2, 0, 0);
            //            _routePtList.Add(routePt);
            //        }

            //        //绘制线
            //        var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            //        rpolygon1?.SetFdeGeometry(polyLine);
            //        rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

            //        ////绘制点
            //        //var rMulPt = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderMultiPoint;
            //        //var mulPt = rMulPt.GetFdeGeometry() as IMultiPoint;
            //        //mulPt.Clear();
            //        //for (int i = 0; i < polyLine.PointCount; i++)
            //        //{
            //        //    mulPt.AddGeometry(polyLine.GetPoint(i));
            //        //}
            //        //rMulPt.SetFdeGeometry(mulPt);
            //        //rMulPt.VisibleMask = gviViewportMask.gviViewAllNormalView;


            //        if (_isAutoDetection)
            //        {
            //            // 飞行安全管道
            //            var rLInes = CreateFlyPipe(polyLine);
            //            //安全性检查:禁飞区+碰撞检测
            //            if (IsTouchedFeature(rLInes, out IMultiPolyline linesInterset, out IMultiPolyline linesNoInterset, out List<int> segIndexs))
            //            {
            //                var rMutlLine1 = GviMap.TempRObjectPool[_RouteInterSetMultiLineKey] as IRenderMultiPolyline;
            //                rMutlLine1.SetFdeGeometry(linesInterset);
            //                rMutlLine1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //                for (int i = 0; i < polyLine.SegmentCount; i++)
            //                {
            //                    if (segIndexs.Contains(i))
            //                    {
            //                        var line = polyLine.GetSegment(i).ToPolyline(GviMap.GeoFactory, GviMap.SpatialCrs);
            //                        //var rPipeInterset = GviMap.ObjectManager.CreateRenderPipeLine(line, GviMap.ProjectTree.RootID);
            //                        //rPipeInterset.Color = Color.Red;
            //                        //rPipeInterset.Radius = 2.05f;

            //                        var rpolyline = GviMap.TempRObjectPool[_RoutePlanPipeLineRedKey] as IRenderPolyline;
            //                        rpolyline.SetFdeGeometry(line);
            //                        rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //                    }
            //                }
            //            }
            //            var rMutlLine = GviMap.TempRObjectPool[_RouteNoInterSetMultiLineKey] as IRenderMultiPolyline;
            //            rMutlLine.SetFdeGeometry(rLInes);
            //            rMutlLine.VisibleMask = gviViewportMask.gviViewAllNormalView;

            //            var intersetLines = IsSafeNoFlyZone(polyLine);

            //            IRenderPipeLine rPipe;
            //            if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanPipeLineKey))
            //            {
            //                rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
            //                GviMap.TempRObjectPool.Add(_RoutePlanPipeLineKey, rPipe);
            //                //GviMap.Camera.FlyToObject(rPipe.Guid, gviActionCode.gviActionFlyTo);
            //            }
            //            else
            //            {
            //                rPipe = GviMap.TempRObjectPool[_RoutePlanPipeLineKey] as IRenderPipeLine;
            //                GviMap.ObjectManager.DeleteObject(rPipe.Guid);
            //                rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
            //                GviMap.TempRObjectPool[_RoutePlanPipeLineKey] = rPipe;
            //            }
            //        }

            //        _polyline = polyLine;

            //        //if (_isAutoDetection)
            //        //{
            //        //    var parmModel = new ParametricModelling();
            //        //    parmModel.PolylineToPipeLine(polyLine, null, out IModelPoint mp, out IModel model);
            //        //    GviMap.ObjectManager.CreateRenderModelPoint(mp, null);
            //        //}
            //        FinishLoadProcess();
            //        UnRegisterDrawLineEvent();
            //    });

            //}
            //catch (Exception ex)
            //{
            //    SystemLog.Log(ex);
            //    FinishLoadProcess();
            //}

            
        }

        private void drawGeometry(IPolyline polyline)
        {
            if (polyline != null)
            {
                this._polyline = polyline.Clone() as IPolyline;
                this._polyline.ProjectEx(WKTString.WGS_84_WKT);
                if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanLineKey))
                {
                    var polyLines = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeNone);
                    polyLines.SpatialCRS = GviMap.SpatialCrs;
                    ICurveSymbol curveeSymbol = new CurveSymbol()
                    {
                        Color = Color.Red,
                        Width = .5f
                    };
                    var rpolyLine = GviMap.ObjectManager.CreateRenderPolyline(polyline, curveeSymbol);
                    GviMap.TempRObjectPool.Add(_RoutePlanLineKey, rpolyLine);
                }
                var rpolyline = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
                rpolyline.SetFdeGeometry(this._polyline);
                rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                polylineToPtlist(this._polyline);
            }
        }

        private void polylineToPtlist(IPolyline polyLine)
        {
            if (_routePtList == null)
                _routePtList = new List<RoutePoint>();//[!1]Tips
            _routePtList.Clear();
            for (int i = 0; i < polyLine.PointCount; i++)
            {
                var position = polyLine.GetPoint(i);
                RoutePoint routePt = new RoutePoint(position.X, position.Y, position.Z, 2, 2, 0, 0);
                _routePtList.Add(routePt);//[!1]Tips
            }
        }


        private void showInputPolyline(IPolyline polyline)
        {
            initRoutePlan();
            UnRegisterDrawLineEvent();

            //line            
            polyline.SpatialCRS = GviMap.SpatialCrs;
            rPolyline = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            rPolyline?.SetFdeGeometry(polyline);
            rPolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
            FlyToObject();
        }

        public void exportKmlFile()
        {
            kml loadkml = new kml();

            List<GeoCoordinate> coordinates = new List<GeoCoordinate>();

            for (int i = 0; i < _routePtList.Count; i++)
            {
                GeoCoordinate coord = new GeoCoordinate();
                coord.Longitude = _routePtList[i].Lng;
                coord.Latitude = _routePtList[i].Lat;
                coord.Altitude = _routePtList[i].Height;
                coordinates.Add(coord);
            }
            loadkml.Document.Add(new Placemark("", "", "colorID", coordinates));
            loadkml.SaveFileDialog();
        }

        public void exportDoublePointKmlFile()
        {
            kml loadkml = new kml();

            List<GeoCoordinate> coordinates = new List<GeoCoordinate>();

            for (int i = 0; i < _routePtList.Count; i++)
            {
                GeoCoordinate coord = new GeoCoordinate();
                coord.Longitude = _routePtList[i].Lng;
                coord.Latitude = _routePtList[i].Lat;
                coord.Altitude = _routePtList[i].Height;
                coordinates.Add(coord);
                coordinates.Add(coord);
            }
            loadkml.Document.Add(new Placemark("", "", "colorID", coordinates));
            loadkml.SaveFileDialog();
        }

        public void exportGeoJsonFile()
        {
            if (_generateGeoJson != null)
            {
                _generateGeoJson.SetGeoPointItem(_polyline);
                _generateGeoJson.SaveFileDialog();
            }
        }

        //上传到服务器
        public void createGeoJson(string routeName)
        {
            if (_generateGeoJson != null)
            {
                //_generateGeoJson.SetGeoPointItem(_polyline);//GeoJson
                _generateGeoJson._routeName = routeName;
                _generateGeoJson.setMMCStationMission(_routePtList);//MMC Station Mission

                bool result = _generateGeoJson.CreateGeoJson();
                //var postReturn = JsonUtil.DeserializeFromString<dynamic>(result);

                //sync
                if (result)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("UploadSucess"));
                    //var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    //var _RouteHost = json.poiUrl;
                    //string url = string.Format(@"{0}/flight-course/manual-list", _RouteHost);
                    string url = string.Format(@"{0}{1}", WebConfig.MspaceHostUrl, UAVInterface.TraceListPage);
                    _routePlanView?.Navigate(url);
                }
                else
                    Messages.ShowMessage(ResourceHelper.FindKey("UploadRouteError"));
            }
        }



        private GeoJson SetGeoPointItem(IPolyline polyline)
        {
            var geoJsonItem = new GeoJson();
            geoJsonItem.type = "FeatureCollection";
            geoJsonItem.features = new List<GeoJsonPoint>();

            for (int i = 0; i < polyline.PointCount; i++)
            {
                var item = polyline.GetPoint(i);

                var coordinate = new List<double>
                {
                    item.X,
                    item.Y,
                    item.Z
                };

                geoJsonItem.features.Add(new GeoJsonPoint
                {
                    type = "Featrue",
                    geometry = new GeoJsonPointGeometry
                    {
                        type = "Point",
                        altitudeMode = "absolute",
                        coordinates = coordinate
                    },
                    properties = new GeoJsonPointProperty
                    {
                        camposx = "",
                        camposy = "",
                        camheading = "",
                        camtilt = "",
                        camroll = "",
                        camcapture = "",
                        uavyaw = ""
                    }
                });

            }
            return geoJsonItem;
        }

        private void GetLocation()
        {
            GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;
            GviMap.InteractMode = gviInteractMode.gviInteractSelect;
            GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            //_label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //var view = (Window)base.View;
            //view.WindowStartupLocation = WindowStartupLocation.Manual;
            //view.Hide();
        }

        //in poi detailview models
        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    var pt = IntersectPoint;
                    //获取位置信息和相机姿态
                    GviMap.Camera.GetCamera(out IVector3 camPt, out IEulerAngle camAngle);
                    //_poiInfo.lng = pt.X;
                    //_poiInfo.lat = pt.Y;
                    //_poiInfo.alt = pt.Z;
                    //_poiInfo.heading = camAngle.Heading;
                    //_poiInfo.roll = camAngle.Roll;
                    //_poiInfo.pitch = camAngle.Tilt;
                    if (GviMap.PoiManager.TempRPoi == null)
                        GviMap.PoiManager.CreateTempRPoi();
                    var _poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                    //创建POI
                    _poi.X = pt.X;
                    _poi.Y = pt.Y;
                    _poi.Z = pt.Z;
                    _poi.ShowName = true;
                    //_poi.Name = _poiInfo.title;
                    _poi.Size = 32;

                    //var localurl = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, this.SelectedPoiType.cat_url);
                    //_poi.ImageName = localurl;
                    // poi.ImageName = poiInfo.icon_url;
                    _poi.SpatialCRS = GviMap.SpatialCrs;

                    //加入缓存管理
                    //if (!string.IsNullOrEmpty(_poiInfo.marker_id) && GviMap.PoiManager.ContainsKey(_poiInfo.cat_Name, _poiInfo.marker_id))
                    //{
                    //    GviMap.PoiManager.UpdatePoi(_poiInfo.marker_id, _poiInfo.cat_Name, _poi);
                    //}
                    //else
                    //{
                    //    GviMap.PoiManager.TempRPoi.SetFdeGeometry(_poi);
                    //    GviMap.PoiManager.TempRPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    //}

                    _label.VisibleMask = gviViewportMask.gviViewNone;
                    //取消事件注册
                    GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                    GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                    GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                    GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

                    //获取屏幕坐标
                    GviMap.Camera.WorldToScreen(pt.X, pt.Y, pt.Z, out double screenX, out double sreenY, 0, out bool inScreen);

                    //var view = (Window)base.View;
                    //view.Left = screenX + 30;
                    //view.Top = sreenY - view.ActualHeight / 2;
                    //view.Show();
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    var pt = IntersectPoint;
                    _label.Text = Helpers.ResourceHelper.FindKey("Markedlocation");
                    _label.Position = pt;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 判断航线是否穿过禁飞区
        /// </summary>
        /// <param name="polyline"></param>
        /// <returns></returns>
        public List<IPolyline> IsSafeNoFlyZone(IPolyline polyline)
        {
            var displayLayers = DataBaseService.Instance.GetShpLayers();
            List<IPolyline> linesInterset = new List<IPolyline>();
            //拿到禁飞区图层
            if (displayLayers != null)
                foreach (var item in displayLayers)
                {
                    var fc = item.Fc;
                    if (fc.Name == "禁飞区")
                    {
                        for (int i = 0; i < polyline.SegmentCount; i++)
                        {
                            var seg = polyline.GetSegment(i);
                            var line = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                            line.StartPoint = seg.StartPoint;
                            line.EndPoint = seg.EndPoint;

                            var spatialFilter = new SpatialFilter();
                            spatialFilter.Geometry = line;
                            spatialFilter.GeometryField = "Geometry";
                            spatialFilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                            var cursor = fc.Search(spatialFilter, true);
                            IRowBuffer row = null;
                            while ((row = cursor.NextRow()) != null)
                            {
                                linesInterset.Add(line);
                                break;
                            }
                            spatialFilter.ReleaseComObject();
                            cursor.ReleaseComObject();
                            row.ReleaseComObject();
                        }
                    }
                }
            return linesInterset;
        }


        /// <summary>
        /// 绘制管道进行碰撞检测
        /// </summary>
        /// <param name="multiPolyline"></param>
        /// <returns></returns>
        public bool IsTouchedFeature(IMultiPolyline multiPolyline, out IMultiPolyline linesInterset, out IMultiPolyline linesNoInterset, out List<int> SegIndex)
        {
            SegIndex = new List<int>();
            linesInterset = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
            linesInterset.SpatialCRS = GviMap.SpatialCrs;
            linesNoInterset = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
            linesNoInterset.SpatialCRS = GviMap.SpatialCrs;
            foreach (var item in DataBaseService.Instance.GetTileLayers())
            {
                var wkt = item.Layer.GetWKT();
                for (int i = 0; i < multiPolyline.GeometryCount; i++)
                {
                    var polyline = multiPolyline.GetPolyline(i);
                    for (int ii = 0; ii < polyline.SegmentCount; ii++)
                    {
                        var line = polyline.GetSegment(ii).ToPolyline(GviMap.GeoFactory, GviMap.SpatialCrs);
                        var line1 = line.Clone() as IPolyline;
                        line1.ProjectEx(wkt);
                        var index = ii / 2;
                        SystemLog.Log("IsTouchedFeature-----1-----");
                        if (item.Layer.PolylineIntersect(line1, out string fdsetName, out string fcName, out int fid, out IVector3 intertSetPt))
                        {
                            if (!SegIndex.Contains(index))
                                SegIndex.Add(index);
                            line1.Project(GviMap.SpatialCrs);
                            line1.SpatialCRS = GviMap.SpatialCrs;
                            linesInterset.AddGeometry(line1);

                        }
                        else
                        {
                            line1.Project(GviMap.SpatialCrs);
                            line1.SpatialCRS = GviMap.SpatialCrs;
                            linesNoInterset.AddGeometry(line1);
                        }
                        SystemLog.Log("IsTouchedFeature-----2-----");
                    }
                }
            }

            return linesInterset.GeometryCount > 0;
        }


        public void Dispase(IPolyline polyLine)
        {
            //离散点
            var rMulPt = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderMultiPoint;
            var mulPt = rMulPt.GetFdeGeometry() as IMultiPoint;
            mulPt.Clear();

            IVector3 temVect = null;
            var spitCount = 5;//离散线采样（默认5个）
                              // var spitLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            for (int i = 0; i < polyLine.SegmentCount; i++)
            {
                var seg = polyLine.GetSegment(i);
                var vStart = seg.StartPoint.ToVector3();
                var vEnd = seg.EndPoint.ToVector3();
                var vector = vEnd.Subtract(vStart);
                vector.Normalize();//单一化

                var length = seg.Length / spitCount;
                if (i == 0)
                    mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
                for (int j = 0; j < spitCount; j++)
                {

                    temVect = vStart.Add(vector.Multiply(length * (j + 1)));
                    mulPt.AddGeometry(temVect.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
                }
            }
            rMulPt.SetFdeGeometry(mulPt);
            rMulPt.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }


        public IPolyline DispaseEx(IPolyline polyLine, double disperLength)
        {

            var curSprs = GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPolyline _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _polyline.SpatialCRS = curSprs;
            polyLine.Project(curSprs);
            //离散点

            var mulPt = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint, gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            mulPt.SpatialCRS = curSprs;
            IVector3 temVect = null;

            for (int i = 0; i < polyLine.SegmentCount; i++)
            {
                var seg = polyLine.GetSegment(i);
                var vStart = seg.StartPoint.ToVector3();
                var vEnd = seg.EndPoint.ToVector3();
                var vector = vEnd.Subtract(vStart);
                vector.Normalize();//单一化


                var spitCount = Math.Floor(seg.Length / disperLength);//离散线采样点个数
                if (disperLength >= seg.Length)//采样距离大于线段距离
                {
                    mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, curSprs));
                    mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                }
                else
                {
                    if (i == 0)
                        mulPt.AddGeometry(vStart.ToPoint(GviMap.GeoFactory, curSprs));
                    for (int j = 0; j < spitCount; j++)
                    {

                        temVect = vStart.Add(vector.Multiply(disperLength * (j + 1)));
                        mulPt.AddGeometry(temVect.ToPoint(GviMap.GeoFactory, curSprs));
                    }
                    mulPt.AddGeometry(vEnd.ToPoint(GviMap.GeoFactory, curSprs));
                }
            }
            for (int i = 0; i < mulPt.GeometryCount; i++)
            {
                var pt = mulPt.GetPoint(i);
                _polyline.AppendPoint(pt);
            }

            _polyline.Project(GviMap.SpatialCrs);
            return _polyline;
        }

        /// <summary>
        /// 调整航线高度
        /// </summary>
        /// <param name="polyline"></param>
        /// <param name="isEscape">是否避让</param>
        /// <param name="flyHeight">飞行高度</param>
        /// <returns></returns>
        public IPolyline AjustToFlyHeight(IPolyline polyline, bool isEscape, double flyHeight)
        {
            IPolyline _polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _polyline.SpatialCRS = GviMap.SpatialCrs;
            if (isEscape)//自动避让
            {
                for (int i = 0; i < polyline.PointCount; i++)
                {
                    var pt = polyline.GetPoint(i);

                    var startPt = pt.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    startPt.SpatialCRS = GviMap.SpatialCrs;
                    startPt.Z = 10000;
                    _polyline.StartPoint = startPt;
                    var endPt = pt.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
                    endPt.SpatialCRS = GviMap.SpatialCrs;
                    endPt.Z = -100;
                    _polyline.EndPoint = endPt;
                    double z = -10;
                    foreach (var item in DataBaseService.Instance.GetTileLayers())
                    {
                        var wkt = item.Layer.GetWKT();
                        _polyline.ProjectEx(wkt);
                        SystemLog.Log("AjustToFlyHeight-----1----");
                        if (item.Layer.PolylineIntersect(_polyline, out string fdsetName, out string fcName, out int fid, out IVector3 intertSetPt))
                        {
                            z = z > intertSetPt.Z ? z : intertSetPt.Z;
                        }
                            
                        SystemLog.Log("AjustToFlyHeight-----2----");
                        _polyline.SpatialCRS = GviMap.SpatialCrs;
                    }
                    pt.Z = z + flyHeight;
                    //pt.Z = pt.Z + flyHeight;
                    //pt.Z = z ;
                    polyline.UpdatePoint(i, pt);
                }

            }
            else
            {
                for (int i = 0; i < polyline.PointCount; i++)
                {
                    var pt = polyline.GetPoint(i);
                    pt.Z = flyHeight;
                    polyline.UpdatePoint(i, pt);
                }
            }
            return polyline;
        }

        /// <summary>
        /// 创建飞行管道
        /// </summary>
        /// <param name="polylineCenter">中心线</param>
        /// <param name="r">半径</param>
        /// <param name="vCount">离散圆顶点数（默认30度）</param>
        /// <returns></returns>
        private IMultiPolyline CreateFlyPipe(IPolyline polylineCenter, float r = 2, int vCount = 4)
        {
            IMultiPolyline lines = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolyline;
            lines.SpatialCRS = GviMap.SpatialCrs;
            List<List<IPoint>> circlePts = new List<List<IPoint>>();


            for (int i = 0; i < polylineCenter.SegmentCount; i++)
            {
                // GetCirclePts(polylineCenter, r, vCount, lines, circlePts, out startPt, out endPt, out pts, i);
                var seg = polylineCenter.GetSegment(i);
                GetCirclePts1(seg, r = 1, vCount, lines, circlePts);
            }

            for (int i = 0; i < vCount; i++)
            {
                IPolyline startLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                startLine.SpatialCRS = GviMap.SpatialCrs;
                foreach (var item in circlePts)
                    startLine.AppendPoint(item[i]);
                lines.AddGeometry(startLine);
            }

            lines.Project(GviMap.SpatialCrs);
            polylineCenter.Project(GviMap.SpatialCrs);
            return lines;
        }

        private void GetCirclePts1(ISegment seg, float r, int vCount, IMultiPolyline lines, List<List<IPoint>> circlePts)
        {
            IPoint startPt, endPt;
            List<IPoint> pts;
            startPt = seg.StartPoint;
            startPt.SpatialCRS = GviMap.SpatialCrs;
            endPt = seg.EndPoint;
            endPt.SpatialCRS = GviMap.SpatialCrs;
            var vStart = startPt.ToVector3();
            var vEnd = endPt.ToVector3();
            var vector = vEnd.Subtract(vStart);//线段向量
            IVector3 vdoStart = null;
            var angles = new List<IEulerAngle>();
            //求法向量，通过相机欧拉角旋转90度获取法平面的任一向量
            var angle = GviMap.Camera.GetAimingAngles(vStart, vEnd);
            var changeAngle = angle.Clone();
            changeAngle.Heading += 90;
            angles.Add(changeAngle);

            changeAngle = angle.Clone();
            changeAngle.Heading -= 90;
            angles.Add(changeAngle);

            //changeAngle = angle.Clone();
            //changeAngle.Roll += 90;
            //angles.Add(changeAngle);

            //changeAngle = angle.Clone();
            //changeAngle.Roll -= 90;
            //angles.Add(changeAngle);

            changeAngle = angle.Clone();
            changeAngle.Tilt += 90;
            angles.Add(changeAngle);

            changeAngle = angle.Clone();
            changeAngle.Tilt -= 90;
            angles.Add(changeAngle);


            //最后一个线段
            vdoStart = vStart;
            pts = CreatePolyline(vdoStart, angles, r);
            circlePts.Add(pts);

            vdoStart = vEnd;
            pts = CreatePolyline(vdoStart, angles, r);
            circlePts.Add(pts);
        }

        private List<IPoint> CreatePolyline(IVector3 vdoStart, List<IEulerAngle> angles, double r)
        {
            var pts = new List<IPoint>();
            foreach (var angle1 in angles)
            {
                var norVector = GviMap.Camera.GetAimingPoint(vdoStart, angle1, r);
                pts.Add(norVector.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
            }
            return pts;
        }

        private void GetCirclePts2(ISegment seg, float r, int vCount, IMultiPolyline lines, List<List<IPoint>> circlePts)
        {
            IPoint startPt, endPt;
            List<IPoint> pts;
            startPt = seg.StartPoint;
            startPt.SpatialCRS = GviMap.SpatialCrs;
            endPt = seg.EndPoint;
            endPt.SpatialCRS = GviMap.SpatialCrs;
            var vStart = startPt.ToVector3();
            var vEnd = endPt.ToVector3();
            var vector = vEnd.Subtract(vStart);//线段向量
            IVector3 vdoStart = null;
            //求法向量，通过相机欧拉角旋转90度获取法平面的任一向量
            var angle1 = GviMap.Camera.GetAimingAngles(vStart, vEnd);
            angle1.Heading += 90;
            //angle.Tilt = 0;
            //angle.Roll = 0;
            var nor = GetVNormOfPlane(angle1);
            var vOrigin = new Vector3() { X = 0, Y = 0, Z = 0 };
            var angle = GviMap.Camera.GetAimingAngles(vOrigin, nor);
            IVector3 norVector;

            //最后一个线段
            vdoStart = vStart;
            norVector = GviMap.Camera.GetAimingPoint(vdoStart, angle1, r);
            // norVector = vdoStart.Add(nor);
            pts = CreatCircle(vCount, lines, vector, vdoStart, norVector);
            circlePts.Add(pts);

            vdoStart = vEnd;
            //norVector = vdoStart.Add(nor);
            norVector = GviMap.Camera.GetAimingPoint(vdoStart, angle1, r);
            pts = CreatCircle(vCount, lines, vector, vdoStart, norVector);
            circlePts.Add(pts);
        }

        /// <summary>
        /// 获取欧拉角的法平面向量
        /// </summary>
        /// <param name="angleCamera"></param>
        /// <returns></returns>
        private IVector3 GetVNormOfPlane(IEulerAngle angleCamera)
        {
            var matrixClass = new Matrix();
            IVector3 vector = new Vector3();
            IVector3 result = new Vector3();
            vector.Set(0.0, 0.0, 1.0);
            ((IMatrix)matrixClass).SetRotation(angleCamera);
            return ((IMatrix)matrixClass).MultiplyVector(vector);
        }

        private void GetPlaneParam(IClipPlaneOperation cp, out IVector3 vNorm, out double dConstPlane)
        {
            vNorm = null;
            dConstPlane = 0.0;
            IVector3 vector;
            IEulerAngle angleCamera;
            cp.GetSingleClip(out vector, out angleCamera);
            vNorm = this.GetVNormOfPlane(angleCamera);
            dConstPlane = vector.DotProduct(vNorm);
        }

        private IVector3 CameraAngleToVector(IVector3 vector, IEulerAngle angle)
        {
            IVector3 aimingPoint = GviMap.Camera.GetAimingPoint(vector, angle, 10.0);
            return new Vector3
            {
                X = vector.X - aimingPoint.X,
                Y = vector.Y - aimingPoint.Y,
                Z = vector.Z - aimingPoint.Z
            }.UnitVector();
        }
        private static List<IPoint> CreatCircle(int vCount, IMultiPolyline lines, IVector3 vector, IVector3 vdoStart, IVector3 norVector)
        {
            List<IPoint> pts = new List<IPoint>(vCount);
            IPolyline startLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            startLine.SpatialCRS = GviMap.SpatialCrs;
            var rad = ToRadians(360 / vCount);
            IPolyline tempLine;
            startLine.RemovePoints(0, startLine.PointCount);
            startLine.AppendPoint(vdoStart.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
            startLine.AppendPoint(norVector.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs));
            IVector3 temVect = norVector;

            //球面转平面
            var prjWkt = WKTString.PROJ_CGCS2000_WKT;

            var prjStar = vdoStart.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs);
            prjStar.ProjectEx(prjWkt);

            //以起点为中心点，线段向量为旋转轴旋转
            for (int ii = 0; ii < vCount; ii++)
            {
                tempLine = startLine.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                tempLine.ProjectEx(prjWkt);
                var tran = tempLine as ITransform;
                //tran.Rotate3D(vector.X, vector.Y, vector.Z, prjStar.X, prjStar.Y, prjStar.Z, rad * ii);
                tran.Rotate3D(vector.X, 0, 0, prjStar.X, prjStar.Y, prjStar.Z, rad * ii);
                tran.Rotate3D(0, vector.Y, 0, prjStar.X, prjStar.Y, prjStar.Z, rad * ii);
                tran.Rotate3D(0, 0, vector.Z, prjStar.X, prjStar.Y, prjStar.Z, rad * ii);
                //lines.AddPolyline(tempLine);

                tempLine.Project(GviMap.SpatialCrs);
                pts.Add(tempLine.EndPoint);
            }

            return pts;
        }

        public static double ToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        /// <summary>
        /// 画多段线
        /// </summary>
        public void DrawPolyline(double offX, double offY, double offZ, List<IVector3> vLines, ICurveSymbol cvSymbol)
        {
            //测试离散线
            IPolyline pLine = null;
            IPoint p = null;

            pLine = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            foreach (IVector3 vector in vLines)
            {
                p = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                p.X = offX + vector.X;
                p.Y = offY + vector.Y;
                p.Z = offZ + vector.Z;
                pLine.AppendPoint(p);
            }
            IRenderPolyline robj = GviMap.ObjectManager.CreateRenderPolyline(pLine, cvSymbol);
            // robj.MaxVisibleDistance = maxVisibleDis;
            tmpList.Add(robj);
        }

        #region 航线展示界面点击a标签调用
        public void ReadMissionJson(string[] missionJson)
        {
            try
            {
                int idCount = 0;
                double speed = 0;//如果不设置默认用前一个点的速度
                if (_routePtList == null)
                    _routePtList = new List<RoutePoint>();
                else
                    _routePtList.Clear();
                var json = JsonUtil.DeserializeFromString<dynamic>(missionJson[1]);
                var tmpPolyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                foreach (var item in json.items)
                {
                    double trigger = 0;//(double)item.trigger;
                    int isCameraTrigger = 0;//(int)item.isCameraTrigger;
                    IPoint tempPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    RoutePoint routePt = null;

                    if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_WAYPOINT ||
                        item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_TAKEOFF ||
                        item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_LAND
                        )
                    {
                        var coordArr = item.coordinate;
                        var coordX = (double)coordArr[0];
                        var coordY = (double)coordArr[1];
                        var coordZ = (double)coordArr[2];
                        var hover = (double)item.param1;
                        routePt = new RoutePoint(coordY, coordX, coordZ, speed, hover, trigger, isCameraTrigger);
                        if (routePt != null)
                        {
                            tempPoint.SetPostion(coordY, coordX, coordZ);
                            tmpPolyline.AppendPoint(tempPoint);
                            _routePtList.Add(routePt);
                            idCount++;
                        }
                    }

                    if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_DO_CHANGE_SPEED)
                    {
                        _routePtList[idCount - 1].Speed = (double)item.param2;

                    }
                    else if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_DO_SET_CAM_TRIGG_DIST)
                    {
                        _routePtList[idCount - 1].Trigger = (double)item.param1;
                    }
                }
                showInputPolyline(tmpPolyline);
                _routeListModel.OnUnchecked();
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        private double parseRoutePointSpeed(int commond)
        {

            return 0;
        }
        /// <summary>
        /// 飞向目标
        /// </summary>
        private void FlyToObject()
        {
            GviMap.Camera.LookAtEnvelope(rPolyline.Envelope);
        }


        private void GenerateRoute(object result,double height = _flyHeight, double interval = _dispLength)
        {
            try
            {
                Task.Run(() =>
                {
                    BeginLoadDsProcess();
                    rPolyline = result as IRenderPolyline;
                    var polyLine = rPolyline.GetFdeGeometry() as IPolyline;

                    polyLine.SpatialCRS = GviMap.SpatialCrs;

                    if (_isAutoDetection)
                    {
                        //算法采样离散
                        polyLine = DispaseEx(polyLine, interval);
                        //高度跟随与自动避让
                        polyLine = AjustToFlyHeight(polyLine, true, height);
                        //用户采样离散
                        double customeLength = 30;
                    }

                    _routePtList = new List<RoutePoint>();
                    for (int i = 0; i < polyLine.PointCount; i++)
                    {
                        IPoint tmpPt = polyLine.GetPoint(i);
                        RoutePoint routePt = new RoutePoint(tmpPt.X, tmpPt.Y, tmpPt.Z, 2, 2, 0, 0);
                        _routePtList.Add(routePt);
                    }

                    //绘制线
                    var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
                    rpolygon1?.SetFdeGeometry(polyLine);
                    rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

                    ////绘制点
                    //var rMulPt = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderMultiPoint;
                    //var mulPt = rMulPt.GetFdeGeometry() as IMultiPoint;
                    //mulPt.Clear();
                    //for (int i = 0; i < polyLine.PointCount; i++)
                    //{
                    //    mulPt.AddGeometry(polyLine.GetPoint(i));
                    //}
                    //rMulPt.SetFdeGeometry(mulPt);
                    //rMulPt.VisibleMask = gviViewportMask.gviViewAllNormalView;


                    if (_isAutoDetection)
                    {
                        // 飞行安全管道
                        var rLInes = CreateFlyPipe(polyLine);
                        //安全性检查:禁飞区+碰撞检测
                        if (IsTouchedFeature(rLInes, out IMultiPolyline linesInterset, out IMultiPolyline linesNoInterset, out List<int> segIndexs))
                        {
                            var rMutlLine1 = GviMap.TempRObjectPool[_RouteInterSetMultiLineKey] as IRenderMultiPolyline;
                            rMutlLine1.SetFdeGeometry(linesInterset);
                            rMutlLine1.VisibleMask = gviViewportMask.gviViewAllNormalView;
                            for (int i = 0; i < polyLine.SegmentCount; i++)
                            {
                                if (segIndexs.Contains(i))
                                {
                                    var line = polyLine.GetSegment(i).ToPolyline(GviMap.GeoFactory, GviMap.SpatialCrs);
                                    //var rPipeInterset = GviMap.ObjectManager.CreateRenderPipeLine(line, GviMap.ProjectTree.RootID);
                                    //rPipeInterset.Color = Color.Red;
                                    //rPipeInterset.Radius = 2.05f;

                                    var rpolyline = GviMap.TempRObjectPool[_RoutePlanPipeLineRedKey] as IRenderPolyline;
                                    rpolyline.SetFdeGeometry(line);
                                    rpolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                                }
                            }
                        }
                        var rMutlLine = GviMap.TempRObjectPool[_RouteNoInterSetMultiLineKey] as IRenderMultiPolyline;
                        rMutlLine.SetFdeGeometry(rLInes);
                        rMutlLine.VisibleMask = gviViewportMask.gviViewAllNormalView;

                        var intersetLines = IsSafeNoFlyZone(polyLine);

                        IRenderPipeLine rPipe;
                        if (!GviMap.TempRObjectPool.ContainsKey(_RoutePlanPipeLineKey))
                        {
                            rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
                            GviMap.TempRObjectPool.Add(_RoutePlanPipeLineKey, rPipe);
                            //GviMap.Camera.FlyToObject(rPipe.Guid, gviActionCode.gviActionFlyTo);
                        }
                        else
                        {
                            rPipe = GviMap.TempRObjectPool[_RoutePlanPipeLineKey] as IRenderPipeLine;
                            GviMap.ObjectManager.DeleteObject(rPipe.Guid);
                            rPipe = GviMap.ObjectManager.CreateRenderPipeLine(polyLine, GviMap.ProjectTree.RootID);
                            GviMap.TempRObjectPool[_RoutePlanPipeLineKey] = rPipe;
                        }
                    }

                    _polyline = polyLine;

                    //if (_isAutoDetection)
                    //{
                    //    var parmModel = new ParametricModelling();
                    //    parmModel.PolylineToPipeLine(polyLine, null, out IModelPoint mp, out IModel model);
                    //    GviMap.ObjectManager.CreateRenderModelPoint(mp, null);
                    //}
                    FinishLoadProcess();
                    UnRegisterDrawLineEvent();
                });

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                FinishLoadProcess();
            }
        }

    }
}
