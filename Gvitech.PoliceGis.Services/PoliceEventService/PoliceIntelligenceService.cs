using Mmc.Windows.Design;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public class PoliceIntelligenceService : Singleton<PoliceIntelligenceService>, IPoliceIntelligenceService
    {
        public PoliceIntelligenceService()
        {
            this._policeIntelligences = new List<PoliceIntelligenceModel>();
            this.SatrtReceive();
        }

        public bool SatrtReceive()
        {
            this.StopReceive();
            this._sps = new SendPoliceIntelligenceService(ConfigurationManager.AppSettings["SocketIP"], 19899, ProtocolType.Udp);
            this._sps.StartSendMessage();
            this._rps = new ReceivePoliceIntelligenceService<PoliceIntelligenceModel>(ConfigurationManager.AppSettings["SocketIP"], 19899, ProtocolType.Udp);
            this._rps.StartReceiveMessage();
            return true;
        }

        public bool StopReceive()
        {
            bool flag = this._sps != null;
            if (flag)
            {
                this._sps.StopSendMessage();
            }
            bool flag2 = this._rps != null;
            if (flag2)
            {
                this._rps.StopReceiveMessage();
            }
            return true;
        }

        public List<PoliceIntelligenceModel> GetPoliceIntelligences()
        {
            return this._rps.ReceivedMessages;
        }

        public static IPoliceIntelligenceService GetDefault(object param)
        {
            return Singleton<PoliceIntelligenceService>.Instance;
        }

        private List<PoliceIntelligenceModel> _policeIntelligences;

        private ReceivePoliceIntelligenceService<PoliceIntelligenceModel> _rps;

        private SendPoliceIntelligenceService _sps;
    }
}