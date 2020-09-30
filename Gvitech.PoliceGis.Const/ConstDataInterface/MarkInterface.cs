namespace Mmc.Mspace.Const.ConstDataInterface
{
    /// <summary>
    /// const for mark interface
    /// </summary>
    public class MarkInterface
    {
        // interface for operating mark 
        public const string GetMarksListInf = "/api/marker/get-list";

        public const string GetFilterMarksListInf = "/api/marker/get-list";

        public const string GetMarksTagsUpdateInf = "/api/marker/addtag";

        public const string DeleteMarkInf = "/api/marker/dropmarker";
        public const string AddMarkInf = "/api/marker/setmarker";
        public const string UpdateMarkInf = "/api/marker/syncmarker";
        public const string GetMarkQueryAreaCollectionInf = "/api/areamanage/list";
        public const string AddQueryWktGroup = "/api/areamanage/add";
        public const string DeleteQueryWktGroup = "/api/areamanage/del";
        public const string QueryWktGroupListCount = "/api/areamanage/listcount";
        public const string QueryWktGroupUpdateName = "/api/areamanage/up";

        // interface for operating tag of poi
        public const string PoiDeleteTagInf = "/api/marker/droptag";
        public const string PoiAddTagInf = "/api/marker/addtag";

        // interface for operating image of mark 
        public const string UploadImgInf = "/api/marker/getloadfile";

        // interface for operating tag
        public const string GetTagsListInf = "/api/tag/list";
        public const string DeleteTagInf = "/api/tag/drop";
        public const string AddTagInf = "/api/tag/add";
        public const string UpdateTagInf = "/api/tag/edit";

        public const string PoiTypesListInf = "/api/category/list";

        // interface for operating accout
        //public static readonly string GetAccountsListInf = "/api/account/row";
        public static readonly string GetAccountInf = "/api/account/row";
        public static readonly string DeleteAccountInf = "/api/account/delete";
        public static readonly string AddAccountInf = "/api/account/create";
        public static readonly string UpdateAccountInf = "/api/account/update";

        public static readonly string GetAccountsListInf = "/api/account/list";
        public static readonly string Getbglist = "/api/account/getbglist";
        public static readonly string DelMyReport = "/api/account/setbgdel";

        public static readonly string DeleteRoutePlanInf = "/api/flight-course/del-flight";
        public static readonly string GetRoutePlanListInf = "/api/flight-course/get-flight-list";
        public static readonly string RoutePlanSendEmail = "/api/flight-course/send-email";

        // interface for download report
        public const string MarksStatisticsReportInf = "/api/marker/download-report";
        public const string MarksStatisticsReportbyGeomInf = "/api/account/wordbygeom";
        public const string DownloadMarksReportInf = "/api/account/exportword";
        public const string PreviewMarksReportInf = "/api/account/exportwordpreview";//预览标注排序结果
        public const string DownloadOriReport = "/api/account/bgdownload";//新导出报告接口
        public const string GetUserReportCenterListNum = "/api/account/getbgcount";
        public const string UserReportCenterUpdatebg = "/api/account/updatebg";

        public const string DownloadExcelAccountReportInf = "/api/account/marker-export";
        

        public static readonly string GetNetMapListInf = "/api/user/get-service-map-info";
       
    }
}
