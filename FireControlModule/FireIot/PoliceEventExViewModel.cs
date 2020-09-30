using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Utils;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Mspace.Services.PoliceEventService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Toolkit.MarkupExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule.FireIot
{
    /// <summary>
    /// 灭火救援
    /// </summary>
    public class PoliceEventExViewModel : CheckedToolItemModel
    {
        private static FireIotEventModel savedPoliceEventModel;
        private IDisplayLayer _dLyr;
        private List<IRenderable> _rObjs;
        private List<double> bufferDistances;
        private Dictionary<string, EnumProvider> dicItems = new Dictionary<string, EnumProvider>();
        private EnumProvider enumeProvider;
        private int filterSelectedIndex;
        private ObservableCollection<FireIotEventModel> fireIotEventModels = new ObservableCollection<FireIotEventModel>();
        private FireIotSevice fireIotSevice;
        private ObservableCollection<EnumProvider> items = new ObservableCollection<EnumProvider>();

        private Window policeEventView;

        private double selectedBufferDistance;

        private FireIotEventModel selectedFireIotEventModel;

        private Timer timer;

        public PoliceEventExViewModel()
        {
            this.fireIotSevice = new FireIotSevice();
            this.fireIotEventModels = this.GetFireIotEventModels();
            this.BufferDistances = new List<double>
            {
                100.0,
                200,
                500.0,
                1000.0,
                2000.0,
                5000.0
            };
            this.SelectedBufferDistance = 200.0;
            this.FilterCmd = new RelayCommand(delegate (object p)
            {
                var bulidInfo = p.ToString();
                foreach (FireIotEventModel policeEventModel in this.FireIotEventModels)
                {
                    bool flag = policeEventModel.BuildInfo.Value != bulidInfo;
                    if (flag)
                    {
                        policeEventModel.IsVisible = Visibility.Collapsed;
                    }
                    else
                    {
                        policeEventModel.IsVisible = Visibility.Visible;
                    }
                }
            });
            this.FilterSelectedIndex = 0;
        }

        public List<double> BufferDistances
        {
            get { return this.bufferDistances; }
            set { base.SetAndNotifyPropertyChanged<List<double>>(ref this.bufferDistances, value, "BufferDistances"); }
        }

        [XmlIgnore]
        public ICommand FilterCmd { get; set; }

        public int FilterSelectedIndex
        {
            get { return this.filterSelectedIndex; }
            set { base.SetAndNotifyPropertyChanged<int>(ref this.filterSelectedIndex, value, "FilterSelectedIndex"); }
        }

        [XmlIgnore]
        public ObservableCollection<FireIotEventModel> FireIotEventModels
        {
            get
            {
                return this.fireIotEventModels;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<FireIotEventModel>>(ref this.fireIotEventModels, value, "FireIotEventModels");
            }
        }

        [XmlIgnore]
        public ObservableCollection<EnumProvider> Items
        {
            get { return this.items; }
            set { base.SetAndNotifyPropertyChanged<ObservableCollection<EnumProvider>>(ref this.items, value, "Items"); }
        }

        public double SelectedBufferDistance
        {
            get
            {
                return this.selectedBufferDistance;
            }
            set
            {
                this.ChangeBufferPolygon(value);
                base.SetAndNotifyPropertyChanged<double>(ref this.selectedBufferDistance, value, "SelectedBufferDistance");
            }
        }

        public EnumProvider SelectedFilterType
        {
            get
            {
                return this.enumeProvider;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<EnumProvider>(ref this.enumeProvider, value, "SelectedFilterType");
                this.FilterCmd.Execute(this.enumeProvider.Value);
            }
        }

        [XmlIgnore]
        public FireIotEventModel SelectedFireIotEventModel
        {
            get
            {
                return this.selectedFireIotEventModel;
            }
            set
            {
                // this.policeEventView.Hide();
                this.FlyToGeometry(value);
                bool flag = this.PolygonRender != null;
                if (flag)
                {
                    ServiceManager.GetService<IQueryService>(null).Geomtry = this.PolygonRender.GetFdeGeometry();
                }
                base.SetAndNotifyPropertyChanged<FireIotEventModel>(ref this.selectedFireIotEventModel, value, "SelectedFireIotEventModel");
            }
        }

        private IComplexParticleEffect Fire { get; set; }
        private IRenderPOI POIRender { get; set; }
        private IRenderPolygon PolygonRender { get; set; }

        public ObservableCollection<FireIotEventModel> GetFireIotEventModels()
        {
            var fireEvents = new ObservableCollection<FireIotEventModel>();
            var allFireInfos = this.fireIotSevice.GetAllFireIotEx();
            var buildKeys = this.fireIotSevice.BuildFireIotKeys;
            var oldBuildInfos = this.fireIotSevice.DicOldBuildInfos;
            foreach (var key in buildKeys.Keys)
            {
                var sensorCodes = buildKeys[key];
                foreach (var item in allFireInfos)
                {
                    if (sensorCodes.Contains(item.code) && item.status == "异常")//异常
                    {
                        var fireEvent = new FireIotEventModel();
                        var enumItem = new EnumProvider() { Value = oldBuildInfos[key].Item1, Alias = oldBuildInfos[key].Item2 };
                        fireEvent.BuildInfo = enumItem;
                        fireEvent.Address = item.addr;
                        fireEvent.Createdate = item.createdate;
                        fireEvent.Value = item.value;
                        fireEvent.Name = item.name;
                        fireEvent.Code = item.code;
                        fireEvent.CodeInfo = new Tuple<string, string>(oldBuildInfos[key].Item1, item.code);
                        fireEvent.TelPepple = "张三";
                        fireEvent.TelPhone = "13568748596";
                        fireEvent.ValueRange = item.cgqfz;
                        fireEvent.Status = item.status;
                        if (!dicItems.ContainsKey(oldBuildInfos[key].Item1))
                        {
                            dicItems.Add(oldBuildInfos[key].Item1, enumItem);
                            this.items.Add(enumItem);
                        }
                        //if (!this.items.Contains(enumItem))
                        //this.items.Add(enumItem);
                        fireEvents.Add(fireEvent);
                    }
                }
            }

            return fireEvents;
        }

        public ObservableCollection<FireIotEventModel> GetFireIotEventModels11111()
        {
            var fireEvents = new ObservableCollection<FireIotEventModel>();
            var allFireInfos = this.fireIotSevice.GetAllFireIotEx();
            var buildKeys = this.fireIotSevice.BuildFireIotKeys;
            var oldBuildInfos = this.fireIotSevice.DicOldBuildInfos;
            foreach (var key in buildKeys.Keys)
            {
                var sensorCodes = buildKeys[key];
                foreach (var item in allFireInfos)
                {
                    if (sensorCodes.Contains(item.code) && item.status == "1")//异常
                    {
                        var fireEvent = new FireIotEventModel();
                        var enumItem = new EnumProvider() { Value = oldBuildInfos[key].Item1, Alias = oldBuildInfos[key].Item2 };
                        fireEvent.BuildInfo = enumItem;
                        fireEvent.Address = item.addr;
                        fireEvent.Createdate = item.createdate;
                        fireEvent.Value = string.Format("{0}Mpa", item.value);
                        fireEvent.Name = item.name;
                        fireEvent.Code = item.code;
                        fireEvent.CodeInfo = new Tuple<string, string>(oldBuildInfos[key].Item1, item.code);
                        fireEvent.TelPepple = "张三";
                        fireEvent.TelPhone = "13568748596";
                        fireEvent.ValueRange = string.Format("{0}Mpa", item.cgqfz);
                        fireEvent.Status = item.status == "1" ? "异常" : "正常";
                        if (!dicItems.ContainsKey(oldBuildInfos[key].Item1))
                        {
                            dicItems.Add(oldBuildInfos[key].Item1, enumItem);
                            this.items.Add(enumItem);
                        }
                        //if (!this.items.Contains(enumItem))
                        //this.items.Add(enumItem);
                        fireEvents.Add(fireEvent);
                    }
                }
            }

            return fireEvents;
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ServiceManager.GetService<IShellService>().HideAllView();
            ServiceManager.GetService<IShellService>().ShowBottomRight(0);
            //var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            //webView.JsScriptInvoker.collopsePanel(true);
            GviMap.Camera.FlyTime = 0.5; //默认飞行为0.5秒
            if (_rObjs == null)
                _rObjs = new List<IRenderable>();
            if (_dLyr == null)
            {
                var actaulLyrs = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
                actaulLyrs.ForEach(p => { if (p.Fc.Alias == "建筑") _dLyr = p; });
            }
            if (this.policeEventView == null)
            {
                this.policeEventView = new PoliceEventExView();
                this.policeEventView.Owner = Application.Current.MainWindow;
                this.policeEventView.DataContext = this;
                this.policeEventView.Left = 10;
                this.policeEventView.Top = 20;
            }
            bool flag2 = this.timer == null;
            if (flag2)
            {
                this.timer = new Timer();
            }
            this.timer.Interval = 5000.0;
            this.timer.Enabled = true;
            this.timer.Elapsed += delegate (object s, ElapsedEventArgs e)
            {
                this.policeEventView.Dispatcher.Invoke(delegate
                {
                    this.FireIotEventModels = this.GetFireIotEventModels();
                    bool flag3 = this.FilterCmd != null && this.enumeProvider != null;
                    if (flag3)
                    {
                        this.FilterCmd.Execute(this.enumeProvider.Value);
                    }
                });
            };
            this.timer.AutoReset = true;
            this.timer.Start();
            this.SelectedFireIotEventModel = this.FireIotEventModels.HasValues<FireIotEventModel>() ? this.FireIotEventModels[0] : null;
            this.policeEventView.Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ServiceManager.GetService<IShellService>().ShowAllView();
            //var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            //webView.JsScriptInvoker.collopsePanel(false);
            ReleaseRobjs();
            HideDynamicParticleEffect();
            bool flag = this.timer != null;
            if (flag)
            {
                this.timer.Stop();
            }
            if (this.policeEventView != null)
                this.policeEventView.Hide();
        }

        public IPolygon PointBuffer(IPoint pnt, double distance)
        {
            ICRSFactory icrsfactory = new CRSFactory();
            ISpatialCRS spatialCRS = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            ISpatialCRS spatialCRS2 = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
            IPoint point = pnt.Clone() as IPoint;
            point.SpatialCRS = spatialCRS;
            IGemetryExtension.ProjectEx(point, WKTString.PROJ_CGCS2000_WKT);
            ITopologicalOperator2D topologicalOperator2D = point as ITopologicalOperator2D;
            bool flag = topologicalOperator2D == null;
            IPolygon result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IGeometry geometry = topologicalOperator2D.Buffer2D(distance, gviBufferStyle.gviBufferCapround);
                geometry.SpatialCRS = spatialCRS2;
                result = (geometry as IPolygon);
            }
            return result;
        }

        public void ReleaseRobjs()
        {
            GviMap.ObjectManager.ReleaseRenderObject(_rObjs.ToArray());
            _rObjs.Clear();
        }

        public override void Reset()
        {
            base.Reset();
            bool isChecked = base.IsChecked;
            if (isChecked)
            {
                base.IsChecked = false;
            }
            try
            {
                this.ReleaseCom();
            }
            catch (Exception)
            {
            }
            PoliceEventExViewModel.savedPoliceEventModel = this.SelectedFireIotEventModel;
            this.SelectedFireIotEventModel = null;
        }

        public IPolygon UpdateZ(IPolygon geo, double Z)
        {
            bool flag = geo == null;
            IPolygon result;
            if (flag)
            {
                result = geo;
            }
            else
            {
                geo = (geo.Clone2(gviVertexAttribute.gviVertexAttributeZ ) as IPolygon);
                int num;
                for (int i = 0; i < geo.ExteriorRing.PointCount; i = num + 1)
                {
                    IPoint point = geo.ExteriorRing.GetPoint(i);
                    point.Z = Z;
                    geo.ExteriorRing.UpdatePoint(i, point);
                    num = i;
                }
                result = geo;
            }
            return result;
        }

        private void ChangeBufferPolygon(double distance)
        {
            bool flag = this.PolygonRender == null;
            if (!flag)
            {
                IPolygon polygon = this.PolygonRender.GetFdeGeometry() as IPolygon;
                IPoint centroid = polygon.Centroid;
                IPolygon geo = this.PointBuffer(centroid, distance);
                IPolygon polygon2 = this.UpdateZ(geo, 5.0);
                bool flag2 = polygon2 == null;
                if (!flag2)
                {
                    this.PolygonRender.SetFdeGeometry(polygon2);
                    ServiceManager.GetService<IQueryService>(null).Geomtry = polygon2;
                }
            }
        }

        private ISurfaceSymbol CreateDefaultSurfaceSymbol()
        {
            return new SurfaceSymbol
            {
                Color = Color.Transparent,
                BoundarySymbol = new CurveSymbol
                {
                    Color = Color.FromArgb(200, Color.Red),
                    Width = 10f
                }
            };
        }

        private void CreateDynamicParticleEffect(PoliceEventExModel model)
        {
            this.HideDynamicParticleEffect();
            switch (model.EventType)
            {
                case CaseType.FireAccident:
                    this.CreateFire(model);
                    break;
            }
        }

        private void CreateDynamicParticleEffect(IPoint point)
        {
            this.HideDynamicParticleEffect();
            this.CreateFire(point);
        }

        private void CreateEventPoint(IPoint pnt)
        {
            bool flag = pnt == null;
            if (!flag)
            {
                bool flag2 = pnt.SpatialCRS == null;
                if (flag2)
                {
                    pnt.SpatialCRS = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT);
                }
                IObjectManager objectManager = GviMap.MapControl.ObjectManager;
                IGeometryFactory geometryFactory = new GeometryFactory();
                IPOI ipoi = geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ ) as IPOI;
                IPointExtension.SetByPoint(ipoi, pnt);
                ipoi.Z = this.GetZ(pnt.X, pnt.Y);
                ipoi.Size = 48;
                ipoi.SpatialCRS = pnt.SpatialCRS;
                objectManager.ReleaseRenderObject(this.POIRender);
                string imageName = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "resources\\目标旗.png");
                ipoi.ImageName = imageName;
                this.POIRender = objectManager.CreateRenderPOI(ipoi);
                this.POIRender.VisibleMask = gviViewportMask.gviViewAllNormalView;
                this.POIRender.MaxVisibleDistance = double.MaxValue;
                this.POIRender.MinVisibleDistance = 0.0;
            }
        }

        private void CreateFire(PoliceEventExModel model)
        {
            bool flag = model == null || model.EventType != CaseType.FireAccident;
            if (!flag)
            {
                bool flag2 = this.Fire == null;
                if (flag2)
                {
                    IObjectManager objectManager = GviMap.MapControl.ObjectManager;
                    this.Fire = objectManager.CreateComplexParticleEffect(gviComplexParticleEffectType.gviComplexParticleEffectFire_0, objectManager.GetGuid());
                    this.Fire.ScalingFactor = 15.0;
                    this.Fire.MaxVisibleDistance = 10000.0;
                    this.Fire.MinVisiblePixels = 15f;
                }
                IPoint point = model.Location as IPoint;
                bool flag3 = point.SpatialCRS == null;
                if (flag3)
                {
                    point.SpatialCRS = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT);
                }
                IGemetryExtension.ProjectEx(point, GviMap.SpatialCrs.AsWKT());
                this.Fire.Position = point;
                this.Fire.VisibleMask = gviViewportMask.gviViewAllNormalView;
                this.Fire.Play();
            }
        }

        private void CreateFire(IPoint point)
        {
            if (this.Fire == null)
            {
                IObjectManager objectManager = GviMap.MapControl.ObjectManager;
                this.Fire = objectManager.CreateComplexParticleEffect(gviComplexParticleEffectType.gviComplexParticleEffectFire_0, objectManager.GetGuid());
                this.Fire.ScalingFactor = 15.0;
                this.Fire.MaxVisibleDistance = 10000.0;
                this.Fire.MinVisiblePixels = 15f;
            }
            IGemetryExtension.ProjectEx(point, GviMap.SpatialCrs.AsWKT());
            this.Fire.Position = point;
            this.Fire.VisibleMask = gviViewportMask.gviViewAllNormalView;
            this.Fire.Play();
        }

        private void FlyToGeometry(PoliceEventExModel model)
        {
            bool flag = model == null || model.Location == null;
            if (!flag)
            {
                IObjectManager objectManager = GviMap.MapControl.ObjectManager;
                try
                {
                    objectManager.ReleaseRenderObject(this.PolygonRender);
                    this.PolygonRender = null;
                    IPoint point = model.Location as IPoint;
                    bool flag2 = point == null;
                    if (!flag2)
                    {
                        bool flag3 = point.SpatialCRS == null;
                        if (flag3)
                        {
                            point.SpatialCRS = (ISpatialCRS)GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT);
                        }
                        this.CreateEventPoint(point);
                        IPolygon geo = this.PointBuffer(point, this.SelectedBufferDistance);
                        IPolygon polygon = this.UpdateZ(geo, 50.0);
                        ISurfaceSymbol surfaceSymbol = this.CreateDefaultSurfaceSymbol();
                        this.PolygonRender = objectManager.CreateRenderPolygon(polygon, surfaceSymbol);
                        this.PolygonRender.HeightStyle = gviHeightStyle.gviHeightRelative;
                        this.PolygonRender.MaxVisibleDistance = double.MaxValue;
                        this.PolygonRender.MinVisibleDistance = 0.0;
                        ICameraExtension.LookAtGeometry(GviMap.MapControl.Camera, model.Location as IGeometry, 1500.0, "");
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }
        }

        private void FlyToGeometry(FireIotEventModel model)
        {
            if (model != null)
            {
                ReleaseRobjs();
                IObjectManager objectManager = GviMap.MapControl.ObjectManager;
                try
                {
                    var code = model.BuildInfo.Value;
                    var filter = new QueryFilter();
                    filter.WhereClause = string.Format("Name='{0}'", code);
                    filter.AddSubField("geometry");
                    //filter.AddSubField("Name");
                    ISurfaceSymbol surfaceSymbol = this.CreateDefaultSurfaceSymbol();
                    IModelPointSymbol mpsym = new ModelPointSymbol();
                    mpsym.Color = ColorConvert.UintToColor(GviColors.Red);
                    mpsym.EnableColor = true;
                    mpsym.SetResourceDataSet(_dLyr.Fc.FeatureDataSet);
                    var cr = _dLyr.Fc.Search(filter, true);
                    IRowBuffer row = null;
                    while ((row = cr.NextRow()) != null)
                    {
                        var geo = row.GetValue<IModelPoint>(0);
                        var rMp = GviMap.ObjectManager.CreateRenderModelPoint(geo, mpsym);
                        rMp.Glow(-1);
                        rMp.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        _rObjs.Add(rMp);
                        var point = geo.Envelope.Center.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs);
                        //  CreateDynamicParticleEffect(point);
                        IPolygon geoBuf = this.PointBuffer(point, this.SelectedBufferDistance);
                        IPolygon polygon = this.UpdateZ(geoBuf, 50.0);
                        this.PolygonRender = objectManager.CreateRenderPolygon(polygon, surfaceSymbol);
                        this.PolygonRender.HeightStyle = gviHeightStyle.gviHeightRelative;
                        this.PolygonRender.MaxVisibleDistance = double.MaxValue;
                        this.PolygonRender.MinVisibleDistance = 0.0;
                        ICameraExtension.LookAtGeometry(GviMap.MapControl.Camera, point as IGeometry, 500, "");
                        _rObjs.Add(PolygonRender);
                    }
                    cr.ReleaseComObject();
                    filter.ReleaseComObject();
                }
                catch (Exception ex)
                {
                    SystemLog.Log(ex);
                }
            }
        }

        private double GetZ(double x, double y)
        {
            bool flag = !GviMap.MapControl.Terrain.IsRegistered;
            double result;
            if (flag)
            {
                result = 6.0;
            }
            else
            {
                bool flag2 = !GviMap.MapControl.Terrain.DemAvailable;
                if (flag2)
                {
                    result = 6.0;
                }
                else
                {
                    result = GviMap.MapControl.Terrain.GetElevation(x, y, gviGetElevationType.gviGetElevationFromDatabase);
                }
            }
            return result;
        }

        private void HideDynamicParticleEffect()
        {
            if (this.Fire != null)
            {
                this.Fire.Stop();
                this.Fire.VisibleMask = gviViewportMask.gviViewNone;
            }
        }

        private void ReleaseCom()
        {
            GviMap.ObjectManager.ReleaseRenderObject(new IRenderable[]
            {
                this.POIRender,
                this.PolygonRender,
                this.Fire
            });
            this.PolygonRender = null;
        }
    }
}