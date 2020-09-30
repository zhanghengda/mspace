using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public class CaseHttpService : Singleton<CaseHttpService>, ICaseHttpService
    {
        public List<CaseInfo> GetCaseInfos(DateTime startTime, DateTime endTime, int curPage = 1, int pageCapacity = 10)
        {
            List<CaseInfo> result = null;
            HttpResult<HttpCaseInfos> httpResult = HttpServiceUtil.RequestResult<HttpCaseInfos>("案件信息服务", string.Format("[\"{0}\",{1},{2}]", endTime.ToString("yyyyMMddHHmmss"), curPage, pageCapacity), CaseHttpService.typeDny);
            bool flag = httpResult != null;
            if (flag)
            {
                result = httpResult.RequestResult.InfoAJLBs;
            }
            return result;
        }

        public static CaseHttpService GetDefault(object obj)
        {
            return Singleton<CaseHttpService>.Instance;
        }

        private static Dictionary<string, Type> typeDny = new Dictionary<string, Type>
        {
            {
                "infoAJLBs",
                typeof(CaseInfo)
            }
        };
    }
}