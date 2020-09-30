using System;
using Mmc.Windows.Design;

namespace Mmc.Mspace.Common.ResourceServices
{
	// Token: 0x02000006 RID: 6
	public class ResourceService : Singleton<ResourceService>, IResourceService
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000211C File Offset: 0x0000031C
		public string GetImagePath()
		{
			return "pack://siteoforigin:,,,/Resources/";
		}

		// Token: 0x0400000B RID: 11
		private const string ImagePrefix = "pack://siteoforigin:,,,/Resources/";
	}
}
