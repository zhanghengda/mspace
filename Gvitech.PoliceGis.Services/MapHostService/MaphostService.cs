using Mmc.Windows.Design;
using System.Windows;

namespace Mmc.Mspace.Services.MapHostService
{
    public class MaphostService : Singleton<MaphostService>, IMaphostService
    {
        public Window MapWindow { get; set; }

        public static IMaphostService GetDefault(object args = null)
        {
            return Singleton<MaphostService>.Instance;
        }
    }
}