using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.UavTracing
{
    //无人机展示信息的VM
    public class UavTraceVModel : BindableBase
    {

        #region 私有变量 属性
        //分类
        public int goods_id { get; set; }
        public int cat_id { get; set; }
        //属性
        public string ground_serial { get; set; }
        public string unmanned_type { get; set; }
        public string mount_type { get; set; }
        //视频流地址
        public string rtmp_url { get; set; }
        //卫星数
        public double sat_count { get; set; }
        //飞行模式
        public string flight_mode { get; set; }
        //此次飞行架次标识
        public string flight_sortie { get; set; }

        //速度
        private double _speed;
        [XmlIgnore]
        public double Speed
        {
            get { return this._speed; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._speed, value, "Speed"); }
        }

        //爬升率
        private string _climb_rate;
        [XmlIgnore]
        public string Climb_rate
        {
            get { return this._climb_rate; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._climb_rate, value, "Climb_rate"); }
        }

        //电压
        private string _voltage;
        [XmlIgnore]
        public string Voltage
        {
            get { return this._voltage; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._voltage, value, "Voltage");
            }
        }

        private string _maxVoltage = "36";

        public string MaxVoltage
        {
            get { return _maxVoltage; }
            set
            {
                SetAndNotifyPropertyChanged(ref _maxVoltage, value);
                double d = double.Parse(_maxVoltage);
                WarningVoltage = (d * 0.65).ToString();
            }
        }

        private string _warningVoltage;
        public string WarningVoltage
        {
            get { return _warningVoltage ; }
            set { SetAndNotifyPropertyChanged(ref _warningVoltage,value); }
        } 

        //离家距离
        private string _distance_to_home;
        [XmlIgnore]
        public string Distance_to_home
        {
            get { return this._distance_to_home; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._distance_to_home, value, "Distance_to_home"); }
        }

        //电流
        private double _current;
        [XmlIgnore]
        public double Current
        {
            get { return this._current; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._current, value, "Current"); }
        }


        //本次飞行时长
        private string _flight_time;
        [XmlIgnore]
        public string Flight_time
        {
            get { return this._flight_time; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._flight_time, value, "Flight_time");
                double d = double.Parse(_flight_time);
                d /= 60;
                Flight_time_min = d.ToString("N0");
            }
        }

        private string  _flight_time_min;
        public string  Flight_time_min
        {
            get { return _flight_time_min; }
            set { SetAndNotifyPropertyChanged(ref _flight_time_min, value); }
        } 



        //剩余电压
        private double _battary_remain;
        [XmlIgnore]
        public double Battary_remain
        {
            get { return this._battary_remain; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._battary_remain, value, "Battary_remain"); }
        }


        //对地速度
        private double _grondSpeed;
        [XmlIgnore]
        public double Ground_speed
        {
            get { return this._grondSpeed; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._grondSpeed, value, "Ground_speed"); }
        }


        //对空速度
        private double _air_speed;
        [XmlIgnore]
        public double Air_speed
        {
            get { return this._air_speed; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._air_speed, value, "Air_speed"); }
        }


        //距离下一个点距离,下一任务点
        private string _distance_next;
        [XmlIgnore]
        public string Distance_next
        {
            get { return this._distance_next; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._distance_next, value, "Distance_next"); }
        }

        #region 姿态角
        private double _heading;
        [XmlIgnore]
        public double Heading
        {
            get { return this._heading; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._heading, value, "Heading"); }
        }

        private double _pitch;
        [XmlIgnore]
        public double Pitch
        {
            get { return this._pitch; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._pitch, value, "Pitch"); }
        }

        private double _roll;
        [XmlIgnore]
        public double Roll
        {
            get { return this._roll; }
            set { base.SetAndNotifyPropertyChanged<double>(ref this._roll, value, "Roll"); }
        }
        #endregion

        #region 地理信息

        //海拔高度
        private double _altitude;
        [XmlIgnore]
        public double Altitude
        {
            get { return this._altitude; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._altitude, value, "Altitude");
                AltitudeKm = Altitude * 0.001;
            }
        }

        private double _altitudeKm;
        public double AltitudeKm
        {
            get { return _altitudeKm; }
            set { SetAndNotifyPropertyChanged(ref _altitudeKm,value); }
        } 

        public double longitude { get; set; }
        public double latitude { get; set; }

        //相对高度
        private double _height;
        [XmlIgnore]
        public double Height
        {
            get { return this._height; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._height, value, "Height");
                HeightKm = Height * 0.001;
            }
        }

        private double _heightKm;
        public double HeightKm
        {
            get { return _heightKm; }
            set { SetAndNotifyPropertyChanged(ref _heightKm, value); }
        } 
        #endregion

        #endregion

        public static void ToUavInfoVM(UavTrace trace, UavTraceVModel uavInfoVM)
        {
            uavInfoVM.goods_id = trace.goods_id;
            uavInfoVM.cat_id = trace.cat_id;
            uavInfoVM.unmanned_type = trace.unmanned_type;
            uavInfoVM.ground_serial = trace.ground_serial;
            uavInfoVM.mount_type = trace.mount_type;
            uavInfoVM.longitude = Math.Round(Convert.ToDouble(trace?.longitude), 1); 
            uavInfoVM.latitude = Math.Round(Convert.ToDouble(trace?.latitude), 1); 
            uavInfoVM.Altitude = Math.Round(Convert.ToDouble(trace?.altitude), 1);
            uavInfoVM.Height = Math.Round(Convert.ToDouble(trace?.height), 1); 
            uavInfoVM.Roll = Math.Round(Convert.ToDouble(trace?.roll), 1);
            uavInfoVM.Pitch = Math.Round(Convert.ToDouble(trace?.pitch), 1);
            uavInfoVM.Heading = Math.Round(Convert.ToDouble(trace?.yaw), 1); 
            uavInfoVM.Speed = Math.Round(Convert.ToDouble(trace?.speed), 1);
            uavInfoVM.rtmp_url = trace.rtmp_url;
            uavInfoVM.Voltage = Convert.ToDouble(trace?.voltage).ToString("0.0");
            uavInfoVM.Climb_rate = Convert.ToDouble(trace?.climb_rate).ToString("0.0");
            uavInfoVM.Distance_to_home = Convert.ToDouble(trace?.distance_to_home).ToString("0.0");
            uavInfoVM.Flight_time = Convert.ToDouble(trace?.flight_time).ToString("0.0");
            uavInfoVM.Current = Math.Round(Convert.ToDouble(trace?.current), 1); 
            uavInfoVM.Battary_remain = Math.Round(Convert.ToDouble(trace?.battary_remain), 1); 
            uavInfoVM.Ground_speed = Math.Round(Convert.ToDouble(trace?.ground_speed), 1); 
            uavInfoVM.Air_speed = Math.Round(Convert.ToDouble(trace?.air_speed), 1);
            uavInfoVM.sat_count = Math.Round(Convert.ToDouble(trace?.sat_count), 0);
            uavInfoVM.Distance_next = Math.Round(Convert.ToDouble(trace?.distance_next), 1).ToString();
            uavInfoVM.flight_mode = trace?.flight_mode;
            uavInfoVM.flight_sortie = trace?.flight_sortie;
        }
    }
}
