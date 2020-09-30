using Mmc.Mspace.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.RegularInspectionModule.Dto
{
    public static class RegInsModelConvert
    {

        public static DomItem DomConvert(InspectModel inModel)
        {
            var newModel = new DomItem();
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            return newModel;
        }

        public static InspectModel DomConvert(DomItem inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Dom, IsChecked = true };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            return newModel;
        }

        public static PictureItem PictureConvert(InspectModel inModel)
        {
            var newModel = new PictureItem();
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.X = inModel.X;
            newModel.Y = inModel.Y;
            newModel.Z = inModel.Z;
            newModel.RouteName = inModel.RouteName;
            newModel.IsTroublePoi = inModel.IsTroublePoi;
            
            return newModel;
        }

        public static InspectModel PictureConvert(PictureItem inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Picture, IsChecked = true };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.X = inModel.X;
            newModel.Y = inModel.Y;
            newModel.Z = inModel.Z;
            newModel.RouteName = inModel.RouteName;
            newModel.IsTroublePoi = inModel.IsTroublePoi;

            return newModel;
        }

        public static ReportItem ReportConvert(InspectModel inModel)
        {
            var newModel = new ReportItem();
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.ReportNum = inModel.ReportNum;

            return newModel;
        }

        public static InspectModel ReportConvert(ReportItem inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Report, IsChecked = true };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.ReportNum = inModel.ReportNum;

            return newModel;
        }

        public static RouteItem RouteConvert(InspectModel inModel)
        {
            var newModel = new RouteItem();
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.Geom = inModel.Geom;
            newModel.Style = inModel.Style;

            return newModel;
        }

        public static InspectModel RouteConvert(RouteItem inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Route, IsChecked = true };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.Geom = inModel.Geom;
            newModel.Style = inModel.Style;

            return newModel;
        }

        public static VideoItem  VideoConvert(InspectModel inModel)
        {
            var newModel = new VideoItem();
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.Size = inModel.Size;
            newModel.VideoType = inModel.VideoType;

            return newModel;
        }


        public static InspectModel  VideoConvert(VideoItem inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Video, IsChecked=true };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            newModel.IsVisible = inModel.IsVisible;
            newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            newModel.Thumbnail = inModel.Thumbnail;

            newModel.Size = inModel.Size;
            newModel.VideoType = inModel.VideoType;

            return newModel;
        }

        public static InspectUnit InspectUnitConvert(InspectModel inModel)
        {
            var newModel = new InspectUnit();
            newModel.Id = inModel.Id;
            newModel.InspectRegionId = inModel.InspectUnitId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            //newModel.IsVisible = inModel.IsVisible;
            //newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            //newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;
            if(inModel.InspectList!=null&& inModel.InspectList.Count>0)
            {
                var piclayer = inModel.InspectList.Where(t => t.DataType == InspectDataType.Picture).ToList().FirstOrDefault().InspectList;
                foreach (var item in piclayer)
                {
                    if (!string.IsNullOrEmpty(item.RouteName))
                    {
                        newModel.PicLayer.AddRange(item.InspectList?.Select(z => PictureConvert(z)).ToList());
                    }
                    else
                    {
                        newModel.PicLayer.Add(PictureConvert(item));
                    }
                }
                var dmolayer = inModel.InspectList?.Where(t => t.DataType == InspectDataType.Dom).ToList();
                if (dmolayer.FirstOrDefault().InspectList.Count > 0)
                    newModel.DomLayer = DomConvert(dmolayer.FirstOrDefault().InspectList?.FirstOrDefault());
                var routeLayer = (inModel.InspectList?.Where(t => t.DataType == InspectDataType.Route).ToList()).FirstOrDefault().InspectList;
                newModel.RouteLayer = routeLayer.Select(z => RouteConvert(z)).ToList();
                var reportlayer = inModel.InspectList?.Where(t => t.DataType == InspectDataType.Report).ToList().FirstOrDefault().InspectList;
                newModel.ReportLayer = reportlayer.Select(z => ReportConvert(z)).ToList();
                var videoLayer = inModel.InspectList?.Where(t => t.DataType == InspectDataType.Video).ToList().FirstOrDefault().InspectList;
                newModel.VideoLayer = videoLayer.Select(z => VideoConvert(z)).ToList();

            }
            return newModel;
        }

        public static InspectModel InspectUnitConvert(InspectUnit inModel)
        {
            var newModel = new InspectModel() { DataType = InspectDataType.Unit };
            newModel.Id = inModel.Id;
            newModel.InspectUnitId  = inModel.InspectRegionId;
            newModel.IsAdministrator = inModel.IsAdministrator;
            //newModel.IsVisible = inModel.IsVisible;
            //newModel.Md5 = inModel.Md5;
            newModel.Name = inModel.Name;
            //newModel.Path = inModel.Path;
            newModel.UserName = inModel.UserName;
            newModel.Time = inModel.Time;

            //  正射
            InspectModel dom = new InspectModel();
            dom.Name = "正射";
            dom.DataType = InspectDataType.Dom;
            dom.InspectUnitId = newModel.Id;
            if (inModel.DomLayer!=null)
                dom.InspectList.Add(DomConvert(inModel.DomLayer));
            newModel.InspectList.Add(dom);

            //  视频
            InspectModel video = new InspectModel();
            video.DataType = InspectDataType.Video;
            video.Name = "视频";
            video.InspectUnitId = newModel.Id;
            if (inModel.VideoLayer!=null && inModel.VideoLayer.Count>0)
                video.InspectList= inModel.VideoLayer?.Select(t => VideoConvert(t)).ToList();
            newModel.InspectList.Add(video);
            //  图片
            InspectModel picture = new InspectModel();
            picture.DataType = InspectDataType.Picture;
            picture.Name = "图片";
            picture.InspectUnitId = newModel.Id;
            if (inModel.PicLayer != null && inModel.PicLayer?.Count > 0)
            {
               var folders = inModel.PicLayer?.Where(t => t.RouteName != null).GroupBy(r => r.RouteName).Select(r => r.First()).ToList();
                foreach (var item in folders)
                {
                    InspectModel folder = new InspectModel();
                    folder.DataType = InspectDataType.Folder;
                    folder.RouteName= item.RouteName;
                    folder.Name = item.RouteName;
                    folder.InspectUnitId = newModel.Id;
                    folder.InspectList= inModel.PicLayer?.Where(t=>t.RouteName== item.RouteName).Select(z=> PictureConvert(z)).ToList();
                    picture.InspectList?.Add(folder);
                }
                picture.InspectList?.AddRange(inModel.PicLayer?.Where(t => t.RouteName == null).Select(t => PictureConvert(t)).ToList());
            }
            newModel.InspectList?.Add(picture);
            //  航线
            InspectModel route = new InspectModel();
            route.DataType = InspectDataType.Route;
            route.Name = "航线";
            route.InspectUnitId = newModel.Id;
            if (inModel.RouteLayer != null && inModel.RouteLayer.Count > 0)
                route.InspectList = inModel.RouteLayer?.Select(t => RouteConvert(t)).ToList();
            newModel.InspectList.Add(route);
            //  报告
            InspectModel report = new InspectModel();
            report.DataType = InspectDataType.Report;
            report.Name = "报告";
            report.InspectUnitId = newModel.Id;
            if (inModel.ReportLayer != null && inModel.ReportLayer.Count > 0)
                report.InspectList = inModel.ReportLayer?.Select(t => ReportConvert(t)).ToList();
            newModel.InspectList.Add(report);
            return newModel;
        }

        public static InspectModel InspectRegionConvert(InspectRegion inspectRegion)
        {
            InspectModel inspectModel = new InspectModel();
            inspectModel.Id = inspectRegion.Id;
            inspectModel.DataType = InspectDataType.Region;
            inspectModel.Name = inspectRegion.Name;
            inspectModel.UserName = inspectRegion.UserName;
            inspectModel.IsAdministrator = inspectRegion.IsAdministrator;
            inspectModel.InspectList = inspectRegion.InspectUnits?.Select(t=> InspectUnitConvert(t)).ToList();

            return inspectModel;
        }
        public static InspectRegion InspectRegionConvert(InspectModel inspectRegion)
        {
            InspectRegion inspectModel = new InspectRegion();
            inspectModel.Id = inspectRegion.Id;
            inspectModel.Name = inspectRegion.Name;
            inspectModel.UserName = inspectRegion.UserName;
            inspectModel.IsAdministrator = inspectRegion.IsAdministrator;
            inspectModel.InspectUnits = inspectRegion.InspectList?.Select(t=> InspectUnitConvert(t)).ToList();
            return inspectModel;
        }
    }
}
