using System;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Models.Video;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoliceResourceModule.VideoMonitor
{
	// Token: 0x02000002 RID: 2
	public class VideoMonitorCmd : SimpleCommand
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override void Execute(object parameter)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		protected bool RenderControl_RcMouseHover(uint flags, int x, int y)
		{
			IPoint point;
			IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(x, y, out point);
			bool flag = pickResult == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = pickResult.Type != gviObjectType.gviObjectFeatureLayer;
				if (flag2)
				{
					result = false;
				}
				else
				{
					IFeatureLayerPickResult featureLayerPickResult = (IFeatureLayerPickResult)pickResult;
					string text = featureLayerPickResult.FeatureLayer.FeatureClassId.ToString();
					string text2 = featureLayerPickResult.FeatureId.ToString();
					bool flag3 = string.IsNullOrEmpty(text);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = text != this._displayLyr.Fc.Guid.ToString();
						if (flag4)
						{
							result = false;
						}
						else
						{
							string videoPath = ServiceManager.GetService<ICameraInfoService>(null).GetVideoPath(text2);
							string deviceId = ServiceManager.GetService<ICameraInfoService>(null).GetDeviceId(text, StringExtension.ParseTo<int>(text2, 0));
							VideoInfo videoInfo = ServiceManager.GetService<IVideoHttpService>(null).GetVideoInfo(deviceId);
							VideoParam vp = new VideoParam
							{
								DeviceID = videoInfo.BH,
								Citizenid = videoInfo.BMBH,
								Sysname = "ycpgis",
								Password = "ycpgis"
							};
							VideoMonitorWebView videoMonitorWebView = new VideoMonitorWebView
							{
								Width = 600.0,
								Height = 800.0
							};
							videoMonitorWebView.ShowDialog();
							videoMonitorWebView.OpenVideo(vp);
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private IDisplayLayer _displayLyr;
	}
}
