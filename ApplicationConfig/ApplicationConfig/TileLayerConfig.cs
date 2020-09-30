using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationConfig
{
    public class TileLayerConfig:DataProviderConfig
    {
        
        [XmlAttribute]
        public string Password { get; set; }
        
    }
}
