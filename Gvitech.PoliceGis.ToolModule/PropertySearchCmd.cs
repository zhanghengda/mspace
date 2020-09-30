using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.ShowCaptureObjectService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Collections.Generic;
using System.Data;

namespace Mmc.Mspace.ToolModule
{
    public class PropertySearchCmd : SimpleCommand
    {
        private List<LayerItemModel> layers = new List<LayerItemModel>();

        public override void Execute(object parameter)
        {
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
            string text = string.Empty;
            bool flag = !IEnumerableExtension.HasValues<LayerItemModel>(this.layers);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                IPoint point;
                IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(X, Y, out point);
                bool flag2 = pickResult == null;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    gviObjectType type = pickResult.Type;
                    if (type != gviObjectType.gviObjectFeatureLayer)
                    {
                        if (type == gviObjectType.gviObjectRenderPOI)
                        {
                            IRenderPOIPickResult renderPOIPickResult = (IRenderPOIPickResult)pickResult;
                            bool flag3 = renderPOIPickResult.RenderPOI != null;
                            if (flag3)
                            {
                                text = (guid = renderPOIPickResult.RenderPOI.Guid.ToString());
                            }
                        }
                    }
                    else
                    {
                        IFeatureLayerPickResult featureLayerPickResult = (IFeatureLayerPickResult)pickResult;
                        guid = featureLayerPickResult.FeatureLayer.FeatureClassId.ToString();
                        text = featureLayerPickResult.FeatureId.ToString();
                    }
                    bool flag4 = string.IsNullOrEmpty(guid);
                    if (flag4)
                    {
                        result = false;
                    }
                    else
                    {
                        IShowLayer layer = null;
                        LayerItemModel layerItemModel = this.layers.Find(delegate (LayerItemModel flyItemModel)
                        {
                            bool flag8 = flyItemModel.Parameters == null;
                            return !flag8 && (layer = (flyItemModel.Parameters as IShowLayer)).ContainObject(guid);
                        });
                        bool flag5 = layerItemModel == null || layer == null;
                        if (flag5)
                        {
                            result = false;
                        }
                        else
                        {
                            DataTable infoTable = layer.GetInfoTable(text);
                            bool flag6 = infoTable == null;
                            if (flag6)
                            {
                                result = false;
                            }
                            else
                            {
                                bool flag7 = ServiceManager.GetService<IShellService>(null).PopView.Children.Count == 0;
                                if (flag7)
                                {
                                    ServiceManager.GetService<IShellService>(null).PopView.Children.Add(ServiceManager.GetService<IShowCaptureObjectService>(null).View);
                                }
                                ServiceManager.GetService<IShowCaptureObjectService>(null).DataContext = new PopViewDataContext
                                {
                                    Left = (double)X,
                                    Top = (double)Y,
                                    DataView = infoTable.DefaultView,
                                    IsOpen = true,
                                    FeatureId = text
                                };
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}