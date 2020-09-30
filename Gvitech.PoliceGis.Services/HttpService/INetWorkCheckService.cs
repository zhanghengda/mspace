namespace Mmc.Mspace.Services.HttpService
{
    public interface INetWorkCheckService
    {
        bool IsUnobstructed(string address);
    }
}