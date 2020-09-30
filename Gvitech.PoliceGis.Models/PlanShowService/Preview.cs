using System;
using System.Drawing;
using System.Xml.Serialization;
using Gvitech.AppPd.Common;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Gvitech.CityMaker.Controls;

namespace Mmc.Mspace.Models.PlanShowService
{
	// Token: 0x02000006 RID: 6
	public class Preview
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000232A File Offset: 0x0000052A
		public Preview()
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002334 File Offset: 0x00000534
		public Preview(IPresentation presenation) : this()
		{
			this._presentation = presenation;
			GviMap.AxMapControl.RcBeforePresentationItemActivation += new _IRenderControlEvents_RcBeforePresentationItemActivationEventHandler(RenderControl_RcBeforePresentationItemActivation);
			GviMap.AxMapControl.RcPresentationStatusChanged += new _IRenderControlEvents_RcPresentationStatusChangedEventHandler(RenderControl_RcPresentationStatusChanged);
            GviMap.AxMapControl.RcBeforePresentationItemActivation += new _IRenderControlEvents_RcBeforePresentationItemActivationEventHandler(RenderControl_RcBeforePresentationItemActivation);
            GviMap.AxMapControl.RcPresentationStatusChanged += new _IRenderControlEvents_RcPresentationStatusChangedEventHandler(RenderControl_RcPresentationStatusChanged);

        }

        // Token: 0x1700001F RID: 31
        // (get) Token: 0x06000044 RID: 68 RVA: 0x00002374 File Offset: 0x00000574
        public Image Image
		{
			get
			{
				return ImageTool.getImage(this._presentation.SlideImageName);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002398 File Offset: 0x00000598
		public Guid RId
		{
			get
			{
				return this._presentation.Guid;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000023B8 File Offset: 0x000005B8
		public string Name
		{
			get
			{
				return this._presentation.Name;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000023D8 File Offset: 0x000005D8
		private void RenderControl_RcPresentationStatusChanged(string PresentationID, gviPresentationStatus Status)
		{
			bool flag = Status == gviPresentationStatus.gviPresentationNotPlaying && this._presentation.Guid.ToString() == PresentationID;
			if (flag)
			{
				bool flag2 = this.AfterStopEevent != null;
				if (flag2)
				{
					this.AfterStopEevent();
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002430 File Offset: 0x00000630
		private void RenderControl_RcBeforePresentationItemActivation(string PresentationID, IPresentationStep Step)
		{
			bool flag = this._presentation.Guid.ToString() == PresentationID;
			if (flag)
			{
				bool flag2 = this.BeforePlayEevent != null;
				if (flag2)
				{
					this.BeforePlayEevent();
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000247E File Offset: 0x0000067E
		public void Play()
		{
			this._presentation.Play(0);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000248E File Offset: 0x0000068E
		public void Stop()
		{
			this._presentation.Stop();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000249D File Offset: 0x0000069D
		public void Pause()
		{
			this._presentation.Pause();
		}

		// Token: 0x0400001F RID: 31
		private readonly IPresentation _presentation;

		// Token: 0x04000020 RID: 32
		[XmlIgnore]
		public Action AfterStopEevent;

		// Token: 0x04000021 RID: 33
		[XmlIgnore]
		public Action BeforePlayEevent;
	}
}
