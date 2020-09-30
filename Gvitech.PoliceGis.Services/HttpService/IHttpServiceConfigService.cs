using Mmc.Mspace.Models.HttpResult;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IHttpServiceConfigService
    {
        bool EnableCacheData { get; }

        HttpServiceParam GetServiceParam(string serviceName);
    }
}