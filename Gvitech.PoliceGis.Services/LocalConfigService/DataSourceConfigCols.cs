using ApplicationConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.LocalConfigService
{
    public interface IDataSourceConfigCols<T> : IBaseConfigCols<T> where T : IDataProviderConfig
    {
        int Delete(string fileName);
        T Find(string fileName);
        bool Exist(string fileName);
    }

    public class DataSourceConfigCols<T> : BaseConfigCols<T>, IDataSourceConfigCols<T> where T : IDataProviderConfig
    {
        public DataSourceConfigCols(LiteDbHelper litedbHelper, string configKey, string userName) : base(litedbHelper, configKey, userName)
        {
            _liteDbHelper = litedbHelper;
            ConfigKey = configKey;
            CurUserName = userName;
        }
        private readonly LiteDbHelper _liteDbHelper;

        public int Delete(string fileName)
        {
            return _liteDbHelper.Delete<T>(ConfigKey, p => p.ConnInfoString == fileName && p.UserName == CurUserName);
        }

        public T Find(string fileName)
        {
            var res = _liteDbHelper.GetCollections<T>(ConfigKey, p => p.ConnInfoString == fileName && p.UserName == CurUserName);
            if (res?.Count > 0)
                return res[0];
            return default(T);
        }

        public bool Exist(string fileName)
        {
            var value = Find(fileName);
            return value != null;
        }


    }
}
