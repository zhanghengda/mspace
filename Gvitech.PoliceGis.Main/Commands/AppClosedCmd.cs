using System;
using System.Windows;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Main.Commands
{
	// Token: 0x02000008 RID: 8
	public class AppClosedCmd : SimpleCommand
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002455 File Offset: 0x00000655
		public override void Execute(object parameter)
		{
			Application.Current.Shutdown(0);
		}
	}
}
