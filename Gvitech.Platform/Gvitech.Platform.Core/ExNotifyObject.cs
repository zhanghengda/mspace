using Gvitech.CityMaker.RenderControl;
using Mmc.Platform.Services;
using Mmc.Windows.Design;
using Mmc.Windows.Framework.Core;
using System;
using System.Xml.Serialization;

namespace Mmc.Platform.Core
{
	public class ExNotifyObject : NotifyObject
	{
		[XmlIgnore]
		public IRenderControl MapControl
		{
			get
			{
				return Singleton<MapService>.Instance.RenderControl;
			}
		}
	}
}
