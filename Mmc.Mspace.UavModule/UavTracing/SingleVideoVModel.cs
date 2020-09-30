using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
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
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.UavModule.Views;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class SingleVideoVModel : CheckedToolItemModel
    {
        //private SingleWebVideoView VideoView;
        HttpService _httpService;
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string TestVideo { get; set; }

        private string _windowTitle;
        private string _indexScreen;
        private string _urlVideo;
        private string _urlMspace;
        private bool _isPlaying = false;

        [XmlIgnore]
        public string WindowTitle
        {
            get { return this._windowTitle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._windowTitle, value, "WindowTitle"); }
        }

        [XmlIgnore]
        public string IndexScreen
        {
            get { return this._indexScreen; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._indexScreen, value, "IndexScreen"); }
        }

        [XmlIgnore]
        public bool IsPlaying
        {
            get { return this._isPlaying; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isPlaying, value, "IsPlaying"); }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _httpService = new HttpService();
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _urlMspace = json1.poiUrl;
            _urlVideo = string.Format("{0}/video.html?rtmpUrl=", json1.poiUrl);
            IsPlaying = false;
          
        }


        public SinggleVideoView VideoView { get; set; }
        private SingleVideoViewModel _singleVideoViewModel = new SingleVideoViewModel();



        public override void OnChecked()
        {


            //该视频控件只能重复用一个,多个会崩溃
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            var realUrlVideo = string.Empty;
            if (ground_state == "1")
            {
                base.OnChecked();
                _httpService.Token = HttpServiceUtil.Token;
                var uavVideo = "rtmp://pull.mmcuav.cn/live/mmcfactory";
                if (string.IsNullOrEmpty(ground_serial))
                    realUrlVideo = uavVideo;
                else
                {
                    try
                    {
                        var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                        string getVideoUrl = string.Format("{0}/api/aircraft/get-video-by-serial?ground_serial=", json.poiUrl) + ground_serial;
                        var res = _httpService.HttpRequest(getVideoUrl);
                        var resDynamic = JsonUtil.DeserializeFromString<dynamic>(res);
                        if ((bool)resDynamic.status)
                        {
                            string rtmp_url = resDynamic.data.rtmp_url;
                            realUrlVideo = rtmp_url;
                        }

                        VideoView.DataContext = _singleVideoViewModel;
                        _singleVideoViewModel.WindowTitle = this.WindowTitle;
                    }
                    catch (Exception ex)
                    {
                        SystemLog.Log(ex);
                        realUrlVideo = uavVideo;
                    }

                }

                shell.Dispatcher.Invoke((Action)(() =>
                {
                    _singleVideoViewModel.SetStreamUrl(realUrlVideo);
                    _singleVideoViewModel.Play();
                    IsPlaying = true;

                }));
            }
            else
            {
                shell.Dispatcher.Invoke((Action)(() =>
                {
                    //this.VideoView?.webCtrl.Navigate(new Uri(string.Format("{0}/video.html?serialNumber={1}", this._urlMspace, this.IndexScreen)));
                    IsPlaying = false;
                }));
            }
        }
        public double OffsetHeight { get; set; }
        public double Width { get; set; }

        public string ground_serial { get; set; }
        public string ground_state { get; set; }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Dispatcher.Invoke((Action)(() =>
            {
                //var url = string.Format("{0}/video.html?serialNumber={1}", this._urlMspace, this.IndexScreen);
                //this.VideoView?.webCtrl.Navigate(new Uri(url));
                _singleVideoViewModel.Stop();
                this.ground_state = "0";
                IsPlaying = false;
            }));
        }
    }
}
