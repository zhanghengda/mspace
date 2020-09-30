using ApplicationConfig;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Windows.Utils;
using Microsoft.Win32;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class MapManagementVModel : BaseViewModel
    {
        public ICommand SearchCommand { get; set; }
        private UserInfo _userInfo;
        private List<IRenderLayer> _renderLayers;
        private RelayCommand<object> _mouseDownCommand;
        private readonly ExportProgressView progressView;
        private int currentIndex = 0;
        private static string layerGuids = null;
        private Dictionary<string, gviViewportMask> _rLayersRenderableStatus;

        private int _splitScreenType = 0;
        
        /// <summary>
        /// 分屏类型 1单屏 2双屏 3三屏 4四屏
        /// </summary>
        public int SplitScreenType
        {
            get { return _splitScreenType; }
            set { _splitScreenType = value; OnPropertyChanged("SplitScreenType"); }
        }


        private bool _twoScreenStatus;

        public bool TwoScreenStatus
        {
            get { return _twoScreenStatus; }
            set
            {
                _twoScreenStatus = value;
                if (_twoScreenStatus) SplitScreenType = 1;
                OnPropertyChanged("TwoScreenStatus");
            }
        }
        private bool _threeScreenStatus;

        public bool ThreeScreenStatus
        {
            get { return _threeScreenStatus; }
            set
            {
                _threeScreenStatus = value;
                if (_threeScreenStatus) SplitScreenType = 2;
                OnPropertyChanged("ThreeScreenStatus");
            }
        }
        private bool _fourScreenStatus;

        public bool FourScreenStatus
        {
            get { return _fourScreenStatus; }
            set
            {
                _fourScreenStatus = value;
                if (_fourScreenStatus) SplitScreenType = 3;
                OnPropertyChanged("FourScreenStatus");
            }
        }

        private Visibility _screenVisibility = Visibility.Collapsed;

        public Visibility ScreenVisibility
        {
            get { return _screenVisibility; }
            set {
                _screenVisibility = value;
                OnPropertyChanged("ScreenVisibility");
                if(ScreenVisibility== Visibility.Collapsed)
                {
                    UnScreenVisibility= Visibility.Visible;
                }
                else
                {
                    UnScreenVisibility = Visibility.Collapsed;
                }
               
            }
        }
        private Visibility _unScreenVisibility = Visibility.Visible;

        public Visibility UnScreenVisibility
        {
            get { return _unScreenVisibility; }
            set { _unScreenVisibility = value; OnPropertyChanged("UnScreenVisibility"); }
        }

        public RelayCommand<object> MouseDownCommand
        {
            get { return _mouseDownCommand ?? (_mouseDownCommand = new RelayCommand<object>(OnMouseDownCommand)); }
            set { _mouseDownCommand = value; }
        }

        private RelayCommand<RenderLayerModel> _selectCommand;

        public RelayCommand<RenderLayerModel> SelectCommand
        {
            get { return _selectCommand ?? (_selectCommand = new RelayCommand<RenderLayerModel>(OnSelectCommand)); }
            set { _selectCommand = value; }
        }
           

        private RelayCommand<object> _addCommand;

        public RelayCommand<object> AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand<object>(OnAddCommand)); }
            set { _addCommand = value; }
        }

        private RelayCommand<object> _deleteCommand;

        public RelayCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(OnDeleteCommad)); }
            set { _deleteCommand = value; }
        }
        private RelayCommand<object> _changeImageAlphaCommand;

        public RelayCommand<object> ChangeImageAlphaCommand
        {
            get { return _changeImageAlphaCommand ?? (_changeImageAlphaCommand = new RelayCommand<object>(OnChangeImageAlphaCommand)); }
            set { _changeImageAlphaCommand = value; }
        }
        
        private RelayCommand<object> _checkMapCommand;

        public RelayCommand<object> CheckMapCommand
        {
            get { return _checkMapCommand ?? (_checkMapCommand = new RelayCommand<object>(OnCheckMapCommand)); }
            set { _checkMapCommand = value; }
        }

        private RelayCommand<object> _symbolicCommand;

        public RelayCommand<object> SymbolicCommand
        {
            get { return _symbolicCommand ?? (_symbolicCommand = new RelayCommand<object>(OnSymbolicCommand)); }
            set { _symbolicCommand = value; }
        }

        private RelayCommand<string> _splitScreenCommand;

        public RelayCommand<string> SplitScreenCommand
        {
            get { return _splitScreenCommand ?? (_splitScreenCommand = new RelayCommand<string>(OnSplitScreenCommand)); }
            set { _splitScreenCommand = value; }
        }


        private ObservableCollection<RenderLayerModel> _mapSource;

        public ObservableCollection<RenderLayerModel> MapSource
        {
            get { return _mapSource ?? (_mapSource = new ObservableCollection<RenderLayerModel>()); }
            set { _mapSource = value; OnPropertyChanged("MapSource"); }
        }
        ObservableCollection<RenderLayerModel> tempMapSource = null;
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged("SearchText"); }
        }
        //private ObservableCollection<LoadType> _typeCollection = new ObservableCollection<LoadType>();
        //public ObservableCollection<LoadType> TypeCollection
        //{
        //    get { return _typeCollection; }
        //    set
        //    {
        //        _typeCollection = value;
        //        OnPropertyChanged("TypeCollection");
        //    }
        //}
        public ICommand AddDataSourceCmd { get; set; }
        public MapManagementVModel()
        {
            _userInfo = CacheData.UserInfo;
            progressView = new ExportProgressView();

            _rLayersRenderableStatus = new Dictionary<string, gviViewportMask>();
            if (_renderLayers == null)
            {
                _renderLayers = new List<IRenderLayer>();
                var tileLayers = DataBaseService.Instance.GetTileLayers();
                var imageLayers = DataBaseService.Instance.GetImageLayers();
                var shpLayers = DataBaseService.Instance.GetShpLayers();
                var actualLayers = DataBaseService.Instance.GetActualityLayers();
                if (shpLayers != null)
                {
                    foreach (var item in shpLayers)
                        _renderLayers.Add(item as IRenderLayer);
                }
                if (actualLayers != null)
                {
                    foreach (var item in actualLayers)
                        _renderLayers.Add(item as IRenderLayer);
                }
                _renderLayers.AddRange(tileLayers);
                _renderLayers.AddRange(imageLayers);

                Task.Run(() =>
                {
                    GetMapSource();
                });
            }
            this.AddDataSourceCmd = new RelayCommand(AddDataSource);
            this.SearchCommand = new RelayCommand(OnSearchCommand);
            Messenger.Messengers.Register<bool>(CommonContract.MessengerKey.Splitscreen.ToString(), (t) =>
            {
                ScreenVisibility = t ? Visibility.Visible : Visibility.Collapsed;
                if (t)
                {
                    SetCompareViewState("2");
                    TwoScreenStatus = true;
                }
                else
                {
                    SplitScreenType = 0;
                }
            });
            Messenger.Messengers.Register<string>(CommonContract.MessengerKey.Openscreen.ToString(), (t) =>
            {
                SetCompareViewState(t);
            });
            Messenger.Messengers.Register<IRenderLayer>(CommonContract.MessengerKey.FlyToRederLayer.ToString(), (t) =>
            {
                _renderLayers.Add(t);
                flyToRederLayer(t.Guid);
            });
            Messenger.Messengers.Register<string>(CommonContract.MessengerKey.DeleteRederLayer.ToString(), (t) =>
            {
                DeleteRenderLayer(t);
            });
            Messenger.Messengers.Register(CommonContract.RenderLayerStatus.AllHide.ToString(), () =>
            {
                HideRenderLayer();
            });
            Messenger.Messengers.Register(CommonContract.RenderLayerStatus.RecoveryRenderStatus.ToString(), () =>
            {
                RecoverRenderLayer();
            });
            Messenger.Messengers.Register(CommonContract.RenderLayerStatus.SaveStatus.ToString(), () =>
            {
                SaveRenderLayersStatus();
            });
        }

        protected override void Loaded()
        {
            base.Loaded();
        }
        protected override void Unloaded()
        {
            base.Unloaded();
        }

        public void LoadData()
        {
           
        }

        private void OnSplitScreenCommand(string paramater)
        {
            SetCompareViewState(paramater);
        }

        /// <summary>
        /// 设置分屏状态
        /// </summary>
        /// <param name="compareViewState">分屏状态，2表示2屏，以此类推</param>
        public void SetCompareViewState(string compareViewState)
        {
            if (compareViewState == "2")
            {
                //开启双屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1R1;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1;

            }
            else if (compareViewState == "3")
            {
                //开启三屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportL1M1R1;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView2;
            }
            else if (compareViewState == "4")
            {
                //开启四屏模式
                GviMap.Viewport.ViewportMode = gviViewportMode.gviViewportQuad;
                GviMap.Viewport.CameraViewBindMask = gviViewportMask.gviView0 | gviViewportMask.gviView1 | gviViewportMask.gviView2 | gviViewportMask.gviView3;
            }

            //foreach (var layer in _renderLayers)
            //{

            //    if (layer.Renderable == null)
            //        continue;
            //    int flag = (int)layer.Renderable.VisibleMask;
            //    if (flag % 2 == 1)
            //    {
            //        layer.Renderable.VisibleMask = gviViewportMask.gviViewAllNormalView;
            //    }
            //    else if (layer.Renderable.VisibleMask != gviViewportMask.gviViewAllNormalView)
            //    {
            //        layer.Renderable.VisibleMask = gviViewportMask.gviViewNone;
            //    }
            //}
        }

        /// <summary>
        /// renderlayer 隐藏以及恢复显示
        /// </summary>
        /// <param name="hide"></param>
        public void HideRenderLayer()
        {
            if (_renderLayers?.Count < 0)
                return;
            foreach (var layer in _renderLayers)
            {
                if (layer.AliasName == "天地图" || layer.AliasName == "img")
                    continue;

                if (layer.Renderable != null)
                    layer.Renderable.VisibleMask = gviViewportMask.gviViewNone;
            }

        }

        public void RecoverRenderLayer()
        {
            if (_renderLayers?.Count < 0)
                return;
            foreach (var layer in _renderLayers)
            {
                if (_rLayersRenderableStatus.ContainsKey(layer.Guid.ToString()))
                    layer.Renderable.VisibleMask = _rLayersRenderableStatus[layer.Guid.ToString()];
            }

        }


        public void SaveRenderLayersStatus()
        {
            _rLayersRenderableStatus?.Clear();
            if (_renderLayers?.Count < 0)
                return;
            foreach (var layer in _renderLayers)
            {
                if (layer.Guid != null && layer.Renderable != null && !_rLayersRenderableStatus.ContainsKey(layer.Guid.ToString()))
                    _rLayersRenderableStatus.Add(layer.Guid.ToString(), layer.Renderable.VisibleMask);
            }
        }
  
        private void GetMapSource()
        {
            List<RenderLayerModel> list = new List<RenderLayerModel>();
            //三维模型
            var modelRenderLayers = new RenderLayerModel()
            {
                ChildCount = 0,
                MenuLevel = "1",
                IsDisplay = false,
                IsChecked = true,
                DataPath = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("3dIcon"),
                Name = Helpers.ResourceHelper.FindKey("3dmodel") + "(0)",
                AliasName = Helpers.ResourceHelper.FindKey("3dmodel"),
                Guid = Guid.Parse("00000000-0000-0000-0000-000000000100").ToString(),
                LayerType = Common.CommonContract.RenderLayerType.DataSetGroupLayer,
                Rederlayers = new List<RenderLayerModel>(),
            };

            list.Add(modelRenderLayers);
            
            var actualLayers = DataBaseService.Instance.GetActualityLayers();     
            if (actualLayers != null)
            {
                Dictionary<string, Tuple<IDataSource, List<IDisplayLayer>>> dicModel = new Dictionary<string, Tuple<IDataSource, List<IDisplayLayer>>>();

                foreach (var item in actualLayers)
                    {
                        var key = item.Fc.DataSource.Guid.ToString();
                        if (!dicModel.ContainsKey(key))
                        {
                            dicModel.Add(key, new Tuple<IDataSource, List<IDisplayLayer>>(item.Fc.DataSource, new List<IDisplayLayer>()));
                          
                        }
                    dicModel[key].Item2.Add(item);
                }
                modelRenderLayers.Name = Helpers.ResourceHelper.FindKey("3dmodel") + "(" + dicModel.Count + ")";
                foreach (var key in dicModel.Keys)
                    {
                        modelRenderLayers.HasPathData = true;
                        modelRenderLayers.ChildCount++;
                        var dsName = dicModel[key].Item1.ConnectionInfo.GetDataSourceName();
                        var dataSourceRenderLayers = new RenderLayerModel()
                        {
                            HasPathData = true,
                            MenuLevel = "2",
                            IsDisplay = true,
                            IsChecked = true,
                            ChildCount = 0,
                            ParentName = modelRenderLayers.Name,
                            IsLocal = !dicModel[key].Item1.IsNetServer(),
                            Name = (!dicModel[key].Item1.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + dsName,
                            AliasName = (!dicModel[key].Item1.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + dsName,
                            Guid = key,
                            LayerType = Common.CommonContract.RenderLayerType.GroupLayer,
                            Rederlayers = new List<RenderLayerModel>()
                        };
                        list.Add(dataSourceRenderLayers);
                        foreach (var item in dicModel[key].Item2)
                        {
                            dataSourceRenderLayers.ChildCount++;
                            list.Add(RenderLayerModel.CreateRenderLayer(RenderLayerDto.RenderLayerConvert(item as DisplayLayer), dataSourceRenderLayers.Name));
                        }
                    }
              
                
                 //   modelRenderLayers.Name = Helpers.ResourceHelper.FindKey("3dmodel") + "(" +"0" + ")";
               
              
            }
            var imgRenderLayers = new RenderLayerModel()
            {
                DataPath = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("image"),
                MenuLevel = "1",               
                ChildCount = 0,
                IsDisplay = false,
                IsChecked = true,
                HasPathData = false,
                Name =  Helpers.ResourceHelper.FindKey("Screenage") + "(0)",
            AliasName = Helpers.ResourceHelper.FindKey("Screenage"),
                Guid = Guid.Parse("00000000-0000-0000-0000-000000000300").ToString(),
                LayerType = Common.CommonContract.RenderLayerType.ImageGroupLayer,
                Rederlayers = new List<RenderLayerModel>()
            };
            list.Add(imgRenderLayers);
            var imageLayers = DataBaseService.Instance.GetImageLayers();
            if (imageLayers != null)
            {
                imgRenderLayers.Name = Helpers.ResourceHelper.FindKey("Screenage") + "(" + imageLayers.Count + ")";
           
            foreach (var item in imageLayers)
            {
                imgRenderLayers.HasPathData = true;
                imgRenderLayers.ChildCount++;
                list.Add(new RenderLayerModel()
                {
                    HasPathData = false,
                    AlphaBtnOn = true,
                    AlphaStation = "1",
                    MenuLevel = "2",
                    IsDisplay = true,
                    IsChecked = true,
                    ParentName = imgRenderLayers.Name,
                    IsLocal = item.IsLocal,
                    Name = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name,
                    AliasName = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.AliasName,
                    Guid = item.Guid,
                    LayerType = RenderLayerType.ImageLayer
                });
            }
            }
            var tileRenderLayers = new RenderLayerModel()
            {
                DataPath = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("photography"),
                MenuLevel = "1",
                IsDisplay = false,
                IsChecked = true,
                ChildCount = 0,
                HasPathData = false,
                Name = Helpers.ResourceHelper.FindKey("Obliquephotography") + "(0)",
                AliasName = Helpers.ResourceHelper.FindKey("Obliquephotography"),
                Guid = Guid.Parse("00000000-0000-0000-0000-000000000200").ToString(),
                LayerType = Common.CommonContract.RenderLayerType.TileGroupLayer,
                Rederlayers = new List<RenderLayerModel>()
            };
            list.Add(tileRenderLayers);
            var tileLayers = DataBaseService.Instance.GetTileLayers();
            if (tileLayers != null)
            {
                tileRenderLayers.Name = Helpers.ResourceHelper.FindKey("Obliquephotography") + "(" + tileLayers.Count + ")";
                 
            foreach (var item in tileLayers)
            {
                tileRenderLayers.HasPathData = true;
                tileRenderLayers.ChildCount++;
                list.Add(new RenderLayerModel()
                {
                    HasPathData = false,
                    MenuLevel = "2",
                    IsDisplay = true,
                    IsChecked = true,
                    IsSymbolic = true,
                    ParentName = tileRenderLayers.Name,
                    IsLocal = item.IsLocal,
                    Name = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name,
                    AliasName = (item.IsLocal ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.AliasName,
                    Guid = item.Guid,
                    LayerType = RenderLayerType.TileLayer,

                });
            }
            }
            //二维矢量
            var shpRenderLayers = new RenderLayerModel()
            {
                DataPath = (ImageSource)Helpers.ResourceHelper.FindResourceByKey("2dIcon"),
                MenuLevel = "1",
                IsDisplay = false,
                IsChecked = true,
                ChildCount = 0,
                HasPathData = false,
                Name = Helpers.ResourceHelper.FindKey("2dvector") + "(0)",
            AliasName = Helpers.ResourceHelper.FindKey("2dvector"),
                Guid = Guid.Parse("00000000-0000-0000-0000-000000000400").ToString(),
                LayerType = Common.CommonContract.RenderLayerType.ShpGroupLayer,
                Rederlayers = new List<RenderLayerModel>()
            };
            list.Add(shpRenderLayers);
            var shpLayers = DataBaseService.Instance.GetShpLayers();
            if (shpLayers!= null)
                { 
                shpRenderLayers.Name = Helpers.ResourceHelper.FindKey("2dvector") + "(" + shpLayers.Count + ")";
                foreach (var item in shpLayers)
                {
                    shpRenderLayers.HasPathData = true;
                    shpRenderLayers.ChildCount++;
                    var shpRen = RenderLayerModel.CreateRenderLayer(RenderLayerDto.RenderLayerConvert(item as DisplayLayer), shpRenderLayers.Name);
                    shpRen.HasPathData = false;
                    shpRen.MenuLevel = "2";
                    shpRen.IsDisplay = true;
                    shpRen.IsChecked = true;
                    shpRen.IsSymbolic = true;
                    shpRen.ParentName = shpRenderLayers.Name;
                    shpRen.IsLocal = !item.Fc.DataSource.IsNetServer();
                    shpRen.Name = (!item.Fc.DataSource.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name;
                    shpRen.AliasName = (!item.Fc.DataSource.IsNetServer() ? Helpers.ResourceHelper.FindKey("LocalDataMark") : Helpers.ResourceHelper.FindKey("ServerDataMark")) + item.Name;
                    list.Add(shpRen);
                }
            }
            MapSource = new ObservableCollection<RenderLayerModel>(list);

            tempMapSource = new ObservableCollection<RenderLayerModel>(list);
            
        }


        private void OnMouseDownCommand(object renderlayer)
        {
            if (renderlayer == null) return;

            RenderLayerModel renderlayermodel = renderlayer as RenderLayerModel;

            if (renderlayermodel.IsChecked)
            {
                foreach (RenderLayerModel item in MapSource.Where(t => t.ParentName == renderlayermodel.Name).ToList())
                {
                    item.IsDisplay = false;
                }
                renderlayermodel.IsChecked = false;
            }
            else
            {
                foreach (RenderLayerModel item in MapSource.Where(t => t.ParentName == renderlayermodel.Name).ToList())
                {
                    foreach (RenderLayerModel item2 in MapSource.Where(t => t.ParentName == item.Name && item.IsDisplay == false).ToList())
                    {
                        item2.IsDisplay = true;
                    }
                    item.IsChecked = true;
                    item.IsDisplay = true;
                }
                renderlayermodel.IsChecked = true;
            }
        }

        private void OnSelectCommand(RenderLayerModel renderlayermodel)
        {
            try
            {
                if (renderlayermodel == null) return;
                if (renderlayermodel.MenuLevel == "1") return;
                //定位到图层

                flyToRederLayer(renderlayermodel.Guid);
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }
       
        
        private void OnSearchCommand()
        {
            MapSource.Clear();
            if (tempMapSource.Count != 0)
            {
                for (int i = 0; i < tempMapSource.Count; i++)
                {
                    MapSource.Add(tempMapSource[i]);
                }
            }
            List<RenderLayerModel> removeList = new List<RenderLayerModel>();
            string _text = SearchText;
            //char[] SearchTextArray = null ;
            //if (_text!=null&&_text!="")
            //{
            //    SearchTextArray = _text.ToCharArray();
            //}
          
            //if(SearchTextArray != null)
            //{
            if(_text != null && _text != "")
            { 
                foreach(var item in tempMapSource)
                {
                    bool isModelRenderLayers = item.Name.IndexOf(Helpers.ResourceHelper.FindKey("3dmodel")) >= 0;
                    bool isImgRenderLayers = item.Name.IndexOf(Helpers.ResourceHelper.FindKey("Screenage")) >= 0;
                    bool isTileRenderLayers = item.Name.IndexOf(Helpers.ResourceHelper.FindKey("Obliquephotography")) >= 0;
                    bool isShpRenderLayers = item.Name.IndexOf(Helpers.ResourceHelper.FindKey("2dvector")) >= 0;
                    if (isModelRenderLayers || isImgRenderLayers || isTileRenderLayers || isShpRenderLayers)
                    {
                        // return;
                    }
                    else
                    {
                        bool isContains = false;
                        //for (int i = 0; i < SearchTextArray.Count(); i++)
                        //{
                            bool tempContains = item.Name.IndexOf(_text, StringComparison.OrdinalIgnoreCase) >= 0;//SearchTextArray[i].ToString()
                    if (tempContains == true)
                            {
                                isContains = true;
                            }
                      //  }
                        if (isContains == false)
                        {
                            // MapSource.Remove(item);
                            removeList.Add(item);
                        }
                    }

                }
                if(removeList.Count!=0)
                {
                    for(int i=0;i<removeList.Count;i++)
                    {
                        MapSource.Remove(removeList[i]);
                    }
                    removeList.Clear();
                }
            }
            else
            {
                // MapSource = tempMapSource;
                MapSource.Clear();
                if(tempMapSource.Count!=0)
                {
                    for(int i =0;i<tempMapSource.Count;i++)
                    {
                        MapSource.Add(tempMapSource[i]);
                    }                   
                }
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="obj"></param>
        private void OnAddCommand(object obj)
        {
            if (obj == null) return;

            RenderLayerModel renderlayermodel = obj as RenderLayerModel;
            AddDataSource();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        private void OnDeleteCommad(object obj)
        {
            if (obj == null) return;

            RenderLayerModel renderlayermodel = obj as RenderLayerModel;
            DeleteDataSource(renderlayermodel.Guid);
            //Dictionary<string, Tuple<IDataSource, List<IDisplayLayer>>>.ValueCollection valueColl = dicModel.Values;

            //if (valueColl != null)
            //{ 
            //foreach (var item in valueColl)
            //{
            //    if (item.Item1.Guid.ToString() == renderlayermodel.Guid)
            //    {
            //        var keys = dicModel.FirstOrDefault(q => q.Value == item).Key;//Select(q => q.Key);
            //        dicModel.Remove(keys);

            //        GetMapSource();
            //    }
            //}
            GetMapSource();
        }

        private void OnChangeImageAlphaCommand(object obj)
        {
            if (obj == null) return;

            RenderLayerModel renderlayermodel = obj as RenderLayerModel;
            //DeleteDataSource(renderlayermodel.Guid);
            ChangeImageLayerAlpha(renderlayermodel.Guid);
            //GetMapSource();
            //int renderIndex = MapSource.IndexOf(renderlayermodel);
            //MapSource.RemoveAt(renderIndex);
            //if (renderlayermodel.AlphaStation == "1")
            //{
            //    renderlayermodel.AlphaStation = "0";
            //}
            //else
            //{
            //    renderlayermodel.AlphaStation = "1";
            //}
          
            //MapSource.Insert(renderIndex, renderlayermodel);

        }

        public void ChangeImageLayerAlpha(string LayerGuid)
        {

            if (_renderLayers.Count > 0)
            {
                foreach (var layer in _renderLayers)
                {
                    if (layer?.Guid == LayerGuid) // remove sigle data
                    {
                        List<IImageLayer> images = DataBaseService.Instance.GetImageLayers();
                        foreach(var item in images)
                        {
                            if (item.Guid == layer?.Guid)
                            {
                                ILocalWsConfigService _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);
                                ImageLayerConfig tempConfig = _localWsCfgSrv.ImgCfgs.Find(item.Layer.ConnectionString);
                                if (tempConfig != null)
                            { 
                                string AlphaStationString = tempConfig.AlphaEnabled;
                                if (AlphaStationString == "true")
                                {
                                    tempConfig.AlphaEnabled = "false";
                                    IRasterSymbol rasterSymbolFalse = item.Layer.GetRasterSymbol();
                                    rasterSymbolFalse.AlphaEnabled = false;
                                    item.Layer.SetRasterSymbol(rasterSymbolFalse);
                                }
                                else
                                {
                                    tempConfig.AlphaEnabled = "true";
                                    IRasterSymbol rasterSymbolTrue = item.Layer.GetRasterSymbol();
                                    rasterSymbolTrue.AlphaEnabled = true;
                                    item.Layer.SetRasterSymbol(rasterSymbolTrue);
                                }
                                _localWsCfgSrv.ImgCfgs.Update(tempConfig);
                            }
                                else
                                {
                                    IRasterSymbol rasterSymbol = item.Layer.GetRasterSymbol();
                                    bool alphaStation = rasterSymbol.AlphaEnabled;
                                    if(alphaStation==true)
                                    {
                                        rasterSymbol.AlphaEnabled = false;
                                        item.Layer.SetRasterSymbol(rasterSymbol);
                                    }
                                    else
                                    {
                                        rasterSymbol.AlphaEnabled = true;
                                        item.Layer.SetRasterSymbol(rasterSymbol);
                                    }                                    
                                }
                               // IRasterSymbol  = item.Layer.GetRasterSymbol();
                                //rasterSymbol.AlphaEnabled
                                
                            }
                        }
                        //layer
                        //_renderLayers.Remove(layer as IRenderLayer);
                        //var tileLayers = DataBaseService.Instance.GetTileLayers();
                        // add by hengda this.UpdateLeftViewLayer();
                        break;
                    }
                }
            }
            //GetMapSource();
        }
            // }

            /// <summary>
            /// 删除数据源
            /// </summary>
            /// <param name="LayerGuid">图层id</param>
        public void DeleteDataSource(string LayerGuid)
        {
            try
            {
                if (_renderLayers.Count > 0)
                {
                    foreach (var layer in _renderLayers)
                    {
                        if (layer?.Guid == LayerGuid) // remove sigle data
                        {
                            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Deletelayer"), string.Format(Helpers.ResourceHelper.FindKey("Deletelayerconfirm"), layer.AliasName)))
                            {
                                DataBaseService.Instance.RemoveSingleLayer(layer);
                                _renderLayers.Remove(layer as IRenderLayer);
                                //var tileLayers = DataBaseService.Instance.GetTileLayers();
                                // add by hengda this.UpdateLeftViewLayer();
                                break;
                            }
                        }
                        else if (layer is DisplayLayer) // remove collection data 
                        {
                            var disLyr = layer as DisplayLayer;
                            if (disLyr != null && disLyr.Fc.DataSource.Guid.ToString() == LayerGuid)
                            {
                                string[] tempArr = disLyr.Fc.DataSource.ConnectionInfo.Database.Split('\\');
                                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Deletedatasource"), string.Format(Helpers.ResourceHelper.FindKey("Deletedatasourceconfirm"), tempArr[tempArr.Length - 1])))
                                {
                                    var disPlayLayers = new List<IDisplayLayer>();
                                    var disPlayGuids = new List<string>();
                                    foreach (var item in _renderLayers)
                                    {
                                        if (item.LayerType == RenderLayerType.FeatureLayer)
                                        {
                                            var disLyr1 = item as DisplayLayer;
                                            if (disLyr1 != null && disLyr1.Fc.DataSource.Guid.ToString() == LayerGuid)
                                            {
                                                disPlayLayers.Add(disLyr1);
                                                disPlayGuids.Add(disLyr1.Guid);
                                            }
                                        }
                                    }
                                    DataBaseService.Instance.RemoveFDbDataSource(disPlayLayers);
                                    foreach (var item in disPlayLayers)
                                        _renderLayers.Remove(item as IRenderLayer);

                                    // add  by hengda  this.UpdateLeftViewLayer();
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }

        }


        public void DeleteRenderLayer(string LayerGuid)
        {
            foreach (var layer in _renderLayers)
            {
                if (layer.Guid == LayerGuid)
                {
                    DataBaseService.Instance.RemoveSingleLayer(layer);
                    _renderLayers.Remove(layer as IRenderLayer);
                    break;
                }
            }
        }
        LoadDataView dataView;
        /// <summary>
        /// 增加数据源
        /// </summary>
        /// <param name="SourceType">数据类型</param>
        public void AddDataSource()
        {
           // var type = (RenderLayerType)SourceType;

            dataView = new LoadDataView();
            dataView.Owner = Application.Current.MainWindow;

            dataView.getLocalFileName += GetLocalFileName;
            dataView.addData += AddData;
            dataView.getNetServiceLayerInfo += GetNetServiceLayerInfo;

            dataView.LoadImage.Visibility = Visibility.Collapsed;
            dataView.ShowDialog();

            dataView.getLocalFileName -= GetLocalFileName;
            dataView.addData -= AddData;
            dataView.getNetServiceLayerInfo -= GetNetServiceLayerInfo;

        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="obj"></param>
        private void OnCheckMapCommand(object obj)
        {
            if (obj == null) return;
            Dictionary<string, object> dic = obj as Dictionary<string, object>;
            RenderLayerModel renderlayermodel = dic["model"] as RenderLayerModel;
            layerGuids = "";
            if (_mapSource.Select(t => t.ParentName == renderlayermodel.Name).ToList().Count > 0)
            {
                layerGuids = GetlayerGuids(renderlayermodel, renderlayermodel.Guid);
            }
            else
                layerGuids = renderlayermodel.Guid;
            bool isvisilbe = false;
            currentIndex = Convert.ToInt32(dic["index"]);
            if (currentIndex == 0)
                isvisilbe = renderlayermodel.FirstEyeStatus;
            else if (currentIndex == 1)
                isvisilbe = renderlayermodel.TwoEyeStatus;
            else if (currentIndex == 2)
                isvisilbe = renderlayermodel.ThreeEyeStatus;
            else if (currentIndex == 3)
                isvisilbe = renderlayermodel.FourEyeStatus;
            if (_splitScreenType == 3)
            {
                if (currentIndex == 0)
                    currentIndex = 2;
                else if (currentIndex == 1)
                    currentIndex = 3;
                else if (currentIndex == 2)
                    currentIndex = 0;
                else if (currentIndex == 3)
                    currentIndex = 1;
            }
            setRederLayersVisible(layerGuids, currentIndex, isvisilbe);
            //处理联动
            Task.Run(() =>
            {
                SetEyes(renderlayermodel, isvisilbe);
            });
        }

        private void SetEyes(RenderLayerModel renderLayerModel, bool isvisilbe)
        {
            //父级
            if (layerGuids.Contains(","))
            {
                string[] Guids = layerGuids.Split(",");
                //四屏to do
                if (_splitScreenType == 3)
                {
                    if (currentIndex == 2)
                    {
                        currentIndex = 0;
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).FirstEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 3)
                    {
                        currentIndex = 1;
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).TwoEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 0)
                    {
                        currentIndex = 2;
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).ThreeEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 1)
                    {
                        currentIndex = 3;
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).FourEyeStatus = isvisilbe;
                    }
                }
                else
                {
                    if (currentIndex == 0)
                    {
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).FirstEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 1)
                    {
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).TwoEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 2)
                    {
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).ThreeEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 3)
                    {
                        foreach (var item in Guids)
                            _mapSource.FirstOrDefault(t => t.Guid == item).FourEyeStatus = isvisilbe;
                    }
                }
                if (string.IsNullOrEmpty(renderLayerModel.ParentName)) return;
                SetParentEyes(renderLayerModel.ParentName, renderLayerModel.MenuLevel);
            }
            else
            {
                var currentitem = _mapSource.FirstOrDefault(t => t.Guid == layerGuids);
                //单点子集
                //四屏to do
                if (_splitScreenType == 3)
                {

                    if (currentIndex == 2)
                    {
                        currentIndex = 0;
                        currentitem.FirstEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 3)
                    {
                        currentIndex = 1;
                        currentitem.TwoEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 0)
                    {
                        currentIndex = 2;
                        currentitem.ThreeEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 1)
                    {
                        currentIndex = 3;
                        currentitem.FourEyeStatus = isvisilbe;
                    }
                }
                else
                {
                    if (currentIndex == 0)
                    {
                        //获取当前父级名称往上找是否所有子集都被隐藏或者显示
                        currentitem.FirstEyeStatus = isvisilbe;
                    }
                    else if (currentIndex == 1)
                        currentitem.TwoEyeStatus = isvisilbe;
                    else if (currentIndex == 2)
                        currentitem.ThreeEyeStatus = isvisilbe;
                    else if (currentIndex == 3)
                        currentitem.FourEyeStatus = isvisilbe;

                }
                SetParentEyes(currentitem.ParentName, currentitem.MenuLevel);
            }
        }
        /// <summary>
        /// 设置反选图层显示
        /// </summary>
        /// <param name="name"></param>
        private void SetParentEyes(string parentname, string menuLevel)
        {
            var model = _mapSource.FirstOrDefault(t => t.Name == parentname);
            if (model != null)
            {
                if (currentIndex == 0)
                {
                    var visibleresult = _mapSource.Where(t => t.ParentName == parentname && t.FirstEyeStatus).ToList();
                    var hideresult = _mapSource.Where(t => t.ParentName == parentname && !t.FirstEyeStatus).ToList();
                    if (hideresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FirstEyeStatus = true;
                    }
                    if (visibleresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FirstEyeStatus = false;
                    }
                    else
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FirstEyeStatus = true;
                    }
                }
                else if (currentIndex == 1)
                {
                    var visibleresult = _mapSource.Where(t => t.ParentName == parentname && t.TwoEyeStatus).ToList();
                    var hideresult = _mapSource.Where(t => t.ParentName == parentname && !t.TwoEyeStatus).ToList();
                    if (hideresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).TwoEyeStatus = true;
                    }
                    if (visibleresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).TwoEyeStatus = false;
                    }
                    else
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).TwoEyeStatus = true;
                    }
                }
                else if (currentIndex == 2)
                {
                    var visibleresult = _mapSource.Where(t => t.ParentName == parentname && t.ThreeEyeStatus).ToList();
                    var hideresult = _mapSource.Where(t => t.ParentName == parentname && !t.ThreeEyeStatus).ToList();
                    if (hideresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).ThreeEyeStatus = true;
                    }
                    if (visibleresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).ThreeEyeStatus = false;
                    }
                    else
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).ThreeEyeStatus = true;
                    }
                }
                else if (currentIndex == 3)
                {
                    var visibleresult = _mapSource.Where(t => t.ParentName == parentname && t.FourEyeStatus).ToList();
                    var hideresult = _mapSource.Where(t => t.ParentName == parentname && !t.FourEyeStatus).ToList();
                    if (hideresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FourEyeStatus = true;
                    }
                    if (visibleresult.Count == 0)
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FourEyeStatus = false;
                    }
                    else
                    {
                        _mapSource.FirstOrDefault(t => t.Name == parentname).FourEyeStatus = true;
                    }
                }
            }
            if (menuLevel == "3")
            {
                var parentitem = _mapSource.FirstOrDefault(t => t.Name == parentname);
                SetParentEyes(parentitem.ParentName, parentitem.MenuLevel);
            }
        }
        private string GetlayerGuids(RenderLayerModel layerModel, string guids)
        {
            layerGuids = guids;
            foreach (var item in _mapSource.Where(t => t.ParentName == layerModel.Name).ToList())
            {
                layerGuids += "," + item.Guid;
                if (_mapSource.Where(t => t.ParentName == item.Name).ToList().Count() > 0)
                    GetlayerGuids(item, layerGuids);
            }
            return layerGuids;
        }

        private TileModifyGeoVModel _tileModifyGeoVM;
        private void OnSymbolicCommand(object obj)
        {
            if (obj == null) return;

            RenderLayerModel renderlayermodel = obj as RenderLayerModel;
            if (renderlayermodel.LayerType == RenderLayerType.TileLayer)
            {
                if (_tileModifyGeoVM == null)
                {
                    _tileModifyGeoVM = new TileModifyGeoVModel() ;
                }
                var layer = _renderLayers.Find(P => P.Guid == renderlayermodel.Guid);
                var tileLayer = layer as ITileLayer;
                _tileModifyGeoVM.TileLayer = tileLayer.Layer;
                _tileModifyGeoVM.OnChecked();
            }
            else if (renderlayermodel.LayerType == RenderLayerType.FeatureLayer)
            {
                SetShpLayerSymbol(renderlayermodel.Guid);
            }
        }
        /// <summary>
        /// 飞入图层
        /// </summary>
        /// <param name="LayerGuid">图层id</param>
        private void flyToRederLayer(string LayerGuid)
        {
            try
            {
                if (_renderLayers.Count > 0)
                {
                    foreach (var layer in _renderLayers)
                    {
                        if (layer?.Guid == LayerGuid && layer?.Renderable != null)
                        {
                            GviMap.Camera.FlyToObject(layer.Renderable.Guid, gviActionCode.gviActionFlyTo);
                        }
                    }
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }

        }


        private List<string> GetLocalFileName()
        {
            string name = string.Empty;
            List<string> _nameList = new List<string>();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = FileFilterStrings.Support;   
            if (openFile.ShowDialog() == true)
            {
                foreach (string file in openFile.FileNames)
                {
                    _nameList.Add(file);
                }
            }

            return _nameList;
        }

        /// <summary>
        /// 设置图层组是否可见
        /// </summary>
        /// <param name="LayerGuids">图层组下的layer guid的集合字符串，以,为分隔符，如0001,0002</param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe">图层是否可见</param>
        public void setRederLayersVisible(string LayerGuids, int viewPortIndex, bool isVisilbe)
        {
            if (LayerGuids.Contains(","))
            {
                string[] layerGuids = LayerGuids.Split(",");
                foreach (var layerGuid in layerGuids)
                {
                    setRederLayerVisible(layerGuid, viewPortIndex, isVisilbe);

                }
            }
            else
                setRederLayerVisible(LayerGuids, viewPortIndex, isVisilbe);
        }

        /// <summary>
        /// 设置图层是否可见
        /// </summary>
        /// <param name="LayerGuid">图层guid</param>
        /// <param name="viewPortIndex">图层视口序号，单屏状态为15,多屏状态下为视口序号，第一屏为0，第二为1，依此类推</param>
        /// <param name="isVisilbe"></param>
        public void setRederLayerVisible(string LayerGuid, int viewPortIndex, bool isVisilbe)
        {
            if (_renderLayers.Count > 0)
            {
                foreach (var layer in _renderLayers)
                {

                    if (layer.Guid == LayerGuid)
                    {
                        var renderable = layer.Renderable;
                        renderable?.SetVisibleMask(GviMap.Viewport.ViewportMode, viewPortIndex, isVisilbe);
                    }
                }
            }
        }

        // 添加符号接口
        public void SetShpLayerSymbol(string LayerGuid)
        {
            //打开文件
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = FileFilterStrings.XML;
                if (openFile.ShowDialog() == false)
                    return;

                this.progressView.Visibility = Visibility.Visible;
                string fileName = openFile.FileName;

                BeginLoadDsProcess();
                foreach (var layer in _renderLayers)
                {
                    if (layer.Guid == LayerGuid)
                    {
                        DataBaseService.Instance.SetLayerSymbol(layer, fileName);
                        break;
                    }
                }
            }
            finally
            {
                FinishLoadProcess();
            }
        }

        private void AddData(string filetype, string fileAddress  , string guid, double cycleTime,int index)
        {
            if (index >= 0)
            {
                switch (filetype)
                {
                    case "ShpGroupLayer":
                        AddShpDataSource(fileAddress, index);
                        break;
                    case "ImageGroupLayer":
                        AddImageDataSource(fileAddress, index, cycleTime: cycleTime);

                        break;
                    case "TileGroupLayer":
                        AddTileDataSource(fileAddress, index);
                        break;
                    case "DataSetGroupLayer":
                        AddFdbDataSource(fileAddress, index);
                        break;
                 }
            }
            if (index == -1)
            {
                switch (filetype)
                {
                    case "WFS":
                        if (_userInfo.mspace_config.is_administrator == "1")
                            AddShpDataSource(fileAddress, index, false, guid);
                        else
                        {
                            ShowAddDataStatus(OperateDataStatus.NOPERMISSION);

                        }
                        break;
                    case "WMTS":
                        if (_userInfo.mspace_config.is_administrator == "1")
                        {
                            AddImageDataSource(fileAddress, index, isLocal: false);
                        }
                        else
                        {
                            ShowAddDataStatus(OperateDataStatus.NOPERMISSION);

                        }
                        break;
                    case "TILE":
                        if (_userInfo.mspace_config.is_administrator == "1")
                        {
                            AddTileDataSource(fileAddress, index, false);
                        }
                        else
                        {
                            ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                        }
                        break;
                    case "MODEL":
                        if (_userInfo.mspace_config.is_administrator == "1")
                        {
                            AddFdbDataSource(fileAddress, index, false);
                        }
                        else
                        {
                            ShowAddDataStatus(OperateDataStatus.NOPERMISSION);
                        }
                        break;
                }

            }
        }
     
        private IDictionary<string, string> GetNetServiceLayerInfo(string url, string servicetype)
        {
            IDictionary<string, string> tempdic = new Dictionary<string, string>();
            var con = new ConnectionInfo();
            con.Database = url;

            switch (servicetype)
            {
                case "WFS":
                    con.ConnectionType = gviConnectionType.gviConnectionWFS;
                    LibraryConfig shpConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                    };
                    IDictionary<string, string> flDic = DataBaseService.Instance.GetWfsServiceLayerGuid(shpConfig);
                    tempdic = flDic;
                    break;
                case "WMTS":
                    ImageLayerConfig imageCfg = new ImageLayerConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        AlphaEnabled = "false",
                        ConType = "File"
                    };

                    break;
                case "Tile":

                    break;
                case "Model":
                    con.ConnectionType = gviConnectionType.gviConnectionCms7Http;
                    break;
            }

            return tempdic;
        }
        private void ShowAddDataStatus(OperateDataStatus status)
        {
            switch (status)
            {
                case OperateDataStatus.NOPERMISSION:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoPermission"));
                    break;
                case OperateDataStatus.LOADFAILED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataFailed"));
                    break;
                case OperateDataStatus.LOADSUCCESSED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataSucceed"));
                    break;
                case OperateDataStatus.DATAEXISTED:
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("LoadDataRepeat"));
                    break;
            }
        }

        private void AddShpDataSource(string fileAddress,int _index = -1, bool isLocal = true, string guid = "" )
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            try
            {
               // Task.Run(() =>
               // {
                   // BeginLoadDsProcess();
                    //string name = string.Empty;
                    var con = new ConnectionInfo();
                    //string hashCode = string.Empty;
                    if (isLocal)
                    {
                        con.ConnectionType = gviConnectionType.gviConnectionShapeFile;
                        //hashCode = FileUtil.GetSHA1FromFile(fileAddress);
                        //name = fileAddress.Substring(fileAddress.LastIndexOf('\\') + 1);
                    }
                    else
                    {
                    //dataView.LoadImage.Visibility = Visibility.Visible;
                    con.ConnectionType = gviConnectionType.gviConnectionWFS;
                    }
                    con.Database = fileAddress;

                    //if (hashCode == "DATAOCCUPIED")
                    //{
                    //    ShowAddDataStatus(OperateDataStatus.DATAEXISTED);
                    //    FinishLoadProcess();
                    //    return;
                    //}

                    LibraryConfig layerConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        //AliasName=name,
                        Guid = guid,
                        Is2DData = true,
                        IsLocal = isLocal,
                        //HashCode = hashCode
                    };
                    List<IDisplayLayer> renderLayers;

                    renderLayers = DataBaseService.Instance.AddFeatureDatasource(layerConfig, out status);

                    if (status == OperateDataStatus.LOADSUCCESSED)//renderLayers != null
                    {
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{
                            _renderLayers.Add(RenderLayer.CreateRenderLayer(renderLayers[0] as IRenderLayer));
                            GetMapSource();
                    if (!isLocal)
                    {
                        //dataView.LoadImage.Visibility = Visibility.Collapsed;
                        Messages.ShowMessage("网络图层加载成功");
                    }
                    // add by hengda  this.UpdateLeftViewLayer();
                    //});
                }
                if (_index != -1)
                {
                    ChangeStatue(status, _index);
                }
                //  FinishLoadProcess();
                //  ShowAddDataStatus(status);
                // });

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                //  FinishLoadProcess();
                if (_index != -1)
                {
                    ChangeStatue(status, _index);
                }

            }
          
        }

     
        private void AddImageDataSource(string fileAddress,int _index, double cycleTime =0, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;

            try
            {
                dataView.LoadImage.Visibility = Visibility.Visible;
                Task task = Task.Run(() =>
                {
                   // BeginLoadDsProcess();
                    //string hashCode = FileUtil.GetSHA1FromFile(fileAddress);
                    ImageLayerConfig layerConfig = new ImageLayerConfig()
                    {
                        ConnInfoString = fileAddress,
                        AlphaEnabled = "false",  //启动A通道
                        IsLocal = isLocal
                    };
                    if (isLocal)
                    {
                        layerConfig.AliasName = Path.GetFileNameWithoutExtension(fileAddress);
                        layerConfig.ConType = "File";
                        //layerConfig.HashCode = hashCode;
                        layerConfig.AddTime = DateTime.Today;
                        layerConfig.CycleTime = cycleTime;
                    }
                    else
                    {
                       // dataView.LoadImage.Visibility = Visibility.Visible;
                        layerConfig.ConType = "WMTS";
                    }

                    //if (hashCode == "DATAOCCUPIED")
                    //{
                    //    ShowAddDataStatus(OperateDataStatus.DATAEXISTED);
                    //    FinishLoadProcess();
                    //    return;
                    //}


                    var renderLayer = DataBaseService.Instance.AddImageLayer(layerConfig, out status);
                    if (renderLayer != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {

                            _renderLayers.Add(renderLayer);
                            GetMapSource();
                            if (!isLocal)
                            {
                           //     dataView.LoadImage.Visibility = Visibility.Collapsed;
                                Messages.ShowMessage("网络图层加载成功");
                            }
                            else if (_index != -1)
                            {
                                ChangeStatue(status, _index);
                                Messages.ShowMessage("数据加载成功");
                            }
                            dataView.LoadImage.Visibility = Visibility.Collapsed;
                            //  successOrNot(status);
                            // add by hengda this.UpdateLeftViewLayer();
                        });
                    }
                    else
                    {
                       // if (_index != -1)
                       // {
                            dataView.LoadImage.Visibility = Visibility.Collapsed;
                            ChangeStatue(status, _index);                           
                       // }
                    }

                    //  FinishLoadProcess();
                    //  ShowAddDataStatus(status);

                });
               
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                if (_index != -1)
                {
                    dataView.LoadImage.Visibility = Visibility.Collapsed;
                    ChangeStatue(status, _index);
                }
            }         

        }

        private void AddTileDataSource(string fileAddress,int _index, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            //打开文件
            try
            {
                //this.progressView.Visibility = Visibility.Visible;
                dataView.LoadImage.Visibility = Visibility.Visible;
                Task.Run(() =>
                {
                 //   BeginLoadDsProcess();

                    string name = string.Empty;
                    //string hashCode = string.Empty;
                    if (isLocal)
                    {
                        name = Path.GetFileNameWithoutExtension(fileAddress);
                        //hashCode = FileUtil.GetSHA1FromFile(fileAddress);
                    }
                    else
                    {
                        
                        name = fileAddress.Split(':', '@')[1];
                    }

                    //if (hashCode == "DATAOCCUPIED")
                    //{
                    //    ShowAddDataStatus(OperateDataStatus.DATAEXISTED);
                    //    FinishLoadProcess();
                    //    return;
                    //}


                    TileLayerConfig layerConfig = new TileLayerConfig()
                    {
                        AliasName = name,
                        ConnInfoString = fileAddress,
                        IsLocal = isLocal,
                        //HashCode = hashCode
                    };


                    var renderLayer = DataBaseService.Instance.Add3DTileLayer(layerConfig, out status);
                    //LoadingDsProcess();
                    if (renderLayer != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            _renderLayers.Add(renderLayer);
                            GetMapSource();
                            if (!isLocal)
                            {
                                
                                Messages.ShowMessage("网络图层加载成功");
                            }
                            else if (_index != -1)
                            {                               
                                ChangeStatue(status, _index);
                                Messages.ShowMessage("数据加载成功");
                            }
                            dataView.LoadImage.Visibility = Visibility.Collapsed;
                            // add by hengda  this.UpdateLeftViewLayer();
                        });
                    }
                    else
                    {
                        if (_index != -1)
                        {
                            dataView.LoadImage.Visibility = Visibility.Collapsed;
                            ChangeStatue(status, _index);
                        }
                    }
                    //    FinishLoadProcess();
                    //   ShowAddDataStatus(status);
                });
                //
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                dataView.LoadImage.Visibility = Visibility.Collapsed;
                ChangeStatue(status, _index);
            }
           
        }
        private void AddFdbDataSource(string fileAddress,int _index, bool isLocal = true)
        {
            OperateDataStatus status = OperateDataStatus.LOADFAILED;
            try
            {
                //var actualLayers = DataBaseService.Instance.GetActualityLayers();
                //if (actualLayers != null)
                //{
                //    foreach (var item in actualLayers)
                //    {
                //        var key = item.Fc.DataSource.Guid.ToString();
                //        if (!dicModel.ContainsKey(key))
                //        {
                //            dicModel.Add(key, new Tuple<IDataSource, List<IDisplayLayer>>(item.Fc.DataSource, new List<IDisplayLayer>()));
                //            dicModel[key].Item2.Add(item);
                //        }
                //    }
                //}
                 //Task.Run(() =>
               // {
                  //  BeginLoadDsProcess();
                    //string hashCode = string.Empty;
                    string name = string.Empty;
                    var con = new ConnectionInfo();
                    if (isLocal)
                    {
                        con.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
                        con.Database = fileAddress;
                        name = fileAddress.Substring(fileAddress.LastIndexOf('\\') + 1);
                        //hashCode = FileUtil.GetSHA1FromFile(fileAddress);
                    }
                    else
                    {
                      //  dataView.LoadImage.Visibility = Visibility.Visible;
                        string[] tempArr = fileAddress.Split(';');
                        Dictionary<string, string> tempDic = new Dictionary<string, string>();
                        if (tempArr.Length > 0)
                        {
                            foreach (string item in tempArr)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    tempDic.Add(item.Split('=')[0].Trim(), item.Split('=')[1].Trim());
                                }
                            }
                        }
                        con.ConnectionType = gviConnectionType.gviConnectionCms7Http;

                        con.Server = tempDic["Server"];
                        con.Port = Convert.ToUInt32(tempDic["Port"]);
                        con.Database = tempDic["DataBase"];
                        name = con.Database;
                    }

                    //if (hashCode == "DATAOCCUPIED")
                    //{
                    //    ShowAddDataStatus(OperateDataStatus.DATAEXISTED);
                    //    FinishLoadProcess();
                    //    return;
                    //}

                    LibraryConfig layerConfig = new LibraryConfig()
                    {
                        ConnInfoString = con.ToConnectionString(),
                        AliasName = name,
                        Is2DData = false,
                        IsLocal = isLocal,
                        //HashCode = hashCode
                    };
                  //  bool alreadyExist = false;
                    var renderLayers = DataBaseService.Instance.AddFeatureDatasource(layerConfig, out status);
                    //string key = "";
                    //if (renderLayers != null && renderLayers?.Count != 0)
                    //{
                    //     key = renderLayers[renderLayers.Count - 1].Fc.DataSource.Guid.ToString();
                    //}
                    //if (dicModel!= null&&key!="")
                    //{
                    //    foreach (var item in dicModel.Values)
                    //    {
                    //        //foreach (var render in renderLayers)
                    //        //{
                    //            if (item.Item1.Guid.ToString() == key)
                    //            {
                    //                alreadyExist = true;
                    //                status = OperateDataStatus.DATAEXISTED;
                    //            }
                    //        //}
                    //    }
                    //}
                    //var key = renderLayers[0].Fc.DataSource.Guid.ToString();
                    //if (dicModel != null)
                    //{
                    //    foreach (var item in dicModel.Values)
                    //    {
                    //        if (item.Item1.Guid.ToString() == key)
                    //        {
                    //            alreadyExist = true;
                    //            status = OperateDataStatus.DATAEXISTED;
                    //        }
                    //    }
                    //}
                    if (renderLayers != null&& status !=OperateDataStatus.DATAEXISTED)//&& alreadyExist==false//
                    {
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{

                            foreach (var item in renderLayers)
                            {
                                _renderLayers.Add(item as DisplayLayer);
                            }
                            GetMapSource();
                            if (!isLocal)
                            {
                            //     dataView.LoadImage.Visibility = Visibility.Collapsed;
                                 Messages.ShowMessage("网络图层加载成功");
                            }
                            else if (_index != -1)
                            {
                                ChangeStatue(status, _index);
                                Messages.ShowMessage("数据加载成功");

                            }
                            // add by hengda  this.UpdateLeftViewLayer();
                        //});
                    }
                if (status == OperateDataStatus.DATAEXISTED)
                    {
                       // DataBaseService.Instance.RemoveFDbDataSource(renderLayers);
                        if (_index != -1)
                        {
                            ChangeStatue(status, _index);
                        }
                    }

             //   });
               
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                if (_index != -1)
                {
                    ChangeStatue(status, _index);
                }
                // FinishLoadProcess();
                // ShowAddDataStatus(status);
            }
            
        }



        private void BeginLoadDsProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartLoading"));
            });
        }

        private void LoadingDsProcess(string msg = "")
        {
            if (string.IsNullOrEmpty(msg))
                msg = Helpers.ResourceHelper.FindKey("Loading");
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = msg;
            });
        }

        private void FinishLoadProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = string.Empty;
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
            });
        }
        private void ChangeStatue(OperateDataStatus _status, int _index)
        {
            if (_status == OperateDataStatus.LOADSUCCESSED)
            {
                LoadType load = dataView.routeplandg.Items.GetItemAt(_index) as LoadType;
                load.loadStation = "完成";
            }
            else if (_status == OperateDataStatus.DATAEXISTED)
            {
                LoadType load = dataView.routeplandg.Items.GetItemAt(_index) as LoadType;
                load.loadStation = "已存在";
            }
            else
            {
                LoadType load = dataView.routeplandg.Items.GetItemAt(_index) as LoadType;
                load.loadStation = "失败";
            }
            //ShowAddDataStatus(_status);
        }

    }
}
