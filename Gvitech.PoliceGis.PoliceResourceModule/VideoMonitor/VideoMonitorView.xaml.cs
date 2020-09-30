using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Mmc.Mspace.PoliceResourceModule.VideoMonitor
{
	// Token: 0x02000004 RID: 4
	public partial class VideoMonitorView : Window
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002488 File Offset: 0x00000688
		public string VideoPath
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
				Uri source = new Uri(this._path);
				this.mediaCtrl.Source = source;
				this.mediaCtrl.Play();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024C4 File Offset: 0x000006C4
		public VideoMonitorView(string videoPath) : this()
		{
			this._path = videoPath;
			Uri source = new Uri(videoPath);
			this.mediaCtrl.Source = source;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024F4 File Offset: 0x000006F4
		public VideoMonitorView()
		{
			this.InitializeComponent();
			this.mediaCtrl.MediaEnded += delegate(object s, RoutedEventArgs e)
			{
				this.mediaCtrl.Position = default(TimeSpan);
				this.mediaCtrl.Play();
			};
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002520 File Offset: 0x00000720
		private void btnPlay_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.mediaCtrl.Source == null;
			if (flag)
			{
				this.mediaCtrl.Source = new Uri(this._path);
			}
			this.mediaCtrl.Position = (this._pausest.Equals(this.mediaCtrl.NaturalDuration) ? default(TimeSpan) : this._pausest);
			this.mediaCtrl.Play();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025A8 File Offset: 0x000007A8
		private void btnStop_Click(object sender, RoutedEventArgs e)
		{
			bool canPause = this.mediaCtrl.CanPause;
			if (canPause)
			{
				this.mediaCtrl.Pause();
				this._pausest = this.mediaCtrl.Position;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025E4 File Offset: 0x000007E4
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

		// Token: 0x06000012 RID: 18 RVA: 0x00002614 File Offset: 0x00000814
		private void Button_CloseCmd(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x04000004 RID: 4
		private string _path;

		// Token: 0x04000005 RID: 5
		private TimeSpan _pausest;
	}
}
