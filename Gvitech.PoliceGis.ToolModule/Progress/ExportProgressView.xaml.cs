using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Mmc.Mspace.ToolModule
{
	// Token: 0x0200000E RID: 14
	public partial class ExportProgressView : UserControl
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00004FF2 File Offset: 0x000031F2
		public ExportProgressView()
		{
			this.InitializeComponent();
			base.Loaded += this.ExportProgressView_Loaded;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00005024 File Offset: 0x00003224
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000503C File Offset: 0x0000323C
		public ExportProgressViewModel ViewModel
		{
			get
			{
				return this.viewModel;
			}
			set
			{
				this.viewModel = value;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005046 File Offset: 0x00003246
		private void ExportProgressView_Loaded(object sender, RoutedEventArgs e)
		{
			base.DataContext = this.ViewModel;
		}

		// Token: 0x04000036 RID: 54
		private ExportProgressViewModel viewModel = new ExportProgressViewModel();
	}
}
