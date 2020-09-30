using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.JscriptInvokeService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;

namespace FireControlModule
{
    public class SupervisoryReviewViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}fireApplication/?pageName={1}", json1.leftViewUrl, "supervision");
            webView.RequestUrl(url);
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            var webView = ServiceManager.GetService<IShellService>(null).LeftWindow as IWebView;
            string url = json1.leftViewUrl;
            webView.RequestUrl(url);
        }

        //private int _x;
        //private int _y;
        //private List<IDisplayLayer> _layers = new List<IDisplayLayer>();

        //public ICommand CheckCommand { get; set; }

        //private BuildDetailViewModel viewModle;
        //private StatisticLegened statisticLegened;
        //public override void Initialize()
        //{
        //    base.Initialize();
        //    base.ViewType = ViewType.CheckedIcon;
        //    this._layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
        //    //this.Items.Clear();

        //    GviMap.MapControl.InteractMode = gviInteractMode.gviInteractNormal;
        //    GviMap.AxMapControl.RcMouseHover += new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);

        //    ServiceManager.GetService<IShellService>(null).LeftWindow.Hide();
        //    ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;

        //    this.CheckCommand = new RelayCommand(delegate (object p)
        //    {
        //        LayerItemModel layerItemModel = (LayerItemModel)p;
        //        ThreeColorItemData thData = ThreeColorItemData.GetSupervisoryReviewTestItems();
        //        statisticLegened = new StatisticLegened
        //        {
        //            Owner = Application.Current.MainWindow,
        //            DataContext = thData
        //        };
        //        statisticLegened.Show();

        //        //Chromatography lengend = ServiceManager.GetService<IStatisticLayerService>(null).GetLengend(layerItemModel.Name.ToString());
        //        //bool flag = ToggleLayerControllerBase.LegenedView == null;
        //        //if (flag)
        //        //{
        //        //    ToggleLayerControllerBase.LegenedView = new StatisticLegened
        //        //    {
        //        //        Owner = Application.Current.MainWindow
        //        //    };
        //        //}
        //        //ToggleLayerControllerBase.LegenedView.DataContext = lengend;
        //        //ToggleLayerControllerBase.LegenedView.Visibility = (layerItemModel.IsVisible ? Visibility.Visible : Visibility.Collapsed);
        //        //bool isVisible = layerItemModel.IsVisible;
        //    }} }

        ////public override void Reset()
        ////{
        ////    base.Reset();
        ////}

        //private List<StaticTypeItemModel> items;
        //public List<StaticTypeItemModel> Items
        //{
        //    get
        //    {
        //        return this.items;
        //    }
        //    set
        //    {
        //        base.SetAndNotifyPropertyChanged<List<StaticTypeItemModel>>(ref this.items, value, "Items");
        //    }
        //}

        //public override FrameworkElement CreatedView()
        //{
        //    bool flag = !IEnumerableExtension.HasValues<StaticTypeItemModel>(this.Items);
        //    if (flag)
        //    {
        //        this.Items = new List<StaticTypeItemModel>();
        //        this.Items.Add(new StaticTypeItemModel() { Name = "监督抽查" });
        //        this.Items.Add(new StaticTypeItemModel() { Name = "其他检查" });
        //        this.Items.Add(new StaticTypeItemModel() { Name = "派出所检查" });
        //        this.items[0].IsChecked = true;
        //    }
        //    ToggleLayersView toggleLayersView = new ToggleLayersView();
        //    IconPopupButton iconPopupButton = toggleLayersView.FindName("layers") as IconPopupButton;
        //    bool flag2 = iconPopupButton != null;
        //    if (flag2)
        //    {
        //        iconPopupButton.Placement = PlacementMode.Right;
        //    }
        //    return toggleLayersView;
        //}

        ////public override void OnUnchecked()
        ////{
        ////    statisticLegened.Close();
        ////    this.UnHighLightBuilding();
        ////    if (viewModle != null)
        ////        viewModle.OnUnchecked();
        ////    GviMap.AxMapControl.RcMouseHover -= new _IRenderControlEvents_RcMouseHoverEventHandler(RenderControl_RcMouseHover);
        ////    GviMap.MapControl.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;
        ////    ServiceManager.GetService<IShellService>(null).LeftWindow.Show();
        ////    ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;

        ////    foreach (var item in this.items)
        ////        item.IsVisible = false;
        ////}

        //protected virtual bool RenderControl_RcMouseHover(uint flags, int x, int y)
        //{
        //    bool flag = this._x == x && this._y == y;
        //    bool result;
        //    if (flag)
        //    {
        //        result = false;
        //    }
        //    else
        //    {
        //        this.UnHighLightBuilding();
        //        this._x = x;
        //        this._y = y;
        //        bool flag3 = this._layers.Count == 0;
        //        if (flag3)
        //        {
        //            this._layers = ServiceManager.GetService<IDataBaseService>(null).GetActualityLayers();
        //        }
        //        IPoint point;
        //        IPickResult pickResult = GviMap.MapControl.Camera.ScreenToWorld(x, y, out point);
        //        bool flag4 = pickResult is IFeatureLayerPickResult;
        //        if (flag4)
        //        {
        //            IFeatureLayerPickResult featureLayerPickResult = pickResult as IFeatureLayerPickResult;
        //            int featureId = featureLayerPickResult.FeatureId;
        //            string fcGuid = featureLayerPickResult.FeatureLayer.Guid.ToString();
        //            foreach (IDisplayLayer displayLayer in this._layers)
        //            {
        //                IFeatureLayer featureLayer = displayLayer.FLyers.FirstOrDefault<IFeatureLayer>();
        //                bool flag5 = featureLayer != null && displayLayer.Name == "JZDK" && featureLayer.Guid.ToString() == fcGuid;
        //                if (flag5)
        //                {
        //                    //displayLayer.HighLightFeature(featureId.ToString(), GviMap.MapControl.FeatureManager, 4294901760u);
        //                    this._oldSelectFc = new KeyValuePair<string, IDisplayLayer>(featureId.ToString(), displayLayer);
        //                    IFieldInfoCollection fields = this._oldSelectFc.Value.Fc.GetFields();
        //                    int position = fields.IndexOf("Name");
        //                    // int position = fields.IndexOf("地址");
        //                    IRowBuffer row = this._oldSelectFc.Value.Fc.GetRow(featureId);
        //                    object obj = row.GetValue(position) ?? string.Empty;
        //                    string buildCode = obj.ToString();
        //                    if (viewModle == null)
        //                    {
        //                        viewModle = new BuildDetailViewModel();
        //                        viewModle.Initialize();
        //                    }
        //                    viewModle.BuildCode = buildCode;
        //                    viewModle.OnChecked();
        //                    return false;
        //                }
        //            }
        //        }
        //        result = false;
        //    }
        //    return result;
        //}

        //private void UnHighLightBuilding()
        //{
        //    //bool flag = !string.IsNullOrEmpty(this._oldSelectFc.Key);
        //    //if (flag)
        //    //{
        //    //    this._oldSelectFc.Value.UnHighLightFeature(this._oldSelectFc.Key, Mmc.Framework.Services.GviMap.MapControl.FeatureManager);
        //    //}
        //}

        //private KeyValuePair<string, IDisplayLayer> _oldSelectFc;
    }
}