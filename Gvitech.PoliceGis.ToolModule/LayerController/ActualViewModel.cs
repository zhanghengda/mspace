using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class ActualViewModel : LayerControllerBase
    {
        public override void Initialize()
        {
            try
            {
                base.Initialize();
                List<IDisplayLayer> showLayers = ServiceManager.GetService<IDataBaseService>(null).GetShowLayers();
                bool flag = IEnumerableExtension.HasValues<IDisplayLayer>(showLayers);
                if (flag)
                {
                    showLayers.ForEach(delegate (IDisplayLayer item)
                    {
                        IFeatureLayer featureLayer = item.FLyers.FirstOrDefault<IFeatureLayer>();
                        base.Items.Add(new ActualLayerItem
                        {
                            Name = (string.IsNullOrEmpty(item.AliasName) ? item.Name : item.AliasName),
                            Parameters = item.FLyers,
                            IsVisible = (featureLayer != null && gviViewportMaskExtension.GetIsVisible(featureLayer.VisibleMask))
                        });
                    });
                }
                base.Items.Add(new TerrainLayerItem
                {
                    Name = "地形",
                    IsVisible = gviViewportMaskExtension.GetIsVisible(GviMap.MapControl.Terrain.VisibleMask)
                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}