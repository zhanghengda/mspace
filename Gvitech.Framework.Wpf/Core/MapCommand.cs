using System;
using System.Xml.Serialization;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Framework.Wpf.Core
{
	/// <summary>
	/// </summary>
	// Token: 0x02000006 RID: 6
	public abstract class MapCommand : SimpleCommand
	{
		/// <summary>
		/// </summary>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002154 File Offset: 0x00000354
		[XmlIgnore]
		public IRenderControl MapControl
		{
			get
			{
				return GviMap.MapControl;
			}
		}

		/// <summary>
		/// </summary>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000216C File Offset: 0x0000036C
		[XmlIgnore]
		public virtual AxRenderControl AxMapControl
		{
			get
			{
				return GviMap.AxMapControl;
			}
		}
	}
}
