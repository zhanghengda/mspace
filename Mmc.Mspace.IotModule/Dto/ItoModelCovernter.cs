using Gvitech.CityMaker.FdeGeometry;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.IotModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Dto
{
    public class ItoModelCovernter
    {
        private string Online = Helpers.ResourceHelper.FindKey("Online");
        private string Offline = Helpers.ResourceHelper.FindKey("Offline");

        public  PatroledData PatroledDataConvert(PatroledDataForClient inModel)
        {
            PatroledData  outModel= new PatroledData();
            //outModel.geom
            return outModel;
        }

        private  PatroledDataForClient PatroledDataConvert(PatroledData inModel)
        {
            PatroledDataForClient outModel = new PatroledDataForClient();
            outModel.Point = GviMap.GeoFactory.CreateFromWKT(inModel.geom) as IPoint;
            outModel.Point.Z = 1.2;
            if (outModel.Point!=null)
                outModel.Point.SpatialCRS = GviMap.SpatialCrs;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //TimeSpan toNow = new TimeSpan(inModel.inspection_time * 1000);
            outModel.Time = dtStart.AddSeconds(inModel.inspection_time);
            outModel.connect_name = inModel.connect_name;
            return outModel;
        }


        private  bool MarkPointLegalByDistance(IPoint point1,IPoint point2)
        {
            bool success = false;

            var newPoint1 = GviMap.PointManager.CreatePoint(point1.X, point1.Y, point1.Z);
            var newPoint2 = GviMap.PointManager.CreatePoint(point2.X, point2.Y, point2.Z);
            var projPoi1 = ToProject(newPoint1);
            var projPoi2 = ToProject(newPoint2);
            double x = projPoi1.X - projPoi2.X;
            double y = projPoi1.Y - projPoi2.Y;

            double distance = Math.Sqrt(x * x + y * y);

            if (distance < 150) // 步行 1min 经验距离
                success = true;

            newPoint1 = null;
            newPoint2 = null;
            projPoi1 = null;
            projPoi2 = null;
            return success;
        }

        private  IPoint ToProject(IPoint point)
        {
            var prjWkt = Wgs84UtmUtil.GetWkt(point.X);
            if (!string.IsNullOrEmpty(prjWkt))
                point.ProjectEx(prjWkt);
            return point;
        }

        public  PatrolmanForClient PatrolmanConvert(Patrolman inModel)
        {
            PatrolmanForClient outModel = new PatrolmanForClient();
            outModel.Name = inModel.name;
            outModel.Phone = inModel.phone;
            outModel.ID = inModel.id;
            outModel.SignInTime =  inModel.LastSignInTime;//string
            if (inModel.status == 1) outModel.Status = Online;
            else outModel.Status = Offline;
            //if (inModel.inspector_list.Count > 0)
            //{
            //    var clientModel = PatroledDataConvert(inModel.inspector_list[0]);
            //    outModel.SignInTime = clientModel.Time;
            //}
            return outModel;
        }

        public PatrolmanDataForRender PatrolmanRenderConvert(Patrolman inModel)
        {
            PatrolmanDataForRender outModel = new PatrolmanDataForRender();
            outModel.Name = inModel.name;
            outModel.Phone = inModel.phone;

            if (inModel.inspector_list.Count > 0)
            {
                string prevStatusTag = inModel.inspector_list[0].connect_name;
                for (var i = 0; i < inModel.inspector_list.Count; i++)
                {
                    var dataModel = PatroledDataConvert(inModel.inspector_list[i]);

                    //int count = outModel.PointsList.Count;
                    //if (count <= 1)
                    //{
                    //dataModel.IsLegalPoint = true;
                    //}
                    //else
                    //{
                    //    clientModel.IsLegalPoint = MarkPointLegalByDistance(outModel.Inspector_list[count - 1].Point, clientModel.Point);
                    //}
                    //if (clientModel.IsLegalPoint)
                    dataModel.Point.Z += 2;//增加偏移，防止与影像或网格重叠造成闪面
                    outModel.PointsList.Add(dataModel.Point);
                    outModel.PointsTime.Add(dataModel.Time);

                    if (prevStatusTag != inModel.inspector_list[i].connect_name)
                    {
                        outModel.StatusLocation.Add(i);
                        prevStatusTag = inModel.inspector_list[i].connect_name;
                    }
                }
            }
            return outModel;
        }

    }
}
