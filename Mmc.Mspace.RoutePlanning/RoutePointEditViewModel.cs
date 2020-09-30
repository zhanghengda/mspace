using Mmc.Mspace.Common.Models;
using Mmc.Windows.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.RoutePlanning
{
    public delegate void Update(double height);
    public delegate void FlyToPoint(int index);
    public delegate void DeleteRoutePoint(bool type);
    public delegate void UpdateRoutePoint(string type, double value);
    public delegate void AddPointInToList();
    public class RoutePointEditViewModel : CheckedToolItemModel
    {
        RoutePointEditView _routePointEditView;
        public Update update;
        public FlyToPoint flyToPoint;
        public DeleteRoutePoint deleteRoutePoint;
        public UpdateRoutePoint updateRoutePoint;
        public AddPointInToList addpointIntoList;
        public Action OnCloseEvent;

        public bool isEditing;
        string _lng;
        string _lat;
        string _height;
        string _speed;
        string _hover;
        string _trigger;
        bool _cameraTrigger;
        int _pointCount;
        private int _pointIndex = 1;
        string _lngLatBtnContent;
        public bool isSaved = false;

        public int CurrentIndex = 1;
        
        double _Lng;
        double _Lat;

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand cmdCloseWindow { get; set; }
        /// <summary>
        /// 上一个航点
        /// </summary>
        public ICommand cmdPrePoint { get; set; }

        /// <summary>
        /// 下一个航线
        /// </summary>
        public ICommand cmdNextPoint { get; set; }

        /// <summary>
        /// 删除航点
        /// </summary>
        public ICommand cmdDeleteRoutePoint { get; set; }

        /// <summary>
        /// 替换经纬度
        /// </summary>
        public ICommand cmdChangeLngLat { get; set; }
        public ICommand AddPointCmd { get; set; }
        public ICommand SaveAllCmd { get; set; }
        public ICommand ReSetDataCmd { get; set; }
        //public ICommand HeightFocusCmd { get; set; }
        
        private string _templng;
        private string _templat;
        private string _tempheight;
        private string _tempspeed;
        private string _temphover;
        private string _temptrigger;
        public override void Initialize()
        {
            base.Initialize();
            cmdCloseWindow = new RelayCommand(() => {
                releaseWindow();
            });
            cmdPrePoint = new RelayCommand(() => {
                if (WeatherIsChanged() && isSaved == false)
                {
                    var _alreadySave = Messages.ShowMessageDialog("保存", "尚未保存数据，是否返回修改");
                    if (_alreadySave)
                    {

                    }
                    else
                    {
                        if (SelectPointIndex > 1)
                        {
                            CurrentIndex = SelectPointIndex;
                            SelectPointIndex--;
                            SetValueAndState();
                           
                        }
                       
                    }
                }
                else
                {
                    if (SelectPointIndex > 1)
                    {
                        CurrentIndex = SelectPointIndex;
                        SelectPointIndex--;
                        SetValueAndState();
                    }
                  
                }
            });
            cmdNextPoint = new RelayCommand(() => {
                if (WeatherIsChanged()&&isSaved==false)
                {
                    var _alreadySave = Messages.ShowMessageDialog("保存", "尚未保存数据，是否返回修改");
                    if (_alreadySave)
                    {

                    }
                    else
                    {
                        if (SelectPointIndex < SelectPointCount)
                        {
                            CurrentIndex = SelectPointIndex;
                            SelectPointIndex++;
                            SetValueAndState();
                        }
                    }
                }
                else {
                    if (SelectPointIndex < SelectPointCount)
                    {
                        CurrentIndex = SelectPointIndex;
                        SelectPointIndex++;
                        SetValueAndState();
                    }
                }
       
            });
            cmdDeleteRoutePoint = new RelayCommand(() =>
            {
               var dr = Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Warn"), Helpers.ResourceHelper.FindKey("DeleteRoutePoint"));
                if (dr)
                {
                    deleteRoutePoint(true);
                }
            });
            cmdChangeLngLat = new RelayCommand(() =>
            {
                if (_lngLatBtnContent == "°′″")
                {
                   
                    _lngLatBtnContent = "°";
                    _routePointEditView.Id_LngLatBtn.Content = _lngLatBtnContent;
                }
                else
                {
                    _lngLatBtnContent = "°′″";
                    _routePointEditView.Id_LngLatBtn.Content = _lngLatBtnContent;
                }
                ChangeLngLat();
            });
            AddPointCmd = new RelayCommand(() =>
            {
                addpointIntoList();
            });
            SaveAllCmd = new RelayCommand(() =>
            {
                SaveAll();
            });
            ReSetDataCmd = new RelayCommand(() =>
            {
                ReSet();
            });
            //HeightFocusCmd = new RelayCommand(() =>
            //     {

            //     });
          
          
        }
        private void HeightFocusCmd()
        {
            Messages.ShowMessage("focus");
        }
        /// <summary>
        /// 浮点数判断
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool isFloat(string val)
        {
            if (val == null) return false;
            Regex reg = new Regex(@"^(-?\d+)(\.\d+)?$");
            return reg.Match(val).Success;
        }
        private bool isChange = false;
        private void ChangeLngLat()
        {
            if (_lngLatBtnContent == "°′″")
            {

                SelectLng = ConvertDigitalToDegrees(_Lng).ToString();
                SelectLat = ConvertDigitalToDegrees(_Lat).ToString();
            }
            else
            {
                SelectLng = Math.Round(_Lng, 6).ToString();
                SelectLat = Math.Round(_Lat, 6).ToString();
            }
        }

        #region 度分秒转换
        static public string ConvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + Math.Round(second,4) + "″";
            return degrees;
        }

        static public double ConvertDegreesToDigital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');           //度的符号对应的 Unicode 代码为：00B0[1]（六十进制），显示为°。
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');           //分的符号对应的 Unicode 代码为：2032[1]（六十进制），显示为′。
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');           //秒的符号对应的 Unicode 代码为：2033[1]（六十进制），显示为″。
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }
      #endregion

        public override void OnChecked()
        {
            if (_routePointEditView == null)
            {
                isEditing = false;
                _routePointEditView = new RoutePointEditView();
                _routePointEditView.Owner = Application.Current.MainWindow;
                _routePointEditView.Closed += (sender, e) =>
                {
                    _routePointEditView = null;
                };

                _routePointEditView.DataContext = this;
            }
            if (!_routePointEditView.IsVisible)
            {
                _routePointEditView.WindowStartupLocation = WindowStartupLocation.Manual;
                _routePointEditView.Left = Application.Current.MainWindow.Left + 74;
                _routePointEditView.Top = 0;
                _routePointEditView.Show();
            }
            IsSelected = true;
            SetValueAndState();
        }

        public override void OnUnchecked()
        {
            OnCloseEvent?.Invoke();
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="height"></param>
        /// <param name="speed"></param>
        /// <param name="hover"></param>
        /// <param name="trigger"></param>
        /// <param name="cameraTrigger"></param>
        public void SetParameter(double lng, double lat, double height, double speed, double hover, double trigger, bool cameraTrigger)
        {
            _Lng = lng;
            _Lat = lat;
            ChangeLngLat();
            SelectHeight = Math.Round(height,2).ToString();
            SelectSpeed = (speed >= 0 ? speed.ToString() : SelectSpeed);
            SelectHover = (hover >= 0 ? hover.ToString() : SelectHover);
            SelectTrigger = (trigger >= 0 ? trigger.ToString() : SelectTrigger);
            SelectCameraTrigger = cameraTrigger;
        }

        /// <summary>
        /// 经度绑定
        /// </summary>
        public string SelectLng
        {
            get
            {
                return _lng;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _lng, value, "SelectLng");
                if (_saveAuto)
                {
                    updateRoutePoint("lng", _Lng);
                }
                isSaved = true;
            }
        }

        /// <summary>
        /// 纬度绑定
        /// </summary>
        public string SelectLat
        {
            get
            {

               return _lat;

            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _lat, value, "SelectLat");
                if (_saveAuto)
                {
                    updateRoutePoint("lat", _Lat);
                }
                isSaved = true;
            }
        }
        private bool _saveAuto = true;
        public bool SaveAuto
        {
            get { return this._saveAuto; }
            set { _saveAuto = value;
                SaveAutoEnable = !_saveAuto;
                NotifyPropertyChanged("SaveAuto"); }
        }

        private bool _saveAutoEnable;
        public bool SaveAutoEnable
        {
            get { return this._saveAutoEnable; }
            set { _saveAutoEnable = value; NotifyPropertyChanged("SaveAutoEnable"); }
        }

        private bool _isChangeAll;
        public bool IsChangeAll
        {
            get { return _isChangeAll; }
            set { _isChangeAll = value;
                NotifyPropertyChanged("IsChangeAll");
            }
        } 

        /// <summary>
        /// 高度绑定
        /// </summary>
        public string SelectHeight
        {
            get
            {
                return _height;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _height, value, "SelectHeight");
                if (_saveAuto)
                {
                    if (isFloat(SelectHeight))
                    {
                        isEditing = true;
                        update(double.Parse(SelectHeight));
                        updateRoutePoint("height", double.Parse(SelectHeight));
                        isEditing = false;
                    }
                    isSaved = true;
                }

            }
        }

        /// <summary>
        /// 速度绑定
        /// </summary>
        public string SelectSpeed
        {
            get { return _speed; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _speed, value, "SelectSpeed");
                if (_saveAuto)
                {
                    if (isFloat(SelectSpeed))
                    {
                        updateRoutePoint("speed", double.Parse(SelectSpeed));
                    }
                }
                isSaved = true;
            }
        }

        /// <summary>
        /// 悬停绑定
        /// </summary>
        public string SelectHover
        {
            get { return _hover; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _hover, value, "SelectHover");

                if (_saveAuto)
                {
                    if (isFloat(SelectHover))
                    {
                        updateRoutePoint("hover", double.Parse(SelectHover));
                    }
                }
                 isSaved = true;
            }
        }

        /// <summary>
        /// 触发绑定
        /// </summary>
        public string SelectTrigger
        {
            get { return _trigger; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref _trigger, value, "SelectTrigger");
                if (_saveAuto)
                {
                    if (isFloat(SelectTrigger))
                    {
                        updateRoutePoint("trigger", double.Parse(SelectTrigger));
                    }
                }
                 isSaved = true;
            }
        }

        /// <summary>
        /// 相机触发绑定
        /// </summary>
        public bool SelectCameraTrigger
        {
            get { return _cameraTrigger; }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref _cameraTrigger, value, "SelectCameraTrigger");
            }
        }

        /// <summary>
        /// 航点总数绑定
        /// </summary>
        public int SelectPointCount
        {
            get { return _pointCount; }
            set
            {
                base.SetAndNotifyPropertyChanged<int>(ref _pointCount, value, "SelectPointCount");
            }
        }

        /// <summary>
        /// 航点位序绑定
        /// </summary>
        public int SelectPointIndex
        {
            get { return _pointIndex; }
            set
            {
                base.SetAndNotifyPropertyChanged<int>(ref _pointIndex, value, "SelectPointIndex");
                flyToPoint(SelectPointIndex - 1);
                
            }
        }


        /// <summary>
        /// 关闭船体
        /// </summary>
        public void releaseWindow()
        {
            IsSelected = false;
            this.OnUnchecked();
            _routePointEditView?.Hide();
            _routePointEditView = null;
            Console.WriteLine("-----------CloseWindow");
        }
        private void SaveAll()
        {
            if (WeatherIsChanged()||isSaved==false)
            { 
            var _save = Messages.ShowMessageDialog("保存", "是否保存更改");
                if (_save)
                {
                   
                    if (isFloat(SelectHeight)&& isFloat(SelectSpeed)&& isFloat(SelectHover)&& isFloat(SelectTrigger)&& isFloat(SelectTrigger))
                    {
                        updateRoutePoint("lng", _Lng);
                        updateRoutePoint("lat", _Lat);
                        isEditing = true;
                        update(double.Parse(SelectHeight));
                        updateRoutePoint("height", double.Parse(SelectHeight));
                        isEditing = false;
                        updateRoutePoint("speed", double.Parse(SelectSpeed));
                        updateRoutePoint("hover", double.Parse(SelectHover));
                        updateRoutePoint("trigger", double.Parse(SelectTrigger));
                        isChange = false;
                        isSaved = true;
                    }
                    else
                    {
                        Messages.ShowMessage("保存失败，请检查数据");   
                    }
                }
                else {
                   
                    Messages.ShowMessage("用户取消保存");
                }
            }
        }
        private void ReSet()
        {
             SelectLng = _templng;
             SelectLat = _templat;
             SelectHeight = _tempheight;
             SelectSpeed = _tempspeed;
             SelectHover =_temphover;
             SelectTrigger =_temptrigger;
             isChange = false;
             isSaved = false;
        }
        public bool WeatherIsChanged()
        {
            if (SelectLng == _templng && SelectLat == _templat && SelectHeight == _tempheight && SelectHover == _temphover && SelectTrigger == _temptrigger && SelectSpeed == _tempspeed)
            {
                isChange = false;
            }
            else
            {
                isChange = true;
            }
            return isChange;
               
        }
        public void SetValueAndState()
        {
            _templng = SelectLng;
            _templat = SelectLat;
            _tempheight = SelectHeight;
            _tempspeed = SelectSpeed;
            _temphover = SelectHover;
            _temptrigger = SelectTrigger;
            isChange = false;
            isSaved = false;
        }
      
    }
}
