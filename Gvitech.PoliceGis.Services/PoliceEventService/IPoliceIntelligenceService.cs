using System.Collections.Generic;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public interface IPoliceIntelligenceService
    {
        List<PoliceIntelligenceModel> GetPoliceIntelligences();

        bool SatrtReceive();

        bool StopReceive();
    }
}