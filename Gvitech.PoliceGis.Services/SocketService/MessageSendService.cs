using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.SocketService
{
    public class MessageSendService : IMessageSendService
    {
        public MessageSendService(int port, ProtocolType pt)
        {
            bool flag = port < 0;
            if (flag)
            {
                throw new ArgumentException(" port should not less than zero");
            }
            this._pt = pt;
            IPAddress ipaddress;
            bool flag2 = !IPAddress.TryParse("192.168.134.1", out ipaddress);
            if (flag2)
            {
                throw new ArgumentException(" ipAddress invalid");
            }
            bool flag3 = ipaddress == null;
            if (flag3)
            {
                throw new ArgumentException(" ipAddress invalid");
            }
            if (pt != ProtocolType.Tcp)
            {
                if (pt == ProtocolType.Udp)
                {
                    this._udpClient = new UdpClient(19999);
                    this._udpClient.Connect(Dns.GetHostName(), port);
                }
            }
            else
            {
                this._tcpListener = new TcpListener(ipaddress, port);
            }
        }

        public MessageSendService(string host, int port, ProtocolType pt)
        {
            bool flag = string.IsNullOrEmpty(host);
            if (flag)
            {
                throw new ArgumentException("host is null or empty");
            }
            bool flag2 = port < 0;
            if (flag2)
            {
                throw new ArgumentException(" port should not less than zero");
            }
            this._pt = pt;
            IPAddress ipaddress;
            bool flag3 = !IPAddress.TryParse(host, out ipaddress);
            if (flag3)
            {
                throw new ArgumentException(" ipAddress invalid");
            }
            bool flag4 = ipaddress == null;
            if (flag4)
            {
                throw new ArgumentException(" ipAddress invalid");
            }
            if (pt != ProtocolType.Tcp)
            {
                if (pt == ProtocolType.Udp)
                {
                    this._udpClient = new UdpClient(19999);
                    this._udpClient.Connect(Dns.GetHostName(), port);
                }
            }
            else
            {
                this._tcpListener = new TcpListener(ipaddress, port);
            }
        }

        public void StartSendMessage()
        {
            ProtocolType pt = this._pt;
            if (pt != ProtocolType.Tcp)
            {
                if (pt == ProtocolType.Udp)
                {
                    this.SartSendUdpMessage();
                }
            }
            else
            {
                this.SartSendTcpMessage();
            }
        }

        public void StopSendMessage()
        {
            this._cs.Cancel();
            this._activeListener = false;
        }

        private void SartSendTcpMessage()
        {
            bool flag = this._tcpListener == null;
            if (!flag)
            {
                this._tcpListener.Start(10);
                TaskFactory tf = new TaskFactory();
                tf.StartNew(delegate
                {
                    while (this._activeListener)
                    {
                        this._tcpListener.Start();
                        TcpClient tcpClient = this._tcpListener.AcceptTcpClient();
                        bool flag2 = tcpClient.Client == null;
                        if (flag2)
                        {
                            tcpClient.Close();
                        }
                        else
                        {
                            tf.StartNew(delegate
                            {
                                try
                                {
                                    tcpClient.SendBufferSize = 1024;
                                    tf.StartNew(new Action<object>(this.SendMessage), tcpClient, TaskCreationOptions.AttachedToParent);
                                }
                                catch (Exception value)
                                {
                                    Console.WriteLine(value);
                                    bool flag3 = tcpClient != null;
                                    if (flag3)
                                    {
                                        tcpClient.Close();
                                        tcpClient = null;
                                    }
                                }
                            }, TaskCreationOptions.AttachedToParent);
                        }
                    }
                }, this._cs.Token);
            }
        }

        private void SartSendUdpMessage()
        {
            bool flag = this._udpClient == null;
            if (!flag)
            {
                TaskFactory taskFactory = new TaskFactory();
                taskFactory.StartNew(delegate
                {
                    while (this._activeListener)
                    {
                        bool flag2 = this._udpClient == null || this._udpClient.Client == null;
                        if (flag2)
                        {
                            bool flag3 = this._udpClient != null;
                            if (flag3)
                            {
                                this._udpClient.Close();
                            }
                        }
                        else
                        {
                            try
                            {
                                this._udpClient.Client.SendBufferSize = 1024;
                                this.SendMessage(this._udpClient);
                            }
                            catch (Exception value)
                            {
                                Console.WriteLine(value);
                                bool flag4 = this._udpClient != null;
                                if (flag4)
                                {
                                    this._udpClient.Close();
                                    this._udpClient = null;
                                }
                            }
                        }
                    }
                }, this._cs.Token);
            }
        }

        protected virtual void SendMessage(object obj)
        {
            bool flag = obj == null;
            if (!flag)
            {
                object arg = null;
                ProtocolType pt = this._pt;
                if (pt != ProtocolType.Tcp)
                {
                    if (pt == ProtocolType.Udp)
                    {
                        arg = (obj as UdpClient);
                    }
                }
                else
                {
                    arg = (obj as TcpClient);
                }
                //if (MessageSendService.<> o__12.<> p__0 == null)
                //{
                //    MessageSendService.<> o__12.<> p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MessageSendService), new CSharpArgumentInfo[]
                //    {
                //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
                //    }));
                //}
                //object obj2 = MessageSendService.<> o__12.<> p__0.Target(MessageSendService.<> o__12.<> p__0, arg, null);
                //if (MessageSendService.<> o__12.<> p__5 == null)
                //{
                //    MessageSendService.<> o__12.<> p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MessageSendService), new CSharpArgumentInfo[]
                //    {
                //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                //    }));
                //}
                bool flag2 = true;
                //if (!MessageSendService.<> o__12.<> p__5.Target(MessageSendService.<> o__12.<> p__5, obj2))
                //{
                //    if (MessageSendService.<> o__12.<> p__4 == null)
                //    {
                //        MessageSendService.<> o__12.<> p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MessageSendService), new CSharpArgumentInfo[]
                //        {
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                //        }));
                //    }
                //    Func<CallSite, object, bool> target = MessageSendService.<> o__12.<> p__4.Target;
                //    CallSite<> p__ = MessageSendService.<> o__12.<> p__4;
                //    if (MessageSendService.<> o__12.<> p__3 == null)
                //    {
                //        MessageSendService.<> o__12.<> p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(MessageSendService), new CSharpArgumentInfo[]
                //        {
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                //        }));
                //    }
                //    Func<CallSite, object, object, object> target2 = MessageSendService.<> o__12.<> p__3.Target;
                //    CallSite<> p__2 = MessageSendService.<> o__12.<> p__3;
                //    object arg2 = obj2;
                //    if (MessageSendService.<> o__12.<> p__2 == null)
                //    {
                //        MessageSendService.<> o__12.<> p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MessageSendService), new CSharpArgumentInfo[]
                //        {
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
                //        }));
                //    }
                //    Func<CallSite, object, object, object> target3 = MessageSendService.<> o__12.<> p__2.Target;
                //    CallSite<> p__3 = MessageSendService.<> o__12.<> p__2;
                //    if (MessageSendService.<> o__12.<> p__1 == null)
                //    {
                //        MessageSendService.<> o__12.<> p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageSendService), new CSharpArgumentInfo[]
                //        {
                //            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                //        }));
                //    }
                //    flag2 = target(<> p__, target2(<> p__2, arg2, target3(<> p__3, MessageSendService.<> o__12.<> p__1.Target(MessageSendService.<> o__12.<> p__1, arg), null)));
                //}
                //else
                //{
                //    flag2 = true;
                //}
                bool flag3 = flag2;
                if (!flag3)
                {
                    while (this._activeListener)
                    {
                        Thread.Sleep(MessageSendService.TimeSpan);
                        try
                        {
                            object obj3 = this.CreateMessage();
                            //	bool flag4 = obj3 == null;
                            //	if (!flag4)
                            //	{
                            //		string text = JsonConvert.SerializeObject(obj3);
                            //		bool flag5 = text == null;
                            //		if (!flag5)
                            //		{
                            //			byte[] bytes = Encoding.UTF8.GetBytes(text);
                            //			if (MessageSendService.<>o__12.<>p__7 == null)
                            //			{
                            //				MessageSendService.<>o__12.<>p__7 = CallSite<Action<CallSite, object, byte[]>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Send", null, typeof(MessageSendService), new CSharpArgumentInfo[]
                            //				{
                            //					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                            //				}));
                            //			}
                            //			Action<CallSite, object, byte[]> target4 = MessageSendService.<>o__12.<>p__7.Target;
                            //			CallSite <>p__4 = MessageSendService.<>o__12.<>p__7;
                            //			if (MessageSendService.<>o__12.<>p__6 == null)
                            //			{
                            //				MessageSendService.<>o__12.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageSendService), new CSharpArgumentInfo[]
                            //				{
                            //					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                            //				}));
                            //			}
                            //			target4(<>p__4, MessageSendService.<>o__12.<>p__6.Target(MessageSendService.<>o__12.<>p__6, arg), bytes);
                            //		}
                            //	}
                        }
                        catch (SocketException ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            break;
                        }
                    }
                    //if (MessageSendService.<> o__12.<> p__8 == null)
                    //{
                    //    MessageSendService.<> o__12.<> p__8 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(MessageSendService), new CSharpArgumentInfo[]
                    //    {
                    //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                    //    }));
                    //}
                    //MessageSendService.<> o__12.<> p__8.Target(MessageSendService.<> o__12.<> p__8, arg);
                }
            }
        }

        protected virtual object CreateMessage()
        {
            return null;
        }

        private static readonly int TimeSpan = 1000;

        private readonly ProtocolType _pt = ProtocolType.Unknown;

        private bool _activeListener = true;

        private readonly CancellationTokenSource _cs = new CancellationTokenSource();

        private readonly TcpListener _tcpListener;

        private UdpClient _udpClient;
    }
}