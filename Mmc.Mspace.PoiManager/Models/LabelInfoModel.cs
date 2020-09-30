using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class LabelInfoModel : BindableBase
    {
        private string _labelName;

        public string LabelName
        {
            get { return _labelName; }
            set { _labelName = value; NotifyPropertyChanged("LabelName"); }
        }

        private string _labelId;

        public string LabelId
        {
            get { return _labelId; }
            set { _labelId = value; NotifyPropertyChanged("LabelId"); }
        }

        private bool _labelIsSelected;

        public bool LabelIsSelected
        {
            get { return _labelIsSelected; }
            set { _labelIsSelected = value; NotifyPropertyChanged("LabelIsSelected"); }
        }
    }
}
