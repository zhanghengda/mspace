using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Common.Models
{
    public class TextItem: BindableBase
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set { _key = value;NotifyPropertyChanged("Key"); }
        }


        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; NotifyPropertyChanged("Value"); }
        }

    }
}
