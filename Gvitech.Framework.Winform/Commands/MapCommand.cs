using System;
using CityMakerBuilder.AddIn.Core;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Framework.Services;

namespace Gvitech.Framework.Winform.Commands
{
	// Token: 0x02000004 RID: 4
	public abstract class MapCommand : AbstractCommand
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000217C File Offset: 0x0000037C
		public virtual RenderControl MapControl
		{
			get
			{
				return GviMap.MapControl;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002194 File Offset: 0x00000394
		public virtual AxRenderControl AxMapControl
		{
			get
			{
				return GviMap.AxMapControl;
			}
		}
	}
}
