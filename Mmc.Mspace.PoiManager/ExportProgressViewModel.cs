using System;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule
{
	public class ExportProgressViewModel : BindableBase
	{
		public object ProgressValue
		{
			get
			{
				return this.progressValue;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<object>(ref this.progressValue, value, "ProgressValue");
			}
		}

		private object progressValue;
	}
}
