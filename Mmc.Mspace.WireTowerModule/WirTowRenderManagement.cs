using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.WireTowerModule.DTO;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Mmc.Mspace.WireTowerModule
{
    class WirTowRenderManagement : Singleton<WirTowRenderManagement>
    {
        #region varies

        public Action<string, double, double, double> OnUpdatePointPosition;
        private readonly string TAG = "MmcWireTowerData";
        private readonly string LineFile = "MmcWireLine.json";
        private readonly WireTowerConverter _wireTowerConverter;

        public List<LineForClient> Lines { get; set; }
        #endregion

        public WirTowRenderManagement()
        {
            _wireTowerConverter = new WireTowerConverter();
            Lines = ReadLineData();
        }
        private void mapInteractModeManagement(bool onEvent)
        {
            if (onEvent)
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            }
            else
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            }
        }

        public void MapClickSelectEventManagement(bool onEvent)
        {
            mapInteractModeManagement(onEvent);

            if (onEvent)
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;
            }
            else
            {
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            }
        }


        public void MapTransformMoveEventManagement(bool onEvent)
        {
            mapInteractModeManagement(onEvent);
            if (onEvent)
            {
                GviMap.AxMapControl.TransformHelper.Type = gviEditorType.gviEditorMove;
                GviMap.AxMapControl.RcTransformHelperMoving -= AxMapControl_RcTransformHelperMoving;
                GviMap.AxMapControl.RcTransformHelperMoving += AxMapControl_RcTransformHelperMoving;
            }
            else
            {
                GviMap.AxMapControl.TransformHelper.Type = gviEditorType.gviEditorNone;
                GviMap.AxMapControl.RcTransformHelperMoving -= AxMapControl_RcTransformHelperMoving;
            }
        }

        public void SetTransformPoint(double x, double y, double z)
        {
            IVector3 vector = new Vector3() { X = x, Y = y, Z = z };
            GviMap.AxMapControl.TransformHelper.SetPosition(vector);
        }

        private void AxMapControl_RcTransformHelperMoving(IVector3 Position)
        {
            var temp = Position;
            if (temp == null) return;

            OnUpdatePointPosition("Move", temp.X, temp.Y, temp.Z);
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask,
            gviMouseSelectMode EventSender)
        {

            var temp = IntersectPoint;
            if (temp == null) return;
            SetTransformPoint(temp.X, temp.Y, temp.Z);
            OnUpdatePointPosition("Click", temp.X, temp.Y, temp.Z);
        }

        //绘制塔及左右点连线
        public void RenderTowerInfo(TowerForClient tower)
        {
            if (tower == null || tower.SignList.Count <= 0) return;

            string towerIcon = "项目数据\\shp\\IMG_POI\\alphabet_N.png";

            IPOI towerpoi = GviMap.GeoFactory.CreatePoi(tower.X, tower.Y, tower.Z, towerIcon, tower.Name, size: 26,
                isShow: true, crs: GviMap.SpatialCrs);
            CreateRenderPoi(towerpoi, tower.Id.ToString() + tower.Serial);

            FlyToPoint(tower.X, tower.Y, tower.Z);

            var leftTop = tower.SignList.ToList().Find(p => p.type.Equals(CommonContract.SignType.TopLeft.ToString()));
            var rightTop = tower.SignList.ToList().Find(p => p.type.Equals(CommonContract.SignType.TopRight.ToString()));

            IPoint AleftPoint = GviMap.PointManager.CreatePoint(leftTop.X, leftTop.Y, leftTop.Z);

            IPoint ArigthPoint = GviMap.PointManager.CreatePoint(rightTop.X, rightTop.Y, rightTop.Z);

            ArigthPoint.Z = AleftPoint.Z + 0.5;
            AleftPoint.Z = ArigthPoint.Z;
            var votorPoints = new List<IPoint>()
            {
                AleftPoint,
                ArigthPoint
            };

            var votorLine = GviMap.GeoFactory.CreatePolyline(votorPoints, GviMap.SpatialCrs);
            var curve = new CurveSymbol()
            { Width = 0.2f, Color = Color.DarkMagenta, Pattern = gviDashStyle.gviDashSmall };

            this.RenderPolyline(votorLine, curve);
        }

        public void RenderLookAt(RouteForClient routemodel)
        {
            if (routemodel?.TowerCount > 0)
            {
                var ptSym = new SimplePointSymbol() { Size = 10,FillColor=Color.White };
                var curSym = new CurveSymbol() {Width=-2, Color=ColorConvert.UintToColor(0xff14C5D7) };
                foreach (var item in routemodel.LeftWay)
                {
                    foreach (var waypoint in item.waypointList)
                    {
                        if (waypoint.photoPositionList.Count > 0)
                        {
                            var pt0 = GviMap.GeoFactory.CreatePoint(waypoint.aircraftLocationLongitude, waypoint.aircraftLocationLatitude, waypoint.aircraftLocationAltitude, GviMap.SpatialCrs);
                            var pt1 = GviMap.GeoFactory.CreatePoint(waypoint.photoPositionList[0].longitude, waypoint.photoPositionList[0].latitude, waypoint.photoPositionList[0].altitude, GviMap.SpatialCrs);
                            var rPt0 = GviMap.ObjectManager.CreateRenderPoint(pt0, ptSym);
                            var rPt1 = GviMap.ObjectManager.CreateRenderPoint(pt1, ptSym);
                            var line = GviMap.GeoFactory.CreatePolyline(pt0, pt1, GviMap.SpatialCrs);
                            var rLine = GviMap.ObjectManager.CreateRenderPolyline(line, curSym);

                            var angle = GviMap.Camera.GetAimingAngles2(pt0, pt1);
                            GviMap.RPolylineManager.AddRenObj(TAG, rLine.Guid.ToString(), null, rLine);
                            GviMap.RPoiManager.AddRenObj(TAG, rPt0.Guid.ToString(), null, rPt0);
                            GviMap.RPoiManager.AddRenObj(TAG, rPt1.Guid.ToString(), null, rPt1);
                        }
                    }

                }
                foreach (var item in routemodel.RightWay)
                {
                    foreach (var waypoint in item.waypointList)
                    {
                        if (waypoint.photoPositionList.Count > 0)
                        {
                            var pt0 = GviMap.GeoFactory.CreatePoint(waypoint.aircraftLocationLongitude, waypoint.aircraftLocationLatitude, waypoint.aircraftLocationAltitude, GviMap.SpatialCrs);
                            var pt1 = GviMap.GeoFactory.CreatePoint(waypoint.photoPositionList[0].longitude, waypoint.photoPositionList[0].latitude, waypoint.photoPositionList[0].altitude, GviMap.SpatialCrs);
                            var rPt0 = GviMap.ObjectManager.CreateRenderPoint(pt0, ptSym);
                            var rPt1 = GviMap.ObjectManager.CreateRenderPoint(pt1, ptSym);
                            var angle = GviMap.Camera.GetAimingAngles2(pt0, pt1);
                            var line = GviMap.GeoFactory.CreatePolyline(pt0, pt1, GviMap.SpatialCrs);
                            var rLine = GviMap.ObjectManager.CreateRenderPolyline(line, curSym);
                            GviMap.RPolylineManager.AddRenObj(TAG, rLine.Guid.ToString(), null, rLine);
                            GviMap.RPoiManager.AddRenObj(TAG, rPt0.Guid.ToString(), null, rPt0);
                            GviMap.RPoiManager.AddRenObj(TAG, rPt1.Guid.ToString(), null, rPt1);
                        }

                    }

                }
            }

        }

        public void FlyToPoint(double x, double y, double z)
        {
            GviMap.Camera.GetCamera(out IVector3 position, out IEulerAngle euler);
            position.X = x;
            position.Y = y;
            position.Z = z;
            GviMap.Camera.LookAt(position, 200, euler);
        }

        public void FlyToGeometry(IGeometry geometry)
        {
            GviMap.Camera.LookAtEnvelope(geometry.Envelope);
        }

        public void RenderSignsOfTower(List<SignModel> signs)
        {
            if (signs == null || signs.Count < 0) return;
            foreach (var sign in signs)
            {
                RenderSignInfo(sign);
            }
        }


        public void RenderSignInfo(SignModel sign)
        {
            string icon = "项目数据\\shp\\IMG_POI\\alphabet_S.png";

            IPOI poi = GviMap.GeoFactory.CreatePoi(sign.X, sign.Y, sign.Z, icon, sign.serial, size: 26, isShow: true,
                crs: GviMap.SpatialCrs);
            CreateRenderPoi(poi, sign.id.ToString() + sign.serial);
        }

        public void CreateRenderPoi(IPOI poi, string key)
        {
            var rPoi = GviMap.ObjectManager.CreateRPoi(poi, maxVisibleDistance: WebConfig.TracePoiMaxDistance);
            GviMap.RPoiManager.AddRenObj(TAG, key, null, rPoi);
        }


        public void AddOrUpdateRPoi(string key, SignModel sign)
        {
            if (string.IsNullOrEmpty(key) || sign == null) return;
            if (GviMap.RPoiManager.ContainKey(TAG, key))
            {
                var rPoi = GviMap.RPoiManager.GetRenObj(TAG, key).Item2;

                string icon = "项目数据\\shp\\IMG_POI\\alphabet_S.png";

                IPOI poi = GviMap.GeoFactory.CreatePoi(sign.X, sign.Y, sign.Z, icon, sign.serial, size: 26,
                    isShow: true,
                    crs: GviMap.SpatialCrs);
                GviMap.RPoiManager.UpdateRenObj(TAG, key, null, rPoi);
            }
            else
            {
                RenderSignInfo(sign);
            }
        }

        public void DeleteRenderObj(string key)
        {
            GviMap.RPoiManager.DeleteRenObj(TAG, key);
            GviMap.RPolylineManager.DeleteRenObj(TAG, key);
        }

        public string ReadFile(string filepath)
        {
            string result = string.Empty;
            if (!File.Exists(filepath)) return result;

            result = File.ReadAllText(filepath);

            return result;
        }

        public void RenderPolyline(IPolyline inline, ICurveSymbol symbol)
        {
            if (inline == null) return;
            symbol = symbol ?? new CurveSymbol()
            { Width = 0.1f, Color = Color.Blue, Pattern = gviDashStyle.gviDashSmall };
            IRenderPolyline rLine = GviMap.ObjectManager.CreateRenderPolyline(inline, symbol);
            GviMap.RPolylineManager.AddRenObj(TAG, rLine.Guid.ToString(), null, rLine);
        }
        //绘制航线
        internal void RenderRoute(RouteForClient routemodel)
        {
            if (routemodel?.Towers?.Count > 0)
            {
                foreach (var tower in routemodel.Towers)
                {
                    RenderTowerInfo(tower);
                    //RenderSignsOfTower(tower.SignList.ToList());
                }
            }

            ICurveSymbol symbol = new CurveSymbol()
            {
                Width = 0.1f,
                Color = Color.Blue,
                Pattern = gviDashStyle.gviDashSmall
            };
            if (routemodel?.LeftLineList?.Count > 0)
            {
                RenderRouteOfTower(routemodel.LeftLineList, symbol);
            }

            if (routemodel?.RightLineList?.Count > 0)
            {
                RenderRouteOfTower(routemodel.RightLineList, symbol);
            }

            GviMap.Camera.LookAtEnvelope(routemodel?.RightLineList?.FirstOrDefault()?.Envelope);
        }

        public void RenderRouteOfTower(IReadOnlyCollection<IPolyline> polylines, ICurveSymbol symbol)
        {
            if (!(polylines?.Count > 0)) return;
            foreach (var item in polylines)
            {
                RenderPolyline(item, symbol);
            }
        }

        public void ClearRenderObj()
        {
            GviMap.RPoiManager.DeleteRenObjs(TAG);
            GviMap.RPolylineManager.DeleteRenObjs(TAG);
        }

        public void WriteFile(string filePath, string dataStr)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(dataStr);
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
        }

        public string ReadData(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            var data = File.ReadAllText(filePath);
            return data;
        }


        public string GetLocalWirePath(string fileName)
        {
            var dir = System.Windows.Forms.Application.LocalUserAppDataPath + "\\" + TAG;
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return dir + "\\" + fileName;
        }

        private List<LineForClient> ReadLineData()
        {
            List<LineForClient> lineClients = new List<LineForClient>();
            try
            {
                string lineFilePath = GetLocalWirePath(LineFile);

                List<LineModel> lines = JsonUtil.DeserializeFromFile<List<LineModel>>(lineFilePath);

                if (lines == null || lines.Count <= 0) return lineClients;
                foreach (var model in lines)
                {
                    lineClients.Add(_wireTowerConverter.LineConvert(model));
                }

                return lineClients;
            }
            catch
            {
                return lineClients;
            }
        }


    }
}
