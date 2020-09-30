using Mmc.Mspace.Models.User;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.Models.Navigation
{

    public class CameraTourData: INotifyPropertyChanged, IUserInfo
    {

        [LiteDB.BsonIgnore]
        [XmlIgnore]
        public ICommand RenameAnimationNavigationCmd { get; set; }

        [LiteDB.BsonIgnore]
        [XmlIgnore]
        public ICommand RemoveAnimationNavigationCmd { get; set; }
        public string CameraTourID
        {
            get { return _cameraTourID; }
            set
            {
                _cameraTourID = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CameraTourID"));
                }
            }
        }

        public string TourGroupID
        {
            get
            {
                return this._tourGroupID;
            }
            set
            {
                this._tourGroupID = value;
            }
        }

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

        public string LocationName
        {
            get
            {
                return this._nodeName;
            }
            set
            {
                this._nodeName = value;
            }
        }

        public string XmlRoute
        {
            get { return _xmlRoute; }
            set
            {
                _xmlRoute = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("XmlRoute"));
                }
            }
        }

        public string XmlRoad
        {
            get { return _xmlRoad; }
            set
            {
                _xmlRoad = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("XmlRoad"));
                }
            }
        }

        /// <summary>
        /// 修改删除按钮的可见性
        /// </summary>
        private Visibility _navigationOperateVisibility = Visibility.Collapsed;
        public Visibility NavigationOperateVisibility
        {
            get { return _navigationOperateVisibility; }
            set
            {
                _navigationOperateVisibility = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NavigationOperateVisibility"));
                }
            }
        }


        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
        }

        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this._nodeName))
            {
                return string.Empty;
            }
            return this._nodeName;
        }

        private string _cameraTourID;

        private string _tourGroupID;

        private string _nodeName;

        private string _xmlRoute;

        private string _xmlRoad;
        private string _imageSource;

        private int _planId = -1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
