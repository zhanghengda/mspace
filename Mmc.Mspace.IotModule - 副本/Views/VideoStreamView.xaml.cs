using Microsoft.Win32;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mmc.Mspace.IotModule.Views
{
    /// <summary>
    /// VideoStreamView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoStreamView 
    {
        public VideoStreamView()
        {
            InitializeComponent();
        }
        public void SetStreamUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;
            _urlTextBox.Text = url;
            //HandlePlayButtonClick(null, null);
        }

        private void HandlePlayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var uri = new Uri(_urlTextBox.Text);
                _streamPlayerControl.StartPlay(uri);
                _statusLabel.Text = "Connecting...";
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        public void Play()
        {
            this.HandlePlayButtonClick(null, null);
        }

        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        public void Stop()
        {
            try
            {
                if (_streamPlayerControl.IsPlaying)
                    _streamPlayerControl.Stop();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        private void HandleImageButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "Bitmap Image|*.bmp" };
            if (dialog.ShowDialog() == true)
            {
                _streamPlayerControl.GetCurrentFrame().Save(dialog.FileName);
            }
        }

        private void UpdateButtons()
        {
            _playButton.IsEnabled = !_streamPlayerControl.IsPlaying;
            _stopButton.IsEnabled = _streamPlayerControl.IsPlaying;
            _imageButton.IsEnabled = _streamPlayerControl.IsPlaying;
        }

        private void HandlePlayerEvent(object sender, RoutedEventArgs e)
        {
            UpdateButtons();

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                _statusLabel.Text = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {
                _statusLabel.Text = "Failed";

                MessageBox.Show(
                    ((WebEye.Controls.Wpf.StreamPlayerControl.StreamFailedEventArgs)e).Error,
                    "Stream Player",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Stop();
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                _statusLabel.Text = "Stopped";
            }
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try { base.DragMove(); } catch (Exception) { }
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            this.Play();
            var parent = Window.GetWindow(this);
            if (parent != null)
                parent.Closed += (sender2, e2) => { Stop(); };
        }
    }
}
