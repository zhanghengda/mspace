using System;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.PoliceDeployedModule.PathAnalysis
{
	// Token: 0x02000016 RID: 22
	public class PathAnalysisViewModel : CheckedToolItemModel
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00006AB8 File Offset: 0x00004CB8
		public override void Initialize()
		{
            base.Initialize();
            base.Command = new PathAnalysisCmd();
			base.ViewType = (ViewType)1;
		}
	}
}
