using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.PlanShowService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PlanShowModule.PlanShow
{
	// Token: 0x02000006 RID: 6
	public class PreviewWrapper : BindableBase
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002314 File Offset: 0x00000514
		public PreviewWrapper()
		{
			this.PlayCmd = new RelayCommand(() =>
            {
				bool flag = this.Preview != null;
				if (flag)
				{
					this.Preview.Play();
				}
			});
			this.PauseCmd = new RelayCommand(()=>
			{
				bool flag = this.Preview != null;
				if (flag)
				{
					this.Preview.Pause();
				}
			});
			this.StopCmd = new RelayCommand(() =>
            {
				bool flag = this.Preview != null;
				if (flag)
				{
					this.Preview.Stop();
				}
				this.Reset();
				this.isChecked = false;
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002374 File Offset: 0x00000574
		public PreviewWrapper(Preview preview) : this()
		{
			this.Preview = preview;
			Preview preview2 = this.Preview;
			preview2.BeforePlayEevent = (Action)Delegate.Combine(preview2.BeforePlayEevent, new Action(this.BeforePlay));
			preview2 = this.Preview;
			preview2.AfterStopEevent = (Action)Delegate.Combine(preview2.AfterStopEevent, new Action(this.AfterStop));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023E3 File Offset: 0x000005E3
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000023EB File Offset: 0x000005EB
		public Preview Preview { get; set; }

		// Token: 0x06000014 RID: 20 RVA: 0x000023F4 File Offset: 0x000005F4
		private void Reset()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023F7 File Offset: 0x000005F7
		public void BeforePlay()
		{
			//ServiceManager.GetService<IShellService>(null).BottomView.Visibility = Visibility.Collapsed;
			ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = Visibility.Collapsed;
			ServiceManager.GetService<IShellService>(null).ToolMenu.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002430 File Offset: 0x00000630
		public void AfterStop()
		{
			//ServiceManager.GetService<IShellService>(null).BottomView.Visibility = Visibility.Visible;
			ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = Visibility.Visible;
			ServiceManager.GetService<IShellService>(null).ToolMenu.Visibility = Visibility.Visible;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002469 File Offset: 0x00000669
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002471 File Offset: 0x00000671
		[XmlIgnore]
		public ICommand PlayCmd { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000247A File Offset: 0x0000067A
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002482 File Offset: 0x00000682
		[XmlIgnore]
		public ICommand PauseCmd { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000248B File Offset: 0x0000068B
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002493 File Offset: 0x00000693
		[XmlIgnore]
		public ICommand StopCmd { get; set; }

		// Token: 0x04000004 RID: 4
		private bool isChecked;
	}
}
