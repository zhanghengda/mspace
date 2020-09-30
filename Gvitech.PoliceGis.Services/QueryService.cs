using Gvitech.CityMaker.FdeGeometry;
using Mmc.Windows.Design;

namespace Mmc.Mspace.Services
{
    public class QueryService : Singleton<QueryService>, IQueryService
    {
        public string WhereClause { get; set; }

        public IGeometry Geomtry { get; set; }

        public static QueryService GetDefault(object args = null)
        {
            return Singleton<QueryService>.Instance;
        }
    }
}