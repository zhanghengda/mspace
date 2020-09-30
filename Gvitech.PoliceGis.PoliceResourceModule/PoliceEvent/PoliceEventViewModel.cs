using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Models.Case;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.PoliceEventService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Toolkit.MarkupExtensions;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services.JscriptInvokeService;

namespace Mmc.Mspace.PoliceResourceModule.PoliceEvent
{
	// Token: 0x02000008 RID: 8
	public class PoliceEventViewModel : CheckedToolItemModel
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public PoliceEventViewModel()
		{
			this.PoliceEventModels = this.GetPoliceEventModels();
			this.BufferDistances = new List<double>
			{
				100.0,
				500.0,
				1000.0,
				2000.0,
				5000.0
			};
			this.SelectedBufferDistance = 500.0;
			this.FilterCmd = new RelayCommand(delegate(object p)
			{
				CaseType caseType = (CaseType)Enum.Parse(typeof(CaseType), p.ToString());
				foreach (PoliceEventModel policeEventModel in this.PoliceEventModels)
				{
					bool flag = policeEventModel.EventType != caseType;
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002B8B File Offset: 0x00000D8B
		private IRenderPolygon PolygonRender { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002B9C File Offset: 0x00000D9C
		private IRenderPOI POIRender { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002BA5 File Offset: 0x00000DA5
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002BAD File Offset: 0x00000DAD
		private IComplexParticleEffect Fire { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002BD0 File Offset: 0x00000DD0
		[XmlIgnore]
		public PoliceEventModel SelectedPoliceEventModel
		{
			get
			{
				return this.selectedPoliceEventModel;
			}
			set
			{
				this.FlyToGeometry(value);
				bool flag = this.PolygonRender != null;
				if (flag)
				{
					ServiceManager.GetService<IQueryService>(null).Geomtry = this.PolygonRender.GetFdeGeometry();
				}
				base.SetAndNotifyPropertyChanged<PoliceEventModel>(ref this.selectedPoliceEventModel, value, "SelectedPoliceEventModel");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C20 File Offset: 0x00000E20
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002C38 File Offset: 0x00000E38
		public List<double> BufferDistances
		{
			get
			{
				return this.bufferDistances;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<List<double>>(ref this.bufferDistances, value, "BufferDistances");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002C50 File Offset: 0x00000E50
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002C68 File Offset: 0x00000E68
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002C86 File Offset: 0x00000E86
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002C8E File Offset: 0x00000E8E
		[XmlIgnore]
		public ICommand FilterCmd { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002CB0 File Offset: 0x00000EB0
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public int FilterSelectedIndex
		{
			get
			{
				return this.filterSelectedIndex;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<int>(ref this.filterSelectedIndex, value, "FilterSelectedIndex");
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D10 File Offset: 0x00000F10
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

		// Token: 0x0600003D RID: 61 RVA: 0x00002D88 File Offset: 0x00000F88
		private void FlyToGeometry(PoliceEventModel model)
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
						this.PolygonRender = objectManager.CreateRenderPolygon( polygon, surfaceSymbol);
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

		// Token: 0x0600003E RID: 62 RVA: 0x00002EEC File Offset: 0x000010EC
		private void CreateDynamicParticleEffect(PoliceEventModel model)
		{
			this.HideDynamicParticleEffect();
			switch (model.EventType)
			{
			case CaseType.FireAccident:
				this.CreateFire(model);
				break;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F40 File Offset: 0x00001140
		private void HideDynamicParticleEffect()
		{
			bool flag = this.Fire != null;
			if (flag)
			{
				this.Fire.Stop();
				this.Fire.VisibleMask = gviViewportMask.gviViewNone;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F78 File Offset: 0x00001178
		private void CreateFire(PoliceEventModel model)
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

		// Token: 0x06000041 RID: 65 RVA: 0x0000307C File Offset: 0x0000127C
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

		// Token: 0x06000042 RID: 66 RVA: 0x00003198 File Offset: 0x00001398
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

		// Token: 0x06000043 RID: 67 RVA: 0x00003200 File Offset: 0x00001400
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

		// Token: 0x06000044 RID: 68 RVA: 0x00003290 File Offset: 0x00001490
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

		// Token: 0x06000045 RID: 69 RVA: 0x00003304 File Offset: 0x00001504
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

		// Token: 0x06000046 RID: 70 RVA: 0x00003370 File Offset: 0x00001570
		public ObservableCollection<PoliceEventModel> GetPoliceEventModels()
		{
			List<CaseInfo> caseInfos = Singleton<CaseHttpService>.Instance.GetCaseInfos(DateTime.Now, DateTime.Now, 1, 10);
			bool flag = IEnumerableExtension.HasValues<CaseInfo>(caseInfos);
			ObservableCollection<PoliceEventModel> result;
			if (flag)
			{
				ObservableCollection<PoliceEventModel> observableCollection = new ObservableCollection<PoliceEventModel>();
				foreach (CaseInfo caseInfo in caseInfos)
				{
					PoliceEventModel item = new PoliceEventModel(caseInfo);
					observableCollection.Add(item);
				}
				result = observableCollection;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003404 File Offset: 0x00001604
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
			PoliceEventViewModel.savedPoliceEventModel = this.SelectedPoliceEventModel;
			this.SelectedPoliceEventModel = null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003460 File Offset: 0x00001660
		private void ReleaseCom()
		{
            GviMap.ObjectManager.ReleaseRenderObject( new IRenderable[]
			{
				this.POIRender,
				this.PolygonRender,
				this.Fire
			});
			this.PolygonRender = null;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003498 File Offset: 0x00001698
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000034B0 File Offset: 0x000016B0
		[XmlIgnore]
		public ObservableCollection<PoliceEventModel> PoliceEventModels
		{
			get
			{
				return this.policeEventModels;
			}
			set
			{
				base.SetAndNotifyPropertyChanged<ObservableCollection<PoliceEventModel>>(ref this.policeEventModels, value, "PoliceEventModels");
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034C8 File Offset: 0x000016C8
		public override void OnChecked()
		{
			base.OnChecked();
            //var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            //webView.JsScriptInvoker.collopsePanel(true);
            bool flag = this.policeEventView == null;
			if (flag)
			{
				this.policeEventView = new PoliceEventView();
				this.policeEventView.Owner = Application.Current.MainWindow;
				this.policeEventView.DataContext = this;
				this.policeEventView.Left = 100.0;
				this.policeEventView.Top = 100.0;
			}
			bool flag2 = this.timer == null;
			if (flag2)
			{
				this.timer = new Timer();
			}
			this.timer.Interval = 5000.0;
			this.timer.Enabled = true;
			this.timer.Elapsed += delegate(object s, ElapsedEventArgs e)
			{
				this.policeEventView.Dispatcher.Invoke(delegate
				{
					this.PoliceEventModels = this.GetPoliceEventModels();
					bool flag3 = this.FilterCmd != null&& this.enumeProvider!=null;
					if (flag3)
					{
						this.FilterCmd.Execute(this.enumeProvider.Value);
					}
				});
			};
			this.timer.AutoReset = true;
			this.timer.Start();
			this.policeEventView.Show();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000035BC File Offset: 0x000017BC
		public override void OnUnchecked()
		{
			base.OnUnchecked();
            bool flag = this.timer != null;
			if (flag)
			{
				this.timer.Stop();
			}
			bool flag2 = this.policeEventView != null;
			if (flag2)
			{
				this.policeEventView.Hide();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003603 File Offset: 0x00001803
		public override void Initialize()
		{
			base.Initialize();
			base.ViewType = (ViewType)1;
		}

		// Token: 0x0400000F RID: 15
		private static PoliceEventModel savedPoliceEventModel;

		// Token: 0x04000010 RID: 16
		private List<double> bufferDistances;

		// Token: 0x04000011 RID: 17
		private EnumProvider enumeProvider;

		// Token: 0x04000012 RID: 18
		private int filterSelectedIndex;

		// Token: 0x04000013 RID: 19
		private double selectedBufferDistance;

		// Token: 0x04000014 RID: 20
		private PoliceEventModel selectedPoliceEventModel;

		// Token: 0x04000019 RID: 25
		private ObservableCollection<PoliceEventModel> policeEventModels = new ObservableCollection<PoliceEventModel>();

		// Token: 0x0400001A RID: 26
		private Window policeEventView;

		// Token: 0x0400001B RID: 27
		private Timer timer;
	}
}
