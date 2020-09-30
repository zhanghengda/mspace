using Mmc.Framework.Wpf.Commands;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class PerspectiveViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new PerspectiveCmd();
        }
    }
}