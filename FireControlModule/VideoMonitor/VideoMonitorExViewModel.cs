using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule
{
    /// <summary>
    /// 视频监控列表
    /// </summary>
    public class VideoMonitorExViewModel : CheckedToolItemModel
    {
        private readonly int HighlightSize;
        private IDisplayLayer _layer;
        private IRenderPOI oldPoi;
        private KeyValuePair<string, IShowLayer> oldSelectdItem;
        private string queryKeys;
        private IEnumerable resultsSource;
        private DataRowView selectedPOIItem;
        protected bool _canFlyTo = true;

        public VideoMonitorExViewModel() : base()
        {
            //string text = ConfigurationManager.AppSettings["poiHighlightSize"];
            //this.HighlightSize = ((!string.IsNullOrEmpty(text)) ? StringExtension.ParseTo<int>(text, 64) : 64);
            //webVideoVm = new WebVideoViewModel();
            this.HighlightSize = 40;
        }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        [XmlIgnore]
        public ICommand QueryCmd { get; set; }

        [XmlIgnore]
        public string QueryKeys
        {
            get { return this.queryKeys; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.queryKeys, value, "QueryKeys"); }
        }

        [XmlIgnore]
        public IEnumerable ResultsSource
        {
            get { return this.resultsSource; }
            set { base.SetAndNotifyPropertyChanged<IEnumerable>(ref this.resultsSource, value, "ResultsSource"); }
        }

        [XmlIgnore]
        public DataRowView SelectedPOIItem
        {
            get { return this.selectedPOIItem; }
            set
            {
                this.ClearOldSelectedPOIItem();
                this.selectedPOIItem = value;
                if (this.selectedPOIItem == null)
                    return;
                string oid = this.SelectedPOIItem.Row["oid"].ToString();
                if (_canFlyTo)
                    _layer.FlyToFeature(oid, GviMap.MapControl.Camera);
                string imgName = string.Empty;
                IsPOILayer(_layer, out imgName);
                this.ShowHightLightPOI(_layer, oid, imgName);
                this.oldSelectdItem = new KeyValuePair<string, IShowLayer>(oid, _layer);
                //ShowVideoView(oid);
                ShowVideoView(oid);
            }
        }

        public virtual void ShowVideoView(string oid)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;

            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                RestoreEnv();
            });
            _layer = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
            this.QueryCmd = new RelayCommand(() =>
            {
                if (_layer == null)
                    _layer = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
                IGeometry geo = null;
                if (ServiceManager.GetService<IQueryService>(null).Geomtry != null)
                    geo = ServiceManager.GetService<IQueryService>(null).Geomtry.Clone2(gviVertexAttribute.gviVertexAttributeNone);
                DataTable dt = new DataTable();
                dt = _layer.FuzzySearch(this.QueryKeys, null, geo);
                this.ResultsSource = ((dt != null) ? dt.DefaultView : null);
                if (dt == null || dt.Rows.Count == 0)
                    MessageBox.Show(Application.Current.MainWindow, "查询结果为空", "提示:", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
            });
        }

        public void ClearOldSelectedPOIItem()
        {
            bool flag = !string.IsNullOrEmpty(this.oldSelectdItem.Key);
            if (flag)
            {
                this.oldSelectdItem.Value.UnHighLightFeature(this.oldSelectdItem.Key, GviMap.MapControl.FeatureManager);
                DisplayLayer displayLayer = this.oldSelectdItem.Value as DisplayLayer;
                GviMap.MapControl.FeatureManager.SetFeatureVisibleMask(displayLayer.Fc, int.Parse(this.oldSelectdItem.Key), gviViewportMask.gviViewAllNormalView);
                bool flag3 = this.oldPoi != null;
                if (flag3)
                {
                    GviMap.MapControl.ObjectManager.DeleteObject(this.oldPoi.Guid);
                    this.oldPoi = null;
                }
            }
        }

        public override FrameworkElement CreatedView()
        {
            var view = new VideoMonitorExView() { Owner = Application.Current.MainWindow };

            return view;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            this.QueryCmd.Execute(null);
            var view = this.View as Window;
            view.Show();
            var rightView = ServiceManager.GetService<IShellService>(null).RightToolMenu;
            var left = ServiceManager.GetService<IShellService>(null).ShellWindow.ActualWidth - rightView.ActualWidth - view.ActualWidth;
            view.Left = left; view.Top = 20;
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            RestoreEnv();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public virtual void RestoreEnv()
        {
            ((Window)this.View).Hide();
            ClearOldSelectedPOIItem();
        }

        private bool IsPOILayer(IShowLayer layer, out string imageName)
        {
            imageName = string.Empty;
            bool result;
            try
            {
                DisplayLayer displayLayer = layer as DisplayLayer;
                if (displayLayer == null)
                    return false;
                IFeatureLayer featureLayer = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>();
                if (featureLayer == null)
                    return false;
                ISimpleGeometryRender simpleGeometryRender = featureLayer.GetGeometryRender() as ISimpleGeometryRender;
                if (simpleGeometryRender == null)
                    return false;
                ImagePointSymbol imagePointSymbol = simpleGeometryRender.Symbol as ImagePointSymbol;
                if (imagePointSymbol == null)
                    return false;
                imageName = imagePointSymbol.ImageName;
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private void ShowHightLightPOI(IShowLayer layer, string oid, string imgName)
        {
            DisplayLayer displayLayer = layer as DisplayLayer;
            GviMap.MapControl.FeatureManager.SetFeatureVisibleMask(displayLayer.Fc, int.Parse(oid), gviViewportMask.gviViewNone);
            IPOI ipoi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ ) as IPOI;
            ipoi.ImageName = imgName;
            ipoi.Size = this.HighlightSize;
            IFieldInfoCollection fields = displayLayer.Fc.GetFields();
            IPoint point = displayLayer.Fc.GetRow(int.Parse(oid)).GetValue(fields.IndexOf("Geometry")) as IPoint;
            int indexName = fields.IndexOf("Name");
            if (indexName == -1)
                indexName = fields.IndexOf("BZM");

            string name = displayLayer.Fc.GetRow(int.Parse(oid)).GetValue(indexName).ToString();
            ipoi.SpatialCRS = GviMap.SpatialCrs;
            point.Project(GviMap.SpatialCrs);
            IPointExtension.SetPostion(ipoi, IPointExtension.ToVector3(point));
            ipoi.Name = name;
            //this.oldPoi = GviMap.MapControl.ObjectManager.CreateRenderPOIFromFDB(ipoi, displayLayer.Fc.FeatureDataSet);
            this.oldPoi = GviMap.MapControl.ObjectManager.CreateRenderPOI(ipoi);
            this.oldPoi.MaxVisibleDistance = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>().MaxVisibleDistance;
            this.oldPoi.MinVisiblePixels = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>().MinVisiblePixels;
            FdeCoreExtension.ReleaseComObject(fields);
        }
    }
}