using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.SocketService
{
    public class MessageReceiveService<T> : IMessageReceiveService<T>
    {
        public MessageReceiveService(string ipAddress, int port, ProtocolType pt)
        {
            bool flag = string.IsNullOrEmpty(ipAddress);
            if (flag)
            {
                throw new ArgumentNullException("ipAddress");
            }
            bool flag2 = port < 0;
            if (flag2)
            {
                throw new ArgumentException(" port should not less than zero");
            }
            this._pt = pt;
            IPAddress ipaddress = null;
            bool flag3 = !IPAddress.TryParse(ipAddress, out ipaddress);
            if (flag3)
            {
                throw new ArgumentException(" ipAddress invalid");
            }
            if (pt != ProtocolType.Tcp)
            {
                if (pt == ProtocolType.Udp)
                {
                    this._udpClient = new UdpClient(port);
                }
            }
            else
            {
                this._tcpClient = new TcpClient(ipAddress, port);
            }
            this.ReceivedMessages = new List<T>();
        }

        public List<T> ReceivedMessages { get; protected set; }

        public void StartReceiveMessage()
        {
            ProtocolType pt = this._pt;
            if (pt != ProtocolType.Tcp)
            {
                if (pt == ProtocolType.Udp)
                {
                    new Task(new Action<object>(this.ReceiveMessage), this._udpClient, this._cs.Token).Start();
                }
            }
            else
            {
                new Task(new Action<object>(this.ReceiveMessage), this._tcpClient, this._cs.Token).Start();
            }
        }

        public void StopReceiveMessage()
        {
            bool flag = this._cs != null;
            if (flag)
            {
                this._cs.Cancel();
            }
        }

        protected virtual void ReceiveMessage(object obj)
        {
            /*
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
				if (MessageReceiveService<T>.<>o__12.<>p__0 == null)
				{
					MessageReceiveService<T>.<>o__12.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				object obj2 = MessageReceiveService<T>.<>o__12.<>p__0.Target(MessageReceiveService<T>.<>o__12.<>p__0, arg, null);
				if (MessageReceiveService<T>.<>o__12.<>p__5 == null)
				{
					MessageReceiveService<T>.<>o__12.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				bool flag2;
				if (!MessageReceiveService<T>.<>o__12.<>p__5.Target(MessageReceiveService<T>.<>o__12.<>p__5, obj2))
				{
					if (MessageReceiveService<T>.<>o__12.<>p__4 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> target = MessageReceiveService<T>.<>o__12.<>p__4.Target;
					CallSite <>p__ = MessageReceiveService<T>.<>o__12.<>p__4;
					if (MessageReceiveService<T>.<>o__12.<>p__3 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object, object> target2 = MessageReceiveService<T>.<>o__12.<>p__3.Target;
					CallSite <>p__2 = MessageReceiveService<T>.<>o__12.<>p__3;
					object arg2 = obj2;
					if (MessageReceiveService<T>.<>o__12.<>p__2 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, object, object> target3 = MessageReceiveService<T>.<>o__12.<>p__2.Target;
					CallSite <>p__3 = MessageReceiveService<T>.<>o__12.<>p__2;
					if (MessageReceiveService<T>.<>o__12.<>p__1 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					flag2 = target(<>p__, target2(<>p__2, arg2, target3(<>p__3, MessageReceiveService<T>.<>o__12.<>p__1.Target(MessageReceiveService<T>.<>o__12.<>p__1, arg), null)));
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (!flag3)
				{
					if (MessageReceiveService<T>.<>o__12.<>p__7 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__7 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReceiveBufferSize", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					Func<CallSite, object, int, object> target4 = MessageReceiveService<T>.<>o__12.<>p__7.Target;
					CallSite <>p__4 = MessageReceiveService<T>.<>o__12.<>p__7;
					if (MessageReceiveService<T>.<>o__12.<>p__6 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					target4(<>p__4, MessageReceiveService<T>.<>o__12.<>p__6.Target(MessageReceiveService<T>.<>o__12.<>p__6, arg), 1024);
					if (MessageReceiveService<T>.<>o__12.<>p__10 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__10 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertArrayIndex, typeof(int), typeof(MessageReceiveService<T>)));
					}
					Func<CallSite, object, int> target5 = MessageReceiveService<T>.<>o__12.<>p__10.Target;
					CallSite <>p__5 = MessageReceiveService<T>.<>o__12.<>p__10;
					if (MessageReceiveService<T>.<>o__12.<>p__9 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ReceiveBufferSize", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, object> target6 = MessageReceiveService<T>.<>o__12.<>p__9.Target;
					CallSite <>p__6 = MessageReceiveService<T>.<>o__12.<>p__9;
					if (MessageReceiveService<T>.<>o__12.<>p__8 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					byte[] array = new byte[target5(<>p__5, target6(<>p__6, MessageReceiveService<T>.<>o__12.<>p__8.Target(MessageReceiveService<T>.<>o__12.<>p__8, arg)))];
					while (this._activeClient)
					{
						try
						{
							if (MessageReceiveService<T>.<>o__12.<>p__13 == null)
							{
								MessageReceiveService<T>.<>o__12.<>p__13 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(MessageReceiveService<T>)));
							}
							Func<CallSite, object, int> target7 = MessageReceiveService<T>.<>o__12.<>p__13.Target;
							CallSite <>p__7 = MessageReceiveService<T>.<>o__12.<>p__13;
							if (MessageReceiveService<T>.<>o__12.<>p__12 == null)
							{
								MessageReceiveService<T>.<>o__12.<>p__12 = CallSite<Func<CallSite, object, byte[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Receive", null, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							Func<CallSite, object, byte[], object> target8 = MessageReceiveService<T>.<>o__12.<>p__12.Target;
							CallSite <>p__8 = MessageReceiveService<T>.<>o__12.<>p__12;
							if (MessageReceiveService<T>.<>o__12.<>p__11 == null)
							{
								MessageReceiveService<T>.<>o__12.<>p__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Client", typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							int num = target7(<>p__7, target8(<>p__8, MessageReceiveService<T>.<>o__12.<>p__11.Target(MessageReceiveService<T>.<>o__12.<>p__11, arg), array));
							bool flag4 = num == 0;
							if (!flag4)
							{
								string @string = Encoding.UTF8.GetString(array, 0, num);
								List<string> list = this.MatchMessage(@string);
								bool flag5 = list != null;
								if (flag5)
								{
									list.ForEach(delegate(string msg)
									{
										T t = this.ResolveMessage(msg);
										Console.WriteLine(t.ToString());
										CollectionExtension.AddEx<T>(this.ReceivedMessages, t);
									});
								}
							}
						}
						catch (Exception ex)
						{
							Console.Write(ex.StackTrace);
						}
					}
					if (MessageReceiveService<T>.<>o__12.<>p__14 == null)
					{
						MessageReceiveService<T>.<>o__12.<>p__14 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", null, typeof(MessageReceiveService<T>), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					MessageReceiveService<T>.<>o__12.<>p__14.Target(MessageReceiveService<T>.<>o__12.<>p__14, arg);
				}
			}*/
        }

        protected virtual T ResolveMessage(string receivedMessage)
        {
            return default(T);
        }

        protected virtual List<string> MatchMessage(string messages)
        {
            bool flag = string.IsNullOrEmpty(messages);
            List<string> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                Regex regex = new Regex("\\{[^\\{^\\}]*\\}", RegexOptions.Compiled);
                MatchCollection matchCollection = regex.Matches(messages, 0);
                bool flag2 = matchCollection.Count < 1;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    List<string> list = new List<string>();
                    int num;
                    for (int i = matchCollection.Count - 1; i >= 0; i = num - 1)
                    {
                        string value = matchCollection[i].Value;
                        bool flag3 = string.IsNullOrEmpty(value);
                        if (!flag3)
                        {
                            CollectionExtension.AddEx<string>(list, value);
                        }
                        num = i;
                    }
                    result = list;
                }
            }
            return result;
        }

        private readonly bool _activeClient = true;

        private readonly CancellationTokenSource _cs = new CancellationTokenSource();

        private readonly ProtocolType _pt;

        private readonly TcpClient _tcpClient;

        private readonly UdpClient _udpClient;
    }
}