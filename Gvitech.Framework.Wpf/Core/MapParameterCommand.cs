using System;
using System.Xml.Serialization;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Wpf.Commands;

namespace Mmc.Framework.Wpf.Core
{
	/// <summary>
	///
	/// </summary>
	// Token: 0x02000005 RID: 5
	public abstract class MapParameterCommand : ParameterCommand
	{
		/// <summary>
		/// </summary>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
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
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002134 File Offset: 0x00000334
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
