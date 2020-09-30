using System;
//using Gvitech.AppPd.UrbanPlan.DAL;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Models.Navigation;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.NavigationModule.Locale
{
	// Token: 0x02000009 RID: 9
	public class LocationSceneWrapper : BindableBase
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000025F8 File Offset: 0x000007F8
		private void FlyToLocation(LocationScene location)
		{
			bool flag = location == null || string.IsNullOrEmpty(location.Location);
			if (!flag)
			{
                string[] array = location.Location.Split(new char[] { ';' });
                if (!(array == null || array.Length != 6))
                {
                    double x = StringExtension.ParseTo<double>(array[0], 0.0);
                    double y = StringExtension.ParseTo<double>(array[1], 0.0);
                    double z = StringExtension.ParseTo<double>(array[2], 0.0);
                    double heading = StringExtension.ParseTo<double>(array[3], 0.0);
                    double tilt = StringExtension.ParseTo<double>(array[4], 0.0);
                    double roll = StringExtension.ParseTo<double>(array[5], 0.0);
                    ICameraExtension.SetCamera(GviMap.AxMapControl.Camera,x, y, z, heading, tilt, roll, GviMap.SpatialCrs, gviSetCameraFlags.gviSetCameraNoFlags);
                }
            }
        }

		
		public LocationScene LocationScene { get; set; }

		
		public bool IsChecked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				this.isChecked = value;
				base.SetAndNotifyPropertyChanged<bool>(ref this.isChecked, value, "IsChecked");
				bool flag = this.isChecked;
				if (flag)
				{
					this.FlyToLocation(this.LocationScene);
				}
			}
		}

		private bool isChecked;
	}
}
