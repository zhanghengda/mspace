using ApplicationConfig;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.LocalConfigService
{
    /// <summary>
    /// 单个配置的kv抽象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseLiteDbKv<T> where T : IUserInfo
    {
        bool Update(T crs);

        int Add(T crs);

        T Find();
    }



    public class BaseLiteDbKv<T> : IBaseLiteDbKv<T> where T : IUserInfo
    {
        public BaseLiteDbKv(LiteDbHelper litedbHelper, string configKey, string userName)
        {
            _liteDbHelper = litedbHelper;
            ConfigKey = configKey;
            CurUserName = userName;
        }

        private LiteDbHelper _liteDbHelper;
        public string ConfigKey { get; private set; }
        public string CurUserName { get; private set; }
        public bool Update(T crs)
        {
            crs.UserName = CurUserName;
            return _liteDbHelper.Update(ConfigKey, crs);
        }

        public int Add(T crs)
        {
            crs.UserName = CurUserName;
            return _liteDbHelper.Insert(ConfigKey, crs);
        }

        public T Find()
        {
            var res = _liteDbHelper.GetCollections<T>(ConfigKey, p => p.UserName == CurUserName);
            if (res?.Count > 0)
                return res[0];
            return default(T);
        }

    }
}
