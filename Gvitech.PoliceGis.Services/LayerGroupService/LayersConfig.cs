using Mmc.Mspace.Services.FieldsFilterService;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.LayerGroupService
{
    [XmlInclude(typeof(FieldsFilter))]
    public class LayersConfig
    {
        [XmlArrayItem(typeof(LayerGroup))]
        public List<LayerGroup> LayerGroups { get; set; }
    }
}