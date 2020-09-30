using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mmc.Mspace.Services.FieldsFilterService
{
    public class FieldsFilterService : Singleton<FieldsFilterService>, IFieldsFilterService
    {
        public FieldsFilterService()
        {
            this.ResolveFieldsFilterConfig(this._fieldsFilterConfig);
        }

        protected FieldsFilterConfig FieldsFilterConfig { get; private set; }

        public string GetFilterFields(string tableName)
        {
            bool flag = this.FieldsFilterConfig == null;
            string result;
            if (flag)
            {
                result = string.Empty;
            }
            else
            {
                FieldsFilter fieldsFilter = this.FieldsFilterConfig.FieldsFilters.FirstOrDefault((FieldsFilter ff) => ff.TableName.ToLower().Equals(tableName.ToLower()));
                result = ((fieldsFilter != null) ? fieldsFilter.FilterFields : string.Empty);
            }
            return result;
        }

        public static IFieldsFilterService GetDefault(object args = null)
        {
            return Singleton<FieldsFilterService>.Instance;
        }

        private void ResolveFieldsFilterConfig(string configPath)
        {
            bool flag = string.IsNullOrEmpty(configPath);
            if (!flag)
            {
                bool flag2 = !File.Exists(configPath);
                if (!flag2)
                {
                    this.FieldsFilterConfig = ConfigHelper<FieldsFilterConfig>.ResovleConfigFromFile(configPath);
                }
            }
        }

        private readonly string _fieldsFilterConfig = Application.StartupPath + "\\" + ConfigPath.FieldsFilterConfig;
    }
}