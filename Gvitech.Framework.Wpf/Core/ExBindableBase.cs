using System;
using System.Xml.Serialization;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Wpf.Mvvm;

namespace Mmc.Framework.Wpf.Core
{
	/// <summary>
	///
	/// </summary>
	// Token: 0x02000004 RID: 4
	public class ExBindableBase : BindableBase
	{
		/// <summary>
		///
		/// </summary>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		[XmlIgnore]
		public IRenderControl MapControl
		{
			get
			{
				return GviMap.MapControl;
			}
		}
	}
}
