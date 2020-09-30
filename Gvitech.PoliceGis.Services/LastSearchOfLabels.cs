using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.LastSearchOfLabelService
{
    public class LastSearchOfLabelService : IUserInfo
    {
        public string Tag { get; set; }
        public string SearchText { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string WktPoly { get; set; }
        public string LevelSearchStr { get; set; }
        public List<string> WktStringList { get; set; }
        public string LayerName { get; set; }

        public int Id { get; set; }
        public int IsAdministrator { get; set; }
        public string UserName { get; set; }
    }
}
