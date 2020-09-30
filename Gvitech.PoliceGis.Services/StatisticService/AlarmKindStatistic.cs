using System.Collections.Generic;

namespace Mmc.Mspace.Services.StatisticService
{
    public class AlarmKindStatistic
    {
        public string Kind { get; set; }

        public List<AlarmKindStatisticItem> AlarmKindStatisticItems { get; set; }
    }
}