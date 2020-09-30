using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.UavModule.HudPanel;
using System.Windows.Forms;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class UavTracingModel : CheckedToolItemModel
    {

        private const double EARTH_RADIUS = 6378137;
        private const double LIMIT_DISTANCE = 200;
        private ICurveSymbol _curveSymbol = null;
        private IDynamicObject _dynamicObject = null;
        private bool _isFollow = true;
        private bool _isStop = false;
        private IPolyline _line = null;
        private IMotionable _motionable = null;
        private IMotionable _motionableTableLabel = null;
        private IMotionPath _motionPath = null;
        private IModelPoint _mp;

        private IRenderPolyline _rLine = null;
        private ISkinnedMesh _skinMesh = null;
        //private IRenderModelPoint _skinMesh = null;
        private IMultiPoint _multiPoint = null;
        private IRenderMultiPoint _rpoint = null;
        private ISimplePointSymbol _pointSym = null;

        //choose altitude model
        private bool _isAbsoluteAltitude = true;
        public string ground_serial { get; set; }
        public string ground_state { get; set; }

        private ITableLabel _dynamicTableLabel = null;
        /// <summary>
        /// uav hud panel
        /// </summary>
        private HUD hudView;
        Form hudDropout;

        private UavTrace _uavTrace;
        private bool _isHudVisible = false;

        [XmlIgnore]
        public bool IsHudVisible
        {
            get { return this._isHudVisible; }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._isHudVisible, value, "IsHudVisible");
                this.isHudUse(_isHudVisible);
            }
        }
        [XmlIgnore]
        public UavTrace uavTraceRealTime
        {
            get { return _uavTrace; }
            set { _uavTrace = value; }
        }
        [XmlIgnore]
        public bool IsFollow
        {
            get { return this._isFollow; }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this._isFollow, value, "IsFollow");
                this.FollowToUav(_isFollow);
            }
        }

        public Action<UavTrace> OnTracking { get; set; }

        public void isHudUse(bool isVisible)
        {
            if (isVisible)
            {
                hudShow();//与鼠标获取位置冲突
            }
            else
            {
                hudUncheck();
            }
        }

        public void FollowToUav(bool isFollow)
        {
            // _isFollow = isFollow;
            if (_isFollow)
            {
                if (_skinMesh != null)
                    GviMap.Camera.FlyToObject(_skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);//上方跟随
            }
            else
            {
                IVector3 v3 = null; IEulerAngle angle = null;
                GviMap.Camera.GetCamera(out v3, out angle);
                GviMap.Camera.SetCamera(v3, angle, gviSetCameraFlags.gviSetCameraNoFlags);//上方跟随
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _curveSymbol = new CurveSymbol();
            hudInit();
        }

        public override void OnChecked()
        {
            if (ground_state == "1")
            {
                base.OnChecked();
                StartMotionPath();
            }
            else
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeviceNotOnline"));
        }

        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();
                _isStop = true;
                ReleaseRObj();
                //isHudUse(false);
                hudRelease();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private IPoint CreateLocalPoint(UavTrace uavTrace)
        {
            var startPt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            if (_isAbsoluteAltitude)
                startPt.SetPostion(uavTrace.longitude, uavTrace.latitude, Math.Round(uavTrace.height, 2));
            else
                startPt.SetPostion(uavTrace.longitude, uavTrace.latitude, Math.Round(uavTrace.altitude, 2));
            return startPt;
        }

        private IModelPoint CreateModelPoint(IVector3 startPt, int catId = 1)
        {
            string fileName;
            var mp = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ) as IModelPoint;
            var matrix = new Matrix();

            switch (catId)
            {
                case 1:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\data\x\Uav\1800_slow.X";
                    break;
                case 20:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\data\x\Uav\ship.X";
                    //设置缩放比例
                    mp.SelfScale(0.005, 0.005, 0.005);
                    mp.SelfScale(0.05, 0.05, 0.05);
                    break;
                default:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\data\x\Uav\1800_slow.X";
                    break;
            }
            mp.ModelName = fileName;
            mp.SpatialCRS = GviMap.SpatialCrs;
            // 设置位置
            matrix.MakeIdentity();
            matrix.SetTranslate(startPt);
            mp.FromMatrix(matrix);
            return mp;
        }

        private IEulerAngle GetEulerAngle(UavTrace uavTrace)
        {
            var v3 = new EulerAngle();
            if (!(string.IsNullOrEmpty(uavTrace.yaw) || string.IsNullOrEmpty(uavTrace.pitch) || string.IsNullOrEmpty(uavTrace.roll)))
            {
                v3.Set(double.Parse(uavTrace.yaw), double.Parse(uavTrace.pitch), double.Parse(uavTrace.roll));
                return v3;
            }
            return null;
        }

        private bool GetUavInfo(UavTrace uavTrace, out IVector3 location, out IEulerAngle angle)
        {
            location = null;
            angle = null;
            if (uavTrace == null)
                return false;
            location = new Vector3();
            angle = new EulerAngle();
            if (_isAbsoluteAltitude)
                location.Set(uavTrace.longitude, uavTrace.latitude, uavTrace.height);
            else
                location.Set(uavTrace.longitude, uavTrace.latitude, uavTrace.altitude);

            if (!(string.IsNullOrEmpty(uavTrace.yaw) || string.IsNullOrEmpty(uavTrace.pitch) || string.IsNullOrEmpty(uavTrace.roll)))
            {
                angle.Set(double.Parse(uavTrace.yaw), double.Parse(uavTrace.pitch), double.Parse(uavTrace.roll));
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool IsSamePoint(IVector3 ptA, IVector3 ptB)
        {
            return ptA.X == ptB.X && ptA.Y == ptB.Y && ptA.Z == ptB.Z;
        }


        private void ReleaseRObj()
        {
            try
            {
                //释放内存
                _multiPoint?.ReleaseComObject();
                _multiPoint = null;
                if (_rpoint != null)
                {
                    GviMap.ObjectManager.DeleteObject(_rpoint.Guid);
                    GviMap.ObjectManager.ReleaseRenderObject(_rpoint);
                    _rpoint = null;
                }
                if (_dynamicTableLabel != null)
                {
                    GviMap.ObjectManager.DeleteObject(_dynamicTableLabel.Guid);
                    GviMap.ObjectManager.ReleaseRenderObject(_dynamicTableLabel);
                    _dynamicTableLabel = null;
                }

                if (_line != null)
                {
                    //_line?.RemovePoints(0, _line.PointCount - 1);
                    for (int i = 0; i < _line.PointCount; i++)
                    {
                        var pt = _line.GetPoint(i);
                        pt.ReleaseComObject();
                    }
                    _line?.ReleaseComObject();
                    _line = null;
                }
                if (_rLine != null)
                {
                    GviMap.ObjectManager.DeleteObject(_rLine.Guid);
                    GviMap.ObjectManager.ReleaseRenderObject(_rLine);
                    _rLine = null;
                }
                _mp?.ReleaseComObject();
                if (_motionPath != null)
                {
                    _motionable?.Unbind();
                    _motionableTableLabel?.Unbind();
                    _motionPath.Stop();
                    //_motionPath.ClearWaypoints();//add by hengda 暂时注释，当打开多个无人机轨迹时，关闭无人机界面时报内存损坏
                    GviMap.ObjectManager.DeleteObject(_motionPath.Guid);
                    // GviMap.ObjectManager.ReleaseRenderObject(_motionPath);
                    _motionPath = null;
                }
                if (_skinMesh != null)
                {
                    GviMap.ObjectManager.DeleteObject(_skinMesh.Guid);
                    GviMap.ObjectManager.ReleaseRenderObject(_skinMesh);
                    _skinMesh = null;
                }
            }
            catch (Exception ex)
            {
                SystemLog.WriteLog(ex.ToString());
            }

        }

        private string RequestUavTrace(HttpService httpservice, string url)
        {
            try
            {
                httpservice.Token = HttpServiceUtil.Token;
                var result = httpservice.HttpRequestAsync(url);
                return result;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                if (ex?.HResult == -2146233088 || ex?.HResult == -2146233079)
                {
                    throw ex;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns>mile</returns>
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
        /// To Rad
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private dynamic IsNullOrEmpty(dynamic str)
        {
            return string.IsNullOrEmpty((string)str) ? "0" : str;
        }

        /// <summary>
        /// 无人机实时动态路径
        /// 注意：运动物体的飞行速度不能大于实际点的飞行速度，否则三维引擎会报错
        /// </summary>
        private void StartMotionPath()
        {
            _isStop = false;

            _curveSymbol.Width = -4;//三个像素
            _curveSymbol.Color = ColorConvert.UintToColor(GviColors.Yellow);

            IEulerAngle angle = null;
            IVector3 lastPt = new Vector3();
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}/api/aircraft/get-three-dim-data?ground_serial=", json.poiUrl) + ground_serial;
            var httpservice = new HttpService();

            var uavResult = string.Empty;
            //获取无人机位置信息
            try
            {
                uavResult = RequestUavTrace(httpservice, url);
            }
            catch (Exception ex)
            {
                if (ex?.HResult == -2146233088 || ex?.HResult == -2146233079)//处理断网或切换网络异常
                    return;
            }
            var uavResultJson = JsonUtil.DeserializeFromString<dynamic>(uavResult);

            _uavTrace = new UavTrace();

            if ((bool)uavResultJson.status)
            {
                _uavTrace.goods_id = Convert.ToInt32(IsNullOrEmpty((string)uavResultJson?.data?.goods_id));
                _uavTrace.cat_id = Convert.ToInt32(IsNullOrEmpty((string)uavResultJson?.data?.cat_id));
                _uavTrace.unmanned_type = IsNullOrEmpty(uavResultJson?.data?.unmanned_type);
                _uavTrace.ground_serial = IsNullOrEmpty(uavResultJson?.data?.ground_serial);
                _uavTrace.mount_type = IsNullOrEmpty(uavResultJson?.data?.mount_type);
                _uavTrace.longitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.longitude));
                _uavTrace.latitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.latitude));
                _uavTrace.altitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.altitude));
                _uavTrace.height = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.height));
                _uavTrace.roll = IsNullOrEmpty(uavResultJson?.data?.roll);
                _uavTrace.pitch = IsNullOrEmpty(uavResultJson?.data?.pitch);
                _uavTrace.yaw = IsNullOrEmpty(uavResultJson?.data?.yaw);
                _uavTrace.speed = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.speed));
                _uavTrace.rtmp_url = IsNullOrEmpty(uavResultJson?.data?.rtmp_url);
                _uavTrace.voltage = IsNullOrEmpty(uavResultJson?.data?.voltage);
                _uavTrace.climb_rate = IsNullOrEmpty(uavResultJson?.data?.climb_rate);
                _uavTrace.distance_to_home = IsNullOrEmpty(uavResultJson?.data?.distance_to_home);
                _uavTrace.flight_time = IsNullOrEmpty(uavResultJson?.data?.flight_time);
                _uavTrace.current = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.current));
                _uavTrace.battary_remain = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.battary_remain));
                _uavTrace.ground_speed = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.ground_speed));
                _uavTrace.air_speed = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.air_speed));
                _uavTrace.sat_count = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.sat_count));
                _uavTrace.distance_next = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.distance_next));
                _uavTrace.flight_mode = IsNullOrEmpty(uavResultJson?.data?.flight_mode);
                _uavTrace.flight_sortie = IsNullOrEmpty(uavResultJson?.data?.flight_sortie);
            }

            if (_uavTrace == null)
            {
                System.Windows.MessageBox.Show(Helpers.ResourceHelper.FindKey("Nolocationretry"));
                return;
            }

            this.OnTracking?.Invoke(uavTraceRealTime);

            IPoint startPt = CreateLocalPoint(_uavTrace);
            lastPt = startPt.Position;
            //创建临时dian
            var tempPt = CreateLocalPoint(_uavTrace);
            //创建运动物体
            if (_motionPath == null)
            {
                _motionPath = GviMap.ObjectManager.CreateMotionPath();
                _motionPath.CrsWKT = GviMap.SpatialCrs.AsWKT();
            }
            var scale = new Vector3();
            //scale.Set(0.01, 0.01, 0.01);
            scale.Set(2, 2, 2);
            angle = GetEulerAngle(_uavTrace);
            _motionPath.AddWaypoint(startPt.Position, angle, scale, 0);

            // 创建运动轨迹线
            _line = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            _line.SpatialCRS = GviMap.SpatialCrs;
            _line.AppendPoint(startPt);
            _rLine = GviMap.ObjectManager.CreateRenderPolyline(_line, _curveSymbol);


            // 创建multipoint 
            _multiPoint = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPoint,
                gviVertexAttribute.gviVertexAttributeZ) as IMultiPoint;
            _multiPoint.AddPoint(startPt);
            _multiPoint.SpatialCRS = GviMap.SpatialCrs;
            _pointSym = new SimplePointSymbol();
            _pointSym.Size = 5;
            _pointSym.FillColor = ColorConvert.UintToColor(GviColors.OrangeRed);
            _pointSym.OutlineColor = ColorConvert.UintToColor(GviColors.Red);
            _rpoint = GviMap.ObjectManager.CreateRenderMultiPoint(_multiPoint, _pointSym, GviMap.ProjectTree.RootID);


            // 创建模型的骨骼动画
            var rotation = new EulerAngle();
            var curPt = new Vector3();
            _mp = CreateModelPoint(startPt.Position, _uavTrace == null ? 1 : _uavTrace.cat_id);
            {
                _skinMesh = GviMap.ObjectManager.CreateSkinnedMesh(_mp, GviMap.ProjectTree.RootID);
                if (_skinMesh == null)
                {
                    System.Windows.MessageBox.Show(Helpers.ResourceHelper.FindKey("Creationfailed"));
                    return;
                }
                _skinMesh.Envelope.SetByEnvelope(_mp.Envelope);
                _skinMesh.Loop = true;
                _skinMesh.Play();
                _skinMesh.MaxVisibleDistance = 1000;
                _skinMesh.ViewingDistance = 100;

                // _skinMesh = GviMap.ObjectManager.CreateRenderModelPoint(_mp, new ModelPointSymbol() { }, GviMap.ProjectTree.RootID);
                if (_skinMesh == null)
                {
                    System.Windows.MessageBox.Show(Helpers.ResourceHelper.FindKey("Creationfailed"));
                    return;
                }
                _skinMesh.Envelope.SetByEnvelope(_mp.Envelope);
                //_skinMesh.Loop = true;
                //_skinMesh.Play();
                _skinMesh.MaxVisibleDistance = 1000;
                _skinMesh.ViewingDistance = 20;

                //先直飞到运动物体
                GviMap.Camera.FlyTime = 0;
                GviMap.Camera.LookAtEnvelope(_skinMesh.Envelope);

                //设置相机跟随运动物体
                GviMap.Camera.FlyToObject(_skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);
            }

            // 绑定到运动路径
            _motionable = _skinMesh as IMotionable;
            var position = new Vector3();
            position.Set(0, 0, 0);
            _motionable.Bind(_motionPath, position, 0, 0, 0);
            double timeCount = 0;

            //window
            LoadTableLable(_uavTrace == null ? 1 : _uavTrace.cat_id);

            Thread thread = new Thread(() =>
            {
                var timeSpan = 500; //500ms
                while (true)
                {
                    Thread.Sleep(timeSpan);
                    try
                    {
                        uavResult = RequestUavTrace(httpservice, url);
                    }
                    catch (Exception ex)
                    {
                        if (ex?.HResult == -2146233088 || ex?.HResult == -2146233079) //处理断网或切换网络异常
                        {
                            _isStop = true;
                            break;
                        }
                    }

                    if (uavResult == null)
                        continue;
                    uavResultJson = JsonUtil.DeserializeFromString<dynamic>(uavResult);
                    if ((bool)uavResultJson.status)
                    {
                        try
                        {
                            _uavTrace.goods_id = Convert.ToInt32(IsNullOrEmpty((string)uavResultJson?.data?.goods_id));
                            _uavTrace.cat_id = Convert.ToInt32(IsNullOrEmpty((string)uavResultJson?.data?.cat_id));
                            _uavTrace.unmanned_type = IsNullOrEmpty(uavResultJson?.data?.unmanned_type);
                            _uavTrace.ground_serial = IsNullOrEmpty(uavResultJson?.data?.ground_serial);
                            _uavTrace.mount_type = IsNullOrEmpty(uavResultJson?.data?.mount_type);
                            _uavTrace.longitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.longitude));
                            _uavTrace.latitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.latitude));
                            _uavTrace.altitude = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.altitude));
                            _uavTrace.height = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.height));
                            _uavTrace.roll = IsNullOrEmpty(uavResultJson?.data?.roll);
                            _uavTrace.pitch = IsNullOrEmpty(uavResultJson?.data?.pitch);
                            _uavTrace.yaw = IsNullOrEmpty(uavResultJson?.data?.yaw);
                            _uavTrace.speed = Convert.ToDouble(IsNullOrEmpty((string)uavResultJson?.data?.speed));
                            _uavTrace.rtmp_url = IsNullOrEmpty(uavResultJson?.data?.rtmp_url);
                            _uavTrace.voltage = IsNullOrEmpty(uavResultJson?.data?.voltage);
                            _uavTrace.climb_rate = IsNullOrEmpty(uavResultJson?.data?.climb_rate);
                            _uavTrace.distance_to_home = IsNullOrEmpty(uavResultJson?.data?.distance_to_home);
                            _uavTrace.flight_time = IsNullOrEmpty(uavResultJson?.data?.flight_time);
                            _uavTrace.current = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.current));
                            _uavTrace.battary_remain = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.battary_remain));
                            _uavTrace.ground_speed = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.ground_speed));
                            _uavTrace.air_speed = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.air_speed));
                            _uavTrace.sat_count = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.sat_count));
                            _uavTrace.distance_next = Convert.ToDouble(IsNullOrEmpty(uavResultJson?.data?.distance_next));
                            _uavTrace.flight_mode = IsNullOrEmpty(uavResultJson?.data?.flight_mode);
                            _uavTrace.flight_sortie = IsNullOrEmpty(uavResultJson?.data?.flight_sortie);
                            //_uavTrace = JsonTool.JsonToObject<_uavTrace>(uavResultJson.data);
                        }
                        catch (Exception ex)
                        {
                            SystemLog.Log(ex);
                        }
                    }

                    hudInit();

                    if (_uavTrace == null)
                        continue;
                    //增加运动物体的位置点
                    tempPt = CreateLocalPoint(_uavTrace);
                    //与上次的坐标点相同，直接丢弃
                    if (IsSamePoint(lastPt, tempPt.Position))
                        continue;
                    //获取与上次坐标的距离
                    var distanceTo = GetDistance(tempPt.Position.X, tempPt.Position.Y, lastPt.X, lastPt.Y);
                    //Console.WriteLine("----两个点位置distanceTo：" + distanceTo);
                    //if (distanceTo >= LIMIT_DISTANCE)
                    //{
                    //    if (_isStop)
                    //        break;
                    //    else
                    //        continue;
                    //}

                    //不合理的坐标点丢弃
                    //if (!tempPt.Position.Valid() && tempPt.Position.X == 0.0)
                    //    continue;

                    timeCount += (timeSpan / 1000.0);
                    //获取姿态
                    angle = GetEulerAngle(_uavTrace);
                    _line?.AppendPoint(tempPt);
                    shell.Dispatcher.Invoke(() =>
                    {
                        if (_motionPath == null)
                            return;
                        if (_line.PointCount <= 1)
                            return;
                        this.OnTracking?.Invoke(uavTraceRealTime);
                        ////插值做平滑
                        //var spitCount = 5;
                        //var pts = Dispase(lastPt, tempPt.Position, spitCount);
                        //foreach (var item in pts)
                        //{
                        //    tempPt = item.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs);
                        //    _multiPoint.AddPoint(tempPt);
                        //    _motionPath.AddWaypoint(tempPt.Position, angle, scale, timeCount / spitCount);
                        //}

                        _multiPoint.AddPoint(tempPt);
                        _motionPath.AddWaypoint(tempPt.Position, angle, scale, timeCount);

                        if (_motionPath.WaypointsNumber == 2)
                        {
                            GviMap.Camera.FlyTime = 8.0;//恢复默认
                            _motionPath.Index = 0;
                            _motionPath.Play();
                            //设置相机跟随运动物体
                            FollowToUav(_isFollow);
                        }
                        else if (_motionPath.WaypointsNumber > 2)
                        {
                            _motionPath.Index = _motionPath.WaypointsNumber - 1;
                            _motionPath.Play();
                        }
                        //获取姿态
                        // GviMap.Camera.FlyToObject(_skinMeshPlane.Guid, gviActionCode.gviActionFollowBehindAndAbove);//后上方
                        // GviMap.Camera.FlyToObject(_skinMeshPlane.Guid, gviActionCode.gviActionFollowAbove);//上方跟随
                        // GviMap.Camera.FlyToObject(_skinMeshPlane.Guid, gviActionCode.gviActionFollowBehindAndAbove);//上方跟随
                        //增加位置点到运动轨迹
                        if (_line.PointCount > 1)
                        {
                            if (_rLine == null)
                                _rLine = GviMap.ObjectManager.CreateRenderPolyline(_line, _curveSymbol);
                            _rLine.SetFdeGeometry(_line);
                        }
                        if (_rpoint != null)
                            _rpoint.SetFdeGeometry(_multiPoint);

                        this.OnFrame();
                    });
                    if (_isStop)
                        break;
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }


        /// <summary>
        /// 离散平滑
        /// </summary>
        /// <param name="vStart"></param>
        /// <param name="vEnd"></param>
        /// <param name="spitCount">离散线采样（默认5个）</param>
        /// <returns></returns>
        public List<IVector3> Dispase(IVector3 vStart, IVector3 vEnd, int spitCount = 5)
        {
            List<IVector3> pts = new List<IVector3>();
            var vector = vEnd.Subtract(vStart);
            vector.Normalize();//单一化

            var length = vStart.GetDistance(vEnd);
            pts.Add(vStart);
            for (int j = 0; j < spitCount; j++)
            {
                var temVect = vStart.Add(vector.Multiply(length * (j + 1)));
                pts.Add(temVect);
            }

            return pts;
        }

        private void OnFrame()
        {
            if (this._dynamicTableLabel != null && this._skinMesh != null)
            {
                _dynamicTableLabel.SetRecord(0, 1, String.Format("{0:F8}", this._skinMesh.ModelPoint.Position.X));
                _dynamicTableLabel.SetRecord(1, 1, String.Format("{0:F8}", this._skinMesh.ModelPoint.Position.Y));
                _dynamicTableLabel.SetRecord(2, 1, String.Format("{0:F2}", this._skinMesh.ModelPoint.Position.Z));
            }
        }

        private void LoadTableLable(int catId = 1)
        {
            _dynamicTableLabel = GviMap.AxMapControl.ObjectManager.CreateTableLabelWith2Col(3, 50, 95);

            switch (catId)
            {
                case 1:
                    _dynamicTableLabel.TitleText = Helpers.ResourceHelper.FindKey("Currentposition").ToString();
                    break;
                case 20:
                    _dynamicTableLabel.TitleText = Helpers.ResourceHelper.FindKey("CurrentpositionForShip").ToString();
                    break;
                default:
                    _dynamicTableLabel.TitleText = Helpers.ResourceHelper.FindKey("Currentposition").ToString();
                    break;
            }
            _dynamicTableLabel.SetRecord(0, 0, Helpers.ResourceHelper.FindKey("Longitude"));
            _dynamicTableLabel.SetRecord(0, 1, String.Format("{0:F8}", _line.GetPoint(0).X));
            _dynamicTableLabel.SetRecord(1, 0, Helpers.ResourceHelper.FindKey("Latitude"));
            _dynamicTableLabel.SetRecord(1, 1, String.Format("{0:F8}", _line.GetPoint(0).Y));
            _dynamicTableLabel.SetRecord(2, 0, Helpers.ResourceHelper.FindKey("Height"));
            _dynamicTableLabel.SetRecord(2, 1, String.Format("{0:F2}", _line.GetPoint(0).Z));
            _dynamicTableLabel.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
            _dynamicTableLabel.Position = _line.GetPoint(0);
            _motionableTableLabel = _dynamicTableLabel as IMotionable;
            _motionableTableLabel.Bind(_motionPath, new Vector3() { X = 0, Y = 0, Z = 1 }, 0, 0, 0);
        }
        private void TestUavLine()
        {
            var uavTrace = new UavTrace();
            uavTrace.longitude = 120.95337;
            uavTrace.latitude = 30.88866;
            //uavTrace.height = 30;
            uavTrace.altitude = 30;
            GviMap.Camera.FlyMode = gviFlyMode.gviFlyArc;
            GviMap.Camera.SetCamera(uavTrace.longitude, uavTrace.latitude, 35, 180, 0, 0, GviMap.SpatialCrs, gviSetCameraFlags.gviSetCameraNoFlags);

            IEulerAngle angular1 = null;
            IVector3 v3 = null;
            GviMap.Camera.GetCamera(out v3, out angular1);

            var angular = new EulerAngle();
            angular.Set(180, 0, 0);

            // 运动路径定位点
            var point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            //point.SpatialCRS = new CRSFactory().CreateFromWKT(wkt) as ISpatialCRS;
            point.SetPostion(uavTrace.longitude, uavTrace.latitude, uavTrace.altitude);

            var point2 = GviMap.Camera.GetAimingPoint2(point, angular, 200);
            var point3 = GviMap.Camera.GetAimingPoint2(point2, angular, 600);
            angular.Heading = -90;
            var point4 = GviMap.Camera.GetAimingPoint2(point3, angular, 100);
            // 加载运动路径
            _dynamicObject = GviMap.ObjectManager.CreateDynamicObject();
            _dynamicObject.MotionStyle = gviDynamicMotionStyle.gviDynamicMotionHover;
            _dynamicObject.AddWaypoint2(point, 10);
            _dynamicObject.AddWaypoint2(point2, 10);
            _dynamicObject.AddWaypoint2(point3, 10);
            _dynamicObject.AddWaypoint2(point4, 10);
            _dynamicObject.CrsWKT = GviMap.Wgs84;

            // 画出运动路径
            var line = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            line.AppendPoint(point);
            line.AppendPoint(point2);
            line.AppendPoint(point3);
            line.AppendPoint(point4);
            line.SpatialCRS = GviMap.SpatialCrs;
            // 构造ModelPoint

            IModelPoint mp = CreateModelPoint(line.GetPoint(0).Position);
            // 创建骨骼动画
            var _skinMesh = GviMap.ObjectManager.CreateSkinnedMesh(mp, GviMap.ProjectTree.RootID);
            if (_skinMesh == null)
            {
                // MessageBox.Show("骨骼动画创建失败！");
                return;
            }
            _skinMesh.Loop = true;
            _skinMesh.Play();
            _skinMesh.MaxVisibleDistance = 1000;
            _skinMesh.ViewingDistance = 100;
            //设置相机跟随运动物体
            GviMap.Camera.FlyToObject(_skinMesh.Guid, gviActionCode.gviActionFollowBehindAndAbove);

            // 绑定到运动路径
            IMotionable m = _skinMesh as IMotionable;
            var position = new Vector3();
            position.Set(0, 0, 0);
            m.Bind2(_dynamicObject, position, 0, 0, 0);
            _dynamicObject.Play();
            //绘制航迹
            _rLine = GviMap.ObjectManager.CreateRenderPolyline(line, new CurveSymbol());
        }


        // 无人机面板相关
        bool huddropoutresize;
        private void hudInit()
        {
            if (hudDropout == null)
            {
                hudDropout = new Form();
                hudDropout.TopMost = true;
                hudDropout.ControlBox = false;
                hudDropout.Load += HudDropout_Load;
            }

            if (hudView == null)
            {
                hudView = new HUD();//578*466+h0 466*365+h20
                hudView.Width = 578;
                hudView.Height = 466;
            }
            string imageName = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "resources\\Uav\\MMC白色@3x.png");

            // 
            // hudView
            // 
            this.hudView.airspeed = 0F;
            this.hudView.alt = Convert.ToSingle(uavTraceRealTime?.altitude);
            this.hudView.altunit = null;
            this.hudView.AOA = 0F;
            this.hudView.BackColor = System.Drawing.Color.Black;
            this.hudView.batterylevel = 0F;
            this.hudView.batteryremaining = 0F;
            this.hudView.bgimage = null;// System.Drawing.Image.FromFile(imageName);
            this.hudView.connected = false;
            //this.hudView.ContextMenuStrip = this.contextMenuStripHud;
            this.hudView.critAOA = 25F;
            this.hudView.critSSA = 30F;
            this.hudView.current = 0F;
            this.hudView.datetime = DateTime.Now; //new System.DateTime(((long)(0)));
            this.hudView.displayAOASSA = false;
            this.hudView.disttowp = 0F;
            this.hudView.distunit = null;
            this.hudView.ekfstatus = 0F;
            this.hudView.failsafe = false;
            this.hudView.gpsfix = 0F;
            this.hudView.gpsfix2 = 0F;
            this.hudView.gpshdop = 0F;
            this.hudView.gpshdop2 = 0F;
            this.hudView.groundalt = 0F;
            this.hudView.groundColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(78)))), ((int)(((byte)(1)))));
            this.hudView.groundColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(33)))), ((int)(((byte)(4)))));
            this.hudView.groundcourse = 0F;
            this.hudView.groundspeed = Convert.ToSingle(uavTraceRealTime?.speed);
            this.hudView.heading = Convert.ToSingle(uavTraceRealTime?.yaw);//0F
            this.hudView.hudcolor = System.Drawing.Color.LightGray;
            this.hudView.linkqualitygcs = 0F;
            this.hudView.lowairspeed = false;
            this.hudView.lowgroundspeed = false;
            this.hudView.lowvoltagealert = false;
            this.hudView.message = "";
            this.hudView.messagetime = new System.DateTime(((long)(0)));
            this.hudView.mode = "Unknown";
            this.hudView.Name = "hudView";
            this.hudView.navpitch = 0F;
            this.hudView.navroll = 0F;
            this.hudView.pitch = Convert.ToSingle(uavTraceRealTime?.pitch);
            this.hudView.roll = Convert.ToSingle(uavTraceRealTime?.roll);//0F
            this.hudView.Russian = false;
            this.hudView.skyColor1 = System.Drawing.Color.Blue;
            this.hudView.skyColor2 = System.Drawing.Color.LightBlue;
            this.hudView.speedunit = null;
            this.hudView.SSA = 0F;
            this.hudView.status = false;
            this.hudView.streamjpg = null;
            this.hudView.targetalt = 0F;
            this.hudView.targetheading = 0F;
            this.hudView.targetspeed = 0F;
            this.hudView.turnrate = 0F;
            this.hudView.verticalspeed = 0F;
            this.hudView.vibex = 0F;
            this.hudView.vibey = 0F;
            this.hudView.vibez = 0F;
            this.hudView.VSync = false;
            this.hudView.wpno = 0;
            this.hudView.xtrack_error = 0F;
            //this.hudView.ekfclick += new System.EventHandler(this.hudView_ekfclick);
            //this.hudView.vibeclick += new System.EventHandler(this.hudView_vibeclick);
            //this.hudView.DoubleClick += new System.EventHandler(this.hudView_DoubleClick);
            //this.hudView.Resize += new System.EventHandler(this.hudView_Resize);                      
        }

        private void HudDropout_Load(object sender, EventArgs e)
        {
            this.hudDropout.Text = this.ground_serial + " 无人机仪表";
			hudView.Refresh();
            //Console.WriteLine("HudDropout_Load " + this.hudDropout.Width + " " + this.hudDropout.Height);
        }

        private void hudShow()
        {
            hudDropout.Size = new System.Drawing.Size(hudView.Width, hudView.Height);
            hudDropout.Controls.Add(hudView);
            hudDropout.Resize += dropout_Resize;
            hudDropout.Show();
        }

        void dropout_Resize(object sender, EventArgs e)
        {
            if (huddropoutresize)
                return;

            huddropoutresize = true;

            int hudh = hudView.Height;
            int formh = ((Form)sender).Height - 30;

            if (((Form)sender).Height < hudh)
            {
                if (((Form)sender).WindowState == FormWindowState.Maximized)
                {
                    System.Drawing.Point tl = ((Form)sender).DesktopLocation;
                    ((Form)sender).WindowState = FormWindowState.Normal;
                    ((Form)sender).Location = tl;
                }
                ((Form)sender).Width = (int)(formh * (hudView.SixteenXNine ? 1.777f : 1.333f));
                ((Form)sender).Height = formh + 20;
            }
            //hudDropout.Size = new System.Drawing.Size(((Form)sender).Width, ((Form)sender).Height + 20);

            //Console.WriteLine("dropout_Resize " + ((Form)sender).Height + "  " + ((Form)sender).Width);
            hudView.Width = ((Form)sender).Width;
            hudView.Height = ((Form)sender).Height;
            hudView.doResize();

            hudView.Refresh();
            huddropoutresize = false;
        }

        private void hudUncheck()
        {
            hudDropout.Hide();
        }

        private void hudRelease()
        {
            hudDropout?.Close();
            hudDropout = null;
        }

    }
}