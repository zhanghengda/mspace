using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using Mmc.Mspace.Models.Video;
using Mmc.Mspace.Services.HttpService;

namespace Mmc.Mspace.PoliceResourceModule.VideoMonitor
{
	// Token: 0x02000005 RID: 5
	[ComVisible(true)]
	public partial class VideoMonitorWebView : Window
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002751 File Offset: 0x00000951
		public VideoMonitorWebView()
		{
			this.InitializeComponent();
			this.AddBrowser();
			base.Loaded += this.VideoMonitorWebView_Loaded;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002784 File Offset: 0x00000984
		public void OpenVideo(VideoParam vp)
		{
			bool flag = vp == null;
			if (!flag)
			{
				this._vp = vp;
				string uriString = "http://10.197.1.219/test2.html";
				WebBrowser webBrowser = this.container.Child as WebBrowser;
				webBrowser.Url = new Uri(uriString);
				webBrowser.Show();
				webBrowser.ObjectForScripting = this;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027D8 File Offset: 0x000009D8
		public void PlayVideo(VideoParam vp)
		{
			bool flag = vp == null;
			if (!flag)
			{
				string url = string.Format("{0}/monitoring/PGISVideoManage.do?operation=playRealtimeVideo&deviceID={4}&sysname={1}&citizenid={2}&password={3}", new object[]
				{
					VideoMonitorWebView.PROJECT_URL,
					vp.Sysname,
					vp.Citizenid,
					vp.Password,
					vp.DeviceID
				});
				HttpService httpService = new HttpService();
				httpService.AsyncHttpPost(url, "", delegate(string result)
				{
					Console.WriteLine(result);
				});
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002860 File Offset: 0x00000A60
		private void AddBrowser()
		{
			WebBrowser webBrowser = new WebBrowser();
			webBrowser.AllowNavigation = true;
			webBrowser.ScriptErrorsSuppressed = true;
			webBrowser.AllowWebBrowserDrop = true;
			webBrowser.IsWebBrowserContextMenuEnabled = true;
			webBrowser.DocumentCompleted += this.Wb_DocumentCompleted;
			webBrowser.HandleCreated += this.Wb_HandleCreated;
			webBrowser.HandleDestroyed += this.Wb_HandleDestroyed;
			this.container.Child = webBrowser;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028DC File Offset: 0x00000ADC
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002614 File Offset: 0x00000814
		private void Button_CloseCmd(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000290C File Offset: 0x00000B0C
		private void Wb_HandleDestroyed(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000290C File Offset: 0x00000B0C
		private void Wb_HandleCreated(object sender, EventArgs e)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002910 File Offset: 0x00000B10
		private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			bool flag = this._vp == null;
			if (!flag)
			{
				WebBrowser webBrowser = this.container.Child as WebBrowser;
				webBrowser.Focus();
				webBrowser.Document.InvokeScript("showPlayerWin", new object[]
				{
					this._vp.Sysname,
					this._vp.Password,
					this._vp.Citizenid,
					this._vp.DeviceID
				});
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000290C File Offset: 0x00000B0C
		private void VideoMonitorWebView_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0400000A RID: 10
		private static string PROJECT_URL = "http://10.197.1.216:7001/PMPlatForm";

		// Token: 0x0400000B RID: 11
		private VideoParam _vp = null;
	}
}
