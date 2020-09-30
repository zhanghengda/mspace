using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Windows.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class PoiMarkerViewModel : MarkerViewModel
    {
        #region varies

        private PoiType _selectPoiTypes;

        [XmlIgnore]
        public PoiType SelectedPoiType
        {
            get { return this._selectPoiTypes; }
            set
            {
                _selectPoiTypes = value;
                OnPropertyChanged("SelectedPoiType");

                if (this.SelectedPoiType == null) return;

                this.UpdateRenderObj();

                if (this.SelectedPoiType != null)
                {
                    MarkerModel.CatId = this.SelectedPoiType.cat_id;
                    //this._poiInfo.cat_Name = this.SelectedPoiType.cat_name;
                }
            }
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

        private void UpdateRenderObj()
        {
            var rPoi = GviMap.TempRObjectPool[MarkerKey] as IRenderPOI;
            rPoi.VisibleMask = gviViewportMask.gviViewNone;
            var poi = GviMap.GeoFactory.CreatePoi(Longitude, Latitude, Altitude, this.SelectedPoiType.cat_url,
                MarkerModel.Title,
                crs: GviMap.SpatialCrs);

            rPoi.SetFdeGeometry(poi);
            GviMap.TempRObjectPool[MarkerKey] = rPoi;
        }

        public PoiMarkerViewModel()
        {
            IsPoiCtrVisible = Visibility.Visible;
            IsSizeCtrVisibility = Visibility.Collapsed;
            IsLineCtrVisibility = Visibility.Collapsed;
            IsFaceCtrVisibility = Visibility.Collapsed;
            base.MarkerKey = "MmcPoiMarkerKey";
            MarkerModel.Type = 1;
            this.PoiTypes = new ObservableCollection<PoiType>(MarkerHelper.Instance.PoiTypeDic.Values);
            
            this.ViewTitle = Helpers.ResourceHelper.FindKey("Addpoint");
            MarkerModel.Title = Helpers.ResourceHelper.FindKey("Defaultpoint");
            this.CreateTempRObj();
        }

        public override void ReAssignData(MarkerNew marker, bool isEdit, bool isInputAdd = false)
        {
            base.ReAssignData(marker, isEdit, isInputAdd );
            if (isEdit && marker != null)
            {
                if (marker.Type == 1 && marker.Geom.Contains("POINT"))
                {
                    var position = marker.Geom.Split('(')[1].Split(')')[0].Split(' ');
                    this.Longitude = Convert.ToDouble(position[0]);
                    this.Latitude = Convert.ToDouble(position[1]);
                    this.Altitude = Convert.ToDouble(position[2]);
                    this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
                    this.ViewTitle = Helpers.ResourceHelper.FindKey("Editpoint");
                    this.SelectedPoiType = MarkerHelper.Instance.PoiTypeDic[marker.CatId];
                }
            }
            else
            {
                this.GetLocation();
            }
        }

        protected override void FlyToObject()
        {
            GviMap.Camera.FlyTime = 1;
            var rPoi = GviMap.TempRObjectPool[MarkerKey] as IRenderPOI;
            if (rPoi != null)
            {
                var poi = rPoi.GetFdeGeometry() as IPoint;

                GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);

                GviMap.Camera.LookAt2(poi, 150, Angle);
                ((IRenderGeometry) rPoi).Glow(1500);
            }
        }

        protected override void GetLocation()
        {
            base.GetLocation();

            GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;

            GviMap.InteractMode = gviInteractMode.gviInteractSelect;
            GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick|gviMouseSelectMode.gviMouseSelectMove;
            _label.VisibleMask = gviViewportMask.gviViewAllNormalView;
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask,
            gviMouseSelectMode EventSender)
        {
            if (!IsEvenOn) return;

            try
            {
                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    //取消事件注册
                    IsEvenOn = false;
                    GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                    GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                    GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                    GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

                    var pt = IntersectPoint.Clone() as IPoint;
                    if (pt == null) return;
                    pt.Project(GviMap.SpatialCrs);
                    this.Longitude = pt.X;
                    this.Latitude = pt.Y;
                    this.Altitude = pt.Z;
                    this.Coor = Math.Round(Longitude, 7).ToString() + "," + Math.Round(Latitude, 7).ToString();
                    this.SelectedPoiType =this.PoiTypes[0];

                    base.GetAddress();

                    _label.VisibleMask = gviViewportMask.gviViewNone;

                    
                    // 点击时就显示点标注,以便截图
                    IRenderPOI rPoi = GviMap.TempRObjectPool[MarkerKey] as IRenderPOI;
                    rPoi.DepthTestMode = gviDepthTestMode.gviDepthTestDisable;
                    rPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;

                    this.ShowView();
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    var pt = IntersectPoint;
                    _label.Text = Helpers.ResourceHelper.FindKey("Markedlocation");
                    _label.Position = pt;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
            }
        }

        protected override void CreateTempRObj()
        {
            var poi = GviMap.GeoFactory.CreatePoi(0, 0, 0, this.PoiTypes[0].cat_url,
                "",
                crs: GviMap.SpatialCrs);

            IRenderPOI rPoi = GviMap.ObjectManager.CreateRenderPOI(poi);

            if (GviMap.TempRObjectPool.ContainsKey(MarkerKey))
            {
                GviMap.TempRObjectPool[MarkerKey] = rPoi;
            }
            else
            {
                GviMap.TempRObjectPool.Add(MarkerKey, rPoi);
            }
        }

        protected override void SaveMarkerData()
        {
            if(GviMap.TempRObjectPool.Count>0&& GviMap.TempRObjectPool[MarkerKey]!=null)
            {
                var rPoi = GviMap.TempRObjectPool[MarkerKey] as IRenderPOI;
                var poi = rPoi.GetFdeGeometry() as IPOI;
                MarkerModel.Geom = poi.AsWKT().ToUpper();
            }

            base.SaveMarkerData();
        }

        /// <summary>
        /// 编辑标注-标题
        /// </summary>
        /// <param name="s"></param>
        private void TitleTextChanged(string s)
        {
            var rPoi = GviMap.TempRObjectPool[MarkerKey] as IRenderPOI;
            var poi = rPoi.GetFdeGeometry() as IPOI;
            poi.Name = s;
            rPoi.SetFdeGeometry(poi);
        }
    }
}