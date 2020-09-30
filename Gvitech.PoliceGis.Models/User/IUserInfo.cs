using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.User
{
    public interface IUserInfo
    {
        string UserName { get; set; }
        int Id { get; set; }
        int IsAdministrator { get; set; }
    }
}
