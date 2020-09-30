using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
    class PlaneClipVModel : CheckedToolItemModel
    {
   
    public override void Initialize()
    {
        // MessageBox.Show("11");
        //  OnFreshClip();
        base.Initialize();
        base.ViewType = (ViewType)1;
    }
    public override void OnChecked()
    {
        base.OnChecked();
         GviMap.AxMapControl.InteractMode = gviInteractMode.gviInteractClipPlane;
         GviMap.AxMapControl.ClipMode = gviClipMode.gviClipCustomePlane;

        }
    public override void OnUnchecked()
    {
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;// gviSelectFeatureLayer;
            GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            base.OnUnchecked();
      

        }
    }
}
