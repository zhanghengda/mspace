using System.Collections.Generic;

namespace Mmc.Mspace.Services.NavigationService
{
    public interface INavigationService
    {
        List<CameraTourData> GetCameraTours();

        List<LocationScene> GetLocationScenes();
    }
}