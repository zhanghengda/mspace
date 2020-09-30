using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Mspace.UavModule.Dto;
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.UavModule.WebSocket;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.UavModule.Service;
using Application = System.Windows.Application;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class GCHelper
    {
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
    }

    public class UavListViewModel : CheckedToolItemModel
    {
        private UavItemViewModel selectedUavItemModel;
        private UavTracingModel tracingModel;
        private UavControlVModel _uavControlVModel;
        private MultiVideoVModel _multiVideoVM;
        private JoyService _joyService;
        private Socket _udpServer;
        private UavTracingView uavTracingView;
        private Window shell;
        private System.Timers.Timer _socketConnectTimer;
        private readonly double _socketConnectTimerOut = 100000; //ms
        private string _groundstationList;


        private bool _isSocketLogin = false;   //用户是否登录  true:在线  false:离线
        private bool _isSocketOffline = true;  //socket是否掉线

        //base on WebSocketSharp
        private WebSocketBase wb;

        private int _camStateSleepTime = 20;//防止出现状态闭环锁死状况

        public UavListViewModel()
        {
            this.WindowTitle = Helpers.ResourceHelper.FindKey("Dronetrack");
        }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        [XmlIgnore]
        public ICommand ButtonCmd { get; set; }

        [XmlIgnore]
        public ICommand connectSocketCMD { get; set; }

        [XmlIgnore]
        public ICommand loginSocketCMD { get; set; }

        [XmlIgnore]
        public UavItemViewModel SelectedUavItemModel
        {
            get { return this.selectedUavItemModel; }
            set
            {
                base.SetAndNotifyPropertyChanged<UavItemViewModel>(ref this.selectedUavItemModel, value, "SelectedUavItemModel");
                var item = value;
                if (this._uavControlVModel != null && item != null)
                {
                    this._uavControlVModel.ground_serial = item.ground_serial;
                    this._uavControlVModel.ground_state = item.ground_state;
                    this._uavControlVModel.mount_type = item.mount_type;
                    this._uavControlVModel.goods_id = (int)item.goods_id;
                    this._uavControlVModel.isSocketConnected = IsSocketLogin;

                    //reQuest
                    var cmdData = _uavControlVModel.cmdRequest();
                    var jsonData = JsonUtil.SerializeToString(cmdData);
                    TimeUpdated(item.ground_serial, Convert.ToInt16(item.mount_type), jsonData);
                }
            }
        }


        ///// <summary>
        ///// Uav List Models for mspace user
        ///// </summary>
        //private ObservableCollection<UavItemViewModel> uavItemModels = new ObservableCollection<UavItemViewModel>();

        //[XmlIgnore]
        //public ObservableCollection<UavItemViewModel> UavItemModels
        //{
        //    get { return this.uavItemModels; }
        //    set { base.SetAndNotifyPropertyChanged<ObservableCollection<UavItemViewModel>>(ref this.uavItemModels, value, "UavItemModels"); }
        //}

        private ObservableCollection<UavItemAreaViewModel> uavItemAreaModels = new ObservableCollection<UavItemAreaViewModel>();

        [XmlIgnore]
        public ObservableCollection<UavItemAreaViewModel> UavItemAreaModels
        {
            get { return this.uavItemAreaModels; }
            set { SetAndNotifyPropertyChanged<ObservableCollection<UavItemAreaViewModel>>(ref uavItemAreaModels, value, "UavItemAreaModels"); }
        }

        private string _socketState;//socket 在线状态
        [XmlIgnore]
        public string socketState
        {
            get { return this._socketState; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._socketState, value, "socketState"); }
        }

        [XmlIgnore]
        public bool IsSocketLogin
        {
            get { return this._isSocketLogin; }
            set { base.SetAndNotifyPropertyChanged<bool>(ref this._isSocketLogin, value, "IsSocketOnline"); }
        }

        public string WindowTitle { get; set; }

        public bool IsMultiScreen { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Closed += (sender, e) =>
            {
                thread_flag = false;
                _udpServer?.Close();
            };


            //initJoyServer();

            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
            this.connectSocketCMD = new RelayCommand(() =>
            {
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                string uri = json.uavSocketUri;
                WebSocketService wss = new BuissnesServiceImpl();
                if (wb == null)
                {
                    wb = new WebSocketBase(uri, wss);
                    wb.start();
                }
                _socketConnectTimer = new System.Timers.Timer();
                _socketConnectTimer.Elapsed += new ElapsedEventHandler(connectLogout);
                _socketConnectTimer.Interval = _socketConnectTimerOut;
                _socketConnectTimer.AutoReset = false;
                _socketConnectTimer.Enabled = true;

            });
            this.loginSocketCMD = new RelayCommand(() =>
            {
                if (_isSocketOffline)
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Loginprompt"));
                    return;
                }
                if (wb != null)
                {
                    var addUser = cmdAddUser();
                    wb.send(JsonUtil.SerializeToString(addUser));
                }

            });

            //websocket data push transfer
            Messenger.Messengers.Register<string>("websocketReceivedMessage", (p) =>
            {
                onReceiveMessage(p);
            });

        }

        /// <summary>
        /// 接收websocket数据
        /// </summary>
        /// <param name="message"></param>
        private void onReceiveMessage(string message)
        {
            if (message != "" && message != null)
            {
                string name = " ";
                string msgNum = "";
                string msgContent = "";
                string dstSocket = " ";
                string srcSocket = " ";
                string time = " ";

                var messageJson = JsonUtil.DeserializeFromString<dynamic>(message);
                int type = messageJson.type;

                switch (type)
                {
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_SERVER_MESSAGE:
                        name = messageJson.name;
                        msgNum = messageJson.data.msgNum;
                        msgContent = messageJson.data.msgContent;
                        dstSocket = messageJson.dstSocket;
                        srcSocket = messageJson.srcSocket;
                        time = messageJson.time;
                        if (msgNum == "110")
                        {
                            _isSocketLogin = true;
                            //this.UavItemModels = this.GetUavList();//sync list use for mount control page

                            shell.Dispatcher.Invoke(() =>
                            {
                                checkConnectState();
                            });
                        }
                        Console.WriteLine("-----CMD_TYPE:" + (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_SERVER_MESSAGE + " msgNum:" + msgNum + " msgContent:" + msgContent);
                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND:
                        Console.WriteLine("-----CMD_TYPE:" + (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_WEB2GROUND);
                        //parseCmdTypeJson(message);
                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_GROUND2WEB:
                        Console.WriteLine("-----CMD_TYPE:" + (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_GROUND2WEB + " show Camera Data." + message);
                        shell.Dispatcher.Invoke(() =>
                        {
                            //var serId = messageJson.groundSerial;
                            var Uavlist = JsonUtil.DeserializeFromString<dynamic>(_groundstationList);
                            var list = Uavlist.data;
                            _camStateSleepTime += 1;
                            foreach (var listItem in list)
                            {
                                foreach (var only in listItem.list)
                                {
                                    string ground_serial = only.ground_serial;

                                    foreach (var uavItemAreaViewModel in UavItemAreaModels)
                                    {
                                        foreach (var item in uavItemAreaViewModel.UavItemModels)
                                        {
                                            if (item.ground_serial == ground_serial)
                                            {
                                                if (messageJson.data.mountType != null &&
                                                    messageJson.data.mountType != 9999)
                                                {
                                                    int mountType = messageJson.data.mountType;
                                                    this._uavControlVModel.mountType = messageJson.data.mountType;
                                                    item.MountControlViewModel.mountType = messageJson.data.mountType;
                                                    switch (mountType)
                                                    {
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_10:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_5100:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_SONGXIA20:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_SONY20:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_QIWA30:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_GOPRO5:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_BIFOCAL10:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_SMALLBIFOCAL:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_LLL18:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_LLLFLOW35:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_Filr:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_F1T2FOCAL:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_PGIY1:
                                                        case (int)TypeEnum.CAMERA_MOUNT.CAM_PG2IF2_LT2Z35:
                                                            string str = messageJson.data.cmdCamHeadAngle;
                                                            item.MountControlViewModel.camCamHeadAngle = str;
                                                            string str1 = messageJson.data.cmdCamPitchAngle;
                                                            item.MountControlViewModel.camCamPitchAngle = str1;
                                                            string str2 = messageJson.data.cmdCamRoolAngle;
                                                            item.MountControlViewModel.camCamRoolAngle = str2;
                                                            string str3 = messageJson.data.cmdCamZoom;
                                                            item.MountControlViewModel.camCamZoom = str3;
                                                            int modeState = messageJson.data.cmdModeState;
                                                            if (_camStateSleepTime == 20)
                                                            {
                                                                _camStateSleepTime = 0;
                                                                switch (modeState)
                                                                {
                                                                    case 0:
                                                                        item.MountControlViewModel.isRadioLockChecked =
                                                                            true;
                                                                        item.MountControlViewModel.isRadioControlChecked =
                                                                            false;
                                                                        item.MountControlViewModel.isRadioResetChecked =
                                                                            false;
                                                                        break;
                                                                    case 1:
                                                                        item.MountControlViewModel.isRadioLockChecked =
                                                                            false;
                                                                        item.MountControlViewModel.isRadioControlChecked =
                                                                            true;
                                                                        item.MountControlViewModel.isRadioResetChecked =
                                                                            false;
                                                                        break;
                                                                    case 2:
                                                                        item.MountControlViewModel.isRadioLockChecked =
                                                                            false;
                                                                        item.MountControlViewModel.isRadioControlChecked =
                                                                            false;
                                                                        item.MountControlViewModel.isRadioResetChecked =
                                                                            true;
                                                                        break;
                                                                    default:
                                                                        break;
                                                                }
                                                            }

                                                            break;
                                                        case (int)TypeEnum.MOUNT_TYPE.MOUNT_GASDETECTION:
                                                            var cmdGasData = messageJson.data.cmdGasData;
                                                            List<GasInfo> MList = new List<GasInfo>();

                                                            if (cmdGasData != null)
                                                                foreach (var gasItem in cmdGasData)
                                                                {
                                                                    //Console.WriteLine("--GasListModel cmdGasId:" + gasItem.cmdGasId  + "--cmdGasUintName:" + gasItem.cmdGasUintName + "--cmdGasValue:" + gasItem.cmdGasValue);
                                                                    string gasname = Enum.GetName(typeof(TypeEnum.GAS_NAME),
                                                                        (int)gasItem.cmdGasId);
                                                                    //string gasunit = Enum.GetName(typeof(TypeEnum.GAS_UNIT_MAP), (int)gasItem.cmdGasUint); 
                                                                    string gasunit = gasItem.cmdGasUintName;
                                                                    string gasValue = gasItem.cmdGasValue;
                                                                    MList.Add(new GasInfo
                                                                    {
                                                                        Name = gasname,
                                                                        Value = gasValue,
                                                                        Unit = gasunit,
                                                                        State = gasItem.cmdGasState,
                                                                        WarningState = gasItem.cmdGasWarningState,
                                                                        Longitude = gasItem.cmdGasLon,
                                                                        Latitude = gasItem.cmdGasLat,
                                                                        AmslAlt = gasItem.cmdGasAMSLAlt,
                                                                        Altitude = gasItem.cmdGasAlt,
                                                                    });
                                                                }

                                                            item.MountControlViewModel.GasListModel = MList;
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine(
                                                        "---error : CMD_TYPE_GROUND2WEB the mount type is 9999 or mount not online");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        });
                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_CONNECT_GROUND_SERVER:

                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_DISCONNECT_GROUND_SERVER:

                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_CONNECT_SERVER_GROUND:

                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_DISCONNECT_SERVER_GROUND:

                        break;
                    case (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_SERVER_HEARTBEAT:
                        if (_socketConnectTimer != null) _socketConnectTimer.Start();
                        _isSocketOffline = false;
                        try
                        {
                            shell.Dispatcher.Invoke(() =>
                            {
                                checkConnectState();
                            });
                        }
                        catch (Exception ex)
                        {
                            SystemLog.Log(ex);
                            return;
                        }

                        Console.WriteLine("-----CMD_TYPE: " + (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_SERVER_HEARTBEAT + " timestamp: " + messageJson.time);
                        break;
                    default:
                        Console.WriteLine("-----CMD_TYPE: MSpace WebControl $ Have NO This Type!");
                        break;
                }
            }
        }

        private SocketLogin cmdAddUser()
        {
            var socketLogin = new SocketLogin();
            socketLogin.name = CacheData.UserInfo.username;
            socketLogin.type = (int)TypeEnum.WEB_CONTROLLER.CMD_TYPE_CONNECT_SERVER_GROUND;
            return socketLogin;
        }

        private void TimeUpdated(string serial, int mountType, string sendMessage)
        {
            if (this._uavControlVModel.ground_serial != serial)
                return;

            if (IsSocketLogin)
            {
                wb?.send(sendMessage);
                Console.WriteLine("-----UavListViewModel::TimeUpdated \n" + mountType + sendMessage);
            }
            //else
            //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceUserNotLogin"));
        }

        public void connectLogout(object source, ElapsedEventArgs e)
        {
            _isSocketOffline = true;
            Console.WriteLine("UavListViewModel::connectLogout  Socket connect is logout!" + _isSocketOffline);
        }

        public void checkConnectState()
        {
            socketState = _isSocketLogin ? Helpers.ResourceHelper.FindKey("Logged") : Helpers.ResourceHelper.FindKey("Notlogged");

            if (_isSocketLogin)
            {
                this.uavTracingView.loginSocketBtn.Visibility = Visibility.Collapsed;
                this.uavTracingView.loginSocketBtn.Content = socketState;
            }
            else
                this.uavTracingView.loginSocketBtn.Visibility = Visibility.Visible;

            if (_isSocketOffline)
                this.uavTracingView.connectSocketBtn.Visibility = Visibility.Visible;
            else
                this.uavTracingView.connectSocketBtn.Visibility = Visibility.Collapsed;
            //Console.WriteLine(_socketState + "  UavListViewModel::checkConnectState  _isSocketLogin:" + _isSocketLogin  + "  _isSocketOffline:" + _isSocketOffline);
        }

        public override void OnChecked()
        {
            base.OnChecked();

            #region 多屏适配

            var json = JsonUtil.DeserializeFromFile<dynamic>(
                AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);

            if (json.openMultiScreen != null && (bool)json.openMultiScreen)
            {
                //获取屏幕信息,判断是否单屏
                Screen[] screens = Screen.AllScreens;
                this.IsMultiScreen = screens?.Length > 1;
            }

            if (IsMultiScreen && _multiVideoVM == null)
            {
                _multiVideoVM = new MultiVideoVModel();
            }

            #endregion

            var shellView = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell = ServiceManager.GetService<IShellService>(null).ShellWindow;//socket

            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Collapsed;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
            ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = System.Windows.Visibility.Hidden;

            if (this._uavControlVModel == null)
            {
                this._uavControlVModel = new UavControlVModel();
                _uavControlVModel.OnTimeUpdated += new Action<string, int, string>(this.TimeUpdated);
            }

            if (this._joyService == null)
            {
                this._joyService = new JoyService();
                this._joyService.uavControlVModel = this._uavControlVModel;
            }


            this.UavItemAreaModels = this.GetUavList();

            if (this.uavTracingView == null)
            {
                this.uavTracingView = new UavTracingView();
                this.uavTracingView.Owner = Application.Current.MainWindow;
            }

            this.uavTracingView.Left = shellView.Left + 10;
            this.uavTracingView.Top = shellView.Top + 10;
            this.uavTracingView.Width = 390;
            this.uavTracingView.Height = 450;
            this.uavTracingView.DataContext = this;
            this.uavTracingView.Show();

            checkConnectState();

            if (UavItemAreaModels != null)
            {
                foreach (var uavItemAreaViewModel in UavItemAreaModels)
                {
                    foreach (var item in uavItemAreaViewModel.UavItemModels)
                    {
                        item.VideoControllerVM.OffsetHeight = this.uavTracingView.ActualHeight;
                        item.VideoControllerVM.Width = this.uavTracingView.Width;
                        item.VideoControllerVM.WindowTitle =
                            string.Format(Helpers.ResourceHelper.FindKey("Realtimevideo") + "--{0}", item.ground_serial);
                    }
                }

                if (UavItemAreaModels.Count > 0)
                {
                    foreach (var uavItemAreaViewModel in UavItemAreaModels)
                    {
                        if (uavItemAreaViewModel.UavItemModels.Count > 0)
                        {
                            this.SelectedUavItemModel = uavItemAreaViewModel.UavItemModels.HasValues<UavItemViewModel>()
                                ? uavItemAreaViewModel.UavItemModels[0]
                                : null;
                            this.selectedUavItemModel.IsExpanded = true;
                            break;
                        }
                    }
                }
            }

            this._uavControlVModel.OnChecked();

            if (IsMultiScreen)
            {
                _multiVideoVM?.OnChecked();

                foreach (var uavItemAreaViewModel in uavItemAreaModels)
                {
                    foreach (var uavItemModel in uavItemAreaViewModel.UavItemModels)
                    {
                        uavItemModel.SetVideoScreenModels(_multiVideoVM.VideoScreenVModels);
                    }
                }
            }
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();

            if (this.uavTracingView != null)
                this.uavTracingView.Hide();

            this._uavControlVModel?.OnUnchecked();
            this._multiVideoVM?.OnUnchecked();
            this.uavItemAreaModels?.ForEach(t => t.UavItemModels?.ForEach(p => p.VideoControllerVM.IsChecked = false));
            RestoreEnv();
            this.UavItemAreaModels?.Clear();
            ServiceManager.GetService<IShellService>(null).LeftPanel.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;
            GCHelper.ClearMemory();
        }

        public void RestoreEnv()
        {
            if (UavItemAreaModels != null)
            {
                foreach (var uavItemAreaViewModel in UavItemAreaModels)
                {
                    foreach (var item in uavItemAreaViewModel.UavItemModels)
                    {
                        item.RestoreEnv();
                    }
                }
            }
        }

        private ObservableCollection<UavItemAreaViewModel> GetUavList()
        {
            try
            {
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                string rootUrl = json.poiUrl;
                string url = string.Format("{0}/api/aircraft/getuavarealist", rootUrl);
                var httpSrv = new HttpService();
                httpSrv.Token = HttpServiceUtil.Token;
                _groundstationList = httpSrv.HttpRequestAsync(url);

                var Uavlist = JsonUtil.DeserializeFromString<dynamic>(_groundstationList);
                var list = Uavlist.data;

                ObservableCollection<UavItemAreaViewModel> models = new ObservableCollection<UavItemAreaViewModel>();

                foreach (var outItem in list)
                {
                    var model = new UavItemAreaViewModel();
                    var uavList = new ObservableCollection<UavItemViewModel>();

                    int onLineCount = 0;

                    foreach (var item in outItem.list)
                    {
                        var innerModel = new UavItemViewModel()
                        {
                            ground_serial = item.ground_serial,
                            UavTitle = item.ground_state == 1
                                ? string.Format("{0}({1})", item.ground_serial,
                                    Helpers.ResourceHelper.FindKey("InService"))
                                : item.ground_serial,
                            uav_type = item.goods_name,
                            mount_type = item.mount_type,
                            ground_state = item.ground_state,
                            device_hard_id = item.deviceHardId,
                            GroundState = item.ground_state == 1
                                ? Helpers.ResourceHelper.FindKey("Online")
                                : Helpers.ResourceHelper.FindKey("Offline"),
                            CameraState = item.camera_state == 1
                                ? Helpers.ResourceHelper.FindKey("Online")
                                : Helpers.ResourceHelper.FindKey("Offline"),
                            IsExpanded = false,
                            goods_id = (int)item.goods_id,
                            IsComboxEnabled = true,
                            IsMultiScreen = this.IsMultiScreen,
                            UavTracingModel = new UavTracingModel
                            {
                                ground_serial = item.ground_serial,
                                ground_state = item.ground_state
                            },
                            VideoControllerVM = new VideoControllerVModel
                            {
                                ground_serial = item.ground_serial,
                                ground_state = item.ground_state,
                                MultiVideoVM = this._multiVideoVM,
                                IsMultiScreen = this.IsMultiScreen
                            },
                            MountControlViewModel = new UavMountControlViewModel
                            {
                                ground_serial = item.ground_serial,
                                ground_state = item.ground_state,
                                mount_type = item.mount_type,
                                goods_id = (int)item.goods_id,
                                isSocketConnected = IsSocketLogin
                            },
                            GasViewModel = new MGasViewModel
                            {
                                ground_serial = item.ground_serial,
                                ground_state = item.ground_state
                            }
                        };
                        //innerModel.MountControlViewModel.OnTimeUpdated += new Action<int, string>(this.TimeUpdated);
                        innerModel.UavTracingModel.OnTracking += new Action<UavTrace>(_uavControlVModel.OnUavtracking);

                        if (item.ground_state == 1)
                            onLineCount++;

                        uavList.Add(innerModel);
                    }

                    model.AreaID = outItem.area_id;
                    model.AreaName = outItem.area_name;
                    model.UavItemModels = uavList;
                    model.OnLine = onLineCount;
                    model.OffLine = uavList.Count - onLineCount;
                    models.Add(model);
                }

                return models;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                return null;
            }
        }

        private void initJoyServer()
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string urip = json.joyUdpServer.ip;
            int port = json.joyUdpServer.port;
            ParseUdpServer(new IPEndPoint(IPAddress.Parse(urip), port));
        }
        bool thread_flag = true;
        /// <summary>
        /// 摇杆接收UDP服务
        /// </summary>
        /// <param name="serverIP"></param>
        private void ParseUdpServer(IPEndPoint serverIP)
        {

            Console.WriteLine("Joy UDP Server Listen" + serverIP.Address.ToString() + ":" + serverIP.Port);
            _udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _udpServer.Bind(serverIP);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)ipep;
            new Thread(() =>
            {
                while (thread_flag)
                {
                    byte[] data = new byte[1024];
                    int length = 0;
                    try
                    {
                        if (_udpServer.Available > 0)
                            length = _udpServer.ReceiveFrom(data, ref Remote);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Joy UDP 出现异常：{0}", ex.Message));
                        break;
                    }
                    string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string message = Encoding.UTF8.GetString(data, 0, length);//06a3:0762$X52 Professional H.O.T.A.S.$name=value
                    string[] spliteArry = message.Split('$');
                    if (spliteArry.Length == 3)
                    {
                        string ev = spliteArry[2];
                        List<JoyInfo> joyinfolist = parseJoyInfo(spliteArry[2].ToString());
                        cmdUavMountControl(spliteArry[1], joyinfolist);
                    }

                }
                _udpServer.Close();
            }).Start();
        }

        /// <summary>
        /// 摇杆数据解析
        /// </summary>
        /// <param name="joyinfos"></param>
        /// <returns></returns>
        private static List<JoyInfo> parseJoyInfo(string joyinfos)
        {
            string[] joyinfoArry = joyinfos.Split(',');
            List<JoyInfo> joyInfos = new List<JoyInfo>();

            foreach (string tem in joyinfoArry)
            {
                string[] temInfo = tem.Split('=');
                JoyInfo joyInfo = new JoyInfo
                {
                    Name = temInfo[0],
                    Value = Convert.ToInt32(temInfo[1])
                };
                joyInfos.Add(joyInfo);
            }

            return joyInfos;
        }

        bool armCheck = false;

        //遥控控制云台步进幅度
        private int _stepJoy = 1;

        /// <summary>
        /// 选择处于开放的无人机界面
        /// </summary>
        /// <param name="list"></param>
        private void cmdUavMountControl(string joyType, List<JoyInfo> list)
        {
            if (this.UavItemAreaModels == null)
                return;

            foreach (var uavItemAreaViewModel in UavItemAreaModels)
            {
                foreach (var item in uavItemAreaViewModel.UavItemModels)
                {
                    if (item.MountControlViewModel.IsChecked)
                    {
                        item.MountControlViewModel.JoyTypeText = joyType;
                        processKeyEvent(list, item.MountControlViewModel);
                    }
                }
            }
        }

        /// <summary>
        /// 摇杆数据处理
        /// </summary>
        /// <param name="list"></param>
        private void processKeyEvent(List<JoyInfo> list, UavMountControlViewModel model)
        {
            foreach (JoyInfo c in list)
            {
                //Console.WriteLine("processKeyEvent " + c.Name + " : " + c.Value.ToString());

                if (c.Name == "Buttons19" && c.Value > 100)
                    model.CamPitchUp.Execute(null);
                else if (c.Name == "Buttons20" && c.Value > 100)
                    model.CamHeadRight.Execute(null);
                else if (c.Name == "Buttons21" && c.Value > 100)
                    model.CamPitchDown.Execute(null);
                else if (c.Name == "Buttons22" && c.Value > 100)
                    model.CamHeadLeft.Execute(null);
                else if (c.Name == "Z")//Arm
                {
                    if (c.Value < 100)
                    {
                        armCheck = true;
                        model.cmdJoyArm.Execute(null);
                    }
                    else if (c.Value > 65000)
                    {
                        model.cmdJoyLocked.Execute(null);
                        armCheck = false;
                    }
                }
                else if (c.Name == "Buttons7" && c.Value > 100 && armCheck)//take off
                {
                    model.cmdJoyTakeoff.Execute(null);
                }
                else if (c.Name == "Buttons6" && c.Value > 100 && armCheck)//auto model
                {
                    model.cmdJoyAutoModel.Execute(null);
                }
                else if (c.Name == "Buttons31" && c.Value > 100)//load route
                {
                    model.cmdJoyLoadRoute.Execute(null);
                }
                else if (c.Name == "Buttons32" && c.Value > 100 && armCheck)//land
                {
                    model.cmdJoyLand.Execute(null);
                }
                else if (c.Name == "Buttons38" && c.Value > 100)//send route to vehicle
                {
                    model.cmdJoySendRoute.Execute(null);
                }
                else if (c.Name == "Buttons1" && c.Value > 100)
                    model.DropDump.Execute(null);
                else if (c.Name == "Buttons2" && c.Value > 100)
                    model.CamCamCapture.Execute(null);
                else if (c.Name == "Buttons3" && c.Value > 100)
                    model.CamCamVideo.Execute(null);
                //else if (c.Name == "Buttons4" && c.Value > 100)
                //    model.CamPitchDown.Execute(null);
                //else if (c.Name == "Buttons5" && c.Value > 100)
                //    model.CamCamCapture.Execute(null);
                else if (c.Name == "PointOfViewControllers0" && c.Value == 0)
                    model.CamZoomIn.Execute(null);
                else if (c.Name == "PointOfViewControllers0" && c.Value == 18000)
                    model.CamZoomOut.Execute(null);
                else if (c.Name == "Buttons9" && c.Value > 100)
                {
                    //Console.WriteLine("---joy Button9  OnRadioCamLockChecked");
                    model.OnRadioCamLockChecked(null, null);
                }
                else if (c.Name == "Buttons11" && c.Value > 100)
                {
                    //Console.WriteLine("---joy Buttons11  OnRadioCamControlChecked");
                    model.OnRadioCamControlChecked(null, null);
                }
                else if (c.Name == "Buttons13" && c.Value > 100)
                {
                    //Console.WriteLine("---joy Buttons13  OnRadioCamResetChecked");
                    model.OnRadioCamResetChecked(null, null);
                }
                else if (c.Name == "Sliders0" && c.Value > 0)
                {
                    //Console.WriteLine("---joy Sliders0  ");
                    if (c.Value > 20000 && c.Value < 30000)
                        _stepJoy = 2;
                    else if (c.Value > 30000 && c.Value < 40000)
                        _stepJoy = 3;
                    else if (c.Value > 40000 && c.Value < 50000)
                        _stepJoy = 4;
                    else if (c.Value > 50000)
                        _stepJoy = 5;
                    else
                        _stepJoy = 1;
                }
                else
                {

                }

            }
        }

    }

    internal class JoyInfo
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}