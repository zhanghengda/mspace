using Gvitech.CityMaker.RenderControl;
using System;
using System.Xml.Serialization;

namespace Mmc.LayerSymbol
{
	public class IGoemetryRender
	{
		[XmlAttribute]
		public double HeightOffset
		{
			get;
			set;
		}

		[XmlAttribute]
		public gviHeightStyle HeightStyle
		{
			get;
			set;
		}

		[XmlAttribute]
		public string RenderGroupField
		{
			get;
			set;
		}

		[XmlAttribute]
		public gviRenderType RenderType
		{
			get;
			set;
		}
	}
}
