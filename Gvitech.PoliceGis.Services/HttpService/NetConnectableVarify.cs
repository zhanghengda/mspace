using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mmc.Mspace.Services.HttpService
{
    public class NetConnectableVarify
    {
        private string _host;
        private int _port;

        public bool IsUrlConnectable(string url)
        {
            bool isConn = false;
            if (GetUriFromUriStr(url))
            {
                string ipStr = getIPFromHost(_host);
                if (!string.IsNullOrEmpty(ipStr))
                {
                    isConn = IsIPConnectable(ipStr, _port);
                }
            }
            return isConn;
        }

        public bool IsTileUrlConnectable(string url)
        {
            bool isConn = false;
            try
            {
                int position = url.IndexOf('@');
                string host = url.Substring(position + 1);

                if (host.Contains(":"))
                {
                    var temp = host.Split(':');
                    _host = temp[0];
                    _port = Convert.ToInt32(temp[1]);
                }
                else
                {
                    GetUriFromUriStr(host);
                }
                string ipStr = getIPFromHost(_host);
                if (!string.IsNullOrEmpty(ipStr))
                {
                    isConn = IsIPConnectable(ipStr, _port);
                }
            }
            catch { }
            return isConn;
        }

        public bool IsModelUrlConnectable(string url)
        {
            bool isConn = false;
            try
            {
                var temp = url.Split(';');
                foreach (var i in temp)
                {
                    if (i.ToLower().Contains("server"))
                        _host = i.Split('=')[1];
                    else if (i.ToLower().Contains("port"))
                        _port = Convert.ToInt32(i.Split('=')[1]);
                    else
                        continue;
                }
                string ipStr = getIPFromHost(_host);
                if (!string.IsNullOrEmpty(ipStr))
                {
                    isConn = IsIPConnectable(ipStr, _port);
                }
            }
            catch { }
            return isConn;
        }

        private bool GetUriFromUriStr(string url)
        {
            bool succeed = false;
            try
            {
                Uri uri = new Uri(url);
                _host = uri.Host;
                _port = uri.Port;
                succeed = true;
            }
            catch { }
            return succeed;
        }


        private string getIPFromHost(string host)
        {
            string tempIp = string.Empty;
            if (!string.IsNullOrEmpty(host))
            {
                if (IsValidIP(host)) tempIp = host;
                else
                {
                    try
                    {
                        IPHostEntry hostinfo = Dns.GetHostEntry(host);
                        IPAddress[] aryIP = hostinfo.AddressList;
                        Console.WriteLine(aryIP[0].ToString());
                        tempIp = aryIP[0].ToString();
                    }
                    catch { }
                }
            }
            return tempIp;
        }

        private bool IsValidIP(string ip)
        {
            bool isValid = false;
            if (Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = ip.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        isValid = true;
                }
            }
            return isValid;
        }

        /// <summary>
        /// ping IP
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        private bool PingIP(string strIP)
        {
            if (!IsValidIP(strIP))
            {
                return false;
            }
            System.Net.NetworkInformation.Ping psender = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply prep = psender.Send(strIP, 500, Encoding.Default.GetBytes("afdafdadfsdacareqretrqtqeqrq8899tu"));
            if (prep.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            return false;
        }

        private bool IsIPConnectable(string ipStr, int port)
        {
            bool isConnectable = false;

            IPAddress ip = IPAddress.Parse(ipStr);
            IPEndPoint point = new IPEndPoint(ip, port);
            using (var client = new TimeOutSocket().Connect(point, 1500))
            {
                isConnectable = client?.Connected ?? false;
                client?.Close();
                Console.WriteLine("连接{0}成功!", point);
            }
            return isConnectable;
        }
    }

    public class TimeOutSocket
    {
        private bool IsConnectionSuccessful = false;
        private Exception socketexception;
        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        public TcpClient Connect(IPEndPoint remoteEndPoint, int timeoutMSec)
        {
            TimeoutObject.Reset();
            socketexception = null;

            string serverip = Convert.ToString(remoteEndPoint.Address);
            int serverport = remoteEndPoint.Port;
            TcpClient tcpclient = new TcpClient();

            tcpclient.BeginConnect(serverip, serverport,
                new AsyncCallback(CallBackMethod), tcpclient);

            try
            {
                if (TimeoutObject.WaitOne(timeoutMSec, false))
                {
                    if (IsConnectionSuccessful)
                    {
                        return tcpclient;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    tcpclient.Close();
                    //throw new TimeoutException("TimeOut Exception");
                    return null;
                }
            }
            catch { throw new Exception(); }

        }
        private void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }
    }
}
