using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldMap
{
	public class TableFieldsFilter
	{
		[XmlAttribute]
		public string TableName
		{
			get;
			set;
		}

		[XmlArrayItem(typeof(FilterField))]
		public List<FilterField> FilterFields
		{
			get;
			set;
		}
	}
}
