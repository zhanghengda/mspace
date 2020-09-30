using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public class SubjectCaseHttpService : Singleton<SubjectCaseHttpService>, ISubjectCaseHttpService
    {
        public static SubjectCaseHttpService GetDefault(object obj)
        {
            return Singleton<SubjectCaseHttpService>.Instance;
        }

        public SubjectCaseHttpService()
        {
            this.httpService = new HttpService();
        }

        public List<SubjectCaseInfo> GetSubjectCaseInfos(DateTime startTime, DateTime endTime, int curPage, int pageCapacity)
        {
            HttpResult<HttpSubjectCaseInfos> httpResult = HttpServiceUtil.RequestResult<HttpSubjectCaseInfos>("专题案件信息服务", string.Format("[{0},{1},{2},{3}]", new object[]
            {
                startTime.ToString("yyyyMMddHHmmss"),
                endTime.ToString("yyyyMMddHHmmss"),
                curPage,
                pageCapacity
            }), SubjectCaseHttpService.typeDny);
            bool flag = httpResult == null || httpResult.RequestResult == null;
            List<SubjectCaseInfo> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = httpResult.RequestResult.InfoZTAJs;
            }
            return result;
        }

        private IHttpService httpService = null;

        private static string subjectCaseUrl = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_020";

        private static Dictionary<string, Type> typeDny = new Dictionary<string, Type>
        {
            {
                "infoZTAJs",
                typeof(SubjectCaseInfo)
            }
        };
    }
}