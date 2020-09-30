using ApplicationConfig;
using LayerPropConfig;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Services.LayerGroupService;
using Mmc.Mspace.Models.Inspection;
using System.Linq.Expressions;
using Mmc.Mspace.Services.InspectionService;
using LiteDB;

namespace Mmc.Mspace.Services.LocalConfigService
{

    public class InspectionService : Singleton<InspectionService>, IInspectionService
    {
        private readonly string InspectUnitSetKey = "InspectUnitSetKey";
        private readonly string ReportItemSetKey = "ReportItemSetKey";
        private readonly string VideoItemSetKey = "VideoItemSetKey";
        private readonly string PictureItemSetKey = "PictureItemSetKey";
        private readonly string DomItemSetKey = "DomItemSetKey";
        private readonly string InspectRegionSetKey = "InspectRegionSetKey";
        private readonly string RouteItemSetKey = "RouteItemSetKey";
        private readonly LiteDbHelper _liteDbHelper;


        public static IInspectionService GetDefault(object args = null)
        {
            return Instance;
        }

       
    
   
        public InspectionService()
        {
            string path = System.Windows.Forms.Application.LocalUserAppDataPath + "\\" + ConfigPath.InspectionDb;
            _liteDbHelper = new LiteDbHelper();
            _liteDbHelper.OpenDb(path);
            CurUserName = CacheData.UserInfo.username;
            InspectRegionSet = new BaseConfigCols<InspectRegion>(_liteDbHelper, InspectRegionSetKey, CurUserName);
            InspectUnitSet = new BaseConfigCols<InspectUnit>(_liteDbHelper, InspectUnitSetKey, CurUserName);
            ReportItemSet = new InspectData<ReportItem>(_liteDbHelper, ReportItemSetKey, CurUserName);
            VideoItemSet = new InspectData<VideoItem>(_liteDbHelper, VideoItemSetKey, CurUserName);
            PictureItemSet = new InspectData<PictureItem>(_liteDbHelper, PictureItemSetKey, CurUserName);
            DomItemSet = new InspectData<DomItem>(_liteDbHelper, DomItemSetKey, CurUserName);
            RouteItemSet = new InspectData<RouteItem>(_liteDbHelper, RouteItemSetKey, CurUserName);
            InitWorkSpace();
        }

        private void InitWorkSpace()
        {
     
        }

        public string CurUserName { get; private set; }

        public BaseConfigCols<InspectUnit> InspectUnitSet { get; private set; }


        public InspectData<ReportItem> ReportItemSet { get; private set; }

        public InspectData<VideoItem> VideoItemSet { get; private set; }

        public InspectData<PictureItem> PictureItemSet { get; private set; }

        public InspectData<DomItem> DomItemSet { get; private set; }

        public BaseConfigCols<InspectRegion> InspectRegionSet { get; private set; }

        public InspectData<RouteItem> RouteItemSet { get; private set; }

        #region 巡检区域
        public InspectRegion FindRegion(Expression<Func<InspectRegion, bool>> predicate)
        {
            var region = this.InspectRegionSet.FindOne(predicate);
            var units = GetUnitsByRegionId(region.Id);
            region.InspectUnits = units;
            return region;
        }
       
        public List<InspectRegion> GetAllRegion(string search="")
        {
            List<InspectRegion> result = new List<InspectRegion>();
            Expression<Func<InspectRegion, bool>> expression = null;
            try
            {
                if (!string.IsNullOrEmpty(search))
                    expression = p => p.Name.Contains(search);
                if (this.InspectRegionSet == null) return null;
                if (expression == null )
                {
                    var regions = this.InspectRegionSet.FindAll();
                    if(regions !=null)
                        foreach (var item in regions)
                        {
                            var units = GetUnitsByRegionId(item.Id);
                            item.InspectUnits = units;
                            result.Add(item);
                        }
                }
                else
                {
                    var region = this.InspectRegionSet.FindOne(expression);
                    if (region == null) return result;
                    var units = GetUnitsByRegionId(region.Id);
                    region.InspectUnits = units;
                    result.Add(region);
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
         
        }


        public List<InspectRegion> FindRegions(Expression<Func<InspectRegion, bool>> predicate)
        {

            var regions = this.InspectRegionSet.Find(predicate);
            foreach (var region in regions)
            {
                var units = GetUnitsByRegionId(region.Id);
                region.InspectUnits = units;
            }
            return regions;
        }

        public bool IsExistsRegion(int id,string regionname, string name)
        {
            bool result = false;
            try
            {
                var regions = this.InspectUnitSet.Find(t=>t.Name==name&&t.InspectRegionId==id);
                if (regions!=null&& regions.ToList().Count > 0)
                    result = true;
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// 增加巡检区域
        /// </summary>
        /// <param name="newRegion">新的巡检区域</param>
        public InspectRegion AddRegion(InspectRegion newRegion)
        {
            int result=0;
            if(newRegion.Id==0)
            {
                result = this.InspectRegionSet.Add(newRegion);
                foreach (var item in newRegion.InspectUnits)
                {
                    item.InspectRegionId = result;
                    this.InspectUnitSet.Add(item);
                }
                newRegion.Id = result;
            }
            else
            {
                foreach (var item in newRegion.InspectUnits)
                {
                    if (item.Id != 0) continue;
                    item.InspectRegionId = newRegion.Id;
                    this.InspectUnitSet.Add(item);
                }
            }
            return newRegion;
        }


        /// <summary>
        /// 删除巡检区域
        /// </summary>
        /// <param name="region">巡检区域</param>
        public bool DeleteRegion(InspectRegion region)
        {
            var regionid = region.Id;
            if (this.InspectRegionSet.Delete(p => p.Id == regionid) > 0)
            {
                var units = region.InspectUnits;
                if (units == null) return true;
                foreach (var unit in units)
                    DeleteUnit(unit);
                units.Clear();
            }

            return true;
        }
        public bool ChangeRegionName(InspectRegion region,string _name)
        {
            var regionid = region.Id;
           
            // this.InspectRegionSet.Update(region);
            this.InspectRegionSet.Delete(p => p.Id == regionid);
            region.Name = _name;
            this.InspectRegionSet.Add(region);
            //var units = region.InspectUnits;
            //if (units == null) return true;
            //foreach (var unit in units)
            //    DeleteUnit(unit);
            //units.Clear();


            return true;
        }
        #endregion


        #region 巡检单元
        public bool DeleteUnit(InspectUnit unit)
        {
            var unitId = unit.Id;
            if (this.InspectUnitSet.Delete(p => p.Id == unitId) < 0)
                return false;
            var dom = unit.DomLayer;
            if (dom != null)
                this.DomItemSet.Delete(p => p.Id == dom.Id);
            var videos = unit.VideoLayer;
            if (videos != null&& videos.Count>0)
                foreach (var video in videos)
                this.VideoItemSet.Delete(p => p.Id == video.Id);
            videos.Clear();

            var pics = unit.PicLayer;
            if (pics != null && pics.Count > 0)
                foreach (var pic in pics)
                this.PictureItemSet.Delete(p => p.Id == pic.Id);
            pics.Clear();

            var routes = unit.RouteLayer;
            if (routes != null && routes.Count > 0)
                foreach (var route in routes)
                this.RouteItemSet.Delete(p => p.Id == route.Id);
            routes.Clear();

            var reports = unit.ReportLayer;
            if (reports != null && reports.Count > 0)
                foreach (var report in reports)
                this.ReportItemSet.Delete(p => p.Id == report.Id);
            reports.Clear();

            return true;
        }
        public bool DeleteReportLayer(List<ReportItem> reportItems)
        {
            try
            {
                foreach (var report in reportItems)
                    this.ReportItemSet.Delete(p => p.Id == report.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
          
        }
        public bool DeleteRouteLayer(List<RouteItem> routeItems)
        {
            try
            {
                foreach (var route in routeItems)
                    this.RouteItemSet.Delete(p => p.Id == route.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeletePicLayer(List<PictureItem> rictureItems)
        {
            try
            {
                foreach (var pic in rictureItems)
                    this.PictureItemSet.Delete(p => p.Id == pic.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteVideo(List<VideoItem> videoItems)
        {
            try
            {
                foreach (var video in videoItems)
                    this.VideoItemSet.Delete(p => p.Id == video.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteDomItem(DomItem domItem)
        {
            try
            {
                this.DomItemSet.Delete(p => p.Id == domItem.Id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddDom(DomItem domItem)
        {
            try
            {
                var doms = this.DomItemSet?.Find(t => t.InspectUnitId == domItem.InspectUnitId);
                if (doms!=null)
                    foreach (var item in doms)
                    {
                        this.DomItemSet.Delete(t=>t.Id==item.Id);
                    }
                this.DomItemSet.Add(domItem);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePicItem(PictureItem picItem)
        {
            try
            {
                this.PictureItemSet.Update(picItem);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<InspectUnit> GetUnitsByRegionId(int regionId)
        {
            var units = this.InspectUnitSet.Find(p => p.InspectRegionId == regionId);
            if (units != null) 
                foreach (var unit in units)
                {
                    var videos = this.VideoItemSet.Find(p => p.InspectUnitId == unit.Id);
                    var pics = this.PictureItemSet.Find(p => p.InspectUnitId == unit.Id);
                    var reports = this.ReportItemSet.Find(p => p.InspectUnitId == unit.Id);
                    var routes = this.RouteItemSet.Find(p => p.InspectUnitId == unit.Id);
                    var dom = this.DomItemSet.FindOne(p => p.InspectUnitId == unit.Id);
                    unit.VideoLayer = videos;
                    unit.ReportLayer = reports;
                    unit.RouteLayer = routes;
                    unit.DomLayer = dom;
                    unit.PicLayer = pics;
                }
            return units;
        }
        #endregion

        public List<InspectUnit> FindUnits(Expression<Func<InspectUnit, bool>> predicate)
        {
            return this.InspectUnitSet.Find(predicate);
        }

        //public List<DomItem> FindDoms(Expression<Func<DomItem, bool>> predicate)
        //{
        //    return this.DomItemSet.Find(predicate);
        //}

        public List<DomItem> FindDoms(Query query)
        {
            return this.DomItemSet.FindQuery(query);
        }

        public DomItem FindOneDom(int unitId)
        {
            return this.DomItemSet.FindOne(p=>p.InspectUnitId == unitId);
        }

        public RouteItem FindOneRoute(int unitId)
        {
            return this.RouteItemSet.FindOne(p => p.InspectUnitId == unitId);
        }
    }


}





