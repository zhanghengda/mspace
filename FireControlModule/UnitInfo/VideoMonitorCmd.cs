using Mmc.Wpf.Commands;
using System;
using System.IO;

namespace FireControlModule
{
    public class VideoMonitorCmd : SimpleCommand
    {
        private VideoMonitorView _view;

        public VideoMonitorCmd(VideoMonitorView view)
        {
            _view = view;
        }

        public override void Execute(object parameter)
        {
            VideoMonitorView videoMonitorView = (VideoMonitorView)_view;
            string unitCode = parameter.ParseTo<string>();
            var videoPath = string.Format(@"{0}项目数据\三小视频\{1}.mp4", AppDomain.CurrentDomain.BaseDirectory, unitCode);
            if (File.Exists(videoPath))
            {
                videoMonitorView.VideoPath = videoPath;
                videoMonitorView.Show();
            }
        }
    }
}