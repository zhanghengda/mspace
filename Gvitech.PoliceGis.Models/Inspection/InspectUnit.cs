using Mmc.Mspace.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.Inspection
{
    /// 巡检单元
    public class InspectUnit : IUserInfo
    {
        public int InspectRegionId { get; set; }
        /// 巡检目标
        public string Name { get; set; }
        /// 巡检时间
        public DateTime Time { get; set; }
        [LiteDB.BsonIgnore]
        public DomItem DomLayer { get; set; }
        /// 视频
        [LiteDB.BsonIgnore]
        public List<VideoItem> VideoLayer { get; set; } = new List<VideoItem>();
        /// 图片
        [LiteDB.BsonIgnore]
        public List<PictureItem> PicLayer { get; set; } = new List<PictureItem>();
        /// 航线
        [LiteDB.BsonIgnore]
        public List<RouteItem> RouteLayer { get; set; } = new List<RouteItem>();
        /// 报告文件
        [LiteDB.BsonIgnore]
        public List<ReportItem> ReportLayer { get; set; } = new List<ReportItem>();
        public string UserName { get; set; }
        public int Id { get; set; }
        public int IsAdministrator { get; set; }
    }
}
