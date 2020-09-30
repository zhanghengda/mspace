using System.Windows;

namespace Mmc.Mspace.Services.ShowCaptureObjectService
{
    public interface IShowCaptureObjectService
    {
        FrameworkElement View { get; set; }

        PopViewDataContext DataContext { get; set; }
    }
}