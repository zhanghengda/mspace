using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavItemAreaViewModel : BindableBase
    {
        private ObservableCollection<UavItemViewModel> uavItemModels;

        public ObservableCollection<UavItemViewModel> UavItemModels
        {
            get { return uavItemModels; }
            set
            {
                SetAndNotifyPropertyChanged<ObservableCollection<UavItemViewModel>>(ref uavItemModels, value,
                    "UavItemModels");
            }
        }

        private string areaName;

        public string AreaName
        {
            get { return areaName; }
            set { SetAndNotifyPropertyChanged(ref areaName,value,"AreaName");}
        }

        private string areaID;

        public string AreaID
        {
            get { return areaID; }
            set { SetAndNotifyPropertyChanged(ref areaID,value,"AreaID");}
        }

        private int offLine;

        public int OffLine
        {
            get { return offLine; }
            set { SetAndNotifyPropertyChanged(ref offLine,value,"OffLine");}
        }

        private int onLine;

        public int OnLine
        {
            get { return onLine; }
            set { SetAndNotifyPropertyChanged(ref onLine,value,"OnLine");}
        }
    }
}
