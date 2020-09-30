using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System.Configuration;
using System.Windows.Forms;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpServiceConfigService : Singleton<HttpServiceConfigService>, IHttpServiceConfigService
    {
        public bool EnableCacheData { get; private set; }

        public HttpServiceConfigService()
        {
            this.Config = ConfigHelper<HttpServiceConfig>.ResovleConfigFromFile(Application.StartupPath + HttpServiceConfigService.configPath);
            this.EnableCacheData = StringExtension.ParseTo<bool>(ConfigurationManager.AppSettings["EnableCacheData"], false);
        }

        public static IHttpServiceConfigService GetDefault(object args = null)
        {
            return Singleton<HttpServiceConfigService>.Instance;
        }

        public HttpServiceParam GetServiceParam(string serviceName)
        {
            bool flag = this.Config != null && this.Config.IsValid();
            HttpServiceParam result;
            if (flag)
            {
                result = this.Config.GetServiceParam(serviceName);
            }
            else
            {
                result = null;
            }
            return result;
        }

        private readonly HttpServiceConfig Config;

        private static readonly string configPath = "\\config\\ServiceConfig.xml";
    }
}