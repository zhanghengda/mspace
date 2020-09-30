using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Device.Location;
using System.Windows.Forms;
using Mmc.Mspace.RoutePlanning.Dto;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Windows.Utils;

using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Mspace.Const.ConstDataInterface;

namespace Mmc.Mspace.RoutePlanning
{

    public class GenerateGeoJson
    {
        private RouteInfo _routeData = new RouteInfo();
        private GeoJson _geoJson;//自定义GeoJson
        private StationMissionJson _stationMissionJson;//适配地面站
        private string _geoJsonPostUrl;
        private HttpService _httpService;
        public string _routeName;
        string _fileNameExt { get; set; }


        public void initGeoJson()
        {
            _httpService = new HttpService();
        }

        public void SetGeoPointItem(IPolyline polyline)
        {
            var geoJsonItem = new GeoJson
            {
                type = "FeatureCollection",
                features = new List<GeoJsonPoint>()
            };

            for (int i = 0; i < polyline.PointCount; i++)
            {
                var item = polyline.GetPoint(i);

                var coordinate = new List<double>
                {
                    item.Y,
                    item.X,
                    item.Z
                };

                geoJsonItem.features.Add(new GeoJsonPoint
                {
                    type = "Featrue",
                    geometry = new GeoJsonPointGeometry
                    {
                        type = "Point",
                        altitudeMode = "absolute",
                        coordinates = coordinate
                    },
                    properties = new GeoJsonPointProperty
                    {
                        camposx = "",
                        camposy = "",
                        camheading = "",
                        camtilt = "",
                        camroll = "",
                        camcapture = "",
                        uavyaw = ""
                    }
                });

            }
            _geoJson = geoJsonItem;
            _routeData = RouteData(polyline);//需要修改
        }   

        public void setMMCStationMission(List<RoutePoint> routePtList)
        {
            var complexItems = new List<StationComplexItems>
            {
            };

            var stationMissionJson = new StationMissionJson
            {
                MAV_AUTOPILOT = 3,
                groundStation = "MMC Station",
                complexItems = complexItems,
                items = new List<StationItems>(),
                plannedHomePosition = new StationItems(),
                version = "1.0"
            };

            for (int i = 0; i < routePtList.Count; i++)
            {

                Console.WriteLine("GenerateGeoJsonset MMCStationMission :" + routePtList[i].Lat + routePtList[i].Lng);
                //add takeoff point
                if (i == 0)
                {
                    var coordinate_takeoff = new List<double>
                    {
                        routePtList[i].Lat,
                        routePtList[i].Lng,
                        routePtList[i].Height
                    };
                    //take off 22
                    stationMissionJson.items.Add(new StationItems
                    {
                        autoContinue = true,
                        command = 22,
                        coordinate = coordinate_takeoff,
                        frame = 3,
                        id = i + 1,
                        param1 = routePtList[i].Hover,
                        param2 = 0,
                        param3 = 0,
                        param4 = 0,
                        type = "missionItem"
                    });
                    //change speed 178
                    stationMissionJson.items.Add(new StationItems
                    {
                        autoContinue = true,
                        command = 178,
                        coordinate = coordinate_takeoff,
                        frame = 3,
                        id = i,
                        param1 = 1,
                        param2 = routePtList[i].Speed,
                        param3 = -1,
                        param4 = 0,
                        type = "missionItem"
                    });

                    //home position
                    stationMissionJson.plannedHomePosition = new StationItems
                    {
                        autoContinue = true,
                        command = 16,
                        coordinate = coordinate_takeoff,
                        frame = 0,
                        id = i,
                        param1 = routePtList[i].Hover,
                        param2 = 0,
                        param3 = 0,
                        param4 = -1,
                        type = "missionItem"
                    };

                }

                var coordinate = new List<double>
                {
                    routePtList[i].Lat,
                    routePtList[i].Lng,
                    routePtList[i].Height
                };

                stationMissionJson.items.Add(new StationItems
                {
                    autoContinue = true,
                    command = 16,
                    coordinate = coordinate,
                    frame = 3,
                    id = i + 1,
                    param1 = routePtList[i].Hover,
                    param2 = 0,
                    param3 = 0,
                    param4 = 0,
                    type = "missionItem"
                });

                if(routePtList[i].Speed != 0)
                {
                    //speed
                    stationMissionJson.items.Add(new StationItems
                    {
                        autoContinue = true,
                        command = 178,
                        coordinate = coordinate,
                        frame = 3,
                        id = i,
                        param1 = 1,
                        param2 = routePtList[i].Speed,
                        param3 = -1,
                        param4 = 0,
                        type = "missionItem"
                    });
                }

            }

            _stationMissionJson = stationMissionJson;
            _routeData = PtRouteListData(routePtList);
        }

        private IPolyline ptListToPolyline(List<RoutePoint> routePtList)
        {
            IPolyline polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;

            IPoint point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;

            for (int i = 0; i < routePtList.Count; i++)
            {
                point.X = routePtList[i].Lng;
                point.Y = routePtList[i].Lat;
                Console.WriteLine("GenerateGeoJsonset MMCStationMission :" + routePtList[i].Lat + routePtList[i].Lng);
                polyline.AppendPoint(point);
            }
            return polyline;
        }

        private RouteInfo PtRouteListData(List<RoutePoint> routePtList)
        {
            double length = 0;
            double time = 0;

            IPoint pointFirst = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;
            IPoint pointEnd = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ) as IPoint;

            if (routePtList.Count >= 1)
                for (int i = 1; i < routePtList.Count; i++)
                {
                    pointFirst.X = routePtList[i].Lng;
                    pointFirst.Y = routePtList[i].Lat;

                    pointEnd.X = routePtList[i - 1].Lng;
                    pointEnd.Y = routePtList[i - 1].Lat;
                    //double dis = pointFirst.GetDistance(pointEnd);
                    double dis = GetDistance(routePtList[i].Lat ,routePtList[i].Lng , routePtList[i - 1].Lat , routePtList[i - 1].Lng);
                    double sec = 0;
                    if (routePtList[i].Speed != 0)
                        sec = dis / routePtList[i].Speed;
                    length += dis;
                    time += sec;
                }

            return new RouteInfo()
            {
                testing_area_type = 0,
                name = _routeName,
                voyage_point_num = routePtList.Count,
                voyage_time = ((int)time  / 60) ==0 ? 1 : (int)time / 60,//default vehicle speed is 2m/s * 60S
                voyage = (int)length,
                flight_course_json = _stationMissionJson//pointStr  _geoJson
            };
        }


        public void SaveFileDialog()
        {
            string localFilePath, filepath;
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = " Save to KML Files(*.json)|*.*",//*.kml|All files(*.*)|
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".json",
                FilterIndex = 2,
                AddExtension = true,
                RestoreDirectory = true
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                localFilePath = fileDialog.FileName.ToString();
                _fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                filepath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                var jsonData = JsonUtil.SerializeToString(_geoJson);
                Console.WriteLine("exportGeoJsonFile" + jsonData);
                SaveToFile(localFilePath, jsonData);
            }
        }

        public void SaveStationMissionFileDialog()
        {
            string localFilePath, filepath;
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = " Save to KML Files(*.mission)|*.*",//*.kml|All files(*.*)|
                FileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".mission",
                FilterIndex = 2,
                AddExtension = true,
                RestoreDirectory = true
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                localFilePath = fileDialog.FileName.ToString();
                _fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                filepath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                var jsonData = JsonUtil.SerializeToString(_stationMissionJson);
                Console.WriteLine("exportGeoJsonFile" + jsonData);

                SaveToFile(localFilePath, jsonData);
            }
        }

        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 获取当前本地时间戳
        /// </summary>
        /// <returns></returns>      
        public int GetCurrentTimeUnix()
        {
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            int t = (int)cha.TotalSeconds;
            return t;
        }

        /// <summary>
        /// </summary>
        /// <param name="desktopPath"></param>
        /// <param name="json"></param>
        private static void SaveToFile(string desktopPath, string jsonData)
        {

            using (FileStream fs = new FileStream(string.Format("{0}", desktopPath), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(jsonData);
                }

            }
        }

        public bool CreateGeoJson()
        {
            string api = UAVInterface.AddTraceInf;
            var jsonData = JsonUtil.SerializeToString(_routeData);//_geoJson  _routeData
            bool status = HttpServiceHelper.Instance.PostRequestForStatus(api, jsonData);
            return status;

            //var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            //_geoJsonPostUrl = string.Format("{0}/api/flight-course/create", json.poiUrl);
            //_httpService.Token = HttpServiceUtil.Token;

            //Console.WriteLine("-----------jsonData \n" + HttpServiceUtil.Token + _geoJsonPostUrl + ": " + jsonData);
            //return _httpService.PostJsonData(_geoJsonPostUrl, jsonData);
        }

        private RouteInfo RouteData(IPolyline polyline)
        {
            var cmd = new RouteInfo();
            //var pointStr = JsonUtil.SerializeToString(_geoJson);//_geoJson

            cmd.testing_area_type = 0;
            cmd.voyage = 0;
            cmd.name = _routeName;
            cmd.voyage_point_num = getRoutePointCount(polyline);
            cmd.voyage_time = (int)(getRouteLength(polyline) / 120);//default vehicle speed is 2m/s * 60S
            cmd.voyage = (int)getRouteLength(polyline);
            cmd.flight_course_json = _stationMissionJson;//pointStr  _geoJson
            return cmd;
        }

        private int getRoutePointCount(IPolyline polyline)
        {
            return polyline.PointCount;
        }

        private double getRouteLength(IPolyline polyline)
        {
            double length = 0;
            if (polyline.PointCount >= 1)
                for (int i = 1; i < polyline.PointCount; i++)
                {

                    double dis = polyline.GetPoint(i).GetDistance(polyline.GetPoint(i - 1));
                    length += dis;
                }
            return length;
        }

    }
}