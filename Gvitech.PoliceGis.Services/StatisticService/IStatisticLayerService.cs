using Mmc.Mspace.Common.Models;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.StatisticService
{
    public interface IStatisticLayerService
    {
        void SetPoliceStationXQLayerVisible(bool visible);

        void InitService();

        List<LayerItemModel> GetStatisticLayers();

        List<StatisticalChartItem> GetAlarmCharData();

        Chromatography GetLengend(string alarmType);

        AlarmStatisticalChart GetChartData(string policement);
    }
}