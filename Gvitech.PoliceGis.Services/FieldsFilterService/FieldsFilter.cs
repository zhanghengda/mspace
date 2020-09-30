using System.Xml.Serialization;

namespace Mmc.Mspace.Services.FieldsFilterService
{
    public class FieldsFilter
    {
        [XmlAttribute]
        public string TableName { get; set; }

        [XmlAttribute]
        public string FilterFields { get; set; }
    }
}