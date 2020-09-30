using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Framework.Wpf.Commands
{
    public class AnimatedNavigationCmd : MapCommand
    {
        private ICameraTour tour = null;
        public override void Execute(object parameter)
        {
           // this._viewHeigth = 3.7;
            this.RegisterEvent();
        }


        private void RegisterEvent()
        {
            this.AxMapControl.InteractMode = gviInteractMode.gviInteractSelect;
            this.AxMapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            this.AxMapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;

        }

    }
}
