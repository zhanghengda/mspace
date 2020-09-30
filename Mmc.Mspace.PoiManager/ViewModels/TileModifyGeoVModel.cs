using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.TileLayer;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class TileModifyGeoVModel : CheckedToolItemModel
    {

        private TileLayerPropView _view;
        private ISpatialCRS _spatialCRS;
        private ISurfaceSymbol _surfaceSymbol;
        private ObservableCollection<ModifyGeoVModel> _geoVModels;
        private ModifyGeoVModel _selectGeo;
        private IMultiPolygon _mulPolygon;
        private bool _isEdit;
        private string _polyMakerKey;
        private string _labelKey;

        [XmlIgnore]
        public I3DTileLayer TileLayer { get; set; }


        public ObservableCollection<ModifyGeoVModel> GeoVModels
        {
            get { return _geoVModels; }
            private set { _geoVModels = value; NotifyPropertyChanged("GeoVModels"); }
        }


        private string _searchTileCondition;
        public string SearchTileCondition
        {
            get { return _searchTileCondition; }
            set
            {
                _searchTileCondition = value;
                NotifyPropertyChanged("SearchTileCondition");
            }
        }

        public ModifyGeoVModel SelectGeo
        {
            get { return _selectGeo; }
            set
            {
                _selectGeo = value;
                NotifyPropertyChanged("SelectGeo");
                _curCreateTileModifier = _selectGeo?.Rpolygon;
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        public ICommand AddPolygonCmd { get; set; }

        /// <summary>
        /// 编辑
        /// </summary>
        public ICommand EditPolygonCmd { get; set; }
        /// <summary>
        /// 移动
        /// </summary>
        public ICommand MovePolygonCmd { get; set; }
        /// <summary>
        /// 高度调整
        /// </summary>
        public ICommand AdjustHeightCmd { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        public ICommand DelPolygonCmd { get; set; }
        /// <summary>
        /// 应用
        /// </summary>
        public ICommand ApplyCmd { get; set; }

        /// <summary>
        /// 飞入
        /// </summary>
        public ICommand FlytoCmd { get; set; }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public ICommand CancelCmd { get; set; }


        /// <summary>
        /// 搜索
        /// </summary>
        public ICommand SearchTileCmd { get; set; }

        //public ICommand ApplyTileCmd { get; set; }

        
        public override void OnChecked()
        {
            base.OnChecked();
            this.OnUnchecked();
            _polyMakerKey = "TileModifyKey";
            _labelKey= "TileModifyLabelKey";
            //建立绑定
            if (_view == null)
            {
                _view = new TileLayerPropView();
            }
            _view.Owner = Application.Current.MainWindow;
            _view.DataContext = this;
            _spatialCRS = (GviMap.CrsFactory.CreateFromWKT(this.TileLayer.GetWKT()) as ISpatialCRS);
            List<TileModifyGeo> geos = LocalWsConfigService.Instance.TileModifyGeos.Find(p => p.ConStr == TileLayer.ConnectionInfo);
            if (geos.HasValues())
            {
                GeoVModels.Clear();
                foreach (var item in geos)
                {
                    var label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                    label.Text = item.LabelText;
                    label.Position = GviMap.GeoFactory.CreateFromWKT(item.PolygonStr).Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS);
                    label.VisibleMask = gviViewportMask.gviViewAllNormalView;

                    var geoVm = new ModifyGeoVModel()
                    {
                        Index = item.Id.ToString(),
                        Name = item.Id.ToString(),
                        Label =label
                    };
                   
                    var geo = GviMap.GeoFactory.CreateFromWKT(item.Geom) as IPolygon;
                    geo.SpatialCRS = _spatialCRS;
                    var rPoly = GviMap.ObjectManager.CreateRenderPolygon(geo, _surfaceSymbol);
                    geoVm.Rpolygon = rPoly;
                    var model=GeoVModels.FirstOrDefault(t => t.Index == geoVm.Index);
                    if (model==null)
                    {
                        GeoVModels.Add(geoVm);
                    }
                }
                SelectGeo = GeoVModels[0];
            }
            ModifyTileLayer();
            GviMap.AxMapControl.RcObjectEditFinish -= AxMapControl_RcObjectEditFinish;
            GviMap.AxMapControl.RcObjectEditFinish += AxMapControl_RcObjectEditFinish;
            GviMap.AxMapControl.RcObjectEditing -= AxMapControl_RcObjectEditing;
            GviMap.AxMapControl.RcObjectEditing += AxMapControl_RcObjectEditing;
            _isEdit = true;
            _view.Show();
        }

        private void AxMapControl_RcObjectEditing(IGeometry Geometry)
        {

        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            _view?.Hide();
            //RemoveAllModel();
            if(GeoVModels.Count>0)
            {
                foreach(var item in GeoVModels)
                {
                    item.Label.VisibleMask = gviViewportMask.gviViewNone;
                    item.Rpolygon.VisibleMask = gviViewportMask.gviViewNone;
                }
            }
            GviMap.AxMapControl.RcObjectEditFinish -= AxMapControl_RcObjectEditFinish;
            GviMap.AxMapControl.RcObjectEditing -= AxMapControl_RcObjectEditing;
        }

        public override void Initialize()
        {
            base.Initialize();
            GeoVModels = new ObservableCollection<ModifyGeoVModel>();
            _surfaceSymbol = new SurfaceSymbol();
            _surfaceSymbol.Color = Color.FromArgb(0, 255, 255, 0);
            _surfaceSymbol.BoundarySymbol = new CurveSymbol
            {
                Color = Color.FromArgb(255, 255, 255, 0)
            };
            _mulPolygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryMultiPolygon, gviVertexAttribute.gviVertexAttributeZ) as IMultiPolygon;
 
            this.AddPolygonCmd = new RelayCommand(OnAddPolygon);
            this.EditPolygonCmd = new RelayCommand<ModifyGeoVModel>((modifyGeoVModel) => OnEdit(modifyGeoVModel));
            this.MovePolygonCmd = new RelayCommand<ModifyGeoVModel>((modifyGeoVModel) => OnMove(modifyGeoVModel));
            this.DelPolygonCmd = new RelayCommand(OnDelGeo);
            this.ApplyCmd = new RelayCommand(OnApply);
            this.CancelCmd = new RelayCommand(OnCloseWindow);
            this.FlytoCmd = new RelayCommand<ModifyGeoVModel>((modifyGeoVModel) =>OnFly(modifyGeoVModel));
            this.SearchTileCmd = new RelayCommand(OnSearchTile);
            //this.ApplyTileCmd = new RelayCommand<ModifyGeoVModel>((modifyGeoVModel) => OnApplyTile(modifyGeoVModel));
        }

        private void OnCloseWindow()
        {
            try
            {
                if(_isEdit)
                {
                    AxMapControl_RcObjectEditFinish();
                }
                OnUnchecked();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        //private void OnApplyTile(ModifyGeoVModel modifyGeoVModel)
        //{

        //}

        private void OnSearchTile()
        {
            try
            {
                List<TileModifyGeo> geos = new List<TileModifyGeo>();
                if (string.IsNullOrEmpty(SearchTileCondition))
                {
                    geos = LocalWsConfigService.Instance.TileModifyGeos.Find(p => p.ConStr == TileLayer.ConnectionInfo);
                }else
                {
                    geos = LocalWsConfigService.Instance.TileModifyGeos.Find(p => p.ConStr == TileLayer.ConnectionInfo&&p.LabelText.Contains(SearchTileCondition));
                }
                
                if (geos.HasValues())
                {
                    RemoveAllModel();
                    foreach (var item in geos)
                    {
                        
                        var label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
                        label.Text = item.LabelText;
                        label.Position = GviMap.GeoFactory.CreateFromWKT(item.PolygonStr).Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS);
                        label.VisibleMask = gviViewportMask.gviViewAllNormalView;

                        var geoVm = new ModifyGeoVModel()
                        {
                            Index = item.Id.ToString(),
                            Name = item.Id.ToString(),
                            Label = label
                        };

                        var geo = GviMap.GeoFactory.CreateFromWKT(item.Geom) as IPolygon;
                        geo.SpatialCRS = _spatialCRS;
                        var rPoly = GviMap.ObjectManager.CreateRenderPolygon(geo, _surfaceSymbol);
                        geoVm.Rpolygon = rPoly;
                        GeoVModels.Add(geoVm);
                    }
                    SelectGeo = GeoVModels[0];
                }
                ModifyTileLayer();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void OnFly(ModifyGeoVModel modifyGeoVModel)

        {
            try
            {
                if (this._curCreateTileModifier == null)
                    return;
                this._curCreateTileModifier = SelectGeo?.Rpolygon;
                GviMap.Camera.FlyToObject(this._curCreateTileModifier.Guid, gviActionCode.gviActionFlyTo);
                this._curCreateTileModifier.Glow(3000);
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }



        private void OnEdit(ModifyGeoVModel modifyGeoVModel)
        {
            try
            {
                OnFly(modifyGeoVModel);
                _isEdit = true;
                this._curCreateTileModifier = SelectGeo?.Rpolygon;
                GviMap.InteractMode = gviInteractMode.gviInteractEdit;
                this._curCreateTileModifier.VisibleMask =gviViewportMask.gviViewAllNormalView;
                GviMap.ObjectEditor.StartEditRenderGeometry(this._curCreateTileModifier, gviGeoEditType.gviGeoEditVertex);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnMove(ModifyGeoVModel modifyGeoVModel)
        {
            try
            {
                OnFly(modifyGeoVModel);
                _isEdit = true;
                this._curCreateTileModifier = SelectGeo?.Rpolygon;
                GviMap.InteractMode = gviInteractMode.gviInteractEdit;
                GviMap.ObjectEditor.StartEditRenderGeometry(this._curCreateTileModifier, gviGeoEditType.gviGeoEdit3DMove);
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnDelGeo()
        {
                this._curCreateTileModifier = SelectGeo?.Rpolygon;
                if (LocalWsConfigService.Instance.TileModifyGeos.Delete(P => P.Id.ToString() == SelectGeo.Index) > 0)
                {
                    //删除polygon
                    GviMap.ObjectManager.DeleteObject(SelectGeo.Rpolygon.Guid);
                    SelectGeo.Label.VisibleMask = gviViewportMask.gviViewNone;
                SelectGeo.Label.VisibleMask = gviViewportMask.gviViewNone;
                //更新压平
                GeoVModels.Remove(SelectGeo);
                
                if (GeoVModels.HasValues())
                    {
                        _curCreateTileModifier = GeoVModels.FirstOrDefault().Rpolygon;
                        SelectGeo = GeoVModels.FirstOrDefault();
                        SelectGeo.Label.VisibleMask = gviViewportMask.gviViewAllNormalView;
                        _curCreateTileModifier.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    }

                    ModifyTileLayer();
                }
        }

        private void ModifyTileLayer()
        {
            _mulPolygon.Clear();
            _mulPolygon.SpatialCRS = _spatialCRS;
            foreach (var item in GeoVModels)
            {
                if (item.Rpolygon == null)
                    continue;
                var geo = item.Rpolygon.GetFdeGeometry() as IPolygon;
                _mulPolygon.AddPolygon(geo);
            }
            var paras = TileLayer.RenderParams;
            int index = TileLayer.SetModifiers(_mulPolygon);
            //TileLayer.SetHoles(_mulPolygon);
        }

        /// <summary>
        /// 设置压平
        /// </summary>
        private void OnApply()
        {
            var polygon = SelectGeo?.Rpolygon.GetFdeGeometry() as IPolygon;
            foreach (var item in GeoVModels)
            {
                if (item.Index == SelectGeo?.Index)
                {
                    item.Label.Position = polygon.Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS);
                }
            }
            var modifyGeo = new TileModifyGeo()
            {
                PolygonStr = polygon.Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS).AsWKT()
            };

            LocalWsConfigService.Instance.TileModifyGeos.Update(modifyGeo);
            ModifyTileLayer();
        }
        private void OnAddPolygon()
        {
            try
            {
                _isEdit = false;
                GviMap.InteractMode = gviInteractMode.gviInteractEdit;
                IPolygon polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon, gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
                polygon.SpatialCRS = _spatialCRS;

                this._curCreateTileModifier = GviMap.ObjectManager.CreateRenderPolygon(polygon, _surfaceSymbol);
                GviMap.ObjectEditor.StartEditRenderGeometry(this._curCreateTileModifier, gviGeoEditType.gviRegionCreator);

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            _view.Hide();
        }

        private void AxMapControl_RcObjectEditFinish()
        {
            try
            {
                var rPolygon = _curCreateTileModifier as IRenderPolygon;
                var polygon = rPolygon.GetFdeGeometry() as IPolygon;
                if (polygon == null || polygon.ExteriorRing.PointCount < 4)
                {
                    GviMap.AxMapControl.RcObjectEditFinish -= AxMapControl_RcObjectEditFinish;
                    return;
                }

                var topo = polygon as ITopologicalOperator3D;
                if (!topo.IsSimple3D())
                {

                    //对拓扑不正确的geometry进行修正

                    var geometry = topo.Simplify3D() as IGeometry;
                    polygon = topo.Simplify3D() as IPolygon;
                    if (polygon == null)
                    {
                        OnDelGeo();
                        //ModifyTileLayer();
                        //GviMap.ObjectManager.DeleteObject(_curCreateTileModifier.Guid);
                        GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                        //GviMap.AxMapControl.RcObjectEditFinish -= AxMapControl_RcObjectEditFinish;
                        SelectGeo = GeoVModels.FirstOrDefault();
                        _view.Show();
                        return;
                    }
                }
                var geo = polygon.Clone() as IPolygon;
                if (!_isEdit)//创建
                {
                    var label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);

                    label.Text = "倾斜压平";
                    label.Position = geo.Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS);
                    label.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    
                    var rPoly = GviMap.ObjectManager.CreateRenderPolygon(geo, _surfaceSymbol);

                    //序号
                    var modifyGeo = new TileModifyGeo()
                    {
                        Geom = geo.AsWKT(),
                        ConStr = TileLayer.ConnectionInfo,
                        LabelText = label.Text,
                        PolygonStr = label.Position.AsWKT()
                    };
                    
                    //入库
                    var index = LocalWsConfigService.Instance.TileModifyGeos.Add(modifyGeo);
                    if (index > 0)
                    {
                        modifyGeo.Id = index;

                        label.Text = label.Text + index;
                        modifyGeo.LabelText = label.Text;
                        LocalWsConfigService.Instance.TileModifyGeos.Update(modifyGeo);
                        //样式
                        ModifyGeoVModel vModel = new ModifyGeoVModel()
                        {
                            Rpolygon = rPoly,
                            Index = modifyGeo.Id.ToString(),
                            Name = modifyGeo.Id.ToString(),
                            Label = label
                        };
                        GeoVModels.Add(vModel);
                        
                    }
                    GviMap.ObjectManager.DeleteObject(_curCreateTileModifier.Guid);
                   
                }
                else//编辑
                {
                    //更新polygon
                    var index = GeoVModels.IndexOf(SelectGeo);
                    var vm = GeoVModels[index];
                    vm.Rpolygon.SetFdeGeometry(geo);
                    var tileGeo = LocalWsConfigService.Instance.TileModifyGeos.FindOne(p => p.ConStr == TileLayer.ConnectionInfo && p.Id.ToString() == vm.Index);
                    tileGeo.Geom = geo.AsWKT();
                    foreach (var item in GeoVModels)
                    {
                        if (item.Index == SelectGeo?.Index)
                        {
                            item.Label.Position = geo.Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS);
                        }
                    }

                    tileGeo.PolygonStr = geo.Envelope.Center.ToPoint(GviMap.GeoFactory, _spatialCRS).AsWKT();
                    
                    LocalWsConfigService.Instance.TileModifyGeos.Update(tileGeo);
                }
                
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                //进行压平操作
               ModifyTileLayer();

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            _view.Show();
        }

        private IRenderPolygon _curCreateTileModifier;


        private void RemoveAllModel()
        {
            if (GeoVModels.HasValues())
            {
                foreach (var item in GeoVModels)
                {
                    GviMap.ObjectManager.DeleteObject(item.Rpolygon.Guid);
                    item.Label.VisibleMask = gviViewportMask.gviViewNone;
                }
                GeoVModels.Clear();
                SelectGeo = null;

            }
            ModifyTileLayer();
        }
    }

    public class ModifyGeoVModel : BindableBase
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public ILabel Label { get; set; }
        [XmlIgnore]
        public IRenderPolygon Rpolygon { get; set; }

        public IRenderPolygon Polygon { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Index))
                return Index;
            return base.ToString();
        }
    }
}
