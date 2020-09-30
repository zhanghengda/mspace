using Mmc.Mspace.Common.Models;

namespace FireControlModule
{
    /// <summary>
    /// 拾取查询
    /// </summary>
    public class PropertySearchExViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new PropertySearchExCmd();
            base.ViewType = (ViewType)1;
        }

        public override void Reset()
        {
            base.Reset();
            ((PropertySearchExCmd)base.Command).Execute(false);
        }
    }
}