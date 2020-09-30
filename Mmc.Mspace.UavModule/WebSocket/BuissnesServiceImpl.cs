using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Common.Messenger;

namespace Mmc.Mspace.UavModule.WebSocket
{
    class BuissnesServiceImpl : WebSocketService
    {
        public void onReceive(string msg)
        {
            Console.WriteLine("BuissnesServiceImpl  " + msg);
            Messenger.Messengers.Notify("websocketReceivedMessage", msg);


        }
    }
}
