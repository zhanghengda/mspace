using Mmc.Mspace.Services.SocketService;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public class ReceivePoliceIntelligenceService<T> : MessageReceiveService<T>
    {
        public ReceivePoliceIntelligenceService(string ipAddress, int port, ProtocolType pt) : base(ipAddress, port, pt)
        {
        }

        protected override T ResolveMessage(string receivedMessage)
        {
            bool flag = string.IsNullOrEmpty(receivedMessage);
            T result;
            if (flag)
            {
                result = default(T);
            }
            else
            {
                result = JsonConvert.DeserializeObject<T>(receivedMessage);
            }
            return result;
        }
    }
}