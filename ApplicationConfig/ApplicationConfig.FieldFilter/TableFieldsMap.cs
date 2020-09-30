using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldFilter
{
	public class TableFieldsMap
	{
		[XmlAttribute]
		public string TableName
		{
			get;
			set;
		}

		[XmlArrayItem(typeof(MapField))]
		public List<MapField> MapFields
		{
			get;
			set;
		}
	}
}
