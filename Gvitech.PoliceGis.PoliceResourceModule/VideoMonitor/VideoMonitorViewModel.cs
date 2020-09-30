using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace Mmc.Mspace.PoliceResourceModule.VideoMonitor
{
    // Token: 0x02000003 RID: 3
    public class VideoMonitorViewModel : CheckedToolItemModel
    {
        // Token: 0x04000002 RID: 2
        private IDisplayLayer _displayLyr; 
        // Token: 0x04000003 RID: 3
        private bool _showLocalVideo;

        // Token: 0x06000005 RID: 5 RVA: 0x00002218 File Offset: 0x00000418
        public override FrameworkElement CreatedView()
        {
            bool showLocalVideo = this._showLocalVideo;
            FrameworkElement result;
            if (showLocalVideo)
            {
                result = new VideoMonitorView
                {
                    Owner = Application.Current.MainWindow
                };
            }
            else
            {
                result = new VideoMonitorWebView
                {
                    Width = 900.0,
                    Height = 750.0,
                    Owner = Application.Current.MainWindow
                };
            }
            return result;
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021E3 File Offset: 0x000003E3
        public override void Initialize()
        {
            base.Initialize();
            base.Command = new VideoMonitorCmd();
            base.ViewType = (ViewType)1;
            this._showLocalVideo = StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["ShowLocalVideo"], true);
        }
        // Token: 0x06000006 RID: 6 RVA: 0x00002288 File Offset: 0x00000488
        public override void OnChecked()
        {
            base.OnChecked();
            //ServiceManager.GetService<IShellService>().HideAllView();
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
            GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            bool flag = this._displayLyr == null;
            if (flag)
            {
                this._displayLyr = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
            }
            Window window = (Window)base.View;
            window.Hide();
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002302 File Offset: 0x00000502
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
        }

        // Token: 0x06000009 RID: 9 RVA: 0x00002453 File Offset: 0x00000653
        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        // Token: 0x06000008 RID: 8 RVA: 0x0000233C File Offset: 0x0000053C
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
                    string cId = featureLayerPickResult.FeatureId.ToString();
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
                            bool showLocalVideo = this._showLocalVideo;
                            if (showLocalVideo)
                            {
                                string videoPath = ServiceManager.GetService<ICameraInfoService>(null).GetVideoPath(cId);
                                VideoMonitorView videoMonitorView = (VideoMonitorView)base.View;
                                if (cId == "1")
                                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_大门.avi";
                                else if (cId == "2")
                                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_娱乐区.avi";
                                else if (cId == "3")
                                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_停车场.avi";
                                else
                                    videoMonitorView.VideoPath = videoPath;

                                //暂时加上
                                videoMonitorView.Width = 300;
                                videoMonitorView.Height = 300;
                                videoMonitorView.Show();
                            }
                            else
                            {
                                Process process = Process.Start("iexplore.exe", "http://10.197.1.216:7001/PMPlatForm/VideoForPGIS/player2.jsp?sysname=ycpgis&citizenid=640195412358742&password=ycpgis&deviceID=64010400001310000150");
                                process.WaitForInputIdle();
                            }
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
    }
}