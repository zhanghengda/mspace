using System.Collections.Generic;

namespace Mmc.Mspace.Services.SocketService
{
    public interface IAsyncMessageReceiveService<T>
    {
        List<T> ReceivedMessages { get; }

        void StartAsyncReceiveMessage();

        void StopAsyncReceiveMessage();
    }
}