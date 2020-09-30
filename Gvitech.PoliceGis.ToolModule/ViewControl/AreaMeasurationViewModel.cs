using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class AreaMeasurationViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new ProjectAreaCmd();
            base.ViewType = (ViewType)1;
        }
    }
}