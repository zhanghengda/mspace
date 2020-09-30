using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Helpers;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.MathUtil;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
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
    /// <summary>
    /// 线标注
    /// </summary>
    public class MakerLineViewModel : PoiBaseViewModel
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
                            var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
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
            _type = 2;//线为2
            _polyMakerKey = "polyLineMakerKey";
            _labelKey = "polyLineMakerLabelKey";
        }
        public override void SaveMarkerToServer()
        {
            base.SaveMarkerToServer();
            _surSym = new CurveSymbol();
            _surSym.Color = Color.FromArgb(66, Color.Orange);
        }

        protected override PostMarkerNew GetPostMarkerItem()
        {
            var markerInfo = new PostMarkerNew();
            if (int.TryParse(marker_id, out int makerId))
                markerInfo.marker_id = makerId;
            markerInfo.type = _type;
            markerInfo.detail = Detial;
            markerInfo.lp_size = Len.ToString();
            if (ImgUrl.ToLower().Contains("http"))
                markerInfo.img = ImgUrl.Substring(ImgUrl.LastIndexOf("resource") + 1 + "resouce".Length);
            else
                markerInfo.img = ImgUrl;

            markerInfo.address = this.Address;
            var rLine = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
            var polyline = rLine.GetFdeGeometry() as IPolyline;

            markerInfo.geom = polyline.AsWKT().ToUpper();
            var renderStyle = new RenderGeometryStyle() { HeightStyle = rLine.HeightStyle, GeoSymbolXml = rLine.Symbol.AsXml() };
            var style = JsonUtil.SerializeToString(renderStyle);
            markerInfo.style = style;
            // marker2.marker.style = rPoly.Symbol.AsXml();
            markerInfo.title = PoiTitle;
            return markerInfo;
        }

        protected override void OnColorChanged(Color color)
        {
            try
            {
                var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
                var symbol = rPolygon.Symbol;
                symbol.Color = color;
                rPolygon.Symbol = symbol;
            }
            catch (Exception)
            {

            }
        }

        

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

            var item = GviMap.LinePolyManager.GetItemByKey(PoiId);

            GviMap.TempRObjectPool[_labelKey] = item.Item2.Clone(GviMap.AxMapControl);
            var rOldPolyline = item.Item3 as IRenderPolyline;
            var rpolyline = GviMap.ObjectManager.CreateRenderPolyline(rOldPolyline.GetFdeGeometry() as IPolyline, rOldPolyline.Symbol);
            rpolyline.HeightStyle = rOldPolyline.HeightStyle;
            rpolyline.MaxVisibleDistance = rOldPolyline.MaxVisibleDistance;
            GviMap.TempRObjectPool[_polyMakerKey] = rpolyline;
            SetColor(rOldPolyline.Symbol.Color);
            this.Lng = item.Item2.Position.X;
            this.Lat = item.Item2.Position.Y;
            this.Alt = item.Item2.Position.Z;

            var polyLine2 = rOldPolyline.GetFdeGeometry() as IPolyline;
            var pt = polyLine2.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            if (!string.IsNullOrEmpty(prjWkt))
                polyLine2.ProjectEx(prjWkt);
            this.Len = polyLine2.Length;
            polyLine2.Project(GviMap.SpatialCrs);

            double width = -(rOldPolyline.Symbol as CurveSymbol).Width;
            this.LineWidth = width.ToString();
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rOldPolyline.HeightStyle);
            //删除原来poi
            GviMap.LinePolyManager.DeletePoi(PoiId);
        }

        protected void SetColor(Color c)
        {
            var view = this.View as MakerLineView;
            view.ColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }


        protected override void CreateTempRobj()
        {
            base.CreateTempRobj();
            //创建标注线
            if (!GviMap.TempRObjectPool.ContainsKey(_polyMakerKey))
            {
                var polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline,
               gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                polyline.SpatialCRS = GviMap.SpatialCrs;
                rpolyLine = GviMap.ObjectManager.CreateRenderPolyline(polyline, _surSym, GviMap.ProjectTree.RootID);
                GviMap.TempRObjectPool.Add(_polyMakerKey, rpolyLine);
                SetColor(rpolyLine.Symbol.Color);
            }
              ((IRenderable)GviMap.TempRObjectPool[_polyMakerKey]).VisibleMask = gviViewportMask.gviViewNone;
            //创建label多边形
            if (!GviMap.TempRObjectPool.ContainsKey(_labelKey))
            {
                _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                GviMap.TempRObjectPool.Add(_labelKey, _label);
            }
              ((IRenderable)GviMap.TempRObjectPool[_labelKey]).VisibleMask = gviViewportMask.gviViewNone;
        }

        protected override void CreateNewRPoi()
        {
            base.CreateNewRPoi();
            //折线标注进缓存
            //var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
            //var rpolygonOld = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
            //var rpolygon = GviMap.ObjectManager.CreateRenderPolyline(rpolygonOld.GetFdeGeometry() as IPolyline, rpolygonOld.Symbol);
            //rpolygon.HeightStyle = rpolygonOld.HeightStyle;
            //rpolygon.MaxVisibleDistance = rpolygonOld.MaxVisibleDistance;
            //GviMap.LinePolyManager.AddPoi(marker_id.ToString(), _type, label.Clone(GviMap.AxMapControl), rpolygon);
            //label.VisibleMask = gviViewportMask.gviViewNone;
            //rpolygonOld.VisibleMask = gviViewportMask.gviViewNone;
        }

        protected override void GetLocation()
        {
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC(ResourceHelper.FindKey("Linedrawn"), DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }
            RCDrawManager.Instance.PolylineDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished += PolylineDraw_OnDrawFinished;

            base.GetLocation();
        }



        private void PolylineDraw_OnDrawFinished(object sender, object result)
        {
            var rPolyline = result as IRenderPolyline;
            var polyLine = rPolyline.GetFdeGeometry() as IPolyline;
            if (polyLine == null || polyLine.PointCount < 2)
            {
                RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
                RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
                return;
            }
            
            var rpolygon1 = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
            rpolygon1?.SetFdeGeometry(polyLine);
            var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
            label.Position = polyLine.Midpoint;
            label.Text = PoiTitle;
            SetColor(rpolygon1.Symbol.Color);
            this.LineWidth = (-rpolygon1.Symbol.Width).ToString();
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rpolygon1.HeightStyle);
            this.Lng = label.Position.X;
            this.Lat = label.Position.Y;
            this.Alt = label.Position.Z;
            var pt = polyLine.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            if (!string.IsNullOrEmpty(prjWkt))
                polyLine.ProjectEx(prjWkt);
            this.Len = polyLine.Length;
            polyLine.Project(GviMap.SpatialCrs);
            label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //var address = amapLocationService.GetAddressByCoor(label.Position.X, label.Position.Y);
            //this.Adrress = address;
            var view = (Window)base.View;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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

        protected override void OnPoiTitleChanged(string title)
        {
            base.OnPoiTitleChanged(title);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ((IRenderable)GviMap.TempRObjectPool[_labelKey]).VisibleMask = gviViewportMask.gviViewNone;
            ((IRenderable)GviMap.TempRObjectPool[_polyMakerKey]).VisibleMask = gviViewportMask.gviViewNone;
            RCDrawManager.Instance.PolylineDraw.OnDrawFinished -= PolylineDraw_OnDrawFinished;
            RCDrawManager.Instance.PolylineDraw.UnRegister(GviMap.AxMapControl);
            ((Window)base.View).Close();
        }
        public override void OnChecked()
        {
            base.OnChecked();
        }

        protected override void OnSelectedHeightChange(HeightType heightType)
        {
            if (GviMap.TempRObjectPool.ContainsKey(_polyMakerKey))
            {
                var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolyline;
                rPolygon.MaxVisibleDistance = 50000;
                rPolygon.HeightStyle = heightType.HeightStyle;
            }
        }

        protected override void SetTitle()
        {
            base.SetTitle();
            this.WinTitle = IsEdit ? ResourceHelper.FindKey("Editlinemarking") : ResourceHelper.FindKey("Newlinemarking");
            if (!IsEdit)
                this.PoiTitle = ResourceHelper.FindKey("Defaultlinemarking");
        }

    }
}
