using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class FaceMarkerViewModel : MarkerViewModel
    {
        #region varies

        private string _outlineWidth;

        public string OutlineWidth
        {
            get { return this._outlineWidth; }
            set
            {
                _outlineWidth = value;


                OnPropertyChanged("OutlineWidth");
                {
                    if (float.TryParse(OutlineWidth, out float width))
                    {
                        var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolygon;
                        if (rPolygon == null) return;
                        var symbol = rPolygon.Symbol;
                        symbol.BoundarySymbol.Width = -width; //负数为单位像素
                        rPolygon.Symbol = symbol;
                    }
                }
            }
        }

        private System.Windows.Media.Color? _outlineColor;

        public System.Windows.Media.Color? OutlineColor
        {
            get => _outlineColor;
            set
            {
                _outlineColor = value ?? new System.Windows.Media.Color();
                var v = this._outlineColor.Value;
                base.SetOutlineColor(Color.FromArgb(v.A, v.R, v.G, v.B));
                OnPropertyChanged("OutlineColor");
            }
        }

        //private static readonly object obj = new object();
        //private static FaceMarkerViewModel _faceMarkerViewModel;

        //public static FaceMarkerViewModel Instance
        //{
        //    get
        //    {
        //        lock (obj)
        //        {
        //            if (_faceMarkerViewModel == null)
        //            {
        //                _faceMarkerViewModel = new FaceMarkerViewModel();
        //            }
        //            return _faceMarkerViewModel;
        //        }
        //    }
        //}




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

        public FaceMarkerViewModel()
        {
            IsPoiCtrVisible = Visibility.Collapsed;
            IsLineCtrVisibility = Visibility.Collapsed;
            IsFaceCtrVisibility = Visibility.Visible;
            base.MarkerKey = "MmcFaceMarkerKey";
            this.ViewTitle = Helpers.ResourceHelper.FindKey("Newfacemarking");
            MarkerModel.Type = 3;
            this.CreateTempRObj();
            MarkerModel.Title = Helpers.ResourceHelper.FindKey("Defaultfacemarking");
            this.OutlineColor = new System.Windows.Media.Color();
            this.OutlineWidth = "1";
            this.SizeName = "面积：";

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

        private void UpdateRObj()
        {
            this.ViewTitle = Helpers.ResourceHelper.FindKey("Editfacemarking");
            var item = GviMap.LinePolyManager.GetItemByKey(MarkerModel.MarkerId.ToString());

            GviMap.TempRObjectPool[LabelMarkerKey] = item.Item2.Clone(GviMap.AxMapControl);
            this.Longitude = item.Item2.Position.X;
            this.Latitude = item.Item2.Position.Y;
            this.Altitude = item.Item2.Position.Z;

            this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
            base.GetAddress();
            var rOldPolygon = item.Item3 as IRenderPolygon;
            rOldPolygon.VisibleMask = gviViewportMask.gviViewNone;
       
            var rOldPolygon2 = rOldPolygon.GetFdeGeometry() as IPolygon;

            var pt = rOldPolygon2.ExteriorRing.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            this.MarkerModel.Size = rOldPolygon2.CalAreaOfPolygon(prjWkt);
            this.MarkerModel.Poitype = "m²";
            var rpolygon =
                GviMap.ObjectManager.CreateRenderPolygon(rOldPolygon.GetFdeGeometry() as IPolygon, rOldPolygon.Symbol);
            var oldPolygon = GviMap.TempRObjectPool[MarkerKey] as IPolygon;
            

            oldPolygon?.Dispose();
            rpolygon.HeightStyle = rOldPolygon.HeightStyle;
            rpolygon.MaxVisibleDistance = rOldPolygon.MaxVisibleDistance;
            GviMap.TempRObjectPool[MarkerKey] = rpolygon;
            base.SetColor(rpolygon.Symbol.Color);
            base.SetOutlineColor(rpolygon.Symbol.BoundarySymbol.Color);
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rOldPolygon.HeightStyle);

            double width = -(rOldPolygon.Symbol as SurfaceSymbol).BoundarySymbol.Width;
            this.OutlineWidth = width.ToString();
            //隐藏原来poi

            GviMap.LinePolyManager.DeletePoi(MarkerModel.MarkerId.ToString());
        }

        protected override void CreateTempRObj()
        {
            var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygon.SpatialCRS = GviMap.SpatialCrs;
            var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym,
                GviMap.ProjectTree.RootID);

            if (GviMap.TempRObjectPool.ContainsKey(MarkerKey))
            {
                GviMap.TempRObjectPool[MarkerKey] = rPolygon;
            }
            else
            {
                GviMap.TempRObjectPool.Add(MarkerKey, rPolygon);
            }

            base.CreateTempRObj();
        }

        protected override void GetLocation()
        {
            if (DrawCustomer == null)
            {
                DrawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("Polygondrawing"),
                    DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }

            RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, DrawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished;

            base.GetLocation();
        }

        private void PolygonDraw_OnDrawFinished(object sender, object result)
        {
            if (!IsEvenOn) return;
            try
            {
                var rPolygon = result as IRenderPolygon;
                var polygon = rPolygon.GetFdeGeometry() as IPolygon;
                if (polygon == null || polygon.ExteriorRing.PointCount < 4)
                {
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;

                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    base.ShowView();
                    return;
                }

                var label = GviMap.TempRObjectPool[LabelMarkerKey] as ILabel;
                label.Text = MarkerModel.Title;
                label.Position = polygon.Envelope.Center.ToPoint(GviMap.GeoFactory, GviMap.SpatialCrs);

                this.Longitude = polygon.Envelope.Center.X;
                this.Latitude = polygon.Envelope.Center.Y;
                this.Altitude = polygon.Envelope.Center.Z;

                this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
                base.GetAddress();

                var rpolygon1 = GviMap.TempRObjectPool[MarkerKey] as IRenderPolygon;
                var oldPolygon = rpolygon1.GetFdeGeometry() as IPolygon;
                oldPolygon.Dispose();
                rpolygon1?.SetFdeGeometry(polygon);
                base.SetColor(rpolygon1.Symbol.Color);
                base.SetOutlineColor(rpolygon1.Symbol.BoundarySymbol.Color);
                this.OutlineWidth = (-rpolygon1.Symbol.BoundarySymbol.Width).ToString();

                var pt = polygon.ExteriorRing.GetPoint(0);
                var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);

                this.MarkerModel.Size = polygon.CalAreaOfPolygon(prjWkt);
                this.MarkerModel.Poitype = "m²";
                this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rpolygon1.HeightStyle);
                label.VisibleMask = gviViewportMask.gviViewAllNormalView;
                rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;

                RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;

                RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                this.ShowView();
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        protected override void SaveMarkerData()
        {
            var rPoly = GviMap.TempRObjectPool[MarkerKey] as IRenderPolygon;
            var polygon = rPoly.GetFdeGeometry() as IPolygon;
            MarkerModel.Geom = polygon.AsWKT().ToUpper();

            var linecolor = _markerView.OutlineColorPicker.SelectedColor.Value;
            var symcolor = _markerView.ColorPicker.SelectedColor.Value;
            Color rPolyboundarycolor = System.Drawing.Color.FromArgb(linecolor.A, linecolor.R, linecolor.G, linecolor.B);
            Color rPolysymbolcolor = System.Drawing.Color.FromArgb(symcolor.A, symcolor.R, symcolor.G, symcolor.B);


            var symbol = rPoly.Symbol;
            symbol.Color = rPolysymbolcolor;
            symbol.BoundarySymbol.Color = rPolyboundarycolor;
            rPoly.Symbol = symbol;
            var renderStyle = new RenderGeometryStyle()
            {
                HeightStyle = rPoly.HeightStyle, GeoSymbolXml = rPoly.Symbol.AsXml()
            };
            var style = JsonUtil.SerializeToString(renderStyle);
            MarkerModel.Style = style;

            base.SaveMarkerData();
        }

        private void HeightTypeSelectionChanged(object selectedItem)
        {
            if (selectedItem != null)
            {
                var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolygon;
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