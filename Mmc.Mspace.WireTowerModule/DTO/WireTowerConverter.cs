using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.WireTowerModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mmc.Mspace.Common;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.WireTowerModule.DTO
{
    public class WireTowerConverter
    {
        public WayPoint WayPointConvert(SignModel loctationPoint, SignModel targetPoint, double relHeight)
        {
            double yaw = Math.Round(loctationPoint.eulerAngle.Heading, 2);
            if (yaw < 0)
            {
                yaw = yaw + 360;
            }

            double gimbalPitch = loctationPoint.eulerAngle.Tilt;

            gimbalPitch = Math.Round(loctationPoint.eulerAngle.Tilt, 2);

            var wayPoint = new WayPoint()
            {
                gimbalPitch = gimbalPitch,
                aircraftLocationLongitude = loctationPoint.X,
                aircraftLocationLatitude = loctationPoint.Y,
                aircraftLocationAltitude = loctationPoint.Z + relHeight,
                aircraftYaw = yaw,
                waypointType = 0 // 默认拍照 
            };

            if (loctationPoint.type.Equals(CommonContract.SignType.Aided.ToString()))
            {
                wayPoint.waypointType = 1; // 1 不拍照
            }

            if (targetPoint != null)
            {
                wayPoint.photoPositionList = new List<PhotoPosition>()
                {
                    new PhotoPosition()
                    {
                        longitude = targetPoint.X,
                        latitude = targetPoint.Y,
                        altitude = targetPoint.Z,
                        name = targetPoint.name,
                    }
                };
            }
            return wayPoint;
        }

        public TowerForClient TowerConvert(TowerModel inModel)
        {
            TowerForClient outModel = new TowerForClient();
            outModel.Id = inModel.Id;
            outModel.Pid = inModel.Pid;
            outModel.Name = inModel.Name;
            outModel.Serial = inModel.Serial;
            outModel.TowerType = inModel.TowerType;
            outModel.SafeDistance = inModel.SafeDistance;
            outModel.X = inModel.X;
            outModel.Y = inModel.Y;
            outModel.Z = inModel.Z;
            outModel.SignList = new System.Collections.ObjectModel.ObservableCollection<SignModel>(inModel.SignList);
            outModel.RelativeHeight = inModel.RelativeHeight;
            this.CreatTowerLineOrder(ref outModel);
            string[] arr = inModel.CrossVotor?.Split(',');
            if (arr?.Length > 0)
            {
                outModel.CrossVotor = new Vector3()
                {

                    X = Convert.ToDouble(arr[0]),
                    Y = Convert.ToDouble(arr[1]),
                    Z = Convert.ToDouble(arr[2])
                };
            }
            return outModel;
        }

        public void CreatTowerLineOrder(ref TowerForClient tower)
        {
            var signModels = tower.SignList;
            if (tower.SignList?.Count > 0)
            {
                SignModel top = new SignModel();
                List<SignModel> leftList = new List<SignModel>();
                List<SignModel> rightList = new List<SignModel>();

                top = signModels.ToList().Find(p => p.type.Equals(SignType.TopCenter.ToString()));
                leftList = signModels.ToList().FindAll(p => p.type.Contains("Left") || p.type.Equals(SignType.Inner.ToString())).OrderBy(p => int.Parse(p.serial)).ToList();
                rightList = signModels.ToList().FindAll(p => p.type.Contains("Right")).OrderBy(p => int.Parse(p.serial)).ToList();
                tower.LeftLine = string.Join("-", top.serial, JoinWithShortSymblo(leftList));
                tower.RightLine = string.Join("-", top.serial, JoinWithShortSymblo(rightList));

                tower.MaxLeftPoint = this.GetMaxPoint(top, leftList);
                tower.MaxRightPoint = this.GetMaxPoint(top, rightList);
            }
        }

        public void StartTowerLineOrder(ref TowerForClient tower)
        {
            var signModels = tower.SignList;
            if (tower.SignList?.Count > 0)
            {
                SignModel top = new SignModel();
                List<SignModel> leftList = new List<SignModel>();
                top = signModels.ToList().Find(p => p.type.Equals("TopCenter"));
                leftList = signModels.ToList().FindAll(p => p.type.Contains("Left")).OrderByDescending(p => int.Parse(p.serial))
                    .ToList();
                tower.LeftLine = string.Join("-", JoinWithShortSymblo(leftList), top.serial);
            }
        }

        private string JoinWithShortSymblo(List<SignModel> signList)
        {
            StringBuilder result = new StringBuilder();
            if (signList?.Count > 0)
            {
                foreach (var item in signList)
                {
                    result.Append(item.serial);
                    result.Append("-");
                }

                if (result.Length >= 1) result.Length -= 1;
            }
            return result.ToString();
        }

        public TowerModel TowerConvert(TowerForClient inModel)
        {
            TowerModel outModel = new TowerModel();
            outModel.Id = inModel.Id;
            outModel.Pid = inModel.Pid;
            outModel.Name = inModel.Name;
            outModel.Serial = inModel.Serial;
            outModel.TowerType = inModel.TowerType;
            outModel.SafeDistance = inModel.SafeDistance;
            outModel.X = inModel.X;
            outModel.Y = inModel.Y;
            outModel.Z = inModel.Z;
            outModel.SignList = inModel.SignList.ToList();
            outModel.RelativeHeight = inModel.RelativeHeight;
            outModel.CrossVotor = string.Join(",", inModel.CrossVotor.X, inModel.CrossVotor.Y, inModel.CrossVotor.Z,
                inModel.CrossVotor.Length);
            return outModel;
        }

        public RouteForClient RouteConvert(RouteModel inModel)
        {
            var outModel = new RouteForClient()
            {
                Pid = inModel.Pid,
                Id = inModel.Id,
                RouteName = inModel.RouteName,
                Serial = inModel.Serial,
                Distance = inModel.Distance
            };
            return outModel;
        }

        public RouteModel RouteConvert(RouteForClient inModel)
        {
            var outModel = new RouteModel()
            {
                Pid = inModel.Pid,
                Id = inModel.Id,
                RouteName = inModel.RouteName,
                Serial = inModel.Serial,
                Distance = inModel.Distance
            };

            return outModel;
        }

        public IPolyline FlightToPolyline(FlightWay inModel)
        {
            var tempList = new List<IPoint>();
            if (inModel?.waypointList?.Count > 0)
            {
                for (int i = 0; i < inModel.waypointList.Count; i++)
                {
                    tempList.Add(WayPointToPoint(inModel.waypointList[i]));
                }

                var polyline = GviMap.GeoFactory.CreatePolyline(tempList, GviMap.SpatialCrs);
                return polyline;
            }
            else
            {
                return null;
            }
        }

        public IPoint WayPointToPoint(WayPoint inModel)
        {
            if (inModel == null) return null;
            var outPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            outPoint.SpatialCRS = GviMap.SpatialCrs;
            outPoint.X = inModel.aircraftLocationLongitude;
            outPoint.Y = inModel.aircraftLocationLatitude;
            outPoint.Z = inModel.aircraftLocationAltitude;
            return outPoint;
        }

        public LineForClient LineConvert(LineModel inModel)
        {
            var outModel = new LineForClient()
            {
                UserName = CacheData.UserInfo.name,
                IsAdministrator = int.TryParse(CacheData.UserInfo.mspace_config.is_administrator, out var isResult)
                    ? isResult
                    : 0,
                Id = inModel.id,
                LineName = inModel.name,
                LineSerial = inModel.serial,
                TowerCount = inModel.towerCount,
                LineLength = inModel.lenght,
                VoltageLevel = inModel.voltageLevel
            };
            return outModel;
        }

        public LineModel LineConvert(LineForClient inModel)
        {
            var outModel = new LineModel()
            {
                //= CacheData.UserInfo.name,
                id = inModel.Id,
                name = inModel.LineName,
                serial = inModel.LineSerial,
                towerCount = inModel.TowerCount,
                lenght = inModel.LineLength,
                voltageLevel = inModel.VoltageLevel
            };
            return outModel;
        }

        //public void SetTowerBaseSafePoint(ref TowerForClient inModel)
        //{
        //    var topCenter = inModel.SignList.ToList().Find(p => p.type.Equals(SignType.TopCenter.ToString()));
        //    var leftList = inModel.SignList.ToList().FindAll(p => p.type.Contains("Left"));
        //    var rightList = inModel.SignList.ToList().FindAll(p => p.type.Contains("Right"));

        //    inModel.MaxLeftPoint = this.GetMaxPoint(topCenter, leftList);
        //    inModel.MaxRightPoint = this.GetMaxPoint(topCenter, rightList);
        //}

        private double[] GetMaxPoint(SignModel topCenter, List<SignModel> signModels)
        {
            double maxDistance = 0;
            double[] temp = new double[2];
            if (signModels != null && signModels.Count > 0)
            {
                foreach (var sign in signModels)
                {
                    var distance = (topCenter.X - sign.X) * (topCenter.X - sign.X) + (topCenter.Y - sign.Y) * (topCenter.Y - sign.Y);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        temp[0] = sign.X;
                        temp[1] = sign.Y;
                    }
                }
            }
            return temp;
        }
    }
}