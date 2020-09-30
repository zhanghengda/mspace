using FireControlModule.UnitInfo;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;

namespace FireControlModule
{
    public class BuildSearchCmd : SimpleCommand
    {
        private UnitDetailViewModel _unitViewModel;
        private BuildDetailViewModel _buildViewModle;
        private List<IDisplayLayer> _actaulLyrs;

        public override void Execute(object parameter)
        {
            if (_unitViewModel == null)
                _unitViewModel = new UnitDetailViewModel();
            _actaulLyrs = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
            //建筑参与拾取
            _actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑") p.FLyers[0].MouseSelectMask = gviViewportMask.gviViewAllNormalView; });
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
            }
        }

        protected virtual bool RenderControl_RcMouseHover(uint Flags, int X, int Y)
        {
            string guid = string.Empty;
            string oid = string.Empty;
            bool result = false;
            try
            {
                if (!IEnumerableExtension.HasValues<LayerItemModel>(this.layers))
                    return false;
                IPoint point;
                IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(X, Y, out point);
                if (pickResult == null)
                    return false;

                if (_buildViewModle != null)
                    _buildViewModle.OnUnchecked();
                UnHighLightBuilding();

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
                    if (disPlayLyr == null)
                        _actaulLyrs.ForEach(p => { if (p.Fc.Guid.ToString() == guid) disPlayLyr = p; });
                }
                if (string.IsNullOrEmpty(guid))
                    return false;
                //IShowLayer layer = null;
                //LayerItemModel layerItemModel = this.layers.Find(delegate (LayerItemModel flyItemModel)
                //{
                //    bool flag8 = flyItemModel.Parameters == null;
                //    return !flag8 && (layer = (flyItemModel.Parameters as IShowLayer)).ContainObject(guid);
                //});
                //if (layerItemModel == null || layer == null)
                //    return false;

                this._oldSelectFc = new KeyValuePair<string, IDisplayLayer>(oid, disPlayLyr);
                if (disPlayLyr != null)
                {
                    if (disPlayLyr.AliasName == "建筑" || disPlayLyr.Fc.Alias == "建筑" || disPlayLyr.Fc.Name == "建筑")
                    {
                        disPlayLyr.HighLightFeature(oid, GviMap.MapControl.FeatureManager, 4294901760u);
                        object obj = GetValueByName(disPlayLyr, "Name", oid);
                        string buildCode = obj.ToString();
                        if (_buildViewModle == null)
                        {
                            _buildViewModle = new BuildDetailViewModel();
                            _buildViewModle.Initialize();
                        }
                        _buildViewModle.BuildOid = oid;
                        _buildViewModle.BuildCode = buildCode;
                        _buildViewModle.OnChecked();
                    }
                    //else if (disPlayLyr.AliasName == "视频监控" || disPlayLyr.fc.Alias == "视频监控" || disPlayLyr.Fc.Name == "视频监控")
                    //{
                    //}
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

            result = false;
            return result;
        }

        private int GetPosByName(IDisplayLayer disPlayLyr, string fieldName)
        {
            IFieldInfoCollection fields = disPlayLyr.Fc.GetFields();
            int position = fields.IndexOf(fieldName);
            fields.ReleaseComObject();
            return position;
        }

        private object GetValueByName(IDisplayLayer disPlayLyr, string fieldName, string oid)
        {
            int position = GetPosByName(disPlayLyr, fieldName);
            IRowBuffer row = disPlayLyr.Fc.GetRow(oid.ParseTo<int>());
            object obj = row.GetValue(position) ?? string.Empty;
            return obj;
        }

        private List<LayerItemModel> layers = new List<LayerItemModel>();

        private void UnHighLightBuilding()
        {
            bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
            if (flag)
            {
                this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, Mmc.Framework.Services.GviMap.MapControl.FeatureManager);
            }
        }

        private KeyValuePair<string, IDisplayLayer> _oldSelectFc;
    }
}