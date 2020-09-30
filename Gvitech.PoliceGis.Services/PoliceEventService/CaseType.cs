using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public enum CaseType
    {
        [Alias("行政（治安）案件")]
        PublicSecurity,

        [Alias("刑事案件")]
        Criminal,

        [Alias("举报投诉")]
        Reports,

        [Alias("纠纷")]
        Disputes,

        [Alias("交通类警情")]
        TrafficAccident,

        [Alias("火灾事故")]
        FireAccident,

        [Alias("群众求助")]
        MassRescue,

        [Alias("其他警情")]
        OtherAlarm
    }
}