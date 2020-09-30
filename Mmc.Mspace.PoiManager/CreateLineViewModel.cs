using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mmc.Mspace.PoiManagerModule
{
    public class CreateLineViewModel : MarkerBaseViewModel
    {
        private IRenderPolyline rpolyLine;
        private DrawCustomerUC drawCustomer;
        private ICurveSymbol _surSym = null;
        private ILabel _label;

        private string _lineWidth;
        /// <summary>
        /// 线宽
        /// </summary>
        public string LineWidth
        {
            get { return this._lineWidth; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._lineWidth, value, "LineWidth");
                {
                    try
                    {
                        if (float.TryParse(LineWidth, out float width))
                        {
                            var rPolygon = GviMap.TempRObjectPool[MarkerTypeKey] as IRenderPolyline;
                            var symbol = rPolygon.Symbol;
                            symbol.Width = -width;  //负数为单位像素
                            rPolygon.Symbol = symbol;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            _type = 2;
            MarkerTypeKey = "polyLineMakerKey";
            LabelKey = "polyLineMakerLabelKey";
        }

        public override void CreateMaker()
        {
            base.CreateMaker();
            _surSym = new CurveSymbol();
            _surSym.Color = Color.FromArgb(66, Color.Orange);
        }

        //protected override PostMarkerModel GetMarkerInfoItem()
        //{
        //    PostMarkerModel markerInfo = new PostMarkerModel() { Marker = new Marker(), MspaceMarker = new MspaceMarker() };

        //    markerInfo.Marker.marker_id = _marker_id;
        //    markerInfo.Marker.type = _type;
        //    //markerInfo.Marker.detail = Detial;
        //    markerInfo.Marker.img = _img_path;
        //    markerInfo.Marker.address = this.Address;
        //    var rLine = GviMap.TempRObjectPool[MarkerTypeKey] as IRenderPolyline;
        //    var polyline = rLine.GetFdeGeometry() as IPolyline;

        //    markerInfo.MspaceMarker.geom = polyline.AsWKT().ToUpper();
        //    var renderStyle = new RenderGeometryStyle() { HeightStyle = rLine.HeightStyle, GeoSymbolXml = rLine.Symbol.AsXml() };
        //    string style = JsonUtil.SerializeToString(renderStyle);
        //    markerInfo.Marker.style = style;
        //    // marker2.marker.style = rPoly.Symbol.AsXml();
        //    markerInfo.Marker.title = MarkerTitle;

        //    return markerInfo;
        //}

        public override FrameworkElement CreatedView()
        {
            return new MakerLineView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        public override void UpdateRobj()
        {
            base.UpdateRobj();
            var item = GviMap.LinePolyManager.GetItemByKey(Convert.ToString(_marker_id));
            GviMap.TempRObjectPool[LabelKey] = item.Item2.Clone(GviMap.AxMapControl);
            var rOldPolyline = item.Item3 as IRenderPolyline;
            var rpolyline = GviMap.ObjectManager.CreateRenderPolyline(rOldPolyline.GetFdeGeometry() as IPolyline, rOldPolyline.Symbol);
            rpolyline.HeightStyle = rOldPolyline.HeightStyle;
            rpolyline.MaxVisibleDistance = rOldPolyline.MaxVisibleDistance;
            GviMap.TempRObjectPool[MarkerTypeKey] = rpolyline;
            SetColor(rOldPolyline.Symbol.Color);
            this.Lng = item.Item2.Position.X;
            this.Lat = item.Item2.Position.Y;
            this.Alt = item.Item2.Position.Z;
            double width = -(rOldPolyline.Symbol as CurveSymbol).Width;
            this.LineWidth = width.ToString();
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rOldPolyline.HeightStyle);
            //删除原来poi
            GviMap.LinePolyManager.DeletePoi(Convert.ToString(_marker_id));
        }


        protected void SetColor(Color c)
        {
            var view = this.View as MakerLineView;
            view.ColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        protected override void CreateTempRobj()
        {
            base.CreateTempRobj();
            if (!GviMap.TempRObjectPool.ContainsKey(MarkerTypeKey))
            {
                var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
               gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                polyline.SpatialCRS = GviMap.SpatialCrs;
                rpolyLine = GviMap.ObjectManager.CreateRenderPolyline(polyline, _surSym, GviMap.ProjectTree.RootID);
                GviMap.TempRObjectPool.Add(MarkerTypeKey, rpolyLine);
                SetColor(rpolyLine.Symbol.Color);
            }
              ((IRenderable)GviMap.TempRObjectPool[MarkerTypeKey]).VisibleMask = gviViewportMask.gviViewNone;
            //创建label多边形
            if (!GviMap.TempRObjectPool.ContainsKey(LabelKey))
            {
                _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                GviMap.TempRObjectPool.Add(LabelKey, _label);
            }
              ((IRenderable)GviMap.TempRObjectPool[LabelKey]).VisibleMask = gviViewportMask.gviViewNone;
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ((IRenderable)GviMap.TempRObjectPool[LabelKey]).VisibleMask = gviViewportMask.gviViewNone;
            ((IRenderable)GviMap.TempRObjectPool[MarkerTypeKey]).VisibleMask = gviViewportMask.gviViewNone;
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
            ((Window)base.View).Close();
        }


        private void PolylineDraw_OnDrawFinished(object sender, object result)
        {
            var rPolyline = result as IRenderPolyline;
            var polyLine = rPolyline.GetFdeGeometry() as IPolyline;

            var rpolygon1 = GviMap.TempRObjectPool[MarkerTypeKey] as IRenderPolyline;
            rpolygon1?.SetFdeGeometry(polyLine);
            var label = GviMap.TempRObjectPool[LabelKey] as ILabel;
            label.Position = polyLine.Midpoint;
            label.Text = MarkerTitle;
            SetColor(rpolygon1.Symbol.Color);
            this.LineWidth = (-rpolygon1.Symbol.Width).ToString();
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rpolygon1.HeightStyle);
            this.Lng = label.Position.X;
            this.Lat = label.Position.Y;
            this.Alt = label.Position.Z;
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //var address = amapLocationService.GetAddressByCoor(label.Position.X, label.Position.Y);
            //this.Adrress = address;
            var view = (Window)base.View;
            //view.Left = screenX + 30;
            //view.Top = sreenY - view.ActualHeight / 2;
            view.Show();
        }

        protected override void OnDetailChanged(string detail)
        {
            base.OnDetailChanged(detail);
        }

        protected override void OnImgUrlChanged(string url)
        {
            base.OnImgUrlChanged(url);
        }

        protected override void OnSelectedHeightChange(HeightType heightType)
        {
            if (GviMap.TempRObjectPool.ContainsKey(MarkerTypeKey))
            {
                var rPolygon = GviMap.TempRObjectPool[MarkerTypeKey] as IRenderPolyline;
                rPolygon.MaxVisibleDistance = 50000;
                rPolygon.HeightStyle = heightType.HeightStyle;
            }
        }

    }
}
