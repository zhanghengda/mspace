using Gvitech.CityMaker.Math;
using Mmc.Wpf.Mvvm;
using Newtonsoft.Json;
using System;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.WireTowerModule.Models
{
    /// <summary>
    /// 标注点
    /// </summary>
    public class SignModel : BindableBase
    {
        private int _id;
        private int _pid;
        private string _name;
        private string _serial;
        private double _x;
        private double _y;
        private double _z;
        private IEulerAngle _eulerAngle; //欧拉角
        private double _distance;
        private string _type;
        private double _pitchAngle;
        private double _trendAngle;

        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("id");
            }
        }

        public int pid
        {
            get { return _pid; }
            set
            {
                _pid = value;
                NotifyPropertyChanged("pid");
            }
        }

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("name");
            }
        }

        public string serial
        {
            get { return _serial; }
            set
            {
                _serial = value;
                NotifyPropertyChanged("serial");
            }
        }

        public double X
        {
            get { return _x; }
            set
            {
                _x = Math.Round(value, 8);
                NotifyPropertyChanged("X");
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = Math.Round(value, 8);
                NotifyPropertyChanged("Y");
            }
        }

        public double Z
        {
            get { return _z; }
            set
            {
                _z = Math.Round(value, 2);
                NotifyPropertyChanged("Z");
            }
        }

        [JsonIgnore]
        public IEulerAngle eulerAngle
        {
            get { return _eulerAngle; }
            set
            {
                _eulerAngle = value;
                NotifyPropertyChanged("eulerAngle");
            }
        }

        public double speDistance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                NotifyPropertyChanged("speDistance");
            }
        }

        public string type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged("type");
            }
        }

        public double pitchAngle
        {
            get { return _pitchAngle; }
            set
            {
                if (value > 90 || value < -90)
                {
                    _pitchAngle = 0;
                }
                else
                {
                    _pitchAngle = value;
                }

                NotifyPropertyChanged("pitchAngle");
            }
        }


        public double trendAngle
        {
            get { return _trendAngle; }
            set
            {
                if (value > 90 || value < -90)
                {
                    _trendAngle = 0;
                }
                else
                {
                    _trendAngle = value;
                }

                NotifyPropertyChanged("trendAngle");
            }
        }
    }
}