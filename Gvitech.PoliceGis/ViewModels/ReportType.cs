using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.MSpace.ViewModels
{
    public class ReportType: BindableBase
    {
        public string ID;
        private string _name;
        private string _code;
        private string _useName;
        private bool _Is_Open;
        public string idList;
        public ReportType(string _id, string _name,bool _isOpen)
        {
            // LabelText = _labelText;
            Name = _name;
            ID = _id;
            Is_Open = _isOpen;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        public string UseName
        {
            get { return _useName; }
            set { _useName = value; NotifyPropertyChanged("UseName"); }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyPropertyChanged("Code"); }
        }
        public bool Is_Open
        {
            get { return _Is_Open; }
            set { _Is_Open = value; NotifyPropertyChanged("Is_Open"); }
        }
        private  string _labelText;
        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; NotifyPropertyChanged("LabelText"); }
        }
        private bool _isEdit = false;
       public  bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; NotifyPropertyChanged("IsEdit"); }
        }


    }
}
