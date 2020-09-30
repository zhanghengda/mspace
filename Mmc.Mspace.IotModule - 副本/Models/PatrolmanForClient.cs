using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class PatrolmanForClient: BindableBase
    {
        private string _name;
        private string _id;
        private string _phone;
        private string _status;
        private string _signInTime;
        //private List<PatroledDataForClient> _inspector_list;
        private bool _isSelected;


        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        public string ID
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("ID"); }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; NotifyPropertyChanged("Phone"); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }

        public string SignInTime
        {
            get { return _signInTime; }
            set { _signInTime = value; NotifyPropertyChanged("SignInTime"); }
        }
        //public List<PatroledDataForClient> Inspector_list
        //{
        //    get { return _inspector_list ?? (_inspector_list=new List<PatroledDataForClient>()); }
        //    set {
        //        _inspector_list = value;
        //        NotifyPropertyChanged("Inspector_list");
        //    }
        //}

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }
    }
}
