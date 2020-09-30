using Mmc.Mspace.Models.MovePoi;
using Mmc.Mspace.Services.SocketService;
using System;
using System.Net.Sockets;

namespace Mmc.Mspace.Services.MovePoiService
{
    public class SendMovePoiliceService : AsyncMessageSendService
    {
        public SendMovePoiliceService(string host, int port, ProtocolType pt) : base(host, port, pt)
        {
        }

        protected override object CreateMessage()
        {
            bool flag = SendMovePoiliceService.rm == null;
            if (flag)
            {
                SendMovePoiliceService.rm = new RecevieMessage();
            }
            SendMovePoiliceService.rm.msgType = 0;
            SendMovePoiliceService.rm.deviceId = this.CreateDeviceId().ToString();
            SendMovePoiliceService.rm.deviceType = ((this.rdm.Next(0, 5) == 3) ? "car" : "equipment2");
            float longti;
            float lati;
            this.CreateRandomCoordinate(out longti, out lati);
            SendMovePoiliceService.rm.longti = longti;
            SendMovePoiliceService.rm.lati = lati;
            SendMovePoiliceService.rm.dateTime = DateTime.Now.ToLongTimeString();
            return SendMovePoiliceService.rm;
        }

        private void CreateRandomCoordinate(out float x, out float y)
        {
            //x = (float)this.rdm.Next(106200, 106300) / 1000f;
            //y = (float)this.rdm.Next(38400, 38500) / 1000f;
            x = (float)this.rdm.Next(49398400, 49908400) / 100f;
            y = (float)this.rdm.Next(248995900, 250005900) / 100f;
        }

        private int CreateDeviceId()
        {
            return this.rdm.Next(10000, 12500);
        }

        private static RecevieMessage rm;

        private readonly Random rdm = new Random();
    }
}