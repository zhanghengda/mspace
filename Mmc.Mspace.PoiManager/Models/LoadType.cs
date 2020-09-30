using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class LoadType: BindableBase
    {
        private string _id { get; set; }
        public string id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("id"); }
        }
       
        private string _name { get; set; }
        public string name
        {
            get { return _name; }
            set
            {
                _name = value; NotifyPropertyChanged("name");
            }
        }
        private string _path { get; set; }
        public string path
        {
            get { return _path; }
            set
            {
                _path = value; NotifyPropertyChanged("path");
            }
        }
        private string _loadStation { get; set; }  //未开始，（暂停），正在运行，结束
        public string loadStation
        {
            get { return _loadStation; }
            set
            {
                _loadStation = value; NotifyPropertyChanged("loadStation");
            }
        }
        private string _updateTime { get; set; } 
        public string UpdateTime
        {
            get { return _updateTime; }
            set
            {
                _updateTime = value; NotifyPropertyChanged("UpdateTime");
            }
        }

        

        public System.Windows.Visibility IsImage { get; set; }
        public LoadType(string _id, string _name, string _path, System.Windows.Visibility _isImage,string _updateTime, string _loadStation="未开始")
        {
            id = _id;
            name = _name;
            path = _path;
            loadStation = _loadStation;
            UpdateTime = _updateTime;
            IsImage = _isImage;
        }    
    }
}
