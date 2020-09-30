using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;

namespace Mmc.Framework.Draw
{
    public class DrawSegmentsTool
    {
        public delegate void FinishDrawEventHandler(object sender, List<IPolyline> segmentList);

        public delegate void ResetBeginPointEventHandler(object sender, List<IPolyline> segmentList);

        /// <summary>
        ///     画同一起点的多条射线
        ///     操作：
        ///     鼠标左键，采集点；
        ///     鼠标右键，停止采集并且调用注册完成事件；
        ///     以鼠标左键点击的第一个点为所有射线的起点
        ///     author:liyang
        /// </summary>
        public class DrawMutiSegmentTool
        {
            #region 初始化

            /// <summary>
            ///     初始化方法
            ///     renderControl为null时，认为是系统错误，记录日志
            /// </summary>
            /// <param name="renderControl">Gvitech.CityMaker.Controls.AxRenderControl</param>
            /// <param name="beginOffsetZ">起始点的高度偏移</param>
            public DrawMutiSegmentTool(AxRenderControl renderControl, double beginOffsetZ)
            {
                try
                {
                    _beginOffsetZ = beginOffsetZ;
                    if (renderControl == null)
                    {
                        SystemLog.Log("DrawMutiSegment:创建绘制同一起点的多条射线的类，发生错误：renderControl为null");
                        return;
                    }
                    _renderControl = renderControl;
                    _beginPointSymbol = new SimplePointSymbol();
                    _beginPointSymbol.FillColor = ColorConvert.UintToColor(0xffffff00);
                    _beginPointSymbol.Alignment=gviPivotAlignment.gviPivotAlignCenterCenter;
                    _beginPointSymbol.Size = 10;
                    _otherPointSymbol = new SimplePointSymbol();
                    _otherPointSymbol.Size = 10;
                    _otherPointSymbol.Alignment=gviPivotAlignment.gviPivotAlignCenterCenter;
                    _groundLineSymbol = new CurveSymbol();
                    _groundLineSymbol.Color = ColorConvert.UintToColor(0xffffff00);
                    _lookLineSymbol = new CurveSymbol();
                    _lookLineSymbol.Color = ColorConvert.UintToColor(0xffffff00);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }

            #endregion

            #region 对外接口

            #region 绘制控制，开始和释放

            /// <summary>
            ///     开始绘制
            /// </summary>
            public void Start()
            {
                if (_renderControl == null)
                {
                    return;
                }
                if (_isStarted)
                {
                    return;
                }
                try
                {
                    _isStarted = true;
                    //设置拾取和鼠标状态
                    _renderControl.InteractMode = gviInteractMode.gviInteractSelect;
                    //this._renderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                    _renderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer |
                                                           gviMouseSelectObjectMask.gviSelectReferencePlane
                                                           | gviMouseSelectObjectMask.gviSelectRenderGeometry |
                                                           gviMouseSelectObjectMask.gviSelectTerrain |
                                                           gviMouseSelectObjectMask.gviSelectTerrainHole;
                    _renderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove |
                                                     gviMouseSelectMode.gviMouseSelectClick;


                    //注册事件
                    _renderControl.RcLButtonDown -= ocx_RcLButtonDown;
                    _renderControl.RcLButtonUp -= ocx_RcLButtonUp;

                    _renderControl.RcMouseClickSelect -= AxRenderControl_RcMouseClickSelect;
                    _renderControl.RcRButtonUp -= AxRenderControl_RcRButtonUp;


                    _renderControl.RcLButtonDown += ocx_RcLButtonDown;
                    _renderControl.RcLButtonUp += ocx_RcLButtonUp;

                    _renderControl.RcMouseClickSelect += AxRenderControl_RcMouseClickSelect;
                    _renderControl.RcRButtonUp += AxRenderControl_RcRButtonUp;


                    //创建工厂
                    if (_geoFactory == null)
                    {
                        _geoFactory = new GeometryFactory();
                    }
                    Release();


                    //开始后，创建鼠标跟随label
                    var point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                    point.X = 0.1;
                    point.Y = 0.1;
                    point.Z = 0.1;
                    _startRenderPoint = AddRenderPoint(point, _beginPointSymbol);
                    _startLabel = CreateRenderTool.CreateTextLabel(point, "观察点");
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }

            /// <summary>
            ///     清楚一次或多次操作的渲染对象
            /// </summary>
            public void Release()
            {
                if (_renderControl == null)
                {
                    return;
                }
                //正在绘制时，不能清除
                if (_isStarted)
                {
                    return;
                }
                try
                {
                    ///删除并释放所有数据对象
                    GviMap.ObjectManager.ReleaseRenderObject(_startRenderPoint, _startLabel, _groundRenderPolyline,
                        _currentRenderPoint, _currentRenderPolyline, _currentLabel);
                    _groundRenderPolyline = null;
                    if (_startPoint != null)
                    {
                        _startPoint.ReleaseComObject();
                        _startPoint = null;
                    }
                    if (_groundPolyline != null)
                    {
                        _groundPolyline.ReleaseComObject();
                        _groundPolyline = null;
                    }
                    if (_segmentList.Count > 0)
                    {
                        foreach (var line in _segmentList)
                        {
                            if (line != null)
                            {
                                line.ReleaseComObject();
                            }
                        }
                        _segmentList.Clear();
                    }
                    if (_renderObjectPolylineList.Count > 0)
                    {
                        foreach (var obj in _renderObjectPolylineList)
                        {
                            if (obj != null)
                            {
                                GviMap.ObjectManager.DeleteObject(obj.Guid);
                            }
                        }
                        _renderObjectPolylineList.Clear();
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }

            /// <summary>
            ///     停止绘制
            ///     并且清除绘制的数据对象
            /// </summary>
            public void Stop()
            {
                if (_renderControl == null)
                {
                    return;
                }
                if (!_isStarted)
                {
                    return;
                }
                _isStarted = false;
                //取消设置拾取和鼠标状态
                _renderControl.InteractMode = gviInteractMode.gviInteractNormal;
                _renderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
                _renderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                //取消注册事件
                _renderControl.RcMouseClickSelect -= AxRenderControl_RcMouseClickSelect;
                _renderControl.RcRButtonUp -= AxRenderControl_RcRButtonUp;
            }

            /// <summary>
            ///     修改起始坐标的Z轴偏移
            /// </summary>
            /// <param name="offset">偏移量</param>
            /// <param name="reDrawRenderLine">是否绘制连接线，目标点，和目标点Label</param>
            public void ResetOffsetZ(double offset, bool reDrawRenderLine)
            {
                try
                {
                    _beginOffsetZ = offset;
                    if (offset < 0)
                    {
                        return;
                    }
                    if (_isStarted) //如果已经开始，则只修改偏移量
                    {
                        return;
                    }
                    if (_renderControl == null)
                    {
                        return;
                    }
                    if (_segmentList.Count <= 0)
                    {
                        return;
                    }
                    if (_startPoint == null)
                    {
                        return;
                    }
                    if (_renderObjectPolylineList.Count > 0)
                    {
                        foreach (var obj in _renderObjectPolylineList)
                        {
                            if (obj != null)
                            {
                                GviMap.ObjectManager.DeleteObject(obj.Guid);
                            }
                        }
                        _renderObjectPolylineList.Clear();
                    }
                    /////////////////////////////开始重绘
                    _startPoint.Z -= _beginOffsetZ;
                    _beginOffsetZ = offset;
                    _startPoint.Z += _beginOffsetZ;
                    //重新绘制观察点
                    _startRenderPoint.SetFdeGeometry(_startPoint);

                    var gpnt = GviMap.GeoFactory.CreatePoint(_startPoint.X, _startPoint.Y, _startPoint.Z);
                    _startLabel.Position = gpnt;

                    //绘制与地面的连接线
                    var groundPoint = _startPoint.Clone() as IPoint;
                    groundPoint.Z -= _beginOffsetZ;
                    DrawGroundLine(groundPoint, _startPoint);

                    foreach (var polyline in _segmentList)
                    {
                        polyline.UpdatePoint(0, _startPoint);
                    }
                    if (reDrawRenderLine) //根据标示，重新绘制目标点和射线的渲染对象
                    {
                        for (var i = 0; i < _segmentList.Count; i++)
                        {
                            var polylin = _segmentList[i];
                            //绘制目标点
                            _renderObjectPolylineList.Add(AddRenderPoint(polylin.EndPoint, _otherPointSymbol));
                            //绘制label
                            _renderObjectPolylineList.Add(AddLabel(polylin.EndPoint,
                                "目标点" + (i + 1)));

                            _renderObjectPolylineList.Add(AddRenderPolyline(polylin, _lookLineSymbol));
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                //触发事件
                try
                {
                    if (OnResetBeginPoint != null)
                    {
                        OnResetBeginPoint(this, _segmentList);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }

            /// <summary>
            ///     删除绘制渲染线
            /// </summary>
            public void DeleteRenderPolyline()
            {
                try
                {
                    if (_renderObjectPolylineList.Count > 0)
                    {
                        foreach (var obj in _renderObjectPolylineList)
                        {
                            if (obj != null)
                            {
                                GviMap.ObjectManager.DeleteObject(obj.Guid);
                            }
                        }
                        _renderObjectPolylineList.Clear();
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }

            #endregion

            #region 绘制结果

            /// <summary>
            ///     把绘制的多射线，以GvitechMultiPolyline形式返回
            ///     gviVertexAttribute.VertexAttributeNone，没有z值
            /// </summary>
            /// <returns>gviVertexAttribute.VertexAttributeNone</returns>
            public IMultiPolyline GetSegmentASMutiPolyline()
            {
                //没有创建工厂，证明还没有开始绘制射线
                if (_geoFactory == null)
                {
                    return null;
                }
                try
                {
                    //创建没有z值的multiPolyline
                    var mutiPolyline =
                        GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline,
                            gviVertexAttribute.gviVertexAttributeNone) as IMultiPolyline;
                    foreach (var line in _segmentList)
                    {
                        mutiPolyline.AddPolyline(line.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline);
                    }
                    return mutiPolyline;
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                    return null;
                }
            }

            /// <summary>
            ///     绘制射线转目标点
            /// </summary>
            /// <returns></returns>
            public List<IPoint> GetTargetPoints()
            {
                if (_segmentList == null || _segmentList.Count <= 0)
                {
                    return null;
                }
                var targetPoints = new List<IPoint>();
                foreach (var polyline in _segmentList)
                {
                    targetPoints.Add(polyline.EndPoint);
                }
                return targetPoints;
            }

            #endregion

            #region 根据结果直接绘制

            public bool DrawSegment(IPoint startPoint, List<IPoint> targetPoints, double offsetZ)
            {
                Release();

                if (startPoint == null)
                {
                    return false;
                }
                if (targetPoints == null || targetPoints.Count <= 0)
                {
                    return false;
                }

                _beginOffsetZ = offsetZ;
                _startPoint = startPoint.Clone() as IPoint;
                _startPoint.Z += offsetZ;
                //绘制接地线
                DrawGroundLine(startPoint, _startPoint);

                _startRenderPoint = AddRenderPoint(_startPoint, _beginPointSymbol);
                _startLabel = AddLabel(_startPoint, "观察点");
                //绘制分析线段
                foreach (var endPoint in targetPoints)
                {
                    var currentPolyLine =
                        GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                            gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
                    currentPolyLine.StartPoint = _startPoint;
                    currentPolyLine.EndPoint = endPoint;
                    //需要克隆，不然当修改点时，把polyline的端点也改变了
                    _segmentList.Add(currentPolyLine);

                    var currentRenderPolyline = AddRenderPolyline(currentPolyLine, _lookLineSymbol);
                    _renderObjectPolylineList.Add(currentRenderPolyline);

                    var currentLabel = AddLabel(endPoint,
                       "目标点" + (_segmentList.Count + 1));
                    _renderObjectPolylineList.Add(currentLabel);

                    var currentRenderPoint = AddRenderPoint(endPoint, _otherPointSymbol);
                    _renderObjectPolylineList.Add(currentRenderPoint);
                }

                return true;
            }

            #endregion

            #endregion

            #region 绘制鼠标事件

            private bool ocx_RcLButtonUp(uint Flags, int X, int Y)
            {
                return false;
            }

            private double tempX, tempY;

            private bool ocx_RcLButtonDown(uint Flags, int X, int Y)
            {
                tempX = X;
                tempY =Y;
                return false;
            }

            private void AxRenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
            {
                if (IntersectPoint == null)
                {
                    return;
                }
                if (_geoFactory == null)
                {
                    return;
                }

                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    var point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                    point.X = IntersectPoint.X;
                    point.Y = IntersectPoint.Y;
                    point.Z = IntersectPoint.Z;
                    if (_startPoint == null) //拾取第一个点
                    {
                        //修改观察点位置
                        point.Z += _beginOffsetZ;
                        _startPoint = point;
                        _startRenderPoint.SetFdeGeometry(_startPoint);

                        var gpnt = GviMap.GeoFactory.CreatePoint(_startPoint.X, _startPoint.Y, _startPoint.Z);
                        _startLabel.Position = gpnt;

                        //绘制地面到点的连接线
                        var offsetPoint = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                        offsetPoint.X = point.X;
                        offsetPoint.Y = point.Y;
                        offsetPoint.Z = point.Z - _beginOffsetZ;
                        DrawGroundLine(_startPoint, offsetPoint);

                        //创建鼠标移动时的临时射线，不加入到渲染集合，等鼠标右键结束绘制时清楚
                        _currentRenderPoint = AddRenderPoint(_startPoint, _otherPointSymbol);
                        _currentLabel = AddLabel(_startPoint,
                            string.Format("{0}{1}:{2:f1}{3}", "目标点",
                                (_segmentList.Count + 1), point.Z, "米"));
                        _currentPolyLine =
                            GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                                gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
                        _currentPolyLine.StartPoint = _startPoint;
                        var endPoint = _startPoint.Clone() as IPoint;
                        endPoint.X += 0.1;
                        endPoint.Y += 0.1;
                        endPoint.Z += 0.1;
                        _currentPolyLine.EndPoint = endPoint;
                        _currentRenderPolyline = AddRenderPolyline(_currentPolyLine, _lookLineSymbol);
                    }
                    else //拾取第二个点
                    {
                        //点击左键，创建当前点击位置的射线
                        _currentPolyLine =
                            GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                                gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
                        _currentPolyLine.StartPoint = _startPoint;
                        _currentPolyLine.EndPoint = point;
                        //需要克隆，不然当修改点时，把polyline的端点也改变了
                        _segmentList.Add(_currentPolyLine.Clone() as IPolyline);
                        //将当前的射线加入到渲染集合
                        _renderObjectPolylineList.Add(_currentRenderPolyline);
                        _renderObjectPolylineList.Add(_currentLabel);
                        _renderObjectPolylineList.Add(_currentRenderPoint);

                        //创建鼠标移动时的临时射线，不加入到渲染集合，等鼠标右键结束绘制时清楚         
                        _currentRenderPoint = AddRenderPoint(point, _otherPointSymbol);
                        _currentRenderPolyline = AddRenderPolyline(_currentPolyLine, _lookLineSymbol);

                        _currentLabel = AddLabel(point,
                            string.Format("{0}{1}:{2:f1}{3}", "目标点",
                                (_segmentList.Count + 1), point.Z, "米"));
                    }
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    try
                    {
                        var point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                        if (_startPoint == null) //修改观察点位置为鼠标跟随
                        {
                            point.X = IntersectPoint.X;
                            point.Y = IntersectPoint.Y;
                            point.Z = IntersectPoint.Z;
                            _startRenderPoint.SetFdeGeometry(point);

                            var gpnt = GviMap.GeoFactory.CreatePoint(point.X, point.Y, point.Z);
                            _startLabel.Position = gpnt;
                        }
                        if (_currentPolyLine != null && _currentRenderPolyline != null) //修改目标点位置为鼠标跟随
                        {
                            point.X = IntersectPoint.X;
                            point.Y = IntersectPoint.Y;
                            point.Z = IntersectPoint.Z;
                            var endPoint = _currentPolyLine.EndPoint;
                            _currentPolyLine.EndPoint = point;
                            if (endPoint != null)
                            {
                                endPoint.ReleaseComObject();
                                endPoint = null;
                            }
                            _currentRenderPolyline.SetFdeGeometry(_currentPolyLine);
                            _currentRenderPoint.SetFdeGeometry(point);

                            IVector3 point1 = new Vector3();
                            point1.X = point.X;
                            point1.Y = point.Y;
                            point1.Z = point.Z;

                            var gpnt = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                            gpnt.Position = point1;
                            var lblName = _currentLabel.Text;
                            var newName = string.Empty;
                            if (!string.IsNullOrEmpty(lblName))
                            {
                                var idx = lblName.IndexOf(":");
                                if (idx > -1)
                                {
                                    newName = lblName.Substring(0, idx);
                                    newName = string.Format("{0}:{1:f1}{2}", newName, gpnt.Z,
                                        "米");
                                }
                                _currentLabel.Text = newName;
                            }
                            _currentLabel.Position = gpnt;
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLog.Log(ex);
                    }
                }
            }

            private bool AxRenderControl_RcRButtonUp(uint Flags, int X, int Y)
            {
                try
                {
                    //删除拖拽的橡皮线，由于拖拽的橡皮线没有加入到渲染集合，所以需要右键结束时清楚
                    if (_currentPolyLine != null)
                    {
                        _currentPolyLine.ReleaseComObject();
                        _currentPolyLine = null;
                    }
                    GviMap.ObjectManager.ReleaseRenderObject(_currentRenderPoint, _currentRenderPolyline, _currentLabel);
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                try
                {
                    if (OnFinishDraw != null)
                    {
                        OnFinishDraw(this, _segmentList);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
                Stop();
                return false;
            }

            #endregion

            #region 内部方法

            /// <summary>
            ///     绘制地面到点的连接线
            /// </summary>
            /// <param name="startPoint"></param>
            /// <param name="endPoint"></param>
            private void DrawGroundLine(IPoint startPoint, IPoint endPoint)
            {
                if (_groundPolyline == null)
                {
                    _groundPolyline =
                        GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                            gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
                }
                _groundPolyline.StartPoint = startPoint;
                _groundPolyline.EndPoint = endPoint;
                if (_groundRenderPolyline == null)
                {
                    _groundRenderPolyline = AddRenderPolyline(_groundPolyline, _groundLineSymbol);
                }
                else
                {
                    _groundRenderPolyline.SetFdeGeometry(_groundPolyline);
                    _groundRenderPolyline.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    _groundRenderPolyline.MouseSelectMask = 0;
                }
            }

            /// <summary>
            ///     添加Lable
            /// </summary>
            /// <param name="point"></param>
            /// <param name="labelString"></param>
            /// <returns></returns>
            private ILabel AddLabel(IPoint point, string labelString)
            {
                var label = CreateRenderTool.DrawLabel(point.X, point.Y, point.Z + 0.1, labelString);
                label.VisibleMask = gviViewportMask.gviViewAllNormalView;
                label.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
                label.MinVisiblePixels = (float) GviMap.RoMinObserveDistance;
                label.MouseSelectMask = 0;
                return label;
            }

            /// <summary>
            ///     添加GvitechRenderPoint
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            private IRenderPoint AddRenderPoint(IPoint point, IPointSymbol pointSymbol)
            {
                pointSymbol.Alignment=gviPivotAlignment.gviPivotAlignCenterCenter;
                var RenderPoint = _renderControl.ObjectManager.CreateRenderPoint(point, pointSymbol);
                RenderPoint.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
                RenderPoint.MinVisiblePixels = (float) GviMap.RoMinObserveDistance;
                RenderPoint.MouseSelectMask = 0;
                return RenderPoint;
            }

            /// <summary>
            ///     绘制IRenderPolyline
            /// </summary>
            /// <param name="Polyline"></param>
            /// <param name="lineSymbol"></param>
            /// <returns></returns>
            private IRenderPolyline AddRenderPolyline(IPolyline Polyline, ICurveSymbol lineSymbol)
            {
                var RenderPolyline = _renderControl.ObjectManager.CreateRenderPolyline(Polyline, lineSymbol);
                RenderPolyline.MaxVisibleDistance = GviMap.RoMaxObserveDistance;
                RenderPolyline.MinVisiblePixels = (float) GviMap.RoMinObserveDistance;
                RenderPolyline.MouseSelectMask = 0;
                return RenderPolyline;
            }

            #endregion

            #region 成员属性

            /// <summary>
            ///     起始点
            /// </summary>
            public IPoint StartPoint
            {
                get { return _startPoint; }
            }

            /// <summary>
            ///     绘制线段
            /// </summary>
            public List<IPolyline> SegmentList
            {
                get { return _segmentList; }
            }

            #endregion

            #region 私有变量

            /// <summary>
            ///     RenderControl
            /// </summary>
            private readonly AxRenderControl _renderControl;

            /// <summary>
            ///     起始点
            /// </summary>
            private IPoint _startPoint;

            private IRenderPoint _startRenderPoint;
            private ILabel _startLabel;

            /// <summary>
            ///     地面到起始点线段的符号化对象
            /// </summary>
            private readonly ICurveSymbol _groundLineSymbol;

            private IPolyline _groundPolyline;
            private IRenderPolyline _groundRenderPolyline;

            /// <summary>
            ///     当前操作的射线
            ///     不用释放，最后会释放集合
            /// </summary>
            private IPolyline _currentPolyLine;

            /// <summary>
            ///     当前操作的射线
            /// </summary>
            private IRenderPolyline _currentRenderPolyline;

            /// <summary>
            ///     当前操作的点
            /// </summary>
            private IRenderPoint _currentRenderPoint;

            /// <summary>
            ///     当前操作的label对象
            /// </summary>
            private ILabel _currentLabel;

            /// <summary>
            ///     绘制的射线集合
            /// </summary>
            private readonly List<IPolyline> _segmentList = new List<IPolyline>();

            /// <summary>
            ///     需要在操作过程中显示的对象，需要释放
            /// </summary>
            private readonly List<IRObject> _renderObjectPolylineList = new List<IRObject>();

            /// <summary>
            ///     起始点的符号化对象
            /// </summary>
            private readonly ISimplePointSymbol _beginPointSymbol;

            /// <summary>
            ///     其他点的符号化对象
            /// </summary>
            private readonly IPointSymbol _otherPointSymbol;

            /// <summary>
            ///     通视射线的符号化对象
            /// </summary>
            private readonly ICurveSymbol _lookLineSymbol;

            /// <summary>
            ///     起始点的高度偏移
            /// </summary>
            private double _beginOffsetZ;

            private IGeometryFactory _geoFactory;
            private bool _isStarted;
            public FinishDrawEventHandler OnFinishDraw;
            public ResetBeginPointEventHandler OnResetBeginPoint;

            #endregion
        }
    }
}