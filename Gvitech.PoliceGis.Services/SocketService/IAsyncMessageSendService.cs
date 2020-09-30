namespace Mmc.Mspace.Services.SocketService
{
    internal interface IAsyncMessageSendService
    {
        void StartAsyncSendMessage();

        void StopAsyncSendMessage();
    }
}