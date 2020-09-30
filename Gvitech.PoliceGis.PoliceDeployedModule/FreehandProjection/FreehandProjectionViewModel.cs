using System;
using System.Windows;
using System.Windows.Controls;
using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;

namespace Mmc.Mspace.PoliceDeployedModule.FreehandProjection
{
	// Token: 0x02000019 RID: 25
	public class FreehandProjectionViewModel : BarModel
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00006B20 File Offset: 0x00004D20
		public override void Initialize()
		{
            base.Initialize();
            base.Command = new FreehandProjectionCmd();
			(base.Command as BarCmd).CommandCompleted += delegate(object s, EventArgs e)
			{
				ServiceManager.GetService<IShellService>(null).ContentView.ClearValue(ContentControl.ContentProperty);
				ServiceManager.GetService<IShellService>(null).ContentView.Content = base.View;
			};
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006B4C File Offset: 0x00004D4C
		public override FrameworkElement CreatedView()
		{
			return new FreehandProjectionView();
		}
	}
}
