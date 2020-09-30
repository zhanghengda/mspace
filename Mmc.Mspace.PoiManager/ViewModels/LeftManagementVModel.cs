using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LeftManagementVModel : BaseViewModel
    {

        private MapManagementVModel mapManagementVModel;

        public MapManagementVModel MapManagementVModel
        {
            get { return mapManagementVModel ?? (mapManagementVModel = new MapManagementVModel()); }
            set { mapManagementVModel = value; OnPropertyChanged("MapManagementVModel"); }
        }


        private LabelManagementVModel labelManagementVModel;

        public LabelManagementVModel LabelManagementVModel
        {
            get { return labelManagementVModel ?? (labelManagementVModel = new LabelManagementVModel()); }
            set { labelManagementVModel = value; OnPropertyChanged("LabelManagementVModel"); }
        }

        private MarksManagementVModel marksManagementVModel;

        public MarksManagementVModel MarksManagementVModel
        {
            get { return marksManagementVModel ?? (marksManagementVModel = new MarksManagementVModel()); }
            set { marksManagementVModel = value; OnPropertyChanged("MarksManagementVModel"); }
        }

        private AddressManagementVModel _addressManagementVModel;

        public AddressManagementVModel AddressManagementVModel
        {
            get { return _addressManagementVModel ?? (_addressManagementVModel = new AddressManagementVModel()); }
            set { _addressManagementVModel = value; OnPropertyChanged("AddressManagementVModel"); }
        }
        private int _selectedIndex = 0;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                OnSelectCommand(value);
                _selectedIndex = value;
                if(MarksManagementVModel != null&& _selectedIndex!=1)
                {
                    MarksManagementVModel.FilterIsChecked = false;
                }
                OnPropertyChanged("SelectedIndex");
            }
        }

        public LeftManagementVModel()
        {

        }

        protected override void Loaded()
        {
            OnSelectCommand(0);
            base.Loaded();
        }

        private void OnSelectCommand(int index)
        {
            if (index == 1)
                mapManagementVModel.LoadData();
            else if (index == 2)
            {
                if (marksManagementVModel?.MarkerList.Count == 0)
                {
                    marksManagementVModel.LoadLastLabels();
                }
            }
               // marksManagementVModel.LoadData();
            else if (index == 3)
                labelManagementVModel.LoadData();
        }


    
    }
}
