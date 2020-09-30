using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.FieldsFilterService
{
    [XmlInclude(typeof(FieldsFilter))]
    public class FieldsFilterConfig
    {
        [XmlArrayItem(typeof(FieldsFilter))]
        public List<FieldsFilter> FieldsFilters { get; set; }
    }
}