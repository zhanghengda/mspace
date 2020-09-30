using Mmc.Mspace.Models.MovePoi;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IPoliceHttpService
    {
        PoliceManInfo GetPoliceMan(string id);

        PoliceCarInfo GetPoliceCar(string id);
    }
}