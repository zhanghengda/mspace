using Mmc.Mspace.Common.Enum;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Models.RoutePlanning;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RoutePlanning.Utils
{
    public class RoutePlanHelper: Singleton<RoutePlanHelper>
    {


        public Action<int> RoutePlanCount;

        public List<RoutePlanModel> GetRoutePlanList(int orderBy,string orderName,string searchRouteCondition,int pageSize, int page)
        {
            List<RoutePlanModel> result = new List<RoutePlanModel>();


            string inf = MarkInterface.GetRoutePlanListInf;
            string paras = JsonUtil.SerializeToString(new { orderBy, orderName,filter=searchRouteCondition, pageSize, page });
            string resStr = HttpServiceHelper.Instance.PostRequestForData(inf, paras);

            var data = JsonUtil.DeserializeFromString<dynamic>(resStr);
            if(RoutePlanCount!=null)
            {
                RoutePlanCount(Convert.ToInt32(data?.count));
            }
            var dataStr = JsonUtil.SerializeToString(data?.list);
            var temp = JsonUtil.DeserializeFromString<List<RouteInfo>>(dataStr);

            if (temp?.Count > 0)
            {
                foreach (var item in temp)
                {
                    result.Add(AccountConvert(item));
                }
            }
            return result;
        }

        public string RoutePlanSendEmail(string id,string phone)
        {
            
            string inf = MarkInterface.RoutePlanSendEmail;
            string paras = JsonUtil.SerializeToString(new { id,phone });
            string resStr = HttpServiceHelper.Instance.PostRequestForMessage(inf, paras);

            var data = JsonUtil.DeserializeFromString<dynamic>(resStr);

            return data;
        }

        public bool DeleteRoutePlan(string id)
        {
            string deleteRoutePlanApi =  MarkInterface.DeleteRoutePlanInf;

            var jsonData = JsonUtil.SerializeToString(new { id });

            bool isSuccess = HttpServiceHelper.Instance.PostRequestForStatus(deleteRoutePlanApi, jsonData);
            return isSuccess;
        }

        private RoutePlanModel AccountConvert(RouteInfo accountModel)
        {
            RoutePlanModel routePlanModel = new RoutePlanModel();
            routePlanModel.MeasurementAreaType = (MeasurementAreaType)accountModel.testing_area_type;
            routePlanModel.RouteID = accountModel.id;
            routePlanModel.RouteAddTime = accountModel.addtime;
            routePlanModel.RouteType = (RouteType)accountModel.voyage_type;
            routePlanModel.RouteName = accountModel.name;
            routePlanModel.NumberofWayPoints = accountModel.voyage_point_num;
            routePlanModel.WorkingArea = accountModel.area;
            routePlanModel.EstimatedTime = accountModel.voyage_time;
            routePlanModel.EstimatedRange = accountModel.voyage;
            routePlanModel.RouteCourseJson = accountModel.flight_course_json;

            return routePlanModel;
        }
    }
}
