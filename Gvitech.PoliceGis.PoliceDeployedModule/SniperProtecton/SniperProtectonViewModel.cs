using System;
using Mmc.Mspace.Common.Models;

namespace Mmc.Mspace.PoliceDeployedModule.SniperProtecton
{
	// Token: 0x0200000A RID: 10
	public class SniperProtectonViewModel : CheckedToolItemModel
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00004ECC File Offset: 0x000030CC
		public override void Initialize()
		{
			base.Initialize();
			base.Command = new SniperProtectonCmd(this);
			base.ViewType = (ViewType)1;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004EEB File Offset: 0x000030EB
		public override void Reset()
		{
			base.Reset();
			((SniperProtectonCmd)base.Command).OnStop();
		}
	}
}
