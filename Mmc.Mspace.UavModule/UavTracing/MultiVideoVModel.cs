using LibVLCSharp.Shared;
using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class MultiVideoVModel : CheckedToolItemModel
    {
        private MultiVideoView _multiView;
        private SingleVideoVModel _LUpViewM;
        private SingleVideoVModel _LDwon1ViewM;
        private SingleVideoVModel _LDwon2ViewM;
        private SingleVideoVModel _RUp1ViewM;
        private SingleVideoVModel _RUp2ViewM;
        private SingleVideoVModel _RM1ViewM;
        private SingleVideoVModel _RM2ViewM;
        private SingleVideoVModel _RDwon1ViewM;
        private SingleVideoVModel _RDwon2ViewM;

        private ObservableCollection<VideoScreenVModel> _videoScreenVModels;

        public ObservableCollection<VideoScreenVModel> VideoScreenVModels
        {
            get { return _videoScreenVModels; }
        }

        public Action<string, string> OnVideoClosed { get; set; }

        public Action<string, string> OnVideoOpened { get; set; }

        private Dictionary<string, SingleVideoVModel> _vmList = new Dictionary<string, SingleVideoVModel>();

        #region NotifyProperty

        [XmlIgnore]
        public SingleVideoVModel LUpViewM
        {
            get { return this._LUpViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._LUpViewM, value, "LUpViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel LDwon1ViewM
        {
            get { return this._LDwon1ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._LDwon1ViewM, value, "LDwon1ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel LDwon2ViewM
        {
            get { return this._LDwon2ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._LDwon2ViewM, value, "LDwon2ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RUp1ViewM
        {
            get { return this._RUp1ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RUp1ViewM, value, "RUp1ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RUp2ViewM
        {
            get { return this._RUp2ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RUp2ViewM, value, "RUp2ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RM1ViewM
        {
            get { return this._RM1ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RM1ViewM, value, "RM1ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RM2ViewM
        {
            get { return this._RM2ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RM2ViewM, value, "RM2ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RDwon1ViewM
        {
            get { return this._RDwon1ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RDwon1ViewM, value, "RDwon1ViewM"); }
        }

        [XmlIgnore]
        public SingleVideoVModel RDwon2ViewM
        {
            get { return this._RDwon2ViewM; }
            set { base.SetAndNotifyPropertyChanged<SingleVideoVModel>(ref this._RDwon2ViewM, value, "RDwon2ViewM"); }
        }


        #endregion


        public override void OnChecked()
        {
            base.OnChecked();

            foreach (var key in _vmList.Keys)
            {
                var item = _vmList[key];
                item.OnChecked();
            }
            _multiView.Show();

        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            foreach (var key in _vmList.Keys)
            {
                var item = _vmList[key];
                item.OnUnchecked();
            }
            _multiView?.Hide();
        }

        public void OpenVideo(string indexScreen, string ground_serial, string groudState, string groundName)
        {
            if (string.IsNullOrEmpty(indexScreen) || !_vmList.ContainsKey(indexScreen))
            {
                MessageBox.Show("请指定屏幕");
                return;
            }
            var videoModel = _vmList[indexScreen];
            videoModel.ground_serial = ground_serial;
            videoModel.IndexScreen = indexScreen;
            videoModel.ground_state = groudState;
            videoModel.WindowTitle = string.Format("{0}:{1}", indexScreen, groundName);
            videoModel.OnChecked();
            //通知 已经连接
            OnVideoOpened?.Invoke(indexScreen, ground_serial);
        }

        public void CloseVideo(string indexScreen, string ground_serial)
        {
            if (indexScreen == null)
                return;
            if (!_vmList.ContainsKey(indexScreen))
            {
                MessageBox.Show("请指定屏幕");
                return;
            }
            var videoModel = _vmList[indexScreen];
            videoModel.IndexScreen = indexScreen;
            videoModel.OnUnchecked();
            //通知 已经关闭
            OnVideoClosed?.Invoke(indexScreen, ground_serial);
        }




        public override void Initialize()
        {
            base.Initialize();
            Core.Initialize();

            _videoScreenVModels = new ObservableCollection<VideoScreenVModel>{
                new VideoScreenVModel { ScreenIndex = "A1", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "A2", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "A3", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "B1", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "B2", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "B3", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "C1", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "C2", UavId =  "-1", IsUsed=false },
                new VideoScreenVModel { ScreenIndex = "C3", UavId =  "-1", IsUsed=false },
            };
            _videoScreenVModels.ForEach(p => { p.ScreenItem = string.Format("{0}-空闲", p.ScreenIndex); });

            _LUpViewM = new SingleVideoVModel();
            _LDwon1ViewM = new SingleVideoVModel();
            _LDwon2ViewM = new SingleVideoVModel();
            _RUp1ViewM = new SingleVideoVModel();
            _RUp2ViewM = new SingleVideoVModel();
            _RM1ViewM = new SingleVideoVModel();
            _RM2ViewM = new SingleVideoVModel();
            _RDwon1ViewM = new SingleVideoVModel();
            _RDwon2ViewM = new SingleVideoVModel();
            _vmList.Add("A1", _LUpViewM);
            _vmList.Add("A2", _LDwon1ViewM);
            _vmList.Add("A3", _LDwon2ViewM);
            _vmList.Add("B1", _RUp1ViewM);
            _vmList.Add("B2", _RM1ViewM);
            _vmList.Add("B3", _RDwon1ViewM);
            _vmList.Add("C1", _RUp2ViewM);
            _vmList.Add("C2", _RM2ViewM);
            _vmList.Add("C3", _RDwon2ViewM);

            if (_multiView == null)
            {
                _multiView = new MultiVideoView();
                Screen[] _screens = Screen.AllScreens;
                Screen s = null;
                if (_screens.Length > 1)
                    s = Screen.AllScreens[1];
                else
                    s = Screen.AllScreens[0];

                System.Drawing.Rectangle rect = s.WorkingArea;
                _multiView.DataContext = this;

                _multiView.Left = rect.Left;
                _multiView.Top = rect.Top;
                _multiView.Width = rect.Width;
                _multiView.Height = rect.Height;

                _LUpViewM.VideoView = _multiView.LUpView;
                _LDwon1ViewM.VideoView = _multiView.LDwon1View;
                _LDwon2ViewM.VideoView = _multiView.LDwon2View;
                _RUp1ViewM.VideoView = _multiView.RUp1View;
                _RUp2ViewM.VideoView = _multiView.RUp2View;
                _RM1ViewM.VideoView = _multiView.RM1View;
                _RM2ViewM.VideoView = _multiView.RM2View;
                _RDwon1ViewM.VideoView = _multiView.RDwon1View;
                _RDwon2ViewM.VideoView = _multiView.RDwon2View;
            }

            int i = 1;
            foreach (var key in _vmList.Keys)
            {
                var item = _vmList[key];
                item.ground_serial = string.Empty;
                item.ground_state = "0";
                item.IndexScreen = key;
                item.IsPlaying = false;
                item.VideoView.DataContext = item;
                //item.TestVideo = "rtmp://192.168.5.57:1935/live/00" + i;
                i++;
            }

            _multiView.DataContext = this;

        }
    }
}
