using Mmc.Mspace.Services;
using Mmc.Windows.Services;
using System;
using System.Windows;

namespace FireControlModule.VideoMonitor
{
    /// <summary>
    /// 视频监控：单个
    /// </summary>
    public class VideoSingleViewModel : VideoMonitorExViewModel
    {
        private VideoMonitorView _videoMonitorView;

        public override void Initialize()
        {
            base.Initialize();
            _videoMonitorView = new VideoMonitorView() { Owner = Application.Current.MainWindow };
            _videoMonitorView.DataContext = new { WindowTitle = "视频监控", ContentWidth = 400, ContentHeight = 300 };
        }

        public override void RestoreEnv()
        {
            base.RestoreEnv();
            _videoMonitorView.CloseMedia();
            _videoMonitorView.Hide();
        }

        public override void ShowVideoView(string oid)
        {
            base.ShowVideoView(oid);
            string videoPath = ServiceManager.GetService<ICameraInfoService>(null).GetVideoPath(oid);
            VideoMonitorView videoMonitorView = _videoMonitorView;
            if (oid == "1")
                videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_大门.avi";
            else if (oid == "2")
                videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_娱乐区.avi";
            else if (oid == "3")
                videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_停车场.avi";
            else
                videoMonitorView.VideoPath = videoPath;
            videoMonitorView.Show();
        }
    }
}