using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApplicationConfig;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeDataInterop;
using Gvitech.CityMaker.RenderControl;
using Helpers;
using LayerPropConfig;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.LayerSymbol;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.LayerGroupService;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Services.MovePoiService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Common.Dto;
using static Mmc.Mspace.Common.CommonContract;
using Gvitech.Windows.Utils;
using System.IO;
using Mmc.Mspace.Models.TileLayer;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.Services.DataSourceServices
{
    public class DataBaseService : Singleton<DataBaseService>, IDataBaseService
    {
        private static readonly string WsConfig = Application.StartupPath + "\\" + ConfigPath.WorkSpaceConfig;

        private List<IDataSourceService> _actualityDataSources;

        private List<IDisplayLayer> _dsLayers;

        private List<IImageLayer> _imageLayers;
        private bool _isLoaded;

        private Dictionary<string, List<LayerItemModel>> _layerModelDny;

        private ILocalWsConfigService _localWsCfgSrv;

        private List<IDataSourceService> _poiDataSources;

        private List<IDataSourceService> _shpDataSources;
        private TerrainService _tedServer;
        private List<ITileLayer> _tileLayers;
        public AxRenderControl RenderControl;
        public Action<string> OnLoadingDataSourceProcess { get; set; }

        private IHttpService _httpService;
        private string _poiUrl;
        private List<UserConfig> _userConfig;

        private OpenDataSource _dataOpen;

        private List<string> _layersRemindForUpdate;

        public List<IDisplayLayer> GetActualityLayers()
        {
            List<IDisplayLayer> result = null;
            try
            {
                if (_actualityDataSources.HasValues())
                    result = (from ds in _actualityDataSources
                              from dlay in ds.DisplayLayers
                              select dlay).ToList();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

            return result;
        }

        public List<LayerItemModel> GetAllLayerItemModels()
        {
            List<LayerItemModel> result = null;
            if (_layerModelDny.HasValues())
                result = (from ly in (from kvp in _layerModelDny
                                      where kvp.Key != "城市总览"
                                      select kvp).SelectMany(kvp => kvp.Value)
                          where !ly.Name.Equals("重点场所")
                          select ly).ToList();
            return result;
        }

        public IDisplayLayer GetDisPlayLayerByFCAliasName(string fcName)
        {
            var flag = string.IsNullOrEmpty(fcName);
            IDisplayLayer result;
            if (flag)
            {
                result = null;
            }
            else
            {
                var flag2 = !_dsLayers.HasValues();
                if (flag2)
                    result = null;
                else
                    result = _dsLayers.Find(dly =>
                        !string.IsNullOrEmpty(dly.AliasName) && dly.AliasName.Equals(fcName) ||
                        dly.Name.Equals(fcName));
            }

            return result;
        }

        public IDisplayLayer GetDisPlayLayerByFCGuid(string guid)
        {
            var flag = string.IsNullOrEmpty(guid);
            IDisplayLayer result;
            if (flag)
            {
                result = null;
            }
            else
            {
                var flag2 = !_dsLayers.HasValues();
                if (flag2)
                    result = null;
                else
                    result = _dsLayers.Find(dly => dly.Fc.Guid.ToString().Equals(guid));
            }

            return result;
        }

        public List<IImageLayer> GetImageLayers()
        {
            return _imageLayers;
        }

        public List<string> GetLayersOutCycle()
        {
            return _layersRemindForUpdate;
        }

        public List<LayerItemModel> GetLayerItemModels(string groupName)
        {
            var flag = !_layerModelDny.HasValues();
            List<LayerItemModel> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                var flag2 = string.IsNullOrEmpty(groupName);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    var flag3 = !_layerModelDny.ContainsKey(groupName = groupName.ToLower());
                    if (flag3)
                    {
                        result = null;
                    }
                    else
                    {
                        List<LayerItemModel> list;
                        if (groupName.Equals("重点场所"))
                            list = (from ly in _layerModelDny[groupName]
                                    where !ly.Name.Equals("重点场所")
                                    select ly).ToList();
                        else
                            list = _layerModelDny[groupName];
                        result = list;
                    }
                }
            }

            return result;
        }

        //public List<LocationScene> GetLocationScenes()
        //{
        //    List<LocationScene> result;
        //    try
        //    {
        //        GvitechDataSource gvitechDataSource = new GvitechDataSource(this._planDataSource.DataSource);
        //        PlanLibDao planLibDao = new PlanLibDao(gvitechDataSource);
        //        UPlanService uplanService = new UPlanService(planLibDao, gvitechDataSource.Guid.ToString());
        //        result = uplanService.SelectLocationScene("");
        //    }
        //    catch (Exception innerException)
        //    {
        //        throw new Exception("GetLocationScenes error", innerException);
        //    }
        //    return result;
        //}

        public List<LayerItemModel> GetOtherLayerItemModels(string groupName)
        {
            var flag = !_layerModelDny.HasValues();
            List<LayerItemModel> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                groupName = !string.IsNullOrEmpty(groupName) ? groupName.ToLower() : groupName;
                var source = from kvp in _layerModelDny
                             where kvp.Key != "城市总览"
                             select kvp;
                List<LayerItemModel> list;
                if (!string.IsNullOrEmpty(groupName) && _layerModelDny.ContainsKey(groupName))
                {
                    if (!groupName.Equals("城市总览"))
                    {
                        if (!_layerModelDny.ContainsKey(groupName))
                            list = source.SelectMany(kvp => kvp.Value).ToList();
                        else
                            list = (from kvp in source
                                    where !kvp.Key.Equals(groupName)
                                    select kvp).SelectMany(kvp => kvp.Value).ToList();
                    }
                    else
                    {
                        list = new List<LayerItemModel>();
                    }
                }
                else
                {
                    list = source.SelectMany(kvp => kvp.Value).ToList();
                }

                var source2 = list;
                result = (from ly in source2
                          where ly.Name.Equals("重点场所") || !ServiceManager.GetService<ILayerGroupService>(null)
                                    .GetGroupLayers("重点场所").Contains(ly.Name)
                          select ly).ToList();
            }

            return result;
        }

        public List<IDisplayLayer> GetShowLayers()
        {
            var actualityLayers = GetActualityLayers();
            var shpLayers = GetShpLayers();
            var list = new List<IDisplayLayer>();
            var flag = actualityLayers.HasValues();
            if (flag) list.AddRange(actualityLayers);
            return list;
        }

        public List<IDisplayLayer> GetShpLayers()
        {
            List<IDisplayLayer> result = null;
            try
            {
                if (_shpDataSources.HasValues())
                    result = (from ds in _shpDataSources
                              from dlay in ds.DisplayLayers
                              select dlay).ToList();

                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public List<ITileLayer> GetTileLayers()
        {
            return _tileLayers;
        }

        public void Init(AxRenderControl renderControl)
        {
            var flag = renderControl == null;
            if (flag) throw new ArgumentNullException("renderControl");
            RenderControl = renderControl;
            _dsLayers = new List<IDisplayLayer>();
            _layerModelDny = new Dictionary<string, List<LayerItemModel>>();
            _actualityDataSources = new List<IDataSourceService>();
            _poiDataSources = new List<IDataSourceService>();
            _shpDataSources = new List<IDataSourceService>();
            _tileLayers = new List<ITileLayer>();
            _imageLayers = new List<IImageLayer>();
            _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);

            _userConfig = CacheData.UserInfo.mspace_config?.configs;
            _dataOpen = new OpenDataSource();
            _layersRemindForUpdate = new List<string>();

            LoadData(renderControl);
        }

        public void ShowPOILayers(bool visible = true)
        {
            var flag = !_layerModelDny.HasValues();
            if (!flag)
            {
                Action<LayerItemModel> layers = null;
                _layerModelDny.ForEach(delegate (KeyValuePair<string, List<LayerItemModel>> dny)
                {
                    var flag2 = dny.Value.HasValues();
                    if (flag2)
                    {
                        var value = dny.Value;
                        Action<LayerItemModel> action;
                        if ((action = layers) == null)
                            action = layers = delegate (LayerItemModel layer)
                            {
                                var flag3 = !layer.Name.Equals("派出所辖区");
                                if (flag3)
                                {
                                    layer.IsVisible = visible;
                                    layer.IsChecked = visible;
                                }
                                else
                                {
                                    var visible2 = visible;
                                    if (visible2)
                                    {
                                        layer.IsVisible = !visible;
                                        layer.IsChecked = !visible;
                                    }
                                }
                            };
                        value.ForEach(action);
                    }
                });
            }
        }

        public static IDataBaseService GetDefault(object args = null)
        {
            return Instance;
        }

        public IRenderLayer Add3DTileLayer(TileLayerConfig layercnfg, out OperateDataStatus status)
        {
            try
            {
                LoadingDataSourceProcess();
                if (layercnfg.IsLocal)
                {
                    if (_localWsCfgSrv.TileCfgs.Exist(layercnfg.ConnInfoString))
                    {
                        status = OperateDataStatus.DATAEXISTED;
                        return null;
                    }
                }
                else
                {
                    var exitedLayer = _userConfig?.Find(p => p.config_value.Equals(EncryptUtil.AESEncrypt(layercnfg.ConnInfoString)));
                    if (exitedLayer != null)
                    {
                        status = OperateDataStatus.DATAEXISTED;
                        return null;
                    }
                }

                var tileLayer = _dataOpen.Open3DTileLayer(RenderControl, layercnfg);

                _tileLayers.Add(tileLayer);

                if (layercnfg.IsLocal)
                {
                    layercnfg.Guid = tileLayer.Guid;
                    _localWsCfgSrv.TileCfgs.Add(layercnfg);
                    status = OperateDataStatus.LOADSUCCESSED;
                }
                else
                {
                    string parentId = _userConfig?.Find(m => m.config_name.Equals("倾斜摄影")).id;
                    string parentType = _userConfig?.Find(m => m.config_name.Equals("倾斜摄影")).config_type;
                    var serviceCfg = new UserConfig()
                    {
                        //id="0",
                        config_value = EncryptUtil.AESEncrypt(layercnfg.ConnInfoString),
                        config_type = parentType,
                        config_name = layercnfg.AliasName,
                        guid = tileLayer.Guid,
                        pid = parentId,
                    };

                    var json = JsonUtil.SerializeToString(serviceCfg);
                    int saveNum = ServiceConfigAdd(json);
                    if (saveNum > 0)
                    {
                        serviceCfg.id = saveNum.ToString();
                        _userConfig?.Add(serviceCfg);
                        status = OperateDataStatus.LOADSUCCESSED;
                    }
                    else
                    {
                        status = OperateDataStatus.LOADFAILED;
                    }
                }

                return tileLayer;
            }
            catch (Exception ex)
            {
                var errorcode = RenderControl.GetLastError();
                SystemLog.Log(string.Format("RenderControl状态码：{0}；堆栈信息：{1}", errorcode, ex.StackTrace));
                status = OperateDataStatus.LOADFAILED;
                return null;
            }
        }

        public List<IDisplayLayer> AddFeatureDatasource(LibraryConfig layercnfg, out OperateDataStatus status)
        {
            try
            {
                LoadingDataSourceProcess();

                List<IDisplayLayer> displaysList;

                DataSourceService<GvitechFeatureDataSet> datasource;

                // 判断数据是否存在
                switch (layercnfg.ToConnectionInfo().ConnectionType)
                {
                    case gviConnectionType.gviConnectionShapeFile:
                        if (_localWsCfgSrv.ShpLibs.Exist(layercnfg.ConnInfoString))
                        {
                            status = OperateDataStatus.DATAEXISTED;
                            return null;
                        }

                        datasource = FeatureLayerAddToDisplay(RenderControl, layercnfg);
                        if (datasource.DataSource == null)
                        {
                            status = OperateDataStatus.DATAEXISTED;
                            return null;
                        }
                        displaysList = datasource.DisplayLayers;
                        layercnfg.Guid = datasource.DataSource.Guid.ToString();

                        _shpDataSources.Add(datasource);
                        _localWsCfgSrv.ShpLibs.Add(layercnfg);
                        // 添加搜索标记
                        _localWsCfgSrv.GroupLayerCfgs.AddGroupLayers(_localWsCfgSrv.SearchLayerTag, displaysList[0].AliasName);
                        status = OperateDataStatus.LOADSUCCESSED;

                        break;
                    case gviConnectionType.gviConnectionWFS:
                        var exictedWms = _userConfig?.Find(p => p.config_value == EncryptUtil.AESEncrypt(layercnfg.ConnInfoString) && p.guid == layercnfg.Guid);
                        if (exictedWms != null)
                        {
                            status = OperateDataStatus.DATAEXISTED;
                            return null;
                        }

                        datasource = FeatureLayerAddToDisplay(RenderControl, layercnfg);
                        if (datasource.DataSource == null)
                        {
                            status = OperateDataStatus.LOADFAILED;
                            return null;
                        }
                        displaysList = datasource.DisplayLayers;
                        
                        _shpDataSources.Add(datasource);
                        var twoDCfg = _userConfig?.Find(m => m.config_name == "二维模型");
                        string twoDId = twoDCfg.id;
                        string twoDType = twoDCfg.config_type;
                        var temp = displaysList?.Find(m => m.Fc?.FeatureDataSet?.Guid.ToString() == layercnfg.Guid);
                        string name = temp.Name;
                        var serviceCfg = new UserConfig()
                        {
                            //id="0",
                            sort ="50",
                            config_value = EncryptUtil.AESEncrypt(layercnfg.ConnInfoString),
                            config_type = twoDType,
                            config_name = name,
                            guid = temp.Fc.FeatureDataSet.Guid.ToString(),
                            pid = twoDId,
                            //configAttributes =null
                        };

                        var json = JsonUtil.SerializeToString(serviceCfg);
                        //var resDyc = JsonUtil.DeserializeFromString<dynamic>();
                        int saveNum = ServiceConfigAdd(json);
                        if (saveNum > 0)
                        {
                            serviceCfg.id = saveNum.ToString();
                            _userConfig.Add(serviceCfg);
                            status = OperateDataStatus.LOADSUCCESSED;
                        }
                        else
                            status = OperateDataStatus.LOADFAILED;

                        break;
                    case gviConnectionType.gviConnectionFireBird2x:
                        if (_localWsCfgSrv.ActaulLibs.Exist(layercnfg.ConnInfoString))
                        {
                            status = OperateDataStatus.DATAEXISTED;
                            return null;
                        }

                        datasource = FeatureLayerAddToDisplay(RenderControl, layercnfg);
                        if (datasource.DataSource == null)
                        {
                            status = OperateDataStatus.LOADFAILED;
                            return null;
                        }
                        displaysList = datasource.DisplayLayers;
                        _actualityDataSources.Add(datasource);
                        _localWsCfgSrv.ActaulLibs.Add(layercnfg);
                        //var displaysLocal = datasource.DisplayLayers;
                        // dataset 中数据逐个加入搜索
                        foreach (var item in displaysList)
                            _localWsCfgSrv.GroupLayerCfgs.AddGroupLayers(_localWsCfgSrv.SearchLayerTag, item.AliasName);

                        status = OperateDataStatus.LOADSUCCESSED;

                        break;
                    case gviConnectionType.gviConnectionCms7Http:
                        var exictedItem = _userConfig?.Find(p => p.config_value == EncryptUtil.AESEncrypt(layercnfg.ConnInfoString) && p.config_name.Equals("三维模型"));
                        if (exictedItem != null)
                        {
                            status = OperateDataStatus.DATAEXISTED;
                            return null;
                        }

                        datasource = FeatureLayerAddToDisplay(RenderControl, layercnfg);
                        if (datasource.DataSource == null)
                        {
                            status = OperateDataStatus.LOADFAILED;
                            return null;
                        }

                        displaysList = datasource.DisplayLayers;
                        _actualityDataSources.Add(datasource);
                        var threeDCfg = _userConfig?.Find(m => m.config_name == "三维模型");
                        string threeDType = threeDCfg.config_type;
                        string threeDId = threeDCfg.id;
                        string parentId = string.Empty;
                        var threeCfg = new UserConfig()
                        {
                            //id="0",
                            config_value = EncryptUtil.AESEncrypt(layercnfg.ConnInfoString),
                            config_type = threeDType,
                            config_name = layercnfg.ToConnectionInfo().Database,
                            guid = datasource.GetFeatueDataSetGuid()[0].ToString(),
                            pid = threeDId,
                            //configAttributes =null
                        };

                        var threeDjson = JsonUtil.SerializeToString(threeCfg);
                        int threeNum = ServiceConfigAdd(threeDjson);
                        if (threeNum > 0)
                        {
                            threeCfg.id = threeNum.ToString();
                            _userConfig.Add(threeCfg);
                            status = OperateDataStatus.LOADSUCCESSED;
                            parentId = threeNum.ToString();
                        }
                        else
                        {
                            status = OperateDataStatus.LOADFAILED;
                        }

                        // dataset 中数据逐个加入
                        foreach (var item in displaysList)
                        {
                            var childServiceCfg = new UserConfig()
                            {
                                sort = "50",
                                config_value = EncryptUtil.AESEncrypt(layercnfg.ConnInfoString),
                                config_type = threeDType,
                                config_name = item.AliasName,
                                guid = item.Fc.Guid.ToString(),
                                pid = parentId,
                            };

                            var childJson = JsonUtil.SerializeToString(childServiceCfg);
                            var childNum = ServiceConfigAdd(childJson);
                            if (childNum> 0)
                            {
                                childServiceCfg.id = childNum.ToString();
                                _userConfig.Add(childServiceCfg);
                            }
                        }
                        break;
                    default:
                        status = OperateDataStatus.LOADFAILED;
                        displaysList = null;
                        break;
                }

                GroupLayerItemModels();
                return displaysList;
            }
            catch (Exception ex)
            {
                var errorcode = RenderControl.GetLastError();
                SystemLog.Log(string.Format("RenderControl状态码：{0}；堆栈信息：{1}", errorcode, ex.StackTrace));
                status = OperateDataStatus.LOADFAILED;
                return null;
            }
        }

        private DataSourceService<GvitechFeatureDataSet> FeatureLayerAddToDisplay(AxRenderControl renderControl, LibraryConfig layercnfg)
        {
            var datasource = _dataOpen.OpenFeatureDatasource(RenderControl, layercnfg);
            if (datasource.DataSource == null)
                return null;

            datasource.CreateFeatureLayers();
            IShowLayer showLayer = datasource.DisplayLayers.FirstOrDefault();
            showLayer?.SetVisibleMask(layercnfg.Is2DData, gviViewportMask.gviViewAllNormalView);
            _dsLayers.AddRange(datasource.DisplayLayers);

            return datasource;
        }

        private int ServiceConfigAdd(string jsonstr)
        {
            int num = 0;
            var is_administrator = CacheData.UserInfo.mspace_config.is_administrator;
            if (is_administrator != "1")
                return num;

            string api = GeoServiceDataInterface.AddGeoServiceInf;
            num = 1;
            string json = HttpServiceHelper.Instance.PostRequestForData(api, jsonstr);

            if (!string.IsNullOrEmpty(json))
            { num = JsonUtil.DeserializeFromString<dynamic>(json).id; }

            return num;
        }

        private string SaveServiceLayerSymbol(string jsonstr)
        {
            string result = string.Empty;
            var is_administrator = CacheData.UserInfo.mspace_config.is_administrator;
            if (is_administrator != "1")
                return result;

            //var config = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            //var poiHost = config.poiUrl;
            //string url = string.Format("{0}/api/mspace-config/update-attribute", poiHost);

            //var httpService = new HttpService.HttpService()
            //{
            //    Token = HttpServiceUtil.Token,
            //};

            string api = GeoServiceDataInterface.UpdateLayerAttInf;
            bool success = HttpServiceHelper.Instance.PostRequestForStatus(api, jsonstr);
            //result = httpService.PostJsonData(url, jsonstr);
            return result;
        }


        public bool ServiceConfigDel(string guid)
        {
            bool success = false;
            var is_administrator = CacheData.UserInfo.mspace_config.is_administrator;
            if (is_administrator != "1")
                return success;

            string url = GeoServiceDataInterface.DeleteGeoServiceInf;
            var temp = _userConfig?.Find(m => m.guid == guid);

            var json = JsonUtil.SerializeToString(new { temp.id });
            success = HttpServiceHelper.Instance.PostRequestForStatus(url, json);

            if (success)
            {
                _userConfig?.Remove(temp);
                _userConfig?.RemoveAll(p => p.pid == temp.id);
            }

            return success;
        }

        public IRenderLayer AddImageLayer(ImageLayerConfig layercnfg, out OperateDataStatus status, bool isSafe = true)
        {
            try
            {
                if (layercnfg.IsLocal)
                {
                    if (_localWsCfgSrv.ImgCfgs.Exist(layercnfg.ConnInfoString))
                    {
                        status = OperateDataStatus.DATAEXISTED;
                        return null;
                    }
                }
                else
                {
                    var exitedLayer = _userConfig?.Find(p => p.config_value == EncryptUtil.AESEncrypt(layercnfg.ConnInfoString));
                    if (exitedLayer != null)
                    {
                        status = OperateDataStatus.DATAEXISTED;
                        return null;
                    }
                }

                LoadingDataSourceProcess();

                var imgLayer = _dataOpen.OpenImageLayer(RenderControl, layercnfg);
                if(imgLayer==null)
                {
                    Messages.ShowMessage("请检查数据后加载");
                    status = OperateDataStatus.LOADFAILED;
                    
                }
                else { 
                layercnfg.AliasName = imgLayer.AliasName;
                layercnfg.Guid = imgLayer.Guid;
                if (isSafe)
                {
                    _imageLayers.Add(imgLayer);
                    if (layercnfg.IsLocal)
                    {
                        _localWsCfgSrv.ImgCfgs.Add(layercnfg);
                        status = OperateDataStatus.LOADSUCCESSED;
                    }
                    else
                    {
                        string parentId = _userConfig?.Find(m => m.config_name.Equals("影像")).id;
                        string parentType = _userConfig?.Find(m => m.config_name.Equals("影像")).config_type;
                        var serviceCfg = new UserConfig()
                        {
                            //id="0",
                            config_value = EncryptUtil.AESEncrypt(layercnfg.ConnInfoString),
                            config_type = parentType,
                            config_name = imgLayer.AliasName,
                            guid = imgLayer.Guid,
                            pid = parentId,
                        };

                        var json = JsonUtil.SerializeToString(serviceCfg);

                        var tileNum = ServiceConfigAdd(json);
                        if (tileNum>0)
                        {
                            serviceCfg.id = tileNum.ToString();
                            _userConfig.Add(serviceCfg);
                            status = OperateDataStatus.LOADSUCCESSED;
                        }
                        else
                        {
                            status = OperateDataStatus.LOADFAILED;
                        }
                    }
                }
                else
                {
                    status = OperateDataStatus.LOADSUCCESSED;
                }
                }
                return imgLayer;
            }
            catch (Exception ex)
            {
                var errorcode = RenderControl.GetLastError();
                SystemLog.Log(string.Format("RenderControl状态码：{0}；堆栈信息：{1}", errorcode, ex.StackTrace));
                status = OperateDataStatus.LOADFAILED;
                return null;
            }
        }

        public IRenderLayer OpenImageLayer(ImageLayerConfig layercnfg, out OperateDataStatus status, bool isSafe = true)
        {
            try
            {
                var imgLayer = _dataOpen.OpenImageLayer(RenderControl, layercnfg);

                status = OperateDataStatus.LOADSUCCESSED;

                return imgLayer;
            }
            catch (Exception ex)
            {
                var errorcode = RenderControl.GetLastError();
                SystemLog.Log(string.Format("RenderControl状态码：{0}；堆栈信息：{1}", errorcode, ex.StackTrace));
                status = OperateDataStatus.LOADFAILED;
                return null;
            }
        }

        public IDictionary<string, string> GetWfsServiceLayerGuid(LibraryConfig layercnfg)
        {
            IDictionary<string, string> layerDic = new Dictionary<string, string>();

            IDataSourceFactory dataSourceFactory = new DataSourceFactory();
            var ds = dataSourceFactory.OpenDataSource(layercnfg.ToConnectionInfo());
            var list = ds.OpenAllFeatureDataSet();
            if (list != null && list.Count > 0)
                list.ForEach(delegate (IFeatureDataSet fds) { layerDic.Add(fds.Guid.ToString(), fds.Name); });
            return layerDic;
        }

        public void LoadingDataSourceProcess()
        {
            OnLoadingDataSourceProcess?.Invoke(ResourceHelper.FindKey("Loading"));
        }

        public void RemoveSingleLayer(IRenderLayer rLayer)
        {
            try
            {
                if (rLayer.LayerType == RenderLayerType.TileLayer)
                {
                    var layer = rLayer as TileLayer;
                    if (layer != null && _tileLayers.Contains(layer))
                    {

                        if (layer.IsLocal)
                            _localWsCfgSrv.TileCfgs.Delete(layer.Layer.ConnectionInfo);
                        else
                        {
                            //if (ServiceConfigDel(layer.Guid))
                            //{
                            //    //status = OperateDataStatus.SUCCESSED;
                            //}
                            //else
                            //{
                            //    //status = OperateDataStatus.FAILED;
                            //}
                        }
                        RenderControl.ObjectManager.DeleteObject(layer.Layer.Guid);
                        _tileLayers.Remove(layer);
                    }
                }
                else if (rLayer.LayerType == RenderLayerType.ImageLayer)
                {
                    var layer = rLayer as ImageLayer;
                    if (layer != null)
                    {
                        if (_imageLayers.Contains(layer))
                        {
                            if (layer.IsLocal)
                                _localWsCfgSrv.ImgCfgs.Delete(layer.Layer.ConnectionString);
                            else
                                ServiceConfigDel(layer.Guid);
                            _imageLayers.Remove(layer);
                        }

                        RenderControl.ObjectManager.DeleteObject(layer.Layer.Guid);
                    }
                }
                else if (rLayer.LayerType == RenderLayerType.FeatureLayer)
                {
                    var layer = _dsLayers.FirstOrDefault(p => p.Guid == rLayer.Guid);
                    if (layer != null)
                    {
                        if (layer.Fc.DataSource.ConnectionInfo.ConnectionType ==
                            gviConnectionType.gviConnectionShapeFile)
                        {
                            _localWsCfgSrv.ShpLibs.Delete(layer.Fc.DataSource.ConnectionInfo.ToConnectionString());
                            _localWsCfgSrv.GroupLayerCfgs.DelGroupLayers(_localWsCfgSrv.SearchLayerTag, layer.Name);
                            _localWsCfgSrv.FeatureLayerProps.Delete(p => p.DataSourceGuid == layer.Fc.DataSource.Guid.ToString());
                        }
                        else if (layer.Fc.DataSource.ConnectionInfo.ConnectionType ==
                            gviConnectionType.gviConnectionWFS)
                        {
                            ServiceConfigDel(layer.Fc.FeatureDataSet.Guid.ToString());
                        }
                        var dsSrv = _shpDataSources.FirstOrDefault(p =>
    p.DataSource.Guid == layer.Fc.DataSource.Guid);
                        RemoveLayerItem(dsSrv?.DisplayLayers);
                        _shpDataSources.Remove(dsSrv);
                        if (layer.FLyers.Count > 0)
                            RenderControl.ObjectManager.DeleteObject(layer.FLyers[0].Guid);
                        _dsLayers.Remove(layer);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void RemoveFDbDataSource(List<IDisplayLayer> displayLayers)
        {
            if (displayLayers.HasValues())
            {
                var dataset = displayLayers[0].Fc.FeatureDataSet;
                var ds = displayLayers[0].Fc.DataSource;
                LibraryConfig lyrCfg = null;
                if (ds.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionFireBird2x)
                {
                    lyrCfg = _localWsCfgSrv.ActaulLibs.FindOne(p => p.ConnInfoString == displayLayers[0].Fc.DataSource.ConnectionInfo.ToConnectionString());
                    _localWsCfgSrv.ActaulLibs.Delete(lyrCfg?.ConnInfoString);
                }
                else if (ds.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionCms7Http)
                {
                    var temp = _userConfig?.Find(p => p.guid == dataset.Guid.ToString());

                    ServiceConfigDel(temp.guid);
                }
                else
                    return;

                RemoveLayerItem(displayLayers);
                foreach (var item in displayLayers)
                {
                    var layers = item.FLyers;
                    // 删除搜索标签
                    if (ds.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionFireBird2x)
                        foreach (var flyr in layers)
                        {
                            _localWsCfgSrv.GroupLayerCfgs.DelGroupLayers(_localWsCfgSrv.SearchLayerTag, item.Fc.Alias);
                            GviMap.ObjectManager.DeleteObject(flyr.Guid);
                        }

                    _dsLayers.Remove(item);
                    item.Fc.ReleaseComObject();
                }

                // 释放对象
                var dsSrv = _actualityDataSources.FirstOrDefault(p => p.DataSource.Guid == ds.Guid);
                _actualityDataSources.Remove(dsSrv);
                dataset.Dispose();
                ds.Close();
                ds.Dispose();
                GroupLayerItemModels();
            }
        }

        private void SymbolRenderOfFeatureLayer(IFeatureLayer featureLayer,string textRenderXml,string geoRenderXml)
        {
            featureLayer.SetTextRender(RenderControl.ObjectManager.CreateTextRenderFromXML(textRenderXml));
            featureLayer.SetGeometryRender(RenderControl.ObjectManager.CreateGeometryRenderFromXML(geoRenderXml));
        }

        public void SetLayerSymbol(IRenderLayer rLayer, string fileName)
        {
            try
            {
                if (rLayer.LayerType == RenderLayerType.FeatureLayer)
                {
                    var layer = _dsLayers.FirstOrDefault(p => p.Guid == rLayer.Guid);
                    if (layer != null)
                    {

                        var heightStyle = string.Empty;
                        // 转换 xml 文档
                        var symbol = new LayerStyle80();
                        var textRenderXml = symbol.getRenderXml(fileName, "TextRender", ref heightStyle);
                        var geoRenderXml = symbol.getRenderXml(fileName, "GeometryRender", ref heightStyle);

                        //var featureLayer = (IFeatureLayer)rLayer.Renderable;
                        IFeatureLayer featureLayer = (IFeatureLayer)rLayer.Renderable;
                        //featureLayer.SetTextRender(RenderControl.ObjectManager.CreateTextRenderFromXML(textRenderXml));
                        //featureLayer.SetGeometryRender(RenderControl.ObjectManager.CreateGeometryRenderFromXML(geoRenderXml));
                        //var georender = layerSymbolRender.GetGeometryRender();
                        //layerSymbolRender.SetGeometryRender(georender);

                        SymbolRenderOfFeatureLayer(featureLayer, textRenderXml, geoRenderXml);

                        if (layer.Fc.DataSource.ConnectionInfo.ConnectionType ==
                            gviConnectionType.gviConnectionShapeFile || layer.Fc.DataSource.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionFireBird2x)
                        {
                            var layerProp =
                                _localWsCfgSrv.FeatureLayerProps.FindOne(p => p.FcGuid == layer.Fc.Guid.ToString());
                            if (layerProp == null)
                            {
                                layerProp = new FeatureLayerProp
                                {
                                    GeoRender = geoRenderXml,
                                    TxtRender = textRenderXml,
                                    FcGuid = layer.Fc.Guid.ToString(),
                                    MaxVisibleDistance = featureLayer.MaxVisibleDistance,
                                    MinVisiblePixels = featureLayer.MinVisiblePixels,
                                    ForceCullMode = featureLayer.ForceCullMode,
                                    GeometryFiledName = featureLayer.GeometryFieldName,
                                    FcName = layer.Fc.Name,
                                    DataSetGuid = layer.Fc.FeatureDataSet.Guid.ToString(),
                                    DataSourceGuid = layer.Fc.DataSource.Guid.ToString()
                                };
                                _localWsCfgSrv.FeatureLayerProps.Add(layerProp);
                            }
                            else
                            {
                                layerProp.GeoRender = geoRenderXml;
                                layerProp.TxtRender = textRenderXml;
                                _localWsCfgSrv.FeatureLayerProps.Update(layerProp);
                            }
                        }
                        else if(layer.Fc.DataSource.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionWFS|| layer.Fc.DataSource.ConnectionInfo.ConnectionType == gviConnectionType.gviConnectionCms7Http)
                        {
                            //string twoDId = _userConfig?.Find(m => m.config_name.Equals("二维模型"))?.id;
                            //if (string.IsNullOrEmpty(twoDId)) return;
                            var serviceCfg = _userConfig.Find(p => p.guid == layer.Fc.Guid.ToString());
                            if (serviceCfg == null) return;
                            var AttrList = serviceCfg.configAttributes;

                            var txtid = AttrList?.Find(p => p.attribute_name == "TextRender")?.id;
                            var geoid = AttrList?.Find(p => p.attribute_name == "GeometryRender")?.id;
                            var texAtt = new ConfigAttribute()
                            {
                                id = txtid,
                                config_id = serviceCfg.id,
                                attribute_name = "TextRender",
                                attribute_value = textRenderXml
                            };
                            var txtjson = JsonUtil.SerializeToString(texAtt);
                            var txtresDyc = JsonUtil.DeserializeFromString<dynamic>(SaveServiceLayerSymbol(txtjson));
                            if (txtresDyc?.status == 1)
                            {
                                //_userConfig.Add(serviceCfg);
                                //serviceCfg.configAttributes.Add(texAtt);
                            }

                            var geoAtt = new ConfigAttribute()
                            {
                                id = geoid,
                                config_id = serviceCfg.id,
                                attribute_name = "GeometryRender",
                                attribute_value = geoRenderXml
                            };


                            var json = JsonUtil.SerializeToString(geoAtt);
                            var resDyc = JsonUtil.DeserializeFromString<dynamic>(SaveServiceLayerSymbol(json));
                            if (resDyc?.status == 1)
                            {
                                //_userConfig.Add(serviceCfg);
                                //serviceCfg.configAttributes.Add(geoAtt);
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

        private void GroupLayerItemModels()
        {
            if (!_dsLayers.HasValues()) return;
            var groupNames = _localWsCfgSrv.GroupLayerCfgs.GetGroupNames();
            if (!groupNames.HasValues()) return;
            string[] arry = null;
            Func<IDisplayLayer, bool> layers = null;
            groupNames.ToList().ForEach(delegate (string group)
            {
                arry = _localWsCfgSrv.GroupLayerCfgs.GetGroupLayers(@group);
                if (!arry.HasValues()) return;
                IEnumerable<IDisplayLayer> dsLayers = _dsLayers;
                Func<IDisplayLayer, bool> predicate;
                if ((predicate = layers) == null)
                    predicate = layers = dly => arry.Contains(dly.AliasName.ToLower());
                var source = dsLayers.Where(predicate);

                var list = source.ToList().Select(delegate (IDisplayLayer dly)
                {
                    var featureLayer = dly.FLyers.FirstOrDefault();
                    return new LayerItemModel
                    {
                        Name = dly.AliasName,
                        Group = @group,
                        IsVisible = featureLayer != null && featureLayer.VisibleMask.GetIsVisible(),
                        Parameters = dly
                    };
                }).ToList();
                if (!_layerModelDny.ContainsKey(@group))
                {
                    _layerModelDny.AddEx(@group, list);
                }
                else
                {
                    var layritems = _layerModelDny[@group];
                    foreach (var item in list)
                        if (!layritems.Exists(p => p.Name == item.Name))
                            layritems.Add(item);
                }
            });
        }

        private void InSertMoveLayerItemModels()
        {
            var dny = ServiceManager.GetService<IMovePoiService>(null).GroupLayerItemModels();
            List<LayerItemModel> lst;
            _layerModelDny.ToList().ForEach(delegate (KeyValuePair<string, List<LayerItemModel>> kvp)
            {
                var flag = (lst = dny[kvp.Key]) != null;
                if (flag) kvp.Value.AddRange(lst);
            });
        }

        private void LoadData(AxRenderControl renderControl)
        {
            var isLoaded = _isLoaded;
            if (!isLoaded)
            {
                ResolveWorkSpaceConfig(WsConfig, renderControl);
                GroupLayerItemModels();

                //暂时屏蔽移动警力
                //TaskFactory taskFactory = new TaskFactory();
                //CancellationToken cancellationToken = default(CancellationToken);
                //taskFactory.StartNew(new Action(this.LoadMovePois), cancellationToken).ContinueWith(delegate (Task task)
                //{
                //    this.InSertMoveLayerItemModels();
                //}, cancellationToken);

                _isLoaded = true;
            }
        }

        private void LoadMovePois()
        {
            if (ServiceManager.HasService<IOracleDataService>())
                ServiceManager.GetService<IOracleDataService>(null).InitEnv();
            if (ServiceManager.HasService<IMovePoiService>())
                ServiceManager.GetService<IMovePoiService>(null).StartWork();
            ServiceManager.GetService<IMovePoiService>(null).TestUrl();
        }

        private void RemoveLayerItem(List<IDisplayLayer> displayLayers)
        {
            List<LayerItemModel> layerItems = null;
            if (_layerModelDny.ContainsKey(_localWsCfgSrv.SearchLayerTag))
                layerItems = _layerModelDny[_localWsCfgSrv.SearchLayerTag];
            if (layerItems.HasValues() && displayLayers.HasValues())
                foreach (var item in displayLayers)
                {
                    var layeritem = layerItems.FirstOrDefault(p => p.Name == item.AliasName);
                    if (layeritem != null)
                        layerItems.Remove(layeritem);
                }
        }

        private void ResolveWorkSpaceConfig(string configPath, AxRenderControl renderControl)
        {
            Console.WriteLine("开始加载地图数据资源");
            var netVarify = new NetConnectableVarify();
            var flag3 = renderControl == null;
            if (renderControl != null)
                try
                {
                    SystemLog.Log("开始解析工作空间配置：\n开始加载网络影像数据：", 0);

                    string imageId = _userConfig?.Find(m => m.config_name.Equals("影像"))?.id;
                    var ServiceImageLayers = _userConfig?.FindAll(p => p.pid == imageId);
                    if (ServiceImageLayers.HasValues())
                    {
                        ServiceImageLayers.ForEach(item =>
                        {
                            try
                            {
                                string url = EncryptUtil.AESDecrypt(item.config_value);
                                if (string.IsNullOrEmpty(url)) return;

                                if (netVarify.IsUrlConnectable(url))
                                {
                                    var laryerCfg = new ImageLayerConfig()
                                    {
                                        Id = Convert.ToInt32(item.id),
                                        ConnInfoString = url,
                                        AliasName = item.config_name,
                                        Guid = item.guid,
                                        AlphaEnabled = "false", //启用A通道
                                        ConType = "WMTS"
                                    };
                                    if (item.config_name == "World_Imagery")//天地图设置为false，以后为将A通道配置UI开发出来 addby  liangms 2019-9-9
                                        laryerCfg.AlphaEnabled = "false";
                                        var imgLayer = _dataOpen.OpenImageLayer(renderControl, laryerCfg);
                                    if (imgLayer != null)
                                        _imageLayers.Add(imgLayer);
                                }
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });
                    }
                    SystemLog.Log("网络Image图层结束...\n开始加载网络三维数据：\n", 0);

                    string threeDId = _userConfig?.Find(m => m.config_name.Equals("三维模型"))?.id;
                    var ServiceActualityLibs = _userConfig?.FindAll(p => p.pid == threeDId);
                    if (ServiceActualityLibs.HasValues())
                        ServiceActualityLibs.ToList().ForEach(item =>
                        {
                            try
                            {
                                string url = EncryptUtil.AESDecrypt(item.config_value);
                                if (string.IsNullOrEmpty(url)) return;

                                if (netVarify.IsModelUrlConnectable(url))
                                {
                                    var laryerCfg = new LibraryConfig()
                                    {
                                        Id = Convert.ToInt32(item.id),
                                        ConnInfoString = url,
                                        AliasName = item.config_name,
                                        Is2DData = false
                                    };

                                    var datasource = _dataOpen.OpenFeatureDatasource(renderControl, laryerCfg);
                                    if (datasource.DataSource != null)
                                        _actualityDataSources.Add(datasource);
                                }
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });

                    SystemLog.Log("网络三维数据结束...\n开始加载网络倾斜数据：\n", 0);

                    string tileId = _userConfig?.Find(m => m.config_name.Equals("倾斜摄影"))?.id;
                    var ServiceTileLayers = _userConfig?.FindAll(p => p.pid == tileId);
                    if (ServiceTileLayers.HasValues())
                    {
                        ServiceTileLayers.ForEach(item =>
                        {
                            try
                            {
                                string url = EncryptUtil.AESDecrypt(item.config_value);
                                if (string.IsNullOrEmpty(url)) return;

                                if (netVarify.IsTileUrlConnectable(url))
                                {
                                    var laryerCfg = new TileLayerConfig()
                                    {
                                        Id = Convert.ToInt32(item.id),
                                        ConnInfoString = url,
                                        AliasName = item.config_name,
                                        Guid = item.guid,
                                        IsLocal = false
                                        //ConType = "WMTS"
                                    };

                                    var tileLayer = _dataOpen.Open3DTileLayer(renderControl, laryerCfg);
                                    if (tileLayer != null)
                                    {
                                        _tileLayers.Add(tileLayer);
                                        LoadTileModifier(tileLayer);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });
                    }

                    SystemLog.Log("网络倾斜数据结束...\n开始加载网络二维数据：\n", 0);

                    string twoDId = _userConfig?.Find(m => m.config_name.Equals("二维模型"))?.id;
                    var ServiceShpLibs = _userConfig?.FindAll(p => p.pid == twoDId);
                    if (ServiceShpLibs.HasValues())
                        ServiceShpLibs.ToList().ForEach(item =>
                        {
                            try
                            {
                                string url = EncryptUtil.AESDecrypt(item.config_value);
                                if (string.IsNullOrEmpty(url)) return;
                                if (netVarify.IsUrlConnectable(url.Split('=')[2]))
                                {
                                    var laryerCfg = new LibraryConfig()
                                    {
                                        Id = Convert.ToInt32(item.id),
                                        ConnInfoString = url,
                                        AliasName = item.config_name,
                                        Guid = item.guid,
                                        Is2DData = true
                                    };
                                    var datasource = _dataOpen.OpenFeatureDatasource(renderControl, laryerCfg);
                                    if (datasource.DataSource != null)
                                        _shpDataSources.Add(datasource);

                                    if (item.configAttributes?.Count > 0)
                                    {
                                        string geoRenderstr = item.configAttributes.Find(p => p.attribute_name == "GeometryRender")?.attribute_value;

                                        datasource.GeometryRender = renderControl.ObjectManager.CreateGeometryRenderFromXML(geoRenderstr);

                                        string txtRenderstr = item.configAttributes.Find(p => p.attribute_name == "TextRender")?.attribute_value;
                                        datasource.TextRender = renderControl.ObjectManager.CreateTextRenderFromXML(txtRenderstr);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });


                    SystemLog.Log("网络二维数据结束...\n开始加载本地三维数据：\n", 0);

                    var SysActualityLibs = _localWsCfgSrv.ActaulLibs.FindAll();
                    if (SysActualityLibs.HasValues())
                        SysActualityLibs.ToList().ForEach(delegate (LibraryConfig lib)
                        {
                            try
                            {
                                if (!File.Exists(lib.ConnInfoString.Split("=")[2]))
                                {
                                    SystemLog.Log(string.Format("该地址下找不到文件：{0}", lib.ConnInfoString.Split("=")[2]));
                                    return;
                                }
                                var datasource = _dataOpen.OpenFeatureDatasource(renderControl, lib);
                                if (datasource.DataSource != null)
                                    _actualityDataSources.Add(datasource);
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });

                    SystemLog.Log("三维模型库解析结束...\n开始解析POI：\n", 0);
                    //if (workSpaceConfig.SysPoiLibs != null)
                    //    workSpaceConfig.SysPoiLibs.ToList().ForEach(delegate (LibraryConfig lib)
                    //    {
                    //        _poiDataSources.Add(
                    //            new DataSourceService<POIFeatureDataSet>(renderControl,
                    //                lib.ToConnectionInfo()));
                    //    });

                    //SystemLog.Log("POI解析结束...\n开始解析Shp：\n", 0);
                    var sysShps = _localWsCfgSrv.ShpLibs.FindAll();
                    if (sysShps.HasValues())
                        sysShps.ToList().ForEach(delegate (LibraryConfig lib)
                        {
                            try
                            {
                                if (!File.Exists(lib.ConnInfoString.Split("=")[2]))
                                {
                                    SystemLog.Log(string.Format("该地址下找不到文件：{0}", lib.ConnInfoString.Split("=")[2]));
                                    return;
                                }

                                DataSourceService<GvitechFeatureDataSet> datasource = _dataOpen.OpenFeatureDatasource(renderControl, lib);

                                IFeatureLayerProp layerProp = null;
                                layerProp = _localWsCfgSrv.FeatureLayerProps.FindOne(p =>
                                    p.DataSourceGuid == datasource.DataSource.Guid.ToString());

                                if (layerProp != null)
                                {
                                    datasource.GeometryRender =
                                        GviMap.ObjectManager.CreateGeometryRenderFromXML(layerProp.GeoRender);
                                }
                                else
                                {
                                    //暂时针对point的shp图层做特殊处理
                                    if (datasource.IsPointShpFile())
                                    {
                                        var layerName = datasource.GetShpName();
                                        var geoTxt =
                                            "<SimpleGeometryRender HeightStyle=\"Absolute\" HeightOffset=\"6\" GroupField=\"\" >\r\n <ImagePointSymbol  ImageName=\"项目数据\\shp\\IMG_POI\\{0}.png\" Script=\"\" PivotAlignment=\"BottomCenter\" Size=\"36\" />\r\n</SimpleGeometryRender>\r\n\t";
                                        geoTxt = string.Format(geoTxt, layerName);
                                        datasource.GeometryRender =
                                            renderControl.ObjectManager.CreateGeometryRenderFromXML(geoTxt);
                                    }
                                }

                                if (layerProp != null)
                                {
                                    datasource.TextRender =
                                        GviMap.ObjectManager.CreateTextRenderFromXML(layerProp.TxtRender);
                                }
                                else
                                {
                                    //暂时针对point的shp图层做特殊处理
                                    if (datasource.IsPointShpFile())
                                    {
                                        var textPointShp =
                                            "<SimpleTextRender Expression=\"$(Name)\" DynamicPlacement=\"true\" MinimizeOverlap=\"true\" RemoveDuplicate=\"true\" >\r\n\t\t<TextSymbol DrawLine=\"false\" LockMode=\"Decal\" MaxVisualDistance=\"10000\" MinVisualDistance=\"0\" Priority=\"1\" PivotOffsetX=\"3\" PivotOffsetY=\"0\" PivotAlignment=\"CenterLeft\" VerticalOffset=\"0\">\r\n\t\t\t  <TextAttribute TextSize=\"12\" Italic=\"false\" Bold=\"true\" Underline=\"false\" Font=\"楷体\" TextColor=\"FFFFFFFF\" OutlineColor=\"FF191919\" BackgroundColor=\"00191919\" />\r\n\t\t</TextSymbol>\r\n\t</SimpleTextRender>\r\n";
                                        datasource.TextRender =
                                            renderControl.ObjectManager.CreateTextRenderFromXML(textPointShp);
                                    }
                                }

                                _shpDataSources.Add(datasource);
                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });

                    SystemLog.Log("Shp解析结束...\n开始解析地形：\n", 0);
                    ITerrainConfig sysTerrain = _localWsCfgSrv.TerrainConfig?.Find();
                    if (sysTerrain != null)
                    {
                        if (_tedServer == null)
                            _tedServer = new TerrainService(renderControl);
                        if (sysTerrain != null)
                        {
                            _tedServer.RegisterTerrain(sysTerrain.Server, sysTerrain.Password);
                            renderControl.Terrain.Opacity = sysTerrain.DemOpacity;
                            renderControl.Terrain.DemAvailable = sysTerrain.DemAvailable;
                            _tedServer.FlyToTerrain();
                        }
                    }

                    SystemLog.Log("地形解析结束...\n开始创建图层：\n", 0);
                    SystemLog.Log("开始创建现状图层：\n", 0);
                    _actualityDataSources.ForEach(delegate (IDataSourceService ds)
                    {
                        try
                        {
                            var fds = ds.GetFeatueDataSetGuid();
                            var dicProps = new Dictionary<string, List<FeatureLayerProp>>();
                            foreach (var item in fds)
                            {
                                var props = _localWsCfgSrv.FeatureLayerProps.Find(p => p.DataSetGuid == item);
                                if (props.HasValues())
                                    dicProps.Add(item, props);
                            }

                            ds.CreateFeatureLayers(dicProps);
                            _dsLayers.AddRange(ds.DisplayLayers);


                            var displayList = ds.DisplayLayers;
                            
                            if (ds.IsNetLib)
                            {
                                string dataSetId = ServiceActualityLibs.Find(p => p.guid == fds[0].ToString())?.id;
                                List<UserConfig> layerList = _userConfig.FindAll(p => p.pid == dataSetId);
                                foreach (var item in displayList)
                                {
                                    var attrArr = layerList.Find(p => p.guid == item.Fc.Guid.ToString())?.configAttributes;
                                    if (attrArr?.Count > 0)
                                    {
                                        string geoRenderstr = attrArr.Find(p => p.attribute_name == "GeometryRender")?.attribute_value;
                                        string txtRenderstr = attrArr.Find(p => p.attribute_name == "TextRender")?.attribute_value;

                                        var featureLayer = item.FLyers.FirstOrDefault();
                                        SymbolRenderOfFeatureLayer(featureLayer, txtRenderstr, geoRenderstr);
                                    }
                                }
                            }
                            else
                            {
                              

                            }

                            

                        }
                        catch (Exception ex)
                        {
                            SystemLog.Log(ex);
                        }
                    });
                    SystemLog.Log("创建现状图层结束...\n开始创建POI图层：\n", 0);
                    _poiDataSources.ForEach(delegate (IDataSourceService poids)
                    {
                        poids.CreateFeatureLayers();
                        _dsLayers.AddRange(poids.DisplayLayers);
                    });
                    SystemLog.Log("创建POI图层结束...\n开始创建Shp图层：\n", 0);
                    _shpDataSources.ForEach(delegate (IDataSourceService shp)
                    {
                        shp.CreateFeatureLayers();
                        IShowLayer showLayer = shp.DisplayLayers.FirstOrDefault();
                        if (showLayer != null)
                            showLayer.SetVisibleMask(true, gviViewportMask.gviViewAllNormalView);
                        _dsLayers.AddRange(shp.DisplayLayers);
                    });

                    SystemLog.Log("创建Shp图层结束...\n开始创建瓦片图层：\n", 0);
                    var tileLyerCfgs = _localWsCfgSrv.TileCfgs.FindAll();
                    if (tileLyerCfgs.HasValues())
                        tileLyerCfgs.ForEach(layercnfg =>
                        {
                            try
                            {
                                if (!File.Exists(layercnfg.ConnInfoString))
                                {
                                    SystemLog.Log(string.Format("该地址下找不到文件：{0}", layercnfg.ConnInfoString));
                                    return;
                                }

                                layercnfg.IsLocal = true;
                                var tileLayer = _dataOpen.Open3DTileLayer(renderControl, layercnfg);
                                if (tileLayer != null)
                                {
                                    _tileLayers.Add(tileLayer);
                                    LoadTileModifier(tileLayer);
                                }

                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });

                    SystemLog.Log("创建瓦片图层结束...\n开始创建Image图层：\n", 0);
                    var SysImageLayers = _localWsCfgSrv.ImgCfgs.FindAll();
                    if (SysImageLayers.HasValues())
                    {
                        DateTime today = DateTime.Today;
                        SysImageLayers.ForEach(layercnfg =>
                        {
                            try
                            {
                                if (!File.Exists(layercnfg.ConnInfoString))
                                {
                                    SystemLog.Log(string.Format("该地址下找不到文件：{0}", layercnfg.ConnInfoString));
                                    return;
                                }

                                var imgLayer = _dataOpen.OpenImageLayer(renderControl, layercnfg);
                                if (imgLayer != null)
                                    _imageLayers.Add(imgLayer);

                                if (layercnfg.AddTime.AddDays(layercnfg.CycleTime) <= today)
                                {
                                    _layersRemindForUpdate.Add(layercnfg.AliasName);
                                }

                            }
                            catch (Exception ex)
                            {
                                SystemLog.Log(ex);
                            }
                        });
                    }

                }
                catch (COMException ex)
                {
                    SystemLog.Log("解析工作空间配置出现组件异常", 0);
                    SystemLog.Log(ex);
                }
                catch (Exception ex2)
                {
                    SystemLog.Log("解析工作空间配置出现程序异常", 0);
                    SystemLog.Log(ex2);
                }

            Console.WriteLine("结束加载地图数据资源");
        }


        /// <summary>
        /// 瓦片压平
        /// </summary>
        /// <param name="tileLayerCfg"></param>
        private void LoadTileModifier(ITileLayer tileLayerCfg)
        {
            List<TileModifyGeo> geos = LocalWsConfigService.Instance.TileModifyGeos.Find(p => p.ConStr == tileLayerCfg.Layer.ConnectionInfo);
            var spatialCRS = GviMap.CrsFactory.CreateFromWKT(tileLayerCfg.Layer.GetWKT()) as ISpatialCRS;
            var mulpolygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
            mulpolygon.SpatialCRS = spatialCRS;
            if (geos.HasValues())
            {
                foreach (var item in geos)
                {
                    var geo = GviMap.GeoFactory.CreateFromWKT(item.Geom) as IPolygon;
                    geo.SpatialCRS = spatialCRS;
                    mulpolygon.AddPolygon(geo);
                }
                tileLayerCfg.Layer.SetModifiers(mulpolygon);
            }
        }
        private bool IPConnectable(string url)
        {
            bool isConnectable = false;
            if (!url.Contains("tianditu"))
            {
                //string ipStr = netVarify.MatchIpFromStr(url);
                //isConnectable = netVarify.PingIP(ipStr);

            }
            return isConnectable;
        }
    }
}