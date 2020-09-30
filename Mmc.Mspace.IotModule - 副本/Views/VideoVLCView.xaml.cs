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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Mmc.Mspace.IotModule.ViewModels;
using Mmc.Windows.Services;

namespace Mmc.Mspace.IotModule.Views
{
    /// <summary>
    /// VideoVLCView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoVLCView
    {
        private SingleVideoViewModel _singleVideoViewModel = new SingleVideoViewModel();
        public VideoVLCView()
        {
            InitializeComponent();

            _streamPlayControl.DataContext = _singleVideoViewModel;
            this.Closed += (s, e) => { _singleVideoViewModel.Stop(); };
        }

        public void SetStreamUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;
            _urlTextBox.Text = url;

            _singleVideoViewModel.SetStreamUrl(url);
        }

        private void HandlePlayButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _singleVideoViewModel.SetStreamUrl(_urlTextBox.Text);
                _statusLabel.Text = "Connecting...";

                Play();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        public void Play()
        {
            _singleVideoViewModel.Play();

            _playButton.IsEnabled = false;
            _stopButton.IsEnabled = true;
            _imageButton.IsEnabled = true;
        }

        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        public void Stop()
        {
            try
            {
                _singleVideoViewModel.Stop();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

            _stopButton.IsEnabled = false;
            _playButton.IsEnabled = true;
            _imageButton.IsEnabled = false;
        }

        public void Pause()
        {
            try
            {
                _singleVideoViewModel.Pause();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

            _stopButton.IsEnabled = false;
            _playButton.IsEnabled = true;
            _imageButton.IsEnabled = false;
        }

        private void HandleImageButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "Bitmap Image|*.bmp" };
            if (dialog.ShowDialog() == true)
            {
                if (!_singleVideoViewModel.TakeSnapshot(dialog.FileName,
                    uint.Parse(_streamPlayControl.ActualWidth.ToString()),
                    uint.Parse(_streamPlayControl.ActualHeight.ToString())))
                {
                    MessageBox.Show(
                        "保存失败",
                        "Stream Player",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    Pause();
                }
            }
        }

        private void UIElement_OnPriviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                base.DragMove();
            }
            catch (Exception ex)
            { }
        }

        private void windowLoaded(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            if (parent != null)
                parent.Closed += (sender2, e2) => { Stop(); };
        }
    }
}
