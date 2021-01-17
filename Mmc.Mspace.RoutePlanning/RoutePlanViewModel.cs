using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.WebSockets;
using System.Threading;
using System.Diagnostics;
using Mmc.Windows.Utils;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Device.Location;
using Newtonsoft.Json;

using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.Grid;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.RoutePlanning.Utils;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.RoutePlanning.Views;
using Helpers;

namespace Mmc.Mspace.RoutePlanning
{
    public class RoutePlanViewModel : CheckedToolItemModel
    {
        private RoutePlanView _routePlanView;
        private GridUI _gridUI;
        private DrawCustomerUC drawCustomer = null;
        private GenerateGeoJson _generateGeoJson;
        private DrawCustomerUC editCustomer1 = null;//ployEdit

        private bool _isAutoDetection = false;
        private string _RoutePlanLineKey = "RoutePlanLine";
        private string _RoutePlanPipeLineKey = "RoutePlanPipeLine";
        private string _RoutePlanMultiPtKey = "RoutePlanMultiPtKey";
        private string _RouteNoInterSetMultiLineKey = "RouteNoInterSetMultiLineKey";
        private string _RouteInterSetMultiLineKey = "RouteInterSetMultiLineKey";
        private List<IRObject> tmpList;
        private List<IRenderPipeLine> pipeLinesInterSet;
        private double _flyHeight = 20;//20米
        private double _dispLength = 8;//8米
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

        RouteplanningmenuVModel routeplanningmenuVModel;
        public void releaseWindow()
        {
            _routePlanView = null;
            _gridUI = null;
            this.OnUnchecked();
            Console.WriteLine("-----------CloseWindow");
        }

        public override void Initialize()
        {
            base.Initialize();
            routeplanningmenuVModel = new RouteplanningmenuVModel();
            PathplanningView pathplanningView = new PathplanningView();
            pathplanningView.DataContext = routeplanningmenuVModel;
            ServiceManager.GetService<IShellService>(null).ToolRouteplanningMenu.Content = pathplanningView;
            _generateGeoJson = new GenerateGeoJson();
            _generateGeoJson.initGeoJson();

            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                _routePlanView?.Close();
                ServiceManager.GetService<IShellService>(null).ToolRouteplanningMenu.Visibility = Visibility.Collapsed;
                _gridUI?.Close();
                this.releaseWindow();
            });

            this.cmdCreatePowerRoute = new RelayCommand(() =>
            {
                ClearAllLines();
                _isAutoDetection = false;
                hideRoutePlanView();
            });

            this.cmdCreateGroundRoute = new RelayCommand(() =>
            {
                ClearAllLines();
                _isAutoDetection = true;
                hideRoutePlanView();
            });

            this.cmdCreateMappingRoute = new RelayCommand(() =>
            {
                ShowGridUI();
                hideRoutePlanView();
            });

            this.cmdPolyLineEditMission = new RelayCommand(() =>
            {
                //绘制线
                var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
                RegisterEditLineEvent(rpolygon1);

            });

            //创建区域
            //this.cmdCreatePolygon = new RelayCommand(() =>
            //{
            //    _routePlanView.Hide();
            //    GridPlugin gridPlugin = new GridPlugin();
            //    gridPlugin.initPolygon();

            //});

            this.cmdCreateLogisticRoute = new RelayCommand(() =>
            {
                hideRoutePlanView();
            });

            this.cmdKmlInput = new RelayCommand(() =>
            {
                ParseKml parseKml = new ParseKml();
                _polyline = parseKml.readXmlFile();
                this.showInputPolyline(_polyline);
            });

            this.cmdMissionInput = new RelayCommand(() =>
            {
                ParseMission parseMission = new ParseMission();
                _polyline = parseMission.readMissionFile();
                this.showInputPolyline(_polyline);
            });

            this.cmdExportKml = new RelayCommand(() =>
            {
                if (_polyline != null)
                {
                    this.exportKmlFile();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));


            });

            this.cmdExportDoublePointKml = new RelayCommand(() =>
            {
                if (_polyline != null)
                {
                    this.exportDoublePointKmlFile();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));

            });

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
                if (_polyline != null)
                {
                    if (_generateGeoJson != null)
                    {
                        //_generateGeoJson.setMMCStationMission(_polyline);
                        _generateGeoJson.SaveStationMissionFileDialog();
                    }
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
            });


            this.cmdUploadRoute = new RelayCommand(() =>
            {
                if (_polyline != null)
                {
                    this.createGeoJson();
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Afterretry"));
            });

            base.ViewType = ViewType.CheckedIcon;

            pipeLinesInterSet = new List<IRenderPipeLine>();
        }
        public override void OnChecked()
        {
            var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            //ServiceManager.GetService<IShellService>(null).BottomView.Content = base.BottomView;
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Collapsed;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;

            ServiceManager.GetService<IShellService>(null).ToolRouteplanningMenu.Visibility = Visibility.Visible;

            WindowTitle = Helpers.ResourceHelper.FindKey("Pathplanning");

            //需要注释的
            //if (this._routePlanView == null)
            //{
            //    this._routePlanView = new RoutePlanView();
            //    this._routePlanView.Owner = Application.Current.MainWindow;

            //    //释放窗口
            //    this._routePlanView.Closed += (sender, e) =>
            //    {
            //        this._routePlanView = null;
            //    };
            //    //this.OnUnchecked();
            //}
            //this._routePlanView.DataContext = this;
            //showRoutePlanView();
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
                ptsym.Size = 5;
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
            base.OnChecked();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public void showRoutePlanView()
        {
            if (_routePlanView == null)
            {
                _routePlanView = new RoutePlanView();
                _routePlanView.Closed += (sender, e) => { _routePlanView = null; };
            }
            _routePlanView.Show();
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

        private void ClearPipeLine()
        {
            foreach (var item in pipeLinesInterSet)
                GviMap.ObjectManager.DeleteObject(item.Guid);
            pipeLinesInterSet.Clear();
        }

        private void ClearAllLines()
        {
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
                }
            }
        }

        public override void OnUnchecked()
        {
           if (this.routeplanningmenuVModel != null)
            {
                this.routeplanningmenuVModel.CloseWindows();
            }
            var pools = GviMap.TempRObjectPool;
            foreach (var item in pools)
            {
                IRenderable renderable = GviMap.TempRObjectPool[item.Key] as IRenderable;
                if (renderable != null)
                    renderable.VisibleMask = gviViewportMask.gviViewNone;
            }
            ClearAllLines();

            UnRegisterDrawLineEvent();
            UnRegisterEditLineEvent();

            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ToolRouteplanningMenu.Visibility = Visibility.Collapsed;
            _isAutoDetection = false;
            if (_routePlanView != null)
            {
                _routePlanView?.Close();
                _routePlanView = null;
            }
        }


        private void RegisterDrawLineEvent()
        {
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("Linedrawn"), DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }
            RCDrawManager.Instance.PolylineDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished += PolylineDraw_OnDrawFinished;
        }

        private void RegisterEditLineEvent(IRenderPolyline renderPolyline)
        {
            if (editCustomer1 == null)
            {
                editCustomer1 = new DrawCustomerUC("Lineedit", DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }
            RCDrawManager.Instance.PolylineEdit.SetRenderPolyline(renderPolyline);
            RCDrawManager.Instance.PolylineEdit.Register(GviMap.AxMapControl, editCustomer1, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolylineEdit.OnDrawFinished -= PolylineEdit_OnDrawFinished;
            RCDrawManager.Instance.PolylineEdit.OnDrawFinished += PolylineEdit_OnDrawFinished;
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

        private void PolylineDraw_OnDrawFinished(object sender, object result)
        {
            var rPolyline = result as IRenderPolyline;
            var polyLine = rPolyline.GetFdeGeometry() as IPolyline;
            polyLine.SpatialCRS = GviMap.SpatialCrs;

            if (_isAutoDetection)
            {
                //算法采样离散
                polyLine = DispaseEx(polyLine, _dispLength);
                //高度跟随与自动避让
                polyLine = AjustToFlyHeight(polyLine, true, _flyHeight = 20);
                //用户采样离散
                double customeLength = 30;
            }

            //绘制线
            var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            rpolygon1?.SetFdeGeometry(polyLine);
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

            //绘制点
            var rMulPt = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderMultiPoint;
            var mulPt = rMulPt.GetFdeGeometry() as IMultiPoint;
            mulPt.Clear();
            for (int i = 0; i < polyLine.PointCount; i++)
            {
                mulPt.AddGeometry(polyLine.GetPoint(i));
            }
            rMulPt.SetFdeGeometry(mulPt);
            rMulPt.VisibleMask = gviViewportMask.gviViewAllNormalView;

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
                            var rPipeInterset = GviMap.ObjectManager.CreateRenderPipeLine(line, GviMap.ProjectTree.RootID);
                            rPipeInterset.Color = Color.Red;
                            rPipeInterset.Radius = 2.05f;
                            pipeLinesInterSet.Add(rPipeInterset);
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

            UnRegisterDrawLineEvent();
            //showRoutePlanView();
            //ShowNewRoutePlanView();

        }
        private Mmc.Mspace.RoutePlanning.ViewModels.RoutePlanningViewModel _newRoutePlanViewModel;
        private void ShowNewRoutePlanView()
        {
            
            if (_newRoutePlanViewModel == null)
            {
                _newRoutePlanViewModel = new ViewModels.RoutePlanningViewModel();
               
            }
            _newRoutePlanViewModel.ShowWindow(false);
        }

        private void showInputPolyline(IPolyline polyline)
        {
            initRoutePlan();
            UnRegisterDrawLineEvent();

            //line            
            polyline.SpatialCRS = GviMap.SpatialCrs;
            var rpolygon1 = GviMap.TempRObjectPool[_RoutePlanLineKey] as IRenderPolyline;
            rpolygon1?.SetFdeGeometry(polyline);
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

            //point
            var rMulPt = GviMap.TempRObjectPool[_RoutePlanMultiPtKey] as IRenderMultiPoint;
            var mulPt = rMulPt.GetFdeGeometry() as IMultiPoint;
            mulPt.Clear();
            for (int i = 0; i < polyline.PointCount; i++)
            {
                mulPt.AddGeometry(polyline.GetPoint(i));
            }
            rMulPt.SetFdeGeometry(mulPt);
            rMulPt.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        public void exportKmlFile()
        {
            kml loadkml = new kml();

            List<GeoCoordinate> coordinates = new List<GeoCoordinate>();

            for (int i = 0; i < _polyline.PointCount; i++)
            {
                GeoCoordinate coord = new GeoCoordinate();
                coord.Longitude = _polyline.GetPoint(i).X;
                coord.Latitude = _polyline.GetPoint(i).Y;
                coord.Altitude = _polyline.GetPoint(i).Z;
                coordinates.Add(coord);
            }
            loadkml.Document.Add(new Placemark("", "", "colorID", coordinates));
            loadkml.SaveFileDialog();
        }

        public void exportDoublePointKmlFile()
        {
            kml loadkml = new kml();

            List<GeoCoordinate> coordinates = new List<GeoCoordinate>();

            for (int i = 0; i < _polyline.PointCount; i++)
            {
                GeoCoordinate coord = new GeoCoordinate();
                coord.Longitude = _polyline.GetPoint(i).X;
                coord.Latitude = _polyline.GetPoint(i).Y;
                coord.Altitude = _polyline.GetPoint(i).Z;
                coordinates.Add(coord);
                coordinates.Add(coord);//每个点都复制一份，用作控制点
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
        public void createGeoJson()
        {
            if (_generateGeoJson != null)
            {
                //_generateGeoJson.SetGeoPointItem(_polyline);//GeoJson
                //_generateGeoJson.setMMCStationMission(_polyline);//MMC Station Mission

                bool result = _generateGeoJson.CreateGeoJson();
                //var postReturn = JsonUtil.DeserializeFromString<dynamic>(result);

                //sync
                if (result)
                {
                    //var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    //var _RouteHost = json.poiUrl;
                    string url = string.Format(@"{0}{1}", WebConfig.MspaceHostUrl, UAVInterface.TraceListPage);
                    _routePlanView.Navigate(url);
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
            _label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            var view = (Window)base.View;
            view.WindowStartupLocation = WindowStartupLocation.Manual;
            view.Hide();
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

                    var view = (Window)base.View;
                    view.Left = screenX + 30;
                    view.Top = sreenY - view.ActualHeight / 2;
                    view.Show();
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
                        if (item.Layer.PolylineIntersect(line1, out string fdsetName, out string fcName, out int fid, out IVector3 intertSetPt))
                        {
                            if (!SegIndex.Contains(index))
                                SegIndex.Add(index);
                            line.Project(GviMap.SpatialCrs);
                            line.SpatialCRS = GviMap.SpatialCrs;
                            linesInterset.AddGeometry(line);

                        }
                        else
                        {
                            line.Project(GviMap.SpatialCrs);
                            line.SpatialCRS = GviMap.SpatialCrs;
                            linesNoInterset.AddGeometry(line);
                        }
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
                        if (item.Layer.PolylineIntersect(_polyline, out string fdsetName, out string fcName, out int fid, out IVector3 intertSetPt))
                            z = z > intertSetPt.Z ? z : intertSetPt.Z;
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
    }
}
