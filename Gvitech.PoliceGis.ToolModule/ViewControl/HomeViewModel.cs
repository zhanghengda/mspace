using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class HomeViewModel : ToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            ICommand command = new HomeCmd();
            base.Command = command;
            ServiceManager.GetService<IShellService>(null).HomeCmd = command;
        }
    }
}