using Mmc.Mspace.Models.Human;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IHumanHttpService
    {
        List<PopulationInfo> GetPeopleInfos(string address);
    }
}