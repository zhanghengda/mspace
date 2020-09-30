using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class TagItem: BindableBase
    {
        private int _marker_id;

        public int marker_id
        {
            get { return _marker_id; }
            set { _marker_id = value; NotifyPropertyChanged("marker_id"); }
        }




        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("id"); }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("name"); }
        }


        private bool _isEdit;

        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; NotifyPropertyChanged("IsEdit"); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }


    }
}
