using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.IotModule.Views;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Windows.Utils;
using Mmc.Mspace.IotModule.FaceDetection;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.Common.Messenger;

namespace Mmc.Mspace.IotModule.ViewModels
{
    public class VideoStreamViewModel : CheckedToolItemModel
    {
        private List<IRenderable> _renderableList;
        private Dictionary<string, DeviceInfoModel> _deviceInfoList;
        private CreatePoiManager _poiManager;
        private DeviceDataManager _deviceManager;
        private Dictionary<string, IRenderPOI> _rPoiDic;

        //private VideoStreamView VideoStreamView;
        private VideoVLCView VideoStreamView;

        private KeyValuePair<string, IDisplayLayer> _oldSelectFc;
        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();
        private string _filterText;
        private string _address;
        private int _x;
        private int _y;
        private string _urlPath;

        public string UrlPath
        {
            get { return _urlPath; }
            set { _urlPath = value; NotifyPropertyChanged("UrlPath"); }
        }

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; NotifyPropertyChanged("WindowTitle"); }
        }


        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._address, value, "Address");
            }
        }
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }
        public override void Initialize()
        {
            base.ViewType = ViewType.CheckedIcon;
            base.Initialize();
            WindowTitle = Helpers.ResourceHelper.FindKey("Videomonitoring");
            //this.Command = new RelayCommand(GetSource);
            CloseCmd = new RelayCommand(() =>
            {
                VideoStreamView.Stop();
                base.IsChecked = false;
                //VideoStreamView.Hide();
            });

            this._renderableList = new List<IRenderable>();
            _deviceInfoList = new Dictionary<string, DeviceInfoModel>();
            _poiManager = new CreatePoiManager();
            _deviceManager = new DeviceDataManager();
            _rPoiDic = new Dictionary<string, IRenderPOI>();

        }

        public override void OnChecked()
        {
            base.OnChecked();
            Messenger.Messengers.Notify("HideSummery", false);
            ShowData();
        }


        public override void OnUnchecked()
        {
            base.OnUnchecked();
            Messenger.Messengers.Notify("ShowSummery", false);
            HiddenData();
        }

        //private static bool ShowState = false;
        //private void GetSource()
        //{
        //    if (ShowState)
        //        HiddenData();
        //    else
        //        ShowData();
        //}
        private void ShowData()
        {
            IsSelected = true;
            //ShowState = true;

            //try
            //{
            //    if (VideoStreamView == null)
            //        VideoStreamView = new VideoStreamView();
            //    VideoStreamView.DataContext = this;

            //    try
            //    {
            //        _deviceManager.GetDeviceInfoList("video", ref _deviceInfoList);
            //    }
            //    catch { }

            //    foreach (var item in _deviceInfoList)
            //    {
            //        var rPoi = _poiManager.CreatePoi(item.Value, "监控");
            //        _rPoiDic.Add(item.Value.longitude.ToString() + item.Value.latitude.ToString(), rPoi);
            //        _renderableList.Add(rPoi);
            //    }

            //    this.UnRegexEvent();
            //    GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            //    GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
            //    //GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            //    GviMap.AxMapControl.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClick);
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.StackTrace);
            //}


            try
            {
                if (VideoStreamView == null)
                    VideoStreamView = new VideoVLCView();
                VideoStreamView.DataContext = this;

                try
                {
                    _deviceManager.GetDeviceInfoList("video", ref _deviceInfoList);
                }
                catch
                {
                }

                foreach (var item in _deviceInfoList)
                {
                    var rPoi = _poiManager.CreatePoi(item.Value, "监控");
                    _rPoiDic.Add(item.Value.longitude.ToString() + item.Value.latitude.ToString(), rPoi);
                    _renderableList.Add(rPoi);
                }

                this.UnRegexEvent();
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectRenderGeometry;
                GviMap.AxMapControl.RcMouseClickSelect +=
                    new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClick);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void RenderControl_RcMouseClick(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (IntersectPoint == null)
                return;

            this.UnHighLightBuilding();

            string key = IntersectPoint.X.ToString("###.#####") + IntersectPoint.Y.ToString("###.#####");
            DeviceInfoModel video;
            if (_deviceInfoList.ContainsKey(key))
                video = _deviceInfoList[key];
            else
                return;

            string url = video?.datainfo;
            string isActivityFaceDetection = CacheData.GetConfigAppSettingsValue("MonitorSet");
            if (isActivityFaceDetection == "true")
            {
                //url = @"rtmp://media3.sinovision.net:1935/live/livestream";
                FaceDetectionStart(url);
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("MonitorFaceDetection"));
            }
            else
            {
                UrlPath = url;

                //if (VideoStreamView == null)
                //    VideoStreamView = new VideoStreamView();
                //VideoStreamView.DataContext = this;
                //VideoStreamView.Owner = Application.Current.MainWindow;
                //VideoStreamView.Show();
                //VideoStreamView.SetStreamUrl(UrlPath);
                //VideoStreamView.Play();

                if (VideoStreamView == null)
                    VideoStreamView = new VideoVLCView();
                VideoStreamView.DataContext = this;
                VideoStreamView.Owner = Application.Current.MainWindow;
                VideoStreamView.Show();
                VideoStreamView.SetStreamUrl(UrlPath);
                VideoStreamView.Play();
            }

        }

        private void HiddenData()
        {
            IsSelected = false;
            //ShowState = false;
            VideoStreamView.Hide();
            this.UnHighLightBuilding();
            this.UnRegexEvent();

            GviMap.ObjectManager.ReleaseRenderObject(_renderableList?.ToArray());
            _renderableList?.Clear();
            _deviceInfoList?.Clear();
            _rPoiDic?.Clear();
        }
        private void UnHighLightBuilding()
        {
            bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
            if (flag)
            {
                this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, GviMap.MapControl.FeatureManager);
            }
        }
        protected virtual bool RenderControl_RcMouseHover(uint flags, int x, int y)
        {
            bool flag = this._x == x && this._y == y;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                this.UnHighLightBuilding();
                this._x = x;
                this._y = y;
                IPoint point;
                IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(x, y, out point);
                if (pickResult == null)
                    return false;
                string key = point.X.ToString() + point.Y.ToString();
                DeviceInfoModel video;
                if (_deviceInfoList.ContainsKey(key))
                    video = _deviceInfoList[key];
                else
                    return false;

                string url = video.datainfo;

                string isActivityFaceDetection = CacheData.GetConfigAppSettingsValue("MonitorSet");
                if (isActivityFaceDetection == "true")
                {
                    //url = @"rtmp://media3.sinovision.net:1935/live/livestream";
                    FaceDetectionStart(url);
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("MonitorFaceDetection"));
                    return false;
                }
                else
                {
                    UrlPath = url;

                    //if (VideoStreamView == null)
                    //    VideoStreamView = new VideoStreamView();

                    if (VideoStreamView == null)
                        VideoStreamView = new VideoVLCView();

                    VideoStreamView.DataContext = this;
                    VideoStreamView.Owner = Application.Current.MainWindow;
                    VideoStreamView.Show();
                    VideoStreamView.SetStreamUrl(UrlPath);
                    VideoStreamView.Play();
                    return false;
                }
            }
            result = false;
            return result;
        }
        private void UnRegexEvent()
        {
            //GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
            GviMap.AxMapControl.RcMouseClickSelect -= new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClick);
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
        }

        FaceDetectionProcess pythonProcess = new FaceDetectionProcess();

        private void FaceDetectionStart(string streamUrl)
        {
            try
            {
                //pythonProcess.ImputFile = fileName;inference_usbCam_face
                //var file = @"F:\DemoSource\face-detection\tensorflow-face-detection\inference_usbCam_face_tkwindow.py";
                var file = AppDomain.CurrentDomain.BaseDirectory + @"PyPlugIn\face-detection\inference_rtsp_face_tkwindow.py";
                pythonProcess.ProcessPythonFile = "\"" + file + "\"";
                pythonProcess.StreamUrl = streamUrl;

                ThreadStart calth = new ThreadStart(pythonProcess.RunCmd);
                Thread t = new Thread(calth);
                t.Start();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}
