using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class AccountModelNew : BindableBase
    {
        private int _id;
        private string _title;
        private DateTime _problemTime;
        private List<string> _imgPathList;
        private int _imgNum;
        private bool _isShowInReport;
        private string _video;

        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged("Title"); }
        }
        public DateTime ProblemTime
        {
            get { return _problemTime; }
            set { _problemTime = value; NotifyPropertyChanged("ProblemTime"); }
        }
        public int ImgNum
        {
            get { return _imgNum; }
            set { _imgNum = value; NotifyPropertyChanged("ImgNum"); }
        }
        public bool IsShowInReport
        {
            get { return _isShowInReport; }
            set { _isShowInReport = value; NotifyPropertyChanged("IsShowInReport"); }
        }
        public string Video
        {
            get { return _video; }
            set { _video = value; NotifyPropertyChanged("Video"); }
        }
        public List<string> ImgPathList { get; set; }
        public AccountModelNew()
        {
            _imgPathList = new List<string>();
        }
    }
}
