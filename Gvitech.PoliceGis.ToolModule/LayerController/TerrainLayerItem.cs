using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class TerrainLayerItem : LayerItemModel
    {
        public override void OnVisibleChanged()
        {
            bool flag = !GviMap.MapControl.Terrain.IsRegistered;
            if (!flag)
            {
                ITerrainExtension.SetVisibleMask(GviMap.MapControl.Terrain, base.IsVisible, gviViewportMask.gviViewAllNormalView);
            }
        }
    }
}