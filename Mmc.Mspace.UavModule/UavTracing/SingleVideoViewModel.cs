using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Xml.Serialization;
using LibVLCSharp.Shared;
using Mmc.Windows.Services;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class SingleVideoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LibVLC _libVLC;
        private string _videoUrl;
        private bool _isPlaying = false;
        private Timer _timer;

        private LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        private string _windowTitle;
        [XmlIgnore]
        public string WindowTitle
        {
            get { return this._windowTitle; }
            set { Set("WindowTitle", ref _windowTitle, value); }
        }

        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Timer检测间隔
        /// </summary>
        private double _interval = 3000;

        public SingleVideoViewModel()
        {
            //使用VLC插件必须先执行这句,否则会报找不到DLL的异常
            Core.Initialize();
            _libVLC = new LibVLC();
            MediaPlayer = new MediaPlayer(_libVLC);

            _timer = new Timer(_interval);
            _timer.Elapsed += Timer_Elapsed;

        }

        public void SetStreamUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;
            _videoUrl = url;
            _isPlaying = false;
        }

        public void Play()
        {
            try
            {
                if (!_isPlaying)
                {
                    var uri = new Media(_libVLC, _videoUrl, FromType.FromLocation);
                    MediaPlayer.Play(uri);
                    _isPlaying = true;
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void Stop()
        {
            try
            {
                _timer.Stop();
                if (MediaPlayer.IsPlaying)
                {

                    MediaPlayer.Stop();
                }
                _isPlaying = false;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void Pause()
        {
            try
            {
                _timer.Stop();
                if (MediaPlayer.IsPlaying)
                {
                    MediaPlayer.Pause();
                }
                _isPlaying = false;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        /// <summary>
        /// 截图方法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool TakeSnapshot(string path, uint width, uint height)
        {
            return _mediaPlayer.TakeSnapshot(0, path, width, height);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_isPlaying && !_mediaPlayer.IsPlaying)
            {
                try
                {
                    var uri = new Media(_libVLC, _videoUrl, FromType.FromLocation);
                    MediaPlayer.Play(uri);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
