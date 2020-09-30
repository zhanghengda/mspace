using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Common.Models;
using System.Collections.Generic;

namespace Mmc.Mspace.ToolModule.LayerController
{
    public class ActualLayerItem : LayerItemModel
    {
        public override void OnVisibleChanged()
        {
            bool flag = base.Parameters != null && base.Parameters is List<IFeatureLayer>;
            if (flag)
            {
                List<IFeatureLayer> list = (List<IFeatureLayer>)base.Parameters;
                bool flag2 = IEnumerableExtension.HasValues<IFeatureLayer>(list);
                if (flag2)
                {
                    list.ForEach(delegate (IFeatureLayer item)
                    {
                        IFeatureLayerExtension.SetVisibleMask(item, base.IsVisible, gviViewportMask.gviViewAllNormalView);
                    });
                }
            }
        }
    }
}