using System;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule
{
    public partial class VideoMonitorView : Window
    {
        private string _path;

        private TimeSpan _pausest;

        public VideoMonitorView(string videoPath) : this()
        {
            this._path = videoPath;
            Uri source = new Uri(videoPath);
            this.mediaCtrl.Source = source;
        }

        public VideoMonitorView()
        {
            this.InitializeComponent();
            this.mediaCtrl.MediaEnded += delegate (object s, RoutedEventArgs e)
            {
                this.mediaCtrl.Position = default(TimeSpan);
                this.mediaCtrl.Play();
            };

            this.DataContext = new { WindowTitle = "室内全景", ContentWidth = 800, ContentHeight = 500 };

        }

        public string VideoPath
        {
            get { return this._path; }
            set
            {
                this._path = value;
                this.mediaCtrl.Source = value == null ? null : new Uri(this._path);
                this.mediaCtrl.Play();
            }
        }

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

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            bool canPause = this.mediaCtrl.CanPause;
            if (canPause)
            {
                this.mediaCtrl.Pause();
                this._pausest = this.mediaCtrl.Position;
            }
        }

        private void Button_CloseCmd(object sender, RoutedEventArgs e)
        {
            CloseMedia();
            base.Hide();
        }


        public void CloseMedia()
        {
            this.mediaCtrl.Close();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try { base.DragMove(); } catch (Exception) { }
        }
    }
}