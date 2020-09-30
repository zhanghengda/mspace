using LiteDB;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConfig
{
    public class CrsConfig : IUserInfo
    {
        public string UserName { get; set; }
        public int Id { get; set; }

        public string CrsWkt { get; set; }
        public int IsAdministrator { get; set; }
    }
}
