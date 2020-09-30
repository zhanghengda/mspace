using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.SocketService
{
    public class AsyncMessageSendService : IAsyncMessageSendService
    {
        public ProtocolType ProType { get; private set; }

        public IPAddress IpAddress { get; private set; }

        public int Port { get; private set; }

        public AsyncMessageSendService(string host, int port, ProtocolType protocolType)
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
            this.ProType = protocolType;
            IPAddress ipaddress;
            this.IpAddress = (IPAddress.TryParse(host, out ipaddress) ? ipaddress : null);
            this.Port = port;
        }

        public void StartAsyncSendMessage()
        {
            ProtocolType proType = this.ProType;
            if (proType != ProtocolType.Tcp)
            {
                if (proType == ProtocolType.Udp)
                {
                    this.UdpClient = new UdpClient(19999);
                    this.UdpClient.Connect(Dns.GetHostName(), this.Port);
                }
            }
            else
            {
                bool flag = this.TcpListener == null;
                if (flag)
                {
                    this.TcpListener = new TcpListener(this.IpAddress, this.Port);
                }
                new Task(new Action(this.SartAsyncSendTcpMessage)).Start();
            }
        }

        private void SartAsyncSendTcpMessage()
        {
            bool flag = this.TcpListener == null;
            if (!flag)
            {
                try
                {
                    this.TcpListener.Start(10);
                }
                catch (Exception ex)
                {
                    return;
                }
                int num = 2000;
                TcpClient client = this.TcpListener.AcceptTcpClient();
                bool flag2 = this.SendtcpTimer == null;
                if (flag2)
                {
                    this.SendtcpTimer = new Timer(async delegate (object state)
                    {
                        bool flag3 = !client.Connected;
                        if (flag3)
                        {
                            await client.ConnectAsync(this.IpAddress, this.Port);
                        }
                        if (client.Client == null)
                        {
                            client.Close();
                        }
                        else
                        {
                            object message = this.CreateMessage();
                            string data = JsonConvert.SerializeObject(message);
                            if (data != null)
                            {
                                byte[] msg = Encoding.UTF8.GetBytes(data);
                                ArraySegment<byte> arryseg = new ArraySegment<byte>(msg);
                                SocketAsyncEventArgs socketArgs = new SocketAsyncEventArgs
                                {
                                    BufferList = new List<ArraySegment<byte>>
                                    {
                                        arryseg
                                    }
                                };
                                client.Client.SendAsync(socketArgs);
                            }
                        }
                    }, null, num, num);
                }
            }
        }

        public void StopAsyncSendMessage()
        {
            bool flag = this.SendtcpTimer != null;
            if (flag)
            {
                this.SendtcpTimer.Change(-1, -1);
            }
        }

        protected virtual object CreateMessage()
        {
            return null;
        }

        protected TcpListener TcpListener;

        protected UdpClient UdpClient;

        protected Timer SendtcpTimer;
    }
}