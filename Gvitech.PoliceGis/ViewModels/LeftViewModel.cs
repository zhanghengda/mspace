using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.MSpace.ViewModels
{
    public class LeftViewModel: BindableBase
    {

        public LeftViewModel()
        {

        }


        private bool _isVisibility=true;

        public bool IsVisibility
        {
            get { return _isVisibility; }
            set { _isVisibility = value; base.NotifyPropertyChanged("IsVisibility"); }
        }


    }
}
