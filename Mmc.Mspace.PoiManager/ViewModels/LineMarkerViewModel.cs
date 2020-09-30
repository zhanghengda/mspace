using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Windows.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class LineMarkerViewModel : MarkerViewModel
    {
        #region varies

        private string _lineWidth;

        public string LineWidth
        {
            get { return this._lineWidth; }
            set
            {
                _lineWidth = value;
                OnPropertyChanged("LineWidth");
                {
                    if (float.TryParse(LineWidth, out float width))
                    {
                        var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
                        var symbol = rPolygon.Symbol;
                        symbol.Width = -width; //负数为单位像素
                        rPolygon.Symbol = symbol;
                    }
                }
            }
        }

        #endregion

        #region Command

        private RelayCommand<object> _heightTypeSelChangedCommand;
        public RelayCommand<object> HeightTypeSelChangedCommand
        {
            get { return _heightTypeSelChangedCommand ?? (_heightTypeSelChangedCommand = new RelayCommand<object>(HeightTypeSelectionChanged)); }
            set { _heightTypeSelChangedCommand = value; }
        }

        private RelayCommand<string> _titleTextChangedCmd;

        public RelayCommand<string> TitleTextChangedCmd
        {
            get
            {
                return _titleTextChangedCmd ?? (_titleTextChangedCmd = new RelayCommand<string>(TitleTextChanged));
            }
            set { _titleTextChangedCmd = value; }
        }
        #endregion

        public LineMarkerViewModel()
        {
            IsPoiCtrVisible = Visibility.Collapsed;
            //IsSizeCtrVisibility = Visibility.Hidden;
            IsLineCtrVisibility = Visibility.Visible;
            IsFaceCtrVisibility = Visibility.Collapsed;
            base.MarkerKey = "MmcLineMarkerKey";
            this.ViewTitle = Helpers.ResourceHelper.FindKey("Newlinemarking");
            MarkerModel.Type = 2;
            this.CreateTempRObj();
            MarkerModel.Title = Helpers.ResourceHelper.FindKey("Defaultlinemarking");
            this.SizeName = "长度：";

        }

        public override void ReAssignData(MarkerNew marker, bool isEdit, bool isInputAdd = false)
        {

            base.ReAssignData(marker, isEdit, isInputAdd);
            if (isEdit)
            {
                this.UpdateRObj();
            }
            else
            {

                this.GetLocation();

            }
        }

        protected override void CreateTempRObj()
        {
            var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
                gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
            polyline.SpatialCRS = GviMap.SpatialCrs;
            var rpolyLine = GviMap.ObjectManager.CreateRenderPolyline(polyline, null, GviMap.ProjectTree.RootID);
            if (GviMap.TempRObjectPool.ContainsKey(MarkerKey))
            {
                GviMap.TempRObjectPool[MarkerKey] = rpolyLine;
            }
            else
            {
                GviMap.TempRObjectPool.Add(MarkerKey, rpolyLine);
            }

            base.SetColor(rpolyLine.Symbol.Color);
            //this.LineWidth = (rpolyLine.Symbol.Width + 1).ToString();
            this.LineWidth = "1";
            base.CreateTempRObj();
        }


        protected void UpdateRObj()
        {
            this.ViewTitle = Helpers.ResourceHelper.FindKey("Editlinemarking");
            var item = GviMap.LinePolyManager.GetItemByKey(MarkerModel.MarkerId.ToString());

            GviMap.TempRObjectPool[LabelMarkerKey] = item.Item2.Clone(GviMap.AxMapControl);
            var rOldPolyline = item.Item3 as IRenderPolyline;
            var rpolyline =
                GviMap.ObjectManager.CreateRenderPolyline(rOldPolyline.GetFdeGeometry() as IPolyline,
                    rOldPolyline.Symbol);
            rpolyline.HeightStyle = rOldPolyline.HeightStyle;
            rpolyline.MaxVisibleDistance = rOldPolyline.MaxVisibleDistance;
            GviMap.TempRObjectPool[MarkerKey] = rpolyline;
            base.SetColor(rOldPolyline.Symbol.Color);
            this.Longitude = item.Item2.Position.X;
            this.Latitude = item.Item2.Position.Y;
            this.Altitude = item.Item2.Position.Z;

            this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
            base.GetAddress();
            var polyLine2 = rOldPolyline.GetFdeGeometry() as IPolyline;
            var pt = polyLine2.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            if (!string.IsNullOrEmpty(prjWkt))
                polyLine2.ProjectEx(prjWkt);

            this.MarkerModel.Size = polyLine2.Length;
            this.MarkerModel.Poitype = "m";
            polyLine2.Project(GviMap.SpatialCrs);

            double width = -(rOldPolyline.Symbol as CurveSymbol).Width;
            this.LineWidth = width.ToString();
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rOldPolyline.HeightStyle);
            //删除原来poi
            GviMap.LinePolyManager.DeletePoi(MarkerModel.MarkerId.ToString());
        }


        protected override void GetLocation()
        {
            if (MarkerModel.Type == 2)
            {
                if (DrawCustomer == null)
                {
                    DrawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("Linedrawn"),
                        DrawCustomerType.MenuCommand);
                }

                //注册绘制多边形事件
                IsEvenOn = true;
                RCDrawManager.Instance.PolylineDraw.Register(GviMap.AxMapControl, DrawCustomer,
                    RCMouseOperType.PickPoint);
                RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
                RCDrawManager.Instance.PolylineDraw.OnDrawFinished += PolylineDraw_OnDrawFinished;
                base.GetLocation();
            }
        }

        private void PolylineDraw_OnDrawFinished(object sender, object result)
        {
            if (!base.IsEvenOn) return;

            var rPolyline = result as IRenderPolyline;
            var polyLine = rPolyline.GetFdeGeometry() as IPolyline;
            if (polyLine == null || polyLine.PointCount < 2)
            {
                RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
                RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
                base.ShowView();
                return;
            }

            var rpolygon1 = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
            rpolygon1?.SetFdeGeometry(polyLine);
            var label = GviMap.TempRObjectPool[LabelMarkerKey] as ILabel;
            label.Position = polyLine.Midpoint;
            label.Text = MarkerModel.Title;
            base.SetColor(rpolygon1.Symbol.Color);

            this.LineWidth = (-rpolygon1.Symbol.Width).ToString();
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rpolygon1.HeightStyle);
            this.Longitude = label.Position.X;
            this.Latitude = label.Position.Y;
            this.Altitude = label.Position.Z;

            this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
            base.GetAddress();

            var pt = polyLine.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            if (!string.IsNullOrEmpty(prjWkt))
                polyLine.ProjectEx(prjWkt);
            MarkerModel.Size = polyLine.Length;
            this.MarkerModel.Poitype = "m";
            polyLine.Project(GviMap.SpatialCrs);
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

            base.ShowView();
        }

        protected override void SaveMarkerData()
        {
            var rLine = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
            var polyline = rLine.GetFdeGeometry() as IPolyline;

            MarkerModel.Geom = polyline.AsWKT().ToUpper();
            var rlinesymbol = rLine.Symbol;

            var symcolor = _markerView.ColorPicker.SelectedColor.Value;
            rlinesymbol.Color = System.Drawing.Color.FromArgb(symcolor.A, symcolor.R, symcolor.G, symcolor.B);
            rLine.Symbol = rlinesymbol;
            var renderStyle = new RenderGeometryStyle()
            { HeightStyle = rLine.HeightStyle, GeoSymbolXml = rLine.Symbol.AsXml() };
            var style = JsonUtil.SerializeToString(renderStyle);
            MarkerModel.Style = style;

            base.SaveMarkerData();
        }

        protected override void OnColorChanged(Color color)
        {
            var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
            var symbol = rPolygon.Symbol;
            symbol.Color = color;
            rPolygon.Symbol = symbol;
        }

        private void HeightTypeSelectionChanged(object selectedItem)
        {
            if (selectedItem != null)
            {
                var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
                rPolygon.HeightStyle = (selectedItem as HeightType).HeightStyle;
            }
        }

        /// <summary>
        /// 编辑标注-标题
        /// </summary>
        /// <param name="s"></param>
        private void TitleTextChanged(string s)
        {
            var label = GviMap.TempRObjectPool[LabelMarkerKey] as ILabel;
            label.Text = s;
        }
    }
}