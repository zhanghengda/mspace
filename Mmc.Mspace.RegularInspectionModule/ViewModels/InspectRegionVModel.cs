using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.RegularInspectionModule.model
{
    public class InspectRegionVModel: CheckedToolItemModel
    {
        private IInspectionService _inspectSrv;
        public override void Initialize()
        {
            base.Initialize();
            this.ViewType = ViewType.CheckedIcon;
            _inspectSrv = ServiceManager.GetService<IInspectionService>(null);

        }
        public bool InitData()
        {
            var region = MockNewInspectRegionData();
            //_inspectSrv.InspectRegionSet.Add(region);
         
            return true;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            var region = MockNewInspectRegionData();
            var region1 = MockGetInspectRegionData();
            MockDeleteInspectRegionData();
            //_inspectSrv.InspectRegionSet.Delete()
        }


        public InspectRegion MockGetInspectRegionData()
        {
            var region = _inspectSrv.FindRegion(p => p.Name == "弥河");
            return region;
        }

        public bool MockDeleteInspectRegionData()
        {
            var region = _inspectSrv.FindRegion(p => p.Name == "弥河");
            return _inspectSrv.DeleteRegion(region);
        }

        public InspectRegion MockNewInspectRegionData()
        {
            InspectRegion region1 = new InspectRegion();
            region1.Name = "弥河";
            var regionId= _inspectSrv.InspectRegionSet.Add(region1);
            var time = new DateTime(2018, 10, 1);
            var unit = new InspectUnit()
            {
                Name = time.ToLongDateString(),
                Time = time,
                InspectRegionId = regionId,
            };
            var unitId = _inspectSrv.InspectUnitSet.Add(unit);
            //正射
            var domItem = new DomItem()
            {
                Path = "xxx.tif",
                Name = "xxxdom",
                InspectUnitId=unitId,
            };
            _inspectSrv.DomItemSet.Add(domItem);
            //视频
            var video1 = new VideoItem()
            {
                Path = "xxx1.video",
                Name = "xxx1video",
                InspectUnitId = unitId,
            };

            var video2 = new VideoItem()
            {
                Path = "xxx2.video",
                Name = "xxx2video",
                InspectUnitId = unitId,
            };
            _inspectSrv.VideoItemSet.Add(video1);
            _inspectSrv.VideoItemSet.Add(video2);
            //图片
            var pic1 = new PictureItem()
            {
                Path = "pic1.pic",
                Name = "pic1.pic",
                InspectUnitId = unitId,
            };

            var pic2 = new PictureItem()
            {
                Path = "pic2.pic",
                Name = "xxx2video",
                InspectUnitId = unitId,
            };
            _inspectSrv.PictureItemSet.Add(pic1);
            _inspectSrv.PictureItemSet.Add(pic2);

            //航线
            var Route1 = new RouteItem()
            {
                Path = "Route1.kml",
                Name = "Route1.kml",
                InspectUnitId = unitId,
            };

            var Route2 = new RouteItem()
            {
                Path = "Route2.kml",
                Name = "Route2.kml",
                InspectUnitId = unitId,
            };

            _inspectSrv.RouteItemSet.Add(Route1);
            _inspectSrv.RouteItemSet.Add(Route2);
            //报告
            var report1 = new ReportItem()
            {
                Path = "report1.kml",
                Name = "report1.kml",
                InspectUnitId = unitId,
            };

            var report12 = new ReportItem()
            {
                Path = "report2.kml",
                Name = "report2.kml",
                InspectUnitId = unitId,
            };
            _inspectSrv.ReportItemSet.Add(report1);
            _inspectSrv.ReportItemSet.Add(report12);


            unit.VideoLayer = new List<VideoItem>() { video1, video2 };
            unit.PicLayer = new List<PictureItem>() { pic1, pic2 };
            unit.RouteLayer = new List<RouteItem>() { Route1, Route2 };
            unit.ReportLayer = new List<ReportItem>() { report1, report12 };
          
            region1.InspectUnits = new List<InspectUnit>();
            region1.InspectUnits.Add(unit);
            return region1;
        }



    }
}
