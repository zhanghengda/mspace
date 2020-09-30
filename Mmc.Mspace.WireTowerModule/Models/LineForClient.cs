using Mmc.Mspace.Models.User;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class LineForClient : BindableBase, IUserInfo
    {
        private string _lineName;
        private string _lineSerial;
        private double _lineLength;
        private int _voltageLevel;
        private int _towerCount;

        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public string LineName
        {
            get => _lineName;
            set
            {
                _lineName = value;
                NotifyPropertyChanged("LineName");
            }
        }

        public string LineSerial
        {
            get => _lineSerial;
            set { _lineSerial = value;
                NotifyPropertyChanged("LineSerial");
            }
        }

        public double LineLength
        {
            get => _lineLength;
            set
            {
                _lineLength = value;
                NotifyPropertyChanged("LineLength");
            }
        }
        public int VoltageLevel
        {
            get => _voltageLevel;
            set
            {
                _voltageLevel = value;
                NotifyPropertyChanged("VoltageLevel");
            }
        }
        public int TowerCount
        {
            get => _towerCount;
            set
            {
                _towerCount = value;
                NotifyPropertyChanged("TowerCount");
            }
        }
    }
}
