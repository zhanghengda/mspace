using Gvitech.CityMaker.RenderControl;
using Mmc.Platform.Services;
using Mmc.Windows.Design;
using Mmc.Windows.Framework.Commands;
using System;
using System.Xml.Serialization;

namespace Mmc.Platform.Core
{
	public abstract class SimpleCommandWithMap : SimpleCommand
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
