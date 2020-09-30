using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.Models.Human;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public class HumanHttpService : Singleton<HumanHttpService>, IHumanHttpService
    {
        public static HumanHttpService GetDefault(object obj)
        {
            return Singleton<HumanHttpService>.Instance;
        }

        public HumanHttpService()
        {
            this.httpService = new HttpService();
        }

        public List<PopulationInfo> GetPeopleInfos(string address)
        {
            bool flag = string.IsNullOrEmpty(address);
            List<PopulationInfo> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                List<PopulationInfo> list = new List<PopulationInfo>();
                try
                {
                    int num = 1;
                    int num2 = num;
                    num = num2 + 1;
                    List<PopulationInfo> peopleInfos = this.GetPeopleInfos(address, num2, 50);
                    bool flag2 = IEnumerableExtension.HasValues<PopulationInfo>(peopleInfos);
                    if (flag2)
                    {
                        list.AddRange(peopleInfos);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log("解析人口信息异常", LogMessageType.ERROR);
                    SystemLog.Log(ex);
                }
                result = list;
            }
            return result;
        }

        private List<PopulationInfo> GetPeopleInfos(string address, int curPage, int pageCapacity)
        {
            List<PopulationInfo> list = null;
            bool flag = string.IsNullOrEmpty(address);
            List<PopulationInfo> result;
            if (flag)
            {
                result = list;
            }
            else
            {
                HttpResult<HttpPopulationInfos> httpResult = HttpServiceUtil.RequestResult<HttpPopulationInfos>("人口信息服务", string.Format("[\"{0}\",{1},{2}]", address, curPage, pageCapacity), HumanHttpService.typeDny);
                bool flag2 = httpResult != null;
                if (flag2)
                {
                    list = httpResult.RequestResult.InfoRKXXs;
                }
                result = list;
            }
            return result;
        }

        private IHttpService httpService = null;

        private static string populationUrl = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_018";

        private static Dictionary<string, Type> typeDny = new Dictionary<string, Type>
        {
            {
                "infoRKXXs",
                typeof(PopulationInfo)
            }
        };
    }
}