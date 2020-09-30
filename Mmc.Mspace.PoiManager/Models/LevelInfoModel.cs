using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class LevelInfoModel : BindableBase
    {
        private string _levelName;

        public string LevelName
        {
            get { return _levelName; }
            set
            {
                _levelName = value;
                NotifyPropertyChanged("LevelName");
            }
        }

        private string _levelValue;

        public string LevelValue
        {
            get { return _levelValue; }
            set
            {
                _levelValue = value;
                NotifyPropertyChanged("LevelValue");
            }
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
            }
        }
    }
}
