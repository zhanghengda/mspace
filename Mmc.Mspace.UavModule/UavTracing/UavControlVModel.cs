﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Common.Models;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Mspace.UavModule.Dto;
using Mmc.Mspace.Theme.Pop;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavControlVModel : CheckedToolItemModel
    {
        private UavControlView _controlView;

        Thread waterDataThread;
        ObservableCollection<CombRouteListModel> routeInfos;
        private string _sendMessage = "";
        private readonly TimeSpan delay = TimeSpan.FromMilliseconds(5);
        private string _takeoffHeight = "2";
        private int _sendVehicleRouteId = 0;

        public string mount_type { get; set; }
        public int goods_id { get; set; }
        public string ground_state { get; set; }
        public bool isSocketConnected { get; set; }

        //Gas
        private List<GasInfo> _gasModel;

        public List<GasInfo> GasListModel
        {
            get { return this._gasModel; }
            set { base.SetAndNotifyPropertyChanged<List<GasInfo>>(ref this._gasModel, value, "GasListModel"); }
        }

        #region socket real time value
        //camera
        private int _mountType;
        private string _camCamPitchAngle;
        private string _camCamHeadAngle;
        private string _camCamRoolAngle;
        private string _camCamZoom;

        //ship
        private string _shipCtValue;
        private string _shipDoValue;
        private string _shipPhValue;
        private string _shipTempValue;
        private string _shipTurValue;

        //radio
        private bool _isRadioLockChecked;
        private bool _isRadioControlChecked;
        private bool _isRadioResetChecked;

        #endregion 
        //无人机信息
        private UavTraceVModel _uavTraceVM;
        [XmlIgnore]
        public UavTraceVModel UavTraceVM
        {
            get { return this._uavTraceVM; }
            set { base.SetAndNotifyPropertyChanged<UavTraceVModel>(ref this._uavTraceVM, value, "UavTraceVM"); }
        }


        public Action<string, int, string> OnTimeUpdated;

        private string _ground_serial;
        [XmlIgnore]
        public string ground_serial
        {
            get { return this._ground_serial; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._ground_serial, value, "ground_serial"); this.VehicleTile = this._ground_serial; }
        }

        private string _vehicleTile;
        [XmlIgnore]
        public string VehicleTile
        {
            get { return this._vehicleTile + " 无人机 "; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._vehicleTile, value, "VehicleTile"); }
        }

        private string _mountControlTile;
        [XmlIgnore]
        public string MountControlTile
        {
            //string camName = string.IsNullOrEmpty(mount_name) ? "" : mount_name + "-"; 
            get { return this._mountControlTile; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._mountControlTile, value, "MountControlTile"); }
        }

        /// <summary>
        /// 摇杆类型名称
        /// </summary>
        private string _joyTypeText;
        public string JoyTypeText
        {
            get { return this._joyTypeText; }
            set { _joyTypeText = value; NotifyPropertyChanged("JoyTypeText"); MountControlTile = (string.IsNullOrEmpty(mount_name) ? "" : mount_name + "-") + "云台控制(" + this._joyTypeText + ")"; }
        }

        [XmlIgnore]
        public int mountType
        {
            get { return this._mountType; }
            set { base.SetAndNotifyPropertyChanged<int>(ref this._mountType, value, "mountType"); mount_name = TypeEnum.getMountName(_mountType); }
        }

        //Mount
        private string _mount_name;
        public string mount_name
        {
            get { return this._mount_name; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._mount_name, value, "mount_name"); MountControlTile = (string.IsNullOrEmpty(mount_name) ? "" : mount_name + "-") + "云台控制(" + this._joyTypeText + ")"; }
        }

        #region camera
        [XmlIgnore]
        public string camCamPitchAngle
        {
            get { return this._camCamPitchAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamPitchAngle, value, "camCamPitchAngle"); }
        }
        [XmlIgnore]
        public string camCamHeadAngle
        {
            get { return this._camCamHeadAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamHeadAngle, value, "camCamHeadAngle"); }
        }
        [XmlIgnore]
        public string camCamRoolAngle
        {
            get { return this._camCamRoolAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamRoolAngle, value, "camCamRoolAngle"); }
        }
        [XmlIgnore]
        public string camCamZoom
        {
            get { return this._camCamZoom; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamZoom, value, "camCamZoom"); }
        }
        #endregion

        #region radio
        [XmlIgnore]
        public bool isRadioLockChecked
        {
            get { return this._isRadioLockChecked; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isRadioLockChecked, value, "isRadioLockChecked"); }
        }

        [XmlIgnore]
        public bool isRadioControlChecked
        {
            get { return this._isRadioControlChecked; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isRadioControlChecked, value, "isRadioControlChecked"); }
        }
        [XmlIgnore]
        public bool isRadioResetChecked
        {
            get { return this._isRadioResetChecked; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isRadioResetChecked, value, "isRadioResetChecked"); }
        }

        #endregion

        #region ship
        [XmlIgnore]
        public string shipCtValue
        {
            get { return this._shipCtValue; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._shipCtValue, value, "shipCtValue"); }
        }

        [XmlIgnore]
        public string shipDoValue
        {
            get { return this._shipDoValue; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._shipDoValue, value, "shipDoValue"); }
        }

        [XmlIgnore]
        public string shipPhValue
        {
            get { return this._shipPhValue; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._shipPhValue, value, "shipPhValue"); }
        }

        [XmlIgnore]
        public string shipTempValue
        {
            get { return this._shipTempValue; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._shipTempValue, value, "shipTempValue"); }
        }

        [XmlIgnore]
        public string shipTurValue
        {
            get { return this._shipTurValue; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._shipTurValue, value, "shipTurValue"); }
        }

        #endregion

        #region 仪表盘
        private ChartValues<double> _lastTenSecSeries;
        [XmlIgnore]
        public ChartValues<double> LastTenSecSeries
        {
            get { return this._lastTenSecSeries; }
            set { base.SetAndNotifyPropertyChanged<ChartValues<double>>(ref this._lastTenSecSeries, value, "SeriesCollection"); }
        }
        private void initLastHourSeries()
        {
            LastTenSecSeries = new ChartValues<double> { -10, -5, 0, 5, 10, 15 };
        }

        #endregion

        #region Command
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }
        [XmlIgnore]
        public ICommand ButtonRequest { get; set; }
        [XmlIgnore]
        public ICommand CamPitchUp { get; set; }
        [XmlIgnore]
        public ICommand CamPitchDown { get; set; }
        [XmlIgnore]
        public ICommand CamHeadLeft { get; set; }
        [XmlIgnore]
        public ICommand CamHeadRight { get; set; }
        [XmlIgnore]
        public ICommand CamZoomOut { get; set; }
        [XmlIgnore]
        public ICommand CamZoomStop { get; set; }
        [XmlIgnore]
        public ICommand CamZoomIn { get; set; }
        [XmlIgnore]
        public ICommand CamCamCapture { get; set; }
        [XmlIgnore]
        public ICommand CamCamVideo { get; set; }
        [XmlIgnore]
        public ICommand GasPitchUp { get; set; }
        [XmlIgnore]
        public ICommand GasPitchDown { get; set; }
        [XmlIgnore]
        public ICommand cmdPanoramic { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyArm { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyLocked { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyLand { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyReturnModel { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyLoadRoute { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyTakeoff { get; set; }
        [XmlIgnore]
        public ICommand cmdJoyAutoModel { get; set; }
        [XmlIgnore]
        public ICommand cmdJoySendRoute { get; set; }
        [XmlIgnore]
        public ICommand DropDump { get; set; }

        [XmlIgnore]
        public ICommand UseJoysticApp { get; set; }
        #endregion





        //释放窗体
        public void releaseWindow()
        {
            if (waterDataThread != null)
            {
                waterDataThread.Abort();
                while (waterDataThread.ThreadState != System.Threading.ThreadState.Aborted)
                {
                    Thread.Sleep(100);
                }
            }
            _controlView?.Close();
            _controlView = null;
        }
        private Window _shellView;

        public string WindowTitle { get; set; }
        public override void OnChecked()
        {
            JoyTypeText = "暂无摇杆手柄接入";
            //Console.WriteLine(" mountcontroller window : " + ground_serial);
            initLastHourSeries();
            ShowWindow();
            //if (ground_state == "1")
            //{
            base.OnChecked();
            ToDo();
            //}
            //else
            //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceNotOnline"));
        }

        private void ShowWindow()
        {
            _shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            WindowTitle = Helpers.ResourceHelper.FindKey("RemoteControl");
            if (_controlView == null)
                _controlView = new UavControlView();
            this._controlView.Left = _shellView.Left;
            this._controlView.Top = _shellView.Height - 256;
            this._controlView.Width = _shellView.Width;
            this._controlView.Height = 256;
            this._controlView.Owner = Application.Current.MainWindow;

            _controlView.DataContext = this;
            _controlView.Show();
        }

        private void ToDo()
        {
            //等待挂载设备的接入            
            if (mount_type == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoControlretry"));
                return;
            }
            else
            {
                //if (isSocketConnected)
                //{
                //reQuest
                var cmdData = cmdRequest();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("ButtonRequest:" + _sendMessage);

                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }

                selectRouteComboBox("");//show combobox list

                //Radio
                _controlView.RadioCamLock.Checked += new RoutedEventHandler(OnRadioCamLockChecked);
                _controlView.RadioCamControl.Checked += new RoutedEventHandler(OnRadioCamControlChecked);
                _controlView.RadioCamReset.Checked += new RoutedEventHandler(OnRadioCamResetChecked);

                _controlView.DataContext = this;
                //}
                //else
                //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceUserNotLogin"));

            }

            if (goods_id == 50)
            {
                OnShipWaterQuality();
                _controlView.DataContext = this;
                _controlView.Show();
            }
        }

        public override void OnUnchecked()
        {
            //JoyTypeText = "None Joystick Use";
            try
            {
                base.OnUnchecked();
                this.releaseWindow();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
        /// <summary>
        /// 获取航线列表
        /// </summary>
        private void selectRouteComboBox(string searchName)
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}/api/flight-course/getlist?name=", json.poiUrl) + searchName;
            var httpservice = new HttpService();

            var listResult = RequestRouteList(httpservice, url);
            var listResultJson = JsonUtil.DeserializeFromString<dynamic>(listResult);

            if ((bool)listResultJson.status)
            {
                if (routeInfos == null)
                    routeInfos = new ObservableCollection<CombRouteListModel>();
                else
                    routeInfos.Clear();
                foreach (var itemInfo in listResultJson.data)
                {
                    CombRouteListModel routeInfo = new CombRouteListModel { id = itemInfo.id, name = itemInfo.name };
                    routeInfos.Add(routeInfo);
                }
                this.CombRouteListModels = routeInfos;
            }
        }

        private string RequestRouteList(HttpService httpservice, string url)
        {
            try
            {
                httpservice.Token = HttpServiceUtil.Token;
                var result = httpservice.HttpRequestAsync(url);
                return result;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        public void OnUavtracking(UavTrace tracingModel)
        {

            if (this.ground_state != "1")
            {
                //Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceNotOnline"));
                return;
            }

            if (this._ground_serial == tracingModel.ground_serial)
            {
                /*                               
                tracingModel:包含所有无人机数据
                */
                UavTraceVModel.ToUavInfoVM(tracingModel, this.UavTraceVM);
                //this.Altitude = Convert.ToSingle(tracingModel?.height);
                //this.Heading = Convert.ToSingle(tracingModel?.yaw);//0F
                //this.Pitch = Convert.ToSingle(tracingModel?.pitch);
                //this.Roll = Convert.ToSingle(tracingModel?.roll);//0F
                //this.GrondSpeed = Convert.ToSingle(tracingModel?.speed);

                LastTenSecSeries.Add(UavTraceVM.Altitude);
                LastTenSecSeries.RemoveAt(0);
            }
        }

        private void OnShipWaterQuality()
        {
            try
            {
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                string url = string.Format("{0}/api/unmanned-ship/get-three-dim-data?ground_serial=", json.poiUrl) + ground_serial;
                var httpservice = new HttpService();
                waterDataThread = new Thread(() =>
                {
                    var timeSpan = 500; //500ms
                    while (true)
                    {
                        Thread.Sleep(timeSpan);
                        var shipResult = RequestWarterService(httpservice, url);
                        if (!string.IsNullOrEmpty(shipResult))
                        {
                            var shipResultJson = JsonUtil.DeserializeFromString<dynamic>(shipResult);

                            //ShipTrace shipTrace = new ShipTrace();
                            if ((bool) shipResultJson.status)
                            {
                                shipCtValue = shipResultJson.data.ct;
                                shipDoValue = shipResultJson.data.doValue;
                                shipPhValue = shipResultJson.data.ph;
                                shipTempValue = shipResultJson.data.temp;
                                shipTurValue = shipResultJson.data.tur;
                                //shipTrace = JsonTool.JsonToObject<ShipTrace>(uavResultJson.data);
                            }

                            //if (shipTrace == null)
                            //{
                            //    MessageBox.Show(Helpers.ResourceHelper.FindKey("Nolocationretry"));
                            //    return;
                            //}
                        }
                    }
                });
                waterDataThread.IsBackground = true;
                waterDataThread.Start();
            }
            catch (ThreadAbortException ex)
            {

            }

        }

        private string RequestWarterService(HttpService httpservice, string url)
        {
            try
            {
                httpservice.Token = HttpServiceUtil.Token;
                var result = httpservice.HttpRequestAsync(url);
                return result;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        public void OnRadioCamLockChecked(object sender, RoutedEventArgs e)
        {
            // 同UnChecked判断。
            var cmdData = cmdCamLock();
            var jsonData = JsonUtil.SerializeToString(cmdData);
            _sendMessage = jsonData;
            Console.WriteLine("CamCamLock:" + _sendMessage);
            if (OnTimeUpdated != null)
            {
                OnTimeUpdated(ground_serial, mountType, _sendMessage);
            }
        }

        private bool isFloat(string val)
        {
            if (val == null) return false;
            Regex reg = new Regex(@"^(-?\d+)(\.\d+)?$");
            return reg.Match(val).Success;
        }

        /// <summary>
        /// set vehicle takeoff height
        /// </summary>
        public string SelectTakeOffHeight
        {
            get { return _takeoffHeight; }
            set { base.SetAndNotifyPropertyChanged<string>(ref _takeoffHeight, value, "SelectTakeOffHeight"); }
        }

        /// <summary>
        /// 选择航线名称
        /// </summary>
        private string _selectRouteName;
        public string SelectRouteName
        {
            get { return _selectRouteName; }
            set { _selectRouteName = value; NotifyPropertyChanged("SelectRouteName"); /*selectRouteComboBox(_selectRouteName);*/ }
        }



        private ObservableCollection<CombRouteListModel> _routeListModel;

        [XmlIgnore]
        public ObservableCollection<CombRouteListModel> CombRouteListModels
        {
            get { return this._routeListModel; }
            set { base.SetAndNotifyPropertyChanged<ObservableCollection<CombRouteListModel>>(ref this._routeListModel, value, "CombRouteListModels"); }
        }

        private CombRouteListModel _selectedRouteListModel;
        [XmlIgnore]
        public CombRouteListModel SelectedRouteListModel
        {
            get { return this._selectedRouteListModel; }
            set
            {
                base.SetAndNotifyPropertyChanged<CombRouteListModel>(ref this._selectedRouteListModel, value, "SelectedRouteListModel");
                if (_selectedRouteListModel != null)
                    _sendVehicleRouteId = (int)_selectedRouteListModel.id;
                //Console.WriteLine("_selectedRouteListModel" + _selectedRouteListModel.id);
            }
        }

        public void OnRadioCamControlChecked(object sender, RoutedEventArgs e)
        {
            var cmdData = cmdCamControl();
            var jsonData = JsonUtil.SerializeToString(cmdData);
            _sendMessage = jsonData;
            Console.WriteLine("CamCamControl:" + _sendMessage);
            if (OnTimeUpdated != null)
            {
                OnTimeUpdated(ground_serial, mountType, _sendMessage);
            }
        }
        public void OnRadioCamResetChecked(object sender, RoutedEventArgs e)
        {
            var cmdData = cmdCamReset();
            var jsonData = JsonUtil.SerializeToString(cmdData);
            _sendMessage = jsonData;
            Console.WriteLine("CamCamReset:" + _sendMessage);
            if (OnTimeUpdated != null)
            {
                OnTimeUpdated(ground_serial, mountType, _sendMessage);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            this.camCamHeadAngle = "1.06502184401";
            this.camCamPitchAngle = "0.8829054252";
            this.camCamRoolAngle = "0.000124858";
            this.camCamZoom = "";

            shipCtValue = "0.001";
            shipDoValue = "0.001";
            shipPhValue = "0.001";
            shipTempValue = "0.001";
            shipTurValue = "0.001";
            this.UavTraceVM = new UavTraceVModel();
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                this.releaseWindow();
            });
            this.DropDump = new RelayCommand(() =>//UseJoysticApp
            {
                //string path = AppDomain.CurrentDomain.BaseDirectory + @"mmcJoyServer\mmcJoyServer.exe";
                using (Process p = new Process())
                {
                    var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    string urip = json.mmcJoyServer;
                    //p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    //p.StartInfo.FileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"mmcJoyServer\mmcJoyServer.exe";
                    p.StartInfo.FileName = @"F:\Work\3dgis_app\binPath\mmcJoyServer\mmcJoyServer.exe";
                    p.Start();
                    p.WaitForExit();
                    p.Close();
                }
            });
            this.UseJoysticApp = new RelayCommand(() =>//DropDump
            {
                var cmdData = cmdDropDump();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("cmdDropDump:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.ButtonRequest = new RelayCommand(() =>
            {
                var cmdData = cmdRequest();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("ButtonRequest:" + _sendMessage);

                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamPitchUp = new RelayCommand(() =>
            {
                var cmdData = cmdPitchUp();
                var jsonData = JsonUtil.SerializeToString(cmdData);

                _sendMessage = jsonData;
                Console.WriteLine("CamPitchUp:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamPitchDown = new RelayCommand(() =>
            {
                var cmdData = cmdPitchDown();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamPitchDown:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamHeadLeft = new RelayCommand(() =>
            {
                var cmdData = cmdHeadLeft();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamHeadLeft:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamHeadRight = new RelayCommand(() =>
            {
                var cmdData = cmdHeadRight();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamHeadRight:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamZoomOut = new RelayCommand(() =>
            {
                var cmdData = cmdZoomOut();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamZoomOut:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                    Thread.Sleep(1000);
                    this.CamZoomStop.Execute(null);
                }
            });
            this.CamZoomStop = new RelayCommand(() =>
            {
                var cmdData = cmdZoomStop();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamZoomOut:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.CamZoomIn = new RelayCommand(() =>
            {
                var cmdData = cmdZoomIn();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamZoomIn:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                    Thread.Sleep(1000);
                    this.CamZoomStop.Execute(null);
                }
            });

            this.CamCamCapture = new RelayCommand(() =>
            {
                var cmdData = cmdCamCapture();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamCamCapture:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.CamCamVideo = new RelayCommand(() =>
            {
                var cmdData = cmdCamVideo();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamPitchDown:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            //Gas Begin
            this.GasPitchUp = new RelayCommand(() =>
            {
                var cmdData = cmdGasPitchUp();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamPitchDown:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.GasPitchDown = new RelayCommand(() =>
            {
                var cmdData = cmdGasPitchDown();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                Console.WriteLine("CamPitchDown:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.cmdPanoramic = new RelayCommand(() =>
            {
                PanoramicViewModel PanoramicViewModel = new PanoramicViewModel
                {
                    ground_serial = ground_serial,
                };
            });

            this.cmdJoyTakeoff = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleTakeoff();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleTakeoff:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.cmdJoyAutoModel = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleAutoModel();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleTakeoff:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.cmdJoyArm = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleArm();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.cmdJoyLocked = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleLocked();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.cmdJoyLand = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleLand();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.cmdJoyReturnModel = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleReturn();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });

            this.cmdJoyLoadRoute = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(SelectRouteName))
                {
                    Messages.ShowMessage("航线名称不存在，请选择后重试！");
                    return;
                }

                var cmdData = cmdVehicleLoadRoute();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
            this.cmdJoySendRoute = new RelayCommand(() =>
            {
                var cmdData = cmdVehicleSendRoute();
                var jsonData = JsonUtil.SerializeToString(cmdData);
                _sendMessage = jsonData;
                //Console.WriteLine("cmdVehicleLand:" + _sendMessage);
                if (OnTimeUpdated != null)
                {
                    OnTimeUpdated(ground_serial, mountType, _sendMessage);
                }
            });
        }


        #region create SocketItem
        private SocketItem cmdVehicleArm()//解锁
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 100;
            cmd.data.cmdStep = 0;//value<100,the uav armed
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdVehicleLocked()//上锁
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 100;
            cmd.data.cmdStep = 1000;// value>100,the uav locked
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdVehicleTakeoff()//起飞
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 101;
            cmd.data.cmdStep = int.Parse(SelectTakeOffHeight);//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdVehicleAutoModel()//航线模式
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 105;
            cmd.data.cmdStep = 0;//no use
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }
        private SocketItem cmdVehicleLand()//降落
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 102;
            cmd.data.cmdStep = 5;//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }
        private SocketItem cmdVehicleReturn()//返航
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 106;
            cmd.data.cmdStep = 0;
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdVehicleLoadRoute()//地面站下载航线
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 103;
            cmd.data.cmdStep = _sendVehicleRouteId;//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdVehicleSendRoute()//发送航线至无人机
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = 11;
            cmd.data.mountType = mountType; //not use
            cmd.data.cmdFunction = 104;
            cmd.data.cmdStep = 0;//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }


        public SocketItem cmdRequest()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_REQUEST;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = 0;
            cmd.data.cmdFunction = 0;
            cmd.data.cmdStep = 0;
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdPitchUp()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 10;
            cmd.data.cmdStep = 5;//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdPitchDown()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 10;
            cmd.data.cmdStep = 5;
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdHeadLeft()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 11;
            cmd.data.cmdStep = 5;
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdHeadRight()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 11;
            cmd.data.cmdStep = 5;
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdZoomOut()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 12;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 2;
            return cmd;
        }

        private SocketItem cmdZoomStop()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 12;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 1;
            return cmd;
        }

        private SocketItem cmdZoomIn()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 12;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdCamLock()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 13;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;//无此项
            return cmd;
        }

        private SocketItem cmdCamControl()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 14;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;//无此项
            return cmd;
        }

        private SocketItem cmdCamReset()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 15;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;//无此项
            return cmd;
        }

        private SocketItem cmdCamCapture()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 16;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;//无此项
            return cmd;
        }

        private SocketItem cmdCamVideo()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 17;
            cmd.data.cmdStep = 0;//无此项
            cmd.data.cmdPlus = 0;//无此项
            cmd.data.cmdZoom = 0;//无此项
            return cmd;
        }

        private SocketItem cmdGasPitchUp()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 50;
            cmd.data.cmdStep = 5;//step exchange for drop-down menu
            cmd.data.cmdPlus = 1;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdGasPitchDown()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = mountType; //Be the CamData.camtype
            cmd.data.cmdFunction = 50;
            cmd.data.cmdStep = 5; //step exchange for drop-down menu
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }

        private SocketItem cmdDropDump()
        {
            var cmd = new SocketItem();
            cmd.data = new SocketData();
            cmd.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND;
            cmd.groundSerial = ground_serial;
            cmd.data.cmdRequest = (int)TypeEnum.WEB_CONTROLLER.CMD_REQUEST_WEB_2_GROUDN_SEND;
            cmd.data.cmdType = (int)TypeEnum.WEB_CONTROLLER.CMD_FUNCTION_WEB_2_GROUND_PITCH;
            cmd.data.mountType = 298; //mountType
            cmd.data.cmdFunction = 42;
            cmd.data.cmdStep = 0;
            cmd.data.cmdPlus = 0;
            cmd.data.cmdZoom = 0;
            return cmd;
        }
        #endregion


    }
}
