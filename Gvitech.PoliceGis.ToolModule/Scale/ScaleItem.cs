using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System.Windows.Input;

namespace Mmc.Mspace.ToolModule.Scale
{
    public class ScaleItem : BindableBase
    {
        private ScaleType scaleType;

        public ScaleItem()
        {
            this.ScaleChangedCmd = new RelayCommand(() =>
            {
                switch (this.ScaleType)
                {
                    case ScaleType.City:
                        this.Scale(15000.0);
                        break;

                    case ScaleType.Street:
                        this.Scale(2000.0);
                        break;

                    case ScaleType.Hamlet:
                        this.Scale(100.0);
                        break;
                }
            });
        }

        public string Icon { get; set; }

        public string MouseOverIcon { get; set; }

        public string PressedIcon { get; set; }
        public string Content { get; set; }

        public ICommand ScaleChangedCmd { get; set; }

        public ScaleType ScaleType
        {
            get
            {
                return this.scaleType;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ScaleType>(ref this.scaleType, value, "ScaleType");
            }
        }
        private void Scale(double dz)
        {
            IVector3 vector;
            IEulerAngle eulerAngle;
            GviMap.MapControl.Camera.GetCamera(out vector, out eulerAngle);
            bool isRegistered = GviMap.MapControl.Terrain.IsRegistered;
            if (isRegistered)
            {
                dz += GviMap.MapControl.Terrain.GetElevation(vector.X, vector.Y, gviGetElevationType.gviGetElevationFromDatabase);
            }
            vector.Set(vector.X, vector.Y, dz);
            GviMap.MapControl.Camera.SetCamera(vector, new EulerAngle
            {
                Heading = 0.0,
                Roll = 0.0,
                Tilt = -90.0
            }, gviSetCameraFlags.gviSetCameraNoFlags);
        }
    }
}