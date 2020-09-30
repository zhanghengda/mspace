using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class FullScreenCmd : SimpleCommand
    {
        public override void Execute(object parameter)
        {
            bool position = parameter.ParseTo<bool>();
            if (position)
                UpdateWindowSize(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
            else
                UpdateWindowSize(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
        }

        private static void UpdateWindowSize(double width, double height)
        {
            var shellWindow = ServiceManager.GetService<IShellService>(null).ShellWindow;
            var mapWindow = ServiceManager.GetService<IMaphostService>(null).MapWindow;
            //var leftView = ServiceManager.GetService<IShellService>(null).LeftWindow;
            shellWindow.MaxWidth = width;
            shellWindow.Width = width;
            shellWindow.MaxHeight = height;
            shellWindow.Height = height;
            shellWindow.UpdateLayout();

            if (GviMap.Viewport.ViewportMode != gviViewportMode.gviViewportSinglePerspective)
            {
                //mapWindow.MaxWidth = width- leftView.Width;
                //mapWindow.Width = width- leftView.Width;
            }
            else
            {
                mapWindow.MaxWidth = width;
                mapWindow.Width = width;
            }

            mapWindow.MaxHeight = height;
            mapWindow.Height = height;
            mapWindow.UpdateLayout();
            //if (leftView.Height != 40)
            //    leftView.Height = height - 74;
            //leftView.UpdateLayout();
        }
    }
}