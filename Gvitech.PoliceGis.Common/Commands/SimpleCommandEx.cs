using System;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Common.Commands
{
	// Token: 0x0200000F RID: 15
	public class SimpleCommandEx : SimpleCommand
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000084 RID: 132 RVA: 0x00002A64 File Offset: 0x00000C64
		// (remove) Token: 0x06000085 RID: 133 RVA: 0x00002A9C File Offset: 0x00000C9C
		public event EventHandler CommandCompleted;

		// Token: 0x06000086 RID: 134 RVA: 0x00002AD1 File Offset: 0x00000CD1
		public override void Execute(object parameter)
		{
			EventHandlerExtension.RaiseEvent(this.CommandCompleted, parameter, new EventArgs());
		}
	}
}
