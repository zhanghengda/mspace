using Gvitech.CityMaker.Math;
using Mmc.Wpf.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class TowerForClient : BindableBase
    {
        private int _id ;
        private int _pid ;
        private string _name ;
        private string _serial ;
        private double _x ;
        private double _y ;
        private double _z ;
        private string _towerType ;
        private ObservableCollection<SignModel> _signList;
        private IVector3 _crossVotor ;
        private IVector3 _extendVotor ;
        private string _leftLine;
        private string _rightLine;
        private bool _isSelected;

        private double _safeDistance = 5;

        private double _relativeHeight;
        //private double[] _maxLeftPonit;
        //private double[] _maxRightPoint;

        public double SafeDistance
        {
            get { return _safeDistance; }
            set
            {
                if (value < 5) _safeDistance = 5;
                else _safeDistance = value;
                NotifyPropertyChanged("SafeDistance");
            }
        } 


        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; NotifyPropertyChanged("Pid"); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        public string Serial
        {
            get { return _serial; }
            set { _serial = value; NotifyPropertyChanged("Serial"); }
        }
        public double X
        {
            get { return _x; }
            set { _x = Math.Round(value, 8); ; NotifyPropertyChanged("X"); }
        }
        public double Y
        {
            get { return _y; }
            set { _y = Math.Round(value, 8); ; NotifyPropertyChanged("Y"); }
        }
        public double Z
        {
            get { return _z; }
            set { _z = Math.Round(value, 2); NotifyPropertyChanged("Z"); }
        }
        public string TowerType
        {
            get { return _towerType; }
            set { _towerType = value; NotifyPropertyChanged("TowerType"); }
        }
        public ObservableCollection<SignModel> SignList
        {
            get { return _signList?? new ObservableCollection<SignModel>(); }
            set { _signList = value; NotifyPropertyChanged("SignList"); }
        }
        public IVector3 CrossVotor
        {
            get { return _crossVotor; }
            set { _crossVotor = value; NotifyPropertyChanged("CrossVotor"); }
        }
        public IVector3 ExtendVotor
        {
            get { return _extendVotor; }
            set { _extendVotor = value; NotifyPropertyChanged("ExtendVotor"); }
        }

        public string LeftLine
        {
            get { return _leftLine; }
            set { _leftLine = value; NotifyPropertyChanged("LeftLine"); }
        }

        public string RightLine
        {
            get { return _rightLine; }
            set { _rightLine = value; NotifyPropertyChanged("RightLine"); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }

        public double RelativeHeight
        {
            get { return _relativeHeight; }
            set { _relativeHeight = value; NotifyPropertyChanged("RelativeHeight"); }
        }

        public double[] MaxLeftPoint { get; set; } = new double[2] { 0, 0 };
        public double[] MaxRightPoint { get; set; } = new double[2] { 0, 0 };
    }
}
