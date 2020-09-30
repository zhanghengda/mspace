using System;
using System.Collections.Generic;

namespace Mmc.Mspace.ToolModule.DynamicClip
{
	// Token: 0x02000006 RID: 6
	public interface IClipDataProvider
	{
		// Token: 0x06000024 RID: 36
		bool Save(List<ClipData> clipDataSet);

		// Token: 0x06000025 RID: 37
		List<ClipData> GetClipDataSet();
	}
}
