using System;
using System.Windows;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Common.Commands
{
	// Token: 0x0200000E RID: 14
	public class BarCmd : SimpleCommand
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000080 RID: 128 RVA: 0x000029B4 File Offset: 0x00000BB4
		// (remove) Token: 0x06000081 RID: 129 RVA: 0x000029EC File Offset: 0x00000BEC
		public event EventHandler CommandCompleted;

		// Token: 0x06000082 RID: 130 RVA: 0x00002A21 File Offset: 0x00000C21
		public override void Execute(object parameter)
		{
            //屏蔽收缩菜单项
			//ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = Visibility.Collapsed;
			//ServiceManager.GetService<IShellService>(null).ToolMenu.Visibility = Visibility.Visible;
			EventHandlerExtension.RaiseEvent(this.CommandCompleted, this, new EventArgs());
		}
	}
}
