using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.Main.DTSelector
{
    public class ToolDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            bool flag = item != null & item is ToolItemModel;
            DataTemplate result;
            if (flag)
            {
                ToolItemModel toolItemModel = (ToolItemModel)item;
                switch ((int)toolItemModel.ViewType)
                {
                    case 0:
                        dataTemplate = (ServiceManager.GetService<IShellService>(null).ShellWindow.FindResource("IconBtnDataTemplate") as DataTemplate);
                        break;

                    case 1:
                        dataTemplate = (ServiceManager.GetService<IShellService>(null).ShellWindow.FindResource("IconRadioBtnDataTemplate") as DataTemplate);
                        break;

                    case 2:
                        dataTemplate = (ServiceManager.GetService<IShellService>(null).ShellWindow.FindResource("IconComboBoxDataTemplate") as DataTemplate);
                        break;

                    case 5:
                        dataTemplate = (ServiceManager.GetService<IShellService>(null).ShellWindow.FindResource("ViewDataTemplate") as DataTemplate);
                        break;
                }
                result = dataTemplate;
            }
            else
            {
                result = base.SelectTemplate(item, container);
            }
            return result;
        }
    }
}