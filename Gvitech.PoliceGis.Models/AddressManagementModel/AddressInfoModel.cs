using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Mmc.Mspace.Models.AddressManagementModel
{
    public class AddressInfoModel : INotifyPropertyChanged, IUserInfo
    {
        [LiteDB.BsonIgnore]
        public AddressInfoModel()
        {
            this.ChlidList = new ObservableCollection<AddressInfoModel>();
            this.AddressParentNode = new ObservableCollection<AddressInfoModel>();
        }
        [LiteDB.BsonIgnore]
        [XmlIgnore]
        private ObservableCollection<AddressInfoModel> _chlidList;
        [LiteDB.BsonIgnore]
        [XmlIgnore]
        public ObservableCollection<AddressInfoModel> ChlidList
        {
            get { return _chlidList; }
            set
            {
                _chlidList = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ChlidList"));
                }
            }
        }
        
        private int _addressLevel;

        public int AddressLevel
        {
            get { return _addressLevel; }
            set
            {
                _addressLevel = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AddressLevel"));
                }
            }
        }

        private string _addressId;
        public string AddressId
        {
            get { return _addressId; }
            set
            {
                _addressId = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AddressId"));
                }
            }
        }
        [LiteDB.BsonIgnore]
        [XmlIgnore]
        private ObservableCollection<AddressInfoModel> _addressParentNode;
        [LiteDB.BsonIgnore]
        [XmlIgnore]
        public ObservableCollection<AddressInfoModel> AddressParentNode
        {
            get { return _addressParentNode; }
            set
            {
                _addressParentNode = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AddressParentNode"));
                }
            }
        }

        private string _nodeName;
        public string NodeName
        {
            get { return _nodeName; }
            set
            {
                _nodeName = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NodeName"));
                }
            }
        }

        private Visibility _addBtnVisibility = Visibility.Visible;
        public Visibility AddBtnVisibility
        {
            get { return _addBtnVisibility; }
            set
            {
                _addBtnVisibility = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AddBtnVisibility"));
                }
            }
        }

        private string _parentId;
        public string ParentId
        {
            get { return _parentId; }
            set
            {
                _parentId = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ParentId"));
                }
            }
        }

        private string _parentName;
        public string ParentName
        {
            get { return _parentName; }
            set
            {
                _parentName = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ParentName"));
                }
            }
        }
        private string _stringPosition;
        public string StringPosition
        {
            get { return _stringPosition; }
            set
            {
                _stringPosition = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StringPosition"));
                }
            }
        }
        private string _stringAngle;
        public string StringAngle
        {
            get { return _stringAngle; }
            set
            {
                _stringAngle = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StringAngle"));
                }
            }
        }

        public string UserName { get; set ; }
        public int Id { get; set ; }
        public int IsAdministrator { get ; set ; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
