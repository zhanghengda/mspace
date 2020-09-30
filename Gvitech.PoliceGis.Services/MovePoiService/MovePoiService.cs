using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.MovePoi;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mmc.Mspace.Services.MovePoiService
{
    public class MovePoiService : Singleton<MovePoiService>, IMovePoiService
    {
        public MovePoiService()
        {
            this._myLock = new object();
            this.PrePoliceCarQueue = new Queue<PoliceCar>();
            this.PrePoliceManQueue = new Queue<Policeman>();
        }

        public void TestUrl()
        {
        }

        public IPOIFeatureClass this[string fcAliasName]
        {
            get
            {
                bool flag = !IEnumerableExtension.HasValues<IPOIFeatureClass>(this._poiFCs);
                IPOIFeatureClass result;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    result = (string.IsNullOrEmpty(fcAliasName) ? null : this._poiFCs.FirstOrDefault((IPOIFeatureClass poifc) => fcAliasName.Equals(poifc.AliasName)));
                }
                return result;
            }
        }

        public void StartWork()
        {
            bool flag = StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["SendSocket"], false);
            if (flag)
            {
                SendMovePoiliceService sendMovePoiliceService = new SendMovePoiliceService(ConfigurationManager.AppSettings["SocketIP"], StringExtension.ParseTo<int>(ConfigurationManager.AppSettings["SocketPort"], 10099), ProtocolType.Tcp);
                sendMovePoiliceService.StartAsyncSendMessage();
            }
            this.StartAsyncWork();
        }

        public void PauseWork()
        {
        }

        public void ContinueWork()
        {
        }

        public void StopWork()
        {
            this._start = false;
        }

        public Dictionary<string, List<LayerItemModel>> GroupLayerItemModels()
        {
            bool flag = !IEnumerableExtension.HasValues<IPOIFeatureClass>(this._poiFCs);
            Dictionary<string, List<LayerItemModel>> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                string[] groupNames = Singleton<LayerGroupService.LayerGroupService>.Instance.GetGroupNames();
                bool flag2 = !CollectionExtension.HasValues<string>(groupNames);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    Dictionary<string, List<LayerItemModel>> layerModelDny = new Dictionary<string, List<LayerItemModel>>();
                    string[] arry;
                    Func<IPOIFeatureClass, bool> pois = null;
                    groupNames.ToList<string>().ForEach(delegate (string group)
                    {
                        arry = Singleton<LayerGroupService.LayerGroupService>.Instance.GetGroupLayers(group);
                        bool flag3 = !CollectionExtension.HasValues<string>(arry);
                        if (!flag3)
                        {
                            IEnumerable<IPOIFeatureClass> poiFCs = this._poiFCs;
                            Func<IPOIFeatureClass, bool> predicate;
                            if ((predicate = pois) == null)
                            {
                                predicate = (pois = ((IPOIFeatureClass fc) => arry.Contains(fc.AliasName.ToLower())));
                            }
                            IEnumerable<IPOIFeatureClass> source = poiFCs.Where(predicate);
                            CollectionExtension.AddEx<string, List<LayerItemModel>>(layerModelDny, group, (from fc in source.ToList<IPOIFeatureClass>()
                                                                                                           select new LayerItemModel
                                                                                                           {
                                                                                                               Name = fc.AliasName,
                                                                                                               Group = @group,
                                                                                                               IsVisible = gviViewportMaskExtension.GetIsVisible(fc.ViewportMask),
                                                                                                               Parameters = fc
                                                                                                           }).ToList<LayerItemModel>());
                        }
                    });
                    result = layerModelDny;
                }
            }
            return result;
        }

        public static MovePoiService GetDefault(object args = null)
        {
            return Singleton<MovePoiService>.Instance;
        }

        private void SocketServiceListen(object state)
        {
            MovePoiService.NetParameter netParameter = state as MovePoiService.NetParameter;
            bool flag = netParameter == null;
            if (!flag)
            {
                bool flag2 = string.IsNullOrEmpty(netParameter.Ip);
                if (!flag2)
                {
                    IPAddress address = IPAddress.Parse(netParameter.Ip);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint remoteEP = new IPEndPoint(address, netParameter.Port);
                    SystemLog.Log("Socket 开始连接", 0);
                    try
                    {
                        //MovePoiService.<> c__DisplayClass20_1 <> c__DisplayClass20_ = new MovePoiService.<> c__DisplayClass20_1();
                        //<> c__DisplayClass20_.<> 4__this = this;

                        while (this._start)
                        {
                            try
                            {
                                bool flag3 = !socket.Connected;
                                if (flag3)
                                {
                                    socket.Connect(remoteEP);
                                }
                            }
                            catch (SocketException ex)
                            {
                                Console.WriteLine(ex.Message);
                                Thread.Sleep(1000);
                                continue;
                            }
                            byte[] array = new byte[1024];
                            int num = socket.Receive(array);
                            bool flag4 = num < 1;
                            if (flag4)
                            {
                                Thread.Sleep(500);
                            }
                            string s = Encoding.UTF8.GetString(array, 0, num);
                            bool flag5 = string.IsNullOrEmpty(s);
                            if (!flag5)
                            {
                                bool flag6 = !s.Contains("deviceId");
                                if (!flag6)
                                {
                                    List<string> list = this.MatchMesage(s);
                                    RecevieMessage rmsg;
                                    Func<PoliceCar, bool> pois = null;
                                    Func<Policeman, bool> policeManFuns = null;
                                    list.ForEach(delegate (string msg)
                                    {
                                        bool flag7 = string.IsNullOrEmpty(msg);
                                        if (!flag7)
                                        {
                                            try
                                            {
                                                rmsg = JsonConvert.DeserializeObject<RecevieMessage>(msg);
                                                bool flag8 = rmsg == null || string.IsNullOrEmpty(rmsg.deviceId);
                                                if (!flag8)
                                                {
                                                    bool flag9 = string.IsNullOrEmpty(rmsg.deviceType);
                                                    if (!flag9)
                                                    {
                                                        string coord = string.Concat(new object[] { rmsg.longti, ";", rmsg.lati, ";20" });
                                                        string a = rmsg.deviceType.ToLower();
                                                        if (!(a == "car"))
                                                        {
                                                            //MovePoiService.<> c__DisplayClass20_1 cs$<> 8__locals = <> c__DisplayClass20_;
                                                            // IEnumerable<Policeman> prePoliceManQueue = <> c__DisplayClass20_.<> 4__this.PrePoliceManQueue;
                                                            IEnumerable<Policeman> prePoliceManQueue = this.PrePoliceManQueue;
                                                            Func<Policeman, bool> predicate;
                                                            if ((predicate = policeManFuns) == null)
                                                            {
                                                                predicate = (policeManFuns = ((Policeman p) => p.POLICE.Equals(rmsg.deviceId)));
                                                            }
                                                            Policeman policeman = prePoliceManQueue.FirstOrDefault(predicate);
                                                            bool flag10 = policeman == null;
                                                            if (flag10)
                                                            {
                                                                //MovePoiService.<> c__DisplayClass20_1 cs$<> 8__locals2 = <> c__DisplayClass20_;
                                                                PoliceManInfo pmInfo;
                                                                if ((pmInfo = ServiceManager.GetService<IPoliceHttpService>(null).GetPoliceMan(rmsg.deviceId)) == null)
                                                                {
                                                                    pmInfo = new PoliceManInfo
                                                                    {
                                                                        XM = RandomPersonName.GenName(),
                                                                        JH = rmsg.deviceId
                                                                    };
                                                                }
                                                                //cs$<> 8__locals2.pmInfo = pmInfo;
                                                                PoliceManInfo pmInfo2 = pmInfo;
                                                                this.PrePoliceManQueue.Enqueue(new Policeman
                                                                {
                                                                    POLICE = rmsg.deviceId,
                                                                    PolicemanCoordinate = coord,
                                                                    PolicemanName = ((pmInfo2 != null) ? pmInfo2.XM : string.Empty),
                                                                    PolicemanNumber = ((pmInfo2 != null) ? pmInfo2.JH : string.Empty)
                                                                });
                                                            }
                                                            else
                                                            {
                                                                policeman.PolicemanCoordinate = coord;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // MovePoiService.<> c__DisplayClass20_1 cs$<> 8__locals3 = <> c__DisplayClass20_;
                                                            IEnumerable<PoliceCar> prePoliceCarQueue = this.PrePoliceCarQueue;
                                                            Func<PoliceCar, bool> predicate2;
                                                            if ((predicate2 = pois) == null)
                                                            {
                                                                predicate2 = (pois = ((PoliceCar p) => p.PoliceCarId.Equals(rmsg.deviceId)));
                                                            }
                                                            PoliceCar policCar = prePoliceCarQueue.FirstOrDefault(predicate2);
                                                            bool flag11 = policCar == null;
                                                            if (flag11)
                                                            {
                                                                //  MovePoiService.<> c__DisplayClass20_1 cs$<> 8__locals4 = <> c__DisplayClass20_;
                                                                PoliceCarInfo pcInfo;
                                                                if ((pcInfo = ServiceManager.GetService<IPoliceHttpService>(null).GetPoliceCar(rmsg.deviceId)) == null)
                                                                {
                                                                    pcInfo = new PoliceCarInfo
                                                                    {
                                                                        CPH = rmsg.deviceId
                                                                    };
                                                                }
                                                                PoliceCarInfo pcInfo2 = pcInfo;
                                                                this.PrePoliceCarQueue.Enqueue(new PoliceCar
                                                                {
                                                                    PoliceCarId = rmsg.deviceId,
                                                                    PoliceCarCoordinate = coord,
                                                                    PoliceCarContent = ((pcInfo2 != null) ? pcInfo2.DWMC : string.Empty),
                                                                    PoliceCarNumber = ((pcInfo2 != null) ? pcInfo2.CPH : string.Empty)
                                                                });
                                                            }
                                                            else
                                                            {
                                                                policCar.PoliceCarCoordinate = coord;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            catch
                                            {
                                                SystemLog.Log("解析失败:     " + s, LogMessageType.ERROR);
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        SystemLog.Log("Socket 连接失败", LogMessageType.ERROR);
                        SystemLog.Log(ex2);
                        return;
                    }
                    finally
                    {
                        bool connected = socket.Connected;
                        if (connected)
                        {
                            socket.Shutdown(SocketShutdown.Both);
                        }
                        socket.Close();
                    }
                    SystemLog.Log("Socket 连接关闭", 0);
                }
            }
        }

        private void AsyncSocketListen(object state)
        {
            try
            {
                MovePoiService.NetParameter netParameter = state as MovePoiService.NetParameter;
                bool flag = netParameter == null;
                if (!flag)
                {
                    bool flag2 = string.IsNullOrEmpty(netParameter.Ip);
                    if (!flag2)
                    {
                        bool flag3 = this._client == null;
                        if (flag3)
                        {
                            this._client = new TcpClient();
                        }
                        bool flag4 = !this._client.Connected;
                        if (flag4)
                        {
                            this._client.BeginConnect(IPAddress.Parse(netParameter.Ip), netParameter.Port, new AsyncCallback(this.ConnectCallback), this._client);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            bool flag = ar.AsyncState == null;
            if (!flag)
            {
                TcpClient client = ar.AsyncState as TcpClient;
                try
                {
                    bool flag2 = client != null && client.Connected;
                    if (flag2)
                    {
                        client.EndConnect(ar);
                        this._msgtimer = new Timer(delegate (object state)
                        {
                            bool flag3 = !client.Connected;
                            if (flag3)
                            {
                                this._msgtimer.Change(-1, -1);
                            }
                            else
                            {
                                NetworkStream stream = client.GetStream();
                                byte[] array = new byte[client.Available];
                                try
                                {
                                    stream.BeginRead(array, 0, array.Length, new AsyncCallback(this.ReadCallback), array);
                                }
                                catch
                                {
                                }
                            }
                        }, client, 10000, 10000);
                    }
                    else
                    {
                        SystemLog.Log("Socket 客户端连接失败!", 0);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            byte[] array = ar.AsyncState as byte[];
            bool flag = array == null;
            if (!flag)
            {
                string strResponse = Encoding.UTF8.GetString(array).Trim();
                this.ResolveMessage(strResponse);
            }
        }

        private void ResolveMessage(string strResponse)
        {
            bool flag = string.IsNullOrEmpty(strResponse);
            if (!flag)
            {
                bool flag2 = !strResponse.Contains("deviceId");
                if (!flag2)
                {
                    RecevieMessage rmsg = null;
                    List<string> list = this.MatchMesage(strResponse);
                    Func<PoliceCar, bool> pois = null;
                    Func<Policeman, bool> policeManFuns = null;
                    list.ForEach(delegate (string msg)
                    {
                        bool flag3 = string.IsNullOrEmpty(msg);
                        if (!flag3)
                        {
                            try
                            {
                                try
                                {
                                    rmsg = JsonConvert.DeserializeObject<RecevieMessage>(msg);
                                }
                                catch (Exception ex)
                                {
                                    SystemLog.Log(ex);
                                }
                                bool flag4 = rmsg == null || string.IsNullOrEmpty(rmsg.deviceId);
                                if (!flag4)
                                {
                                    bool flag5 = string.IsNullOrEmpty(rmsg.deviceType);
                                    if (!flag5)
                                    {
                                        string text = string.Concat(new object[]
                                        {
                                            rmsg.longti,
                                            ";",
                                            rmsg.lati,
                                            ";20"
                                        });
                                        string a = rmsg.deviceType.ToLower();
                                        if (!(a == "car"))
                                        {
                                            IEnumerable<Policeman> prePoliceManQueue = this.PrePoliceManQueue;
                                            Func<Policeman, bool> predicate;
                                            if ((predicate = policeManFuns) == null)
                                            {
                                                predicate = (policeManFuns = ((Policeman p) => p.POLICE.Equals(rmsg.deviceId)));
                                            }
                                            Policeman policeman = prePoliceManQueue.FirstOrDefault(predicate);
                                            bool flag6 = policeman == null;
                                            if (flag6)
                                            {
                                                PoliceManInfo policeManInfo;
                                                if ((policeManInfo = ServiceManager.GetService<IPoliceHttpService>(null).GetPoliceMan(rmsg.deviceId)) == null)
                                                {
                                                    policeManInfo = new PoliceManInfo
                                                    {
                                                        XM = RandomPersonName.GenName(),
                                                        JH = rmsg.deviceId
                                                    };
                                                }
                                                PoliceManInfo policeManInfo2 = policeManInfo;
                                                this.PrePoliceManQueue.Enqueue(new Policeman
                                                {
                                                    POLICE = rmsg.deviceId,
                                                    PolicemanCoordinate = text,
                                                    PolicemanName = ((policeManInfo2 != null) ? policeManInfo2.XM : string.Empty),
                                                    PolicemanNumber = ((policeManInfo2 != null) ? policeManInfo2.JH : string.Empty)
                                                });
                                            }
                                            else
                                            {
                                                policeman.PolicemanCoordinate = text;
                                            }
                                        }
                                        else
                                        {
                                            IEnumerable<PoliceCar> prePoliceCarQueue = this.PrePoliceCarQueue;
                                            Func<PoliceCar, bool> predicate2;
                                            if ((predicate2 = pois) == null)
                                            {
                                                predicate2 = (pois = ((PoliceCar p) => p.PoliceCarId.Equals(rmsg.deviceId)));
                                            }
                                            PoliceCar policeCar = prePoliceCarQueue.FirstOrDefault(predicate2);
                                            bool flag7 = policeCar == null;
                                            if (flag7)
                                            {
                                                PoliceCarInfo policeCarInfo;
                                                if ((policeCarInfo = ServiceManager.GetService<IPoliceHttpService>(null).GetPoliceCar(rmsg.deviceId)) == null)
                                                {
                                                    policeCarInfo = new PoliceCarInfo
                                                    {
                                                        CPH = rmsg.deviceId
                                                    };
                                                }
                                                PoliceCarInfo policeCarInfo2 = policeCarInfo;
                                                this.PrePoliceCarQueue.Enqueue(new PoliceCar
                                                {
                                                    PoliceCarId = rmsg.deviceId,
                                                    PoliceCarCoordinate = text,
                                                    PoliceCarContent = ((policeCarInfo2 != null) ? policeCarInfo2.DWMC : string.Empty),
                                                    PoliceCarNumber = ((policeCarInfo2 != null) ? policeCarInfo2.CPH : string.Empty)
                                                });
                                            }
                                            else
                                            {
                                                policeCar.PoliceCarCoordinate = text;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex2)
                            {
                                SystemLog.Log("解析失败:     " + strResponse, LogMessageType.ERROR);
                                SystemLog.Log(ex2);
                            }
                        }
                    });
                }
            }
        }

        private void StartAsyncWork()
        {
            bool flag = !this._start;
            if (!flag)
            {
                int num = 900000;
                MovePoiService.NetParameter state = new MovePoiService.NetParameter(ConfigurationManager.AppSettings["SocketIP"], StringExtension.ParseTo<int>(ConfigurationManager.AppSettings["SocketPort"], 10099));
                if (this._sockettimer == null)
                    this._sockettimer = new Timer(new TimerCallback(this.AsyncSocketListen), state, 200, num);
                else
                    this._sockettimer.Change(num, num);
                int num2 = 5000;
                if (this._createtimer == null)
                    this._createtimer = new Timer(new TimerCallback(this.CreatePoi), num2, num2, num2);
                else
                    this._createtimer.Change(num2, num2);
                int num3 = 600000;
                if (this._hidetimer == null)
                    this._hidetimer = new Timer(new TimerCallback(this.HideTimOutPoi), num3, num3, num3);
                else
                    this._hidetimer.Change(num3, num3);
            }
        }

        private void CreatePoi(object state)
        {
            bool flag = !this._start;
            if (!flag)
            {
                try
                {
                    while (this.PrePoliceManQueue != null && this.PrePoliceManQueue.Count > 0)
                    {
                        Policeman policeman = this.PrePoliceManQueue.Dequeue();
                        bool flag2 = policeman == null;
                        if (flag2)
                        {
                            return;
                        }
                        bool flag3 = string.IsNullOrEmpty(policeman.POLICE);
                        if (flag3)
                        {
                            return;
                        }
                        this["移动警员"].UpdateData(new MovePoliceman(policeman));
                    }
                    while (this.PrePoliceCarQueue != null && this.PrePoliceCarQueue.Count > 0)
                    {
                        PoliceCar policeCar = this.PrePoliceCarQueue.Dequeue();
                        bool flag4 = policeCar == null;
                        if (flag4)
                        {
                            break;
                        }
                        bool flag5 = string.IsNullOrEmpty(policeCar.PoliceCarId);
                        if (flag5)
                        {
                            break;
                        }
                        this["移动警车"].UpdateData(new MovePoliceCar(policeCar));
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                finally
                {
                }
            }
        }

        private void HideTimOutPoi(object state)
        {
            bool flag = !this._start;
            if (!flag)
            {
                try
                {
                    bool flag2 = !IEnumerableExtension.HasValues<IPOIFeatureClass>(this._poiFCs);
                    if (!flag2)
                    {
                        this._poiFCs.ForEach(delegate (IPOIFeatureClass fc)
                        {
                            fc.HideOutTimePoi();
                        });
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                finally
                {
                }
            }
        }

        private List<string> MatchMesage(string msg)
        {
            List<string> list = new List<string>();
            bool flag = string.IsNullOrEmpty(msg);
            List<string> result;
            if (flag)
            {
                result = list;
            }
            else
            {
                Regex regex = new Regex("(\\{[^\\{^\\}]*\\})");
                MatchCollection matchCollection = regex.Matches(msg);
                int num;
                for (int i = 0; i < matchCollection.Count; i = num + 1)
                {
                    CollectionExtension.AddEx<string>(list, matchCollection[i].Value);
                    num = i;
                }
                result = list;
            }
            return result;
        }

        private object _myLock;

        private Timer _createtimer;

        private Timer _hidetimer;

        private Timer _sockettimer;

        private Timer _msgtimer;

        private readonly List<IPOIFeatureClass> _poiFCs = new List<IPOIFeatureClass>
        {
            new PoiFeatueClass("Police", "移动警员"),
            new PoiFeatueClass("PoliceCar", "移动警车")
        };

        public Queue<PoliceCar> PrePoliceCarQueue;

        public Queue<Policeman> PrePoliceManQueue;

        private bool _start = true;

        private TcpClient _client;

        private class NetParameter
        {
            public NetParameter(string ip, int port)
            {
                this.Ip = ip;
                this.Port = port;
            }

            public string Ip { get; private set; }

            public int Port { get; private set; }
        }
    }
}