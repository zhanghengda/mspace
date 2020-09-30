using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.Models.MovePoi;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Mspace.Services.HttpService
{
    public class PoliceHttpService : Singleton<PoliceHttpService>, IPoliceHttpService
    {
        public static PoliceHttpService GetDefault(object obj)
        {
            return Singleton<PoliceHttpService>.Instance;
        }

        public PoliceHttpService()
        {
            this.httpService = new HttpService();
            PoliceHttpService.Policemen = new List<PoliceManInfo>();
            PoliceHttpService.Policecars = new List<PoliceCarInfo>();
        }

        public PoliceCarInfo GetPoliceCar(string id)
        {
            PoliceCarInfo policeCarInfo = null;
            bool flag = IEnumerableExtension.HasValues<PoliceCarInfo>(PoliceHttpService.Policecars);
            if (flag)
            {
                policeCarInfo = PoliceHttpService.Policecars.Find((PoliceCarInfo p) => !string.IsNullOrEmpty(p.CPH) && p.CPH.Equals(id));
            }
            bool flag2 = policeCarInfo == null;
            if (flag2)
            {
                List<PoliceCarInfo> policeInfo = this.GetPoliceInfo<List<PoliceCarInfo>>(id, "car");
                bool flag3 = IEnumerableExtension.HasValues<PoliceCarInfo>(policeInfo);
                if (flag3)
                {
                    policeCarInfo = policeInfo.FirstOrDefault<PoliceCarInfo>();
                    policeInfo.ForEach(delegate (PoliceCarInfo p)
                    {
                        CollectionExtension.AddEx<PoliceCarInfo>(PoliceHttpService.Policecars, p);
                    });
                }
            }
            return policeCarInfo;
        }

        public PoliceManInfo GetPoliceMan(string id)
        {
            PoliceManInfo policeManInfo = null;
            bool flag = IEnumerableExtension.HasValues<PoliceManInfo>(PoliceHttpService.Policemen);
            if (flag)
            {
                policeManInfo = PoliceHttpService.Policemen.Find((PoliceManInfo p) => !string.IsNullOrEmpty(p.JH) && p.JH.Equals(id));
            }
            bool flag2 = policeManInfo == null;
            if (flag2)
            {
                List<PoliceManInfo> policeInfo = this.GetPoliceInfo<List<PoliceManInfo>>(id, "person");
                bool flag3 = IEnumerableExtension.HasValues<PoliceManInfo>(policeInfo);
                if (flag3)
                {
                    policeManInfo = policeInfo.FirstOrDefault<PoliceManInfo>();
                    policeInfo.ForEach(delegate (PoliceManInfo p)
                    {
                        CollectionExtension.AddEx<PoliceManInfo>(PoliceHttpService.Policemen, p);
                    });
                }
            }
            return policeManInfo;
        }

        private T GetPoliceInfo<T>(string id, string lb) where T : class
        {
            HttpResult<HttpPoliceInfo> httpResult = HttpServiceUtil.RequestResult<HttpPoliceInfo>("警员警车信息服务", string.Format("[\"{0}\",{1}]", lb, id), PoliceHttpService.typeDny);
            bool flag = httpResult == null || httpResult.RequestResult == null;
            T result;
            if (flag)
            {
                result = default(T);
            }
            else
            {
                bool flag2 = IEnumerableExtension.HasValues<PoliceManInfo>(httpResult.RequestResult.InfoSWRYResps) && httpResult.RequestResult.InfoSWRYResps.GetType().Equals(typeof(T));
                if (flag2)
                {
                    result = (httpResult.RequestResult.InfoSWRYResps as T);
                }
                else
                {
                    bool flag3 = IEnumerableExtension.HasValues<PoliceCarInfo>(httpResult.RequestResult.InfoSWCLResps) && httpResult.RequestResult.InfoSWCLResps.GetType().Equals(typeof(T));
                    if (flag3)
                    {
                        result = (httpResult.RequestResult.InfoSWCLResps as T);
                    }
                    else
                    {
                        result = default(T);
                    }
                }
            }
            return result;
        }

        private void GetAllPolicemen()
        {
            bool flag = IEnumerableExtension.HasValues<PoliceManInfo>(PoliceHttpService.Policemen);
            if (!flag)
            {
                HttpResult<HttpPoliceMenInfo> httpResult = HttpServiceUtil.RequestResult<HttpPoliceMenInfo>("警员信息服务", "", PoliceHttpService.typeDny);
                bool flag2 = httpResult != null;
                if (flag2)
                {
                    PoliceHttpService.Policemen = httpResult.RequestResult.InfoJYSJs;
                }
            }
        }

        private IHttpService httpService = null;

        private static string policemanUrl = string.Empty;

        private static string policecarUrl = string.Empty;

        private static Dictionary<string, Type> typeDny = new Dictionary<string, Type>
        {
            {
                "infoJYSJs",
                typeof(PoliceManInfo)
            },
            {
                "infoSWRYResps",
                typeof(PoliceManInfo)
            },
            {
                "infoSWCLResps",
                typeof(PoliceCarInfo)
            }
        };

        private static List<PoliceManInfo> Policemen;

        private static List<PoliceCarInfo> Policecars;
    }
}