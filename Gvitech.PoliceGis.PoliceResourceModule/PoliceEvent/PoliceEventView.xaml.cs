using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Mmc.Mspace.PoliceResourceModule.PoliceEvent
{
	// Token: 0x02000006 RID: 6
	public partial class PoliceEventView : Window
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002A03 File Offset: 0x00000C03
		public PoliceEventView()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A14 File Offset: 0x00000C14
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
