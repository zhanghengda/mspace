using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using System.Windows;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class FullScreenViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            base.Command = new FullScreenCmd();
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

        private static void UpdateWindowSize(double width, double height)
        {
            FrameworkElement shellWindow = ServiceManager.GetService<IShellService>(null).ShellWindow;
            ServiceManager.GetService<IShellService>(null).ShellWindow.MaxWidth = width;
            shellWindow.Width = width;
            FrameworkElement shellWindow2 = ServiceManager.GetService<IShellService>(null).ShellWindow;
            ServiceManager.GetService<IShellService>(null).ShellWindow.MaxHeight = height;
            shellWindow2.Height = height;
            ServiceManager.GetService<IShellService>(null).ShellWindow.UpdateLayout();
            FrameworkElement mapWindow = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            ServiceManager.GetService<IMaphostService>(null).MapWindow.MaxWidth = width;
            mapWindow.Width = width;
            FrameworkElement mapWindow2 = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            ServiceManager.GetService<IMaphostService>(null).MapWindow.MaxHeight = height;
            mapWindow2.Height = height;
            ServiceManager.GetService<IMaphostService>(null).MapWindow.UpdateLayout();
        }
    }
}