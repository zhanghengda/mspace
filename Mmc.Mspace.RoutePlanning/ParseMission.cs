using System;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Gvitech.CityMaker.FdeGeometry;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.WebSockets;
using System.Threading;
using System.Diagnostics;
using Mmc.Windows.Utils;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Device.Location;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.RoutePlanning.Dto;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RoutePlanning.Grid;

namespace Mmc.Mspace.RoutePlanning
{
    public class ParseMission
    {
        public List<RoutePoint> _routePtList;
        public ParseMission()
        {
            Console.WriteLine("ParseMission init");

        }

        public IPolyline readMissionFile()
        {
            IPolyline polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            int idCount = 0;
            double speed = 0;//如果不设置默认用前一个点的速度
            if (_routePtList == null)
                _routePtList = new List<RoutePoint>();
            else
                _routePtList.Clear();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Please Select The Kml File";
            dialog.Filter = "Mission File|*.mission";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                var json = JsonUtil.DeserializeFromFile<dynamic>(file);
                foreach (var item in json.items)
                {
                    double trigger = 0;//(double)item.trigger;
                    int isCameraTrigger = 0;//(int)item.isCameraTrigger;
                    IPoint tempPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    RoutePoint routePt = null;

                    if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_WAYPOINT ||
                        item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_TAKEOFF ||
                        item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_NAV_LAND
                        )
                    {
                        var coordArr = item.coordinate;
                        var coordX = (double)coordArr[0];
                        var coordY = (double)coordArr[1];
                        var coordZ = (double)coordArr[2];
                        var hover = (double)item.param1;
                        routePt = new RoutePoint(coordY, coordX, coordZ, speed, hover, trigger, isCameraTrigger);
                        if (routePt != null)
                        {
                            tempPoint.SetPostion(coordY, coordX, coordZ);
                            polyline.AppendPoint(tempPoint);
                            _routePtList.Add(routePt);
                            idCount++;
                        }
                    }

                    if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_DO_CHANGE_SPEED)
                    {
                        _routePtList[idCount - 1].Speed = (double)item.param2;

                    }
                    else if (item.command == (int)Ardupilotmega.MAV_CMD.MAV_CMD_DO_SET_CAM_TRIGG_DIST)
                    {
                        _routePtList[idCount - 1].Trigger = (double)item.param1;
                    }
                }
                //foreach (var item in json.items)
                //{
                //    switch ((int)item.command)
                //    {
                //        case 16:
                //        case 22:
                //        case 21:
                //            var coord = item.coordinate;
                //            IPoint tempPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                //            double coordX = coord[0];
                //            double coordY = coord[1];
                //            double coordZ = coord[2];
                //            var speed = (double)item.speed;
                //            var hover = (double)item.hover;
                //            var trigger = (double)item.trigger;
                //            var isCameraTrigger = (int)item.isCameraTrigger;
                //            RoutePoint routePt = new RoutePoint(coordX, coordY, coordZ, speed, hover, trigger, isCameraTrigger);
                //            _routePtList.Add(routePt);
                //            tempPoint.SetPostion(coordY, coordX, coordZ);
                //polyline.AppendPoint(tempPoint);
                //            break;
                //        default:
                //            break;
                //    }
                //}

                _routePtList.RemoveAt(0);
            }
            return polyline;
        }
    }
}
