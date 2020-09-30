using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.Inspection
{
    /// <summary>
    /// 巡检文件条目
    /// </summary>
    public class InspectItem: IUserInfo
    {
        public int InspectUnitId { get; set; }
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        public String Name { get; set; }
        public String Path { get; set; }
        public string UserName { get; set; }
        public int IsAdministrator { get; set; }
        public string Md5 { get; set; }
        public DateTime Time { get; set; }
        public string Thumbnail { get; set; }
    }
}
