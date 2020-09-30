using Gvitech.CityMaker.FdeGeometry;

namespace Mmc.Mspace.Services
{
    public interface IQueryService
    {
        string WhereClause { get; set; }

        IGeometry Geomtry { get; set; }
    }
}