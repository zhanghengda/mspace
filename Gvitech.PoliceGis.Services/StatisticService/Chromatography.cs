using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.StatisticService
{
    public class Chromatography
    {
        public List<ChromatographyItem> ChromatographyItems { get; set; }

        [XmlAttribute]
        public string Kind { get; set; }
    }
}