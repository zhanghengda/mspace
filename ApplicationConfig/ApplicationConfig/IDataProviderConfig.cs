using LiteDB;
using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConfig
{
    public interface IDataProviderConfig : IUserInfo
    {
        string ConnInfoString { get; set; }
        bool IsLocal { get; set; }
        string Guid { get; set; }
        string AliasName { get; set; }
        //string HashCode { get; set; }
    }

    
}
