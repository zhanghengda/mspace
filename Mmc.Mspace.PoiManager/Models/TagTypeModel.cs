using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class TagTypeModel : BindableBase
    {
        private string _id;
        public string id
        {
            get { return _id; }
            set { SetAndNotifyPropertyChanged(ref _id,value); }
        }

        private string _user_id;
        public string user_id
        {
            get { return _user_id; }
            set { SetAndNotifyPropertyChanged(ref _user_id,value); }
        } 

        private string _name;

        public string name
        {
            get { return _name; }
            set { SetAndNotifyPropertyChanged(ref _name, value); }
        }

        private ObservableCollection<LabelInfoModel> _tags;

        public ObservableCollection<LabelInfoModel> tags
        {
            get { return _tags ?? (_tags = new ObservableCollection<LabelInfoModel>()); }
            set { SetAndNotifyPropertyChanged(ref _tags, value); }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetAndNotifyPropertyChanged(ref _isChecked, value); }
        }
    }
}
