using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
   public class GridModel : BindableBase
    {
        public string id { get; set; }
        public string name { get; set; }
        public string geom { get; set; }
        public string group { get; set; }
        public string level { get; set; }
        public string level_name { get; set; }

        public List<GridModel> list { get; set; }

    

        private bool _IsExpanded;
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set { _IsExpanded = value; NotifyPropertyChanged("IsExpanded"); }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; NotifyPropertyChanged("IsSelected"); }
        }
        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value; NotifyPropertyChanged("IsChecked"); }
        }
    }
}
