using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class AddressInfoModel : BaseViewModel
    {
        public AddressInfoModel()
        {
            this.ChlidList = new ObservableCollection<AddressInfoModel>();
        }

        private ObservableCollection<AddressInfoModel> _chlidList;
        public ObservableCollection<AddressInfoModel> ChlidList
        {
            get { return _chlidList; }
            set
            {
                _chlidList = value;
                OnPropertyChanged("ChlidList");
            }
        }

        private int _addressLevel;
        public int AddressLevel
        {
            get { return _addressLevel; }
            set
            {
                _addressLevel = value;
                OnPropertyChanged("AddressLevel");
            }
        }

        private string _addressId;
        public string AddressId
        {
            get { return _addressId; }
            set
            {
                _addressId = value;
                OnPropertyChanged("AddressId");
            }
        }
        private string _nodeName;
        public string NodeName
        {
            get { return _nodeName; }
            set
            {
                _nodeName = value;
                OnPropertyChanged("NodeName");
            }
        }

        private Visibility _addBtnVisibility=Visibility.Visible;
        public Visibility AddBtnVisibility
        {
            get { return _addBtnVisibility; }
            set
            {
                _addBtnVisibility = value;
                OnPropertyChanged("AddBtnVisibility");
            }
        }
        
        private string _parentId;
        public string ParentId
        {
            get { return _parentId; }
            set
            {
                _parentId = value;
                OnPropertyChanged("ParentId");
            }
        }

        private string _parentName;
        public string ParentName
        {
            get { return _parentName; }
            set
            {
                _parentName = value;
                OnPropertyChanged("ParentName");
            }
        }
        private string _stringPosition;
        public string StringPosition
        {
            get { return _stringPosition; }
            set
            {
                _stringPosition = value;
                OnPropertyChanged("StringPosition");
            }
        }
        private string _stringAngle;
        public string StringAngle
        {
            get { return _stringAngle; }
            set
            {
                _stringAngle = value;
                OnPropertyChanged("StringAngle");
            }
        }

    }
}
