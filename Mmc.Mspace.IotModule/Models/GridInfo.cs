using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class GridInfo:BindableBase
    {
        public string id { get; set; }
        public string name { get; set; }
        public string geom { get; set; }
        public string group { get; set; }
        public string online_count { get; set; }
        public string departments { get; set; }
        private bool _Is_Open;
        public bool Is_Open
        {
            get { return _Is_Open; }
            set { _Is_Open = value; NotifyPropertyChanged("Is_Open"); }
        }

    }
}
