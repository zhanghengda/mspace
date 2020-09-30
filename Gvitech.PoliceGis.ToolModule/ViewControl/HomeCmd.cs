using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class HomeCmd : SimpleCommandEx
    {
        public override void Execute(object parameter)
        {
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ToolMenu.Visibility = Visibility.Collapsed;
            bool flag = ServiceManager.GetService<IShellService>(null).BottomToolMenu != null;
            if (flag)
            {
                FrameworkElement bottomToolMenu = ServiceManager.GetService<IShellService>(null).BottomToolMenu;
                ItemsControl itemsControl = (ItemsControl)bottomToolMenu.FindName("tools");
                ObservableCollection<ToolItemModel> observableCollection = (ObservableCollection<ToolItemModel>)itemsControl.ItemsSource;
                foreach (ToolItemModel toolItemModel in observableCollection)
                {
                    toolItemModel.Reset();
                    bool flag2 = !string.IsNullOrEmpty(toolItemModel.GroupName);
                    if (flag2)
                    {
                        toolItemModel.Visible = false;
                    }
                }
            }
            bool flag3 = ServiceManager.GetService<IShellService>(null).ShellMenu != null;
            if (flag3)
            {
                FrameworkElement shellMenu = ServiceManager.GetService<IShellService>(null).ShellMenu;
                ItemsControl itemsControl2 = (ItemsControl)shellMenu.FindName("menu");
                if (itemsControl2 != null && itemsControl2.ItemsSource != null)
                {
                    ObservableCollection<ToolItemModel> observableCollection2 = (ObservableCollection<ToolItemModel>)itemsControl2.ItemsSource;
                    foreach (ToolItemModel toolItemModel2 in observableCollection2)
                        toolItemModel2.Reset();
                }
            }
            base.Execute(parameter);
        }
    }
}