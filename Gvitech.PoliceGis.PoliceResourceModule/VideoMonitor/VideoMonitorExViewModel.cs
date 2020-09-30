using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.PoliceResourceModule.VideoMonitor;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.FieldsFilterService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoliceResourceModule
{
    public class VideoMonitorExViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }


        public VideoMonitorExViewModel() : base()
        {
            string text = ConfigurationManager.AppSettings["poiHighlightSize"];
            this.HighlightSize = ((!string.IsNullOrEmpty(text)) ? StringExtension.ParseTo<int>(text, 64) : 64);
            //webVideoVm = new WebVideoViewModel();
        }

        private IRenderPOI oldPoi;
        private readonly int HighlightSize;
        private string queryKeys;
        private IEnumerable resultsSource;
        private DataRowView selectedPOIItem;
        private IDisplayLayer _layer;
        private KeyValuePair<string, IShowLayer> oldSelectdItem;
        //private WebVideoViewModel webVideoVm;

        [XmlIgnore]
        public string QueryKeys
        {
            get { return this.queryKeys; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.queryKeys, value, "QueryKeys"); }
        }

        [XmlIgnore]
        public ICommand QueryCmd { get; set; }
        [XmlIgnore]
        public IEnumerable ResultsSource
        {
            get { return this.resultsSource; }
            set { base.SetAndNotifyPropertyChanged<IEnumerable>(ref this.resultsSource, value, "ResultsSource"); }
        }

        public override FrameworkElement CreatedView()
        {
            return new VideoMonitorExView()
            {
                Owner = Application.Current.MainWindow
            };
        }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                ((Window)this.View).Hide();
                ClearOldSelectedPOIItem();

            });
            _layer = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
            this.QueryCmd = new RelayCommand(() =>
            {
                if (_layer == null)
                    _layer = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCAliasName("视频监控");
                DataTable dt = new DataTable();
                dt = _layer.FuzzySearch(this.QueryKeys);
                this.ResultsSource = ((dt != null) ? dt.DefaultView : null);
                if (dt == null || dt.Rows.Count == 0)
                    MessageBox.Show(Application.Current.MainWindow, "查询结果为空", "提示:", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK);
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ((Window)this.View).Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();

            ((Window)this.View).Hide();
            ClearOldSelectedPOIItem();
        }

        public override void Reset()
        {
            base.Reset();
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
        [XmlIgnore]
        public DataRowView SelectedPOIItem
        {
            get
            {
                return this.selectedPOIItem;
            }
            set
            {
                this.ClearOldSelectedPOIItem();
                this.selectedPOIItem = value;
                if (this.selectedPOIItem == null)
                    return;
                string oid = this.SelectedPOIItem.Row["oid"].ToString();
                _layer.FlyToFeature(oid, GviMap.MapControl.Camera);
                string imgName = string.Empty;
                IsPOILayer(_layer, out imgName);
                this.ShowHightLightPOI(_layer, oid, imgName);
                this.oldSelectdItem = new KeyValuePair<string, IShowLayer>(oid, _layer);
                string videoPath = ServiceManager.GetService<ICameraInfoService>(null).GetVideoPath(oid);
                VideoMonitorExView videoMonitorView = (VideoMonitorExView)base.View;
                if (oid == "1")
                {
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_大门.avi";
                    //webVideoVm.SetVideoPath(4);
                }
                else if (oid == "2")
                {
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_娱乐区.avi";
                }
                else if (oid == "3")
                {
                    videoMonitorView.VideoPath = AppDomain.CurrentDomain.BaseDirectory + @"项目数据\监控视频\JG_停车场.avi";
                }
                else
                {
                    videoMonitorView.VideoPath = videoPath;
                }
            }
        }


        private bool IsPOILayer(IShowLayer layer, out string imageName)
        {
            imageName = string.Empty;
            bool result;
            try
            {
                DisplayLayer displayLayer = layer as DisplayLayer;
                bool flag = displayLayer == null;
                if (flag)
                {
                    result = false;
                }
                else
                {
                    IFeatureLayer featureLayer = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>();
                    bool flag2 = featureLayer == null;
                    if (flag2)
                    {
                        result = false;
                    }
                    else
                    {
                        ISimpleGeometryRender simpleGeometryRender = featureLayer.GetGeometryRender() as ISimpleGeometryRender;
                        bool flag3 = simpleGeometryRender == null;
                        if (flag3)
                        {
                            result = false;
                        }
                        else
                        {
                            ImagePointSymbol imagePointSymbol = simpleGeometryRender.Symbol as ImagePointSymbol;
                            bool flag4 = imagePointSymbol == null;
                            if (flag4)
                            {
                                result = false;
                            }
                            else
                            {
                                imageName = imagePointSymbol.ImageName;
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }



    }
}
