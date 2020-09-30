using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class MarkerNew :BindableBase
    {
        private int _marker_id ;
        private string _geom ;
        private string _code ;
        private string _img ;
        private string _address ;
        private int _type ;
        private int _cat_id ;
        private string _title ;
        private string _style ;
        private double _lp_size;
        private string _poitype;
        private string _phone ;
        private string _account_end_time;
        private string _life_day;
        private string _operator ;
        private string _detail ;
        private int _level ;
        private int _status;
        private int _user_id ;
        private string _addtime ;
        private string _letter ;
        private ObservableCollection<TagItem> _tags;

        public int MarkerId
        {
            get => _marker_id;
            set
            {
                _marker_id = value;
                NotifyPropertyChanged("MarkerId");
            }
        }
        public string Geom
        {
            get => _geom;
            set
            {
                _geom = value;
                NotifyPropertyChanged("Geom");
            }
        }
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                NotifyPropertyChanged("Code");
            }
        }
        public string ImgPath
        {
            get => _img;
            set
            {
                _img = value;
                NotifyPropertyChanged("ImgPath");
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                NotifyPropertyChanged("Address");
            }
        }
        public int Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

        public int CatId
        {
            get => _cat_id;
            set
            {
                _cat_id = value;
                NotifyPropertyChanged("CatId");
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public string Style
        {
            get => _style;
            set
            {
                _style = value;
                NotifyPropertyChanged("Style");
            }
        }

        public double Size
        {
            get => _lp_size;
            set
            {
                _lp_size = value;
                NotifyPropertyChanged("Size");
            }
        }

        public string Poitype
        {
            get => _poitype;
            set
            {
                _poitype = value;
                NotifyPropertyChanged("Poitype");
            }
        }

        
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                NotifyPropertyChanged("Phone");
            }
        }

        public string Account_end_time
        {
            get => _account_end_time;
            set
            {
                _account_end_time = value;
                NotifyPropertyChanged("Account_end_time");
            }
        }
        public string Life_day
        {
            get => _life_day;
            set
            {
                _life_day = value;
                NotifyPropertyChanged("Life_day");
            }
        }

        public string Operator
        {
            get => _operator;
            set
            {
                _operator = value;
                NotifyPropertyChanged("Operator");
            }
        }

        public string Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                NotifyPropertyChanged("Detail");
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                NotifyPropertyChanged("Level");
            }
        }
        public int Status
        {
            get => _status;
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }
        public int UserId
        {
            get => _user_id;
            set
            {
                _user_id = value;
                NotifyPropertyChanged("UserId");
            }
        }
        public string AddTime
        {
            get => _addtime;
            set
            {
                _addtime = value;
                NotifyPropertyChanged("AddTime");
            }
        }
        public string Letter
        {
            get => _letter;
            set
            {
                _letter = value;
                NotifyPropertyChanged("Letter");
            }
        }

        public ObservableCollection<TagItem> Tags
        {
            get => _tags??new ObservableCollection<TagItem>();
            set
            {
                _tags = value;
                NotifyPropertyChanged("Tags");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; NotifyPropertyChanged("IsChecked"); }
        }

        public string Guid { get; set; }
    }
}
