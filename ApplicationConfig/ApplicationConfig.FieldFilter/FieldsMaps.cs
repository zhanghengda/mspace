using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldFilter
{
	public class FieldsMaps
	{
		[XmlArrayItem(typeof(TableFieldsMap))]
		public List<TableFieldsMap> TableFieldsMaps
		{
			get;
			set;
		}
	}
}
