using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule
{
    public class DeviceDataManager
    {
        private string _poiHost;
        private HttpService _httpService;
        
        public DeviceDataManager()
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _poiHost = json.poiUrl;
            _httpService = new HttpService();
            _httpService.Token = HttpServiceUtil.Token;
        }

        public void GetDeviceInfoList(string deviceType,ref Dictionary<string, DeviceInfoModel> _deviceInfoList,bool isStable=true)
        {
            try
            {
                string url = string.Format("{0}/api/monitoring-data/alldevicedata", _poiHost);
                if (!string.IsNullOrEmpty(deviceType))
                    url = string.Format("{0}?device_type={1}", url, deviceType);
                var result = _httpService.HttpRequestAsync(url);
                var resDyn = JsonUtil.DeserializeFromString<dynamic>(result);
                if (resDyn.status == 1)
                {
                    var data = resDyn.data;
                    if (data != null)
                        foreach (var i in data)
                        {
                            try
                            {
                                var temp = new DeviceInfoModel();
                                temp.device = Convert.ToString(i.device);
                                temp.device_name = i.device_name;
                                temp.device_type = i.device_type;
                                temp.longitude = i.longitude == null ? 0 : Convert.ToDouble(i.longitude);
                                temp.latitude = i.latitude == null ? 0 : Convert.ToDouble(i.latitude);
                                temp.altitude = i.altitude == null ? 0 : Convert.ToDouble(i.altitude);
                                temp.datainfo = i.datainfo;
                                string key = string.Empty;
                                if (isStable)
                                    key = temp.longitude.ToString() + temp.latitude.ToString();
                                else
                                    key = temp.device;
                                if (!_deviceInfoList.ContainsKey(key))
                                    _deviceInfoList.Add(key, temp);
                                else
                                    _deviceInfoList[key] = temp;
                            }
                            catch { }
                        }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }

        }
    }
}
