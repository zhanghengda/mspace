using Mmc.Mspace.Common.Models;

namespace FireControlModule
{
    public class BuildSearchViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new BuildSearchCmd();
            base.ViewType = (ViewType)1;
        }

        public override void Reset()
        {
            base.Reset();
            ((PropertySearchExCmd)base.Command).Execute(false);
        }
    }
}