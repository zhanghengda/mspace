using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Windows.Services;
using Mmc.Mspace.PoiManagerModule.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Mmc.Windows.Utils;
using Helpers;
using Mmc.MathUtil;
using Mmc.Mspace.PoiManagerModule.Models;

namespace Mmc.Mspace.PoiManagerModule
{
    /// <summary>
    /// 面标注
    /// </summary>
    public class PolygonMakerViewModel : PoiBaseViewModel
    {
        //  private IRenderPolygon rPolygon;
     
        private DrawCustomerUC drawCustomer;
        private string _sLLineWidth;

        /// <summary>
        /// 线宽
        /// </summary>
        public string SLLineWidth
        {
            get { return this._sLLineWidth; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._sLLineWidth, value, "SLLineWidth");
                {
                    try
                    {
                        if (float.TryParse(SLLineWidth, out float width))
                        {
                            var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
                            var symbol = rPolygon.Symbol;
                            symbol.BoundarySymbol.Width = -width;  //负数为单位像素
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

            _type = 3;//面为3
            _labelKey = "polygonMakerLabelKey";
            _polyMakerKey = "polygonMakerKey";
        }     


        public override void SaveMarkerToServer()
        {
            base.SaveMarkerToServer();

            //上传
        }

        protected void SetColor(Color c)
        {
            var view = this.View as MakerPolygonView;
            view.ColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        protected void SetSLColor(Color c)
        {
            var view = this.View as MakerPolygonView;
            view.SLColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }
        protected override void OnColorChanged(Color color)
        {
            try
            {
                var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
                var symbol = rPolygon.Symbol;
                symbol.Color= color;
                rPolygon.Symbol = symbol;
            }
            catch (Exception)
            {

            }
          
        }
        /// <summary>
        /// 标注面的外边线变化赋值
        /// </summary>
        /// <param name="color"></param>
        protected override void OnSLColorChanged(Color color)
        {
            try
            {
                var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
                var symbol = rPolygon.Symbol;
                symbol.BoundarySymbol.Color = color;
                rPolygon.Symbol = symbol;
            }
            catch (Exception)
            {

            }
        }

        public override void UpdateRobj()
        {
            base.UpdateRobj();

            var item = GviMap.LinePolyManager.GetItemByKey(PoiId);
            GviMap.TempRObjectPool[_labelKey] = item.Item2.Clone(GviMap.AxMapControl);
            this.Lng = item.Item2.Position.X;
            this.Lat = item.Item2.Position.Y;
            this.Alt = item.Item2.Position.Z;
            var rOldPolygon = item.Item3 as IRenderPolygon;
            var rOldPolygon2 = rOldPolygon.GetFdeGeometry() as IPolygon;

            var pt = rOldPolygon2.ExteriorRing.GetPoint(0);
            var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);
            this.Area = rOldPolygon2.CalAreaOfPolygon(prjWkt);

            var rpolygon = GviMap.ObjectManager.CreateRenderPolygon(rOldPolygon.GetFdeGeometry() as IPolygon, rOldPolygon.Symbol);
            var oldPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IPolygon;
            oldPolygon?.Dispose();
            rpolygon.HeightStyle = rOldPolygon.HeightStyle;
            rpolygon.MaxVisibleDistance = rOldPolygon.MaxVisibleDistance;
            GviMap.TempRObjectPool[_polyMakerKey] = rpolygon;
            SetColor(rpolygon.Symbol.Color);
            SetSLColor(rpolygon.Symbol.BoundarySymbol.Color);
            this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rOldPolygon.HeightStyle);
            //隐藏原来poi
            GviMap.LinePolyManager.DeletePoi(PoiId);
        }

        protected override void CreateNewRPoi()
        {
            base.CreateNewRPoi();


            //多边形标注进缓存
            //var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
            //var rpolygonOld = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
            //var rpolygon = GviMap.ObjectManager.CreateRenderPolygon(rpolygonOld.GetFdeGeometry() as IPolygon, rpolygonOld.Symbol);
            //GviMap.LinePolyManager.AddPoi(marker_id.ToString(), _type, label.Clone(GviMap.AxMapControl), rpolygon);
            //label.VisibleMask = gviViewportMask.gviViewNone;
            //rpolygonOld.VisibleMask = gviViewportMask.gviViewNone;



        }


        public override FrameworkElement CreatedView()
        {
            return new MakerPolygonView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        protected override void CreateTempRobj()
        {
            try
            {
                base.CreateTempRobj();
                //创建标注多边形
                if (!GviMap.TempRObjectPool.ContainsKey(_polyMakerKey))
                {
                    var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                  gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                    polygon.SpatialCRS = GviMap.SpatialCrs;
                    var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym, GviMap.ProjectTree.RootID);
                    GviMap.TempRObjectPool.Add(_polyMakerKey, rPolygon);
                }
            ((IRenderable)GviMap.TempRObjectPool[_polyMakerKey]).VisibleMask = gviViewportMask.gviViewNone;

                //创建label多边形
                if (!GviMap.TempRObjectPool.ContainsKey(_labelKey))
                {
                    var _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                    GviMap.TempRObjectPool.Add(_labelKey, _label);
                }

            ((IRenderable)GviMap.TempRObjectPool[_labelKey]).VisibleMask = gviViewportMask.gviViewNone;
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }

        }
        protected override void GetLocation()
        {

            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC(ResourceHelper.FindKey("Polygondrawing"), DrawCustomerType.MenuCommand);
                //注册绘制多边形事件
            }
            RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished += PolygonDraw_OnDrawFinished;

            base.GetLocation();
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

        private void PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                var rPolygon = result as IRenderPolygon;
                var polygon = rPolygon.GetFdeGeometry() as IPolygon;
                if (polygon == null || polygon.ExteriorRing.PointCount < 4)
                {
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;

                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    return;
                }

                var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
                label.Text = PoiTitle;
                label.Position = polygon.Envelope.Center.ToPoint(GviMap.GeoFactory,GviMap.SpatialCrs);

                this.Lng = polygon.Envelope.Center.X;
                this.Lat = polygon.Envelope.Center.Y;
                this.Alt = polygon.Envelope.Center.Z;

                var rpolygon1 = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
                var oldPolygon = rpolygon1.GetFdeGeometry() as IPolygon;
                oldPolygon.Dispose();
                rpolygon1?.SetFdeGeometry(polygon);
                SetColor(rpolygon1.Symbol.Color);
                //SetSLColor(rpolygon1.Symbol.BoundarySymbol.Color);
                this.SLLineWidth = (-rpolygon1.Symbol.BoundarySymbol.Width).ToString();

                var pt = polygon.ExteriorRing.GetPoint(0);
                var prjWkt = Wgs84UtmUtil.GetWkt(pt.X);

                this.Area = polygon.CalAreaOfPolygon(prjWkt);
                //if (!string.IsNullOrEmpty(prjWkt))
                //    polygon.ProjectEx(prjWkt);

                //this.Area=polygon.Area();
                //polygon.Project(GviMap.SpatialCrs);

                this.SelectedHeightType = HeightTypes.FirstOrDefault(p => p.HeightStyle == rpolygon1.HeightStyle);
                label.VisibleMask = gviViewportMask.gviViewAllNormalView;
                rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }


            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;

            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
            var view = (Window)base.View;
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            view.Show();
        }

        protected override void OnSelectedHeightChange(HeightType heightType)
        {
            if (GviMap.TempRObjectPool.ContainsKey(_polyMakerKey))
            {
                var rPolygon = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
                rPolygon.MaxVisibleDistance = 50000;
                rPolygon.HeightStyle = heightType.HeightStyle;
            }
        }

        protected override PostMarkerNew GetPostMarkerItem()
        {
            var postMarker = new PostMarkerNew();
            if (int.TryParse(marker_id, out int makerId))
                postMarker.marker_id = makerId;
            postMarker.type = _type;//面标注
            postMarker.detail = Detial;
            postMarker.lp_size = Area.ToString();

            if (ImgUrl.ToLower().Contains("http"))
                postMarker.img = ImgUrl.Substring(ImgUrl.LastIndexOf("resource") + 1 + "resouce".Length);
            else
                postMarker.img = ImgUrl;

            postMarker.address = this.Address;
            //markerInfo.points = new List<PoiPositon>();
            var rPoly = GviMap.TempRObjectPool[_polyMakerKey] as IRenderPolygon;
            var polygon = rPoly.GetFdeGeometry() as IPolygon;

            postMarker.geom = polygon.AsWKT().ToUpper();
            var renderStyle = new RenderGeometryStyle() { HeightStyle = rPoly.HeightStyle, GeoSymbolXml = rPoly.Symbol.AsXml() };
            var style = JsonUtil.SerializeToString(renderStyle);
            postMarker.style = style;
            // markerInfo.marker.style = rPoly.Symbol.AsXml();
            postMarker.title = PoiTitle;
            return postMarker;
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            ((IRenderable)GviMap.TempRObjectPool[_labelKey]).VisibleMask = gviViewportMask.gviViewNone;
            ((IRenderable)GviMap.TempRObjectPool[_polyMakerKey]).VisibleMask = gviViewportMask.gviViewNone;
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
            ((Window)base.View).Close();
        }
        public override void OnChecked()
        {
            base.OnChecked();

        }

        protected override void SetTitle()
        {
            base.SetTitle();
            this.WinTitle = IsEdit ? ResourceHelper.FindKey("Editfacemarking") : ResourceHelper.FindKey("Newfacemarking");
            if (!IsEdit)
                this.PoiTitle = ResourceHelper.FindKey("Defaultfacemarking");
        }
    }
}
