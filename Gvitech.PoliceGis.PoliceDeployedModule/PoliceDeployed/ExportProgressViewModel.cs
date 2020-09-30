using System;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoliceDeployedModule.PoliceDeployed
{
	// Token: 0x0200000F RID: 15
	public class ExportProgressViewModel : BindableBase
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00005098 File Offset: 0x00003298
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000050B0 File Offset: 0x000032B0
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

		// Token: 0x04000038 RID: 56
		private object progressValue;
	}
}
