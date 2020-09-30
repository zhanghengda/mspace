using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.Framework.Services;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _IRenderControlEvents_RcMouseHoverEventHandler =
    Gvitech.CityMaker.Controls._IRenderControlEvents_RcMouseHoverEventHandler;

namespace Mmc.Framework.Draw
{
    public delegate void OnRCDrawFinished(object sender, object result); //绘制功能完成事件委托

    //右键Up事件委托
    public delegate bool OnRCRightButtonUp();

    public delegate void OnRCKeyPress(RcKeyCode rcKeyCode);

    /// <summary>
    ///     绘制工具的常量
    /// </summary>
    public class RCConst
    {
        public const double MinValue = 0.001500000;
        public const double MaxValue = 9999999.999;
        public const double RO_MaxObserveDistance = 1000000;
        public const double RO_MinObserveDistance = 0;
    }

    public class DrawClient : Singleton<DrawClient>
    {
        public DrawClient()
        {
            DrawCustomer = null;
        }

        public IDrawCustomer DrawCustomer { get; set; }
    }

    /// <summary>
    ///     RenderControl控件的事件管理器
    /// </summary>
    public class RCDrawManager
    {
        private static volatile RCDrawManager instance;
        private static readonly object syncRoot = new object();
        //private static ThreeDControl_DrawPolygon _drawPolygon;
        private static PointPick _pointPick;
        private static PointPickMove _pointMove;
        private static PointPickKey _pointPcikKey;
        private static PointKeyDrag _pointKeyDrag;
        private static PolylineDraw _polylineDraw;
        private static PolylineEdit _polylineEdit;
        private static PolygonDraw _PolygonDraw;
        private static  RectangleDraw _RectangleDraw = null;
        private static CustomDrawCircle _CircleDraw;
        private static readonly RubberLineDraw _RubberLineDraw = null;
        private static readonly EmptyDraw _emptyDraw = null;
        private static  DrawCircle _drawCircle = null;


        /// <summary>
        ///     开始吸取材质的事件
        /// </summary>
        //public event StartPickUpTextureHandler StartPickUpTextureEvent;

        //private static pointMove_RButton _pointMove_RButton;
        private RCDrawManager()
        {
        }

        public static RCDrawManager Instance
        {
            get
            {
                if (instance == null) //Double-Checked Locking
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RCDrawManager();
                        SystemLog.Log("dd");
                        _drawCircle = new DrawCircle();
                        _pointPick = new PointPick();

                        SystemLog.Log("dd2");
                        _pointMove = new PointPickMove();
                        _pointPcikKey = new PointPickKey();
                        //_pointKeyDrag = new PointKeyDrag();
                        _polylineDraw = new PolylineDraw();
                        _polylineEdit = new PolylineEdit();
                        _PolygonDraw = new PolygonDraw();
                        _RectangleDraw = new RectangleDraw();
                        _CircleDraw = new CustomDrawCircle();
                        //_RubberLineDraw = new RubberLineDraw();
                        //_pointPickMoveHover = new PointPickMoveHover();
                        //_emptyDraw = new EmptyDraw();                    
                    }
                }
                return instance;
            }
        }

        /// <summary>
        ///     为部分功能未调用Eventmanage实现，但需要事件互斥用的
        /// </summary>
        public EmptyDraw EmptyDraw
        {
            get { return _emptyDraw; }
        }

        /// <summary>
        ///     鼠标拾取、移动和悬停工具
        /// </summary>
        public static PointPickMoveHover PointPickMoveHover { get; set; }

        /// <summary>
        ///     点拾取工具
        /// </summary>
        public PointPick PointPick
        {
            get { return _pointPick; }
        }

        public PointPickMove PointMove
        {
            get { return _pointMove; }
        }

        public PointPickKey PointKey
        {
            get { return _pointPcikKey; }
        }

        public PointKeyDrag PointKeyDrag
        {
            get { return _pointKeyDrag; }
        }

        public PolylineDraw PolylineDraw
        {
            get { return _polylineDraw; }
        }

        public PolylineEdit PolylineEdit
        {
            get { return _polylineEdit; }
        }

        public PolygonDraw PolygonDraw
        {
            get { return _PolygonDraw; }
        }

        public RectangleDraw RectangleDraw
        {
            get { return _RectangleDraw; }
        }

        /// <summary>
        ///     自定义绘制圆的工具
        /// </summary>
        public CustomDrawCircle CustomDrawCircle
        {
            get { return _CircleDraw; }
        }

        /// <summary>
        ///     绘制圆的工具
        /// </summary>
        public DrawCircle DrawCircle
        {
            get { return _drawCircle; }
        }

        public RubberLineDraw RubberLineDraw
        {
            get { return _RubberLineDraw; }
        }

        /// <summary>
        ///     特定用例，取消“选择”按钮
        ///     暂时放在这
        /// </summary>
        public void CancelChooseButton()
        {
            //if (DrawClient.Instance.DrawCustomer != null && DrawClient.Instance.DrawCustomer.BeChooseFacility())
            //{
            //    MainFrmService.BarPerformClick("ChooseFacility");

            //    DrawClient.Instance.DrawCustomer = null;
            //}
        }
    }

    /// <summary>
    ///     为部分功能未调用Eventmanage实现，但需要互斥用的
    /// </summary>
    public class EmptyDraw : Draw
    {
        public void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer)
        {
            base.Register(ctl3d, eCustomer, RCMouseOperType.UnKnow);
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
        }
    }

    /// <summary>
    ///     橡皮线绘制
    /// </summary>
    public class RubberLineDraw : Draw
    {
        public RubberLineDraw()
        {
            Init();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
            if (_renderPolyline != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolyline.Guid);
            }
            _renderPolyline = null;
        }

        private void Start()
        {
            Ocx.InteractMode = gviInteractMode.gviInteractSelect;
            Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;

            if (_renderPolyline != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolyline.Guid);
            }
            _renderPolyline = null;

            Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
        }

        public void Restart()
        {
            End();
            Start();
        }

        private void End()
        {
            _isStarted = false;
            if (_geoPolyline != null)
            {
                _geoPolyline.ReleaseComObject();
                _geoPolyline = null;
            }
            _geoPolyline = (IPolyline)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
        }

        private void ocx_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (_geoPolyline == null)
                    return;

                if (IntersectPoint == null)
                    return;
                IPoint p = null, p1 = null;
                //开始，确定起点
                p = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                p.X = IntersectPoint.X;
                p.Y = IntersectPoint.Y;
                p.Z = IntersectPoint.Z;


                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    if (!_isStarted)
                    {
                        #region 第一点，起点

                        _vStart = new Vector3();
                        _vStart.X = p.X;
                        _vStart.Y = p.Y;
                        _vStart.Z = p.Z;

                        //第一次，添加两个顶点                  
                        _geoPolyline.AppendPoint(p);

                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第2点
                        p1.X = _vStart.X + RCConst.MinValue;
                        p1.Y = _vStart.Y - RCConst.MinValue;
                        p1.Z = _vStart.Z;
                        _geoPolyline.AppendPoint(p1);

                        //渲染Polygon
                        _renderPolyline = Ocx.ObjectManager.CreateRenderPolyline(_geoPolyline,
                            _curveSymbol);
                        _isStarted = true;

                        #endregion
                    }
                    else //结束
                    {
                        #region 第二点，终点

                        _vEnd = new Vector3();
                        _vEnd.X = p.X;
                        _vEnd.Y = p.Y;
                        _vEnd.Z = p.Z;

                        var radius = Math.Sqrt((_vEnd.X - _vStart.X) * (_vEnd.X - _vStart.X)
                                               + (_vEnd.Y - _vStart.Y) * (_vEnd.Y - _vStart.Y));
                        if (radius < RCConst.MinValue) //半径不能为0
                            return;

                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第2点
                        p1.X = _vEnd.X;
                        p1.Y = _vEnd.Y;
                        p1.Z = _vEnd.Z;
                        _geoPolyline.UpdatePoint(1, p1);

                        //更新几何信息                       
                        _renderPolyline.SetFdeGeometry(_geoPolyline);
                        End();
                        Finish(_renderPolyline);
                        //结束绘制

                        #endregion
                    }
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    #region 鼠标拖动

                    if (_isStarted == false)
                        return;

                    //对线长度小于最小值
                    if (Math.Sqrt((_vStart.X - p.X) * (_vStart.X - p.X)
                                  + (_vStart.Y - p.Y) * (_vStart.Y - p.Y)) < RCConst.MinValue)
                        return;

                    //修正顶点
                    p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第三点
                    p1.X = p.X;
                    p1.Y = p.Y;
                    p1.Z = p.Z;
                    _geoPolyline.UpdatePoint(1, p1);

                    //更新几何信息
                    _renderPolyline.SetFdeGeometry(_geoPolyline);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolyline = (IPolyline)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            _curveSymbol = new CurveSymbol();
            _curveSymbol.Color = ColorConvert.UintToColor(0xffffff00);
        }

        #region 成员变量

        private IObjectEditor _geoEditor = null;
        private IRenderPolyline _renderPolyline;
        private IPolyline _geoPolyline;
        private ICurveSymbol _curveSymbol;
        private IGeometryFactory _gfactory;
        private IVector3 _vStart;
        private IVector3 _vEnd;
        private bool _isStarted;

        #endregion
    }

    /// <summary>
    ///     调用平台接口实现绘制多边形接口，返回绘制的Polyline
    /// </summary>
    public class PolylineDraw : Draw
    {
        public PolylineDraw()
        {
            Init();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
        }

        private void Start()
        {
            _geoEditor = Ocx.ObjectEditor;
            Ocx.RcObjectEditFinish += GeometryEditor_EditFinish;
            StartDraw();
        }

        public void Restart()
        {
            _geoEditor.FinishEdit();
            StartDraw();
        }

        private void StartDraw()
        {
            try
            {
                Ocx.InteractMode = gviInteractMode.gviInteractEdit;
                Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                if (_renderPolyline != null)
                {
                    Ocx.ObjectManager.DeleteObject(_renderPolyline.Guid);
                }
                _renderPolyline = null;
                _renderPolyline = Ocx.ObjectManager.CreateRenderPolyline(_geoPolyline, _curveSymbol);
                //GviMap.TempRObjectPool.Add(Guid.NewGuid().ToString(),
                //    _renderPolyline);
                var startBeOk = _geoEditor.StartEditRenderGeometry(_renderPolyline, gviGeoEditType.gviGeoEditCreator);
                if (!startBeOk)
                    SystemLog.Log("StartEditRenderGeometry失败");
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void End()
        {
            if (_geoEditor != null)
            {
                _geoEditor.FinishEdit();
                Ocx.RcObjectEditFinish -= GeometryEditor_EditFinish;
            }
            if (_renderPolyline != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolyline.Guid);
                _renderPolyline = null;
            }
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolyline = (IPolyline)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolyline.SpatialCRS = GviMap.SpatialCrs;
            _curveSymbol = new CurveSymbol();
            _curveSymbol.Color = ColorConvert.UintToColor(0xffffff00);
        }

        private void GeometryEditor_EditFinish()
        {
            if (_renderPolyline != null)
            {
                Finish(_renderPolyline);
            }
        }

        #region 成员变量

        private IObjectEditor _geoEditor;
        private IRenderPolyline _renderPolyline;
        private IPolyline _geoPolyline;
        private ICurveSymbol _curveSymbol;
        private IGeometryFactory _gfactory;

        #endregion
    }

    /// <summary>
    ///     调用平台接口实现绘制多边形接口，返回绘制的Polyline
    /// </summary>
    public class PolylineEdit : Draw
    {
        public PolylineEdit()
        {
            Init();
        }

        public void SetRenderPolyline(IRenderPolyline _Polyline)
        {
            _renderPolyline = _Polyline;
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
        }

        private void Start()
        {
            _geoEditor = Ocx.ObjectEditor;
            Ocx.RcObjectEditFinish += GeometryEditor_EditFinish;
            StartDraw();
        }

        public void Restart()
        {
            _geoEditor.CancelEdit();
            StartDraw();
        }

        private void StartDraw()
        {
            try
            {
                if (_renderPolyline == null)
                {
                    Ocx.RcObjectEditFinish -= GeometryEditor_EditFinish;
                    return;
                }
                Ocx.InteractMode = gviInteractMode.gviInteractEdit;
                Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                //GviMap.TempRObjectPool.Add(Guid.NewGuid().ToString(), _renderPolyline);
                var startBeOk = _geoEditor.StartEditRenderGeometry(_renderPolyline,  gviGeoEditType.gviGeoEditVertex);
                if (!startBeOk)
                    SystemLog.Log("StartEditRenderGeometry失败");
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void End()
        {
            if (_geoEditor != null)
            {
                _geoEditor.CancelEdit();
                Ocx.RcObjectEditFinish -= GeometryEditor_EditFinish;
            }
            if (_renderPolyline != null)
            {
                //Ocx.ObjectManager1.DeleteObject(_renderPolyline as GvitechRObject);
                _renderPolyline = null;
            }
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolyline = (IPolyline)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ);
            _curveSymbol = new CurveSymbol();
            _curveSymbol.Color = ColorConvert.UintToColor(0xffffff00);
        }

        private void GeometryEditor_EditFinish()
        {
            if (_renderPolyline != null)
            {
                Finish(_renderPolyline);
            }
        }

        #region 成员变量

        private IObjectEditor _geoEditor;
        private IRenderPolyline _renderPolyline;
        private IPolyline _geoPolyline;
        private ICurveSymbol _curveSymbol;
        private IGeometryFactory _gfactory;

        #endregion
    }


    /// <summary>
    ///     调用平台接口实现绘制多边形接口，返回绘制的Polygon
    /// </summary>
    public class PolygonDraw : Draw
    {
        public PolygonDraw()
        {
            Init();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
        }

        private void Start()
        {
            _geoEditor = Ocx.ObjectEditor;
            Ocx.RcObjectEditFinish += GeometryEditor_EditFinish;
            StartDraw();
        }

        public void Restart()
        {
            _geoEditor.FinishEdit();
            StartDraw();
        }

        public void Restart(bool flag)
        {
        }

        private void StartDraw()
        {
            try
            {
                Ocx.InteractMode = gviInteractMode.gviInteractEdit;
                Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                if (_renderPolygon != null)
                {
                    Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
                }
                _renderPolygon = null;
                _renderPolygon = Ocx.ObjectManager.CreateRenderPolygon(_geoPolygon, _surfaceSymbol);
                _renderPolygon.MaxVisibleDistance = double.MaxValue;
                _renderPolygon.MinVisiblePixels = 1;
                //GviMap.TempRObjectPool.Add(Guid.NewGuid().ToString(),
                //     _renderPolygon );
                var startBeOk = _geoEditor.StartEditRenderGeometry(_renderPolygon, gviGeoEditType.gviGeoEditCreator);
                if (!startBeOk)
                    SystemLog.Log("StartEditRenderGeometry失败");
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void End()
        {
            if (_geoEditor != null)
            {
                _geoEditor.FinishEdit();
                Ocx.RcObjectEditFinish -= GeometryEditor_EditFinish;
            }
            if (_renderPolygon != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
                _renderPolygon = null;
            }
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolygon = (IPolygon)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolygon.SpatialCRS = GviMap.SpatialCrs;
            _surfaceSymbol = new SurfaceSymbol();
            var cs = new CurveSymbol();
            cs.Color = ColorConvert.UintToColor(0xffffff00);
            _surfaceSymbol.BoundarySymbol = cs;
            _surfaceSymbol.Color = ColorConvert.UintToColor(0x000000ff);
        }

        private void GeometryEditor_EditFinish()
        {
            try
            {
                if (_renderPolygon != null)
                {
                    Finish(_renderPolygon);
                }
            }
            catch (COMException ex)
            {
                Finish(null);
            }
        }

        #region 成员变量

        private IObjectEditor _geoEditor;
        private IRenderPolygon _renderPolygon;
        private IPolygon _geoPolygon;
        private ISurfaceSymbol _surfaceSymbol;
        private IGeometryFactory _gfactory;

        #endregion
    }

    /// <summary>
    ///     矩形绘制
    /// </summary>
    public class RectangleDraw : Draw
    {
        public RectangleDraw()
        {
            Init();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
            if (_renderPolygon != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
            }
            _renderPolygon = null;
        }

        private void Start()
        {
            Ocx.InteractMode = gviInteractMode.gviInteractSelect;
            Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectReferencePlane |
                                        gviMouseSelectObjectMask.gviSelectTerrain| gviMouseSelectObjectMask.gviSelectTileLayer;

            if (_renderPolygon != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
            }
            _renderPolygon = null;

            Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
        }

        public void Restart()
        {
            End();
            Start();
        }

        private void End()
        {
            _isStarted = false;
            if (_geoPolygon != null)
            {
                _geoPolygon.ReleaseComObject();
                _geoPolygon = null;
            }
            _geoPolygon = (IPolygon)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolygon.SpatialCRS = GviMap.SpatialCrs;
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
        }

        private void ocx_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (_geoPolygon == null)
                    return;

                if (IntersectPoint == null)
                    return;
                IPoint p = null, p1 = null;
                //开始，确定起点
                p = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                p.SpatialCRS = GviMap.SpatialCrs;
                p.X = IntersectPoint.X;
                p.Y = IntersectPoint.Y;
                p.Z = IntersectPoint.Z;


                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    if (!_isStarted)
                    {
                        #region 第一点，起点

                        _vStart = new Vector3();
                        _vStart.X = p.X;
                        _vStart.Y = p.Y;
                        _vStart.Z = p.Z;

                        //第一次，添加五个顶点                  
                        _geoPolygon.ExteriorRing.AppendPoint(p); //第一个点                                              

                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第二点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vStart.X;
                        p1.Y = _vStart.Y - RCConst.MinValue;
                        p1.Z = _vStart.Z;
                        _geoPolygon.ExteriorRing.AppendPoint(p1);

                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第三点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vStart.X + RCConst.MinValue;
                        p1.Y = _vStart.Y - RCConst.MinValue;
                        p1.Z = _vStart.Z;
                        _geoPolygon.ExteriorRing.AppendPoint(p1);
                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第四点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vStart.X + RCConst.MinValue;
                        p1.Y = _vStart.Y;
                        p1.Z = _vStart.Z;
                        _geoPolygon.ExteriorRing.AppendPoint(p1);

                        _geoPolygon.ExteriorRing.AppendPoint(p); //第五个点
                        p1.SpatialCRS = GviMap.SpatialCrs;

                        //渲染Polygon
                        _renderPolygon = Ocx.ObjectManager.CreateRenderPolygon(_geoPolygon,
                            _surfaceSymbol);
                        _isStarted = true;

                        #endregion
                    }
                    else //结束
                    {
                        #region 第二点，终点

                        _vEnd = new Vector3();
                        _vEnd.X = p.X;
                        _vEnd.Y = p.Y;
                        _vEnd.Z = p.Z;

                        var radius = Math.Sqrt((_vEnd.X - _vStart.X) * (_vEnd.X - _vStart.X)
                                               + (_vEnd.Y - _vStart.Y) * (_vEnd.Y - _vStart.Y));
                        if (radius < RCConst.MinValue) //半径不能为0
                            return;

                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第二点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vStart.X;
                        p1.Y = _vEnd.Y;
                        p1.Z = _vStart.Z;
                        _geoPolygon.ExteriorRing.UpdatePoint(1, p1);
                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第三点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vEnd.X;
                        p1.Y = _vEnd.Y;
                        p1.Z = _vEnd.Z;
                        _geoPolygon.ExteriorRing.UpdatePoint(2, p1);
                        p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第四点
                        p1.SpatialCRS = GviMap.SpatialCrs;
                        p1.X = _vEnd.X;
                        p1.Y = _vStart.Y;
                        p1.Z = _vEnd.Z;
                        _geoPolygon.ExteriorRing.UpdatePoint(3, p1);

                        //更新几何信息                       
                        _renderPolygon.SetFdeGeometry(_geoPolygon);
                        End();
                        Finish(_renderPolygon);
                        //结束绘制

                        #endregion
                    }
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    #region 鼠标拖动

                    if (_isStarted == false)
                        return;

                    //对线长度小于最小值
                    if (Math.Sqrt((_vStart.X - p.X) * (_vStart.X - p.X)
                                  + (_vStart.Y - p.Y) * (_vStart.Y - p.Y)) < RCConst.MinValue)
                        return;

                    //修正顶点
                    p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第二点
                    p1.SpatialCRS = GviMap.SpatialCrs;
                    p1.X = _vStart.X;
                    p1.Y = p.Y;
                    p1.Z = _vStart.Z;
                    _geoPolygon.ExteriorRing.UpdatePoint(1, p1);
                    p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第三点
                    p1.SpatialCRS = GviMap.SpatialCrs;
                    p1.X = p.X;
                    p1.Y = p.Y;
                    p1.Z = _vStart.Z;
                    _geoPolygon.ExteriorRing.UpdatePoint(2, p1);
                    p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ); //第四点
                    p1.SpatialCRS = GviMap.SpatialCrs;
                    p1.X = p.X;
                    p1.Y = _vStart.Y;
                    p1.Z = _vStart.Z;
                    _geoPolygon.ExteriorRing.UpdatePoint(3, p1);

                    //更新几何信息
                    _renderPolygon.SetFdeGeometry(_geoPolygon);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolygon = (IPolygon)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolygon.SpatialCRS = GviMap.SpatialCrs;
            _surfaceSymbol = new SurfaceSymbol();
            var cs = new CurveSymbol();
            cs.Color = ColorConvert.UintToColor(0xffffff00);
            _surfaceSymbol.BoundarySymbol = cs;
            _surfaceSymbol.Color = ColorConvert.UintToColor(0x000000ff);
        }

        #region 成员变量

        private IObjectEditor _geoEditor = null;
        private IRenderPolygon _renderPolygon;
        private IPolygon _geoPolygon;
        private ISurfaceSymbol _surfaceSymbol;
        private IGeometryFactory _gfactory;
        private IVector3 _vStart;
        private IVector3 _vEnd;
        private bool _isStarted;

        #endregion
    }

    /// <summary>
    ///     自定义圆形绘制
    /// </summary>
    public class CustomDrawCircle : Draw
    {
        public CustomDrawCircle()
        {
            Init();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End();
            if (_renderPolygon != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
            }
            _renderPolygon = null;
        }

        private void Start()
        {
            Ocx.InteractMode = gviInteractMode.gviInteractSelect;
            Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectReferencePlane |
                                        gviMouseSelectObjectMask.gviSelectTerrain| gviMouseSelectObjectMask.gviSelectTileLayer;
            ;

            if (_renderPolygon != null)
            {
                Ocx.ObjectManager.DeleteObject(_renderPolygon.Guid);
            }
            _renderPolygon = null;

            //this.StartDraw();
            Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
        }

        public void Restart()
        {
            End();
            Start();
        }

        private void End()
        {
            _isStarted = false;
            if (_geoPolygon != null)
            {
                _geoPolygon.ReleaseComObject();
                _geoPolygon = null;
            }
            _geoPolygon = (IPolygon)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolygon.SpatialCRS = GviMap.SpatialCrs;
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
        }

        private void ocx_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (IntersectPoint == null)
                    return;
                IPoint p = null, p1 = null;
                //开始，确定起点
                p = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                p.SpatialCRS = GviMap.SpatialCrs;
                p.X = IntersectPoint.X;
                p.Y = IntersectPoint.Y;
                p.Z = IntersectPoint.Z;

                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    if (!_isStarted)
                    {
                        #region 第一个点，开始

                        _geoPolygon =
                            _gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                                gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                        _geoPolygon.SpatialCRS = GviMap.SpatialCrs;

                        _vStart = new Vector3();
                        _vStart.X = p.X;
                        _vStart.Y = p.Y;
                        _vStart.Z = p.Z;

                        //第一次创建顶点
                        for (var ii = 0; ii < _nEdge + 1; ii++)
                        {
                            if (ii < _nEdge)
                            {
                                p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                p1.SpatialCRS = GviMap.SpatialCrs;
                                p1.X = _vStart.X + _radius * Math.Sin(_arc * ii);
                                p1.Y = _vStart.Y + _radius * Math.Cos(_arc * ii);
                                p1.Z = p.Z;
                            }
                            else
                            {
                                p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                p1.SpatialCRS = GviMap.SpatialCrs;
                                p1.X = _vStart.X + _radius * Math.Sin(0.0);
                                p1.Y = _vStart.Y + _radius * Math.Cos(0.0);
                                p1.Z = p.Z;
                            }

                            _geoPolygon.ExteriorRing.AppendPoint(p1);
                        }

                        //渲染Polygon                        
                        _renderPolygon = Ocx.ObjectManager.CreateRenderPolygon(_geoPolygon,
                            _surfaceSymbol);
                        _isStarted = true;

                        #endregion
                    }
                    else //结束
                    {
                        #region 第二个点，结束

                        _vEnd = new Vector3();
                        _vEnd.X = p.X;
                        _vEnd.Y = p.Y;
                        _vEnd.Z = p.Z;

                        _radius = Math.Sqrt((_vEnd.X - _vStart.X) * (_vEnd.X - _vStart.X)
                                            + (_vEnd.Y - _vStart.Y) * (_vEnd.Y - _vStart.Y));
                        if (_radius < RCConst.MinValue) //半径不能为0
                            return;

                        for (var ii = 0; ii < _nEdge + 1; ii++)
                        {
                            if (ii < _nEdge)
                            {
                                p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                p1.SpatialCRS = GviMap.SpatialCrs;
                                p1.SpatialCRS = GviMap.SpatialCrs;
                                p1.X = _vStart.X + _radius * Math.Sin(_arc * ii);
                                p1.Y = _vStart.Y + _radius * Math.Cos(_arc * ii);
                                p1.Z = p.Z;
                            }
                            else
                            {
                                p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                p1.SpatialCRS = GviMap.SpatialCrs;
                                p1.X = _vStart.X + _radius * Math.Sin(0.0);
                                p1.Y = _vStart.Y + _radius * Math.Cos(0.0);
                                p1.Z = p.Z;
                            }

                            _geoPolygon.ExteriorRing.UpdatePoint(ii, p1);
                        }

                        //更新几何信息
                        _renderPolygon.SetFdeGeometry(_geoPolygon);

                        //结束绘制
                        End();
                        Finish(_renderPolygon);

                        #endregion
                    }
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    #region 拖动事件

                    if (_isStarted == false)
                        return;

                    _radius = Math.Sqrt((p.X - _vStart.X) * (p.X - _vStart.X)
                                        + (p.Y - _vStart.Y) * (p.Y - _vStart.Y));
                    if (_radius < RCConst.MinValue) //半径不能为0
                        return;

                    for (var ii = 0; ii < _nEdge + 1; ii++)
                    {
                        if (ii < _nEdge)
                        {
                            p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            p1.SpatialCRS = GviMap.SpatialCrs;
                            p1.X = _vStart.X + _radius * Math.Sin(_arc * ii);
                            p1.Y = _vStart.Y + _radius * Math.Cos(_arc * ii);
                            p1.Z = p.Z;
                        }
                        else
                        {
                            p1 = _gfactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            p1.SpatialCRS = GviMap.SpatialCrs;
                            p1.X = _vStart.X + _radius * Math.Sin(0.0);
                            p1.Y = _vStart.Y + _radius * Math.Cos(0.0);
                            p1.Z = p.Z;
                        }

                        _geoPolygon.ExteriorRing.UpdatePoint(ii, p1);
                    }

                    //更新几何信息
                    _renderPolygon.SetFdeGeometry(_geoPolygon);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void Init()
        {
            _gfactory = new GeometryFactory();
            _geoPolygon = (IPolygon)_gfactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ);
            _geoPolygon.SpatialCRS = GviMap.SpatialCrs;
            _surfaceSymbol = new SurfaceSymbol();
            var cs = new CurveSymbol();
            cs.Color = ColorConvert.UintToColor(0xffffff00);
            _surfaceSymbol.BoundarySymbol = cs;
            _surfaceSymbol.Color = ColorConvert.UintToColor(0x000000ff);
        }

        #region 成员变量

        private IObjectEditor _geoEditor = null;
        private IRenderPolygon _renderPolygon;
        private IPolygon _geoPolygon;
        private ISurfaceSymbol _surfaceSymbol;
        private IGeometryFactory _gfactory;
        private IVector3 _vStart;
        private IVector3 _vEnd;
        private bool _isStarted;
        private double _radius = 0.03;
        private readonly int _nEdge = 32; //圆离散的边数
        private readonly double _arc = Math.PI / 16;

        #endregion
    }


    public class DrawCircle : Draw
    {
        private ISimplePointSymbol _beginPointSymbol;
        private double _bx, _by, _bz, _ex, _ey, _ez;
        private IGeometryFactory _factory;
        private IGeometry _geometry;
        private List<IRObject> _gviTmpObjList;
        private int _mouseClickNum;
        private IPoint _point;
        private IRenderPolyline _rLine;
        private ILabel _watchLabel, _endLabel;
        private IRenderPoint _watchPoint, _endPoint;

        public DrawCircle()
        {
            SystemLog.Log("DrawCircle");
           // Init();
        }

        private void Init()
        {
            SystemLog.Log("start1");
            _factory = new GeometryFactory();
            SystemLog.Log("start1112");
            _gviTmpObjList = new List<IRObject>();

            SystemLog.Log("start1113");
            _beginPointSymbol = new SimplePointSymbol();
            _beginPointSymbol.FillColor = ColorConvert.UintToColor(0xffffff00);
            _beginPointSymbol.Size = 10;
            SystemLog.Log("start2");

            _watchPoint = CreateIRenderPoint(0, 0, 0);
            SystemLog.Log("start3");
            _endPoint = CreateIRenderPoint(0, 0, 0);
            SystemLog.Log("start4");
            _watchLabel = CreateLabel(0, 0, 0, "分析中心");
            _endLabel = CreateLabel(0, 0, 0, " ");
            SystemLog.Log("start5");
            _rLine = CreateRenderPolyLine(CreateIPoint(0, 0, 0), CreateIPoint(0, 0, 0));

            SystemLog.Log("start6");
            _watchPoint.MouseSelectMask = 0;
            _endPoint.MouseSelectMask = 0;
            _watchLabel.MouseSelectMask = 0;
            _endLabel.MouseSelectMask = 0;
            _rLine.MouseSelectMask = 0;
            _mouseClickNum = 0;
            SystemLog.Log("start7");
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            SystemLog.Log("start0000");
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start(ctl3d, eCustomer, operaType);
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            SystemLog.Log("start0000");
            Start(ctl3d, eCustomer, operaType);
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            SystemLog.Log("start0000");
            Start(ctl3d, null, operaType);
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            End(ctl3d);
        }

        private void End(AxRenderControl ctl3d)
        {
            Ocx.FeatureManager.UnhighlightAll();
            Ocx.HighlightHelper.SetRegion(null);
            RCDrawManager.Instance.PointMove.UnRegister(Ocx);
            RCDrawManager.Instance.PointMove.OnDrawFinished -= FinishingPickUp;
            //RCEventManager.Instance.PointPick.OnRButtonUp -= new OnRcRButtonUp(RcRButtonUp);
            ClearRenderObj(_gviTmpObjList);
            // ClearRenderObj(_gviObjList);
        }

        private void ClearRenderObj(List<IRObject> list)
        {
            _mouseClickNum = 0;

            if (list != null)
            {
                foreach (var o in list)
                {
                    if (o == null)
                        continue;
                    Ocx.ObjectManager.DeleteObject(o.Guid);
                }
                list.Clear();
            }
            //if (_rLine != null) 
            //{
            //    _renderControl.ObjectManager.DeleteObject(_rLine.Guid);
            //    _rLine.ReleaseComObject();
            //}
            _beginPointSymbol = null;
        }

        private void Start(AxRenderControl ctl3d, IDrawCustomer ec, RCMouseOperType operaType)
        {
            if (_rLine == null)
                Init();
            SystemLog.Log("start0");
            RCDrawManager.Instance.PointMove.Register(Ocx, ec, RCMouseOperType.PickMove);
            RCDrawManager.Instance.PointMove.OnDrawFinished += FinishingPickUp;
            SystemLog.Log("start");
        }


        private void FinishingPickUp(object sender, object result)
        {
            var p = result as DrawPoint;
            if (p == null)
                return;
            if (p.eventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                MouseSelectClick(p);
            }
            else if (p.eventSender == gviMouseSelectMode.gviMouseSelectMove)
            {
                MouseSelectMove(p);
            }
        }


        public void MouseSelectClick(DrawPoint p)
        {
            _mouseClickNum++;
            switch (_mouseClickNum)
            {
                case 0:
                    break;
                case 1: //选择了圆心
                    _point =
                        _factory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ)
                            as IPoint;
                    _point.X = p.x;
                    _point.Y = p.y;
                    _point.Z = p.z;
                    _bx = p.x;
                    _by = p.y;
                    _bz = p.z;
                    SetRenderPointPosition(_watchPoint, p.x, p.y, p.z);
                    break;
                case 2: //选择了半径
                    _ex = p.x;
                    _ey = p.y;
                    _ez = p.z;
                    SetRenderPointPosition(_endPoint, p.x, p.y, p.z);
                    IPoint point1, point2;
                    CreateTwoPoint(out point1, out point2);
                    DragRegion(point1, point2);

                    try
                    {
                        _geometry = Ocx.HighlightHelper.GetRegion();
                        Finish(_geometry);
                        ClearRenderObj(_gviTmpObjList);
                    }
                    catch (COMException ex)
                    {
                        SystemLog.Log(ex);
                    }
                    finally
                    {
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     鼠标移动
        /// </summary>
        public void MouseSelectMove(DrawPoint p)
        {
            if (_mouseClickNum >= 2)
                return;

            switch (_mouseClickNum)
            {
                case 0:
                    _bx = p.x;
                    _by = p.y;
                    _bz = p.z;
                    _ex = p.x;
                    _ey = p.y;
                    _ez = p.z;
                    SetRenderPointPosition(_watchPoint, p.x, p.y, p.z);
                    SetLabelPosition(_watchLabel, p.x, p.y, p.z + 0.1);
                    SetRenderPointPosition(_endPoint, p.x, p.y, p.z);
                    break;
                case 1: //选择了观察点
                    _ex = p.x;
                    _ey = p.y;
                    _ez = p.z;
                    IPoint point1, point2;
                    CreateTwoPoint(out point1, out point2);
                    DragRegion(point1, point2);
                    SetRenderPointPosition(_endPoint, p.x, p.y, p.z);
                    SetLabelPosition(_endLabel, p.x, p.y, p.z + 0.1);
                    SetPolylinePosition(_rLine, point1, point2);
                    break;
                default:
                    break;
            }
        }

        private void SetPolylinePosition(IRenderPolyline polyline, IPoint begin, IPoint end)
        {
            polyline.SetFdeGeometry(CreatePolyLine(begin, end));
        }

        private IPolyline CreatePolyLine(IPoint begin, IPoint end)
        {
            var polyline =
                _factory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as
                    IPolyline;
            polyline.AppendPoint(begin);
            polyline.AppendPoint(end);
            ICurveSymbol symbol = new CurveSymbol();
            symbol.Color = Color.BlueViolet;
            return polyline;
        }

        private IRenderPolyline CreateRenderPolyLine(IPoint begin, IPoint end)
        {
            var polyline =
                _factory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as
                    IPolyline;
            polyline.SpatialCRS = GviMap.SpatialCrs;
            polyline.AppendPoint(begin);
            polyline.AppendPoint(end);
            ICurveSymbol symbol = new CurveSymbol();
            symbol.Color = Color.BlueViolet;
            var rpolyline = Ocx.ObjectManager.CreateRenderPolyline(polyline, symbol);
            _gviTmpObjList.Add(rpolyline);
            return rpolyline;
        }

        private void DragRegion(IPoint begin, IPoint end)
        {
            var line =
                _factory.CreateGeometry(gviGeometryType.gviGeometryLine, gviVertexAttribute.gviVertexAttributeZ) as
                    ILine;
            line.StartPoint = begin;
            line.EndPoint = end;
            Ocx.HighlightHelper.VisibleMask = 1;
            Ocx.HighlightHelper.SetCircleRegion(begin, line.Length);
        }

        private void CreateTwoPoint(out IPoint begin, out IPoint end)
        {
            var geo = _factory.CreateGeometry(gviGeometryType.gviGeometryModelPoint,
                gviVertexAttribute.gviVertexAttributeZ);
            begin = geo as IPoint;
            begin.SetCoords(_bx, _by, _bz, 0, 0);
            geo = _factory.CreateGeometry(gviGeometryType.gviGeometryModelPoint, gviVertexAttribute.gviVertexAttributeZ);
            end = geo as IPoint;
            end.SetCoords(_ex, _ey, _ez, 0, 0);
        }

        private ILabel CreateLabel(double x, double y, double z, string name)
        {
            var label = CreateRenderTool.DrawLabel(x, y, z, name, 0, 10);
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            label.MaxVisibleDistance = RCConst.RO_MaxObserveDistance;
            label.MinVisiblePixels = (float)RCConst.RO_MinObserveDistance;
            _gviTmpObjList.Add(label);
            return label;
        }

        private IRenderPoint CreateIRenderPoint(double x, double y, double z)
        {
            var pw = CreateIPoint(x, y, z);
            pw.SpatialCRS = GviMap.SpatialCrs;
            var RenderPoint = Ocx.ObjectManager.CreateRenderPoint(pw, _beginPointSymbol);
            RenderPoint.MaxVisibleDistance = RCConst.RO_MaxObserveDistance;
            RenderPoint.MinVisiblePixels = (float)RCConst.RO_MinObserveDistance;
            _gviTmpObjList.Add(RenderPoint);
            return RenderPoint;
        }

        private void SetRenderPointPosition(IRenderPoint point, double x, double y, double z)
        {
            try
            {
                if (point == null)
                    return;
                var p =
                    _factory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as
                        IPoint;
                p.X = x;
                p.Y = y;
                p.Z = z;
                point.SetFdeGeometry(p);
            }
            catch (COMException ex)
            {
                SystemLog.Log(ex);
            }
        }

        private IPoint CreateIPoint(double x, double y, double z)
        {
            var p =
                _factory.CreateGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ) as
                    IPoint;
            p.X = x;
            p.Y = y;
            p.Z = z;
            return p;
        }


        private void SetLabelPosition(ILabel label, double x, double y, double z)
        {
            var gpnt = _factory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            gpnt.Position = new Vector3 { X = x, Y = y, Z = z };
            label.Position = gpnt;
        }
    }

    /// <summary>
    ///     点拾取工具,也是点移动、点右键等工具基类,供其他子类扩展
    /// </summary>
    public class PointPick : Draw
    {
        protected int ex, ey;
        public OnRCRightButtonUp OnRButtonUp = null;

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            Start(ctl3d, operaType);
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            Start(ctl3d, operaType);
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            base.Register(ctl3d, operaType);
            Start(ctl3d, operaType);
        }

        private void Start(AxRenderControl ctl3d, RCMouseOperType OperType)
        {
            Ocx.MouseSelectObjectMask = (gviMouseSelectObjectMask)_selectObjType;
            switch (OperType)
            {
                case RCMouseOperType.PickPoint:
                default:
                    {
                        Ocx.InteractMode = gviInteractMode.gviInteractSelect;
                        //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                        Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                //case MouseOperType.PickFeatureLayer:
                //  {
                //      Ocx.InteractMode = gviInteractMode.gviInteractSelect;
                //      //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                //      Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                //  }
                //  break;
                case RCMouseOperType.PickMove:
                    {
                        Ocx.InteractMode = gviInteractMode.gviInteractSelect;
                        //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll; //gviMouseSelectObjectMask.gviSelectReferencePlane | gviMouseSelectObjectMask.gviSelectTerrain;
                        Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectMove | gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                case RCMouseOperType.PickHover:
                    {
                        Ocx.InteractMode = gviInteractMode.gviInteractSelect;
                        //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll; //gviMouseSelectObjectMask.gviSelectReferencePlane | gviMouseSelectObjectMask.gviSelectTerrain;
                        Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover | gviMouseSelectMode.gviMouseSelectMove |
                                              gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                case RCMouseOperType.InteractEdit:
                    {
                        Ocx.InteractMode = gviInteractMode.gviInteractEdit;
                        //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                        Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                case RCMouseOperType.PickDrag:
                    {
                        Ocx.InteractMode = gviInteractMode.gviInteractSelect;
                        //Ocx.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
                        Ocx.MouseSelectMode = gviMouseSelectMode.gviMouseSelectDrag | gviMouseSelectMode.gviMouseSelectClick;
                    }
                    break;
                case RCMouseOperType.UnKnow:
                    break;
            }
            //如果为Unknown，不挂接任何事件
            if (OperType == RCMouseOperType.UnKnow)
            {
            }
            else
            {
                //Ocx.RcLButtonDown += Ocx_RcLButtonDown;
                Ocx.RcLButtonDown += ocx_RcLButtonDown;
                Ocx.RcLButtonUp += ocx_RcLButtonUp;
                Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
                Ocx.RcRButtonUp += ocx_RcRButtonUp;
            }
            iCount++;
        }



        protected void EnableMouseClickSelect()
        {
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
            //Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
            Ocx.RcMouseClickSelect += ocx_RcMouseClickSelect;
        }



        protected void DisableMouseClickSelect()
        {
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            End(ctl3d);
        }

        private void End(AxRenderControl ctl3d)
        {
            Ocx.RcLButtonDown -= ocx_RcLButtonDown;
            Ocx.RcLButtonUp -= ocx_RcLButtonUp;
            Ocx.RcMouseClickSelect -= ocx_RcMouseClickSelect;
            Ocx.RcRButtonUp -= ocx_RcRButtonUp;
            Ocx.InteractMode = gviInteractMode.gviInteractNormal;
            iCount--;
            if (iCount < 0) iCount = 0;
            _point = null;
        }

        #region 系统事件

        private bool ocx_RcLButtonUp(uint Flags, int X, int Y)
        {
            return false;
        }

        private double tempX, tempY;

        private bool ocx_RcLButtonDown(uint Flags, int X, int Y)
        {
            tempX = X;
            tempY = Y;
            return false;
        }

        //鼠标右键UP事件处理方法
        public bool ocx_RcRButtonUp(uint Flags, int X, int Y)
        {
            if (OnRButtonUp != null)
            {
                OnRButtonUp();
            }
            return false;
        }

        /// <summary>
        ///     鼠标拾取事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ocx_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                DrawPoint modelPoint = null;

                switch (OperType)
                {
                    case RCMouseOperType.PickPoint:
                    case RCMouseOperType.PickMove:
                    case RCMouseOperType.PickHover:
                    case RCMouseOperType.InteractEdit:
                    case RCMouseOperType.PickDrag:
                        if (PickResult != null)
                        {
                            switch (PickResult.Type)
                            {
                                case gviObjectType.gviObjectFeatureLayer:
                                    {
                                        modelPoint = PickResultoUP(PickResult, IntersectPoint, Mask, EventSender);
                                    }
                                    break;
                                case gviObjectType.gviObjectRenderModelPoint:
                                    {
                                        modelPoint = PickResultoModelUP(PickResult, IntersectPoint, Mask, EventSender);
                                    }
                                    break;
                                case gviObjectType.gviObjectRenderPolygon:
                                    {
                                        modelPoint = PickResultoRenderPolygonUP(PickResult, IntersectPoint, Mask, EventSender);
                                    }
                                    break;
                                case gviObjectType.gviObjectRenderMultiPolygon:
                                    {
                                        modelPoint = PickResultoRenderMultiPolygonUP(PickResult, IntersectPoint, Mask, EventSender);
                                    }
                                    break;
                                default:
                                    {
                                        modelPoint = PickResultoSimpleUP(PickResult, IntersectPoint, Mask, EventSender);
                                    }
                                    break;
                            }
                        }
                        Finish(modelPoint);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                Finish(null);
            }
        }

        /// <summary>
        ///     将拾取结果转换为简单的UPPoint对象，返回的UPPoint对象的x，y，z为相交点的坐标值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DrawPoint PickResultoSimpleUP(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var modelPoint = new DrawPoint();
            var vector3 = IntersectPoint;
            modelPoint.x = vector3.X;
            modelPoint.y = vector3.Y;
            modelPoint.z = vector3.Z;
            modelPoint.beFeature = false;
            var es = (int)EventSender;
            modelPoint.eventSender = (gviMouseSelectMode)es;
            modelPoint.mask = Mask;
            return modelPoint;
        }

        /// <summary>
        ///     普通Feature的解析
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DrawPoint PickResultoUP(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var modelPoint = PickResultoSimpleUP(PickResult, IntersectPoint, Mask, EventSender);
            FeatureLayerPickRsToUP(PickResult, ref modelPoint);
            return modelPoint;
        }

        /// <summary>
        ///     ModelPoint对象解析
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DrawPoint PickResultoModelUP(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var modelPoint = PickResultoSimpleUP(PickResult, IntersectPoint, Mask, EventSender);
            ModelPickRsToUP(PickResult, ref modelPoint);
            //FeatureLayerPickRsToUP(PickResult, ref modelPoint);
            return modelPoint;
        }

        /// <summary>
        ///     RenderPolygon解析
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DrawPoint PickResultoRenderPolygonUP(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var modelPoint = PickResultoSimpleUP(PickResult, IntersectPoint, Mask, EventSender);
            RenderPolygonPickRsToUP(PickResult, ref modelPoint);
            return modelPoint;
        }

        /// <summary>
        ///     RenderPolygon解析
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public DrawPoint PickResultoRenderMultiPolygonUP(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var modelPoint = PickResultoSimpleUP(PickResult, IntersectPoint, Mask, EventSender);
            RenderMultiPolygonPickRsToUP(PickResult, ref modelPoint);
            return modelPoint;
        }

        /// <summary>
        ///     拾取对象为ModelPoint对象时，解析相关参数
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="modelPoint"></param>
        public void ModelPickRsToUP(IPickResult pr, ref DrawPoint modelPoint)
        {
            var mpp = pr as IRenderModelPointPickResult;
            if (mpp != null)
            {
                modelPoint.modelPoint = mpp.RenderModelPoint;
                modelPoint.primitiveIndex = mpp.PrimitiveIndex;
                modelPoint.drawGroupIndex = mpp.DrawGroupIndex;
                modelPoint.pickFeatureType = mpp.Type;
            }
        }

        /// <summary>
        ///     拾取对象的FeatureClass和FeatureLayer解析
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="modelPoint"></param>
        public void FeatureLayerPickRsToUP(IPickResult pr, ref DrawPoint modelPoint)
        {
            var flpr = pr as IFeatureLayerPickResult;
            if (flpr != null)
            {
                modelPoint.pickFeatureID = flpr.FeatureId;
                modelPoint.pickFeatureClassGUID = flpr.FeatureLayer.FeatureClassId;
                modelPoint.pickFeatureLayer = flpr.FeatureLayer;
                modelPoint.pickFeatureType = flpr.Type;

                modelPoint.beFeature = true;
            }
            else
            {
                modelPoint.beFeature = false;
            }
        }

        /// <summary>
        ///     拾取RenderPolygon
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="polygon"></param>
        public void RenderPolygonPickRsToUP(IPickResult pr, ref DrawPoint polygon)
        {
            var polygonPr = pr as IRenderPolygonPickResult;
            if (polygonPr != null)
            {
                polygon.polygon = polygonPr.RenderPolygon;
                polygon.beFeature = false;
                polygon.pickFeatureType = polygonPr.Type;
            }
        }

        /// <summary>
        ///     拾取RenderPolygon
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="polygon"></param>
        public void RenderMultiPolygonPickRsToUP(IPickResult pr, ref DrawPoint polygon)
        {
            var polygonPr = pr as IRenderMultiPolygonPickResult;
            if (polygonPr != null)
            {
                polygon.multiPolygon = polygonPr.RenderMultiPolygon;
                polygon.beFeature = false;
                polygon.pickFeatureType = polygonPr.Type;
            }
        }

        #endregion
    }

    /// <summary>
    ///     鼠标拾取类，带键盘事件
    /// </summary>
    public class PointPickKey : PointPick
    {
        public OnRCKeyPress OnRCKeyDown = null;
        public OnRCKeyPress OnRCKeyUp = null;

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            EnableKeyUp();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            EnableKeyUp();
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            base.Register(ctl3d, operaType);
            EnableKeyUp();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            DisableKeyUp();
        }

        private void EnableKeyUp()
        {
            Ocx.RcKeyUp += ocx_RcKeyUp;
            Ocx.RcKeyDown += ocx_RcKeyDown;
        }

        private void DisableKeyUp()
        {
            Ocx.RcKeyUp -= ocx_RcKeyUp;
            Ocx.RcKeyDown -= ocx_RcKeyDown;
        }

        private bool ocx_RcKeyUp(uint Flags, uint Ch)
        {
            if (OnRCKeyUp != null)
            {
                var kcode = (RcKeyCode)Ch;
                OnRCKeyUp(kcode);
            }
            return false;
        }

        private bool ocx_RcKeyDown(uint Flags, uint Ch)
        {
            if (OnRCKeyDown != null)
            {
                var kcode = (RcKeyCode)Ch;
                OnRCKeyDown(kcode);
            }
            return false;
        }
    }

    /// <summary>
    ///     鼠标移动、悬停
    /// </summary>
    public class PointPickMoveHover : PointPickMove
    {
        public _IRenderControlEvents_RcMouseHoverEventHandler onMouseHover = null;

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            EnableMouseHover();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            EnableMouseHover();
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            base.Register(ctl3d, operaType);
            EnableMouseHover();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            DisableMouseHover();
        }

        private void EnableMouseHover()
        {
            Ocx.RcMouseHover += Ocx_RcMouseHover;
        }

        private void DisableMouseHover()
        {
            Ocx.RcMouseHover -= Ocx_RcMouseHover;
        }

        public bool Ocx_RcMouseHover(uint Flags, int X, int Y)
        {
            if (onMouseHover != null)
            {
                onMouseHover(Flags, X, Y);
            }
            return false;
        }
    }

    /// <summary>
    ///     鼠标拾取类，带鼠标拾取事件、Move事件和键盘事件
    /// </summary>
    public class PointPickMove : PointPickKey
    {
        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            EnableMouseMove();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            EnableMouseMove();
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            base.Register(ctl3d, operaType);
            EnableMouseMove();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            DisableMouseMove();
        }

        private void EnableMouseMove()
        {
            Ocx.RcMouseMove += Ocx_RcMouseMove;
        }

        private void DisableMouseMove()
        {
            Ocx.RcMouseMove -= Ocx_RcMouseMove;
        }

        public bool Ocx_RcMouseMove(uint Flags, int X, int Y)
        {
            if (BeInOcxScreen())
            {
                //this.EnableMouseMove();
                EnableMouseClickSelect();
            }
            else
            {
                // this.DisableMouseMove();
                DisableMouseClickSelect();
            }
            return false;
        }

        /// <summary>
        ///     获取鼠标在哪个屏幕
        /// </summary>
        /// <returns>0为第一屏，1为第二屏，依次类推</returns>
        private bool BeInOcxScreen()
        {
            var x = Cursor.Position.X;
            var y = Cursor.Position.Y;
            var pt = Ocx.PointToScreen(GviMap.AxMapControl.Location);
            var width = Ocx.Width;
            var height = Ocx.Height;
            if (x >= pt.X && x < pt.X + width && y > pt.Y && y < pt.Y + height) return true;
            return false;
        }
    }

    public delegate void OnDragFinished(object sender, List<DrawPoint> result);

    /// <summary>
    ///     鼠标拾取类，带键盘事件和框选事件
    /// </summary>
    public class PointKeyDrag : PointPickKey
    {
        public event OnDragFinished OnDragFinish;

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operaObj)
        {
            base.Register(ctl3d, eCustomer, operaType, operaObj);
            EnableDrag();
        }

        public override void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            base.Register(ctl3d, eCustomer, operaType);
            EnableDrag();
        }

        public override void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            base.Register(ctl3d, operaType);
            EnableDrag();
        }

        public override void UnRegister(AxRenderControl ctl3d)
        {
            base.UnRegister(ctl3d);
            DisEnableDrag();
        }

        private void EnableDrag()
        {
            Ocx.RcMouseDragSelect += Ocx_RcMouseDragSelect;
        }

        private void DisEnableDrag()
        {
            Ocx.RcMouseDragSelect -= Ocx_RcMouseDragSelect;
        }

        private void Ocx_RcMouseDragSelect(IPickResultCollection PickResults, gviModKeyMask Mask)
        {
            try
            {
                List<DrawPoint> modelPoint = null;
                switch (OperType)
                {
                    case RCMouseOperType.PickPoint:
                    case RCMouseOperType.PickMove:
                    case RCMouseOperType.InteractEdit:
                    case RCMouseOperType.PickDrag:
                        modelPoint = PickResultsToUPset(PickResults, Mask);
                        FinishDrag(modelPoint);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                FinishDrag(null);
            }
        }

        public List<DrawPoint> PickResultsToUPset(IPickResultCollection PickResults, gviModKeyMask Mask)
        {
            var upntSet = new List<DrawPoint>();
            var res = PickResults;
            if (res != null)
            {
                for (var i = 0; i < res.Count; i++)
                {
                    var pres = res.Get(i);
                    if (pres.Type == gviObjectType.gviObjectFeatureLayer)
                    {
                        var flpr = pres as IFeatureLayerPickResult;
                        if (flpr == null) continue;
                        var mp = new DrawPoint();
                        FeatureLayerPickRsToUP(pres, ref mp);
                        mp.mask = Mask;
                        upntSet.Add(mp);
                    }
                    else if (pres.Type == gviObjectType.gviObjectRenderPolygon)
                    {
                        var flpr = pres as IRenderPolygonPickResult;
                        if (flpr == null) continue;
                        var polygon = new DrawPoint();
                        RenderPolygonPickRsToUP(pres, ref polygon);
                        polygon.mask = Mask;
                        upntSet.Add(polygon);
                    }
                }
            }
            return upntSet;
        }

        public void FinishDrag(List<DrawPoint> result)
        {
            if (OnDragFinish != null && result != null)
                OnDragFinish(this, result);
        }
    }

    //鼠标操作类型
    public enum RCMouseOperType
    {
        /// <summary>
        ///     点拾取，拾取所有图层，包括FeatureLayer、DatumPlane和terrian等
        /// </summary>
        PickPoint,

        /// <summary>
        ///     点拾取，拾取FeatureLayer
        /// </summary>
        /// <summary>
        ///     鼠标跟踪，即可拾取又可Move,为录入设施服务
        /// </summary>
        PickMove,

        /// <summary>
        ///     悬停拾取
        /// </summary>
        PickHover,

        /// <summary>
        ///     拾取多边形
        /// </summary>
        PickPolygon,

        /// <summary>
        ///     交互编辑，包含点拾取
        /// </summary>
        InteractEdit,

        /// <summary>
        ///     拾取，包括框选
        /// </summary>
        PickDrag,

        UnKnow
    }

    public enum RcKeyCode
    {
        Delete = 46,
        Ctrl = 17,
        Esc = 27
    }

    /// <summary>
    ///     绘制工具的基类
    /// </summary>
    public abstract class Draw : IDraw
    {
        /// <summary>
        ///     开始吸取材质的事件
        /// </summary>
        //public event StartPickUpTextureHandler StartPickUpTextureEvent;
        private RCMouseOperType _oper = RCMouseOperType.UnKnow;

        protected DrawPoint _point;
        internal gviMouseSelectObjectMask _selectObjType = gviMouseSelectObjectMask.gviSelectAll;
        protected int iCount;

        public Draw()
        {
            Ocx = null;
        }

        public AxRenderControl Ocx { set; get; }

        public int NumOfUsingTools
        {
            get { return iCount; }
        }

        public RCMouseOperType OperType
        {
            set { _oper = value; }
            get { return _oper; }
        }

        public virtual void Register(AxRenderControl ctl3d, RCMouseOperType operaType)
        {
            Ocx = ctl3d;
            _oper = operaType;
            //清除上个事件消费者的事件，同时恢复其对话环境
            if (DrawClient.Instance.DrawCustomer != null)
            {
                DrawClient.Instance.DrawCustomer.Restore();
                //_eventCustomer.UnRegister();
                DrawClient.Instance.DrawCustomer = null;
            }
            //注册新的事件消费者对象
            iCount++;
        }

        public virtual void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType)
        {
            Ocx = ctl3d;
            _oper = operaType;
            if (eCustomer != null)
            {
                //清除上个事件消费者的事件，同时恢复其对话环境
                if (DrawClient.Instance.DrawCustomer != null)
                {
                    if (eCustomer.Equals(DrawClient.Instance.DrawCustomer))
                    {
                    }
                    else
                    {
                        //本来不应该加在这的（caoronglong 2-13-6-5），没有办法，只能加在这里
                        if (DrawClient.Instance.DrawCustomer.BeChooseFacility()) // && !eCustomer.BeChooseFacility())
                        {
                            //WorkUIService.Instance.SenderIsPreformClick = true;
                            //MainFrmService.BarPerformClick("ChooseFacility");
                        }
                        DrawClient.Instance.DrawCustomer.Restore();
                        //_eventCustomer.UnRegister();
                        DrawClient.Instance.DrawCustomer = null;
                        //注册新的事件消费者对象
                        DrawClient.Instance.DrawCustomer = eCustomer;
                    }
                }
                else
                {
                    //注册新的事件消费者对象
                    DrawClient.Instance.DrawCustomer = eCustomer;
                }
            }
            else
            {
                //清除上个事件消费者的事件，同时恢复其对话环境
                if (DrawClient.Instance.DrawCustomer != null)
                {
                    //本来不应该加在这的（caoronglong 2-13-6-5），没有办法，只能加在这里
                    if (DrawClient.Instance.DrawCustomer.BeChooseFacility())
                    {
                        //WorkUIService.Instance.SenderIsPreformClick = true;
                        //MainFrmService.BarPerformClick("ChooseFacility");
                    }
                    DrawClient.Instance.DrawCustomer.Restore();
                    DrawClient.Instance.DrawCustomer = null;
                }
            }
            iCount++;
        }

        /// <summary>
        ///     事件注册方法
        /// </summary>
        /// <param name="ctl3d">事件控件</param>
        /// <param name="eCustomer">事件消费者，记录事件的消费者，便于进行事件完成后，回调该对象恢复工作环境</param>
        /// <param name="operaType">操作类型,如点选、框选、移动等</param>
        /// <param name="operObj">事件的操作对象，如FeatureLayer，地形等</param>
        public virtual void Register(AxRenderControl ctl3d, IDrawCustomer eCustomer, RCMouseOperType operaType,
            gviMouseSelectObjectMask operObj)
        {
            Ocx = ctl3d;
            _oper = operaType;
            _selectObjType = operObj;
            if (eCustomer != null)
            {
                //清除上个事件消费者的事件，同时恢复其对话环境
                if (DrawClient.Instance.DrawCustomer != null)
                {
                    //本来不应该加在这的（caoronglong 2-13-6-5），没有办法，只能加在这里
                    if (DrawClient.Instance.DrawCustomer.BeChooseFacility() && !eCustomer.BeChooseFacility())
                    {
                        //WorkUIService.Instance.SenderIsPreformClick = true;
                        //MainFrmService.BarPerformClick("ChooseFacility");
                    }
                    DrawClient.Instance.DrawCustomer.Restore();
                    //_eventCustomer.UnRegister();
                    DrawClient.Instance.DrawCustomer = null;
                }
                //注册新的事件消费者对象
                DrawClient.Instance.DrawCustomer = eCustomer;
            }
            else
            {
                //清除上个事件消费者的事件，同时恢复其对话环境
                if (DrawClient.Instance.DrawCustomer != null)
                {
                    //本来不应该加在这的（caoronglong 2-13-6-5），没有办法，只能加在这里
                    if (DrawClient.Instance.DrawCustomer.BeChooseFacility())
                    {
                        //WorkUIService.Instance.SenderIsPreformClick = true;
                        //MainFrmService.BarPerformClick("ChooseFacility");
                    }
                    DrawClient.Instance.DrawCustomer.Restore();
                    DrawClient.Instance.DrawCustomer = null;
                }
            }
            iCount++;
        }

        public virtual void UnRegister(AxRenderControl ctl3d)
        {
            Ocx = ctl3d;
            iCount--;
        }

        public virtual void Finish(object result)
        {
            if (OnDrawFinished != null)
            {
                OnDrawFinished(this, result);
            }


            //else
            //{
            //    throw new Exception("绘制完成事件委托未绑定！");
            //}
        }

        public event OnRCDrawFinished OnDrawFinished;
    }

    /// <summary>
    ///     绘制功能接口
    /// </summary>
    internal interface IDraw
    {
        //使用该功能的工具个数
        int NumOfUsingTools { get; }
        RCMouseOperType OperType { set; get; }

        /// <summary>
        ///     注册绘制功能
        /// </summary>
        /// <param name="ctl3d"></param>
        /// <param name="cmd"></param>
        /// <param name="_eventDrawFinished"></param>
        void Register(AxRenderControl ctl3d, RCMouseOperType objectType);

        void Register(AxRenderControl ctl3d, IDrawCustomer eventCustomer, RCMouseOperType operType);

        void Register(AxRenderControl ctl3d, IDrawCustomer eventCustomer, RCMouseOperType operType,
            gviMouseSelectObjectMask selectObjType);

        /// <summary>
        ///     完成对象绘制
        /// </summary>
        /// <param name="Polygon"></param>
        void Finish(object result);

        /// <summary>
        ///     注销绘制功能
        /// </summary>
        /// <param name="ctl3d"></param>
        /// <param name="cmd"></param>
        /// <param name="_eventDrawFinished"></param>
        void UnRegister(AxRenderControl ctl3d);
    }
}