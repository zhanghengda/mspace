using System;
using System.Collections.Generic;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoliceDeployedModule.SniperProtecton
{
	// Token: 0x02000008 RID: 8
	public class SniperProtectonCmd : SimpleCommand
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000042CC File Offset: 0x000024CC
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000042D4 File Offset: 0x000024D4
		public CheckedToolItemModel CmdHost { get; set; }

		// Token: 0x06000033 RID: 51 RVA: 0x000042DD File Offset: 0x000024DD
		public SniperProtectonCmd(CheckedToolItemModel cmdHost)
		{
			this.CmdHost = cmdHost;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004314 File Offset: 0x00002514
		public override void Execute(object parameter)
		{
			bool flag = StringExtension.ParseTo<bool>(parameter, false);
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this._segmentTool == null;
				if (flag3)
				{
					this._segmentTool = new DrawMutiSegmentTool(GviMap.AxMapControl, 1.7);
					DrawMutiSegmentTool segmentTool = this._segmentTool;
					segmentTool.OnFinishDraw = (FinishDrawEventHandler)Delegate.Combine(segmentTool.OnFinishDraw, new FinishDrawEventHandler(this.FinishDrawBeginAnalize));
					this._renderControl = GviMap.MapControl;
					this._beforeLineSymbol = new CurveSymbol();
					this._beforeLineSymbol.Color = ColorConvert.UintToColor(4278190335u);
					this._afterLineSymbol = new CurveSymbol();
					this._afterLineSymbol.Color = ColorConvert.UintToColor(4294901760u);
					this._LineSymbol = new CurveSymbol();
					this._geoFactory = GviMap.GeoFactory;
					this._redText = new TextSymbol();
					this._redText.TextAttribute.TextColor = ColorConvert.UintToColor(4294901760u);
					this._redText.PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter;
					this._redRenderPoint = new SimplePointSymbol();
					this._redRenderPoint.FillColor = ColorConvert.UintToColor(4294901760u);
					this._redRenderPoint.Size = 15;
					this._greenText = new TextSymbol();
					this._greenText.TextAttribute.TextColor = ColorConvert.UintToColor(4278255360u);
					this._greenText.PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter;
					this._greenRenderPoint = new SimplePointSymbol();
					this._greenRenderPoint.Size = 15;
					ICRSFactory icrsfactory = new CRSFactory();
					this._localCRS = (icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS);
					this._wgs84CRS = GviMap.SpatialCrs;
				}
				else
				{
					DrawMutiSegmentTool segmentTool = this._segmentTool;
					segmentTool.OnFinishDraw = (FinishDrawEventHandler)Delegate.Combine(segmentTool.OnFinishDraw, new FinishDrawEventHandler(this.FinishDrawBeginAnalize));
				}
				this._segmentTool.Start();
			}
			else
			{
				DrawMutiSegmentTool segmentTool = this._segmentTool;
				segmentTool.OnFinishDraw = (FinishDrawEventHandler)Delegate.Remove(segmentTool.OnFinishDraw, new FinishDrawEventHandler(this.FinishDrawBeginAnalize));
				this.ClearRenderObject();
				this._segmentTool.Stop();
				this._segmentTool.Release();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004534 File Offset: 0x00002734
		public void OnStop()
		{
			this.ClearRenderObject();
			bool flag = this._segmentTool != null;
			if (flag)
			{
				this._segmentTool.Stop();
				this._segmentTool.Release();
			}
			this.CmdHost.IsChecked = false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004580 File Offset: 0x00002780
		private void FinishDrawBeginAnalize(object sender, List<IPolyline> segmentList)
		{
			bool flag = segmentList == null || segmentList.Count <= 0;
			if (flag)
			{
				this.ClearRenderObject();
				this._segmentTool.Stop();
				this._segmentTool.Release();
				this.CmdHost.IsChecked = false;
			}
			else
			{
				this._segmentTool.DeleteRenderPolyline();
				Dictionary<IRowBuffer, IFeatureClass> dictionary = null;
				List<IDisplayLayer> actualityLayers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
				SpatialFilter spatialFilter = new SpatialFilter();
				int num = 0;
				foreach (IPolyline polyline in segmentList)
				{
					dictionary = new Dictionary<IRowBuffer, IFeatureClass>();
					foreach (IDisplayLayer displayLayer in actualityLayers)
					{
						spatialFilter.Geometry = polyline;
						spatialFilter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
						spatialFilter.GeometryField = "Geometry";
						IFdeCursor fdeCursor = displayLayer.Fc.Search(spatialFilter, false);
						IRowBuffer key;
						while ((key = fdeCursor.NextRow()) != null)
						{
							dictionary.Add(key, displayLayer.Fc);
						}
					}
					polyline.Project(this._localCRS);
					double length = polyline.Length;
					polyline.Project(this._wgs84CRS);
					this._renderObjectList.Add(this.AddLabel(polyline.Midpoint, string.Format("{0:N}m", length), 10, 2868903680u));
					IPoint point = null;
					bool flag2 = AnalysisUtils.PolylineIntersectModel(polyline, dictionary, out point);
					string text = "狙击点" + this._segmentTool.WatchPointindex;
					bool flag3 = flag2;
					IRenderPoint renderPoint;
					if (flag3)
					{
						this._renderObjectList.Add(this.AddRenderPolyline(polyline.StartPoint, point, this._beforeLineSymbol));
						this._renderObjectList.Add(this.AddRenderPolyline(point, polyline.EndPoint, this._afterLineSymbol));
						renderPoint = this.AddRenderPoint(polyline.EndPoint, this._redRenderPoint);
						this._renderObjectList.Add(renderPoint);
						this._renderObjectList.Add(this.AddLabel(polyline.EndPoint, text, 13, 4294967040u));
					}
					else
					{
						this._renderObjectList.Add(this.AddRenderPolyline(polyline.StartPoint, polyline.EndPoint, this._LineSymbol));
						renderPoint = this.AddRenderPoint(polyline.EndPoint, this._greenRenderPoint);
						this._renderObjectList.Add(renderPoint);
						this._renderObjectList.Add(this.AddLabel(polyline.EndPoint, text, 13, 4294967040u));
					}
					bool flag4 = !this._targetRenderPointList.ContainsKey(text);
					if (flag4)
					{
						this._targetRenderPointList.Add(text, renderPoint);
					}
					int num2 = num;
					num = num2 + 1;
				}
				this._segmentTool.SegmentList.Clear();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000048BC File Offset: 0x00002ABC
		private IRenderPolyline AddRenderPolyline(IPoint startPoint, IPoint endPoint, ICurveSymbol lineSymbol)
		{
			IGeometryFactory geometryFactory = new GeometryFactory();
			IPolyline polyline = geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
			polyline.StartPoint = startPoint;
			polyline.EndPoint = endPoint;
			polyline.SpatialCRS = GviMap.SpatialCrs;
			IRenderPolyline renderPolyline = GviMap.MapControl.ObjectManager.CreateRenderPolyline( polyline, lineSymbol);
			renderPolyline.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			renderPolyline.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			return renderPolyline;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000492C File Offset: 0x00002B2C
		private ILabel AddLabel(IPoint point, string labelString, ITextSymbol textSymbol)
		{
			ILabel label = this.DrawLabel(point.X, point.Y, point.Z + 0.1, labelString, 13, 4294967040u);
			label.TextSymbol = textSymbol;
			label.VisibleMask = gviViewportMask.gviViewAllNormalView;
			label.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			label.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			label.MouseSelectMask = gviViewportMask.gviViewNone;
			return label;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000499C File Offset: 0x00002B9C
		public ILabel DrawLabel(double posX, double posY, double posZ, string msg, int textSize = 13, uint textColor = 4294967040u)
		{
			ILabel label = this._renderControl.ObjectManager.CreateLabel(Guid.Empty);
			label.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
			label.Text = msg;
			label.TextSymbol = new TextSymbol
			{
				TextAttribute = 
				{
					TextColor =  ColorConvert.UintToColor(textColor),
					TextSize = textSize
				},
				PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter
			};
			IVector3 vector = new Vector3();
			vector.X = posX;
			vector.Y = posY;
			vector.Z = posZ;
			IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.Position = vector;
			label.Position = point;
			label.VisibleMask = gviViewportMask.gviViewAllNormalView;
			return label;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004A50 File Offset: 0x00002C50
		private ILabel AddLabel(IPoint point, string labelString, int textSize = 13, uint textColor = 4294967040u)
		{
			ILabel label = this.DrawLabel(point.X, point.Y, point.Z + 0.1, labelString, textSize, textColor);
			label.VisibleMask = gviViewportMask.gviViewAllNormalView;
			label.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			label.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			label.MouseSelectMask = gviViewportMask.gviViewNone;
			return label;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004AB4 File Offset: 0x00002CB4
		private IRenderPoint AddRenderPoint(IPoint point, IPointSymbol pointSymbol)
		{
			point.SpatialCRS = GviMap.SpatialCrs;
			IRenderPoint renderPoint = this._renderControl.ObjectManager.CreateRenderPoint( point, pointSymbol);
			renderPoint.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			renderPoint.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			renderPoint.MouseSelectMask = gviViewportMask.gviViewNone;
			return renderPoint;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004B08 File Offset: 0x00002D08
		private void ClearRenderObject()
		{
			try
			{
				this._targetRenderPointList.Clear();
				bool flag = this._renderObjectList.Count > 0;
				if (flag)
				{
					foreach (IRObject irobject in this._renderObjectList)
					{
						bool flag2 = irobject != null;
						if (flag2)
						{
							this._renderControl.ObjectManager.DeleteObject(irobject.Guid);
						}
					}
					this._renderObjectList.Clear();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004BBC File Offset: 0x00002DBC
		private void VilidateCRS()
		{
			ICRSFactory icrsfactory = new CRSFactory();
			ISpatialCRS spatialCRS = icrsfactory.CreateCGCS2000();
			ISpatialCRS spatialCRS2 = icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS;
			ISimplePointSymbol simplePointSymbol = new SimplePointSymbol();
			simplePointSymbol.Size = 20;
			IGeometryFactory geometryFactory = new GeometryFactory();
			IPoint point = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.SpatialCRS = spatialCRS2;
			point.X = 607070.03;
			point.Y = 4261966.32;
			point.Z = 13.17;
			Console.WriteLine("平面 {0}   {1}  {2}", point.X, point.Y, point.Z);
			IGemetryExtension.ProjectEx(point, WKTString.PROJ_CGCS2000_WKT);
			Console.WriteLine("{0}   {1}  {2}", point.X, point.Y, point.Z);
			IRenderPoint renderPoint = GviMap.MapControl.ObjectManager.CreateRenderPoint(point, simplePointSymbol, default(Guid));
			IGemetryExtension.ProjectEx(point, WKTString.PROJ_CGCS2000_WKT);
			IPoint point2 = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point2.SpatialCRS = spatialCRS2;
			point2.X = 607121.18;
			point2.Y = 4262001.62;
			point2.Z = 8.08;
			Console.WriteLine("平面 {0}   {1}  {2}", point2.X, point2.Y, point2.Z);
			IGemetryExtension.ProjectEx(point2, WKTString.PROJ_CGCS2000_WKT);
			Console.WriteLine("{0}   {1}  {2}", point2.X, point2.Y, point2.Z);
			IRenderPoint renderPoint2 = GviMap.MapControl.ObjectManager.CreateRenderPoint(point2, simplePointSymbol, default(Guid));
			IPoint point3 = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point3.SpatialCRS = spatialCRS2;
			point3.X = 607102.44;
			point3.Y = 4261999.51;
			point3.Z = 8.77;
			Console.WriteLine("平面 {0}   {1}  {2}", point3.X, point3.Y, point3.Z);
			IGemetryExtension.ProjectEx(point3, WKTString.PROJ_CGCS2000_WKT);
			Console.WriteLine("{0}   {1}  {2}", point3.X, point3.Y, point3.Z);
			IRenderPoint renderPoint3 = GviMap.MapControl.ObjectManager.CreateRenderPoint(point3, simplePointSymbol, default(Guid));
		}

		// Token: 0x04000024 RID: 36
		private IGeometryFactory _geoFactory;

		// Token: 0x04000025 RID: 37
		private DrawMutiSegmentTool _segmentTool;

		// Token: 0x04000026 RID: 38
		private Dictionary<IRowBuffer, IFeatureClass> _footPrintSearchResultForActual = null;

		// Token: 0x04000027 RID: 39
		private Dictionary<IRowBuffer, IFeatureClass> _footPrintSearchResultForUrban = null;

		// Token: 0x04000028 RID: 40
		private ICurveSymbol _beforeLineSymbol;

		// Token: 0x04000029 RID: 41
		private ICurveSymbol _afterLineSymbol;

		// Token: 0x0400002A RID: 42
		private ICurveSymbol _LineSymbol;

		// Token: 0x0400002B RID: 43
		private ITextSymbol _redText;

		// Token: 0x0400002C RID: 44
		private ISimplePointSymbol _redRenderPoint;

		// Token: 0x0400002D RID: 45
		private IRenderControl _renderControl;

		// Token: 0x0400002E RID: 46
		private ITextSymbol _greenText;

		// Token: 0x0400002F RID: 47
		private IPointSymbol _greenRenderPoint;

		// Token: 0x04000030 RID: 48
		private readonly Dictionary<string, IRenderGeometry> _targetRenderPointList = new Dictionary<string, IRenderGeometry>();

		// Token: 0x04000031 RID: 49
		private readonly List<IRObject> _renderObjectList = new List<IRObject>();

		// Token: 0x04000032 RID: 50
		private ISpatialCRS _wgs84CRS;

		// Token: 0x04000033 RID: 51
		private ISpatialCRS _localCRS;
	}
}
