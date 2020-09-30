using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class PropertySearchViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new PropertySearchCmd();
            base.ViewType = (ViewType)1;
        }

        public override void Reset()
        {
            base.Reset();
            ((PropertySearchCmd)base.Command).Execute(false);
        }
    }
}