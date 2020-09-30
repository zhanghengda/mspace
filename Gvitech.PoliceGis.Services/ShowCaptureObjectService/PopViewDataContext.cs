using Mmc.Wpf.Mvvm;
using System.Data;

namespace Mmc.Mspace.Services.ShowCaptureObjectService
{
    public class PopViewDataContext : BindableBase
    {
        public string FeatureId { get; set; }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this.isOpen, value, "IsOpen");
            }
        }

        public double Left
        {
            get
            {
                return this.left;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this.left, value, "Left");
            }
        }

        public double Top
        {
            get
            {
                return this.top;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this.top, value, "Top");
            }
        }

        public DataView DataView
        {
            get
            {
                return this.dataView;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<DataView>(ref this.dataView, value, "DataView");
            }
        }

        private DataView dataView;

        private bool isOpen;

        private double left;

        private double top;
    }
}