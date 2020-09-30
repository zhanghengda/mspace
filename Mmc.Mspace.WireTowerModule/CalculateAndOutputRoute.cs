using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.DTO;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Tools;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.WireTowerModule
{
    class CalculateAndOutputRoute
    {
        #region varies

        private WireTowerConverter _wireTowerConverter;

        #endregion

        public CalculateAndOutputRoute()
        {
            _wireTowerConverter = new WireTowerConverter();
        }

        private string GetSavePath()
        {
            string filePath = string.Empty;
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            if (dialog.SelectedPath != string.Empty) filePath = dialog.SelectedPath;
            return filePath;
        }

        public List<IPolyline> ShowTowerDetail(TowerForClient tower)
        {
            if (tower == null) return null;
            _wireTowerConverter.CreatTowerLineOrder(ref tower);

            if (string.IsNullOrEmpty(tower.LeftLine) && string.IsNullOrEmpty(tower.RightLine)) return null;

            this.GenerateFlightWay(tower, tower.LeftLine, out IPolyline leftline);
            this.GenerateFlightWay(tower, tower.RightLine, out IPolyline rightline);
            return new List<IPolyline>()
            {
                leftline,
                rightline
            };
        }




        public void GenerateRoute(string RouteName, List<TowerForClient> towers, bool isAddToRouteSet = true)
        {
            if (towers == null || towers.Count <= 0)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTTowerInfoMiss"));
                return;
            }

            string filePath = GetSavePath();
            if (string.IsNullOrEmpty(filePath)) return;

            string towerKmlFile = string.Empty;
            string towerKmlFile2 = string.Empty;

            kml pointkml = new kml();

            List<Placemark> placemarks = new List<Placemark>();
            List<IPolyline> leftPolylines = new List<IPolyline>();
            List<IPolyline> rightPolylines = new List<IPolyline>();
            List<FlightWay> leftWays = new List<FlightWay>();
            List<FlightWay> rightWays = new List<FlightWay>();

            FlightWay leftWay = new FlightWay();
            FlightWay rightWay = new FlightWay();
            for (int i = 0; i < towers.Count; i++)
            {
                var tower = towers[i];

                if (tower.SignList?.Count > 0)
                {
                    placemarks.Add(new Placemark(tower.X, tower.Y, tower.Z + tower.RelativeHeight, i));

                    leftWay.towerAltitude = tower.Z + tower.RelativeHeight;

                    rightWay.towerAltitude = tower.Z + tower.RelativeHeight;

                    leftWay = GenerateFlightWay(tower, tower.LeftLine, out IPolyline leftLine);
                    rightWay = GenerateFlightWay(tower, tower.RightLine, out IPolyline rightLine);

                    leftWays.Add(leftWay);
                    rightWays.Add(rightWay);
                    leftPolylines.Add(leftLine);
                    rightPolylines.Add(rightLine);

                    var leftObj = new HttpFlight() { code = "200", data = leftWay, message = "成功" };
                    var rightObj = new HttpFlight() { code = "200", data = rightWay, message = "成功" };

                    string leftWayStr = Newtonsoft.Json.JsonConvert.SerializeObject(leftObj);
                    string rightWayStr = Newtonsoft.Json.JsonConvert.SerializeObject(rightObj);


                    string RouteDir = filePath + "//" + RouteName;
                    if (!Directory.Exists(RouteDir))
                    {
                        Directory.CreateDirectory(RouteDir);
                    }

                    string leftDir = RouteDir + "//" + RouteName + "甲线";
                    towerKmlFile = leftDir + "//" + RouteName + "甲线" + ".kml";
                    string rightDir = RouteDir + "//" + RouteName + "乙线";
                    towerKmlFile2 = rightDir + "//" + RouteName + "乙线" + ".kml";

                    if (!Directory.Exists(leftDir))
                    {
                        Directory.CreateDirectory(leftDir);
                    }

                    if (!Directory.Exists(rightDir))
                    {
                        Directory.CreateDirectory(rightDir);
                    }

                    string leftRouteDir = leftDir + "//AirRoute";
                    string rightRouteDir = rightDir + "//AirRoute";
                    if (!Directory.Exists(leftRouteDir))
                    {
                        Directory.CreateDirectory(leftRouteDir);
                    }

                    if (!Directory.Exists(rightRouteDir))
                    {
                        Directory.CreateDirectory(rightRouteDir);
                    }

                    string leftFile = leftRouteDir + "//" + RouteName + "甲线_" + i + ".json";
                    string rightFile = rightRouteDir + "//" + RouteName + "乙线_" + i + ".json";

                    WirTowRenderManagement.Instance.WriteFile(leftFile, leftWayStr);
                    WirTowRenderManagement.Instance.WriteFile(rightFile, rightWayStr);
                }
            }

            pointkml.PlacemarkList = placemarks;

            pointkml.SaveToFile(towerKmlFile);
            pointkml.SaveToFile(towerKmlFile2);

            var routemodel = new RouteForClient()
            {
                RouteName = RouteName,
                //Pid = SelectedLine,
            };

            routemodel.TowerList = placemarks;
            routemodel.TowerCount = placemarks.Count;
            routemodel.LeftLineList = leftPolylines;
            routemodel.RightLineList = rightPolylines;
            routemodel.LeftWay = leftWays;
            routemodel.RightWay = rightWays;
            routemodel.Towers = towers;

            if (isAddToRouteSet) Messenger.Messengers.Notify("AddRouteOfWir", routemodel);

            List<TowerModel> tempList = new List<TowerModel>();
            foreach (var item in towers)
            {
                tempList.Add(_wireTowerConverter.TowerConvert(item));
            }

            string towersStr = JsonUtil.SerializeToString(tempList);
            WirTowRenderManagement.Instance.WriteFile(string.Format("{0}//{1}//{1}Towers.json", filePath, RouteName),
                towersStr);
        }

        internal IPolyline GenerateMissionRoute(string routeName, List<TowerForClient> towers)
        {
            if (towers == null || towers.Count <= 0) return null;

            string filePath = GetSavePath();
            if (string.IsNullOrEmpty(filePath)) return null;

            string filename = filePath + "//" + routeName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mission";

            var stationMissionJson = new StationMissionJson
            {
                MAV_AUTOPILOT = 3,
                groundStation = "MMC Station",
                complexItems = new List<StationComplexItems>(),
                items = new List<StationItems>(),
                plannedHomePosition = new StationItems(),
                version = "1.0"
            };

            var points = new List<IPoint>();
            var stationItemses = new List<StationItems>();
            var temppoints = new List<IPoint>();
            double originAltitude = 0;

            for (int i = 0; i < towers.Count; i++)
            {
                int id = stationItemses.Count;
                var tower = towers[i];

                // takeoff point 
                if (i == 0)
                {
                    originAltitude = tower.Z;
                    var temppoi = GviMap.PointManager.CreatePoint(tower.X, tower.Y, tower.Z);
                    stationMissionJson.plannedHomePosition.coordinate = new List<double>()
                        {tower.X, tower.Y, tower.Z};

                    stationMissionJson.plannedHomePosition = CreateStationItemsOfPoint(temppoi, null, originAltitude, 0,
                        MissionProtocol.FlightCommand.Home)?[0];

                    // the calculation method of  take-off-point is same as the left point, but the altitude value is  altitude of the first tower
                    var vector = this.GetPitchVector(tower.CrossVotor as Vector3, "Left", -90);
                    var newpoi = this.CalculateOutlinePoint(temppoi, vector, tower.SafeDistance,
                        out IEulerAngle euler);
                    newpoi.Z = tower.Z;
                    stationItemses.AddRange(CreateStationItemsOfPoint(newpoi, euler, originAltitude, id,
                        MissionProtocol.FlightCommand.TakeOff));
                    points.Add(newpoi);
                    _wireTowerConverter.StartTowerLineOrder(ref tower);
                }

                id = stationItemses.Count;
                stationItemses.AddRange(
                    GeneratePointsOfTower(tower, tower.LeftLine, id, originAltitude, ref temppoints));
                points.AddRange(temppoints);

                id = stationItemses.Count;
                stationItemses.AddRange(GeneratePointsOfTower(tower, tower.RightLine, id, originAltitude,
                    ref temppoints));
                points.AddRange(temppoints);
            }

            // 最后点处理
            var lastPoint = points[points.Count - 1];
            points.Remove(lastPoint);
            lastPoint = points[points.Count - 1];
            lastPoint.Z = towers[towers.Count - 1].Z - originAltitude;
            stationItemses.AddRange(CreateStationItemsOfPoint(lastPoint, null, originAltitude, stationItemses.Count,
                MissionProtocol.FlightCommand.Landing));

            var polyline = GviMap.GeoFactory.CreatePolyline(points, GviMap.SpatialCrs);

            stationMissionJson.items = stationItemses;
            string dataStr = JsonUtil.SerializeToString(stationMissionJson);
            WirTowRenderManagement.Instance.WriteFile(filename, dataStr);

            return polyline;
        }

        private List<StationItems> GeneratePointsOfTower(TowerForClient tower, string orderStr, int id,
            double originAlt, ref List<IPoint> points)
        {
            points.Clear();
            if (tower == null || tower.SignList == null || tower.SignList.Count <= 0) return null;

            var tempItems = new List<StationItems>();

            var topCenterSign = tower.SignList.ToList().Find(p => p.type.ToLower().Equals("topcenter"));

            double safeDistance = tower.SafeDistance;
            if (safeDistance < 5) safeDistance = 15;

            var serials = orderStr.Split('-');

            SignModel tempsign = new SignModel();
            IPoint temppoi;
            IPoint newpoi;
            IEulerAngle euler;

            // 循环包含最后点处理
            for (int i = 0; i <= serials.Length; i++)
            {
                if (i != serials.Length)
                {
                    tempsign = tower.SignList.FirstOrDefault(p => p.serial.Equals(serials[i]));
                    if (tower.TowerType.Equals(TowerType.Safe.ToString()))
                    {
                        if (tempsign.type.Equals(SignType.Left.ToString()))
                        {
                            tempsign.X = tower.MaxLeftPoint[0] != 0 ? tower.MaxLeftPoint[0] : tempsign.X;
                            tempsign.Y = tower.MaxLeftPoint[1] != 0 ? tower.MaxLeftPoint[1] : tempsign.Y;
                        }
                        if (tempsign.type.Equals(SignType.Right.ToString()))
                        {
                            tempsign.X = tower.MaxRightPoint[0] != 0 ? tower.MaxRightPoint[0] : tempsign.X;
                            tempsign.Y = tower.MaxRightPoint[1] != 0 ? tower.MaxRightPoint[1] : tempsign.Y;
                        }
                    }
                    safeDistance = tower.SafeDistance + tempsign.speDistance;
                    if (safeDistance < 5) safeDistance = 15;
                    temppoi = GviMap.PointManager.CreatePoint(tempsign.X, tempsign.Y, tempsign.Z);
                }
                else
                {
                    tempsign = tower.SignList.FirstOrDefault(p => p.serial.Equals(serials[i - 1]));
                    if (tower.TowerType.Equals(TowerType.Safe.ToString()))
                    {
                        if (tempsign.type.Equals(SignType.Left.ToString()))
                        {
                            tempsign.X = tower.MaxLeftPoint[0] != 0 ? tower.MaxLeftPoint[0] : tempsign.X;
                            tempsign.Y = tower.MaxLeftPoint[1] != 0 ? tower.MaxLeftPoint[1] : tempsign.Y;
                        }
                        if (tempsign.type.Equals(SignType.Right.ToString()))
                        {
                            tempsign.X = tower.MaxRightPoint[0] != 0 ? tower.MaxRightPoint[0] : tempsign.X;
                            tempsign.Y = tower.MaxRightPoint[1] != 0 ? tower.MaxRightPoint[1] : tempsign.Y;
                        }
                    }
                    safeDistance = tower.SafeDistance + tempsign.speDistance;
                    if (safeDistance < 5) safeDistance = 15;
                    temppoi = GviMap.PointManager.CreatePoint(tempsign.X, tempsign.Y, topCenterSign.Z + safeDistance);
                }

                if (i == serials.Length && tempsign.type.ToLower().Equals("topcenter"))
                {
                    break; //it could not add point when last point type is topcenter.
                }

                Vector3 pitchVector;
                Vector3 trendVector;

                // 酒杯塔  内部点单独计算
                if (tower.TowerType.Equals(TowerType.Wine.ToString()) &&
                    tempsign.type.Equals(SignType.Inner.ToString()))
                {
                    trendVector = this.GetHorizonTrendVector(tower.CrossVotor as Vector3, tempsign.type, tempsign.trendAngle);
                    newpoi = this.CalculateOutlineOfInnerPoint(temppoi, trendVector, safeDistance,
                        topCenterSign.Z + safeDistance, out euler);
                }
                else
                {
                    trendVector = this.GetHorizonTrendVector(tower.CrossVotor as Vector3, tempsign.type,
                        tempsign.trendAngle);
                    pitchVector = this.GetPitchVector(trendVector, tempsign.type, tempsign.pitchAngle);
                    newpoi = this.CalculateOutlinePoint(temppoi, pitchVector,
                        safeDistance / Math.Cos(Math.Abs(tempsign.trendAngle) * Math.PI / 180), out euler);
                }

                points.Add(newpoi);

                id += tempItems.Count;
                tempItems.AddRange(CreateStationItemsOfPoint(newpoi, euler, originAlt + tower.RelativeHeight, id,
                    MissionProtocol.FlightCommand.TakePhoto));
            }

            // 最后点不是顶点,竖直拉起
            if (!tempsign.type.ToLower().Contains("topcenter"))
            {
                temppoi = GviMap.PointManager.CreatePoint(topCenterSign.X, topCenterSign.Y,
                    topCenterSign.Z + safeDistance);
                points.Add(temppoi);

                id += tempItems.Count;
                tempItems.AddRange(CreateStationItemsOfPoint(temppoi, null, originAlt + tower.RelativeHeight, id,
                    MissionProtocol.FlightCommand.Normal));
            }

            return tempItems;
        }

        private List<StationItems> CreateStationItemsOfPoint(IPoint newpoi, IEulerAngle euler,
            double originAlt, int id, MissionProtocol.FlightCommand flyCommand)
        {
            var x = newpoi.X;
            var y = newpoi.Y;
            var z = id == 0 ? originAlt : newpoi.Z - originAlt;
            var yaw = euler?.Tilt ?? 0;
            var pitch = euler?.Heading ?? 0;

            var tempItems = new List<StationItems>();
            CreateFlyActions CreateFlyItem = new CreateFlyActions();
            switch (flyCommand)
            {
                case MissionProtocol.FlightCommand.Home:
                    tempItems = CreateFlyItem.CreateNormalActions(0, y, x, z, yaw: 0, hoverTime: 0);
                    break;
                case MissionProtocol.FlightCommand.TakeOff:
                    tempItems = CreateFlyItem.CreateTakeOffActions(1, y, x, z, 2);
                    break;
                case MissionProtocol.FlightCommand.Landing:
                    tempItems = CreateFlyItem.CreateLandingActions(id, y, x, z);
                    break;
                case MissionProtocol.FlightCommand.TakePhoto:
                    tempItems = CreateFlyItem.CreateTakePhotoActions(id, y, x, z, yaw, pitch);
                    break;
                case MissionProtocol.FlightCommand.Velocity:
                    tempItems = CreateFlyItem.CreateVelocityChangeActions(id, y, x, z);
                    break;
                case MissionProtocol.FlightCommand.Normal:
                    tempItems = CreateFlyItem.CreateNormalActions(id, y, x, z);
                    break;
            }

            return tempItems;
        }

        private FlightWay GenerateFlightWay(TowerForClient tower, string orderStr, out IPolyline polyline)
        {
            List<IPoint> temppointlist = new List<IPoint>();
            FlightWay tempway = new FlightWay();
            IPoint temppoi;
            IPoint newpoi = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            SignModel outlinePoint = new SignModel();
            SignModel sign;
            var tempSignList = tower.SignList.ToList();

            double safeDistance = tower.SafeDistance;

            string[] serialArr = orderStr.Split('-');

            var topPoint = tempSignList.Find(p => p.type.Equals("TopCenter"));

            foreach (var serial in serialArr)
            {
                sign = tempSignList.Find(p => p.serial.Equals(serial));

                safeDistance = tower.SafeDistance + sign.speDistance;
                if (safeDistance < 5) safeDistance = 15;
                if (sign == null) continue;

                outlinePoint.serial = sign.serial;
                outlinePoint.X = sign.X;
                outlinePoint.Y = sign.Y;
                outlinePoint.Z = sign.Z;
                outlinePoint.type = sign.type;
                outlinePoint.speDistance = sign.speDistance;
                outlinePoint.pitchAngle = sign.pitchAngle;
                outlinePoint.trendAngle = sign.trendAngle;


                if (tower.TowerType.Equals(TowerType.Safe.ToString()))
                {
                    if (sign.type.Equals(SignType.Left.ToString()))
                    {
                        outlinePoint.X = tower.MaxLeftPoint[0] != 0 ? tower.MaxLeftPoint[0] : sign.X;
                        outlinePoint.Y = tower.MaxLeftPoint[1] != 0 ? tower.MaxLeftPoint[1] : sign.Y;
                    }
                    if (sign.type.Equals(SignType.Right.ToString()))
                    {
                        outlinePoint.X = tower.MaxRightPoint[0] != 0 ? tower.MaxRightPoint[0] : sign.X;
                        outlinePoint.Y = tower.MaxRightPoint[1] != 0 ? tower.MaxRightPoint[1] : sign.Y;
                    }
                }
                temppoi = GviMap.PointManager.CreatePoint(outlinePoint.X, outlinePoint.Y, outlinePoint.Z);


                Vector3 pitchVector;//俯仰角
                Vector3 trendVector;//偏航角
                IEulerAngle euler;

                // 酒杯塔  内部点单独计算
                if (tower.TowerType.Equals(TowerType.Wine.ToString()) &&
                    outlinePoint.type.Equals(SignType.Inner.ToString()))
                {
                    trendVector = this.GetHorizonTrendVector(tower.CrossVotor as Vector3, outlinePoint.type, outlinePoint.trendAngle);
                    newpoi = this.CalculateOutlineOfInnerPoint(temppoi, trendVector, safeDistance,
                        topPoint.Z + safeDistance, out euler);
                }
                else
                {
                    trendVector = this.GetHorizonTrendVector(tower.CrossVotor as Vector3, outlinePoint.type,
                        outlinePoint.trendAngle);
                    pitchVector = this.GetPitchVector(trendVector, outlinePoint.type, outlinePoint.pitchAngle);
                    newpoi = this.CalculateOutlinePoint(temppoi, pitchVector,
                        safeDistance / Math.Cos(Math.Abs(outlinePoint.trendAngle) * Math.PI / 180), out euler);
                }

                //if (tempsign.type.Contains("Top")) // 当为顶点时  调整机头方向为向量方向
                //{
                //    euler.Heading = Math.Acos(tower.CrossVotor.Y) * 180 / Math.PI;
                //}

                outlinePoint.X = newpoi.X;
                outlinePoint.Y = newpoi.Y;
                outlinePoint.Z = newpoi.Z;
                outlinePoint.eulerAngle = euler;

                tempway.waypointList.Add(_wireTowerConverter.WayPointConvert(outlinePoint, sign, tower.RelativeHeight));
                temppointlist.Add(newpoi);
            }

            var lastPoint = newpoi.Clone() as IPoint;
            // 末尾点处理
            outlinePoint.Z = topPoint.Z + safeDistance;
            tempway.waypointList.Add(_wireTowerConverter.WayPointConvert(outlinePoint, null, tower.RelativeHeight));
            if (lastPoint != null)
            {
                lastPoint.Z = topPoint.Z + safeDistance;
                temppointlist.Add(lastPoint);
            }

            polyline = GviMap.GeoFactory.CreatePolyline(temppointlist, GviMap.SpatialCrs);

            return tempway;
        }




        private IPoint CalculateOutlineOfInnerPoint(IPoint point, Vector3 vector, double distance, double safeZ,
            out IEulerAngle euler)
        {
            IPoint newPoint = point.Clone() as IPoint;
            var poiUtm = Wgs84ToUtm(newPoint);

            poiUtm.X = poiUtm.X + distance * vector.X;
            poiUtm.Y = poiUtm.Y + distance * vector.Y;
            //poiUtm.Z = poiUtm.Z + distance * vector.Z;

            //if (type.Equals(SignType.Inner.ToString()))
            //{
            poiUtm.Z = safeZ;
            //}

            poiUtm.Project(GviMap.SpatialCrs);

            euler = GviMap.Camera.GetAimingAngles2(poiUtm, point);

            return poiUtm;
        }

        private IPoint CalculateOutlinePoint(IPoint point, Vector3 vector, double distance,
            out IEulerAngle euler)
        {
            IPoint newPoint = point.Clone() as IPoint;
            var poiUtm = Wgs84ToUtm(newPoint);

            poiUtm.X = poiUtm.X + distance * vector.X;
            poiUtm.Y = poiUtm.Y + distance * vector.Y;
            poiUtm.Z = poiUtm.Z + distance * vector.Z;

            poiUtm.Project(GviMap.SpatialCrs);

            euler = GviMap.Camera.GetAimingAngles2(poiUtm, point);

            return poiUtm;
        }

        private Vector3 CalulateUnitVector(IPoint pt1, IPoint pt2, bool isHorizon = false)
        {
            var pt1utm = Wgs84ToUtm(pt1);

            var pt2utm = Wgs84ToUtm(pt2);

            Vector3 vactor = new Vector3() { X = pt2utm.X - pt1utm.X, Y = pt2utm.Y - pt1utm.Y, Z = pt2utm.Z - pt1utm.Z };
            if (isHorizon) vactor.Z = 0;

            vactor.Normalize();
            return vactor;
        }

        private IPoint Wgs84ToUtm(IPoint point)
        {
            var prjWkt = Wgs84UtmUtil.GetWkt(point.X);
            if (!string.IsNullOrEmpty(prjWkt)) point.ProjectEx(prjWkt);
            return point;
        }

        /// <summary>
        /// 由水平向量 及俯仰角 计算俯仰向量
        /// </summary>
        /// <param name="horiVector3"></param>
        /// <param name="type"></param>
        /// <param name="inAngle"></param>
        /// <returns></returns>
        private Vector3 GetPitchVector(Vector3 horiVector3, string type, double inAngle = 0)
        {
            double angle;

            if (type == SignType.Right.ToString()) // 向量 指向右侧，因此右侧同向为 0
            {
                angle = inAngle * Math.PI / 180;
            }
            else if (type == SignType.Left.ToString()) // 左侧相反方向为 180
            {
                angle = (180 - inAngle) * Math.PI / 180;
            }
            else if (type == SignType.TopCenter.ToString())
            {
                angle = 90 * Math.PI / 180;
            }
            else if (type == SignType.TopLeft.ToString())
            {
                angle = (180 - 60) * Math.PI / 180;
            }
            else if (type == SignType.TopRight.ToString())
            {
                angle = (60) * Math.PI / 180;
            }
            else
            {
                angle = 90 * Math.PI / 180;
            }

            double atan = Math.Atan2(horiVector3.Y, horiVector3.X);

            var vector = new Vector3()
            {
                X = Math.Cos(angle) * Math.Cos(atan),
                Y = Math.Cos(angle) * Math.Sin(atan),
                Z = Math.Sin(angle)
            };

            return vector;
        }

        /// <summary>
        /// 计算偏向向量
        /// </summary>
        /// <param name="horiVector3"></param>
        /// <param name="type"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Vector3 GetHorizonTrendVector(Vector3 horiVector3, string type, double angle)
        {
            double atan = Math.Atan2(horiVector3.Y, horiVector3.X);

            if (type == SignType.Right.ToString())
            {
                angle = angle * Math.PI / 180;
            }
            else if (type == SignType.Left.ToString())
            {
                angle = -angle * Math.PI / 180;
            }
            else if (type == SignType.Inner.ToString())
            {
                angle = (180 - angle) * Math.PI / 180;
            }
            else
            {
                return horiVector3; // 不符合类型直接返回原方向向量
            }
            var vector = new Vector3()
            {
                X = Math.Cos(atan + angle),
                Y = Math.Sin(atan + angle)
            };
            return vector;
        }
    }
}