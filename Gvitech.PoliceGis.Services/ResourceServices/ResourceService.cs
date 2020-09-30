using Mmc.Windows.Design;

namespace Mmc.Mspace.Services.ResourceServices
{
    public class ResourceService : Singleton<ResourceService>, IResourceService
    {
        public string GetImagePath()
        {
            return "pack://siteoforigin:,,,/Resources/";
        }

        private const string ImagePrefix = "pack://siteoforigin:,,,/Resources/";
    }
}