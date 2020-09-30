using Mmc.Mspace.Common.Models;

namespace FireControlModule.ReturnOrigin
{
    public class ReturnOriginViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new ReturnOriginCmd();
        }
    }
}