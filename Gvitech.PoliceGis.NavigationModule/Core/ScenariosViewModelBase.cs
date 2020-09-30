using System;
using System.Windows.Controls;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.NavigationModule.Core
{
    // Token: 0x0200000A RID: 10
    public class ScenariosViewModelBase : CheckedToolItemModel
    {
        // Token: 0x0600002D RID: 45 RVA: 0x00002758 File Offset: 0x00000958
        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        // Token: 0x0600002E RID: 46 RVA: 0x0000276A File Offset: 0x0000096A
        public override void OnChecked()
        {
            base.OnChecked();
            //ServiceManager.GetService<IShellService>(null).BottomView.Content = base.BottomView;
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility= System.Windows.Visibility.Collapsed;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
        }

        // Token: 0x0600002F RID: 47 RVA: 0x0000278C File Offset: 0x0000098C
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            //ContentControl bottomView = ServiceManager.GetService<IShellService>(null).BottomView;
            //if (bottomView.Content != null)
            //    bottomView.ClearValue(ContentControl.ContentProperty);
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;

        }
    }
}
