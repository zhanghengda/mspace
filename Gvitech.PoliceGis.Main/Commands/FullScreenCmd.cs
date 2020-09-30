using System;
using System.Configuration;
using System.Windows;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.Main.Commands
{
	// Token: 0x0200000A RID: 10
	public class FullScreenCmd : ParameterCommand
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002481 File Offset: 0x00000681
		public FullScreenCmd()
		{
			this.isFullScreen = StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["IsFullScreen"], false);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000024C0 File Offset: 0x000006C0
		public bool IsFullScreen
		{
			get
			{
				return this.isFullScreen;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<bool>(ref this.isFullScreen, value, "IsFullScreen");
				bool flag = this.isFullScreen;
				if (flag)
				{
					FullScreenCmd.UpdateWindowSize(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);
				}
				else
				{
					FullScreenCmd.UpdateWindowSize(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000251F File Offset: 0x0000071F
		public override void Execute(object parameter)
		{
			this.IsFullScreen = !this.IsFullScreen;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002534 File Offset: 0x00000734
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

		// Token: 0x0400000D RID: 13
		private bool isFullScreen;
	}
}
