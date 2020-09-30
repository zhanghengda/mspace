using LiteDB;
using System;
using System.Xml.Serialization;

namespace ApplicationConfig
{
    public class TerrainConfig : ITerrainConfig
    {
        [XmlAttribute]
        public string Server { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        [XmlAttribute]
        public bool DemAvailable { get; set; }

        [XmlAttribute]
        public float DemOpacity { get; set; }

        [XmlAttribute]
        public string AliasName { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
    }
}
