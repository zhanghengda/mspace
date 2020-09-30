using Mmc.Wpf.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class AccountNew : BindableBase
    {
        private int _id;
        private string _title;
        private string _problemTime;
        private ObservableCollection<string> _imgPathList;
        private int _imgNum;
        private string _isShowInReport;
        private string _video;

        public int MarkerId { get; set; }

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string ProblemTime
        {
            get { return _problemTime; }
            set
            {
                _problemTime = value;
                NotifyPropertyChanged("ProblemTime");
            }
        }

        public int ImgNum
        {
            get { return _imgNum; }
            set
            {
                _imgNum = value;
                NotifyPropertyChanged("ImgNum");
            }
        }

        public string IsShowInReport
        {
            get { return _isShowInReport; }
            set
            {
                _isShowInReport = value;
                NotifyPropertyChanged("IsShowInReport");
            }
        }

        public string Video
        {
            get { return _video; }
            set
            {
                _video = value;
                NotifyPropertyChanged("Video");
            }
        }

        public ObservableCollection<string> ImgPathList
        {
            get => _imgPathList;
            set
            {
                _imgPathList = value;
                NotifyPropertyChanged("ImgPathList");
            }
        }

        public AccountNew()
        {
            _imgPathList = new ObservableCollection<string>();
        }
    }
}
