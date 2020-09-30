using System;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldFilter
{
	public class MapField
	{
		[XmlAttribute]
		public string Key
		{
			get;
			set;
		}

		[XmlAttribute]
		public string Value
		{
			get;
			set;
		}
	}
}
