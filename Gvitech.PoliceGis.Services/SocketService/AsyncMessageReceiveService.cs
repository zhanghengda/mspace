using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.SocketService
{
    public class AsyncMessageReceiveService<T> : IAsyncMessageReceiveService<T>
    {
        public List<T> ReceivedMessages
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void StartAsyncReceiveMessage()
        {
            throw new NotImplementedException();
        }

        public void StopAsyncReceiveMessage()
        {
            throw new NotImplementedException();
        }
    }
}