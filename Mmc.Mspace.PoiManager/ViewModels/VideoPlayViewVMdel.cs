using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    class VideoPlayViewVMdel : BindableBase
    {
        private VideoPlayView _VideoDisplayView;
        private string _VideoPath;

        public string VideoPath
        {
            get { return _VideoPath; }
            set
            {
                _VideoPath = value;
                NotifyPropertyChanged("VideoPath");
            }
        }

        private double _Progress_value;

        public double Progress_value
        {
            get { return this._Progress_value; }
            set
            {
                _Progress_value = value;
                NotifyPropertyChanged("Progress_value");
            }
        }

        private double _Playtime;

        public double Playtime
        {
            get { return this._Playtime; }
            set
            {
                _Playtime = value;
                NotifyPropertyChanged("Playtime");
                // _Playtime = this._VideoDisplayView.Media.Position.TotalSeconds/ this._VideoDisplayView.Media.NaturalDuration.TimeSpan.TotalSeconds;
                TimeSpan ts = new TimeSpan(0, 0, 0, 0,
                    Convert.ToInt32(Math.Floor(this._VideoDisplayView.Media.NaturalDuration.TimeSpan.TotalMilliseconds *
                                               _Playtime)));
                this._VideoDisplayView.Media.Position =
                    ts; //this._VideoDisplayView.Media.NaturalDuration.TimeSpan.TotalSeconds
            }
        }

        [XmlIgnore] public ICommand CancelCmd { get; set; }

        ///  [XmlIgnore]
        public ICommand play_puase_Cmd { get; set; }

        public VideoPlayViewVMdel()
        {
            _VideoDisplayView = new VideoPlayView();
            _VideoDisplayView.DataContext = this;
            this.CancelCmd = new RelayCommand(() => { CloseVideoView(); });
            this.play_puase_Cmd = new RelayCommand(() => { });
        }

        private bool _Play_station;

        public bool Play_station
        {
            get { return this._Play_station; }
            set
            {
                _Play_station = value;
                NotifyPropertyChanged("Play_station");
            }
        }

        public void ShowVideoView(string VPath)
        {
            VideoPath = VPath;
            _VideoDisplayView.Show();
        }

        private void CloseVideoView()
        {
            _VideoDisplayView.Close();
        }

    }
}