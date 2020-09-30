using Mmc.Windows.Design;
using System.Windows;

namespace Mmc.Mspace.Services.ShowCaptureObjectService
{
    public class ShowCaptureObjectService : Singleton<ShowCaptureObjectService>, IShowCaptureObjectService
    {
        public ShowCaptureObjectService()
        {
            this.View = new ShowCaptureObjectServiceView();
        }

        public PopViewDataContext DataContext
        {
            get
            {
                return this.dataContext;
            }
            set
            {
                bool flag = this.dataContext != null;
                if (flag)
                {
                    this.dataContext.IsOpen = false;
                    this.dataContext = null;
                }
                this.dataContext = value;
                bool flag2 = this.View != null;
                if (flag2)
                {
                    this.View.DataContext = this.dataContext;
                }
            }
        }

        public FrameworkElement View { get; set; }

        public static IShowCaptureObjectService GetDefault(object args = null)
        {
            return Singleton<ShowCaptureObjectService>.Instance;
        }

        private PopViewDataContext dataContext;
    }
}