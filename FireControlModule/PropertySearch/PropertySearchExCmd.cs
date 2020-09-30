using FireControlModule.UnitInfo;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.ShowCaptureObjectService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FireControlModule
{
    public class PropertySearchExCmd : SimpleCommand
    {
        private List<IDisplayLayer> _actaulLyrs;
        private Full3dView _full3dView;
        private KeyValuePair<string, IDisplayLayer> _oldSelectFc;
        private IDisplayLayer _threeSmallLayer;
        private UnitDetailViewModel _unitViewModel;
        private VideoMonitorView _videoMonitorView;
        private Process _process;
        // 三小场所
        private List<LayerItemModel> layers = new List<LayerItemModel>();

        public PropertySearchExCmd() : base()
        {
            _videoMonitorView = new VideoMonitorView();
            _videoMonitorView.DataContext = new { WindowTitle = "视频监控", ContentWidth = 400, ContentHeight = 300 };
            //_full3dView = new Full3dView();
            //_full3dView.Closed += (sender, e) => { _full3dView = null; };
            //_full3dView = new WebViewModel() { TitleName = "全景" };
            //_full3dView.WebView.ResizeMode = System.Windows.ResizeMode.CanResize;
            //_full3dView.WebView.WindowStyle = System.Windows.WindowStyle.None;
        }

        public override void Execute(object parameter)
        {
            if (_unitViewModel == null)
                _unitViewModel = new UnitDetailViewModel();
            _actaulLyrs = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            //建筑不参与拾取
            _actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑" || p.Fc.Alias == "地面") p.FLyers[0].MouseSelectMask = gviViewportMask.gviViewNone; });

            // _actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑") p.FLyers[0].MouseSelectMask = gviViewportMask.gviViewNone; });
            var layers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
            layers.ForEach(p => { if (p.AliasName == "三小场所") _threeSmallLayer = p; });
            bool flag = StringExtension.ParseTo<bool>(parameter, false);
            this.layers = ServiceManager.GetService<IDataBaseService>(null).GetAllLayerItemModels();
            bool flag2 = flag;
            if (flag2)
            {
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MapControl.MouseSelectObjectMask = (gviMouseSelectObjectMask)257;
                GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            }
            else
            {
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
                RestoreEnv();
            }
        }

        protected virtual bool RenderControl_RcMouseHover(uint Flags, int X, int Y)
        {
            bool result = false;
            try
            {
                string guid = string.Empty;
                string oid = string.Empty;

                if (!IEnumerableExtension.HasValues<LayerItemModel>(this.layers))
                    result = false;
                IPoint point;
                IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(X, Y, out point);
                if (pickResult == null)
                    return false;

                //if (_buildViewModle != null)
                //    _buildViewModle.OnUnchecked();
                RestoreEnv();
                gviObjectType type = pickResult.Type;
                IDisplayLayer disPlayLyr = null;
                if (type != gviObjectType.gviObjectFeatureLayer)
                {
                    if (type == gviObjectType.gviObjectRenderPOI)
                    {
                        IRenderPOIPickResult renderPOIPickResult = (IRenderPOIPickResult)pickResult;
                        if (renderPOIPickResult.RenderPOI != null)
                            oid = (guid = renderPOIPickResult.RenderPOI.Guid.ToString());
                    }
                }
                else
                {
                    IFeatureLayerPickResult featureLayerPickResult = (IFeatureLayerPickResult)pickResult;
                    guid = featureLayerPickResult.FeatureLayer.FeatureClassId.ToString();
                    oid = featureLayerPickResult.FeatureId.ToString();
                    disPlayLyr = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCGuid(guid);
                    //if (disPlayLyr == null)
                    //    _actaulLyrs.ForEach(p => { if (p.Fc.Guid.ToString() == guid) disPlayLyr = p; });
                }
                if (string.IsNullOrEmpty(guid))
                    result = false;
                IShowLayer layer = null;
                this._oldSelectFc = new KeyValuePair<string, IDisplayLayer>(oid, disPlayLyr);
                if (disPlayLyr != null)
                {
                    if (IsLayerName(disPlayLyr, "三小场所"))
                    {
                        ShowUnitView(oid, disPlayLyr);
                    }
                    else if (IsLayerName(disPlayLyr, "视频监控"))
                    {
                        OpenVideoView(oid);
                    }
                    else if (IsLayerName(disPlayLyr, "三小场所内墙"))
                    {
                        if (_process != null && !_process.HasExited)
                            _process.Close();
                        var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                        string url = json1.full3dUrl;
                        //调用系统默认的浏览器  
                        _process = System.Diagnostics.Process.Start("iexplore.exe", " -k " + url);

                        //OpenFull3dView();
                        // System.Windows.MessageBox.Show("暂时没有全景图");
                    }
                    else if (IsLayerName(disPlayLyr, "三小场所招牌"))
                    {
                        int indexName = GetPosByName(disPlayLyr, "NAME");
                        // var geo
                        IRowBuffer row = disPlayLyr.Fc.GetRow(oid.ParseTo<int>());

                        //高亮

                        //显示详情
                        object obj = row.GetValue(indexName) ?? string.Empty;
                        string unitId = obj.ToString().Replace("_", "");
                        ShowUnitDetail(unitId, _threeSmallLayer);
                        row.ReleaseComObject();
                    }
                    else
                    {
                        // if (this._oldSelectFc.)
                        disPlayLyr.HighLightFeature(oid, GviMap.MapControl.FeatureManager, 4294901760u);
                        //LayerItemModel layerItemModel = this.layers.Find(delegate (LayerItemModel flyItemModel)
                        //{
                        //    bool flag8 = flyItemModel.Parameters == null;
                        //    return !flag8 && (layer = (flyItemModel.Parameters as IShowLayer)).ContainObject(guid);
                        //});
                        //if (layerItemModel == null || layer == null)
                        //    return false;
                        //DataTable infoTable = layer.GetInfoTable(oid);
                        //if (infoTable == null)
                        //    result = false;
                        //if (ServiceManager.GetService<IShellService>(null).PopView.Children.Count == 0)
                        //    ServiceManager.GetService<IShellService>(null).PopView.Children.Add(ServiceManager.GetService<IShowCaptureObjectService>(null).View);
                        //ServiceManager.GetService<IShowCaptureObjectService>(null).DataContext = new PopViewDataContext
                        //{
                        //    Left = (double)X,
                        //    Top = (double)Y,
                        //    DataView = infoTable.DefaultView,
                        //    IsOpen = true,
                        //    FeatureId = oid
                        //};
                    }
                }
                else
                {
                    LayerItemModel layerItemModel = this.layers.Find(delegate (LayerItemModel flyItemModel)
                    {
                        bool flag8 = flyItemModel.Parameters == null;
                        return !flag8 && (layer = (flyItemModel.Parameters as IShowLayer)).ContainObject(guid);
                    });
                    if (layerItemModel == null || layer == null)
                        return false;
                    DataTable infoTable = layer.GetInfoTable(oid);
                    if (infoTable == null)
                        result = false;
                    if (ServiceManager.GetService<IShellService>(null).PopView.Children.Count == 0)
                        ServiceManager.GetService<IShellService>(null).PopView.Children.Add(ServiceManager.GetService<IShowCaptureObjectService>(null).View);
                    ServiceManager.GetService<IShowCaptureObjectService>(null).DataContext = new PopViewDataContext
                    {
                        Left = (double)X,
                        Top = (double)Y,
                        DataView = infoTable.DefaultView,
                        IsOpen = true,
                        FeatureId = oid
                    };
                }
                result = false;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return false;
            }

            return result;
        }

        private int GetPosByName(IDisplayLayer disPlayLyr, string fieldName)
        {
            IFieldInfoCollection fields = disPlayLyr.Fc.GetFields();
            int position = fields.IndexOf(fieldName);
            fields.ReleaseComObject();
            return position;
        }

        private IRowBufferCollection GetUintRow(string unitIdValue, IDisplayLayer threeSamllLayer)
        {
            return threeSamllLayer.Fc.GetRowBufferCollection(string.Format("{0}='{1}'", "UNIT_ID", unitIdValue));
        }

        private object GetValueByName(IDisplayLayer disPlayLyr, string fieldName, string oid)
        {
            int position = GetPosByName(disPlayLyr, fieldName);
            IRowBuffer row = disPlayLyr.Fc.GetRow(oid.ParseTo<int>());
            object obj = row.GetValue(position) ?? string.Empty;
            return obj;
        }

        private void HideVideo()
        {
            this._videoMonitorView.VideoPath = null;
            this._videoMonitorView.Hide();
        }

        private bool IsLayerName(IDisplayLayer disPlayLyr, string name)
        {
            return disPlayLyr.AliasName == name || disPlayLyr.Fc.Alias == name || disPlayLyr.Fc.Name == name;
        }

        private void OpenFull3dView()
        {
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json1.full3dUrl;
            //_full3DView.Navigate(url);
            //_full3DView.Show();
            if (_full3dView == null)
            {
                _full3dView = new Full3dView();
                // ServiceManager.GetService<IShellService>().ShellWindow.AllowsTransparency = false;

                _full3dView.Closed += (sender, e) =>
                {
                    //  ServiceManager.GetService<IShellService>().ShellWindow.AllowsTransparency = true;
                    _full3dView = null;
                };
            }
            //_full3dView.WebView.WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            //_full3dView.WebView.ResizeMode = System.Windows.ResizeMode.CanResize;
            _full3dView.Navigate(url);
            _full3dView.Show();
        }

        private void OpenVideoView(string oid)
        {
            // bool showLocalVideo = this._showLocalVideo;
            bool showLocalVideo = true;
            if (showLocalVideo)
            {
                string videoPath = ServiceManager.GetService<ICameraInfoService>(null).GetVideoPath(oid);
                VideoMonitorView videoMonitorView = _videoMonitorView;
                if (oid == "1")
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_大门.avi";
                else if (oid == "2")
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_娱乐区.avi";
                else if (oid == "3")
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_停车场.avi";
                else
                    videoMonitorView.VideoPath = videoPath;
                videoMonitorView.Show();
            }
        }

        private void RestoreEnv()
        {
            UnHighLightBuilding();
            HideVideo();
        }

        private void ShowUnitDetail(string unitId, IDisplayLayer disPlayLyr)
        {
            var rows = GetUintRow(unitId, _threeSmallLayer);
            if (rows == null || rows.Count <= 0)
                return;
            IRowBuffer row = rows.Get(0);
            int indexUId = GetPosByName(disPlayLyr, "UNIT_ID");
            int indexName = GetPosByName(disPlayLyr, "UNIT_NAME");
            int indexImg = GetPosByName(disPlayLyr, "UNIT_IMAGE");
            int indexAdree = GetPosByName(disPlayLyr, "UNIT_ADDRE");
            //object obj = row.GetValue(indexUId) ?? string.Empty;
            //string unitId = obj.ToString();

            string imgName = row.GetValue<string>(indexImg);
            string unitName = row.GetValue<string>(indexName);
            string unitAdrr = row.GetValue<string>(indexAdree);
            imgName = string.IsNullOrEmpty(imgName) ? "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png" : imgName;

            if (_unitViewModel == null)
            {
                _unitViewModel = new UnitDetailViewModel();
                _unitViewModel.Initialize();
            }
            _unitViewModel.UnitId = unitId;
            string buildCode = "4294901760";
            _unitViewModel.BuildContent = new BuildContentViewModel
            {
                BuildCode = buildCode,
                BuildName = unitName,
                BuildAdrress = unitAdrr,
                ImgName = imgName,
                BuildAdrressTitle = "单位地址",
                BuildNameTitle = "单位名称",
                BuildCodeTitle = "单位编号",
            };
            _unitViewModel.UnitName = unitName;
            _unitViewModel.OwnerName = "张三";
            _unitViewModel.OwnerPhone = "1378970545";
            _unitViewModel.OnChecked();
            row.ReleaseComObject();
        }

        private void ShowUnitView(string oid, IDisplayLayer disPlayLyr)
        {
            int indexUnitId = GetPosByName(disPlayLyr, "UNIT_ID");
            IRowBuffer row = disPlayLyr.Fc.GetRow(oid.ParseTo<int>());
            object obj = row.GetValue(indexUnitId) ?? string.Empty;
            string unitId = obj.ToString();
            disPlayLyr.HighLightFeature(oid, GviMap.MapControl.FeatureManager, 4294901760u);
            ShowUnitDetail(unitId, _threeSmallLayer);
            row.ReleaseComObject();
        }
        private void UnHighLightBuilding()
        {
            if (_oldSelectFc.Key == null || _oldSelectFc.Value == null)
                return;
            bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
            if (flag)
            {
                this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, Mmc.Framework.Services.GviMap.MapControl.FeatureManager);
            }
        }
    }
}