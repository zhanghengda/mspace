using ApplicationConfig;
using FireControlModule;
using FireControlModule.UnitInfo;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Microsoft.Win32;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.PoiManagerModule;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.RoutePlanning;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.Main
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]//将该类设置为com可访问
    public class ObjectForScriptingHelper : IJsScriptInvokerService
    {
        private List<IDisplayLayer> _allLayers = null;
        private BuildDetailViewModel _buildDetailVm;
        private string _buildOid;
        private IRenderGeometry _geoRender;
        private ISurfaceSymbol _sfaceSymbol;
        private UnitDetailViewModel _unitDetailVm;
        private readonly string FcNameUnit = Helpers.ResourceHelper.FindKey("Signs");
        private readonly string FieldNameUnit = "NAME";
        private Window leftWindow;
        private PoiDetailViewModel _poiDetailViewModel;
        private PolygonMakerViewModel _polygonMakerModel;
        private MakerLineViewModel _polyLineMakerModel;
        private RoutePlanShowPageViewModel _routePlanShowPageModel;
        private RoutePointEditViewModel _routePointEditViewModel;
        private List<IRenderLayer> _renderLayers;
        private IWebView _webView;
        private HttpService _httpService;
        private string _poiHost;
        private readonly ExportProgressView progressView;
        private AccountListView _accountView;
        private UserInfo _userInfo;

        private System.Timers.Timer timer = new System.Timers.Timer(1000);//实例化Timer类
        private readonly object SynObject = new object();

        private EditAccountView editAccountView = null;
        private EditAccountVModel editAccountVModel = null;
        public ObjectForScriptingHelper()
        {
            _httpService = new HttpService();
            _httpService.Token = HttpServiceUtil.Token;
            _sfaceSymbol = new SurfaceSymbol();
            _sfaceSymbol.Color = ColorConvert.UintToColor(ColorConvert.Rgba(255, 0, 66, 15));
            _sfaceSymbol.BoundarySymbol = new CurveSymbol() { Width = 20 };
            if (_allLayers == null)
            {
                var actalLayers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                var shpLayers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
                _allLayers = new List<Mmc.DataSourceAccess.IDisplayLayer>();
                if (shpLayers != null)
                    _allLayers.AddRange(shpLayers);
                if (actalLayers != null)
                    _allLayers.AddRange(actalLayers);
            }
            ServiceManager.GetService<IDataBaseService>(null).OnLoadingDataSourceProcess += new Action<string>(LoadingDsProcess);
            if (_renderLayers == null)
            {
                _renderLayers = new List<IRenderLayer>();
                var tileLayers = DataBaseService.Instance.GetTileLayers();
                var imageLayers = DataBaseService.Instance.GetImageLayers();
                var shpLayers = DataBaseService.Instance.GetShpLayers();
                var actualLayers = DataBaseService.Instance.GetActualityLayers();
                if (shpLayers != null)
                {
                    foreach (var item in shpLayers)
                        _renderLayers.Add(item as IRenderLayer);
                }
                if (actualLayers != null)
                {
                    foreach (var item in actualLayers)
                        _renderLayers.Add(item as IRenderLayer);
                }
                _renderLayers.AddRange(tileLayers);
                _renderLayers.AddRange(imageLayers);

            }
            progressView = new ExportProgressView();
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _poiHost = json.poiUrl;
            _webView = this.leftWindow as IWebView;

            _userInfo = CacheData.UserInfo;



        }

        private void AxMapControl_RcCameraChanged(bool IsPositionChanged, bool IsRotationChanged)
        {
            GetMarkerListByMapRange();
        }


        public void GetMarkerListByMapRange()
        {
            //bool needToFresh = MarkerHelper.Instance.RequestMarkerListByMapRangeChange();
            //MarkerHelper.Instance.RequestCompleted -= new Action<string>(ReturnMarkerListData);
            //MarkerHelper.Instance.RequestCompleted += new Action<string>(ReturnMarkerListData);
            //if (!needToFresh)
            //{
            //    var obj = new { pageNumber = 1 };
            //    string objStr = JsonUtil.SerializeToString(obj);
            //    ReturnMarkerListData(objStr);
            //}
        }

        public Action<string, string> ColorVisibleEvent { get; set; }
        public Window LeftWindow
        {
            get { return leftWindow; }
            set { leftWindow = value; }
        }

        public void collopsePanel(bool isCollopse)
        {
            var sysmenu = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            if (isCollopse)
            {
                this.leftWindow.SizeToContent = SizeToContent.Manual;
                this.leftWindow.Height = 40;

                this.leftWindow.Width = int.Parse(ConfigurationManager.AppSettings["LeftViewWidth"]);//原始290
            }
            else
            {
                if (ServiceManager.GetService<IMaphostService>(null).MapWindow.MaxHeight == SystemParameters.PrimaryScreenHeight)
                    this.leftWindow.Height = SystemParameters.PrimaryScreenHeight - 48;
                else
                    this.leftWindow.Height = SystemParameters.WorkArea.Height - 48;
                this.leftWindow.Width = int.Parse(ConfigurationManager.AppSettings["LeftViewWidth"]);//原始290   380
            }
        }

        public void colorVisible(string color, string visible)
        {
            ColorVisibleEvent?.Invoke(color, visible);
        }

        public void flyToBuild(string buildCode)
        {
            flyToBuild(Helpers.ResourceHelper.FindKey("Building"), buildCode);
        }

        /// <summary>
        /// 定位到建筑
        /// 高亮飞入
        /// </summary>
        /// <param name="buildCode">建筑编码</param>
        public void flyToBuild(string layerName, string buildCode)
        {
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            layers.ForEach(p =>
            {
                if (p.Fc.Alias == layerName)
                {
                    //先清除高亮
                    p.UnHighLightFeatureClass(GviMap.FeatureManager);
                    //高亮飞入
                    var fc = p.Fc;
                    var filter = new QueryFilter();
                    filter.WhereClause = string.Format("{0}='{1}'", "Name", buildCode);
                    var cursor = fc.Search(filter, false);
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        var fid = row.GetFid().ParseTo<string>();
                        _buildOid = fid;
                        p.HighLightFeature(fid, GviMap.FeatureManager);
                        p.FlyToFeature(fid, GviMap.Camera);
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }

        /// <summary>
        /// 定位到格网区域
        /// </summary>
        /// <param name="regionCode">区域编码</param>
        /// <param name="regionType">区域类型</param>
        /// <param name="centerPt">区域中心坐标点</param>
        public void flyToRegion(string regionCode, string regionType)
        {
            float lookDistance = 1000;
            var shpLayers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
            var layerName = "深圳市范围";
            if (regionType == "1")
            {
                layerName = "深圳市范围";

                setLayerVisible("街道办范围", false);
                setLayerVisible("社区范围", false);
                setLayerVisible("小网格范围", false);
                lookDistance = 25000;
            }
            else if (regionType == "2")
            {
                layerName = "街道办范围";
                setLayerVisible("深圳市范围", false);
                setLayerVisible("社区范围", false);
                setLayerVisible("小网格范围", false);
                lookDistance = 11000;
            }
            else if (regionType == "3")
            {
                layerName = "社区范围";
                setLayerVisible("街道办范围", false);
                setLayerVisible("小网格范围", false);
                setLayerVisible("深圳市范围", false);
                lookDistance = 5000;
            }
            else if (regionType == "4")
            {
                layerName = "小网格范围";
                setLayerVisible("街道办范围", false);
                setLayerVisible("社区范围", false);
                setLayerVisible("深圳市范围", false);
                lookDistance = 1000;
            }
            setLayerVisible(layerName, true);
            shpLayers.ForEach(p =>
            {
                var fc = p.Fc;
                if (p.Fc.Alias == layerName || p.Fc.Name == layerName)
                {
                    IEulerAngle angle = null;
                    IVector3 centerPoint = null;
                    GviMap.Camera.GetCamera(out centerPoint, out angle);
                    GviMap.Camera.FlyTime = 0.5;
                    var filter = new QueryFilter();
                    filter.WhereClause = string.Format("{0}='{1}'", "code", regionCode);
                    filter.AddSubField("Geometry");
                    var cursor = fc.Search(filter, false);
                    IRowBuffer row = null;
                    while ((row = cursor.NextRow()) != null)
                    {
                        var geo = row.GetValue<IGeometry>(0);
                        centerPoint = geo.Envelope.Center;
                        GviMap.Camera.LookAt(centerPoint, lookDistance, angle);
                        //GviMap.Camera.FlyToEnvelope(geo.Envelope,GviMap.SpatialCrs,lookDistance);
                        if (_geoRender == null)
                            _geoRender = GviMap.ObjectManager.CreateRenderPolygon(geo as IPolygon, _sfaceSymbol, GviMap.ProjectTree.RootID);

                        _geoRender.SetFdeGeometry(geo);
                        _geoRender.Glow(3000);
                        _geoRender.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        geo.ReleaseComObject();
                        row.ReleaseComObject();
                    }
                    cursor.ReleaseComObject();
                    filter.ReleaseComObject();
                }
            });
        }

        public void flyToUnit(string unitCode)
        {
            var p = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName(FcNameUnit);
            //先清除高亮
            p.UnHighLightFeatureClass(GviMap.FeatureManager);
            //高亮飞入
            var fc = p.Fc;
            if (p.Fc.CustomData != null)
            {
                string location = p.Fc.CustomData.GetProperty(unitCode) as string;
                if (!string.IsNullOrEmpty(location))
                {
                    string[] array = location.Split(new char[] { ';' });
                    if (!(array == null || array.Length != 6))
                    {
                        double x = StringExtension.ParseTo<double>(array[0], 0.0);
                        double y = StringExtension.ParseTo<double>(array[1], 0.0);
                        double z = StringExtension.ParseTo<double>(array[2], 0.0);
                        double heading = StringExtension.ParseTo<double>(array[3], 0.0);
                        double tilt = StringExtension.ParseTo<double>(array[4], 0.0);
                        double roll = StringExtension.ParseTo<double>(array[5], 0.0);
                        GviMap.Camera.SetCamera(x, y, z, heading, tilt, roll, GviMap.SpatialCrs, gviSetCameraFlags.gviSetCameraNoFlags);
                    }
                }
            }
            else
            {
                var filter = new QueryFilter();
                filter.WhereClause = string.Format("{0}='_{1}'", FieldNameUnit, unitCode);
                var cursor = fc.Search(filter, false);
                IRowBuffer row = null;
                while ((row = cursor.NextRow()) != null)
                {
                    var fid = row.GetFid().ParseTo<string>();
                    var modelPt = fc.GetModelPoint(fid.ParseTo<int>());
                    //var infos = p.Fc.GetModelInfo(fid.ParseTo<int>());
                    //var modelPt = infos.Item1;
                    //var model = infos.Item2;

                    //飞入
                    GviMap.Camera.LookAtEnvelope(modelPt.Envelope);
                    //高亮
                    //var polygons = GviMap.GeoConvertor.ProjectModelPointToPolygon(modelPt, model, 0.5);
                    //GviMap.HighlightHelper.MaxZ = modelPt.Envelope.MaxZ - 0.05;
                    //GviMap.HighlightHelper.Color = 0x99ff0000;
                    //GviMap.HighlightHelper.MinZ = modelPt.Envelope.MinZ;
                    //GviMap.HighlightHelper.SetRegion(polygons);
                    //GviMap.HighlightHelper.VisibleMask = 1;
                    modelPt.ReleaseComObject();
                    //polygons.ReleaseComObject();
                    row.ReleaseComObject();
                }
                cursor.ReleaseComObject();
                filter.ReleaseComObject();
            }
        }

        /// <summary>
        /// 保存观察单位的视角
        /// </summary>
        /// <param name="unitCode"></param>
        public void saveCameraParam(string unitCode)
        {
            var p = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName(FcNameUnit);
            IVector3 pt = null; IEulerAngle angle = null;
            GviMap.Camera.GetCamera(out pt, out angle);
            string location = string.Format("{0};{1};{2};{3};{4};{5}", pt.X, pt.Y, pt.Z, angle.Heading, angle.Tilt, angle.Roll);
            if (p.Fc.CustomData == null)
            {
                var propSet = new PropertySet();
                propSet.SetProperty(unitCode, location);
                p.Fc.CustomData = propSet;
            }
            else
            {
                var propSet = p.Fc.CustomData;
                propSet.SetProperty(unitCode, location);
                p.Fc.CustomData = propSet;
            }
        }

        public void InvokeThreeColor()
        {
            //this.webBrowser1.Document.InvokeScript(“messageBox”, objects);
        }

        // 提供给JavaScript调用的方法
        public void MyMessageBox(string message)
        {
            Messages.ShowMessage(message);
        }

        public void openUnitVideo(string unitCode)
        {
            // saveCameraParam(unitCode);
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json1.full3dUrl;
            //调用系统默认的浏览器
            var _process = System.Diagnostics.Process.Start("iexplore.exe", " -k " + url + unitCode);
        }

        public void setLayersVisible(string layerNames, bool isVisilbe)
        {
            string[] layers = layerNames.Split(",");
            foreach (var layerName in layers)
                setLayerVisible(layerName, isVisilbe);
        }

        public void setLayerVisible(string layerName, bool isVisilbe)
        {
            //if (layerName == "地形")
            //{
            //    GviMap.Terrain.SetVisibleMask(isVisilbe);
            //}
            //else
            //{
            //    _allLayers.ForEach(p =>
            //    {
            //        if (p.Fc.Name == layerName || p.Fc.Alias == layerName)
            //            p.SetVisibleMask(isVisilbe);
            //    });
            //}
        }

        public void getToken(string token)
        {

            _httpService.Token = token;
        }

        public void isLogin(bool isLogin, string token)
        {


        }

        /// <summary>
        /// 设置图层是否可见
        /// </summary>
        /// <param name="LayerGuid">图层guid</param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe"></param>
        public void setRederLayerVisible(string LayerGuid, int viewPortIndex, bool isVisilbe)
        {
            if (_renderLayers.HasValues())
            {
                foreach (var layer in _renderLayers)
                {

                    if (layer.Guid == LayerGuid)
                    {
                        var renderable = layer.Renderable;
                        renderable?.SetVisibleMask(GviMap.Viewport.ViewportMode, viewPortIndex, isVisilbe);
                    }
                }
            }
        }

        /// <summary>
        /// 设置分屏状态
        /// </summary>
        /// <param name="compareViewState">分屏状态，2表示2屏，以此类推</param>
        public void setCompareViewState(int compareViewState)
        {
            if (compareViewState == 2)
            {
                //开启双屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;
            }
            else if (compareViewState == 3)
            {
                //开启三屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1M1R1;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView2;
            }
            else if (compareViewState == 4)
            {
                //开启四屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportQuad;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView2 | gviViewportMask.gviView3;
            }

            foreach (var layer in _renderLayers)
            {
                if (layer.Renderable == null)
                    continue;
                int flag = (int)layer.Renderable.VisibleMask;
                if (flag % 2 == 1)
                {
                    layer.Renderable.VisibleMask = gviViewportMask.gviViewAllNormalView;
                }
                else if (layer.Renderable.VisibleMask != gviViewportMask.gviViewAllNormalView)
                {
                    layer.Renderable.VisibleMask = gviViewportMask.gviViewNone;
                }
                //else if ((int)(layer.Renderable.VisibleMask) > 1 && layer.Renderable.VisibleMask != gviViewportMask.gviViewNone && layer.Renderable.VisibleMask != gviViewportMask.gviViewAllNormalView)
                //{
                //    layer.Renderable.VisibleMask = gviViewportMask.gviViewNone;
                //}
            }

        }





        /// <summary>
        /// 设置图层组是否可见
        /// </summary>
        /// <param name="LayerGuids">图层组下的layer guid的集合字符串，以,为分隔符，如0001,0002</param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">图层是否可见</param>
        public void setRederLayersVisible(string LayerGuids, int viewPortIndex, bool isVisilbe)
        {
            if (LayerGuids.Contains(","))
            {
                string[] layerGuids = LayerGuids.Split(",");
                foreach (var layerGuid in layerGuids)
                    setRederLayerVisible(layerGuid, viewPortIndex, isVisilbe);
            }
            else
                setRederLayerVisible(LayerGuids, viewPortIndex, isVisilbe);
        }

        public void SetAllLayerUnvisible()
        {
            if (_renderLayers.HasValues())
            {
                foreach (var layer in _renderLayers)
                {
                    var renderable = layer.Renderable;
                    if (renderable != null)
                        renderable.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
        }

        /// <summary>
        /// 增加数据源
        /// </summary>
        /// <param name="SourceType">数据类型</param>
        public void AddDataSource()
        {
           // var type = (RenderLayerType)SourceType;

            LoadDataView dataView = new LoadDataView();
            dataView.Owner = Application.Current.MainWindow;

            dataView.getLocalFileName += GetLocalFileName;
            //dataView.addData += AddData;
            dataView.getNetServiceLayerInfo += GetNetServiceLayerInfo;


            dataView.ShowDialog();

            dataView.getLocalFileName -= GetLocalFileName;
            //dataView.addData -= AddData;
            dataView.getNetServiceLayerInfo -= GetNetServiceLayerInfo;

        }

        public IRenderLayer AddImageLayer(string fileName, bool issafe = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            IRenderLayer renderLayer = null;
            try
            {
                //打开文件
                BeginLoadDsProcess();
                var name = Path.GetFileNameWithoutExtension(fileName);
                ImageLayerConfig layerConfig = new ImageLayerConfig()
                {
                    AliasName = name,
                    ConnInfoString = fileName,
                    AlphaEnabled = "false",
                    ConType = "File",
                };
                renderLayer = DataBaseService.Instance.AddImageLayer(layerConfig, out status, issafe);
                if (renderLayer != null)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _renderLayers.Add(renderLayer);
                    });
                }
                FinishLoadProcess();
                return renderLayer;
            }
            catch (Exception e)
            {
                FinishLoadProcess();
                return null;
            }
        }

        private void AddImageDataSource(string fileAddress, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            ImageLayerConfig layerConfig = new ImageLayerConfig()
            {
                ConnInfoString = fileAddress,
                AlphaEnabled = "false",
                IsLocal = isLocal
            };
            if (isLocal)
            {
                layerConfig.AliasName = Path.GetFileNameWithoutExtension(fileAddress);
                layerConfig.ConType = "File";
            }
            else
            {
                layerConfig.ConType = "WMTS";
            }

            try
            {
                Task.Run(() =>
                {
                    BeginLoadDsProcess();

                    var renderLayer = DataBaseService.Instance.AddImageLayer(layerConfig, out status);
                    if (renderLayer != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _renderLayers.Add(renderLayer);
                            this.UpdateLeftViewLayer();
                        });
                    }
                    FinishLoadProcess();
                    ShowAddDataStatus(status);
                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                FinishLoadProcess();
                ShowAddDataStatus(status);
            }
        }



        private string GetLocalFileName(string filetype)
        {
            string name = string.Empty;

            OpenFileDialog openFile = new OpenFileDialog();

            switch (filetype)
            {
                case "ShpGroupLayer":
                    openFile.Filter = FileFilterStrings.SHP;
                    break;
                case "ImageGroupLayer":
                    openFile.Filter = FileFilterStrings.TIF;
                    break;
                case "TileGroupLayer":
                    openFile.Filter = FileFilterStrings.TDBX;
                    break;
                case "DataSetGroupLayer":
                    openFile.Filter = FileFilterStrings.FDB;
                    break;
            }
            if (openFile.ShowDialog() == true)
            {
                var fileName = openFile.FileName;
                name = fileName;                
            }

            return name;

        }

        private List<string> GetLocalFileName()
        {
            string name = string.Empty;
            List<string> _nameList = new List<string>();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = FileFilterStrings.Support;        
            if (openFile.ShowDialog() == true)
            {
                foreach (string file in openFile.FileNames)
                {
                    _nameList.Add(file);
                }
            }

            return _nameList;
        }

        private void AddData(string filetype, string fileAddress, string guid)
        {
            switch (filetype)
            {
                case "ShpGroupLayer":
                    AddShpDataSource(fileAddress);
                    break;
                case "ImageGroupLayer":
                    AddImageDataSource(fileAddress);
                    break;
                case "TileGroupLayer":
                    AddTileDataSource(fileAddress);
                    break;
                case "DataSetGroupLayer":
                    AddFdbDataSource(fileAddress);
                    break;

                case "WFS":
                    if (_userInfo.mspace_config.is_administrator == "1")
                        AddShpDataSource(fileAddress, false, guid);
                    else
                        ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                    break;
                case "WMTS":
                    if (_userInfo.mspace_config.is_administrator == "1")
                        AddImageDataSource(fileAddress, false);
                    else
                        ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                    break;
                case "TILE":
                    if (_userInfo.mspace_config.is_administrator == "1")
                        AddTileDataSource(fileAddress, false);
                    else
                        ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                    break;
                case "MODEL":
                    if (_userInfo.mspace_config.is_administrator == "1")
                        AddFdbDataSource(fileAddress, false);
                    else
                        ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                    break;
            }
        }

        private void AddShpDataSource(string fileAddress, bool isLocal = true, string guid = "")
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            try
            {
                Task.Run(() =>
                {
                    BeginLoadDsProcess();
                    //string name = string.Empty;
                    var con = new ConnectionInfo();

                    if (isLocal)
                    {
                        con.ConnectionType = gviConnectionType.gviConnectionShapeFile;
                        //name = fileAddress.Substring(fileAddress.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                        con.ConnectionType = gviConnectionType.gviConnectionWFS;
                    }
                    con.Database = fileAddress;

                    LibraryConfig layerConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        //AliasName=name,
                        Guid = guid,
                        Is2DData = true,
                        IsLocal = isLocal
                    };
                    List<IDisplayLayer> renderLayers;

                    renderLayers = DataBaseService.Instance.AddFeatureDatasource(layerConfig, out status);

                    if (renderLayers != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _renderLayers.Add(RenderLayer.CreateRenderLayer(renderLayers[0] as IRenderLayer));
                            this.UpdateLeftViewLayer();
                        });
                    }
                    FinishLoadProcess();
                    ShowAddDataStatus(status);
                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                FinishLoadProcess();
                ShowAddDataStatus(status);
            }
        }


        private void BeginLoadDsProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartLoading"));
            });
        }

        private void LoadingDsProcess(string msg = "")
        {
            if (string.IsNullOrEmpty(msg))
                msg = Helpers.ResourceHelper.FindKey("Loading");
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = msg;
            });
        }

        private void FinishLoadProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = string.Empty;
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
            });
        }

        private void AddFdbDataSource(string fileAddress, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            try
            {
                Task.Run(() =>
                {
                    BeginLoadDsProcess();

                    string name = string.Empty;
                    var con = new ConnectionInfo();
                    if (isLocal)
                    {
                        con.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                        con.Database = fileAddress;
                        name = fileAddress.Substring(fileAddress.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                        string[] tempArr = fileAddress.Split(';');
                        Dictionary<string, string> tempDic = new Dictionary<string, string>();
                        if (tempArr.Length > 0)
                        {
                            foreach (string item in tempArr)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    tempDic.Add(item.Split('=')[0].Trim(), item.Split('=')[1].Trim());
                                }
                            }
                        }
                        con.ConnectionType = gviConnectionType.gviConnectionCms7Http;

                        con.Server = tempDic["Server"];
                        con.Port = Convert.ToUInt32(tempDic["Port"]);
                        con.Database = tempDic["DataBase"];
                        name = con.Database;
                    }

                    LibraryConfig layerConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        AliasName = name,
                        Is2DData = false,
                        IsLocal = isLocal
                    };
                    var renderLayers = DataBaseService.Instance.AddFeatureDatasource(layerConfig, out status);

                    if (renderLayers != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {

                            foreach (var item in renderLayers)
                                _renderLayers.Add(item as DisplayLayer);
                            this.UpdateLeftViewLayer();
                        });
                    }
                    FinishLoadProcess();
                    ShowAddDataStatus(status);
                });
            }
            catch
            {
                FinishLoadProcess();
                ShowAddDataStatus(status);
            }
        }


        private void AddTileDataSource(string fileAddress, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            string name = string.Empty;
            if (isLocal)
            {
                name = Path.GetFileNameWithoutExtension(fileAddress);
            }
            else
            {
                name = fileAddress.Split(':', '@')[1];
            }

            TileLayerConfig layerConfig = new TileLayerConfig()
            {
                AliasName = name,
                ConnInfoString = fileAddress,
                IsLocal = isLocal
            };
            //打开文件
            try
            {
                //this.progressView.Visibility = Visibility.Visible;
                Task.Run(() =>
                {
                    BeginLoadDsProcess();

                    var renderLayer = DataBaseService.Instance.Add3DTileLayer(layerConfig, out status);
                    LoadingDsProcess();
                    if (renderLayer != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _renderLayers.Add(renderLayer);
                            this.UpdateLeftViewLayer();
                        });
                    }
                    FinishLoadProcess();
                    ShowAddDataStatus(status);
                });
            }
            catch
            {
                FinishLoadProcess();
                ShowAddDataStatus(status);
            }
        }

        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="LayerGuid">图层id</param>
        public void DeleteDataSource(string LayerGuid)
        {
            try
            {
                if (_renderLayers.HasValues())
                {
                    foreach (var layer in _renderLayers)
                    {

                        if (layer.Guid == LayerGuid)
                        {
                            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Deletelayer"), string.Format(Helpers.ResourceHelper.FindKey("Deletelayerconfirm"), layer.AliasName)))
                            {
                                DataBaseService.Instance.RemoveSingleLayer(layer);
                                _renderLayers.Remove(layer as IRenderLayer);
                                this.UpdateLeftViewLayer();
                                break;
                            }
                        }
                        else if (layer is DisplayLayer)
                        {
                            var disLyr = layer as DisplayLayer;
                            if (disLyr != null && disLyr.Fc.DataSource.Guid.ToString() == LayerGuid)
                            {
                                string[] tempArr = disLyr.Fc.DataSource.ConnectionInfo.Database.Split('\\');
                                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Deletedatasource"), string.Format(Helpers.ResourceHelper.FindKey("Deletedatasourceconfirm"), tempArr[tempArr.Length - 1])))
                                {
                                    var disPlayLayers = new List<IDisplayLayer>();
                                    var disPlayGuids = new List<string>();
                                    foreach (var item in _renderLayers)
                                    {
                                        if (item.LayerType == RenderLayerType.FeatureLayer)
                                        {
                                            var disLyr1 = item as DisplayLayer;
                                            if (disLyr1 != null && disLyr1.Fc.DataSource.Guid.ToString() == LayerGuid)
                                            {
                                                disPlayLayers.Add(disLyr1);
                                                disPlayGuids.Add(disLyr1.Guid);
                                            }
                                        }
                                    }
                                    DataBaseService.Instance.RemoveFDbDataSource(disPlayLayers);
                                    foreach (var item in disPlayLayers)
                                        _renderLayers.Remove(item as IRenderLayer);

                                    this.UpdateLeftViewLayer();
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        public void deleteImgRenderLayer(string LayerGuid)
        {
            foreach (var layer in _renderLayers)
            {
                if (layer.Guid == LayerGuid)
                {
                    DataBaseService.Instance.RemoveSingleLayer(layer);
                    _renderLayers.Remove(layer as IRenderLayer);
                    break;
                }
            }
        }

        /// <summary>
        ///   guid
        /// --根节点：0000000000
        ///    --三维模型节点：0000000100
        ///    --瓦片数据节点：0000000200
        ///    --影像数据：0000000300
        ///    --二维矢量：0000000400
        /// </summary>
        public void UpdateLeftViewLayer()
        {
            try
            {
                SystemLog.Log("ObjectForScriptingHelper.UpdateLeftViewLayer");
                //推送图层组结构
                var view = this.leftWindow as IWebView;
                //根节点
                var rootRenderLayer = new RenderLayer()
                {
                    Name = Helpers.ResourceHelper.FindKey("Root"),
                    AliasName = Helpers.ResourceHelper.FindKey("Root"),
                    Guid = Guid.Parse("00000000-0000-0000-0000-000000000000").ToString(),
                    LayerType = RenderLayerType.GroupLayer,
                    Rederlayers = new List<RenderLayer>()
                };

                var actualLayers = DataBaseService.Instance.GetActualityLayers();
                //三维模型
                var modelRenderLayers = new RenderLayer()
                {
                    Name = Helpers.ResourceHelper.FindKey("3dmodel"),
                    AliasName = Helpers.ResourceHelper.FindKey("3dmodel"),
                    Guid = Guid.Parse("00000000-0000-0000-0000-000000000100").ToString(),
                    LayerType = RenderLayerType.DataSetGroupLayer,
                    Rederlayers = new List<RenderLayer>(),
                };
                if (actualLayers != null)
                {
                    Dictionary<string, Tuple<IDataSource, List<IDisplayLayer>>> dicModel = new Dictionary<string, Tuple<IDataSource, List<IDisplayLayer>>>();
                    foreach (var item in actualLayers)
                    {
                        var key = item.Fc.DataSource.Guid.ToString();
                        if (!dicModel.ContainsKey(key))
                            dicModel.Add(key, new Tuple<IDataSource, List<IDisplayLayer>>(item.Fc.DataSource, new List<IDisplayLayer>()));
                        dicModel[key].Item2.Add(item);
                    }
                    foreach (var key in dicModel.Keys)
                    {
                        var dsName = dicModel[key].Item1.ConnectionInfo.GetDataSourceName();
                        var dataSourceRenderLayers = new RenderLayer()
                        {
                            IsLocal = !dicModel[key].Item1.IsNetServer(),
                            Name = (!dicModel[key].Item1.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + dsName,
                            AliasName = (!dicModel[key].Item1.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + dsName,
                            Guid = key,
                            LayerType = RenderLayerType.GroupLayer,
                            Rederlayers = new List<RenderLayer>()
                        };

                        foreach (var item in dicModel[key].Item2)
                            dataSourceRenderLayers.Rederlayers.Add(RenderLayer.CreateRenderLayer(item as IRenderLayer));
                        modelRenderLayers.Rederlayers.Add(dataSourceRenderLayers);
                    }
                }
                rootRenderLayer.Rederlayers.Add(modelRenderLayers);



                //获取瓦片数据
                var tileLayers = DataBaseService.Instance.GetTileLayers();
                var tileRenderLayers = new RenderLayer()
                {
                    Name = Helpers.ResourceHelper.FindKey("Obliquephotography"),
                    AliasName = Helpers.ResourceHelper.FindKey("Obliquephotography"),
                    Guid = Guid.Parse("00000000-0000-0000-0000-000000000200").ToString(),
                    LayerType = RenderLayerType.TileGroupLayer,
                    Rederlayers = new List<RenderLayer>()
                };

                foreach (var item in tileLayers)
                {
                    tileRenderLayers.Rederlayers.Add(new RenderLayer()
                    {
                        IsLocal = item.IsLocal,
                        Name = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name,
                        AliasName = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.AliasName,
                        Guid = item.Guid,
                        LayerType = RenderLayerType.TileLayer,

                    });
                }
                rootRenderLayer.Rederlayers.Add(tileRenderLayers);
                //获取影像数据
                var imageLayers = DataBaseService.Instance.GetImageLayers();
                var imgRenderLayers = new RenderLayer()
                {
                    Name = Helpers.ResourceHelper.FindKey("Screenage"),
                    AliasName = Helpers.ResourceHelper.FindKey("Screenage"),
                    Guid = Guid.Parse("00000000-0000-0000-0000-000000000300").ToString(),
                    LayerType = RenderLayerType.ImageGroupLayer,
                    Rederlayers = new List<RenderLayer>()
                };
                //  imgRenderLayers.Rederlayers.AddRange(imageLayers);
                foreach (var item in imageLayers)
                {
                    imgRenderLayers.Rederlayers.Add(new RenderLayer()
                    {
                        IsLocal = item.IsLocal,
                        Name = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name,
                        AliasName = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.AliasName,
                        Guid = item.Guid,
                        LayerType = RenderLayerType.ImageLayer
                    });
                }
                rootRenderLayer.Rederlayers.Add(imgRenderLayers);

                //二维矢量
                var shpRenderLayers = new RenderLayer()
                {
                    Name = Helpers.ResourceHelper.FindKey("2dvector"),
                    AliasName = Helpers.ResourceHelper.FindKey("2dvector"),
                    Guid = Guid.Parse("00000000-0000-0000-0000-000000000400").ToString(),
                    LayerType = RenderLayerType.ShpGroupLayer,
                    Rederlayers = new List<RenderLayer>()
                };
                var shpLayers = DataBaseService.Instance.GetShpLayers();
                if (shpLayers != null)
                    foreach (var item in shpLayers)
                    {
                        var shpRen = RenderLayer.CreateRenderLayer(item as IRenderLayer);
                        shpRen.IsLocal = !item.Fc.DataSource.IsNetServer();
                        shpRen.Name = (!item.Fc.DataSource.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name;
                        shpRen.AliasName = (!item.Fc.DataSource.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name;
                        shpRenderLayers.Rederlayers.Add(shpRen);
                    }

                rootRenderLayer.Rederlayers.Add(shpRenderLayers);
                var json = JsonUtil.SerializeToString(rootRenderLayer);
                view.InvokeScript("mapBaseLayerList", json);
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void UpdateMarkerTable(string objStr)
        {
            //6bool needToFresh = MarkerHelper.Instance.RequestMarkerList(jsonStr: objStr);
            MarkerHelper.Instance.RequestCompleted -= new Action<string>(ReturnMarkerListData);
            MarkerHelper.Instance.RequestCompleted += new Action<string>(ReturnMarkerListData);
            //if (!needToFresh)
            //    ReturnMarkerListData(objStr);
        }


        public void ReturnMarkerListData(string objStr)
        {
            var markerListDic = MarkerHelper.Instance.MarkerDic;
            int totalNum = MarkerHelper.Instance.TotalMarkerCounts;
            int pageNum = 1;
            int pageSize = 20;
            //int totalNum = 100;
            Dictionary<string, MarkerModel> temp = new Dictionary<string, MarkerModel>();
            Dictionary<string, MarkerModel> dic = new Dictionary<string, MarkerModel>();
            var obj = JsonUtil.DeserializeFromString<dynamic>(objStr);
            //string tags = obj.keyword;
            pageNum = obj.pageNumber ?? pageNum;
            //totalNum = obj.TotalMarkerCounts ?? totalNum;
            //pageSize = obj.pageSize ?? pageSize;



            var pageMarkers = (from o in markerListDic orderby o.Key descending select o).Skip((pageNum - 1) * pageSize).Take(pageSize);
            //foreach (var item in pageMarkers) temp.Add(item.Key, item.Value);

            if (totalNum == 0) totalNum = 1;//分页使用
            var outObj = new { total_number = totalNum, page_number = pageNum, page_size = pageSize, list = temp.Values };

            string jsonStr = JsonUtil.SerializeToString(outObj);
            var view = this.leftWindow as IWebView;
            if (temp.Values == null || temp.Values.Count == 0)
            {
                view.InvokeScript("updateMarkerList");
            }
            else
            {
                view.InvokeScript("markerList", jsonStr);
            }
        }

        public void OnOffMarkersInteract(bool ison)
        {


            MarkerMethodManager(ison);
        }


        public void MarkerMethodManager(bool ison)
        {
            if (ison)
            {
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectMarker;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelectMarker;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;

                GviMap.AxMapControl.RcCameraChanged -= AxMapControl_RcCameraChanged;
                GviMap.AxMapControl.RcCameraChanged += AxMapControl_RcCameraChanged;
            }
            else
            {
                GviMap.AxMapControl.RcCameraChanged -= AxMapControl_RcCameraChanged;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectMarker;
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
            }
        }

        int index = 0;
        long firstTick = 0;
        string prvguid = string.Empty;
        private void AxMapControl_RcMouseClickSelectMarker(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            //var pt = IntersectPoint;
            //if (pt == null)
            //    return;

            //if (PickResult == null)
            //    return;




            //IRenderPOIPickResult rPoiPk = null;
            //IRenderPolylinePickResult rLinePk = null;
            //IRenderPolygonPickResult rPolyPk = null;
            //if (PickResult is IRenderPOIPickResult)
            //    rPoiPk = PickResult as IRenderPOIPickResult;

            //if (PickResult is IRenderPolylinePickResult)
            //    rLinePk = PickResult as IRenderPolylinePickResult;

            //if (PickResult is IRenderPolygonPickResult)
            //    rPolyPk = PickResult as IRenderPolygonPickResult;

            //if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            //{

            //    string key = pt.X.ToString() + pt.Y.ToString();
            //    var markerList = MarkerHelper.Instance.MarkerDic;

            //    if (index == 0)
            //    {
            //        //markerList.FirstOrDefault(p=>p.)
            //        foreach (var ii in markerList.Values.ToArray())
            //        {

            //            if (rPoiPk?.RenderPOI.Guid.ToString() == ii.guid || rLinePk?.RenderPolyline.Guid.ToString() == ii.guid || rPolyPk?.RenderPolygon.Guid.ToString() == ii.guid)
            //            {
            //                prvguid = ii.guid;
            //                firstTick = DateTime.Now.Ticks;

            //                Console.WriteLine("prvguid:{0}", prvguid);
            //                Console.WriteLine("ii.guid:{0}", ii.guid);
            //                UpdatedPoi(ii);
            //                break;
            //            }
            //        }
            //    }
            //    else if (index == 1)
            //    {
            //        long secondTick = 0;
            //        secondTick = DateTime.Now.Ticks;

            //        long interTime = (secondTick - firstTick) / 1000;

            //        Console.WriteLine(string.Format("firstTick:{0}", firstTick));
            //        Console.WriteLine(string.Format("secondTick:{0}", secondTick));
            //        Console.WriteLine(string.Format("interTime:{0}", interTime));
            //        Console.WriteLine(string.Format("prvguid:{0}", prvguid));


            //        foreach (var ii in markerList.Values.ToArray())
            //        {

            //            if (rPoiPk?.RenderPOI.Guid.ToString() == ii.guid || rLinePk?.RenderPolyline.Guid.ToString() == ii.guid || rPolyPk?.RenderPolygon.Guid.ToString() == ii.guid)
            //            {
            //                if (prvguid == ii.guid && interTime > 0 && interTime < 2000)
            //                {

            //                    Console.WriteLine("prvguid:{0}", prvguid);
            //                    Console.WriteLine("ii.guid:{0}", ii.guid);
            //                    UpdatedPoi(ii);
            //                    updateMarkerPoi(ii.id.ToString(), ii.type.ToString());
            //                }
            //                break;
            //            }
            //        }
            //    }

            //    index++;

            //    if (index > 1)
            //    {
            //        Console.WriteLine(index);
            //        prvguid = null;
            //        index = 0;
            //        firstTick = 0;
            //    }

            //}
            //else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
            //{
            //    index = 0;
            //    //Console.Write("1");
            //}
        }


        public void SearchMarkers(string objStr)
        {
            //var obj = new { keyword = key };
            //string objStr = JsonUtil.SerializeToString(obj);
            UpdateMarkerTable(objStr);
        }

        public void setMarkerPoiVisible(string poiIds, bool isVisilbe)
        {
            if (string.IsNullOrEmpty(poiIds))
                return;

            GviMap.PoiManager.Clear();
            GviMap.LinePolyManager.Clear();

            IList<MarkerModel> markerList = JsonUtil.DeserializeFromString<List<MarkerModel>>(poiIds);

        }

        /// <summary>
        /// 飞入图层
        /// </summary>
        /// <param name="LayerGuid">图层id</param>
        public void flyToRederLayer(string LayerGuid)
        {

            if (_renderLayers.HasValues())
            {
                foreach (var layer in _renderLayers)
                {
                    if (layer.Guid == LayerGuid&& layer.Renderable!=null)
                        GviMap.Camera.FlyToObject(layer.Renderable.Guid, gviActionCode.gviActionFlyTo);
                }
            }
        }

        /// <summary>
        /// 建筑详情
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="buildCode"></param>
        public void showBuildDetail(string buildCode)
        {
            flyToBuild(Helpers.ResourceHelper.FindKey("Building"), buildCode);
            _buildDetailVm.BuildCode = buildCode;
            _buildDetailVm.BuildOid = _buildOid;
            _buildDetailVm.OnChecked();
            setLayerVisible(Helpers.ResourceHelper.FindKey("Smallgrid"), false);
            if (_geoRender != null)
                _geoRender.VisibleMask = gviViewportMask.gviViewNone;
        }

        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="buildCode"></param>
        /// <param name="peopleCode"></param>
        public void showPeopleDetial(string peopleCode)
        {
            //flyToBuild("建筑", buildCode);
        }

        public void flyToMarkerPoi(string poiId)
        {
            if (GviMap.LinePolyManager.ContainsKey(poiId))
                GviMap.LinePolyManager.Flyto(poiId);
            else
                GviMap.PoiManager.FlyTo(poiId);
        }

        /// <summary>
        /// 单元详情
        /// </summary>
        /// <param name="buildCode"></param>
        /// <param name="unitCode"></param>
        public void showUnitDetail(string unitCode)
        {
            _unitDetailVm.UnitId = unitCode;
            _unitDetailVm.OnChecked();
            //flyToBuild("建筑", buildCode);
        }

        /// <summary>
        /// 创建标注
        /// </summary>
        /// <param name="type">1：点；2线；3：面</param>
        public void createMarkerPoi(string type)
        {
            if (type == "1")
                CreatePoiMaker();
            else if (type == "2")
                CreatePolyLineMaker();
            else if (type == "3")
                CreatePolygonMaker();

        }

        private void CreatePoiMaker()
        {

            _poiDetailViewModel = new PoiDetailViewModel();

            _poiDetailViewModel.status = (int)OperationStatus.ADD;
            _poiDetailViewModel.OnChecked();

            _poiDetailViewModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
            _poiDetailViewModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);
        }

        private void UpdatedPoi(MarkerModel markerInfo)
        {
            //MarkerHelper.Instance.UpdateMarkerList(markerInfo);
            //MarkerHelper.Instance.RenderPoi(markerInfo);
            var newPoi = new Dictionary<string, MarkerModel>();
            newPoi.Add(markerInfo.id.ToString(), markerInfo);
            var view = this.leftWindow as IWebView;
            string jsonStr = JsonUtil.SerializeToString(newPoi);
            view.InvokeScript("addMarker", jsonStr);
        }

        private void CreatePolygonMaker()
        {

            _polygonMakerModel = new PolygonMakerViewModel();

            //_polygonMakerModel.status = (int)OperationStatus.ADD;
            _polygonMakerModel.OnChecked();

            _polygonMakerModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
            _polygonMakerModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);
        }

        private void CreatePolyLineMaker()
        {

            _polyLineMakerModel = new MakerLineViewModel();

            //_polyLineMakerModel.status = (int)OperationStatus.ADD;
            _polyLineMakerModel.OnChecked();

            _polyLineMakerModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
            _polyLineMakerModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);
        }

        /// <summary>
        /// 更新标注
        /// </summary>
        /// <param name=""></param>
        public void updateMarkerPoi(string poiId, string geotype)
        {
            if (geotype == "1")
            {
                _poiDetailViewModel = new PoiDetailViewModel();

                _poiDetailViewModel.PoiId = poiId;
                _poiDetailViewModel.status = (int)OperationStatus.EDIT;
                _poiDetailViewModel.OnChecked();
                _poiDetailViewModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
                _poiDetailViewModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);
            }
            else if (geotype == "2")
            {
                _polyLineMakerModel = new MakerLineViewModel
                {
                    PoiId = poiId,
                    IsEdit = true
                };
                _polyLineMakerModel.OnChecked();
                _polyLineMakerModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
                _polyLineMakerModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);

            }
            else if (geotype == "3")
            {
                _polygonMakerModel = new PolygonMakerViewModel
                {
                    PoiId = poiId,
                    IsEdit = true
                };
                _polygonMakerModel.OnChecked();
                _polygonMakerModel.RefreshPoiList -= new Action<MarkerModel>(UpdatedPoi);
                _polygonMakerModel.RefreshPoiList += new Action<MarkerModel>(UpdatedPoi);
            }
        }

        /// <summary>
        /// 打开台账
        /// </summary>
        /// <param name="poiId"></param>
        /// <param name="geotype"></param>
        public void openAccountView(string poiId, string geotype)
        {
            //AddAccountView(2249);
            if (_accountView == null)
            {
                _accountView = new AccountListView();
                _accountView.Closed += (sender, e) => { _accountView = null; };
            }
            _accountView.Owner = Application.Current.MainWindow;
            //_accountView.LoadPage(poiId);
            _accountView.Show();
        }

        /// <summary>
        /// 编辑台账信息
        /// </summary>
        /// <param name="id">台账id</param>
        public void EditAccountView(int id)
        {
            if (editAccountView == null)
                editAccountView = new EditAccountView();
            if (editAccountVModel == null)
            {
                editAccountVModel = new EditAccountVModel();
                editAccountView.DataContext = editAccountVModel;
            }
            editAccountVModel.LoadData(id);
            editAccountView.Owner = Application.Current.MainWindow;
            editAccountView.Show();
        }

        /// <summary>
        /// 删除标注
        /// </summary>
        /// <param name=""></param>
        public void deleteMarkerPoi(string objJson)
        {
            if (MessageBox.Show("确定删除标注吗？", "删除标注", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                return;
            try
            {
                var obj = JsonUtil.DeserializeFromString<dynamic>(objJson);
                List<string> markerIdList = new List<string>();
                foreach (var item in obj.markerId)
                {
                    markerIdList.Add(item.ToString());
                }

                //= obj.markerId;

                int pageNumber = Convert.ToInt32(obj.pageNumber);

                if (MarkerHelper.Instance.DeleteMarker(markerIdList))
                {
                    foreach (var item in markerIdList)
                    {
                        //删除渲染
                        if (GviMap.PoiManager.ContainsKey(item))
                        {
                            if (!GviMap.PoiManager.DeletePoi(item))
                                SystemLog.Log(string.Format("渲染层删除标注失败,标注id={0}", item));
                        }
                        else
                        {
                            if (!GviMap.LinePolyManager.DeletePoi(item))
                                SystemLog.Log(string.Format("渲染层删除标注失败,标注id={0}", item));
                        }
                    }


                }
                else
                    SystemLog.Log(string.Format("服务器删除标注失败,标注id={0}", objJson));
            }
            catch { }
        }

        public void deleteMarkerTag(string MarkerId, string tagId)
        {
            //MarkerHelper.Instance.DeleteMarkerTag(MarkerId, tagId);
        }

        // 添加符号接口
        public void setShpLayerSymbol(string LayerGuid)
        {
            //打开文件
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = FileFilterStrings.XML;
                if (openFile.ShowDialog() == false)
                    return;

                this.progressView.Visibility = Visibility.Visible;
                string fileName = openFile.FileName;

                BeginLoadDsProcess();
                foreach (var layer in _renderLayers)
                {
                    if (layer.Guid == LayerGuid)
                    {
                        DataBaseService.Instance.SetLayerSymbol(layer, fileName);
                        break;
                    }
                }
            }
            finally
            {
                FinishLoadProcess();
            }
        }

        private IDictionary<string, string> GetNetServiceLayerInfo(string url, string servicetype)
        {
            IDictionary<string, string> tempdic = new Dictionary<string, string>();
            var con = new ConnectionInfo();
            con.Database = url;

            switch (servicetype)
            {
                case "WFS":
                    con.ConnectionType = gviConnectionType.gviConnectionWFS;
                    LibraryConfig shpConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                    };
                    IDictionary<string, string> flDic = DataBaseService.Instance.GetWfsServiceLayerGuid(shpConfig);
                    tempdic = flDic;
                    break;
                case "WMTS":
                    ImageLayerConfig imageCfg = new ImageLayerConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        AlphaEnabled = "false",
                        ConType = "File"
                    };

                    break;
                case "Tile":

                    break;
                case "Model":
                    con.ConnectionType = gviConnectionType.gviConnectionCms7Http;
                    break;
            }

            return tempdic;
        }


        private void ShowAddDataStatus(OperateDataStatus status)
        {
            switch (status)
            {
                case OperateDataStatus.NOPERMISSION:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoPermission"));
                    break;
                case OperateDataStatus.LOADFAILED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataFailed"));
                    break;
                case OperateDataStatus.LOADSUCCESSED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataSucceed"));
                    break;
                case OperateDataStatus.DATAEXISTED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataRepeat"));
                    break;
            }
        }

    }
}