using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.ToolModule.LayerController;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Mspace.PoiManagerModule;
using Mmc.Mspace.PoiManagerModule.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.MultiViewCompare
{


    public class CompareViewModel : CheckedToolItemModel
    {
        private CompareView compareView;
        private LayerViewModel layerViewModel0;
        private LayerViewModel layerViewModel1;
        private int widthWiew;
        private PoiDetailViewModel poiDetailViewModel;
        [XmlIgnore]
        public ICommand ExitCmd { get; set; }

        [XmlIgnore]
        public ICommand CreatePoiCmd { get; set; }

        [XmlIgnore]
        public ICommand DeletePoiCmd { get; set; }

        [XmlIgnore]
        public ICommand EditPoiCmd { get; set; }

        [XmlIgnore]
        public LayerViewModel LayerViewModel0
        {
            get { return this.layerViewModel0; }
            set { base.SetAndNotifyPropertyChanged<LayerViewModel>(ref this.layerViewModel0, value, "LayerViewModel0"); }
        }

        [XmlIgnore]
        public LayerViewModel LayerViewModel1
        {
            get { return this.layerViewModel1; }
            set { base.SetAndNotifyPropertyChanged<LayerViewModel>(ref this.layerViewModel1, value, "LayerViewModel1"); }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
           
            this.ExitCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });

            this.DeletePoiCmd = new RelayCommand(() =>
            {
                var jsInvoker = ServiceManager.GetService<IShellService>(null).LeftWindow as IJsScriptInvokerService;
                jsInvoker.deleteMarkerPoi("8");
            });
            this.EditPoiCmd = new RelayCommand(() =>
            {
                
            });
            CreatePoiCmd = new RelayCommand(() =>
            {
                poiDetailViewModel = new PoiDetailViewModel();
                poiDetailViewModel.RefreshPoiList -= new Action<MarkerModel>(OnUpdatedPoi);
                poiDetailViewModel.RefreshPoiList += new Action<MarkerModel>(OnUpdatedPoi);
                poiDetailViewModel.status = (int)OperationStatus.ADD;
                poiDetailViewModel.OnChecked();

            });
            widthWiew = 350;


        }
        public void OnUpdatedPoi(MarkerModel poiInfo)
        {
            UpdatePoiLayerTree(poiInfo, this.layerViewModel0, 0);
            UpdatePoiLayerTree(poiInfo, this.layerViewModel1, 1);
        }

        private void UpdatePoiLayerTree(MarkerModel poiInfo, LayerViewModel layerViewModel, int viewPortIndex)
        {
            var poiLayerRoot = layerViewModel.PoiGroupLayer;
            var tagLayer = poiLayerRoot.Children.FirstOrDefault(p => p.Name == poiInfo.cat_Name);
            if (tagLayer == null)
            {
                tagLayer = new GroupLayerItemModel
                {
                    Name = poiInfo.cat_Name
                };
                poiLayerRoot.Children.Add(tagLayer);
            }
            if (IsExitPoiItem(tagLayer.Children, poiInfo, out PoiItem poiItem))
            {
                poiItem.Name = poiInfo.title;
                poiItem.Tag = poiInfo.cat_Name;
            }
            else
            {
                var newPoiItem = new PoiItem()
                {
                    PoiId = poiInfo.id.ToString(),
                    Name = poiInfo.title,
                    Tag = poiInfo.cat_Name,
                    ViewPort = viewPortIndex,
                };
                tagLayer.Children.Add(newPoiItem);
            }
            layerViewModel.PoiGroupLayer = poiLayerRoot;
        }

        public bool IsExitPoiItem(ObservableCollection<LayerItemModel> poiLayers, MarkerModel poiInfo, out PoiItem poiItem)
        {
            poiItem = null;
            foreach (var item in poiLayers)
            {
                var poiItem1 = item as PoiItem;
                if (poiItem1.PoiId == poiInfo.id.ToString())
                {
                    poiItem = poiItem1;
                    return true;
                }
            }
            return false;
        }


        public override void OnChecked()
        {
            base.OnChecked();
            widthWiew = 350;
            //
            LoadPoi();
            if (layerViewModel0 == null)
                layerViewModel0 = new LayerViewModel(0) { Name = Helpers.ResourceHelper.FindKey("Leftlayer") };
            layerViewModel0.init();
            if (layerViewModel1 == null)
                layerViewModel1 = new LayerViewModel(1) { Name = Helpers.ResourceHelper.FindKey("Rightlayer") };
            layerViewModel1.init();
           
            ShowView();
            //开启双屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
            GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;
        }


        private void LoadPoi()
        {
            //初始化类型
            try
            {
                var httpService = new HttpService();
                httpService.Token = HttpServiceUtil.Token;
                var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                var poiUrl = json.poiUrl;
                string url = string.Format("{0}/getcategory", poiUrl);
                var resutl = httpService.HttpRequestAsync(url);
                var poiTypes = JsonUtil.DeserializeFromString<List<PoiType>>(resutl);
                var dic = new Dictionary<int, PoiType>();
                foreach (var item in poiTypes)
                    dic.Add(item.cat_id, item);
                url = string.Format("{0}/getmarkerlist", poiUrl);
                var makerList = httpService.HttpRequestAsync(url);
                var poiDetails = JsonUtil.DeserializeFromString<List<PoiDetail>>(makerList);

                foreach (var maker in poiDetails)
                {
                    url = string.Format("{0}/clickmarker?type=1&marker_id={1}", poiUrl, maker.marker_id);
                    var resPos = httpService.HttpRequestAsync(url);
                    var poiPoss = JsonUtil.DeserializeFromString<List<PoiPositon>>(resPos);
                    if (poiPoss.HasValues())
                    {
                        if (!dic.ContainsKey(maker.cat_id))
                            continue;
                        var poiPos = poiPoss[0];
                        var poi = GviMap.PoiManager.CreatePoi(poiPos.lng, poiPos.lat, poiPos.alt, dic[maker.cat_id].cat_url, maker.title);
                        var rPoi = GviMap.PoiManager.CreateRPoi(poi);
                        GviMap.PoiManager.AddPoi(maker.marker_id, dic[maker.cat_id].cat_name, rPoi);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();

            var imageLayers = DataBaseService.Instance.GetImageLayers();
            foreach (var item in imageLayers)
                item.Layer.VisibleMask = gviViewportMask.gviViewAllNormalView;

            GviMap.PoiManager.Clear();
            //退出恢复单屏模式
            GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportSinglePerspective;
            HideView();
        }

        private void HideView()
        {
            compareView?.Hide();

            ServiceManager.GetService<IShellService>(null).LeftWindow.Show();
            ServiceManager.GetService<IShellService>(null).RightToolMenu.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).BottomToolMenu.Visibility = System.Windows.Visibility.Visible;
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Visible;

            ServiceManager.GetService<IMaphostService>(null).MapWindow.Width += widthWiew;
            ServiceManager.GetService<IMaphostService>(null).MapWindow.Left = 0;
        }

        //
        private void ShowView()
        {
            //其他组建隐藏
            ServiceManager.GetService<IShellService>(null).LeftWindow.Hide();
            ServiceManager.GetService<IShellService>(null).ShellMenu.Visibility = System.Windows.Visibility.Hidden;
            ServiceManager.GetService<IShellService>(null).RightToolMenu.Visibility = System.Windows.Visibility.Hidden;

            ServiceManager.GetService<IMaphostService>(null).MapWindow.Width -= widthWiew;
            ServiceManager.GetService<IMaphostService>(null).MapWindow.Left = widthWiew;
            if (this.compareView == null)
            {
                this.compareView = new CompareView();
                this.compareView.Left = 0;
                this.compareView.Top = 0;
            }

            this.compareView.MaxWidth = ServiceManager.GetService<IMaphostService>(null).MapWindow.Left;
            this.compareView.Height = ServiceManager.GetService<IMaphostService>(null).MapWindow.ActualHeight;
            this.compareView.Owner = ServiceManager.GetService<IShellService>(null).ShellWindow;
            this.compareView.DataContext = this;
            ServiceManager.GetService<IShellService>(null).CompareView = compareView;
            this.compareView.Show();
        }
    }
}
