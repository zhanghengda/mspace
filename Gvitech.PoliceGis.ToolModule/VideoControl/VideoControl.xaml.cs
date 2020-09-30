using System;
using System.Windows;
using System.Windows.Controls;

namespace Mmc.Mspace.ToolModule.VideoControl
{
    /// <summary>
    /// VideoView.xaml 的交互逻辑   
    /// </summary>
    public partial class VideoControl : UserControl
    {
        private string _path;
        private TimeSpan _pausest;

        public VideoControl()
        {
            InitializeComponent();
            this.mediaCtrl.MediaEnded += delegate (object s, RoutedEventArgs e)
            {
                this.mediaCtrl.Position = default(TimeSpan);
                this.mediaCtrl.Play();
            };
        }

        public string VideoPath
        {
            get { return this._path; }
            set
            {
                this._path = value;
                Uri source = new Uri(this._path);
                this.mediaCtrl.Source = source;
                this.mediaCtrl.Play();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            bool flag = this.mediaCtrl.Source == null;
            if (flag) { this.mediaCtrl.Source = new Uri(this._path); }
            this.mediaCtrl.Position = (this._pausest.Equals(this.mediaCtrl.NaturalDuration) ? default(TimeSpan) : this._pausest);
            this.mediaCtrl.Play();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bool canPause = this.mediaCtrl.CanPause;
            if (canPause)
            {
                this.mediaCtrl.Pause();
                this._pausest = this.mediaCtrl.Position;
            }
        }

        public void SetVideoPath(string videoPath)
        {
            this.mediaCtrl.Source = new Uri(videoPath);
            this.mediaCtrl.Play();
        }
    }
}