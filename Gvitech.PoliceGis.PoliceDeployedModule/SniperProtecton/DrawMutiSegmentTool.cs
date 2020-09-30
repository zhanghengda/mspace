using System;
using System.Collections.Generic;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.PoliceDeployedModule.SniperProtecton
{
	// Token: 0x02000005 RID: 5
	public class DrawMutiSegmentTool
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000218C File Offset: 0x0000038C
		public DrawMutiSegmentTool(AxRenderControl renderControl, double beginOffsetZ)
		{
			this.IsStarted = false;
			try
			{
				this._beginOffsetZ = beginOffsetZ;
				bool flag = renderControl == null;
				if (!flag)
				{
					this._renderControl = renderControl;
					this._beginPointSymbol = new SimplePointSymbol();
					this._beginPointSymbol.FillColor = ColorConvert.UintToColor(4294967040u);
					this._beginPointSymbol.Size = 15;
					this._otherPointSymbol = new SimplePointSymbol();
					this._otherPointSymbol.Size = 10;
					this._groundLineSymbol = new CurveSymbol();
					this._groundLineSymbol.Color = ColorConvert.UintToColor(4294967040u);
					this._lookLineSymbol = new CurveSymbol();
					this._lookLineSymbol.Color = ColorConvert.UintToColor(4294967040u);
					ICRSFactory icrsfactory = new CRSFactory();
					this._localCrs = (icrsfactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS);
					this._wgs84Crs = GviMap.SpatialCrs;
				}
			}
			catch (Exception ex)
			{
				SystemLog.Log(ex);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022B0 File Offset: 0x000004B0
		public void Start()
		{
			bool flag = this._renderControl == null;
			if (!flag)
			{
				bool isStarted = this.IsStarted;
				if (!isStarted)
				{
					try
					{
						this.IsStarted = true;
						this._renderControl.InteractMode = gviInteractMode.gviInteractSelect;
						this._renderControl.MouseSelectObjectMask = (gviMouseSelectObjectMask)283;
						this._renderControl.MouseSelectMode = (gviMouseSelectMode)5;
						this._renderControl.RcLButtonDown -= this.ocx_RcLButtonDown;
						this._renderControl.RcLButtonUp -= this.ocx_RcLButtonUp;
						this._renderControl.RcMouseClickSelect -= this.AxRenderControl_RcMouseClickSelect;
						this._renderControl.RcRButtonUp -= this.AxRenderControl_RcRButtonUp;
						this._renderControl.RcLButtonDown += this.ocx_RcLButtonDown;
						this._renderControl.RcLButtonUp += this.ocx_RcLButtonUp;
						this._renderControl.RcMouseClickSelect += this.AxRenderControl_RcMouseClickSelect;
						this._renderControl.RcRButtonUp += this.AxRenderControl_RcRButtonUp;
						bool flag2 = this._geoFactory == null;
						if (flag2)
						{
							this._geoFactory = new GeometryFactory();
						}
						this.Release();
						IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
						point.SpatialCRS = this._wgs84Crs;
						this.SetPointOffSet(ref point, 0.1, 0.1, 0.1);
						this._startRenderPoint = this.AddRenderPoint(point, this._beginPointSymbol);
						this._startLabel = this.AddLabel(point, "案发点");
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002480 File Offset: 0x00000680
		public void Release()
		{
			bool flag = this._renderControl == null;
			if (!flag)
			{
				bool isStarted = this.IsStarted;
				if (!isStarted)
				{
					try
					{
						bool flag2 = this._startPoint != null;
						if (flag2)
						{
							FdeGeometryRelease.ReleaseComObject(this._startPoint);
							this._startPoint = null;
						}
						bool flag3 = this._startRenderPoint != null;
						if (flag3)
						{
							this._renderControl.ObjectManager.DeleteObject(this._startRenderPoint.Guid);
							this._startRenderPoint = null;
						}
						bool flag4 = this._startLabel != null;
						if (flag4)
						{
							this._renderControl.ObjectManager.DeleteObject(this._startLabel.Guid);
							this._startLabel = null;
						}
						bool flag5 = this._groundPolyline != null;
						if (flag5)
						{
							FdeGeometryRelease.ReleaseComObject(this._groundPolyline);
							this._groundPolyline = null;
						}
						bool flag6 = this._groundRenderPolyline != null;
						if (flag6)
						{
							this._renderControl.ObjectManager.DeleteObject(this._groundRenderPolyline.Guid);
							this._groundRenderPolyline = null;
						}
						bool flag7 = this._currentRenderPoint != null;
						if (flag7)
						{
							this._renderControl.ObjectManager.DeleteObject(this._currentRenderPoint.Guid);
							this._currentRenderPoint = null;
						}
						bool flag8 = this._currentRenderPolyline != null;
						if (flag8)
						{
							this._renderControl.ObjectManager.DeleteObject(this._currentRenderPolyline.Guid);
							this._currentRenderPolyline = null;
						}
						bool flag9 = this._currentLabel != null;
						if (flag9)
						{
							this._renderControl.ObjectManager.DeleteObject(this._currentLabel.Guid);
							this._currentLabel = null;
						}
						bool flag10 = this._segmentList.Count > 0;
						if (flag10)
						{
							foreach (IPolyline polyline in this._segmentList)
							{
								bool flag11 = polyline != null;
								if (flag11)
								{
									FdeGeometryRelease.ReleaseComObject(polyline);
								}
							}
							this._segmentList.Clear();
						}
						bool flag12 = this._renderObjectPolylineList.Count > 0;
						if (flag12)
						{
							foreach (IRObject irobject in this._renderObjectPolylineList)
							{
								bool flag13 = irobject != null;
								if (flag13)
								{
									this._renderControl.ObjectManager.DeleteObject(irobject.Guid);
								}
							}
							this._renderObjectPolylineList.Clear();
						}
						bool flag14 = this._renderObjectGroundLineList.Count > 0;
						if (flag14)
						{
							foreach (IRObject irobject2 in this._renderObjectGroundLineList)
							{
								bool flag15 = irobject2 != null;
								if (flag15)
								{
									this._renderControl.ObjectManager.DeleteObject(irobject2.Guid);
								}
							}
							this._renderObjectGroundLineList.Clear();
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002800 File Offset: 0x00000A00
		public void Stop()
		{
			bool flag = this._renderControl == null;
			if (!flag)
			{
				bool flag2 = !this.IsStarted;
				if (!flag2)
				{
					this.IsStarted = false;
					FdeGeometryRelease.ReleaseComObject(this._intersectPoint);
					this._intersectPoint = null;
					this.DeletePolyline();
					this.WatchPointindex = 0;
					this._renderControl.InteractMode = gviInteractMode.gviInteractNormal;
					this._renderControl.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
					this._renderControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
					this._renderControl.RcMouseClickSelect -= this.AxRenderControl_RcMouseClickSelect;
					this._renderControl.RcRButtonUp -= this.AxRenderControl_RcRButtonUp;
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028B4 File Offset: 0x00000AB4
		public void ResetOffsetZ(double offset, bool reDrawRenderLine)
		{
			try
			{
				bool flag = offset < 0.0;
				if (flag)
				{
					return;
				}
				bool isStarted = this.IsStarted;
				if (isStarted)
				{
					this._beginOffsetZ = offset;
					return;
				}
				bool flag2 = this._renderControl == null;
				if (flag2)
				{
					return;
				}
				bool flag3 = this._segmentList.Count <= 0;
				if (flag3)
				{
					return;
				}
				bool flag4 = this._startPoint == null;
				if (flag4)
				{
					return;
				}
				bool flag5 = this._renderObjectPolylineList.Count > 0;
				if (flag5)
				{
					foreach (IRObject irobject in this._renderObjectPolylineList)
					{
						bool flag6 = irobject != null;
						if (flag6)
						{
							this._renderControl.ObjectManager.DeleteObject(irobject.Guid);
						}
					}
					this._renderObjectPolylineList.Clear();
				}
				IPoint point = this._startPoint;
				point.Z -= this._beginOffsetZ;
				this._beginOffsetZ = offset;
				point = this._startPoint;
				point.Z += this._beginOffsetZ;
				this._startRenderPoint.SetFdeGeometry(this._startPoint);
				IPoint point2 = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
				point2.Position = IPointExtension.ToVector3(this._startPoint);
				this._startLabel.Position = point2;
				IPoint point3 = this._startPoint.Clone() as IPoint;
				point = point3;
				point.Z -= this._beginOffsetZ;
				this.DrawGroundLine(point3, this._startPoint);
				foreach (IPolyline polyline in this._segmentList)
				{
					polyline.UpdatePoint(0, this._startPoint);
				}
				if (reDrawRenderLine)
				{
					int num;
					for (int i = 0; i < this._segmentList.Count; i = num + 1)
					{
						IPolyline polyline2 = this._segmentList[i];
						this._renderObjectPolylineList.Add(this.AddRenderPoint(polyline2.EndPoint, this._otherPointSymbol));
						this._renderObjectPolylineList.Add(this.AddLabel(polyline2.EndPoint, "狙击点" + (i + 1)));
						this._renderObjectPolylineList.Add(this.AddRenderPolyline(polyline2, this._lookLineSymbol));
						num = i;
					}
				}
			}
			catch (Exception ex)
			{
			}
			try
			{
				bool flag7 = this.OnResetBeginPoint != null;
				if (flag7)
				{
					this.OnResetBeginPoint(this, this._segmentList);
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public void DeleteRenderPolyline()
		{
			try
			{
				bool flag = this._renderObjectPolylineList.Count <= 0;
				if (!flag)
				{
					foreach (IRObject irobject in this._renderObjectPolylineList)
					{
						bool flag2 = irobject != null;
						if (flag2)
						{
							this._renderControl.ObjectManager.DeleteObject(irobject.Guid);
						}
					}
					this._renderObjectPolylineList.Clear();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public IMultiPolyline GetSegmentASMutiPolyline()
		{
			bool flag = this._geoFactory == null;
			IMultiPolyline result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					IMultiPolyline multiPolyline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolyline, gviVertexAttribute.gviVertexAttributeNone) as IMultiPolyline;
					multiPolyline.SpatialCRS = this._wgs84Crs;
					foreach (IPolyline polyline in this._segmentList)
					{
						multiPolyline.AddPolyline(polyline.Clone2(gviVertexAttribute.gviVertexAttributeNone) as IPolyline);
					}
					result = multiPolyline;
				}
				catch (Exception ex)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D58 File Offset: 0x00000F58
		public List<IPoint> GetTargetPoints()
		{
			bool flag = this._segmentList == null || this._segmentList.Count <= 0;
			List<IPoint> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<IPoint> list = new List<IPoint>();
				foreach (IPolyline polyline in this._segmentList)
				{
					list.Add(polyline.EndPoint);
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public bool DrawSegment(IPoint startPoint, List<IPoint> targetPoints, double offsetZ)
		{
			this.Release();
			bool flag = startPoint == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = targetPoints == null || targetPoints.Count <= 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this._beginOffsetZ = offsetZ;
					this._startPoint = (startPoint.Clone() as IPoint);
					IPoint startPoint2 = this._startPoint;
					startPoint2.Z += offsetZ;
					this.DrawGroundLine(startPoint, this._startPoint);
					this._startRenderPoint = this.AddRenderPoint(this._startPoint, this._beginPointSymbol);
					this._startLabel = this.AddLabel(this._startPoint, "案发点");
					foreach (IPoint point in targetPoints)
					{
						IPolyline polyline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
						polyline.SpatialCRS = this._wgs84Crs;
						polyline.StartPoint = this._startPoint;
						polyline.EndPoint = point;
						this._segmentList.Add(polyline);
						IRenderPolyline item = this.AddRenderPolyline(polyline, this._lookLineSymbol);
						this._renderObjectPolylineList.Add(item);
						ILabel item2 = this.AddLabel(point, "狙击点" + (this._segmentList.Count + 1));
						this._renderObjectPolylineList.Add(item2);
						IRenderPoint item3 = this.AddRenderPoint(point, this._otherPointSymbol);
						this._renderObjectPolylineList.Add(item3);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F9C File Offset: 0x0000119C
		private bool ocx_RcLButtonUp(uint Flags, int X, int Y)
		{
			this._timeUp = DateTime.Now;
			bool flag = (this._timeUp - this._timeDown).TotalMilliseconds > 1000.0;
			if (flag)
			{
				this.Stop();
			}
			else
			{
				bool flag2 = this._intersectPoint == null;
				if (flag2)
				{
					return false;
				}
				IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
				point.SpatialCRS = this._wgs84Crs;
				point.X = this._intersectPoint.X;
				point.Y = this._intersectPoint.Y;
				point.Z = this._intersectPoint.Z;
				bool flag3 = this._startPoint == null;
				if (flag3)
				{
					IPoint point2 = point;
					point2.Z += this._beginOffsetZ;
					this._startPoint = point;
					bool flag4 = this._startRenderPoint != null;
					if (flag4)
					{
						this._startRenderPoint.SetFdeGeometry(this._startPoint);
					}
					IPoint point3 = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
					point3.SpatialCRS = this._wgs84Crs;
					point3.Position = IPointExtension.ToVector3(this._startPoint);
					IPoint point4 = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
					point4.SpatialCRS = this._wgs84Crs;
					IPoint point5 = point.Clone() as IPoint;
					this.SetPointOffSet(ref point5, 0.0, 0.0, -this._beginOffsetZ);
					point4.X = point5.X;
					point4.Y = point5.Y;
					point4.Z = point5.Z;
					this.DrawGroundLine(this._startPoint, point4);
					this._currentRenderPoint = this.AddRenderPoint(this._startPoint, this._otherPointSymbol);
					this._currentLabel = this.AddLabel(this._startPoint, string.Format("{0}{1}", "狙击点", this.WatchPointindex + 1));
					this._currentPolyLine = (this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline);
					this._currentPolyLine.SpatialCRS = this._wgs84Crs;
					this._currentPolyLine.StartPoint = this._startPoint;
					IPoint endPoint = this._startPoint.Clone() as IPoint;
					this.SetPointOffSet(ref endPoint, 0.1, 0.1, 0.1);
					this._currentPolyLine.EndPoint = endPoint;
					this._currentRenderPolyline = this.AddRenderPolyline(this._currentPolyLine, this._lookLineSymbol);
				}
				else
				{
					this._currentPolyLine = (this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline);
					this._currentPolyLine.SpatialCRS = this._wgs84Crs;
					this._currentPolyLine.StartPoint = this._startPoint;
					IPoint startPoint = point.Clone() as IPoint;
					this.SetPointOffSet(ref point, 0.0, 0.0, this._beginOffsetZ);
					this._currentPolyLine.EndPoint = point;
					this.DrawGroundLineEx(startPoint, point);
					this._currentRenderPoint.SetFdeGeometry(point);
					this._currentRenderPolyline.SetFdeGeometry(this._currentPolyLine);
					this._segmentList.Add(this._currentPolyLine.Clone() as IPolyline);
					this._renderObjectPolylineList.Add(this._currentRenderPolyline);
					this._renderObjectPolylineList.Add(this._currentLabel);
					this._renderObjectPolylineList.Add(this._currentRenderPoint);
					this._currentRenderPoint = this.AddRenderPoint(point, this._otherPointSymbol);
					this._currentRenderPolyline = this.AddRenderPolyline(this._currentPolyLine, this._lookLineSymbol);
					int watchPointindex = this.WatchPointindex;
					this.WatchPointindex = watchPointindex + 1;
					this._currentLabel = this.AddLabel(point, string.Format("{0}{1}", "狙击点", this.WatchPointindex + 1));
					bool flag5 = this.OnFinishDraw != null;
					if (flag5)
					{
						this.OnFinishDraw(this, this._segmentList);
					}
				}
			}
			return false;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000033C8 File Offset: 0x000015C8
		private bool ocx_RcLButtonDown(uint Flags, int X, int Y)
		{
			this._timeDown = DateTime.Now;
			this._tempX = (double)X;
			this._tempY = (double)Y;
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003400 File Offset: 0x00001600
		private void ProjectLocalCrs(IGeometry geo)
		{
			geo.SpatialCRS = this._wgs84Crs;
			geo.Project(this._localCrs);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000341D File Offset: 0x0000161D
		private void ProjectWgs84(IGeometry geo)
		{
			geo.SpatialCRS = this._localCrs;
			geo.Project(this._wgs84Crs);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000343C File Offset: 0x0000163C
		public void SetPointOffSet(ref IPoint pt, double offX = 0.0, double offY = 0.0, double offZ = 0.0)
		{
			this.ProjectLocalCrs(pt);
			IPoint point = pt;
			point.X += offX;
			point = pt;
			point.Y += offY;
			point = pt;
			point.Z += offZ;
			this.ProjectWgs84(pt);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003494 File Offset: 0x00001694
		private void AxRenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
		{
			bool flag = IntersectPoint == null;
			if (!flag)
			{
				bool flag2 = this._geoFactory == null;
				if (!flag2)
				{
					this._intersectPoint = (IPoint)IntersectPoint.Clone();
					bool flag3 = EventSender == gviMouseSelectMode.gviMouseSelectMove;
					if (flag3)
					{
						try
						{
							IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
							point.SpatialCRS = this._wgs84Crs;
							bool flag4 = this._startPoint == null;
							if (flag4)
							{
								point.X = IntersectPoint.X;
								point.Y = IntersectPoint.Y;
								point.Z = IntersectPoint.Z;
								bool flag5 = this._startRenderPoint != null;
								if (flag5)
								{
									this._startRenderPoint.SetFdeGeometry(point);
								}
								IPoint point2 = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
								point2.SpatialCRS = this._wgs84Crs;
								point2.Position = IPointExtension.ToVector3(point);
								bool flag6 = this._startLabel != null;
								if (flag6)
								{
									this._startLabel.Position = point2;
								}
							}
							bool flag7 = this._currentPolyLine != null && this._currentRenderPolyline != null;
							if (flag7)
							{
								point.X = IntersectPoint.X;
								point.Y = IntersectPoint.Y;
								point.Z = IntersectPoint.Z;
								IPoint endPoint = this._currentPolyLine.EndPoint;
								this._currentPolyLine.EndPoint = point;
								bool flag8 = endPoint != null;
								if (flag8)
								{
									FdeGeometryRelease.ReleaseComObject(endPoint);
								}
								this._currentRenderPolyline.SetFdeGeometry(this._currentPolyLine);
								this._currentRenderPoint.SetFdeGeometry(point);
								IVector3 vector = new Vector3();
								vector.X = point.X;
								vector.Y = point.Y;
								vector.Z = point.Z;
								IPoint point3 = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
								point3.SpatialCRS = this._wgs84Crs;
								point3.Position = vector;
								this._currentLabel.Position = point3;
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000036E8 File Offset: 0x000018E8
		private bool AxRenderControl_RcRButtonUp(uint Flags, int X, int Y)
		{
			try
			{
				this.DeletePolyline();
			}
			catch (Exception ex)
			{
			}
			try
			{
				bool flag = this.OnFinishDraw != null;
				if (flag)
				{
					this.OnFinishDraw(this, this._segmentList);
				}
			}
			catch (Exception ex2)
			{
			}
			this.Stop();
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000375C File Offset: 0x0000195C
		private void DeletePolyline()
		{
			bool flag = this._currentPolyLine != null;
			if (flag)
			{
				FdeGeometryRelease.ReleaseComObject(this._currentPolyLine);
				this._currentPolyLine = null;
			}
			bool flag2 = this._currentRenderPoint != null;
			if (flag2)
			{
				this._renderControl.ObjectManager.DeleteObject(this._currentRenderPoint.Guid);
				this._currentRenderPoint = null;
			}
			bool flag3 = this._currentRenderPolyline != null;
			if (flag3)
			{
				this._renderControl.ObjectManager.DeleteObject(this._currentRenderPolyline.Guid);
				this._currentRenderPolyline = null;
			}
			bool flag4 = this._currentLabel != null;
			if (flag4)
			{
				this._renderControl.ObjectManager.DeleteObject(this._currentLabel.Guid);
				this._currentLabel = null;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003824 File Offset: 0x00001A24
		private void DrawGroundLine(IPoint startPoint, IPoint endPoint)
		{
			bool flag = this._groundPolyline == null;
			if (flag)
			{
				this._groundPolyline = (this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline);
				this._groundPolyline.SpatialCRS = this._wgs84Crs;
			}
			this._groundPolyline.StartPoint = startPoint;
			this._groundPolyline.EndPoint = endPoint;
			bool flag2 = this._groundRenderPolyline == null;
			if (flag2)
			{
				this._groundRenderPolyline = this.AddRenderPolyline(this._groundPolyline, this._groundLineSymbol);
			}
			else
			{
				this._groundRenderPolyline.SetFdeGeometry(this._groundPolyline);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000038C4 File Offset: 0x00001AC4
		private void DrawGroundLineEx(IPoint startPoint, IPoint endPoint)
		{
			IPolyline polyline = this._geoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ ) as IPolyline;
			polyline.SpatialCRS = this._wgs84Crs;
			polyline.StartPoint = startPoint;
			polyline.EndPoint = endPoint;
			this._renderObjectGroundLineList.Add(this.AddRenderPolyline(polyline, this._groundLineSymbol));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000391C File Offset: 0x00001B1C
		private ILabel AddLabel(IPoint point, string labelString)
		{
			ILabel label = this.DrawLabel(point.X, point.Y, point.Z + 0.1, labelString);
			label.VisibleMask = gviViewportMask.gviViewAllNormalView;
			label.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			label.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			label.MouseSelectMask = gviViewportMask.gviViewNone;
			label.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
			return label;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003988 File Offset: 0x00001B88
		public ILabel DrawLabel(double posX, double posY, double posZ, string msg)
		{
			ILabel label = this._renderControl.ObjectManager.CreateLabel(Guid.Empty);
			label.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
			label.Text = msg;
			label.TextSymbol = new TextSymbol
			{
				TextAttribute = 
				{
					TextColor = ColorConvert.UintToColor(4294967040u)
				},
				PivotAlignment = gviPivotAlignment.gviPivotAlignTopCenter
			};
			IVector3 vector = new Vector3();
			vector.X = posX;
			vector.Y = posY;
			vector.Z = posZ;
			IPoint point = this._geoFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ );
			point.SpatialCRS = this._wgs84Crs;
			point.Position = vector;
			label.Position = point;
			label.VisibleMask = gviViewportMask.gviViewAllNormalView;
			return label;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003A3C File Offset: 0x00001C3C
		public ILabel DrawLabel(IPoint gpnt, string msg)
		{
			ILabel label = this._renderControl.ObjectManager.CreateLabel( null);
			label.Text = msg;
			label.TextSymbol = new TextSymbol
			{
				TextAttribute = 
				{
					TextColor =  ColorConvert.UintToColor(4294967040u)
				}
			};
			label.Position = gpnt;
			return label;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003A90 File Offset: 0x00001C90
		private IRenderPoint AddRenderPoint(IPoint point, IPointSymbol pointSymbol)
		{
			point.SpatialCRS = this._wgs84Crs;
			IRenderPoint renderPoint = this._renderControl.ObjectManager.CreateRenderPoint(point, pointSymbol, Guid.Empty);
			renderPoint.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			renderPoint.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			renderPoint.MouseSelectMask = gviViewportMask.gviViewNone;
			renderPoint.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
			return renderPoint;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003AF4 File Offset: 0x00001CF4
		private IRenderPolyline AddRenderPolyline(IPolyline polyline, ICurveSymbol lineSymbol)
		{
			polyline.SpatialCRS = this._wgs84Crs;
			IRenderPolyline renderPolyline = this._renderControl.ObjectManager.CreateRenderPolyline(polyline, lineSymbol, Guid.Empty);
			renderPolyline.MaxVisibleDistance = AnalysisConstance.RO_MaxObserveDistance;
			renderPolyline.MinVisiblePixels = (float)AnalysisConstance.RO_MinObserveDistance;
			renderPolyline.MouseSelectMask = gviViewportMask.gviViewNone;
			renderPolyline.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
			return renderPolyline;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003B58 File Offset: 0x00001D58
		public IPoint StartPoint
		{
			get
			{
				return this._startPoint;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003B70 File Offset: 0x00001D70
		public List<IPolyline> SegmentList
		{
			get
			{
				return this._segmentList;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003B88 File Offset: 0x00001D88
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00003B90 File Offset: 0x00001D90
		public bool IsStarted { get; set; }

		// Token: 0x04000003 RID: 3
		private double _tempX;

		// Token: 0x04000004 RID: 4
		private double _tempY;

		// Token: 0x04000005 RID: 5
		private DateTime _timeDown;

		// Token: 0x04000006 RID: 6
		private DateTime _timeUp;

		// Token: 0x04000007 RID: 7
		private IPoint _intersectPoint;

		// Token: 0x04000008 RID: 8
		private readonly AxRenderControl _renderControl;

		// Token: 0x04000009 RID: 9
		private IPoint _startPoint;

		// Token: 0x0400000A RID: 10
		public int WatchPointindex = 0;

		// Token: 0x0400000B RID: 11
		private IRenderPoint _startRenderPoint;

		// Token: 0x0400000C RID: 12
		private ILabel _startLabel;

		// Token: 0x0400000D RID: 13
		private readonly ICurveSymbol _groundLineSymbol;

		// Token: 0x0400000E RID: 14
		private IPolyline _groundPolyline;

		// Token: 0x0400000F RID: 15
		private IRenderPolyline _groundRenderPolyline;

		// Token: 0x04000010 RID: 16
		private IPolyline _currentPolyLine;

		// Token: 0x04000011 RID: 17
		private IRenderPolyline _currentRenderPolyline;

		// Token: 0x04000012 RID: 18
		private IRenderPoint _currentRenderPoint;

		// Token: 0x04000013 RID: 19
		private ILabel _currentLabel;

		// Token: 0x04000014 RID: 20
		private readonly List<IPolyline> _segmentList = new List<IPolyline>();

		// Token: 0x04000015 RID: 21
		private readonly List<IRObject> _renderObjectPolylineList = new List<IRObject>();

		// Token: 0x04000016 RID: 22
		private readonly List<IRObject> _renderObjectGroundLineList = new List<IRObject>();

		// Token: 0x04000017 RID: 23
		private readonly ISimplePointSymbol _beginPointSymbol;

		// Token: 0x04000018 RID: 24
		private readonly IPointSymbol _otherPointSymbol;

		// Token: 0x04000019 RID: 25
		private readonly ICurveSymbol _lookLineSymbol;

		// Token: 0x0400001A RID: 26
		private readonly ISpatialCRS _wgs84Crs;

		// Token: 0x0400001B RID: 27
		private readonly ISpatialCRS _localCrs;

		// Token: 0x0400001C RID: 28
		private double _beginOffsetZ;

		// Token: 0x0400001D RID: 29
		private IGeometryFactory _geoFactory;

		// Token: 0x0400001F RID: 31
		public FinishDrawEventHandler OnFinishDraw;

		// Token: 0x04000020 RID: 32
		public ResetBeginPointEventHandler OnResetBeginPoint;
	}
}
