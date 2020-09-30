namespace Mmc.Mspace.Const.ConstDataInterface
{
    /// <summary>
    /// const for grid patrol interface
    /// </summary>
    public class GridPatrolInterface
    {
        public const string PatrolGridListInf = "/api/inspection-area/getlist";
        public const string PatrolmanListOfAreaInf = "/api/inspector/getlist-by-area";
        public const string RecordDateOfPatrolmanInf = "/api/inspector/inspection-date";
        public const string DataListOfPatrolmanInf = "/api/inspector/getlist-by-phone";
        public const string SinglePatrolmanInf = "/api/inspector/position";
        public const string AllonlinePatrolmanInf = "/api/inspector/allonline"; 
        public const string OnlinePatrolByAreaInf = "/api/inspector/onlinebyarea";
        public const string KeyWordOnlinesearch = "/api/inspector/search";
        public const string GetEventreportInf = "/api/eventreport/getlist";
        public const string GetDhcamera = "/api/dhcamera/list";
        public const string GetGridlist = "/api/grid/list";
        public const string GetGridinfolist = "/api/grid/getcount";

    }
}
