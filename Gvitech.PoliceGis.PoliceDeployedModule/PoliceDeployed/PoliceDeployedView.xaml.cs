using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Mmc.Mspace.PoliceDeployedModule.PoliceDeployed
{
	// Token: 0x02000010 RID: 16
	public partial class PoliceDeployedView : Window
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000050CF File Offset: 0x000032CF
		public PoliceDeployedView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000050E0 File Offset: 0x000032E0
		private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			try
			{
				base.DragMove();
			}
			catch (Exception)
			{
			}
		}
	}
}
