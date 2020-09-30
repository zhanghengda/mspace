using Mmc.Framework.Wpf.Commands;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class PersonAngleViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new PersonAngleCmd();
        }
    }
}