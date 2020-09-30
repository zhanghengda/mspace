using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Threading;
using System.Windows.Controls;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavVideoViewModel : CheckedToolItemModel
    {
        private UavVideoView videoView;
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string WindowTitle { get; set; }

        public string Url { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();
            //该视频控件只能重复用一个,多个会崩溃
            if (videoView == null)
                videoView = new UavVideoView();
            this.videoView.Owner = Application.Current.MainWindow;
            this.videoView.Width = Width;
            this.videoView.Height = 350;
            this.videoView.Left = 10;
            this.videoView.Top = this.OffsetHeight + 15;
            videoView.DataContext = this;
            videoView.SetStreamUrl(Url);
            videoView.Show();
            videoView.Play();


        }
        public double OffsetHeight { get; set; }
        public double Width { get; set; }
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            videoView?.Stop();
            videoView?.Hide();
            // videoView = null;
        }
    }
}
