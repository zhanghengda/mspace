using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FireControlModule.FireIot
{
    public class FireIotSevice
    {
        public FireIotSevice()
        {
            InitBuildKey();
        }

        private Dictionary<string, List<string>> buildFireIotKeys;

        private Dictionary<string, Tuple<string, string>> dicNewBuildInfos = new Dictionary<string, Tuple<string, string>>();
        private Dictionary<string, Tuple<string, string>> dicOldBuildInfos = new Dictionary<string, Tuple<string, string>>();

        private Dictionary<string, string> _dicCgqTypes = new Dictionary<string, string>();

        public Dictionary<string, List<string>> BuildFireIotKeys
        {
            get
            {
                return buildFireIotKeys;
            }
        }

        public Dictionary<string, string> DicCgqTypes
        {
            get
            {
                return _dicCgqTypes;
            }
        }

        public Dictionary<string, Tuple<string, string>> DicNewBuildInfos
        {
            get
            {
                return dicNewBuildInfos;
            }
        }

        public Dictionary<string, Tuple<string, string>> DicOldBuildInfos
        {
            get
            {
                return dicOldBuildInfos;
            }
        }

        private void InitBuildKey()
        {
            var DynamicObject = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + @"\data\json\FireIotEvent.json");

            //var DynamicObject = JsonConvert.DeserializeObject<dynamic>(json);
            string key = DynamicObject.buildkey;
            string[] keys = key.Split("|");
            this.buildFireIotKeys = new Dictionary<string, List<string>>();
            foreach (var item in keys)
            {
                var buildcodes = item.Split("_");
                var buildcode = buildcodes[1];
                if (!BuildFireIotKeys.ContainsKey(buildcode))
                    BuildFireIotKeys.Add(buildcode, new List<string>());
                this.BuildFireIotKeys[buildcode].Add(buildcodes[0]);
            }

            //传感器类型
            if (DynamicObject.cgqType == null || DynamicObject.cgqType.Count == 0)
                return;
            foreach (var item in DynamicObject.cgqType)
                this.DicCgqTypes.Add(item.type.Value, item.name.Value);

            //建筑新老编码映射
            if (DynamicObject.buiildInfo == null || DynamicObject.buiildInfo.Count == 0)
                return;
            foreach (var item in DynamicObject.buiildInfo)
            {
                this.DicNewBuildInfos.Add(item.newCode.Value, new Tuple<string, string>(item.oldCode.Value, item.name.Value));
                this.DicOldBuildInfos.Add(item.oldCode.Value, new Tuple<string, string>(item.newCode.Value, item.name.Value));
            }
        }

        public Mmc.Mspace.Models.FireIot.FireIotInfo RequestFireIot(string cgqcode)
        {
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format(@"?buildid={0}&mark={1}&cgqcode={2}", json.sensorOneUrl, "4403050080040510099xxxx", "1", cgqcode);
            //http 请求测试数据
            HttpService httpService = new HttpService();
            var result = httpService.HttpRequestAsync(url);
            {
                var DynamicObject = JsonConvert.DeserializeObject<dynamic>(result);
                if (DynamicObject.content == null)
                    return null;
                var item = DynamicObject.content;
                var fireIot = new Mmc.Mspace.Models.FireIot.FireIotInfo();
                fireIot.addr = item.addr;
                fireIot.type = item.type;
                fireIot.value = item.value;
                fireIot.status = item.status;
                fireIot.name = item.name;
                fireIot.model = item.model;
                fireIot.code = item.code;
                fireIot.cgq_dy = item.cgq_dy;
                fireIot.cgqfz = item.cgqfz;
                fireIot.createdate = item.createdate;
                fireIot.cgq_xhqd = item.cgq_xhqd;
                return fireIot;
            }
        }

        public List<Mmc.Mspace.Models.FireIot.FireIotInfo> GetFireIotByStatus(string Status = "0")
        {
            var allFireIots = GetAllFireIot();
            var fireIots = new List<Mmc.Mspace.Models.FireIot.FireIotInfo>();
            foreach (var item in allFireIots)
            {
                if (item.status == Status)
                    fireIots.Add(item);
            }
            return fireIots;
        }

        public List<Mmc.Mspace.Models.FireIot.FireIotInfo> GetFireIotByBuildCode(string buildCode)
        {
            if (!DicNewBuildInfos.ContainsKey(buildCode) || !BuildFireIotKeys.ContainsKey(DicNewBuildInfos[buildCode].Item1))
                return null;
            var allFireIots = GetAllFireIot();
            var cgqCodes = BuildFireIotKeys[DicNewBuildInfos[buildCode].Item1];
            var fireIots = new List<Mmc.Mspace.Models.FireIot.FireIotInfo>();
            foreach (var item in allFireIots)
            {
                if (cgqCodes.Contains(item.code))
                    fireIots.Add(item);
            }
            return fireIots;
        }

        public List<Mmc.Mspace.Models.FireIot.FireIotInfo> GetAllFireIot()
        {
            var allFireIots = new List<Mmc.Mspace.Models.FireIot.FireIotInfo>();
            //http 请求测试数据
            HttpService httpService = new HttpService();
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format(@"{0}?buildid={1}&mark={2}", json.sensorAllUrl, "4403050080040510099xxxx", "1");
            var result = httpService.HttpRequestAsync(url);
            {
                var DynamicObject = JsonConvert.DeserializeObject<dynamic>(result);
                if (DynamicObject.content == null || DynamicObject.content.list == null || DynamicObject.content.list.Count == 0)
                    return null;
                foreach (var item in DynamicObject.content.list)
                {
                    var fireIot = new Mmc.Mspace.Models.FireIot.FireIotInfo();
                    fireIot.addr = item.addr;
                    fireIot.type = item.type;
                    fireIot.value = item.value;
                    fireIot.status = item.status;
                    fireIot.name = item.name;
                    fireIot.model = item.model;
                    fireIot.code = item.code;
                    fireIot.cgq_dy = item.cgq_dy;
                    fireIot.cgqfz = item.cgqfz;
                    fireIot.createdate = item.createdate;
                    fireIot.cgq_xhqd = item.cgq_xhqd;
                    allFireIots.Add(fireIot);
                }
                return allFireIots;
            }
        }

        public List<Mmc.Mspace.Models.FireIot.FireIotInfo> GetAllFireIotEx()
        {
            var allFireIots = new List<Mmc.Mspace.Models.FireIot.FireIotInfo>();
            var DynamicObject = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + @"\data\json\FireIotEvent.json");
            //伪造数据
            if (DynamicObject.list == null || DynamicObject.list.Count == 0)
                return null;
            foreach (var item in DynamicObject.list)
            {
                var fireIot = new Mmc.Mspace.Models.FireIot.FireIotInfo();
                fireIot.addr = item.addr;
                fireIot.value = item.value;
                fireIot.status = item.status;
                fireIot.name = item.name;
                fireIot.code = item.code;
                fireIot.cgqfz = item.valueRange;
                fireIot.createdate = DateTime.Now.ToString();
                allFireIots.Add(fireIot);
            }
            return allFireIots;
        }
    }
}