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
using GFramework.BlankWindow;
using LibVLCSharp.Shared;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavWebVideoViewModel : CheckedToolItemModel
    {
        //private UavVideoWebView videoView;
        private UavVideoVLCView videoView;
        HttpService _httpService;
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string WindowTitle { get; set; }
        public override void Initialize()
        {
            base.Initialize();

            base.ViewType = ViewType.CheckedIcon;
            _httpService = new HttpService();
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
        }
        public Action<string> OnClosed { get; set; }
        public override void OnChecked()
        {
            if (ground_state == "1")
            {
                base.OnChecked();
                _httpService.Token = HttpServiceUtil.Token;
                var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                //string url = string.Format("{0}/video.html?rtmpUrl=", json1.poiUrl);
                string url = "";
                var uavVideo = "rtmp://pull.mmcuav.cn/live/test";



                if (string.IsNullOrEmpty(ground_serial))
                    url = url + uavVideo;
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
                            url = url + rtmp_url;
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLog.Log(ex);
                        url = url + uavVideo;
                    }

                }

                //该视频控件只能重复用一个,多个会崩溃
                var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
                shell.Dispatcher.Invoke(() =>
                {
                    //if (videoView == null)
                    //videoView = new UavVideoWebView();
                    //this.videoView.Owner = Application.Current.MainWindow;
                    //this.videoView.Width = Width;
                    //this.videoView.Height = 259;
                    //this.videoView.Left = shell.Width - Width - 10;
                    //this.videoView.Top = 10;
                    //videoView.DataContext = this;
                    //videoView.webCtrl.Navigate(new Uri(url));
                    //videoView.Show();

                    if (videoView == null)
                        videoView = new UavVideoVLCView();
                    videoView.Owner = Application.Current.MainWindow;
                    videoView.Width = 750;
                    videoView.Height = 525;
                    videoView.Left = shell.Width - 750 - 10;
                    videoView.Top = 10;
                    videoView.DataContext = this;
                    videoView.Show();
                    videoView.SetUrl(url);
                });
            }
            else
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceNotOnline"));
        }
        public double OffsetHeight { get; set; }
        public double Width { get; set; }

        public string ground_serial { get; set; }
        public string ground_state { get; set; }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Dispatcher.Invoke(() =>
            {
                //videoView?.webCtrl.Dispose();
                //videoView?.Close();
                //videoView = null;
                //if (OnClosed != null)
                //    this.OnClosed(this.ground_serial);

                videoView?.Close();
                videoView = null;
                if (OnClosed != null)
                    this.OnClosed(this.ground_serial);
            });
        }
    }
}
