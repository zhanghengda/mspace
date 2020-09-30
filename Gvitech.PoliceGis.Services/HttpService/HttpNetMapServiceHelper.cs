using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Models.NetMapServiceModel;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpNetMapServiceHelper : Singleton<HttpNetMapServiceHelper>
    {
        public List<NetMapServiceModel> GetNetMapList()
        {
            List<NetMapServiceModel> result = new List<NetMapServiceModel>();


            string inf = MarkInterface.GetNetMapListInf;
            string paras = JsonUtil.SerializeToString(null);
            string resStr = HttpServiceHelper.Instance.PostRequestForData(inf, paras);

            var data = JsonUtil.DeserializeFromString<dynamic>(resStr);

            var dataStr = JsonUtil.SerializeToString(data.serviceType);
            //var dataType = JsonUtil.DeserializeFromString<dynamic>(data);

            var temp = JsonUtil.DeserializeFromString<List<NetMapServiceInfo>>(dataStr);

            if (temp?.Count > 0)
            {
                foreach (var item in temp)
                {
                    result.Add(AccountConvert(item));
                }
            }
            return result;
        }

        private NetMapServiceModel AccountConvert(NetMapServiceInfo netMapServiceInfo)
        {
            NetMapServiceModel netMapServiceModel = new NetMapServiceModel();
            netMapServiceModel.Catalog_id = netMapServiceInfo.catalog_id;
            netMapServiceModel.Name  = netMapServiceInfo.name;
            netMapServiceModel.Config_type = netMapServiceInfo.config_type;
            netMapServiceModel.ServiceData  = netMapServiceInfo.serviceData;

            return netMapServiceModel;
        }
    }
}
