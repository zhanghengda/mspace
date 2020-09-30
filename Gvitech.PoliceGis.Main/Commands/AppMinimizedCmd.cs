using System;
using System.Windows;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Main.Commands
{
	// Token: 0x02000009 RID: 9
	public class AppMinimizedCmd : SimpleCommand
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000246D File Offset: 0x0000066D
		public override void Execute(object parameter)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}
	}
}
