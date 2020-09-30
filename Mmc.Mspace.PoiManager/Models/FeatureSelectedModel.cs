using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class FeatureSelectedModel : BindableBase
    {
        private string _selectedInfo;
        public string SelectedInfo
        {
            get { return _selectedInfo; }
            set { _selectedInfo = value; NotifyPropertyChanged("SelectedInfo"); }
        }
        private int _selectedFID;
        public int SelectedFID
        {
            get { return _selectedFID; }
            set { _selectedFID = value; NotifyPropertyChanged("SelectedFID"); }
        }
        private string _selectedAddress;
        public string SelectedAddress
        {
            get { return _selectedAddress; }
            set { _selectedAddress = value; NotifyPropertyChanged("SelectedAddress"); }
        }
        private string _selectedPrincipal;
        public string SelectedPrincipal
        {
            get { return _selectedPrincipal; }
            set { _selectedPrincipal = value; NotifyPropertyChanged("SelectedPrincipal"); }
        }
        private string _selectedPPhone;
        public string SelectedPPhone
        {
            get { return _selectedPPhone; }
            set { _selectedPPhone = value; NotifyPropertyChanged("SelectedPPhone"); }
        }
        private string _selectedManager;
        public string SelectedManager
        {
            get { return _selectedManager; }
            set { _selectedManager = value; NotifyPropertyChanged("SelectedManager"); }
        }
        private string _selectedMPhone;
        public string SelectedMPhone
        {
            get { return _selectedMPhone; }
            set { _selectedMPhone = value; NotifyPropertyChanged("SelectedMPhone"); }           
        }
    }
}
