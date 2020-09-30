using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;

namespace Gvitech.CityMaker.Models
{
	public class MarkLine
	{
		private static readonly ICurveSymbol LineSymbol = new CurveSymbol
		{
			Color = ColorConvert.UintToColor(4289593135u),
			Width = 0.1f
		};

		private static readonly IPointSymbol PointSymbol = new SimplePointSymbol
		{
			FillColor = ColorConvert.UintToColor(4294967040u),
			Size = 10
		};

		private static readonly ITextSymbol TextSymbol = new TextSymbol
		{
			DrawLine = false,
			PivotAlignment = gviPivotAlignment.gviPivotAlignTopLeft,
			TextAttribute = new TextAttribute
			{
				TextColor = ColorConvert.UintToColor(4278222848u),
				OutlineColor = ColorConvert.UintToColor(65280u),
				Font = "Î¢ÈíÑÅºÚ",
				TextSize = 13
			}
		};

		private static readonly IGeometryFactory GeoFactory = new GeometryFactory();

		public IRenderPoint StartPoint
		{
			get;
			set;
		}

		public IRenderPoint EndPoint
		{
			get;
			set;
		}

		public IRenderPolyline Route
		{
			get;
			set;
		}

		public ILabel Text
		{
			get;
			set;
		}

		public ILabel StartMarkText
		{
			get;
			set;
		}

		public ILabel EndMarkText
		{
			get;
			set;
		}

		private MarkLine()
		{
		}

		private MarkLine(IRenderPoint startPoint, IRenderPoint endPoint, IRenderPolyline route, ILabel text, ILabel startText = null, ILabel endText = null)
		{
			this.StartPoint = startPoint;
			this.EndPoint = endPoint;
			this.Route = route;
			this.Text = text;
			this.StartMarkText = startText;
			this.EndMarkText = endText;
		}

		public static MarkLine CreateEmptyMarkLine()
		{
			return new MarkLine();
		}

		public static MarkLine CreateMarkLine(IObjectManager omg, IPoint startPoint, IPoint endPoint, string text = null, string startText = null, string endText = null, IPointSymbol pointSymbol = null, ICurveSymbol lineSymbol = null, ITextSymbol textSymbol = null)
		{
			bool flag = omg == null || startPoint == null || endPoint == null;
			MarkLine result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IPolyline polyline = MarkLine.GeoFactory.CreatePolyline(new List<IPoint>
				{
					startPoint,
					endPoint
				}, null);
				IRenderPoint startPoint2 = omg.CreateRenderPoint(startPoint, pointSymbol ?? MarkLine.PointSymbol);
				IRenderPoint endPoint2 = omg.CreateRenderPoint(endPoint, pointSymbol ?? MarkLine.PointSymbol);
				IRenderPolyline route = omg.CreateRenderPolyline(polyline, lineSymbol ?? MarkLine.LineSymbol);
				ILabel startText2 = string.IsNullOrEmpty(startText) ? null : MarkLine.CreateLabel(omg, startPoint, startText, textSymbol);
				ILabel endText2 = string.IsNullOrEmpty(endText) ? null : MarkLine.CreateLabel(omg, endPoint, endText, textSymbol);
				ILabel text2 = null;
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					IPoint point = MarkLine.GeoFactory.CreatePoint((startPoint.X + endPoint.X) / 2.0, (startPoint.Y + endPoint.Y) / 2.0, (startPoint.Z + endPoint.Z) / 2.0, null);
					text2 = MarkLine.CreateLabel(omg, point, text, textSymbol);
					point.ReleaseComObject();
				}
				polyline.ReleaseComObject();
				result = new MarkLine(startPoint2, endPoint2, route, text2, startText2, endText2);
			}
			return result;
		}

		public MarkLine AddStartPoint(IObjectManager omg, IPoint startPoint, IPointSymbol pointSymbol = null, string startText = null, ITextSymbol textSymbol = null)
		{
			bool flag = omg == null || startPoint == null || this.StartPoint != null;
			MarkLine result;
			if (flag)
			{
				result = this;
			}
			else
			{
				this.StartPoint = omg.CreateRenderPoint(startPoint, pointSymbol ?? MarkLine.PointSymbol);
				this.StartMarkText = (string.IsNullOrEmpty(startText) ? null : MarkLine.CreateLabel(omg, startPoint, startText, textSymbol));
				result = this;
			}
			return result;
		}

		public MarkLine AddEndPoint(IObjectManager omg, IPoint endPoint, IPointSymbol pointSymbol = null, string sendText = null, ITextSymbol textSymbol = null)
		{
			bool flag = omg == null || endPoint == null || this.StartPoint == null || this.EndPoint != null;
			MarkLine result;
			if (flag)
			{
				result = this;
			}
			else
			{
				this.EndPoint = omg.CreateRenderPoint(endPoint, pointSymbol ?? MarkLine.PointSymbol);
				this.EndMarkText = (string.IsNullOrEmpty(sendText) ? null : MarkLine.CreateLabel(omg, endPoint, sendText, textSymbol));
				result = this;
			}
			return result;
		}

		public MarkLine CreateLine(IObjectManager omg, string text = null, ICurveSymbol lineSymbol = null, ITextSymbol textSymbol = null)
		{
			bool flag = omg == null || this.StartPoint == null || this.EndPoint == null;
			MarkLine result;
			if (flag)
			{
				result = this;
			}
			else
			{
				IPoint point = (IPoint)this.StartPoint.GetFdeGeometry();
				IPoint point2 = (IPoint)this.EndPoint.GetFdeGeometry();
				IPolyline polyline = MarkLine.GeoFactory.CreatePolyline(new List<IPoint>
				{
					point,
					point2
				}, null);
				this.Route = omg.CreateRenderPolyline(polyline, lineSymbol ?? MarkLine.LineSymbol);
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					IPoint point3 = MarkLine.GeoFactory.CreatePoint((point.X + point2.X) / 2.0, (point.Y + point2.Y) / 2.0, (point.Z + point2.Z) / 2.0, null);
					this.Text = MarkLine.CreateLabel(omg, point3, text, textSymbol);
					point3.ReleaseComObject();
				}
				point.ReleaseComObject();
				point2.ReleaseComObject();
				polyline.ReleaseComObject();
				result = this;
			}
			return result;
		}

		public MarkLine UpData(IPoint startPoint, IPoint endPoint, string text = null, string startText = null, string endText = null)
		{
			bool flag = startPoint == null || endPoint == null;
			MarkLine result;
			if (flag)
			{
				result = this;
			}
			else
			{
				bool flag2 = this.StartPoint != null;
				if (flag2)
				{
					this.StartPoint.SetFdeGeometry(startPoint);
				}
				bool flag3 = this.EndPoint != null;
				if (flag3)
				{
					this.EndPoint.SetFdeGeometry(endPoint);
				}
				bool flag4 = this.StartMarkText != null;
				if (flag4)
				{
					this.StartMarkText.SetPosition(startPoint);
					this.StartMarkText.Text = startText;
				}
				bool flag5 = this.EndMarkText != null;
				if (flag5)
				{
					this.EndMarkText.SetPosition(endPoint);
					this.EndMarkText.Text = endText;
				}
				bool flag6 = this.Text != null;
				if (flag6)
				{
					this.Text.Text = text;
				}
				this.UpdateRoute(startPoint, endPoint);
				result = this;
			}
			return result;
		}

		public MarkLine UpdataEndPoint(IPoint endPoint)
		{
			bool flag = this.EndPoint != null;
			if (flag)
			{
				this.EndPoint.SetFdeGeometry(endPoint);
			}
			bool flag2 = this.EndMarkText != null;
			if (flag2)
			{
				this.EndMarkText.SetPosition(endPoint);
			}
			this.UpdateRoute(null, endPoint);
			return this;
		}

		public void Release(IObjectManager omg)
		{
			bool flag = omg == null;
			if (!flag)
			{
				omg.ReleaseRenderObject(new IRenderable[]
				{
					this.StartPoint,
					this.EndPoint,
					this.Route,
					this.Text,
					this.StartMarkText,
					this.EndMarkText
				});
			}
		}

		public MarkLine ChangeLineSymbol(ICurveSymbol lineSymbol)
		{
			bool flag = this.Route != null;
			if (flag)
			{
				this.Route.Symbol = lineSymbol;
			}
			return this;
		}

		public MarkLine ChangePointSymbol(IPointSymbol pointSymbol)
		{
			bool flag = this.StartPoint != null;
			if (flag)
			{
				this.StartPoint.Symbol = pointSymbol;
			}
			bool flag2 = this.EndPoint != null;
			if (flag2)
			{
				this.EndPoint.Symbol = pointSymbol;
			}
			return this;
		}

		public MarkLine ChangeTextSymbol(ITextSymbol textSymbol)
		{
			bool flag = this.Text != null;
			if (flag)
			{
				this.Text.TextSymbol = textSymbol;
			}
			return this;
		}

		public MarkLine SetVisible(bool isVisible = true, gviViewportMask gvm = gviViewportMask.gviViewAllNormalView)
		{
			gvm = (isVisible ? gvm : gviViewportMask.gviViewNone);
			bool flag = this.StartPoint != null;
			if (flag)
			{
				this.StartPoint.VisibleMask = gvm;
			}
			bool flag2 = this.EndPoint != null;
			if (flag2)
			{
				this.EndPoint.VisibleMask = gvm;
			}
			bool flag3 = this.Route != null;
			if (flag3)
			{
				this.Route.VisibleMask = gvm;
			}
			bool flag4 = this.Text != null;
			if (flag4)
			{
				this.Text.VisibleMask = gvm;
			}
			bool flag5 = this.StartMarkText != null;
			if (flag5)
			{
				this.StartMarkText.VisibleMask = gvm;
			}
			bool flag6 = this.EndMarkText != null;
			if (flag6)
			{
				this.EndMarkText.VisibleMask = gvm;
			}
			return this;
		}

		private static ILabel CreateLabel(IObjectManager omg, IPoint position, string text = null, ITextSymbol textSymbol = null)
		{
			ILabel label = omg.CreateLabel(position);
			label.TextSymbol = (textSymbol ?? MarkLine.TextSymbol);
			label.Text = text;
			return label;
		}

		private void UpdateRoute(IPoint startPoint, IPoint endPoint)
		{
			bool flag = startPoint == null && endPoint == null;
			if (!flag)
			{
				bool flag2 = this.Route != null;
				if (flag2)
				{
					IPolyline polyline = (IPolyline)this.Route.GetFdeGeometry();
					bool flag3 = startPoint != null;
					if (flag3)
					{
						polyline.UpdatePoint(0, startPoint);
					}
					bool flag4 = endPoint != null;
					if (flag4)
					{
						polyline.UpdatePoint(1, endPoint);
					}
					this.Route.SetFdeGeometry(polyline);
					bool flag5 = this.Text != null;
					if (flag5)
					{
						IPoint midpoint = polyline.GetSegment(0).Midpoint;
						this.Text.SetPosition(midpoint);
						polyline.ReleaseComObject();
						midpoint.ReleaseComObject();
					}
				}
			}
		}
	}
}
