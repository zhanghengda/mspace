using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public enum CaseState
    {
        [Alias("有效案件")]
        Effective,

        [Alias("无效案件")]
        Invalid
    }
}