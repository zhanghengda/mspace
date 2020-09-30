using System.Collections.Generic;

namespace Mmc.Mspace.Services.SocketService
{
    public interface IMessageReceiveService<T>
    {
        List<T> ReceivedMessages { get; }

        void StartReceiveMessage();

        void StopReceiveMessage();
    }
}