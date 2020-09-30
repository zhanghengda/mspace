using Mmc.Framework.Wpf.Commands;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class HMeasurationViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new HorizontalDistanceCmd();
            base.ViewType = (ViewType)1;
        }
    }
}