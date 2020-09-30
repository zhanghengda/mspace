using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.Services.LocalConfigService;
using System;

namespace Mmc.Mspace.Services.InspectionService
{
    public class InspectData<T> : BaseConfigCols<T> where T : InspectItem
    {
        public InspectData(LiteDbHelper litedbHelper, string configKey, string userName) : base(litedbHelper, configKey, userName)
        {
            LiteDbHelper = litedbHelper;
            ConfigKey = configKey;
            CurUserName = userName;
        }
        public override int Add(T item)
        {

            if (item.InspectUnitId <= 0)
                throw new Exception("no inspect unit id");
            return base.Add(item);
        }
    }
}
