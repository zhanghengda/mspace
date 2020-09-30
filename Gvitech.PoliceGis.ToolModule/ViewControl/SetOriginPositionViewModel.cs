using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class SetOriginPositionViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            base.Command = new SetOriginPositionCmd();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            // UpdateWindowSize(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            // UpdateWindowSize(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
        }

        private static void SetOriginPosition()
        {
           
        }
    }
}
