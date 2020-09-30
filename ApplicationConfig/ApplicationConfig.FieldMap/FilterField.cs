using System;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldMap
{
	public class FilterField
	{
		[XmlAttribute]
		public string FieldName
		{
			get;
			set;
		}
	}
}
