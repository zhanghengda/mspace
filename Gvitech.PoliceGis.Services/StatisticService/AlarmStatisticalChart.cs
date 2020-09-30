using Mmc.Wpf.Mvvm;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.StatisticService
{
    public class AlarmStatisticalChart : BindableBase
    {
        public string PoliceStation
        {
            get
            {
                return this.policeStation;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this.policeStation, value, "PoliceStation");
            }
        }

        public string Tittle
        {
            get
            {
                return this.tittle;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this.tittle, value, "Tittle");
            }
        }

        public List<StatisticalChartItem> ChartItems
        {
            get
            {
                return this.chartItems;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<StatisticalChartItem>>(ref this.chartItems, value, "ChartItems");
            }
        }

        private List<StatisticalChartItem> chartItems;

        private string policeStation;

        private string tittle;
    }
}