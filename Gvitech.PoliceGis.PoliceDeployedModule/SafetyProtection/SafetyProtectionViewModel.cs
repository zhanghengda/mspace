using System;
using System.Windows;
using System.Windows.Controls;
using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.PoliceDeployedModule.SafetyProtection
{
	// Token: 0x0200000C RID: 12
	public class SafetyProtectionViewModel : BarModel
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004F60 File Offset: 0x00003160
		public override void Initialize()
		{
            base.Initialize();
            base.Command = new SafetyProtectionCmd();
			(base.Command as BarCmd).CommandCompleted += delegate(object s, EventArgs e)
			{
				ServiceManager.GetService<IShellService>(null).ContentView.ClearValue(ContentControl.ContentProperty);
				ServiceManager.GetService<IShellService>(null).ContentView.Content = base.View;
			};
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F8C File Offset: 0x0000318C
		public override FrameworkElement CreatedView()
		{
			return new SafetyProtectionView();
		}
	}
}
