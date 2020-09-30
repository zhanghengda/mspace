using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.UavModule.UavTracing;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System.Xml.Serialization;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Cache;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.UavModule.Service
{
    public class JoyService
    {
        private Socket _udpServer;

        //遥控控制云台步进幅度
        private int _stepJoy = 1;

        [XmlIgnore]
        public UavControlVModel uavControlVModel { get; set; }

        /// <summary>
        /// 摇杆类型名称
        /// </summary>
        private string _joyTypeText;
        public string JoyTypeText
        {
            get { return _joyTypeText; }
            set { _joyTypeText = value; uavControlVModel.JoyTypeText = _joyTypeText; }
        }

        public JoyService()
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Closed += (sender, e) =>
            {
                thread_flag = false;
                _udpServer.Close();
            };
            initJoyServer();
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

        /// <summary>
        /// 选择处于开放的无人机界面
        /// </summary>
        /// <param name="list"></param>
        private void cmdUavMountControl(string joyType, List<JoyInfo> list)
        {
            if (this.uavControlVModel == null)
                return;

            //foreach (var item in this.uavItemModels)
            //{
            //    if (item.MountControlViewModel.IsChecked)
            //    {
            //        item.MountControlViewModel.JoyTypeText = joyType;
            //        processKeyEvent(list, item.MountControlViewModel);
            //    }
            //}
            JoyTypeText = joyType;
            processKeyEvent(list, uavControlVModel);
        }

        /// <summary>
        /// 摇杆数据处理
        /// </summary>
        /// <param name="list"></param>
        private void processKeyEvent(List<JoyInfo> list, UavControlVModel model)
        {
            if (JoyTypeText == "Saitek Pro Flight X-56 Rhino Stick")
            {
                X56RockerKey(list, model);
            }
            else
            {
                DefaultRockerKey(list, model);
            }
        }

        private void DefaultRockerKey(List<JoyInfo> list, UavControlVModel model)
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

        private void X56RockerKey(List<JoyInfo> list, UavControlVModel model)
        {
            foreach (JoyInfo c in list)
            {
                if (c.Name == "Buttons1" && c.Value > 100)    //拍照
                {
                    model.CamCamCapture.Execute(null);
                    Console.WriteLine("拍照");
                }
                else if (c.Name == "PointOfViewControllers0") //缩放
                {
                    if (c.Value <= 9000 && c.Value >= 0)
                    {
                        model.CamZoomIn.Execute(null);
                        Console.WriteLine("缩小");
                    }
                    else if (c.Value > 9000)
                    {
                        model.CamZoomOut.Execute(null);
                        Console.WriteLine("放大");
                    }
                }
                else if (c.Name == "Buttons2" && c.Value > 100)    //录像
                {
                    model.CamCamVideo.Execute(null);
                    Console.WriteLine("录像");
                }
                else if (c.Name == "Buttons10" && c.Value > 100)   //云台向上
                {
                    model.CamPitchUp.Execute(null);
                    Console.WriteLine("云台向上");
                }
                else if (c.Name == "Buttons11" && c.Value > 100)    //云台向右
                {
                    model.CamHeadRight.Execute(null);
                    Console.WriteLine("云台向右");
                }
                else if (c.Name == "Buttons12" && c.Value > 100)    //云台向下
                {
                    model.CamPitchDown.Execute(null);
                    Console.WriteLine("云台向下");
                }
                else if (c.Name == "Buttons13" && c.Value > 100)    //云台向左
                {
                    model.CamHeadLeft.Execute(null);
                    Console.WriteLine("云台向左");
                }
            }

        }
    }//end service


    internal class JoyInfo
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
