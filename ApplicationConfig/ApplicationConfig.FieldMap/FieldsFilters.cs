using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ApplicationConfig.FieldMap
{
	public class FieldsFilters
	{
		[XmlArrayItem(typeof(TableFieldsFilter))]
		public List<TableFieldsFilter> TableFieldsFilters
		{
			get;
			set;
		}
	}
}
