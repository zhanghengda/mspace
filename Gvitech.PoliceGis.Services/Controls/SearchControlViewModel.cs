using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.Services.Controls
{
    public class SearchControlViewModel : BindableBase
    {
        public SearchControlViewModel()
        {
            this.IsUseBufferGeo = false;
            string text = ConfigurationManager.AppSettings["poiHighlightSize"];
            this.HighlightSize = ((!string.IsNullOrEmpty(text)) ? StringExtension.ParseTo<int>(text, 64) : 64);
            this.QueryLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(null);
            this.QueryCmd = new RelayCommand(() =>
            {
                bool flag2 = this.selectedLayer == null;
                if (!flag2)
                {
                    IShowLayer showLayer = this.selectedLayer.Parameters as IShowLayer;
                    IGeometry geo = null;
                    bool flag3 = this.IsUseBufferGeo;
                    if (this.IsUseBufferGeo)
                    {
                        if (ServiceManager.GetService<IQueryService>(null).Geomtry != null)
                            geo = ServiceManager.GetService<IQueryService>(null).Geomtry.Clone2(gviVertexAttribute.gviVertexAttributeNone);
                    }
                    else
                    {
                        geo = null;
                    }
                    DataTable dt = new DataTable();
                    bool flag5 = this.selectedLayer.Name.Equals("重点场所");
                    if (flag5)
                    {
                        List<LayerItemModel> layerItemModels = ServiceManager.GetService<IDataBaseService>(null).GetLayerItemModels(this.selectedLayer.Name);
                        bool flag6 = this.dny != null;
                        if (flag6)
                        {
                            this.dny.Clear();
                        }
                        string fields = FieldsFilterService.FieldsFilterService.GetDefault(null).GetFilterFields(this.selectedLayer.Name);
                        bool flag7 = IEnumerableExtension.HasValues<LayerItemModel>(layerItemModels);
                        if (flag7)
                        {
                            layerItemModels.ForEach(delegate (LayerItemModel ly)
                            {
                                dt.Merge(((IShowLayer)ly.Parameters).FuzzySearch(this.QueryKeys, fields, geo));
                                string guidString = ((IDisplayLayer)ly.Parameters).Fc.Guid.ToString();
                                CollectionExtension.AddEx<string, List<int>>(this.dny, guidString, new List<int>());
                                foreach (object obj in dt.Rows)
                                {
                                    DataRow dataRow = (DataRow)obj;
                                    CollectionExtension.AddEx<int>(this.dny[guidString], dataRow.GetHashCode());
                                }
                            });
                        }
                    }
                    else
                    {
                        dt = showLayer.FuzzySearch(this.QueryKeys, FieldsFilterService.FieldsFilterService.GetDefault(null).GetFilterFields(showLayer.AliasName), geo);
                    }
                    this.ResultsSource = ((dt != null) ? dt.DefaultView : null);
                    bool flag8 = dt == null || dt.Rows.Count == 0;
                    if (flag8)
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Seachnull"));
                    }
                }
            });
            bool flag = IEnumerableExtension.HasValues<LayerItemModel>(this.QueryLayers);
            if (flag)
            {
                this.SelectedLayer = this.QueryLayers.FirstOrDefault<LayerItemModel>();
            }
        }

        public bool IsUseBufferGeo
        {
            get
            {
                return this.isUseBufferGeo;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<bool>(ref this.isUseBufferGeo, value, "IsUseBufferGeo");
            }
        }

        public Visibility IsBufferGeoVisible
        {
            get
            {
                return this.isBufferGeoVisible;
            }
            set
            {
                bool flag = value == Visibility.Collapsed;
                if (flag)
                {
                    this.IsUseBufferGeo = false;
                }
                else
                {
                    this.IsUseBufferGeo = true;
                }
                base.SetAndNotifyPropertyChanged<Visibility>(ref this.isBufferGeoVisible, value, "IsBufferGeoVisible");
            }
        }

        public string QueryKeys
        {
            get
            {
                return this.queryKeys;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this.queryKeys, value, "QueryKeys");
            }
        }

        public ICommand QueryCmd { get; set; }

        public IEnumerable ResultsSource
        {
            get
            {
                return this.resultsSource;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<IEnumerable>(ref this.resultsSource, value, "ResultsSource");
            }
        }

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
                IShowLayer showLayer = (IShowLayer)this.SelectedLayer.Parameters;
                bool flag = this.SelectedPOIItem != null;
                if (flag)
                {
                    var oid = showLayer.GetFid();
                    string text = this.SelectedPOIItem.Row[oid].ToString();
                    bool flag2 = showLayer == null && this.SelectedLayer.Name.Equals("重点场所");
                    if (flag2)
                    {
                        string guid = string.Empty;
                        bool flag3 = this.dny != null;
                        if (flag3)
                        {
                            guid = this.dny.FirstOrDefault((KeyValuePair<string, List<int>> kvp) => kvp.Value.Contains(this.SelectedPOIItem.Row.GetHashCode())).Key;
                        }
                        showLayer = ServiceManager.GetService<IDataBaseService>(null).GetDisPlayLayerByFCGuid(guid);
                    }
                    bool flag4 = showLayer == null;
                    if (!flag4)
                    {
                        showLayer.FlyToFeature(text, GviMap.MapControl.Camera);
                        string empty = string.Empty;
                        bool flag5 = this.IsPOILayer(showLayer, out empty);
                        if (flag5)
                        {
                            this.ShowHightLightPOI(showLayer, text, empty);
                        }
                        else
                        {
                            showLayer.HighLightFeature(text, GviMap.MapControl.FeatureManager, 4294901760u);
                        }
                        this.oldSelectdItem = new KeyValuePair<string, IShowLayer>(text, showLayer);
                    }
                }
            }
        }

        public List<LayerItemModel> QueryLayers
        {
            get
            {
                return this.queryLayers;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<List<LayerItemModel>>(ref this.queryLayers, value, "QueryLayers");
            }
        }

        public LayerItemModel SelectedLayer
        {
            get
            {
                return this.selectedLayer;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<LayerItemModel>(ref this.selectedLayer, value, "SelectedLayer");
                this.QueryKeys = string.Empty;
                this.ResultsSource = null;
                GviMap.MapControl.FeatureManager.UnhighlightAll();
                this.ClearOldSelectedPOIItem();
            }
        }

        private void HightLightResult(IEnumerable resultsSource)
        {
            bool flag = resultsSource == null;
            if (!flag)
            {
                bool flag2 = this.selectedLayer == null;
                if (!flag2)
                {
                    IShowLayer showLayer = (IShowLayer)this.SelectedLayer.Parameters;
                    bool flag3 = showLayer == null;
                    if (!flag3)
                    {
                        IEnumerator enumerator = resultsSource.GetEnumerator();
                        List<string> list = new List<string>();
                        while (enumerator.MoveNext())
                        {
                            object obj = enumerator.Current;
                            DataRowView dataRowView = obj as DataRowView;
                            bool flag4 = dataRowView == null;
                            if (!flag4)
                            {
                                CollectionExtension.AddEx<string>(list, dataRowView["oid"].ToString());
                            }
                        }
                        GviMap.MapControl.FeatureManager.UnhighlightAll();
                        showLayer.HighLightFeatures(list.ToArray(), GviMap.MapControl.FeatureManager, 4294901760u);
                    }
                }
            }
        }

        private void UnHightLightResult()
        {
            bool flag = this.selectedLayer == null;
            if (!flag)
            {
                IShowLayer showLayer = (IShowLayer)this.SelectedLayer.Parameters;
                bool flag2 = showLayer == null;
                if (!flag2)
                {
                    showLayer.UnHighLightFeatureClass(GviMap.MapControl.FeatureManager);
                }
            }
        }

        private void ShowHightLightPOI(IShowLayer layer, string oid, string imgName)
        {
            DisplayLayer displayLayer = layer as DisplayLayer;
            GviMap.MapControl.FeatureManager.SetFeatureVisibleMask(displayLayer.Fc, int.Parse(oid), gviViewportMask.gviViewNone);
            IPOI ipoi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
            ipoi.Position.Z = 6;
            ipoi.ImageName = imgName;
            ipoi.Size = this.HighlightSize;
            IFieldInfoCollection fields = displayLayer.Fc.GetFields();
            IPoint point = displayLayer.Fc.GetRow(int.Parse(oid)).GetValue(fields.IndexOf("Geometry")) as IPoint;
            int indexName = fields.IndexOf("Name");
            if (indexName == -1)
                indexName = fields.IndexOf("BZM");
            string name = displayLayer.Fc.GetRow(int.Parse(oid)).GetValue(indexName)?.ToString();
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
                string text;
                bool flag2 = this.IsPOILayer(this.oldSelectdItem.Value, out text);
                if (flag2)
                {
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

        private Dictionary<string, List<int>> dny = new Dictionary<string, List<int>>();

        private readonly int HighlightSize;

        private IRenderPOI oldPoi;

        private bool isUseBufferGeo;

        private Visibility isBufferGeoVisible;

        private string queryKeys;

        private KeyValuePair<string, IShowLayer> oldSelectdItem;

        private IEnumerable resultsSource;

        private DataRowView selectedPOIItem;

        private List<LayerItemModel> queryLayers;

        private LayerItemModel selectedLayer;
    }
}