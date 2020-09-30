using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.RegularInspectionModule.Dto
{
    public class InspectModel : BindableBase
    {
        private int _inspectUnitId;
        public int InspectUnitId
        {
            get { return _inspectUnitId; }
            set { _inspectUnitId = value; NotifyPropertyChanged("InspectUnitId"); }
        }

        /// 巡检目标
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; NotifyPropertyChanged("Path"); }
        }

        /// 巡检时间
        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; NotifyPropertyChanged("Time"); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; NotifyPropertyChanged("UserName"); }
        }

        private InspectDataType _dataType;
        public InspectDataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; NotifyPropertyChanged("DataType");
                if(DataType== InspectDataType.Region)
                {
                    IsRegion = true;
                }
                else
                {
                    IsRegion = false;
                }
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }

        private int _isAdministrator;
        public int IsAdministrator
        {
            get { return _isAdministrator; }
            set { _isAdministrator = value; NotifyPropertyChanged("IsAdministrator"); }
        }

        private string _md5;
        public string Md5
        {
            get { return _md5; }
            set { _md5 = value; NotifyPropertyChanged("Md5"); }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; NotifyPropertyChanged("IsVisible"); }
        }

        private List<InspectModel> _inspectList;
        public List<InspectModel> InspectList
        {
            get { return _inspectList ?? (_inspectList = new List<InspectModel>()); }
            set {
                _inspectList = value; NotifyPropertyChanged("InspectList");
                
            }
        }

        private bool _isRegion =true;
        public bool IsRegion
        {
            get { return _isRegion; }
            set { _isRegion = value; NotifyPropertyChanged("IsRegion"); }
        }


        private string _geom;
        public string Geom
        {
            get { return _geom; }
            set { _geom = value; NotifyPropertyChanged("Geom"); }
        }
        private string _style;
        public string Style
        {
            get { return _style; }
            set { _style = value; NotifyPropertyChanged("Style"); }
        }
        private double _x;
        public double X
        {
            get { return _x; }
            set { _x = value; NotifyPropertyChanged("X"); }
        }
        private double _y;
        public double Y
        {
            get { return _y; }
            set { _y = value; NotifyPropertyChanged("Y"); }
        }
        private double _z;
        public double Z
        {
            get { return _z; }
            set { _z = value; NotifyPropertyChanged("Z"); }
        }
        private string _size;
        public string Size
        {
            get { return _size; }
            set { _size = value; NotifyPropertyChanged("Size"); }
        }
        private string _videoType;
        public string VideoType
        {
            get { return _videoType; }
            set { _videoType = value; NotifyPropertyChanged("VideoType"); }
        }

        private string _thumbnail;
        public string Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; NotifyPropertyChanged("Thumbnail"); }
        }
        private int _reportNum;
        public int ReportNum
        {
            get { return _reportNum; }
            set { _reportNum = value; NotifyPropertyChanged("ReportNum"); }
        }


        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; NotifyPropertyChanged("IsChecked"); }
        }

        private string _routeName;
        public string RouteName
        {
            get { return _routeName; }
            set { _routeName = value; NotifyPropertyChanged("RouteName"); }
        }

        private bool _isTroublePoi;
        public bool IsTroublePoi
        {
            get { return _isTroublePoi; }
            set { _isTroublePoi = value; NotifyPropertyChanged("IsTroublePoi"); }
        }

        private string _guid;
        public string Guid { get; set; }

    }
}
