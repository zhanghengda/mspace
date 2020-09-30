using System;
using System.Collections.Generic;
using Mmc.Mspace.Models.Inspection;
using System.Linq.Expressions;
using Mmc.Mspace.Services.InspectionService;

namespace Mmc.Mspace.Services.LocalConfigService
{
    public interface IInspectionService
    {
        /// <summary>
        /// 当前用户名称
        /// </summary>
        string CurUserName { get; }
        /// <summary>
        /// 巡检区域记录集
        /// </summary>
        BaseConfigCols<InspectRegion> InspectRegionSet { get; }
        /// <summary>
        /// 巡检单元记录集
        /// </summary>
        BaseConfigCols<InspectUnit> InspectUnitSet { get; }
        /// <summary>
        /// 报告文件记录集
        /// </summary>
        InspectData<ReportItem> ReportItemSet { get; }
        /// <summary>
        /// 巡检视频记录集
        /// </summary>
        InspectData<VideoItem> VideoItemSet { get; }
        /// <summary>
        /// 巡检照片记录集
        /// </summary>
        InspectData<PictureItem> PictureItemSet { get; }

        /// <summary>
        /// 正射影像记录集
        /// </summary>
        InspectData<DomItem> DomItemSet { get; }

        /// <summary>
        /// 巡检航线记录集
        /// </summary>
        InspectData<RouteItem> RouteItemSet { get; }

        /// <summary>
        /// 查找单个巡检单元
        /// </summary>
        InspectRegion FindRegion(Expression<Func<InspectRegion, bool>> predicate);
        /// <summary>
        /// 查找所有巡检单元
        /// </summary>
        List<InspectRegion> GetAllRegion(string search = "");

        /// <summary>
        /// 查找多个巡检单元
        /// </summary>
        List<InspectRegion> FindRegions(Expression<Func<InspectRegion, bool>> predicate);
        /// <summary>
        /// 增加新的区域
        /// </summary>
        InspectRegion AddRegion(InspectRegion newRegion);
        /// <summary>
        /// 删除巡检区域
        /// </summary>
        /// <param name="region">巡检区域</param>
        bool DeleteRegion(InspectRegion region);
        /// <summary>
        /// 删除巡检单元
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        bool DeleteUnit(InspectUnit unit);

        /// <summary>
        /// 删除巡检单元
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        bool AddDom(DomItem domItem);
        /// <summary>
        /// 通过区域id查找巡检单元
        /// </summary>
        List<InspectUnit> GetUnitsByRegionId(int regionId);

    }
}
