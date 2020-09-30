using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.NetRouteAnalysisService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Models.MovePoi;

namespace Mmc.Mspace.PoliceDeployedModule.PoliceDeployed
{
    // Token: 0x02000012 RID: 18
    public class DeployPlan : BindableBase
    {
        // Token: 0x0600006E RID: 110 RVA: 0x00005C88 File Offset: 0x00003E88
        public void RestoreEnv()
        {
            this.UnregisterEvents();
            this.DeleteLines();
        }

        // Token: 0x0600006F RID: 111 RVA: 0x00005C9C File Offset: 0x00003E9C
        public DeployPlan(ObservableCollection<DeployPlan> deployPlans)
        {
            this.DeployPlans = deployPlans;
            this.layers = ServiceManager.GetService<IDataBaseService>(null).GetAllLayerItemModels();
            this.CreateTargetPointCmd = new RelayCommand(() =>
            {
                this.isTargetPointDown = true;
                this.IsSelected = true;
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractSelect;
                GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.AxMapControl.RcMouseClickSelect += this.SelectedTargetPoint_RcMouseClickSelect;
                this.isTargetPointDown = true;
            });
            this.CreatePolicesCmd = new RelayCommand(() =>
            {
                this.isPolicesDown = true;
                this.IsSelected = true;
                GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MapControl.MouseSelectObjectMask = (gviMouseSelectObjectMask)257;
                GviMap.AxMapControl.RcLButtonDown += new _IRenderControlEvents_RcLButtonDownEventHandler(SelectedPolices_RenderControl_RcLButtonDown);
                this.isPolicesDown = true;
            });
            this.DeletePlanCmd = new RelayCommand(() =>
            {
                this.UnregisterEvents();
                this.DeleteLines();
                this.DeployPlans.Remove(this);
            });
            this.Polices.CollectionChanged += delegate (object s, NotifyCollectionChangedEventArgs e)
            {
                this.CreateLines();
            };
        }

        // Token: 0x06000070 RID: 112 RVA: 0x00005D4C File Offset: 0x00003F4C
        public void SetDepolyCamera()
        {
            bool flag = this.targetPoint != null;
            if (flag)
            {
                IEnvelope envelope = this.targetPoint.Envelope.Clone();
                foreach (Police police in this.Polices)
                {
                    envelope.ExpandByVector(police.Position);
                }
                GviMap.MapControl.Camera.FlyTime = 0.0;
                IPoint point = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                point.SpatialCRS = GviMap.SpatialCrs;
                point.SetCoords(envelope.MinX, envelope.MinY, envelope.MinZ, 0.0, 0);
                IGemetryExtension.ProjectEx(point, WKTString.PROJ_CGCS2000_WKT);
                IPoint point2 = GviMap.GeoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
                point2.SpatialCRS = GviMap.SpatialCrs;
                point2.SetCoords(envelope.MaxX, envelope.MaxY, envelope.MaxZ, 0.0, 0);
                IGemetryExtension.ProjectEx(point2, WKTString.PROJ_CGCS2000_WKT);
                IEnvelope Envelope = new Gvitech.CityMaker.Math.Envelope();
                Envelope.Set(point.X, point2.X, point.Y, point2.Y, point.Z, point2.Z);
                FdeGeometryRelease.ReleaseComObject(point);
                FdeGeometryRelease.ReleaseComObject(point2);
                float num = IEnvelopeExtension.DiagonalDistance(Envelope);
                bool flag2 = num < 500f;
                if (flag2)
                {
                    GviMap.MapControl.Camera.LookAt(new Vector3
                    {
                        X = envelope.Center.X,
                        Y = envelope.Center.Y,
                        Z = envelope.Center.Z
                    }, 1000.0, new EulerAngle
                    {
                        Heading = 45.0,
                        Roll = 0.0,
                        Tilt = -45.0
                    });
                    Thread.Sleep(2000);
                }
                else
                {
                    GviMap.MapControl.Camera.LookAt(new Vector3
                    {
                        X = envelope.Center.X,
                        Y = envelope.Center.Y,
                        Z = envelope.Center.Z
                    }, (double)num, new EulerAngle
                    {
                        Heading = 45.0,
                        Roll = 0.0,
                        Tilt = -45.0
                    });
                    Thread.Sleep(2000);
                }
            }
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00006018 File Offset: 0x00004218
        private void UnregisterEvents()
        {
            GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            GviMap.MapControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
            bool flag = this.isTargetPointDown;
            if (flag)
            {
                GviMap.AxMapControl.RcMouseClickSelect -= this.SelectedTargetPoint_RcMouseClickSelect;
                this.isTargetPointDown = false;
            }
            bool flag2 = this.isPolicesDown;
            if (flag2)
            {
                GviMap.AxMapControl.RcLButtonDown -= new _IRenderControlEvents_RcLButtonDownEventHandler(SelectedPolices_RenderControl_RcLButtonDown);
                this.isPolicesDown = false;
            }
        }

        // Token: 0x06000072 RID: 114 RVA: 0x000060A0 File Offset: 0x000042A0
        private bool SelectedPolices_RenderControl_RcLButtonDown(uint Flags, int X, int Y)
        {
            IPoint point;
            IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(X, Y, out point);
            bool flag = pickResult == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = pickResult.Type == gviObjectType.gviObjectRenderPOI;
                if (flag2)
                {
                    IRenderPOIPickResult renderPOIPickResult = pickResult as IRenderPOIPickResult;
                    bool flag3 = renderPOIPickResult == null;
                    if (flag3)
                    {
                        return false;
                    }
                    IMovePoi movePoi = null;
                    foreach (LayerItemModel layerItemModel in this.layers)
                    {
                        bool flag4 = layerItemModel.Parameters is PoiFeatueClass;
                        if (flag4)
                        {
                            PoiFeatueClass poiFeatueClass = (PoiFeatueClass)layerItemModel.Parameters;
                            bool flag5 = poiFeatueClass.Name == "Police" || poiFeatueClass.Name == "PoliceCar";
                            if (flag5)
                            {
                                movePoi = poiFeatueClass.SearchByRenderId(renderPOIPickResult.RenderPOI.Guid.ToString());
                            }
                        }
                        bool flag6 = movePoi != null;
                        if (flag6)
                        {
                            break;
                        }
                    }
                    bool flag7 = movePoi != null;
                    if (flag7)
                    {
                        DataTable showDataTable = movePoi.ShowDataTable;
                        Police p = new Police(this.Polices);
                        int num;
                        for (int i = 0; i < showDataTable.Rows.Count; i = num + 1)
                        {
                            bool flag8 = showDataTable.Rows[i][0].ToString() == "警员Id" || showDataTable.Rows[i][0].ToString() == "车辆Id";
                            if (flag8)
                            {
                                p.ID = showDataTable.Rows[i][1].ToString();
                            }
                            bool flag9 = showDataTable.Rows[i][0].ToString() == "警员名称" || showDataTable.Rows[i][0].ToString() == "车牌号";
                            if (flag9)
                            {
                                p.Name = showDataTable.Rows[i][1].ToString();
                            }
                            num = i;
                        }
                        bool flag10 = (from plan in this.DeployPlans
                                       from pl in plan.Polices
                                       where plan != this && p.ID == pl.ID
                                       select plan).Any<DeployPlan>();
                        if (flag10)
                        {
                            return false;
                        }
                        Police police = this.Polices.FirstOrDefault((Police po) => p.ID == po.ID);
                        bool flag11 = police != null;
                        if (flag11)
                        {
                            this.Polices.Remove(police);
                            return false;
                        }
                        IPoint point2 = null;
                        GviMap.MapControl.Camera.ScreenToWorld(X, Y, out point2);
                        p.Position = IPointExtension.ToVector3(point2);
                        IVector3 position = p.Position;
                        position.Z += 5.0;
                        this.Polices.Add(p);
                    }
                }
                result = true;
            }
            return result;
        }

        // Token: 0x06000073 RID: 115 RVA: 0x00006460 File Offset: 0x00004660
        private void CreateLines()
        {
            try
            {
                this.DeleteLines();
                bool flag = this.targetPoint == null;
                if (!flag)
                {
                    this.targetRenderPoint = GviMap.MapControl.ObjectManager.CreateRenderPoint(IPointExtension.ToVector3(this.targetPoint), new ImagePointSymbol
                    {
                        ImageName = Application.StartupPath + "\\data\\poi\\targetFlag.png",
                        Size = 48
                    }, GviMap.SpatialCrs, GviMap.GeoFactory);
                    this.targetRenderPoint.MaxVisibleDistance = 100000.0;
                    bool flag2 = this.Polices == null || this.Polices.Count < 1;
                    if (!flag2)
                    {
                        IVector3 position = IPointExtension.ToVector3(this.targetPoint);
                        IPolyline lineClone = null;
                        this.Polices.ToList<Police>().ForEach(delegate (Police p)
                        {
                            var result = ServiceManager.GetService<IRouteBDAnalysisService>(RouteBDAnalysisService.GetDefault()).RouteAnalysis(new List<IVector3>    {
                                p.Position,
                                position
                            }, GviMap.MapControl.ObjectManager, GviMap.SpatialCrs);

                            IRenderPolyline renderPolyline = result.Item2;
                            if (renderPolyline != null)
                            {
                                this.lines.Add(p.ID, renderPolyline);
                                lineClone = (IPolyline)renderPolyline.GetFdeGeometry().Clone();
                                lineClone.SpatialCRS = GviMap.SpatialCrs;
                                IGemetryExtension.ProjectEx(lineClone, WKTString.PROJ_CGCS2000_WKT);
                                p.Distance = Math.Round(lineClone.Length, 2);
                                p.UseTime =result.Item1;
                                FdeGeometryRelease.ReleaseComObject(lineClone);
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        // Token: 0x06000074 RID: 116 RVA: 0x0000656C File Offset: 0x0000476C
        public void ShowLines(bool show)
        {
            gviViewportMask visMask = show ? gviViewportMask.gviViewAllNormalView : gviViewportMask.gviViewNone;
            IEnumerableExtension.ForEach<IRenderPolyline>(this.lines.Values, delegate (IRenderPolyline line)
            {
                line.VisibleMask = visMask;
            });
            bool flag = this.targetRenderPoint != null;
            if (flag)
            {
                this.targetRenderPoint.VisibleMask = visMask;
            }
        }

        // Token: 0x06000075 RID: 117 RVA: 0x000065CC File Offset: 0x000047CC
        public void DeleteLines()
        {
            bool flag = IDictionaryExtension.HasValues<string, IRenderPolyline>(this.lines);
            if (flag)
            {
                GviMap.MapControl.ObjectManager.ReleaseRenderObject(this.lines.Values.ToArray<IRenderable>());
                this.lines.Clear();
            }
            bool flag2 = this.targetRenderPoint != null;
            if (flag2)
            {
                GviMap.MapControl.ObjectManager.ReleaseRenderObject(new IRenderable[]
                {
                    this.targetRenderPoint
                });
            }
        }

        // Token: 0x06000076 RID: 118 RVA: 0x00006644 File Offset: 0x00004844
        private void SelectedTargetPoint_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            bool flag = EventSender == gviMouseSelectMode.gviMouseSelectMove;
            if (!flag)
            {
                this.targetPoint = IntersectPoint;
                bool flag2 = this.targetPoint != null;
                if (flag2)
                {
                    IPoint point = this.targetPoint;
                    point.Z += 10.0;
                }
                this.CreateLines();
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x06000077 RID: 119 RVA: 0x0000669F File Offset: 0x0000489F
        // (set) Token: 0x06000078 RID: 120 RVA: 0x000066A7 File Offset: 0x000048A7
        public ICommand CreateTargetPointCmd { get; set; }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x06000079 RID: 121 RVA: 0x000066B0 File Offset: 0x000048B0
        // (set) Token: 0x0600007A RID: 122 RVA: 0x000066B8 File Offset: 0x000048B8
        public ICommand CreatePolicesCmd { get; set; }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x0600007B RID: 123 RVA: 0x000066C1 File Offset: 0x000048C1
        // (set) Token: 0x0600007C RID: 124 RVA: 0x000066C9 File Offset: 0x000048C9
        public ICommand DeletePlanCmd { get; set; }

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x0600007D RID: 125 RVA: 0x000066D2 File Offset: 0x000048D2
        // (set) Token: 0x0600007E RID: 126 RVA: 0x000066DA File Offset: 0x000048DA
        public string Snapshot { get; set; }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x0600007F RID: 127 RVA: 0x000066E4 File Offset: 0x000048E4
        // (set) Token: 0x06000080 RID: 128 RVA: 0x000066FC File Offset: 0x000048FC
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this.name, value, "Name");
            }
        }

        // Token: 0x17000011 RID: 17
        // (get) Token: 0x06000081 RID: 129 RVA: 0x00006714 File Offset: 0x00004914
        // (set) Token: 0x06000082 RID: 130 RVA: 0x0000672C File Offset: 0x0000492C
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.UnregisterEvents();
                base.SetAndNotifyPropertyChanged<bool>(ref this.isSelected, value, "IsSelected");
                bool flag = !this.isSelected;
                if (flag)
                {
                    this.DeleteLines();
                }
                else
                {
                    this.CreateLines();
                }
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x06000083 RID: 131 RVA: 0x00006774 File Offset: 0x00004974
        // (set) Token: 0x06000084 RID: 132 RVA: 0x0000678C File Offset: 0x0000498C
        public ObservableCollection<Police> Polices
        {
            get
            {
                return this.polices;
            }
            set
            {
                this.polices = value;
            }
        }

        // Token: 0x04000043 RID: 67
        private readonly ObservableCollection<DeployPlan> DeployPlans;

        // Token: 0x04000044 RID: 68
        private readonly List<LayerItemModel> layers = new List<LayerItemModel>();

        // Token: 0x04000045 RID: 69
        private readonly Dictionary<string, IRenderPolyline> lines = new Dictionary<string, IRenderPolyline>();

        // Token: 0x04000046 RID: 70
        private bool isPolicesDown;

        // Token: 0x04000047 RID: 71
        private bool isTargetPointDown;

        // Token: 0x04000048 RID: 72
        private IPoint targetPoint;

        // Token: 0x04000049 RID: 73
        private IRenderPoint targetRenderPoint;

        // Token: 0x0400004E RID: 78
        private string name;

        // Token: 0x0400004F RID: 79
        private bool isSelected;

        // Token: 0x04000050 RID: 80
        private ObservableCollection<Police> polices = new ObservableCollection<Police>();
    }
}
