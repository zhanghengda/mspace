using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationConfig
{
    public class DataProviderConfig : IDataProviderConfig
    {
        [XmlAttribute]
        public string AliasName { get; set; }
        [XmlAttribute]
        public string ConnInfoString { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public bool IsLocal { get; set; }
        public string Guid { get; set; }
        //public string HashCode { get; set; }
    }
}
