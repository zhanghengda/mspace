using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Toolkit.Controls.InkPad;
using System;
using System.Windows;
using System.Windows.Media;

namespace Mmc.Mspace.ToolModule
{
    public class PaintViewModel : ToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new RelayCommand(() =>
            {
                (ServiceManager.GetService<IShellService>(null).ShellWindow.Content as FrameworkElement).Visibility = Visibility.Collapsed;
                ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Collapsed;
                ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Collapsed;
                InkPadWindow inkPadWindow = new InkPadWindow();
                inkPadWindow.Owner = ServiceManager.GetService<IShellService>(null).ShellWindow;
                inkPadWindow.Width = (inkPadWindow.MaxWidth = inkPadWindow.Owner.MaxWidth);
                inkPadWindow.Height = (inkPadWindow.MaxHeight = inkPadWindow.Owner.MaxHeight);
                inkPadWindow.Background = (Brush)new BrushConverter().ConvertFromString("#0F000000");
                inkPadWindow.WindowStyle = WindowStyle.None;
                inkPadWindow.ResizeMode = ResizeMode.NoResize;
                inkPadWindow.AllowsTransparency = true;
                inkPadWindow.ShowInTaskbar = false;
                inkPadWindow.Show();
                inkPadWindow.InkPadCloseCompleted += delegate (object sender, EventArgs ars)
                {
                    ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
                    (ServiceManager.GetService<IShellService>(null).ShellWindow.Content as FrameworkElement).Visibility = Visibility.Visible;
                    ServiceManager.GetService<IShellService>(null).ShellWindow.WindowState = WindowState.Maximized;
                    ServiceManager.GetService<IShellService>(null).ShellWindow.Activate();
                    ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
                };
            });
        }
    }
}