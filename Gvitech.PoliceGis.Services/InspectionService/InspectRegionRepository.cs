using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.InspectionService
{
    /// <summary>
    /// 巡检区域仓储类Repository(暂时弃用)
    /// </summary>
    [Obsolete("This class is obsolete; use class InspectionService instead")]
    public class InspectRegionRepository
    {
        private IInspectionService _inspectSrv = ServiceManager.GetService<IInspectionService>(null);
        public InspectRegion FindRegion(Expression<Func<InspectRegion, bool>> predicate)
        {
            var region = _inspectSrv.InspectRegionSet.FindOne(predicate);
            var units = GetUnitsByRegionId(region.Id);
            region.InspectUnits = units;
            return region;
        }

        private List<InspectUnit> GetUnitsByRegionId(int regionId)
        {
            var units = _inspectSrv.InspectUnitSet.Find(p => p.InspectRegionId == regionId);
            foreach (var unit in units)
            {
                var videos = _inspectSrv.VideoItemSet.Find(p => p.InspectUnitId == unit.Id);
                var pics = _inspectSrv.PictureItemSet.Find(p => p.InspectUnitId == unit.Id);
                var reports = _inspectSrv.ReportItemSet.Find(p => p.InspectUnitId == unit.Id);
                var routes = _inspectSrv.RouteItemSet.Find(p => p.InspectUnitId == unit.Id);
                var dom = _inspectSrv.DomItemSet.FindOne(p => p.InspectUnitId == unit.Id);
                unit.VideoLayer = videos;
                unit.ReportLayer = reports;
                unit.RouteLayer = routes;
                unit.DomLayer = dom;
                unit.PicLayer = pics;
            }
            return units;
        }


        private List<InspectRegion> FindRegions(Expression<Func<InspectRegion, bool>> predicate)
        {

            var regions = _inspectSrv.InspectRegionSet.Find(predicate);
            foreach (var region in regions)
            {
                var units = GetUnitsByRegionId(region.Id);
                region.InspectUnits = units;
            }
            return regions;
        }


        /// <summary>
        /// 增加巡检区域
        /// </summary>
        /// <param name="newRegion">新的巡检区域</param>
        public InspectRegion AddRegion(InspectRegion newRegion)
        {
            var regionid = _inspectSrv.InspectRegionSet.Add(newRegion);
            newRegion.Id = regionid;
            return newRegion;
        }


        /// <summary>
        /// 删除巡检区域
        /// </summary>
        /// <param name="region">巡检区域</param>
        public bool DeleteRegion(InspectRegion region)
        {
            var regionid = region.Id;
            if (_inspectSrv.InspectRegionSet.Delete(p => p.Id == regionid) > 0)
            {
                var units = region.InspectUnits;
                foreach (var unit in units)
                    DeleteUnit(unit);
                units.Clear();
            }

            return true;
        }

        #region 巡检单元
        public bool DeleteUnit(InspectUnit unit)
        {
            var unitId = unit.Id;
            if (_inspectSrv.InspectUnitSet.Delete(p => p.Id == unitId) < 0)
                return false;
            var dom = unit.DomLayer;
            _inspectSrv.InspectUnitSet.Delete(p => p.Id == dom.Id);
            var videos = unit.VideoLayer;
            foreach (var video in videos)
                _inspectSrv.VideoItemSet.Delete(p => p.Id == video.Id);
            videos.Clear();

            var pics = unit.PicLayer;
            foreach (var pic in pics)
                _inspectSrv.PictureItemSet.Delete(p => p.Id == pic.Id);
            pics.Clear();

            var routes = unit.RouteLayer;
            foreach (var route in routes)
                _inspectSrv.RouteItemSet.Delete(p => p.Id == route.Id);
            routes.Clear();

            var reports = unit.VideoLayer;
            foreach (var report in reports)
                _inspectSrv.ReportItemSet.Delete(p => p.Id == report.Id);
            reports.Clear();

            return true;
        }
        #endregion
    }
}
