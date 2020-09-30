using Mmc.Mspace.Models.MovePoi;

namespace Mmc.Mspace.Services.MovePoiService
{
    public interface IOracleDataService
    {
        void InitEnv();

        PoliceManInfo SearchPoliceManInfo(string policeId);

        PoliceCarInfo SearchPoliceCarInfo(string policecarId);
    }
}