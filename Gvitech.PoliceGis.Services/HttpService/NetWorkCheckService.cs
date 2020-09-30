using Mmc.Windows.Design;
using System.Net.NetworkInformation;
using System.Text;

namespace Mmc.Mspace.Services.HttpService
{
    public class NetWorkCheckService : Singleton<NetWorkCheckService>, INetWorkCheckService
    {
        public static NetWorkCheckService GetDefault(object obj)
        {
            return Singleton<NetWorkCheckService>.Instance;
        }

        public bool IsUnobstructed(string address)
        {
            bool flag = string.IsNullOrEmpty(address);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                Ping ping = new Ping();
                PingOptions pingOptions = new PingOptions();
                pingOptions.DontFragment = true;
                string s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                int timeout = 120;
                PingReply pingReply = ping.Send(address, timeout, bytes, pingOptions);
                result = (pingReply.Status == IPStatus.Success);
            }
            return result;
        }
    }
}