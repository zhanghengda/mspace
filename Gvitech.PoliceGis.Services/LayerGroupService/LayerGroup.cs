using System.Xml.Serialization;
using ApplicationConfig;
using Mmc.Mspace.Models.User;

namespace Mmc.Mspace.Services.LayerGroupService
{
    public class LayerGroup:IUserInfo
    {
        [XmlAttribute]
        public string GroupName { get; set; }

        public string Alis { get; set; }
        [XmlAttribute]
        public string Layers { get; set; }
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
    }

   
}