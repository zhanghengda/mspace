using Mmc.Windows.Attributes;

namespace Mmc.Mspace.Services.PoliceEventService
{
    public enum ProcessingState
    {
        [Alias("未处理")]
        UnTreated,

        [Alias("已处理")]
        Processed,

        [Alias("处理中")]
        Processing
    }
}