using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Framework.Wpf.Core;
using Mmc.MathUtil;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class ProjectAreaCmd : MapCommand
    {
        private bool AxRenderControl_RcLButtonDown(uint Flags, int X, int Y)
        {
            GviMap.Camera.ScreenToWorld(X, Y, out this.samePoint);
            bool flag = this.samePoint == null;
            return flag && false;
        }

        private bool IsDownAndUpSame(IPoint down, IPoint up)
        {
            bool flag = down == null;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                bool flag2 = up == null;
                if (flag2)
                {
                    result = true;
                }
                else
                {
                    bool flag3 = down.X == up.X && down.Y == up.Y && down.Z == up.Z;
                    result = flag3;
                }
            }
            return result;
        }

        private bool AxRenderControl_RcLButtonUp(uint Flags, int X, int Y)
        {
            IPoint point = null;
            GviMap.Camera.ScreenToWorld(X, Y, out point);
            bool flag = point == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.IsDownAndUpSame(this.samePoint, point);
                if (flag2)
                {
                    bool flag3 = this._renderPolygon3D == null;
                    if (flag3)
                    {
                        IPoint point2 = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point2.SpatialCRS = GviMap.SpatialCrs;
                        point2.SetCoords(point.X, point.Y, point.Z, 0.0, 0);
                        this._polygon = (this._gFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon);
                        _polygon.SpatialCRS = GviMap.SpatialCrs;
                        this._polygon.ExteriorRing.AppendPoint(point2);
                        this._renderPolygon3D = GviMap.ObjectManager.CreateRenderPolygon(this._polygon, this.surfaceSym);
                        this._renderPolygon3D.MaxVisibleDistance = 1000000.0;
                        this._renderPolygon3D.MinVisiblePixels = 0f;
                        IRenderPoint renderPoint = GviMap.ObjectManager.CreateRenderPoint(point2, this._beginPointSymbol);
                        renderPoint.MaxVisibleDistance = 1000000.0;
                        renderPoint.MinVisiblePixels = 0f;
                        this._listRenderPoints.Add(renderPoint);
                    }
                    else
                    {
                        IPoint point3 = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point3.SpatialCRS = GviMap.SpatialCrs;
                        point3.SetCoords(point.X, point.Y, point.Z, 0.0, 0);
                        IRenderPoint renderPoint2 = GviMap.ObjectManager.CreateRenderPoint(point3, this._beginPointSymbol);
                        renderPoint2.MaxVisibleDistance = 1000000.0;
                        renderPoint2.MinVisiblePixels = 0f;
                        this._listRenderPoints.Add(renderPoint2);
                        IPoint point4 = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                        point4.SpatialCRS = GviMap.SpatialCrs;
                        point4.SetCoords(point.X, point.Y, point.Z, 0.0, 0);
                        this._polygon.ExteriorRing.AppendPoint(point4);
                        bool flag4 = this._polygon.ExteriorRing.PointCount == 3;
                        if (flag4)
                        {
                            IPoint point5 = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                            point5.SpatialCRS = GviMap.SpatialCrs;
                            point5.SetCoords(this._polygon.ExteriorRing.StartPoint.X, this._polygon.ExteriorRing.StartPoint.Y, this._polygon.ExteriorRing.StartPoint.Z, 0.0, 0);
                            this._polygon.ExteriorRing.AppendPoint(point5);
                        }
                        else
                        {
                            bool flag5 = this._polygon.ExteriorRing.PointCount > 3;
                            if (flag5)
                            {
                                this._polygon.ExteriorRing.RemovePoints(this._polygon.ExteriorRing.PointCount - 2, 1);
                                IPoint point6 = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                                point6.SpatialCRS = GviMap.SpatialCrs;
                                point6.SetCoords(this._polygon.ExteriorRing.StartPoint.X, this._polygon.ExteriorRing.StartPoint.Y, this._polygon.ExteriorRing.StartPoint.Z, 0.0, 0);
                                this._polygon.ExteriorRing.AppendPoint(point6);
                            }
                        }
                        //this._polygon.Project(GviMap.CrsFactory.CreateFromWKT(WKTString.PROJ_CGCS2000_WKT) as ISpatialCRS);
                        this._renderPolygon3D.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                        this._renderPolygon3D.SetFdeGeometry(this._polygon);
                        bool flag6 = this._polygon.ExteriorRing.PointCount > 3;
                        if (flag6)
                        {
                            bool flag7 = !this._polygon.IsClosed;
                            if (flag7)
                            {
                                this._polygon.Close();
                            }
                            IPolygon polygon = this._polygon.Clone() as IPolygon;
                            ITopologicalOperator2D topologicalOperator2D = polygon as ITopologicalOperator2D;
                            bool flag8 = !topologicalOperator2D.IsSimple2D();
                            if (flag8)
                            {
                                return false;
                            }
                            var areaPoly = this._polygon.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                            var pt = areaPoly.ExteriorRing.GetPoint(0);
                            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
                            if (!string.IsNullOrEmpty(prjWkt))
                                areaPoly.ProjectEx(prjWkt);
                            double num = this.CalcProjectedArea(areaPoly);
                            string area = string.Format(Helpers.ResourceHelper.FindKey("Squaremeters"), Math.Round(num, 3));
                            this.DrawLabel(area);
                        }
                    }
                }
                result = false;
            }
            return result;
        }

        private bool AxRenderControl_RcRButtonUp(uint Flags, int X, int Y)
        {
            this.ReleaseRenderObj();
            return false;
        }

        private void ReleaseRenderObj()
        {
            this.ReleaseRenderPolygon();
            bool flag = this._iLabel != null;
            if (flag)
            {
                GviMap.ObjectManager.DeleteObject(this._iLabel.Guid);
                this._iLabel = null;
            }
            bool flag2 = this._listRenderPoints != null && this._listRenderPoints.Count > 0;
            if (flag2)
            {
                foreach (IRenderPoint renderPoint in this._listRenderPoints)
                {
                    GviMap.ObjectManager.DeleteObject(renderPoint.Guid);
                }
                this._listRenderPoints.Clear();
            }
        }

        private void DrawLabel(string area)
        {
            this.DeleteLabel();
            bool flag = this._polygon != null;
            if (flag)
            {
                IVector3 vector = new Vector3();
                bool flag2 = this._polygon.Centroid != null;
                if (flag2)
                {
                    IPoint centroid = this._polygon.Centroid;
                    vector.X = centroid.X;
                    vector.Y = centroid.Y;
                    vector.Z = centroid.Z;
                }
                else
                {
                    vector = this._polygon.Envelope.Center;
                }
                this._iLabel = this.DrawLabel(area, vector);
                this._iLabel.MaxVisibleDistance = 1000000.0;
                this._iLabel.MinVisiblePixels = 0f;
            }
        }

        private ILabel DrawLabel(string title, IVector3 position)
        {
            ILabel label = null;
            ILabel result;
            try
            {
                bool flag = position == null;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    label = GviMap.ObjectManager.CreateLabel(null);
                    label.Text = title;
                    TextAttribute textAttribute = new TextAttribute();
                    textAttribute.TextColor = ColorConvert.UintToColor(4294967040u);
                    textAttribute.TextSize = 20;
                    label.TextSymbol = new TextSymbol
                    {
                        TextAttribute = textAttribute
                    };
                    IPoint point = this._gFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
                    point.SetCoords(position.X, position.Y, position.Z, 0.0, 0);
                    label.Position = point;
                    result = label;
                }
            }
            catch (Exception ex)
            {
                bool flag2 = label != null;
                if (flag2)
                {
                    GviMap.ObjectManager.DeleteObject(label.Guid);
                }
                result = null;
            }
            return result;
        }

        private void DeleteLabel()
        {
            bool flag = this._iLabel != null;
            if (flag)
            {
                GviMap.ObjectManager.DeleteObject(this._iLabel.Guid);
                this._iLabel = null;
            }
        }

        private double CalcProjectedArea(IPolygon polygon3D)
        {
            IGeometryFactory geometryFactory = new GeometryFactory();
            IPolygon polygon = geometryFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            IPoint point = geometryFactory.CreatePoint(gviVertexAttribute.gviVertexAttributeZ);
            IRing exteriorRing = polygon.ExteriorRing;
            IRing exteriorRing2 = polygon3D.ExteriorRing;
            int num;
            for (int i = 0; i < exteriorRing2.PointCount; i = num + 1)
            {
                IPoint point2 = exteriorRing2.GetPoint(i);
                point.X = point2.X;
                point.Y = point2.Y;
                point.Z = 0.0;
                exteriorRing.AppendPoint(point);
                num = i;
            }
            return polygon.Area();
        }

        private void ReleaseRenderPolygon()
        {
            bool flag = this._renderPolygon3D != null;
            if (flag)
            {
                GviMap.ObjectManager.DeleteObject(this._renderPolygon3D.Guid);
                this._renderPolygon3D = null;
            }
        }

        private void onUnchecked()
        {
            GviMap.AxMapControl.RcLButtonDown -= this.AxRenderControl_RcLButtonDown;
            GviMap.AxMapControl.RcLButtonUp -= this.AxRenderControl_RcLButtonUp;
            GviMap.AxMapControl.RcRButtonUp -= this.AxRenderControl_RcRButtonUp;
            this.DeleteLabel();
            this.ReleaseRenderObj();
            GviMap.MapControl.MouseCursor = null;
            GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
            GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectHover;
        }

        private bool AxMapControl_RcLButtonUp(uint Flags, int X, int Y)
        {
            throw new NotImplementedException();
        }

        public override void Execute(object parameter)
        {
            bool flag = StringExtension.ParseTo<bool>(parameter, false);
            bool flag2 = flag;
            if (flag2)
            {
                this.onChecked();
            }
            else
            {
                this.onUnchecked();
            }
        }

        private void onChecked()
        {
            GviMap.MapControl.MouseCursor = Path.Combine(Application.StartupPath + "/data/cursor", "select.cur");
            GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            GviMap.AxMapControl.RcLButtonDown += this.AxRenderControl_RcLButtonDown;
            GviMap.AxMapControl.RcLButtonUp += this.AxRenderControl_RcLButtonUp;
            GviMap.AxMapControl.RcRButtonUp += this.AxRenderControl_RcRButtonUp;
            this._gFactory = new GeometryFactory();
            this.surfaceSym = new SurfaceSymbol();
            this.curveSym = new CurveSymbol();
            this.curveSym.Color = ColorConvert.UintToColor(4294944000u);
            this.curveSym.Width = 0f;
            this.surfaceSym.BoundarySymbol = this.curveSym;
            this.surfaceSym.Color = ColorConvert.UintToColor(ColorConvert.AlphaUintToUint(100, 4294944000u));
            this._beginPointSymbol = new SimplePointSymbol();
            this._beginPointSymbol.FillColor = ColorConvert.UintToColor(4288335154u);
            this._beginPointSymbol.Size = 10;
            this._listRenderPoints = new List<IRenderPoint>();
            GviMap.AxMapControl.Focus();
        }

        private ISimplePointSymbol _beginPointSymbol;

        private IGeometryFactory _gFactory;

        private ILabel _iLabel;

        private List<IRenderPoint> _listRenderPoints;

        private IPolygon _polygon;

        private IRenderPolygon _renderPolygon3D;

        private ICurveSymbol curveSym;

        private IPoint samePoint;

        private ISurfaceSymbol surfaceSym;
    }
}