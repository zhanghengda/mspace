using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using System;

namespace FireControlModule.VideoMonitor
{
    /// <summary>
    /// 视频融合
    /// </summary>
    public class VideoMergeViewModel : VideoMonitorExViewModel
    {
        private ITerrainVideo _terrVideo;

        public override void Initialize()
        {
            base.Initialize();
            _canFlyTo = false;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ServiceManager.GetService<IShellService>().HideAllView();
            ServiceManager.GetService<IShellService>().ShowBottomRight(0);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ServiceManager.GetService<IShellService>().ShowAllView();
        }

        public override void RestoreEnv()
        {
            base.RestoreEnv();
            this.ClearTerrVideo();
        }

        public override void ShowVideoView(string oid)
        {
            base.ShowVideoView(oid);
            this.ClearTerrVideo();
            if (_terrVideo == null)
            {
                _terrVideo = CreateMergeVideo(CreateVideoParam(oid));
                //调整相机位置与角度
                var pt = GviMap.Camera.GetAimingPoint2(_terrVideo.Position, _terrVideo.Angle, -5);
                pt.Z += 3;
                GviMap.Camera.SetCamera2(pt, _terrVideo.Angle, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }

        private void ClearTerrVideo()
        {
            if (_terrVideo != null)
            {
                GviMap.ObjectManager.DeleteObject(_terrVideo.Guid);
                _terrVideo = null;
            }
        }
        private ITerrainVideo CreateMergeVideo(dynamic VideoParam)
        {
            var pt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
            pt.X = VideoParam.X;
            pt.Y = VideoParam.Y;
            pt.Z = VideoParam.Z;
            var terrVideo = GviMap.ObjectManager.CreateTerrainVideo(pt);
            terrVideo.Angle = new EulerAngle() { Heading = VideoParam.Heading, Roll = VideoParam.Roll, Tilt = VideoParam.Tilt };
            terrVideo.AspectRatio = VideoParam.AspectRatio;
            terrVideo.FarClip = VideoParam.FarClip;
            terrVideo.FieldOfView = VideoParam.FieldOfView;
            terrVideo.VideoFileName = VideoParam.VideoFileName;
            terrVideo.DisplayMode = VideoParam.DisplayMode;
            terrVideo.PlayVideoOnStartup = VideoParam.PlayVideoOnStartup;
            terrVideo.VideoPosition = VideoParam.VideoPosition;
            return terrVideo;
        }

        private dynamic CreateVideoParam(string oid)
        {
            object VideoParam = null;
            //AspectRatio 纵横比 FarClip 远裁距离 FieldOfView 纵向广角
            if (oid == "1")
            {
                VideoParam = new
                {
                    X = 496761.69,
                    Y = 2496395.41,
                    Z = 5,
                    Heading = -45,
                    Roll = 0,
                    Tilt = -40,
                    AspectRatio = 1.9,
                    FarClip = 30,
                    FieldOfView = 45,
                    VideoFileName = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_大门.avi",
                    DisplayMode = gviTVDisplayMode.gviTVShowAll,
                    PlayVideoOnStartup = true,
                    VideoPosition = 0,
                };
            }
            else if (oid == "2")
            {
                VideoParam = new
                {
                    X = 496811.85,
                    Y = 2496325.4,
                    Z = 2.6,
                    Heading = -6,
                    Roll = 0,
                    Tilt = -15,
                    AspectRatio = 3,
                    FarClip = 50,
                    FieldOfView = 30,
                    VideoFileName = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_娱乐区.avi",
                    DisplayMode = gviTVDisplayMode.gviTVShowAll,
                    PlayVideoOnStartup = true,
                    VideoPosition = 14.98,
                };
            }
            else if (oid == "3")
            {
                VideoParam = new
                {
                    X = 496982.97,
                    Y = 2496410.97,
                    Z = 10.04,
                    Heading = 175,
                    Roll = 0,
                    Tilt = -30,
                    AspectRatio = 1.37,
                    FarClip = 50,
                    FieldOfView = 40,
                    VideoFileName = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_停车场.avi",
                    DisplayMode = gviTVDisplayMode.gviTVShowAll,
                    PlayVideoOnStartup = true,
                    VideoPosition = 36.23,
                };
            }

            return VideoParam;
        }
    }
}