using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.DataSourceAccess;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.ShowCaptureObjectService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Threading;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Services.LastSearchOfLabelService;
using Mmc.Mspace.Common;
using Mmc.Mspace.Services.FieldsFilterService;
using System.Runtime.InteropServices;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class MarksManagementVModel : BaseViewModel
    {
        private PoiDetailViewModel _poiDetailViewModel;
        private PolygonMakerViewModel _polygonMakerModel;
        private MakerLineViewModel _polyLineMakerModel;

        private FeatureSelectVModel _featureSelectVModel;
        private PlotFeatureSelectVModel _plotFeatureSelectVModel;
        private readonly ExportProgressView progressView;
        public string _currentFileName = null;
        private string searchText;
        private int index = 0;
        private long firstTick = 0;
        private string prvguid = string.Empty;
        IPolygon polygon = null;

        private bool FilterorExportFlag = false;
        private DrawCustomerUC drawCustomer;

        private DrawCustomerUC poidrawCustomer;
        private string _geom;
        int num11;
        List<string> NameList = new List<string>();
        List<string> NumList = new List<string>();

        private MarkerNew _currentSelectedItem = null;

        #region 属性

        private List<MarkerNew> _markerList;
        public List<MarkerNew> MarkerList
        {
            get { return _markerList; }
            set { _markerList = value; OnPropertyChanged("MarkerList"); }
        }
        private Visibility _loadImageVisible = Visibility.Collapsed;
        public Visibility LoadImageVisible
        {
            get { return _loadImageVisible; }
            set { _loadImageVisible = value; OnPropertyChanged("LoadImageVisible"); }
        }
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged("SearchText"); }
        }
        private StringBuilder _labelSelectedText = new StringBuilder();
        public StringBuilder LabelSelectedText
        {
            get { return _labelSelectedText; }
            set { _labelSelectedText = value; OnPropertyChanged("LabelSelectedText"); }
        }

        private bool _allPoiSelectedIsChecked;
        public bool AllPoiSelectedIsChecked
        {
            get { return _allPoiSelectedIsChecked; }
            set { _allPoiSelectedIsChecked = value; OnPropertyChanged("AllPoiSelectedIsChecked"); }
        }
        private bool _queryAreaManageChecked;
        public bool QueryAreaManageChecked
        {
            get { return _queryAreaManageChecked; }
            set { _queryAreaManageChecked = value; OnPropertyChanged("QueryAreaManageChecked"); }
        }

        private string _levelSelectedItem = "0";
        public string LevelSelectedItem
        {
            get { return _levelSelectedItem; }
            set { _levelSelectedItem = value; OnPropertyChanged("LevelSelectedItem"); }
        }

        private ObservableCollection<LabelInfoModel> _labelCollection = new ObservableCollection<LabelInfoModel>();
        public ObservableCollection<LabelInfoModel> LabelCollection
        {
            get { return _labelCollection; }
            set
            {
                _labelCollection = value;
                OnPropertyChanged("LabelCollection");
            }
        }

        private ObservableCollection<LevelInfoModel> _levelCollection;

        public ObservableCollection<LevelInfoModel> LevelCollection
        {
            get
            {
                return _levelCollection ?? (_levelCollection = new ObservableCollection<LevelInfoModel>
                {

                    new LevelInfoModel
                    {
                        IsChecked = false,
                        LevelName = Helpers.ResourceHelper.FindResourceByKey("Level_First").ToString(),
                        LevelValue = "1"
                    },
                    new LevelInfoModel
                    {
                        IsChecked = false,
                        LevelName = Helpers.ResourceHelper.FindResourceByKey("Level_Second").ToString(),
                        LevelValue = "2"
                    },
                    new LevelInfoModel
                    {
                        IsChecked = false,
                        LevelName = Helpers.ResourceHelper.FindResourceByKey("Level_Third").ToString(),
                        LevelValue = "3"
                    },
                    new LevelInfoModel
                    {
                        IsChecked = false,
                        LevelName = Helpers.ResourceHelper.FindResourceByKey("Level_Fourth").ToString(),
                        LevelValue = "4"
                    },
                    new LevelInfoModel
                    {
                        IsChecked = false,
                        LevelName = Helpers.ResourceHelper.FindResourceByKey("Level_Fifth").ToString(),
                        LevelValue = "5"
                    }
                });
            }
            set
            {
                _levelCollection = value;
                OnPropertyChanged("LevelCollection");
            }
        }

        private bool _levelIsSelectAll;

        public bool LevelIsSelectAll
        {
            get { return _levelIsSelectAll; }
            set
            {
                _levelIsSelectAll = value;
                OnPropertyChanged("LevelIsSelectAll");
            }
        }

        private string _levelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelected");
        public string LevelSelectedDiscribe
        {
            get { return _levelSelectedDiscribe; }
            set { _levelSelectedDiscribe = value; OnPropertyChanged("LevelSelectedDiscribe"); }
        }


        Dictionary<bool, AreaPoiSelectedModel> AreaPoiDic = new Dictionary<bool, AreaPoiSelectedModel>();

        private bool _areaIsSelected;
        public bool AreaIsSelected
        {
            get { return _areaIsSelected; }
            set
            {
                _areaIsSelected = value;
                if (!_areaIsSelected)
                {
                    AreaSelectedName = Helpers.ResourceHelper.FindKey("MarksM_AreaPoiUnSelected");
                    AreaPoiBtnVisibility = Visibility.Collapsed;
                }
                else
                {
                    AreaSelectedName = Helpers.ResourceHelper.FindKey("MarksM_AreaPoiSelected");
                    AreaPoiBtnVisibility = Visibility.Visible;
                }
                OnPropertyChanged("AreaIsSelected");
            }
        }

        private Visibility _areaPoiBtnVisibility = Visibility.Collapsed;

        public Visibility AreaPoiBtnVisibility
        {
            get { return _areaPoiBtnVisibility; }
            set { _areaPoiBtnVisibility = value; OnPropertyChanged("AreaPoiBtnVisibility"); }
        }
        private string _areaSelectedName = Helpers.ResourceHelper.FindKey("MarksM_AreaPoiUnSelected");
        public string AreaSelectedName
        {
            get { return _areaSelectedName; }
            set { _areaSelectedName = value; OnPropertyChanged("AreaSelectedName"); }
        }

        private IRenderPolygon _areaSelectedPolygon;
        public IRenderPolygon AreaSelectedPolygon
        {
            get { return _areaSelectedPolygon; }
            set { _areaSelectedPolygon = value; OnPropertyChanged("AreaSelectedPolygon"); }
        }

        private string _wktPoly;
        public string WktPoly
        {
            get { return _wktPoly; }
            set { _wktPoly = value; OnPropertyChanged("WktPoly"); }
        }
        int EnterTimes = 0;
        private bool _filterIsChecked;
        public bool FilterIsChecked
        {
            get { return _filterIsChecked; }
            set
            {
                _filterIsChecked = value;
                if (_filterIsChecked)
                {


                    //ModelIsChecked = true;
                    LabelCollection.Clear();
                    //LabelInfoModel labelInfoNoLabel = new LabelInfoModel();
                    //labelInfoNoLabel.LabelName = "无标签";
                    //labelInfoNoLabel.LabelId = "-1";
                    //LabelCollection.Add(labelInfoNoLabel);
                    if (areaList?.Count == 0 || areaList == null)
                    {
                        RefreshQueryCollection();
                    }
                    RefreshQueryLayersCollection();
                    _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);
                    LastSearchOfLabelService lastSearchOfLabel = _localWsCfgSrv.LastSearchOfLabels.FindOne(t => t.UserName == _localWsCfgSrv.CurUserName);
                    if (lastSearchOfLabel != null)
                    {
                        string tag = lastSearchOfLabel.Tag ?? "";
                        //if(tag!=null)
                        //{
                        //    LabelSelectedText = tag;
                        //}
                        string searchText = lastSearchOfLabel.SearchText ?? "";
                        string _startDate = lastSearchOfLabel.StartDate ?? "";
                        string _endDate = lastSearchOfLabel.EndDate ?? "";
                        string _WktPoly = lastSearchOfLabel.WktPoly ?? "";
                        string _LevelSearchStr = lastSearchOfLabel.LevelSearchStr ?? "";
                        string _LayerString = lastSearchOfLabel.LayerName ?? "";
                        string[] TempString = tag.Split(',');
                        List<TagItem> tagList = MarkerHelper.Instance.GetTagsSource();
                        System.Windows.Forms.Application.DoEvents();
                        if (Array.IndexOf(TempString, "-1") >= 0)
                        {
                            LabelInfoModel labelInfoNoLabel = new LabelInfoModel();
                            labelInfoNoLabel.LabelName = "无标签";
                            labelInfoNoLabel.LabelId = "-1";
                            labelInfoNoLabel.LabelIsSelected = true;
                            LabelCollection.Add(labelInfoNoLabel);
                        }
                        else
                        {
                            LabelInfoModel labelInfoNoLabel = new LabelInfoModel();
                            labelInfoNoLabel.LabelName = "无标签";
                            labelInfoNoLabel.LabelId = "-1";
                            //   labelInfoNoLabel.LabelIsSelected = true;
                            LabelCollection.Add(labelInfoNoLabel);
                        }
                        foreach (var item in tagList)
                        {
                            LabelInfoModel labelInfo = new LabelInfoModel();
                            labelInfo.LabelName = item.name.ToString();
                            labelInfo.LabelId = item.id.ToString();

                            if (Array.IndexOf(TempString, labelInfo.LabelId) >= 0)
                            {
                                labelInfo.LabelIsSelected = true;
                                LabelCollection.Add(labelInfo);
                            }
                            else
                            {
                                labelInfo.LabelIsSelected = false;
                                LabelCollection.Add(labelInfo);
                            }

                        }
                        if (LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count)
                        {
                            TagsIsCheckedAll = true;
                        }
                        string[] LevelTempString = _LevelSearchStr.Split(',');
                        foreach (var item in LevelCollection)
                        {
                            if (Array.IndexOf(LevelTempString, item.LevelValue) >= 0)
                            {
                                item.IsChecked = true;
                            }
                        }
                        if (LevelCollection.Count(t => t.IsChecked) == LevelCollection.Count)
                        {
                            LevelIsSelectAll = true;
                        }
                        if (lastSearchOfLabel?.WktStringList != null && lastSearchOfLabel?.WktStringList?.Count != 0)
                        {
                            SelectIsChecked = true;
                            DrawLabel1_Visibility = Visibility.Collapsed;
                            DrawLabel2_Visibility = Visibility.Collapsed;
                            DrawBtn1_Visibility = Visibility.Collapsed;
                            AreaPoiBtnVisibility = Visibility.Collapsed;
                            //AreaPoiBtnVisibility = Visibility.Collapsed;
                            SelectLabel1_Visibility = Visibility.Visible;
                            QueryComBox_Visibility = Visibility.Visible;
                            SelectCheck1_Visibility = Visibility.Visible;
                            foreach (var item in areaList)
                            {
                                if (item.WktStringList[0] == lastSearchOfLabel?.WktStringList[0])
                                {
                                    //SelectID = Convert.ToString(areaList.IndexOf(item));
                                    QuerySelect = item.Name;
                                }
                            }

                        }
                        else { ModelIsChecked = true; }
                        //if(lastSearchOfLabel?.WktPoly=="")
                        //{
                        //    ModelIsChecked = true;
                        //}
                        //if(lastSearchOfLabel?.WktStringList?.Count == 0 && lastSearchOfLabel?.WktPoly == "")
                        //{
                        //    ModelIsChecked = true;
                        //}

                        // EnterTimes++;
                        if (_LayerString != null && _LayerString != "")
                        {
                            if (QueryLayersCollection.IndexOf(_LayerString) >= 0)
                            {
                                LayerSelect = _LayerString;
                            }
                        }
                    }
                    else if (lastSearchOfLabel == null)
                    {
                        ModelIsChecked = true;
                        foreach (var item in MarkerHelper.Instance.GetTagsSource())
                        {
                            LabelInfoModel labelInfo = new LabelInfoModel();
                            labelInfo.LabelName = item.name.ToString();
                            labelInfo.LabelId = item.id.ToString();
                            LabelCollection.Add(labelInfo);
                        }
                    }
                    TagTypes.Clear();
                    foreach (var item in MarkerHelper.Instance.GetTagTypesSource(pageSize: 100))
                    {
                        TagTypeModel model = new TagTypeModel
                        {
                            name = item.name,
                            id = item.id,
                            user_id = item.user_id,
                            IsChecked = false
                        };
                        foreach (var labelInfoModel in item.tags)
                        {
                            LabelInfoModel labelModel = new LabelInfoModel
                            {
                                LabelId = labelInfoModel.id,
                                LabelName = labelInfoModel.name
                            };
                            model.tags.Add(labelModel);
                        }

                        TagTypes.Add(model);
                    }

                    DicTagTypeCheckedCount = new Dictionary<string, int>();
                }
                OnPropertyChanged("FilterIsChecked");
            }
        }

        private bool _labelSelectedAllIsChecked;
        public bool LabelSelectedAllIsChecked
        {
            get { return _labelSelectedAllIsChecked; }
            set { _labelSelectedAllIsChecked = value; OnPropertyChanged("LabelSelectedAllIsChecked"); }
        }

        private bool _labelChecked;
        public bool LabelChecked
        {
            get { return _labelChecked; }
            set { _labelChecked = value; OnPropertyChanged("LabelChecked"); }
        }

        private string _labelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelected");
        public string LabelSelectedDiscribe
        {
            get { return _labelSelectedDiscribe; }
            set { _labelSelectedDiscribe = value; OnPropertyChanged("LabelSelectedDiscribe"); }
        }
        private ObservableCollection<string> _queryAreaCollection;
        public ObservableCollection<string> QueryAreaCollection
        {
            get { return _queryAreaCollection; }
            set { _queryAreaCollection = value; OnPropertyChanged("QueryAreaCollection"); }
        }

        private ObservableCollection<string> _queryLayersCollection;
        public ObservableCollection<string> QueryLayersCollection
        {
            get { return _queryLayersCollection; }
            set { _queryLayersCollection = value; OnPropertyChanged("QueryLayersCollection"); }
        }
        private string _selectID;
        public string SelectID
        {
            get { return _selectID; }
            set {
                _selectID = value;
                OnPropertyChanged("SelectID");
            }
        }
        List<string> wktList = new List<string>();


        private List<MarkerNew> _markerSearchList = new List<MarkerNew>();
        public List<MarkerNew> MarkerSearchList
        {
            get { return _markerSearchList; }
            set
            {
                _markerSearchList = value;
                OnPropertyChanged("MarkerSearchList");
            }
        }

        private bool _imageIsOpen;
        public bool ImageIsOpen
        {
            get { return _imageIsOpen; }
            set { _imageIsOpen = value; OnPropertyChanged("ImageIsOpen"); }
        }

        private Visibility _dateTimeVisib = Visibility.Visible;

        public Visibility DateTimeVisib
        {
            get { return _dateTimeVisib; }
            set { _dateTimeVisib = value; OnPropertyChanged("DateTimeVisib"); }
        }
        private Visibility _dateTime2Visib = Visibility.Collapsed;

        public Visibility DateTime2Visib
        {
            get { return _dateTime2Visib; }
            set { _dateTime2Visib = value; OnPropertyChanged("DateTime2Visib"); }
        }

        private DateTime? _startDate;

        public DateTime? StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged("StartDate"); }
        }

        private DateTime? _endDate;

        public DateTime? EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged("EndDate"); }
        }

        private ObservableCollection<MarkerNew> _markerSet;

        public ObservableCollection<MarkerNew> MarkerSet
        {
            get { return _markerSet ?? (_markerSet = new ObservableCollection<MarkerNew>()); }
            set { _markerSet = value; OnPropertyChanged("MarkerSet"); }
        }

        private UIElement _labelFilterPop;

        private ObservableCollection<TagTypeModel> _tagTypes;
        public ObservableCollection<TagTypeModel> TagTypes
        {
            get { return _tagTypes ?? (_tagTypes = new ObservableCollection<TagTypeModel>()); }
            set { _tagTypes = value; OnPropertyChanged("TagTypes"); }
        }

        private bool _tagsIsCheckedAll;
        public bool TagsIsCheckedAll
        {
            get { return _tagsIsCheckedAll; }
            set { _tagsIsCheckedAll = value; OnPropertyChanged("TagsIsCheckedAll"); }
        }

        private Dictionary<string, int> DicTagTypeCheckedCount;
        #endregion
        #region 命令
        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand(OnSearchCommand)); }
            set { _searchCommand = value; }
        }

        private RelayCommand<bool> _labelFilterCmd;

        public RelayCommand<bool> LabelFilterCmd
        {
            get { return _labelFilterCmd ?? (_labelFilterCmd = new RelayCommand<bool>(OnLabelFilter)); }
            set { _labelFilterCmd = value; }
        }

        private RelayCommand<bool> _labelSelectedAllCmd;

        public RelayCommand<bool> LabelSelectedAllCmd
        {
            get { return _labelSelectedAllCmd ?? (_labelSelectedAllCmd = new RelayCommand<bool>(OnLabelSelectedAll)); }
            set { _labelSelectedAllCmd = value; }
        }

        private RelayCommand _confirmLabelFilterCmd;
        public RelayCommand ConfirmLabelFilterCmd
        {
            get { return _confirmLabelFilterCmd ?? (_searchCommand = new RelayCommand(OnConfirmLabelFilter)); }
            set { _confirmLabelFilterCmd = value; }
        }

        private RelayCommand _cancelLabelFilterCmd;
        public RelayCommand CancelLabelFilterCmd
        {
            get { return _cancelLabelFilterCmd ?? (_cancelLabelFilterCmd = new RelayCommand(OnCancelLabelFilter)); }
            set { _cancelLabelFilterCmd = value; }
        }

        private RelayCommand _selectPOIAreaCmd;
        public RelayCommand SelectPOIAreaCmd
        {
            get { return _selectPOIAreaCmd ?? (_selectPOIAreaCmd = new RelayCommand(OnSelectPOIArea)); }
            set { _selectPOIAreaCmd = value; }
        }


        private RelayCommand _lookOverPOIAreaCmd;
        public RelayCommand LookOverPOIAreaCmd
        {
            get { return _lookOverPOIAreaCmd ?? (_lookOverPOIAreaCmd = new RelayCommand(OnLookOverPOIArea)); }
            set { _lookOverPOIAreaCmd = value; }
        }

        private RelayCommand _deletePOIAreaCmd;
        public RelayCommand DeletePOIAreaCmd
        {
            get { return _deletePOIAreaCmd ?? (_deletePOIAreaCmd = new RelayCommand(OnDeletePOIArea)); }
            set { _deletePOIAreaCmd = value; }
        }

        private RelayCommand<bool> _labelItemSelectedCmd;
        public RelayCommand<bool> LabelItemSelectedCmd
        {
            get { return _labelItemSelectedCmd ?? (_labelItemSelectedCmd = new RelayCommand<bool>(OnLabelItemSelected)); }
            set { _labelItemSelectedCmd = value; }
        }

        private RelayCommand _cancelSearchByFilterCmd;
        public RelayCommand CancelSearchByFilterCmd
        {
            get { return _cancelSearchByFilterCmd ?? (_cancelSearchByFilterCmd = new RelayCommand(OnCancelSearchByFilter)); }
            set { _cancelSearchByFilterCmd = value; }
        }

        private RelayCommand _confirmSearchByFilterCmd;
        public RelayCommand ConfirmSearchByFilterCmd
        {
            get { return _confirmSearchByFilterCmd ?? (_confirmSearchByFilterCmd = new RelayCommand(OnConfirmSearchByFilterThread)); }
            set { _confirmSearchByFilterCmd = value; }
        }

        private RelayCommand<MarkerNew> _poiCheckedCmd;
        public RelayCommand<MarkerNew> PoiCheckedCmd
        {
            get { return _poiCheckedCmd ?? (_poiCheckedCmd = new RelayCommand<MarkerNew>(OnPoiChecked)); }
            set { _poiCheckedCmd = value; }
        }

        private RelayCommand _SelMenuCommand;
        public RelayCommand SelMenuCommand { get { return _SelMenuCommand ?? (_SelMenuCommand = new RelayCommand(DealWithSelectedMenu)); } }

        private RelayCommand<bool> _selectAllCmd;

        public RelayCommand<bool> SelectAllCmd
        {
            get { return _selectAllCmd ?? (_selectAllCmd = new RelayCommand<bool>((MarkerNew) => OnSelectAllCmd(MarkerNew))); }
            set { _selectAllCmd = value; }
        }

        private RelayCommand _clearLayerSelectCmd;

        public RelayCommand ClearLayerSelectCmd
        {
            get { return _clearLayerSelectCmd ?? (_clearLayerSelectCmd = new RelayCommand(OnClearLayerSelect)); }
            set { _clearLayerSelectCmd = value; }
        }
        private RelayCommand _inverseSelectionCmd;

        public RelayCommand InverseSelectionCmd
        {
            get { return _inverseSelectionCmd ?? (_inverseSelectionCmd = new RelayCommand(OnInverseSelectionCmd)); }
            set { _inverseSelectionCmd = value; }
        }


        private RelayCommand<MouseButtonEventArgs> _buttonDownCommand;

        public RelayCommand<MouseButtonEventArgs> ButtonDownCommand
        {
            get { return _buttonDownCommand ?? (_buttonDownCommand = new RelayCommand<MouseButtonEventArgs>(OnButtonDownCommand)); }
            set { _buttonDownCommand = value; }
        }

        private RelayCommand<object> _editCommand;

        public RelayCommand<object> EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand<object>(OnEditCommand)); }
            set { _editCommand = value; }
        }

        private RelayCommand<object> _deteleTagCommand;

        public RelayCommand<object> DeteleTagCommand
        {
            get { return _deteleTagCommand ?? (_deteleTagCommand = new RelayCommand<object>(OnDeteleTagCommand)); }
            set { _deteleTagCommand = value; }
        }

        private RelayCommand<object> _deleteCommand;

        public RelayCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(OnDeleteCommand)); }
            set { _deleteCommand = value; }
        }
        private RelayCommand _batchDeleteCommand;

        public RelayCommand BatchDeleteCommand
        {
            get { return _batchDeleteCommand ?? (_batchDeleteCommand = new RelayCommand(OnBatchDeleteCommand)); }
            set { _batchDeleteCommand = value; }
        }

        private RelayCommand<object> _accountCommand;

        public RelayCommand<object> AccountCommand
        {
            get { return _accountCommand ?? (_accountCommand = new RelayCommand<object>(OnAccountCommand)); }
            set { _accountCommand = value; }
        }

        private RelayCommand _mouseDownCommand;

        public RelayCommand MouseDownCommand
        {
            get { return _mouseDownCommand ?? (_mouseDownCommand = new RelayCommand(OnMouseDownCommand)); }
            set { _mouseDownCommand = value; }
        }

        private RelayCommand _selectedDate;

        public RelayCommand SelectedDate
        {
            get
            {
                return _selectedDate ?? (_mouseDownCommand = new RelayCommand(() =>
          {
              StartDate = null;
              EndDate = null;
              DateTimeVisib = _dateTimeVisib == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
              DateTime2Visib = _dateTime2Visib == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
          }));
            }
            set { _selectedDate = value; }
        }

        private RelayCommand<bool> _levelSelectedAllCmd;

        public RelayCommand<bool> LevelSelectedAllCmd
        {
            get { return _levelSelectedAllCmd ?? (_levelSelectedAllCmd = new RelayCommand<bool>(OnLevelSelectedAllCmd)); }
            set { _levelSelectedAllCmd = value; }
        }

        private RelayCommand<bool> _levelItemCheckedCmd;

        public RelayCommand<bool> LevelItemCheckedCmd
        {
            get
            {
                return _levelItemCheckedCmd ?? (_levelItemCheckedCmd = new RelayCommand<bool>(OnLevelItemCheckedCmd));
            }
            set { _levelItemCheckedCmd = value; }
        }

        private RelayCommand<bool> _tagsIsCheckedAllCommand;
        public RelayCommand<bool> TagsIsCheckedAllCommand
        {
            get { return _tagsIsCheckedAllCommand ?? (_tagsIsCheckedAllCommand = new RelayCommand<bool>(OnTagsIsCheckedAllCommand)); }
            set { _tagsIsCheckedAllCommand = value; }
        }

        private RelayCommand<LabelInfoModel> _tagItemCheckedCommand;
        public RelayCommand<LabelInfoModel> TagItemCheckedCommand
        {
            get { return _tagItemCheckedCommand ?? (_tagItemCheckedCommand = new RelayCommand<LabelInfoModel>(OnTagItemCheckedCommand)); }
            set { _tagItemCheckedCommand = value; }
        }

        private RelayCommand<TagTypeModel> _tagTypeCheckedCommand;
        public RelayCommand<TagTypeModel> TagTypeCheckedCommand
        {
            get { return _tagTypeCheckedCommand ?? (_tagTypeCheckedCommand = new RelayCommand<TagTypeModel>(OnTagTypeCheckedCommand)); }
            set { _tagTypeCheckedCommand = value; }
        }

        #endregion
        //private MarksManagementView marksManagementView;

        #region page control attitude binding

        private int totalNum;
        private int currentPage;
        private readonly int PAGESIZE = 10;
        private int totalPages;

        private bool _isTopBtnEnabled;
        public bool IsTopBtnEnabled
        {
            get { return _isTopBtnEnabled; }
            set { _isTopBtnEnabled = value; OnPropertyChanged("IsTopBtnEnabled"); }
        }
        private bool _isPrevBtnEnabled;
        public bool IsPrevBtnEnabled
        {
            get { return _isPrevBtnEnabled; }
            set { _isPrevBtnEnabled = value; OnPropertyChanged("IsPrevBtnEnabled"); }
        }
        private bool _isNextBtnEnabled;
        public bool IsNextBtnEnabled
        {
            get { return _isNextBtnEnabled; }
            set { _isNextBtnEnabled = value; OnPropertyChanged("IsNextBtnEnabled"); }
        }
        private bool _isEndBtnEnabled;
        public bool IsEndBtnEnabled
        {
            get { return _isEndBtnEnabled; }
            set { _isEndBtnEnabled = value; OnPropertyChanged("IsEndBtnEnabled"); }
        }
        private RelayCommand _pageTopBtnCmd;

        public RelayCommand PageTopBtnCmd
        {
            get { return _pageTopBtnCmd ?? (_pageTopBtnCmd = new RelayCommand(OnPageToTopPage)); }
            set { _pageTopBtnCmd = value; }
        }
        private RelayCommand _pagePrevBtnCmd;

        public RelayCommand PagePrevBtnCmd
        {
            get { return _pagePrevBtnCmd ?? (_pagePrevBtnCmd = new RelayCommand(OnPageToPrevPage)); }
            set { _pagePrevBtnCmd = value; }
        }

        private RelayCommand _pageNextBtnCmd;

        public RelayCommand PageNextBtnCmd
        {
            get { return _pageNextBtnCmd ?? (_pageNextBtnCmd = new RelayCommand(OnPageToNextPage)); }
            set { _pageNextBtnCmd = value; }
        }

        private RelayCommand _pageEndBtnCmd;

        public RelayCommand PageEndBtnCmd
        {
            get { return _pageEndBtnCmd ?? (_pageEndBtnCmd = new RelayCommand(OnPageToEndPage)); }
            set { _pageEndBtnCmd = value; }
        }

        private RelayCommand _pageTurnToBtnCmd;

        public RelayCommand PageTurnToBtnCmd
        {
            get { return _pageTurnToBtnCmd ?? (_pageTurnToBtnCmd = new RelayCommand(OnPageTurnToPage)); }
            set { _pageTurnToBtnCmd = value; }
        }

        private string _currentPageStatus;
        public string CurrentPageStatus
        {
            get { return _currentPageStatus; }
            set { _currentPageStatus = value; OnPropertyChanged("CurrentPageStatus"); }
        }
        private string _turnToPageNum;
        public string TurnToPageNum
        {
            get { return _turnToPageNum; }
            set { _turnToPageNum = value; OnPropertyChanged("TurnToPageNum"); }
        }
        public void OnPageToTopPage()
        {
            ResetPageControlBtnStatus(false);
            if (totalPages < 1)
            {
                IsNextBtnEnabled = false;
                IsEndBtnEnabled = false;
            }
            else
            {
                IsNextBtnEnabled = true;
                IsEndBtnEnabled = true;
            }
            currentPage = 1;

            OnRefreshCerrentTablePage();
        }
        private void OnPageToPrevPage()
        {
            currentPage--;
            ResetPageControlBtnStatus(true);
            if (currentPage <= 1)
            {
                IsTopBtnEnabled = false;
                IsPrevBtnEnabled = false;
            }

            OnRefreshCerrentTablePage();
        }
        private void OnPageToNextPage()
        {
            currentPage++;

            ResetPageControlBtnStatus(true);
            if (currentPage >= totalPages)
            {
                IsNextBtnEnabled = false;
                IsEndBtnEnabled = false;
            }

            OnRefreshCerrentTablePage();
        }
        private void OnPageToEndPage()
        {
            ResetPageControlBtnStatus(false);

            IsTopBtnEnabled = true;
            IsPrevBtnEnabled = true;

            currentPage = totalPages;

            OnRefreshCerrentTablePage();
        }

        private void OnPageTurnToPage()
        {
            if (int.TryParse(TurnToPageNum, out int ToPageNum) && ToPageNum <= totalPages && ToPageNum >= 1)
            {
                if (ToPageNum != currentPage)
                {
                    ResetPageControlBtnStatus(false);

                    if (totalPages > 1)
                    {
                        if (ToPageNum <= 1)
                        {
                            IsNextBtnEnabled = true;
                            IsEndBtnEnabled = true;
                        }
                        else if (ToPageNum >= totalPages)
                        {
                            IsPrevBtnEnabled = true;
                            IsTopBtnEnabled = true;
                        }
                        else
                        {
                            ResetPageControlBtnStatus(true);
                        }
                    }
                    currentPage = ToPageNum;

                    OnRefreshCerrentTablePage();
                }
                TurnToPageNum = null;
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("IllegalIntNumber"));
            }
        }
        #endregion
        Color newColor = Color.FromArgb(150, 174, 221, 129);
        string PoiAreaMarkerKey;
        public ICommand QueryAreaManageCmd { get; set; }
        public ICommand LayerQueryManageCmd { get; set; }
        public ICommand DrawModelCmd { get; set; }
        public ICommand SelectModelCmd { get; set; }
        public ICommand NoModelCmd { get; set; }
        private Visibility _drawLabel1_Visibility = Visibility.Collapsed;

        public Visibility DrawLabel1_Visibility
        {
            get { return _drawLabel1_Visibility; }
            set { _drawLabel1_Visibility = value; OnPropertyChanged("DrawLabel1_Visibility"); }
        }
        private Visibility _drawLabel2_Visibility = Visibility.Collapsed;

        public Visibility DrawLabel2_Visibility
        {
            get { return _drawLabel2_Visibility; }
            set { _drawLabel2_Visibility = value; OnPropertyChanged("DrawLabel2_Visibility"); }
        }
        private Visibility _drawBtn1_Visibility = Visibility.Collapsed;

        public Visibility DrawBtn1_Visibility
        {
            get { return _drawBtn1_Visibility; }
            set { _drawBtn1_Visibility = value; OnPropertyChanged("DrawBtn1_Visibility"); }
        }
        private Visibility _selectLabel1_Visibility = Visibility.Collapsed;

        public Visibility SelectLabel1_Visibility
        {
            get { return _selectLabel1_Visibility; }
            set { _selectLabel1_Visibility = value; OnPropertyChanged("SelectLabel1_Visibility"); }
        }
        private Visibility _queryComBox_Visibility = Visibility.Collapsed;

        public Visibility QueryComBox_Visibility
        {
            get { return _queryComBox_Visibility; }
            set { _queryComBox_Visibility = value; OnPropertyChanged("QueryComBox_Visibility"); }
        }

        private Visibility _selectCheck1_Visibility = Visibility.Collapsed;

        public Visibility SelectCheck1_Visibility
        {
            get { return _selectCheck1_Visibility; }
            set { _selectCheck1_Visibility = value; OnPropertyChanged("SelectCheck1_Visibility"); }
        }
        private string _querySelect;
        public string QuerySelect
        {
            get { return _querySelect; }
            set
            {
                _querySelect = value; OnPropertyChanged("QuerySelect");
                // 
            }
        }

        private string _layerSelect;
        public string LayerSelect
        {
            get { return _layerSelect; }
            set
            {
                _layerSelect = value; OnPropertyChanged("LayerSelect");
            }
        }
        private bool _selectIsChecked = true;
        public bool SelectIsChecked
        {
            get { return _selectIsChecked; }
            set
            {
                _selectIsChecked = value; OnPropertyChanged("SelectIsChecked");
                if (SelectIsChecked == true)
                {
                    //DrawLabel1_Visibility = Visibility.Collapsed;
                    //DrawLabel2_Visibility = Visibility.Collapsed;
                    //DrawBtn1_Visibility = Visibility.Collapsed;
                    //AreaPoiBtnVisibility = Visibility.Collapsed;
                    ////AreaPoiBtnVisibility = Visibility.Collapsed;
                    //SelectLabel1_Visibility = Visibility.Visible;
                    //QueryComBox_Visibility = Visibility.Visible;
                    //SelectCheck1_Visibility = Visibility.Visible;
                }
                // querySelect
            }
        }
        private bool _drawIsChecked = true;
        public bool DrawIsChecked
        {
            get { return _drawIsChecked; }
            set
            {
                _drawIsChecked = value; OnPropertyChanged("DrawIsChecked");
                //DrawLabel1_Visibility = Visibility.Visible;
                //DrawLabel2_Visibility = Visibility.Visible;
                //DrawBtn1_Visibility = Visibility.Visible;
                //AreaPoiBtnVisibility = Visibility.Collapsed;
                ////AreaPoiBtnVisibility = Visibility.Collapsed;
                //SelectLabel1_Visibility = Visibility.Collapsed;
                //QueryComBox_Visibility = Visibility.Collapsed;
                //SelectCheck1_Visibility = Visibility.Collapsed;
            }
        }
        private bool _modelIsChecked = true;
        public bool ModelIsChecked
        {
            get { return _modelIsChecked; }
            set
            {
                _modelIsChecked = value; OnPropertyChanged("ModelIsChecked");
                if (ModelIsChecked == true)
                {

                    CreateTempRObj();
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    AreaPoiDic.Clear();
                    AreaSelectedPolygon = null;
                    _wktPoly = null;
                    poiAreaSelectFlag = true;
                    FilterorExportFlag = true;
                    AreaIsSelected = false;
                    DrawLabel1_Visibility = Visibility.Collapsed;
                    DrawLabel2_Visibility = Visibility.Collapsed;
                    DrawBtn1_Visibility = Visibility.Collapsed;
                    AreaPoiBtnVisibility = Visibility.Collapsed;
                    SelectLabel1_Visibility = Visibility.Collapsed;
                    QueryComBox_Visibility = Visibility.Collapsed;
                    SelectCheck1_Visibility = Visibility.Collapsed;
                    queryEnvelopeManagement.QueryClose();
                }
            }
        }

        public MarksManagementVModel()
        {
            //httpDowLoadManager = new HttpDowLoadManager();
            //var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            //httpDowLoadManager._poiHost = json.poiUrl;
            //httpDowLoadManager.Token = HttpServiceUtil.Token;
            //_MarkerNewList = new List<MarkerNew>();
            //marksManagementView= new MarksManagementView();
            //marksManagementView.DataContext = this;
            MarkerList = new List<MarkerNew>();
            if (drawCustomer == null)
            {
                drawCustomer = new DrawCustomerUC("WaterPolygonDrawn", DrawCustomerType.MenuCommand);
            }
            GviMap.AxMapControl.RcMouseClickSelect += new _IRenderControlEvents_RcMouseClickSelectEventHandler(RenderControl_RcMouseClickSelect);
            GviMap.AxMapControl.RcObjectEditing += new _IRenderControlEvents_RcObjectEditingEventHandler(g_RcObjectEditing);

            GviMap.AxMapControl.HighlightHelper.Color = newColor;
            PoiAreaMarkerKey = "PoiAreaMarkerKey";
            this.LayerQueryManageCmd = new RelayCommand(LayerQueryManage);
            this.QueryAreaManageCmd = new RelayCommand(QueryAreaManage);
            this.DrawModelCmd = new RelayCommand(EnterDrawModel);
            this.SelectModelCmd = new RelayCommand(EnterSelectModel);
            this.NoModelCmd = new RelayCommand(EnterNoModel);
            //LevelIsSelectAll = true;
            //OnLevelSelectedAllCmd(true);
        }

        private List<IDisplayLayer> _layers = new List<IDisplayLayer>();
        private List<ITileLayer> _layers2 = new List<ITileLayer>();
        private void g_RcObjectEditing(IGeometry Geometry)
        {
            GviMap.AxMapControl.HighlightHelper.SetRegion(Geometry);
        }
        private Dictionary<string, RowObject> rowMap = new Dictionary<string, RowObject>();
        private void RenderControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                this._layers = ServiceManager.GetService<IDataBaseService>(null).GetShpLayers();
                IPickResult pr = PickResult;
                IFeatureLayerPickResult fpr = pr as IFeatureLayerPickResult;
                IPolyline polyline = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolyline, gviVertexAttribute.gviVertexAttributeZ) as IPolyline;
                FeatureSelectedModel featureSelectedModel = new FeatureSelectedModel();
                PlotFeatureSelectedModel plotFeatureSelectedModel = new PlotFeatureSelectedModel();
                if (this._layers != null)
                {
                    foreach (IDisplayLayer displayLayer in this._layers)
                    {
                        IFeatureClass shpfc = displayLayer.Fc;

                        var sp = shpfc.FeatureDataSet.SpatialReference;
                        polyline.SpatialCRS = sp;
                        if (sp != GviMap.SpatialCrs) IntersectPoint.Project(sp);

                        var startPoint = IntersectPoint.Clone() as IPoint;
                        startPoint.Z = 100000;
                        polyline.StartPoint = startPoint;
                        var endPoint = IntersectPoint.Clone() as IPoint;
                        endPoint.Z = -10000;
                        polyline.EndPoint = endPoint;

                        IFdeCursor cursor = null;
                        IRowBuffer row = null;
                        List<IRowBuffer> list = new List<IRowBuffer>();

                        ISpatialFilter filter = new SpatialFilter();
                        filter.Geometry = polyline;
                        filter.SpatialRel = gviSpatialRel.gviSpatialRelEnvelope;
                        filter.GeometryField = "Geometry";
                        cursor = shpfc.Search(filter, false);
                        while ((row = cursor.NextRow()) != null)
                        {
                            list.Add(row);
                            int geometryIndex = -1;
                            geometryIndex = row.FieldIndex("Geometry");
                            if (geometryIndex != -1)
                            {
                                int oid = int.Parse(row.GetValue(0).ToString());


                                /*if (row.GetValue(row.IndexOf("Linetype"))!=null)
                                    {
                                        plotFeatureSelectedModel.SelectedInfo = row.GetValue(row.IndexOf("Layer")).ToString();
                                        if (_featureSelectVModel != null)
                                        {
                                            _featureSelectVModel.CloseWindow();
                                        }
                                        if (_plotFeatureSelectVModel == null)
                                        {
                                            _plotFeatureSelectVModel = new PlotFeatureSelectVModel();
                                        }
                                        System.Drawing.Point screenPoint = System.Windows.Forms.Control.MousePosition;
                                       // _plotFeatureSelectVModel.ShowWindow(plotFeatureSelectedModel, screenPoint.X, screenPoint.Y);
                                    }
                                    else
                                    {
                                        featureSelectedModel.SelectedFID = oid;
                                        featureSelectedModel.SelectedInfo = row.GetValue(row.IndexOf("Info")).ToString();
                                        featureSelectedModel.SelectedAddress = row.GetValue(row.IndexOf("Address")).ToString();
                                        featureSelectedModel.SelectedPrincipal = row.GetValue(row.IndexOf("Principal")).ToString();
                                        featureSelectedModel.SelectedPPhone = row.GetValue(row.IndexOf("PPhone")).ToString();
                                        featureSelectedModel.SelectedManager = row.GetValue(row.IndexOf("Manager")).ToString();
                                        featureSelectedModel.SelectedMPhone = row.GetValue(row.IndexOf("MPhone")).ToString();
                                        if (_plotFeatureSelectVModel != null)
                                        {
                                            _plotFeatureSelectVModel.CloseWindow();
                                        }
                                        if (_featureSelectVModel == null)
                                        {
                                            _featureSelectVModel = new FeatureSelectVModel();
                                        }
                                        System.Drawing.Point screenPoint = System.Windows.Forms.Control.MousePosition;
                                        //_featureSelectVModel.ShowWindow(featureSelectedModel, screenPoint.X, screenPoint.Y);
                                    }
                                    */
                                IRowBuffer shprow = shpfc.GetRow(oid);
                                int shpindex = shprow.FieldIndex("Geometry");
                                IPolygon polygon = shprow.GetValue(shpindex) as IPolygon;
                                if (polygon == null)
                                    break;

                                GviMap.AxMapControl.HighlightHelper.MinZ = -100;
                                GviMap.AxMapControl.HighlightHelper.MinZ = 1000;
                                GviMap.AxMapControl.HighlightHelper.SetRegion(polygon);
                                GviMap.AxMapControl.HighlightHelper.VisibleMask = (byte)gviViewportMask.gviViewAllNormalView;


                                DataTable infoTable = displayLayer.GetInfoTable(oid.ToString());
                                bool flag6 = infoTable == null;
                                if (flag6)
                                {

                                }
                                else
                                {
                                    bool flag7 = ServiceManager.GetService<IShellService>(null).PopView.Children.Count == 0;
                                    if (flag7)
                                    {
                                        ServiceManager.GetService<IShellService>(null).PopView.Children.Add(ServiceManager.GetService<IShowCaptureObjectService>(null).View);
                                    }
                                    System.Drawing.Point screenPoint = System.Windows.Forms.Control.MousePosition;
                                    ServiceManager.GetService<IShowCaptureObjectService>(null).DataContext = new PopViewDataContext
                                    {
                                        Left = screenPoint.X,
                                        Top = screenPoint.Y,
                                        DataView = infoTable.DefaultView,
                                        IsOpen = true,
                                        FeatureId = oid.ToString()
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void ShowFeatureSelectWindow()
        {

        }
        private void EnterDrawModel()
        {
            DrawLabel1_Visibility = Visibility.Visible;
            DrawLabel2_Visibility = Visibility.Visible;
            DrawBtn1_Visibility = Visibility.Visible;
            AreaPoiBtnVisibility = Visibility.Collapsed;
            //AreaPoiBtnVisibility = Visibility.Collapsed;
            SelectLabel1_Visibility = Visibility.Collapsed;
            QueryComBox_Visibility = Visibility.Collapsed;
            SelectCheck1_Visibility = Visibility.Collapsed;
            queryEnvelopeManagement.QueryClose();
        }
        private void EnterSelectModel()
        {
            CreateTempRObj();
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
            AreaPoiDic.Clear();
            AreaSelectedPolygon = null;
            _wktPoly = null;
            poiAreaSelectFlag = true;
            FilterorExportFlag = true;
            AreaIsSelected = false;
            DrawLabel1_Visibility = Visibility.Collapsed;
            DrawLabel2_Visibility = Visibility.Collapsed;
            DrawBtn1_Visibility = Visibility.Collapsed;
            AreaPoiBtnVisibility = Visibility.Collapsed;
            //AreaPoiBtnVisibility = Visibility.Collapsed;
            SelectLabel1_Visibility = Visibility.Visible;
            QueryComBox_Visibility = Visibility.Visible;
            SelectCheck1_Visibility = Visibility.Visible;
        }
        private void EnterNoModel()
        {

        }

        //SelectModelIsChecked 

        #region 搜索条件方法
        /// <summary>
        /// 标签的全选
        /// </summary>
        /// <param name="ischecked"></param>
        private void OnLabelSelectedAll(bool ischecked)
        {
            try
            {
                if (LabelCollection.Count > 0)
                {
                    foreach (var item in LabelCollection)
                    {
                        item.LabelIsSelected = ischecked;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 选择标签
        /// </summary>
        /// <param name="ischecked"></param>
        private void OnLabelFilter(bool ischecked)
        {
            try
            {

                //OnConfirmLabelFilter();

                if (!ischecked)
                {

                    OnConfirmLabelFilter();

                }
                else
                {
                    LabelCollection.ForEach(e =>
                    {
                        if (LabelSelectedText.ToString().Contains(e.LabelId))
                        {
                            e.LabelIsSelected = true;
                        }
                    });


                    LabelSelectedText = new StringBuilder();
                    if (LabelCollection.Count > 0)
                    {
                        foreach (var item in LabelCollection)
                        {
                            if (item.LabelIsSelected)
                            {
                                LabelSelectedText.Append(item.LabelId);
                                LabelSelectedText.Append(",");
                            }
                        }
                    }
                }

                var LabelIsSelectedNum = LabelCollection.Where(t => t.LabelIsSelected == true).ToList().Count;
                if (LabelCollection.Count != LabelIsSelectedNum && LabelCollection.Count != 0)
                {
                    LabelSelectedAllIsChecked = false;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 标签子项选择
        /// </summary>
        private void OnLabelItemSelected(bool isselected)
        {
            try
            {
                if (!isselected)
                {
                    LabelSelectedAllIsChecked = false;
                }
                else
                {
                    var LabelIsSelectedNum = LabelCollection.Where(t => t.LabelIsSelected == true).ToList().Count;
                    if (LabelCollection.Count == LabelIsSelectedNum && LabelCollection.Count != 0)
                    {
                        LabelSelectedAllIsChecked = true;
                    }
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 取消标签的选择
        /// </summary>
        private void OnCancelLabelFilter()
        {
            try
            {
                var LabelSelected = LabelSelectedText.ToString().Split(",").ToArray();
                if (LabelCollection.Count > 0)
                {
                    LabelCollection.ForEach(t =>
                    {
                        t.LabelIsSelected = false;
                    });
                    LabelSelected.ForEach(e =>
                    {
                        foreach (var item in LabelCollection)
                        {
                            if (item.LabelId == e && !string.IsNullOrEmpty(item.LabelId))
                            {
                                item.LabelIsSelected = true;
                                break;
                            }
                        }
                    });
                }
                LabelChecked = false;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 确定标签的选择
        /// </summary>
        private void OnConfirmLabelFilter()
        {
            try
            {
                LabelSelectedText.Clear();
                if (LabelCollection.Count > 0)
                {
                    var lst = LabelCollection.Where(t => t.LabelIsSelected).Select(t => t.LabelId).ToList();
                    LabelSelectedText = new StringBuilder(string.Join(",", lst));
                }
                LabelChecked = false;

                var LabelSelectedNum = LabelCollection.Where(t => t.LabelIsSelected == true).ToList().Count;
                if (LabelCollection.Count == LabelSelectedNum && LabelCollection.Count != 0)
                {
                    LabelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelectedAll");
                }
                else
                {
                    LabelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelected");
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        protected bool poiAreaSelectFlag = false;
        /// <summary>
        /// 选择筛选的区域
        /// </summary>
        private void OnSelectPOIArea()
        {
            try
            {
                if (poidrawCustomer == null)
                {
                    poidrawCustomer = new DrawCustomerUC(Helpers.ResourceHelper.FindKey("PoiAreaSelect"),
                        DrawCustomerType.MenuCommand);
                    //注册绘制多边形事件

                }

                CreateTempRObj();
                RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, poidrawCustomer, RCMouseOperType.PickPoint);
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                RCDrawManager.Instance.PolygonDraw.OnDrawFinished += Rone_PolygonDraw_OnDrawFinished;
                poiAreaSelectFlag = true;
                FilterorExportFlag = true;
                AreaIsSelected = false;
                AreaPoiDic.Clear();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 查看筛选选择的区域
        /// </summary>
        private void OnLookOverPOIArea()
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {

                    if (GviMap.TempRObjectPool.ContainsKey(PoiAreaMarkerKey))
                    {
                        AreaSelectedPolygon = GviMap.TempRObjectPool[PoiAreaMarkerKey] as IRenderPolygon;

                        GviMap.Camera.LookAtEnvelope(AreaSelectedPolygon.Envelope);
                        HightLight((IRenderGeometry)AreaSelectedPolygon);
                    }

                    //AreaSelectedPolygon = AreaPoiDic[true].AreaSelectedPolygon;
                    //WktPoly = AreaPoiDic[true].WktPoly;


                });

            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void HightLight(IRenderGeometry render)
        {
            if (render != null)
                ((IRenderGeometry)render).Glow(1500);
        }
        private void OnDeletePOIArea()
        {
            try
            {
                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Marks_MesTip"), Helpers.ResourceHelper.FindKey("Marks_ConfirmDelete")))
                {
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);

                    AreaIsSelected = false;

                    if (GviMap.TempRObjectPool.ContainsKey(PoiAreaMarkerKey))
                    {
                        if (GviMap.TempRObjectPool[PoiAreaMarkerKey] != null)
                        {
                            var item = GviMap.TempRObjectPool[PoiAreaMarkerKey] as IRenderPolygon;

                            if (item != null)
                            {
                                var itemObject = item as IRObject;
                                var Deleteflag = GviMap.ObjectManager.DeleteObject(itemObject.Guid);
                            }
                        }
                    }

                    AreaPoiDic.Clear();
                }

                AreaSelectedPolygon = null;
                // AreaIsSelected = false;
                _wktPoly = null;
                poiAreaSelectFlag = !poiAreaSelectFlag;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnCancelSearchByFilter()
        {
            try
            {
                FilterIsChecked = false;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        List<QueryWktGroup> areaList = new List<QueryWktGroup>();
        private ILocalWsConfigService _localWsCfgSrv;
        public void OnConfirmSearchByFilter()
        {
            try
            {

                MarkerList?.Clear();
                ReSetPageStatus();
                int QueryID = -1;
                if (SelectID != null)
                {
                    QueryID = Convert.ToInt32(SelectID);
                }
                QueryWktGroup temp = null;
                if (SelectID != "" && QueryID >= 0)
                {
                    temp = areaList[QueryID];
                    //Messages.ShowMessage(temp.WktStringList.Count.ToString());
                }
                string tag = string.Empty;
                if (LabelCollection?.Count > 0)
                {
                    var lst = LabelCollection.Where(t => t.LabelIsSelected).Select(t => t.LabelId).ToList();
                    LabelSelectedText = new StringBuilder(string.Join(",", lst));
                }
                if (!string.IsNullOrEmpty(LabelSelectedText.ToString()))
                {
                    tag = LabelSelectedText.ToString();
                }
                if (MarkerSearchList.Count > 0)
                {
                    MarkerHelper.Instance.OnShowMaker(MarkerSearchList, false);
                }

                if (tag != "")
                {
                    List<MarkerNew> layeSelectMarkList = new List<MarkerNew>();
                    LoadImageVisible = Visibility.Visible;
                    List<MarkerNew> MarkerListTemp = new List<MarkerNew>();
                    MarkerListTemp.AddRange(MarkerSearchList);

                    // 筛选等级多选
                    var levelList = LevelCollection.Where(t => t.IsChecked).SelectMany(t => t.LevelValue).ToList();
                    string levelSearchStr = String.Join(",", levelList);

                    _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);
                    if (_localWsCfgSrv != null)
                    {
                        LastSearchOfLabelService lastSearchOfLabel = new LastSearchOfLabelService();
                        lastSearchOfLabel.Tag = tag;
                        lastSearchOfLabel.SearchText = searchText;
                        lastSearchOfLabel.StartDate = _startDate.ToString();
                        lastSearchOfLabel.EndDate = _endDate.ToString();
                        lastSearchOfLabel.SearchText = searchText;
                        lastSearchOfLabel.WktPoly = WktPoly;
                        lastSearchOfLabel.LevelSearchStr = levelSearchStr;
                        if (LayerSelect != "")
                        {
                            lastSearchOfLabel.LayerName = LayerSelect;
                        }
                        if (ModelIsChecked == true)
                        {
                            lastSearchOfLabel.WktStringList = null;
                        }
                        else if (temp != null)
                        {
                            lastSearchOfLabel.WktStringList = temp.WktStringList;
                        }

                        _localWsCfgSrv.LastSearchOfLabels.Delete(t => t.UserName == _localWsCfgSrv.CurUserName);
                        _localWsCfgSrv.LastSearchOfLabels.Add(lastSearchOfLabel);
                    }
                    if (LayerSelect != "" && LayerSelect != null)//包含矢量区域
                    {
                        List<LayerItemModel> QueryLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(null);
                        foreach (var item in QueryLayers)
                        {
                            if (item.Name == LayerSelect)
                            {
                                IShowLayer showLayer = item.Parameters as IShowLayer;

                                //dt = showLayer.FuzzySearch("", FieldsFilterService.GetDefault(null).GetFilterFields(showLayer.AliasName), null);

                                var layer = showLayer as DisplayLayer;
                                var render = layer.Renderable as IFeatureLayer;
                                IFeatureClass renderFeatureclass = layer.Fc;
                                //renderFeatureclass.GetFeaturesEnvelope
                                //int count = renderFeatureclass.GetFeatureCount();
                                ISpatialFilter filter = new SpatialFilter();
                                //filter.Geometry = polyline;
                                filter.SpatialRel = gviSpatialRel.gviSpatialRelIntersects;
                                filter.GeometryField = "Geometry";
                                IFdeCursor cursor = null;
                                try
                                {
                                    cursor = renderFeatureclass.Search(filter, false);
                                    IRowBuffer row = null;

                                    while ((row = cursor.NextRow()) != null)
                                    {
                                        int geometryIndex = -1;
                                        geometryIndex = row.FieldIndex("Geometry");
                                        IGeometry geometry = row.GetValue(geometryIndex) as IGeometry;
                                        geometry.Project(GviMap.SpatialCrs);
                                        geometry.SpatialCRS = GviMap.SpatialCrs;
                                        IGeometry tempGeometry = null;
                                        tempGeometry = geometry.Clone2(gviVertexAttribute.gviVertexAttributeZ) as IGeometry;
                                        string geoWkt = geometry.AsWKT();

                                        List<MarkerNew> layeSelecttempList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), geoWkt, levelSearchStr);
                                        foreach (var markitem in layeSelecttempList)
                                        {
                                            if (layeSelectMarkList.IndexOf(markitem) < 0)
                                            {
                                                layeSelectMarkList.Add(markitem);
                                            }

                                        }
                                        //layeSelectMarkList = layeSelectMarkList.Union(layeSelecttempList) as List<MarkerNew>;


                                    }
                                }
                                catch (COMException ex)
                                {
                                    System.Diagnostics.Trace.WriteLine(ex.Message);
                                }
                                finally
                                {
                                    //if (cursor != null)
                                    //{
                                    //    //System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
                                    //    cursor = null;
                                    //}
                                }
                            }
                        }
                    }
                    if (layeSelectMarkList?.Count == 0 || layeSelectMarkList == null)//不包含矢量区域
                    {
                        if (temp != null && ModelIsChecked == false && _wktPoly == null)//区域库
                        {
                            foreach (var item in temp.WktStringList)
                            {
                                MarkerSearchList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), item, levelSearchStr);
                                MarkerList = MarkerList.Union(MarkerSearchList).ToList();// = MarkerSearchList;
                            }
                            MarkerSearchList?.Clear();
                            for (int i = 0; i < MarkerList.Count; i++)
                            {
                                MarkerSearchList.Add(MarkerList[i]);
                            }
                        }
                        if (ModelIsChecked == true || _wktPoly != null)//全部，手绘
                        {
                            MarkerList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), WktPoly, levelSearchStr);
                            MarkerSearchList?.Clear();
                            for (int i = 0; i < MarkerList.Count; i++)
                            {
                                MarkerSearchList.Add(MarkerList[i]);
                            }

                        }
                    }
                    else//包含矢量区域
                    {
                        if (temp != null && ModelIsChecked == false && _wktPoly == null)//
                        {
                            foreach (var item in temp.WktStringList)
                            {
                                MarkerSearchList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), item, levelSearchStr);
                                MarkerList = MarkerList.Union(MarkerSearchList).ToList();// = MarkerSearchList;
                            }
                            List<MarkerNew> IntersectList = new List<MarkerNew>();
                            foreach (var item in MarkerList)//IntersectList = MarkerSearchList.Intersect(layeSelectMarkList).ToList();//不是简单类型，无法通过Intersect方法去重
                            {
                                for (int i = 0; i < layeSelectMarkList.Count; i++)
                                {
                                    if (item.Code == layeSelectMarkList[i].Code)
                                    {
                                        IntersectList.Add(item);
                                    }
                                }
                            }
                            MarkerSearchList?.Clear();//获取两个集合的交集以后对MarkerSearchList和MarkerList重新赋值
                            MarkerList?.Clear();
                            for (int i = 0; i < IntersectList.Count; i++)
                            {
                                MarkerSearchList.Add(IntersectList[i]);
                            }
                            for (int i = 0; i < IntersectList.Count; i++)
                            {
                                MarkerList.Add(IntersectList[i]);
                            }
                        }
                        if (ModelIsChecked == true || _wktPoly != null)
                        {
                            if (_wktPoly != null)//区域交矢量
                            {
                                MarkerList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), WktPoly, levelSearchStr);


                                List<MarkerNew> IntersectList = new List<MarkerNew>();
                                foreach (var item in MarkerList)//IntersectList = MarkerSearchList.Intersect(layeSelectMarkList).ToList();//不是简单类型，无法通过Intersect方法去重
                                {
                                    for (int i = 0; i < layeSelectMarkList.Count; i++)
                                    {
                                        if (item.Code == layeSelectMarkList[i].Code)
                                        {
                                            IntersectList.Add(item);
                                        }
                                    }
                                }
                                MarkerSearchList?.Clear();//获取两个集合的交集以后对MarkerSearchList和MarkerList重新赋值
                                MarkerList?.Clear();
                                for (int i = 0; i < IntersectList.Count; i++)
                                {
                                    MarkerSearchList.Add(IntersectList[i]);
                                }
                                for (int i = 0; i < IntersectList.Count; i++)
                                {
                                    MarkerList.Add(IntersectList[i]);
                                }
                            }
                            else//全部结果集按照矢量区域的查询结果  all交矢量
                            {
                                MarkerList.Clear();
                                MarkerSearchList.Clear();
                                MarkerList = layeSelectMarkList;
                                MarkerSearchList = layeSelectMarkList;
                            }

                        }
                    }

                    // 保留筛选区域内之前的状态
                    if (MarkerListTemp != null)
                    {
                        foreach (var i in MarkerSearchList)
                        {
                            foreach (var j in MarkerListTemp)
                            {
                                if (i.Code.Equals(j.Code) && j.IsChecked)
                                {
                                    i.IsChecked = true;
                                    OnShowMaker(i, true);
                                }
                            }
                        }
                    }

                    // 校验全选状态
                    if (MarkerList.Count > 0)
                        AllPoiSelectedIsChecked = MarkerSearchList.Count(t => t.IsChecked) == MarkerSearchList.Count;

                    //FilterIsChecked = false;                  
                    //FilterIsChecked = false;
                    ReSetPageStatus();
                    LoadImageVisible = Visibility.Collapsed;
                }
                if (MarkerList.Count < 100)
                {
                    OnSelectAllCmd(true);
                }

            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                Messages.ShowMessage("网络请求发生错误，请改变查询条件稍后重试");
                LoadImageVisible = Visibility.Collapsed;
            }
        }

        private void OnClearLayerSelect()
        {
            LayerSelect = "";
            RefreshQueryLayersCollection();
        }
        public void LoadLastLabels()
        {
            try
            {
                Task.Run(() =>
                {

                    //BeginLoadDsProcess();
                    MapEventRegistManager(true);
                    MarkerList?.Clear();
                    ReSetPageStatus();
                    _localWsCfgSrv = ServiceManager.GetService<ILocalWsConfigService>(null);
                    LastSearchOfLabelService lastSearchOfLabel = _localWsCfgSrv.LastSearchOfLabels.FindOne(t => t.UserName == _localWsCfgSrv.CurUserName);
                    string tag = lastSearchOfLabel?.Tag ?? "";
                    string searchText = lastSearchOfLabel?.SearchText ?? "";
                    string _startDate = lastSearchOfLabel?.StartDate ?? "";
                    string _endDate = lastSearchOfLabel?.EndDate ?? "";
                    string _WktPoly = lastSearchOfLabel?.WktPoly ?? "";
                    string _levelSearchStr = lastSearchOfLabel?.LevelSearchStr ?? "";
                    if (!string.IsNullOrEmpty(LabelSelectedText.ToString()))
                    {
                        string tags = LabelSelectedText.ToString();
                        tag = tags.Substring(0, tags.Length - 1);
                    }
                    if (MarkerSearchList.Count > 0)
                    {
                        MarkerHelper.Instance.OnShowMaker(MarkerSearchList, false);
                    }
                    if (tag != "")
                    {
                        LoadImageVisible = Visibility.Visible;
                        List<MarkerNew> MarkerListTemp = new List<MarkerNew>();
                        MarkerListTemp.AddRange(MarkerSearchList);

                        // 筛选等级多选
                        // var levelList = LevelCollection.Where(t => t.IsChecked).SelectMany(t => t.LevelValue).ToList();
                        // string levelSearchStr = String.Join(",", levelList);


                        if (lastSearchOfLabel.WktStringList != null)
                        {
                            foreach (var item in lastSearchOfLabel.WktStringList)
                            {
                                MarkerSearchList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate, _endDate, item, _levelSearchStr);
                                MarkerList = MarkerList.Union(MarkerSearchList).ToList();// = MarkerSearchList;
                            }
                            MarkerSearchList?.Clear();
                            for (int i = 0; i < MarkerList.Count; i++)
                            {
                                MarkerSearchList.Add(MarkerList[i]);
                            }
                        }
                        else if (lastSearchOfLabel.WktStringList == null || _WktPoly != "")
                        {
                            MarkerList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate, _endDate, "", _levelSearchStr);//_WktPoly不存，按照全部区域查询，产品要求
                            MarkerSearchList?.Clear();
                            for (int i = 0; i < MarkerList.Count; i++)
                            {
                                MarkerSearchList.Add(MarkerList[i]);
                            }

                        }
                        // 保留筛选区域内之前的状态
                        if (MarkerListTemp != null)
                        {
                            foreach (var i in MarkerSearchList)
                            {
                                foreach (var j in MarkerListTemp)
                                {
                                    if (i.Code.Equals(j.Code) && j.IsChecked)
                                    {
                                        i.IsChecked = true;
                                        OnShowMaker(i, true);
                                    }
                                }
                            }
                        }

                        // 校验全选状态
                        if (MarkerList.Count > 0)
                            AllPoiSelectedIsChecked = MarkerSearchList.Count(t => t.IsChecked) == MarkerSearchList.Count;

                        //FilterIsChecked = false;                  
                        //FilterIsChecked = false;
                        ReSetPageStatus();
                        if (MarkerList.Count < 300)
                        {
                            OnSelectAllCmd(true);
                        }
                    }
                    LoadImageVisible= Visibility.Collapsed;
                   // FinishLoadProcess();
                });
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
                Messages.ShowMessage("网络请求发生错误，请改变查询条件稍后重试");
                LoadImageVisible = Visibility.Collapsed;
            }
        }
        //private void BeginLoadDsProcess()
        //{
        //    ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
        //    {
        //        ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
        //        this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("StartLoading"));
        //    });
        //}

        //private void LoadingDsProcess(string msg = "")
        //{
        //    if (string.IsNullOrEmpty(msg))
        //        msg = "正在加载历史标注...";//Helpers.ResourceHelper.FindKey("Loading");
        //    System.Windows.Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        this.progressView.ViewModel.ProgressValue = msg;
        //    });
        //}

        //private void FinishLoadProcess()
        //{
        //    ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
        //    {
        //        this.progressView.ViewModel.ProgressValue = string.Empty;
        //        ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        //    });
        //}
        public void OnConfirmSearchByFilterThread()
        {
            Thread thread = new Thread(OnConfirmSearchByFilter);
            thread.Start();
            FilterIsChecked = false;
        }
        private void OnPoiChecked(MarkerNew MarkerNew)
        {
            try
            {
                if (!MarkerNew.IsChecked)
                {
                    AllPoiSelectedIsChecked = false;
                }
                var model = MarkerList.Exists(t => t.IsChecked == false);
                if (!model && MarkerList.Count > 0)
                {
                    AllPoiSelectedIsChecked = true;
                }

                OnShowMaker(MarkerNew, MarkerNew.IsChecked);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 显示隐藏标注
        /// </summary>
        private void OnShowMaker(MarkerNew MarkerNew, bool ischecked)
        {
            var list = new List<MarkerNew>();
            list.Add(MarkerNew);
            MarkerHelper.Instance.OnShowMaker(list, ischecked);
            //try
            //{
            //    if (MarkerNew.Type == 1)
            //    {
            //        GviMap.PoiManager.SetVisible(MarkerNew.MarkerId.ToString(), MarkerNew.cat_Name, ischecked);
            //    }
            //    else if (MarkerNew.type == 2 || MarkerNew.type == 3)
            //    {
            //        GviMap.LinePolyManager.SetPoiVisible(MarkerNew.id.ToString(), ischecked);
            //    }
            //}
            //catch (Exception e)
            //{
            //    SystemLog.Log(e);
            //}
        }


        private void OnSelectAllCmd(bool ischeckd)
        {
            try
            {
                MarkerList.ForEach(e =>
                {

                    e.IsChecked = ischeckd;
                    OnShowMaker(e, e.IsChecked);
                });


            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnInverseSelectionCmd()
        {
            try
            {
                MarkerList.ForEach(e =>
                {
                    if (e.IsChecked)
                    {
                        e.IsChecked = false;
                        OnShowMaker(e, e.IsChecked);
                    }
                    else
                    {
                        e.IsChecked = true;
                        OnShowMaker(e, e.IsChecked);
                    }

                });
                var model = MarkerList.Exists(t => t.IsChecked == false);
                if (model)
                {
                    AllPoiSelectedIsChecked = false;
                }
                else
                {
                    AllPoiSelectedIsChecked = true;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        #endregion





        private void OnMouseDownCommand()
        {
            ImageIsOpen = true;
        }

        private void AxMapControl_RcCameraChanged(bool IsPositionChanged, bool IsRotationChanged)
        {
            if (clickDoubleStatus) return;
            string newgeom = MarkerHelper.Instance.GetCurrentRange();
            if (string.IsNullOrEmpty(newgeom)) return;
            //MarkerHelper.Instance.ClearDicCache();
            Task.Run(() =>
            {
                MarkerList = MarkerHelper.Instance.RequestMarkerList(searchText, geom: newgeom);
                ReSetPageStatus();
                if (_currentSelectedItem == null) return;
                if (_markerSet.Where(t => t.MarkerId == _currentSelectedItem.MarkerId).Count() > 0)
                {
                    _markerSet.FirstOrDefault(t => t.MarkerId == _currentSelectedItem.MarkerId).IsSelected = true;
                }
                else
                    _currentSelectedItem = null;
            });
        }
        public void LoadData()
        {
            MapEventRegistManager(true);
            // init 
            ReSetPageStatus();
            string newgeom = MarkerHelper.Instance.GetCurrentRange();
            if (string.IsNullOrEmpty(newgeom)) return;
            //Task.Run(() =>
            //{
            //    //_MarkerNewList = MarkerHelper.Instance.RequestMarkerList(loadStatus: true, geom: newgeom);
            //    MarkerList = MarkerHelper.Instance.RequestMarkerList();
            //    ReSetPageStatus();
            //});
            //if (MarkerSearchList.Count == 0)
            //{
            //    Thread thread = new Thread(InitLoadPoiData);
            //    thread.Start();
            //   // InitLoadPoiData();
            //}

            InitLoadPoiData();
        }

        private readonly int REQUESTNUMLIMIT = 50;
        private readonly int MAXDISPLAYNUM = 3000;

        private void InitLoadPoiData()
        {
            string tag = string.Empty;
            if (!string.IsNullOrEmpty(LabelSelectedText.ToString()))
            {
                string tags = LabelSelectedText.ToString();
                tag = tags.Substring(0, tags.Length - 1);
            }

            AllPoiSelectedIsChecked = false;

            int getdatacurrentPage = 0;
            Task.Run(() =>
            {
                //if (MarkerSearchList.Count > 0)
                //{
                //    MarkerHelper.Instance.OnShowMaker(MarkerSearchList, false);
                //}

                MarkerSearchList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), WktPoly, LevelSelectedItem);
                MarkerList = MarkerSearchList;
                ReSetPageStatus();
            });
        }

        private void MapEventRegistManager(bool on)
        {
            if (on)
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectMarker;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelectMarker;

                //GviMap.AxMapControl.RcLButtonUp -= AxMapControl_RcLButtonUp;
                //GviMap.AxMapControl.RcMouseWheel -= AxMapControl_RcMouseWheel;
                //GviMap.AxMapControl.RcMButtonUp -= AxMapControl_RcMButtonUp;
                //GviMap.AxMapControl.RcMButtonUp += AxMapControl_RcMButtonUp;
                //GviMap.AxMapControl.RcLButtonUp += AxMapControl_RcLButtonUp;
                //GviMap.AxMapControl.RcMouseWheel += AxMapControl_RcMouseWheel;
                //GviMap.AxMapControl.RcCameraChanged -= AxMapControl_RcCameraChanged;
                //GviMap.AxMapControl.RcCameraChanged += AxMapControl_RcCameraChanged;
                GviMap.AxMapControl.RcCameraFlyFinished -= AxMapControl_RcCameraFlyFinished;
                //GviMap.AxMapControl.RcCameraFlyFinished += AxMapControl_RcCameraFlyFinished;
            }
            else
            {
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelectMarker;
                GviMap.AxMapControl.RcCameraChanged -= AxMapControl_RcCameraChanged;
                GviMap.AxMapControl.RcCameraFlyFinished -= AxMapControl_RcCameraFlyFinished;
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;

            }
        }
        /// <summary>
        /// reset page btns to init status
        /// </summary>
        private void ResetPageControlBtnStatus(bool Enable)
        {
            IsTopBtnEnabled = Enable;
            IsPrevBtnEnabled = Enable;
            IsNextBtnEnabled = Enable;
            IsEndBtnEnabled = Enable;
        }

        private void ReSetPageStatus()
        {
            totalNum = MarkerList.Count;
            int flag = totalNum % PAGESIZE;
            if (flag == 0)
            {
                totalPages = totalNum / PAGESIZE;
            }
            else
            {
                totalPages = totalNum / PAGESIZE + 1;
            }

            ResetPageControlBtnStatus(false);

            if (totalPages <= 1)
            {
                IsNextBtnEnabled = false;
                IsEndBtnEnabled = false;
            }
            else
            {
                IsNextBtnEnabled = true;
                IsEndBtnEnabled = true;
            }
            currentPage = 1;

            OnRefreshCerrentTablePage();
        }

        private bool AxMapControl_RcMButtonUp(uint Flags, int X, int Y)
        {
            return RequestMarker();
        }

        private bool AxMapControl_RcMouseWheel(uint Flags, short Delta, int X, int Y)
        {
            return RequestMarker();
        }

        private bool RequestMarker()
        {
            if (clickDoubleStatus) return false;
            string newgeom = MarkerHelper.Instance.GetCurrentRange();
            if (string.IsNullOrEmpty(newgeom)) return false;
            //MarkerHelper.Instance.ClearDicCache();
            Task.Run(() =>
            {
                MarkerList = MarkerHelper.Instance.RequestMarkerList(searchText, geom: newgeom);
                ReSetPageStatus();
                if (_currentSelectedItem == null) return;
                if (_markerSet.Where(t => t.MarkerId == _currentSelectedItem.MarkerId).Count() > 0)
                {
                    _markerSet.FirstOrDefault(t => t.MarkerId == _currentSelectedItem.MarkerId).IsSelected = true;
                }
                else
                    _currentSelectedItem = null;
            });
            return false;
        }
        private bool AxMapControl_RcLButtonUp(uint Flags, int X, int Y)
        {
            return RequestMarker();
        }

        private void AxMapControl_RcCameraFlyFinished(byte Type)
        {
            clickDoubleStatus = false;
            string newgeom = MarkerHelper.Instance.GetCurrentRange();
            if (string.IsNullOrEmpty(newgeom)) return;
            MarkerHelper.Instance.ClearDicCache();
            Task.Run(() =>
            {
                MarkerList = MarkerHelper.Instance.RequestMarkerList(searchText, geom: newgeom);
                ReSetPageStatus();
                if (_currentSelectedItem == null) return;
                if (_markerSet.Where(t => t.MarkerId == _currentSelectedItem.MarkerId).Count() > 0)
                {
                    _markerSet.FirstOrDefault(t => t.MarkerId == _currentSelectedItem.MarkerId).IsSelected = true;
                }
                else
                    _currentSelectedItem = null;
            });
        }
        protected override void Loaded()
        {
            //base.Loaded();
            //LoadData();
        }

        protected override void Unloaded()
        {
            //base.Unloaded();
            //_currentSelectedItem = null;
            //MarkerSet = null;
            //MapEventRegistManager(false);
        }
        /// <summary>
        /// 搜索
        /// </summary>
        private async void OnSearchCommand()
        {
            string tag = string.Empty;
            if (!string.IsNullOrEmpty(LabelSelectedText.ToString()))
            {
                string tags = LabelSelectedText.ToString();
                tag = tags;//.Substring(0, tags.Length - 1);
            }
            await Task.Run(() =>
            {
                //if(MarkerSearchList.Count>0)
                //{
                //    MarkerHelper.Instance.OnShowMaker(MarkerSearchList, false);
                //}
                currentPage = 1;
                MarkerSearchList = MarkerHelper.Instance.RequestMarkerListBySearchFilter(tag, searchText, _startDate.ToString(), _endDate.ToString(), WktPoly, LevelSelectedItem);
                MarkerList = MarkerSearchList;
                ReSetPageStatus();
            });
        }
        private void DealWithSelectedMenu(object para)
        {
            MmcComboboxData Data = (MmcComboboxData)para;
            poiAreaSelectFlag = false;
            MethodInfo method = this.GetType().GetMethod(Data.NavMethodName);
            if (method != null)
            {
                //var labels = "a,b,c,d,e,f,g".Split(',').ToList();
                //var values = "78,59,62,36,15,91,42".Split(',').ToList();
                //var reportPreVM =new ReportPreViewModel(labels, values);

                method.Invoke(this, new object[] { });
            }
        }
        private bool clickDoubleStatus = false;
        private void OnButtonDownCommand(MouseButtonEventArgs obj)
        {
            if (obj == null) return;
            _markerSet.ForEach(t => t.IsSelected = false);
            MarkerNew MarkerNew = (obj.Source as FrameworkElement).DataContext as MarkerNew;
            if (MarkerNew == null) return;
            MarkerNew.IsSelected = true;
            _currentSelectedItem = MarkerNew;
            if (obj.ClickCount == 2)
            {
                clickDoubleStatus = true;
                MarkerNew.IsChecked = true;
                flyToMarkerPoi(MarkerNew.MarkerId.ToString());
            }
        }

        public void flyToMarkerPoi(string poiId)
        {
            var marker = MarkerHelper.Instance.MarkerDic[poiId];
            marker.IsChecked = true;
            OnShowMaker(marker, true);

            if (GviMap.LinePolyManager.ContainsKey(poiId))
                GviMap.LinePolyManager.Flyto(poiId);
            else
                GviMap.PoiManager.FlyTo(poiId);
        }

        private void OnDeteleTagCommand(object obj)
        {
            if (obj == null) return;
            TagItem tagItem = obj as TagItem;

            if (!Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("MMV_DelLabel"), Helpers.ResourceHelper.FindKey("MMV_ConfirmDel") + tagItem.name + "?"))
            {
                return;
            }
            var marker = MarkerHelper.Instance.MarkerDic[tagItem.marker_id.ToString()];
            marker.IsChecked = true;
            OnShowMaker(marker, true);
            var result = MarkerHelper.Instance.DeleteMarkerTag(tagItem.marker_id.ToString(), tagItem.id.ToString());
            if (result)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeleteSuccess"));
                MarkerSet.Single(t => t.MarkerId == tagItem.marker_id).Tags.Remove(tagItem);
                OnPropertyChanged("tags");
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeleteFailed"));
                return;
            }

        }

        private void OnEditCommand(object obj)
        {
            if (obj == null) return;
            MarkerNew MarkerNew = obj as MarkerNew;
            updateMarkerPoi(MarkerNew.MarkerId.ToString(), MarkerNew.Type.ToString());
        }
        private void OnDeleteCommand(object obj)
        {
            if (obj == null) return;
            MarkerNew MarkerNew = obj as MarkerNew;
            List<string> list = new List<string>();
            list.Add(MarkerNew.MarkerId.ToString());
            deleteMarkerPoi(list);
        }

        private void OnBatchDeleteCommand()
        {
            var list = MarkerList.Where(t => t.IsChecked == true).ToList();
            if (list.Count > 0)
            {
                deleteMarkerPoi(list.Select(t => t.MarkerId.ToString()).ToList());
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SelectedDeletePoi"));
            }
        }

        /// <summary>
        /// 删除标注
        /// </summary>
        /// <param name=""></param>
        public void deleteMarkerPoi(List<string> objJson)
        {
            if (!Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("DeletePoi"), Helpers.ResourceHelper.FindKey("ConfirmDeletePoi")))
                return;
            try
            {
                if (MarkerHelper.Instance.DeleteMarker(objJson))
                {
                    foreach (var item in objJson)
                    {
                        int id = Convert.ToInt32(item);

                        MarkerList.Remove(MarkerList.Where(t => t.MarkerId == id).FirstOrDefault());


                        //删除渲染
                        if (GviMap.PoiManager.ContainsKey(item))
                        {
                            if (!GviMap.PoiManager.DeletePoi(item))
                                SystemLog.Log(string.Format("渲染层删除标注失败,标注id={0}", item));
                        }
                        else
                        {
                            if (!GviMap.LinePolyManager.DeletePoi(item))
                                SystemLog.Log(string.Format("渲染层删除标注失败,标注id={0}", item));
                        }

                        if (_accountListVModel != null)
                        {
                            if (objJson.Contains(_accountListVModel.PoiId))
                            {
                                _accountListVModel.OnCloseWindow();
                            }

                        }
                    }
                    //OnSearchCommand();
                    LoadLastLabels();//加载上一次查询后的结果
                }
                else
                    SystemLog.Log(string.Format("服务器删除标注失败,标注id={0}", objJson));
            }
            catch
            {
            }
        }

        private void OnAccountCommand(object obj)
        {
            if (obj == null) return;
            MarkerNew MarkerNew = obj as MarkerNew;
            openAccountView(MarkerNew.MarkerId.ToString(), "");
        }
        //private AccountView _accountView;

        private AccountListVModel _accountListVModel;
        MarkerViewModel markerViewModel;
        /// <summary>
        /// 更新标注
        /// </summary>
        /// <param name=""></param>
        public void updateMarkerPoi(string poiId, string geotype)
        {

            if (MarkerHelper.Instance.MarkerDic.ContainsKey(poiId))
            {
                if (markerViewModel != null && markerViewModel.MarkerModel.MarkerId.ToString() == poiId)
                {
                    return;
                }
                else if (markerViewModel != null && markerViewModel.MarkerModel.MarkerId.ToString() != poiId)
                {
                    markerViewModel.CloseView();
                }
                var marker = MarkerHelper.Instance.MarkerDic[poiId];
                marker.IsChecked = true;
                OnShowMaker(marker, true);

                if (int.TryParse(geotype, out int type))
                {
                    marker.Type = type;
                }



                if (geotype == "1")
                {
                    markerViewModel = new PoiMarkerViewModel();
                }
                else if (geotype == "2")
                {

                    markerViewModel = new LineMarkerViewModel();
                }
                else if (geotype == "3")
                {
                    markerViewModel = new FaceMarkerViewModel();
                }
                else
                {
                    markerViewModel = new MarkerViewModel();
                }
                markerViewModel.OnCloseMarkerView += markerViewModel_OnCloseMarkerView;
                markerViewModel.ReAssignData(marker, true, false);
                markerViewModel.OnRefreshPoiList -= new Action<MarkerNew>(OnRefreshPoiList);
                markerViewModel.OnRefreshPoiList += new Action<MarkerNew>(OnRefreshPoiList);
            }
        }

        private void markerViewModel_OnCloseMarkerView(bool obj)
        {
            if (obj)
            {
                if (markerViewModel != null)
                {
                    markerViewModel.MarkerModel.IsChecked = true;
                    OnShowMaker(markerViewModel.MarkerModel, true);
                }
                markerViewModel = null;
            }
        }


        //public void RefreshSource(MarkerNew model)
        //{
        //    Task.Run(() =>
        //    {
        //        var list = new List<MarkerNew>();
        //        list.Add(model);
        //        MarkerSet.Remove(MarkerSet.Last());
        //        list.InsertRange(1, MarkerSet);
        //        MarkerSet = new ObservableCollection<MarkerNew>(list);
        //        MarkerHelper.Instance.UpdateMarkerList(model);

        //    });
        //}

        /// <summary>
        /// 打开台账
        /// </summary>
        /// <param name="poiId"></param>
        /// <param name="geotype"></param>
        public void openAccountView(string poiId, string geotype)
        {
            //AddAccountView(2249);
            /*if (_accountView == null)
            {
                _accountView = new AccountView();
                _accountView.Closed += (sender, e) => { _accountView = null; };
            }
            _accountView.Owner = System.Windows.Application.Current.MainWindow;
            _accountView.LoadPage(poiId);
            _accountView.Show();*/

            if (_accountListVModel == null)
            {
                _accountListVModel = new AccountListVModel();
            }
            _accountListVModel.ShowWindow(poiId/*, geotype*/);
        }


        private void OnRefreshPoiList(MarkerNew MarkerNew)
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {

                SetTagetMarker(MarkerNew);
                MapEventRegistManager(true);
                var item = MarkerList?.ToList().Find(p => p.Code == MarkerNew.Code);
                MarkerList.Remove(item);
                this.MarkerList.Add(MarkerNew);
            });
        }

        private void SetTagetMarker(MarkerNew inModel)
        {
            var list = new List<MarkerNew>();
            list.Add(inModel);

            if (inModel.ImgPath.ToLower().Contains(WebConfig.MspaceHostUrl))
                inModel.ImgPath = inModel.ImgPath;
            else
                inModel.ImgPath = string.Format("{0}/resource{1}", WebConfig.MspaceHostUrl, inModel.ImgPath);

            if (MarkerSet?.Count != 0)
            {

                var item = MarkerSet?.ToList().Find(p => p.MarkerId == inModel.MarkerId);
                if (item != null)
                {
                    MarkerSet?.Remove(item);
                }
                else if (item == null && MarkerSet.Count >= PAGESIZE)
                {
                    item = MarkerSet?.Last();
                    MarkerSet?.Remove(item);
                }
            }
            list.InsertRange(1, MarkerSet);
            MarkerSet = new ObservableCollection<MarkerNew>(list);
            MarkerHelper.Instance.UpdateMarkerList(inModel);
            MarkerSet.ForEach(p => p.IsSelected = false);
            MarkerSet.ToList().Find(p => p.MarkerId == inModel.MarkerId).IsSelected = true;
            // MarkerList.Insert(0, inModel);

        }
        PoiMarkerViewModel poiMarkerViewModel = null;
        public void CreatePoiMaker()
        {

            poiMarkerViewModel = new PoiMarkerViewModel();
            poiMarkerViewModel.ReAssignData(null, false, false);
            poiMarkerViewModel.OnRefreshPoiList -= new Action<MarkerNew>(OnRefreshPoiList);
            poiMarkerViewModel.OnRefreshPoiList += new Action<MarkerNew>(OnRefreshPoiList);
        }
        LineMarkerViewModel lineMarkerViewModel = null;
        public void CreatePolyLineMaker()
        {
            lineMarkerViewModel = new LineMarkerViewModel();
            lineMarkerViewModel.ReAssignData(null, false, false);

            lineMarkerViewModel.OnRefreshPoiList -= new Action<MarkerNew>(OnRefreshPoiList);
            lineMarkerViewModel.OnRefreshPoiList += new Action<MarkerNew>(OnRefreshPoiList);
        }

        FaceMarkerViewModel markerViewModelNew = null;
        public void CreatePolygonMaker()
        {
            markerViewModelNew = new FaceMarkerViewModel();
            markerViewModelNew.ReAssignData(null, false, false);

            markerViewModelNew.OnRefreshPoiList -= new Action<MarkerNew>(OnRefreshPoiList);
            markerViewModelNew.OnRefreshPoiList += new Action<MarkerNew>(OnRefreshPoiList);
        }


        private void AxMapControl_RcMouseClickSelectMarker(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            var pt = IntersectPoint;
            if (pt == null)
                return;

            if (PickResult == null)
                return;
            IRenderPOIPickResult rPoiPk = null;
            IRenderPolylinePickResult rLinePk = null;
            IRenderPolygonPickResult rPolyPk = null;
            if (PickResult is IRenderPOIPickResult)
                rPoiPk = PickResult as IRenderPOIPickResult;

            if (PickResult is IRenderPolylinePickResult)
                rLinePk = PickResult as IRenderPolylinePickResult;

            if (PickResult is IRenderPolygonPickResult)
                rPolyPk = PickResult as IRenderPolygonPickResult;
            if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
            {
                index++;
                string key = pt.X.ToString() + pt.Y.ToString();
                var markerList = MarkerHelper.Instance.MarkerDic;

                if (index == 1)
                {
                    firstTick = DateTime.Now.Ticks;
                    foreach (var ii in markerList.Values.ToArray())
                    {
                        if (!string.IsNullOrEmpty(ii.Guid) && (rPoiPk?.RenderPOI.Guid.ToString() == ii.Guid || rLinePk?.RenderPolyline.Guid.ToString() == ii.Guid || rPolyPk?.RenderPolygon.Guid.ToString() == ii.Guid))
                        {
                            //MarkerHelper.Instance.UpdateMarkerList(ii);
                            //_markerSet.ForEach(t => t.IsSelected = false);
                            //ii.IsSelected = true;
                            SetTagetMarker(ii);
                            if (GviMap.LinePolyManager.ContainsKey(ii.MarkerId.ToString()))
                            {
                                var render = GviMap.LinePolyManager.GetPoi(ii.MarkerId.ToString()).Item3;
                                GviMap.LinePolyManager.HightLight((IRenderGeometry)render);
                            }
                            break;
                        }
                    }
                }
                //else if (index == 2)
                //{
                //    long secondTick = 0;
                //    secondTick = DateTime.Now.Ticks;
                //    long interTime = (secondTick - firstTick) / 1000;

                //    foreach (var ii in markerList.Values.ToArray())
                //    {

                //        if (rPoiPk?.RenderPOI.Guid.ToString() == ii.guid || rLinePk?.RenderPolyline.Guid.ToString() == ii.guid || rPolyPk?.RenderPolygon.Guid.ToString() == ii.guid)
                //        {
                //            if (interTime > 0 && interTime < 2000)
                //            {
                //                MarkerHelper.Instance.UpdateMarkerList(ii);
                //                updateMarkerPoi(ii.id.ToString(), ii.type.ToString());
                //            }
                //            break;
                //        }
                //    }
                //}
                //if (index == 2)
                //{
                //    Console.WriteLine(index);
                //    prvguid = null;
                index = 0;
                //    firstTick = 0;
                //}
            }
            else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
            {
                index = 0;
            }
        }

        private void tagsSource()
        {

        }

        private ReportRankingViewModel _reportRankingViewModel;
        public void DowloadDoc()
        {

            var select = MarkerList.Where(t => t.IsChecked == true).ToList();
            if (select?.Count > 500)
            {
                Messages.ShowMessage("选择的标注超过500条，请减少数目后导出");
            }
            else
            {
                if (select.Count == 0)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SelectedExportReport"));
                return;
            }
            LabelManagementVModel labelManagementVModel = new LabelManagementVModel();

            List<string> arr = new List<string>();
            arr = labelManagementVModel.TagSource.Select(t => t.name).ToList();

            if (arr.Count == 0)
            {
                var tags = MarkerHelper.Instance.GetTagsSource();
                arr = tags.Select(t => t.name).ToList();
            }



            List<string> valuelist = new List<string>();
            List<string> valuecount = new List<string>();
            List<string> selecttagelist = new List<string>();
            var tagList = select.Select(t => t.Tags).Where(e => e.Count > 0).ToList();

            tagList.ForEach(e =>
            {
                valuelist.AddRange(e.Select(t => t.name).ToList());
            });
            selecttagelist = valuelist.Distinct().ToList();
            selecttagelist.ForEach(e =>
            {
                valuecount.Add(valuelist.Where(t => t == e).Count().ToString());
            });
           
                _reportRankingViewModel = new ReportRankingViewModel(selecttagelist, valuecount, select);
            }
            //var reportPreVM = new ReportPreViewModel(selecttagelist, valuecount, select);
            
            //var select = _markerSet.Where(t => t.IsChecked == true).ToList();
            //if (select.Count == 0)
            //{
            //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SelectedExportReport"));
            //    return;
            //}
            //var array = select.Select(t => t.MarkerId).ToArray();
            //string poiids = string.Join(",", array);
            //SaveFileDialog saveDlg = new SaveFileDialog();
            //string mydocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //saveDlg.InitialDirectory = mydocPath/* + "\\"*/;
            //saveDlg.Filter = "word文档|*.doc";
            //saveDlg.FilterIndex = 2;
            //saveDlg.FileName = GetTimeStamp();
            //if (saveDlg.ShowDialog() == DialogResult.OK)
            //{
            //    //httpDowLoadManager.DownloadReport(saveDlg.FileName, poiids, DownloadResult);
            //    Task.Run(() =>
            //    {
            //        string downloadReport = string.Format("{0}?marker_id={1}", MarkInterface.DownloadMarksReportInf, poiids);
            //        HttpServiceHelper.Instance.DownloadFile(downloadReport, saveDlg.FileName, DownloadResult);
            //    });
            //}
        }

        public void DowloadDocByArea()
        {
            //RCDrawManager.Instance.PolygonDraw.Register(GviMap.AxMapControl, drawCustomer, RCMouseOperType.PickPoint);
            //RCDrawManager.Instance.PolygonDraw.OnDrawFinished += Rone_PolygonDraw_OnDrawFinished;
            //FilterorExportFlag = false;
            //AreaIsSelected = false;
        }
        protected void CreateTempRObj()
        {
            var polygon = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPolygon,
                          gviVertexAttribute.gviVertexAttributeZ) as IPolygon;
            polygon.SpatialCRS = GviMap.SpatialCrs;
            var rPolygon = GviMap.ObjectManager.CreateRenderPolygon(polygon, GviMap.LinePolyManager.SurfaceSym,
                GviMap.ProjectTree.RootID);

            if (GviMap.TempRObjectPool.ContainsKey(PoiAreaMarkerKey))
            {
                if (GviMap.TempRObjectPool[PoiAreaMarkerKey] != null)
                {
                    var item = GviMap.TempRObjectPool[PoiAreaMarkerKey] as IRenderPolygon;

                    if (item != null)
                    {
                        var itemObject = item as IRObject;
                        var Deleteflag = GviMap.ObjectManager.DeleteObject(itemObject.Guid);
                    }
                }
                GviMap.TempRObjectPool[PoiAreaMarkerKey] = rPolygon;
            }
            else
            {
                GviMap.TempRObjectPool.Add(PoiAreaMarkerKey, rPolygon);
            }
        }

        private void Rone_PolygonDraw_OnDrawFinished(object sender, object result)
        {
            try
            {
                if (!poiAreaSelectFlag)
                {
                    return;
                }
                var rPolygon = result as IRenderPolygon;
                polygon = rPolygon.GetFdeGeometry() as IPolygon;
                polygon.SpatialCRS = GviMap.SpatialCrs;

                if (polygon == null || polygon.ExteriorRing.PointCount < 4)
                {
                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    return;
                }
                try
                {
                    var rpolygon1 = GviMap.TempRObjectPool[PoiAreaMarkerKey] as IRenderPolygon;
                    rpolygon1?.SetFdeGeometry(polygon);
                    rPolygon.Symbol.BoundarySymbol.Color = Color.FromArgb(255, 0, 255, 255);
                    rpolygon1.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    GviMap.TempRObjectPool[PoiAreaMarkerKey] = rpolygon1;
                    polygon = rpolygon1.GetFdeGeometry() as IPolygon;
                    polygon.SpatialCRS = GviMap.SpatialCrs;
                    var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                    string _poiHost = json.poiUrl;
                    string wkt_poly = polygon.AsWKT();
                    _geom = wkt_poly;

                    AreaIsSelected = true;
                    AreaPoiSelectedModel areaPoiSelected = new AreaPoiSelectedModel();
                    areaPoiSelected.AreaSelectedPolygon = rpolygon1;
                    areaPoiSelected.WktPoly = wkt_poly;
                    WktPoly = wkt_poly;
                    AreaPoiDic.Clear();
                    AreaPoiDic.Add(AreaIsSelected, areaPoiSelected);

                    RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
                    RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
                    poiAreaSelectFlag = false;
                }
                catch (Exception e)
                {
                    SystemLog.Log(e);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private MarkerView _markerView;
        protected void SetOutlineColor(Color c)
        {
            if (_markerView == null) _markerView = new MarkerView();
            _markerView.OutlineColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(255, 0, 255, 255);
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)//请求报头设置
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        private void OnExportCommand()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = FileFilterStrings.WORD;
            saveFileDialog.FileName = GetTimeStamp();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFileName = saveFileDialog.FileName;
                var httpDowLoadManager = new HttpDowLoadManager();
                httpDowLoadManager.Token = HttpServiceUtil.Token;
                Task.Run(() =>
                {
                    string downloadReport = string.Format("{0}?limit={1}&token={2}&geom={3}", MarkInterface.MarksStatisticsReportbyGeomInf, 1000, HttpServiceUtil.Token, _geom);
                    HttpServiceHelper.Instance.DownloadFile(downloadReport, _currentFileName, DownloadResult);
                });

            }
            RCDrawManager.Instance.PolygonDraw.OnDrawFinished -= Rone_PolygonDraw_OnDrawFinished;
            RCDrawManager.Instance.PolygonDraw.UnRegister(GviMap.AxMapControl);
        }

        public void Analyzejsonstring(string input, out int Object_num)//序列化json并取值
        {
            List<string> namelist = new List<string>();

            using (Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(new StringReader(input)))
            {
                JObject o = (JObject)JToken.ReadFrom(reader);
                var d = o["data"];
                foreach (JObject e in d)
                {
                    var name = e["name"];
                    namelist.Add(name.ToString());
                }
                Object_num = namelist.Count;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public void DownloadResult(bool result)
        {
            if (result)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ReportSuccess"));
                return;
            }
            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ReportFaild"));
        }

        public void OnRefreshCerrentTablePage()
        {
            CurrentPageStatus = string.Format(" {0} / {1} ", currentPage, totalPages);
            MarkerSet = new ObservableCollection<MarkerNew>(MarkerList.Skip((currentPage - 1) * PAGESIZE).Take(PAGESIZE));
        }
        QueryEnvelopeManagementVModel queryEnvelopeManagement = new QueryEnvelopeManagementVModel();
        public void QueryAreaManage()
        {          
            queryEnvelopeManagement.freshCollection -= new Action(RefreshQueryCollection);
            queryEnvelopeManagement.freshCollection += new Action(RefreshQueryCollection);          
            queryEnvelopeManagement.QueryOpen();
            QueryAreaManageChecked = !QueryAreaManageChecked;
        }
        public void RefreshQueryCollection()
        {
            areaList.Clear();
            string url = MarkInterface.GetMarkQueryAreaCollectionInf;
            var json = ""; //@"{""page_size"":" + "" + @",""page"":" + page + "}";
            string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
            try
            {
                GetQueryMessage(resStr, areaList);
            }
            catch (Exception e)
            {
                SystemLog.WriteLog(e.ToString());
            }
            if (areaList?.Count > 0)
            {
                QueryAreaCollection = new ObservableCollection<string>();
                QueryAreaCollection?.Clear();
                foreach (var area in areaList)
                {
                    QueryAreaCollection?.Add(area.Name);                    
                }
            }
        }
        public void GetQueryMessage(string _input, List<QueryWktGroup> _areaList)
        {

            using (JsonTextReader reader = new JsonTextReader(new StringReader(_input)))
            {

                JArray jArray = (JArray)JToken.ReadFrom(reader);
                
                foreach (JObject obj in jArray)
                {
                    wktList = new List<string>();
                    string objID = obj["id"].ToString();
                    string objName = obj["name"].ToString();
                    JArray objContent = (JArray)obj["content"];
                    //var data = objContent["content"];
                    foreach (JObject enm in objContent)
                    {
                        var content = enm["content"];
                        wktList.Add(content.ToString());
                    }
                    QueryWktGroup queryWktGroup = new QueryWktGroup(objID, objName, wktList);
                    _areaList.Add(queryWktGroup);
                }
            }
        }
        public void LayerQueryManage()
        {
            Messages.ShowMessage("该项仅支持50个面以下的面状矢量图层作为查询条件");
            //if (LayerSelect != "" && LayerSelect != null)
            //{
            //    LayersQueryManagementVModel layersQueryManagementVModel = new LayersQueryManagementVModel();
            //    layersQueryManagementVModel.OpenLayersQuery(LayerSelect);
            //}
        }
        public void RefreshQueryLayersCollection()
        {
            QueryLayersCollection = new ObservableCollection<string>();
            QueryLayersCollection?.Clear();
            List<LayerItemModel> QueryLayers = ServiceManager.GetService<IDataBaseService>(null).GetOtherLayerItemModels(null);
            DataTable dt = new DataTable();
            if (QueryLayers != null) { 
            foreach (var item in QueryLayers)
            {
                if (item.Name != "")
                {
                    IShowLayer showLayer = item.Parameters as IShowLayer;

                    dt = showLayer.FuzzySearch("", FieldsFilterService.GetDefault(null).GetFilterFields(showLayer.AliasName), null);

                    var layer = showLayer as DisplayLayer;
                    var render = layer.Renderable as IFeatureLayer;
                    if (render.GeometryType == gviGeometryColumnType.gviGeometryColumnPolygon)
                    {
                        int rowNum = dt.DefaultView.Table.Rows.Count;
                        if (rowNum > 0 && rowNum <= 50)
                        {
                            QueryLayersCollection.Add(item.Name);
                        }
                    }
                    //this.ResultsSource = ((dt != null) ? dt.DefaultView : null);

                }
            }
        }
        }
        public void SetQueryLayers()
        {

        }
            /// <summary>
            /// 筛选区域-等级-全部等级
            /// </summary>
            /// <param name="isChecked"></param>
            private void OnLevelSelectedAllCmd(bool isChecked)
        {
            try
            {
                foreach (var item in LevelCollection)
                {
                    item.IsChecked = isChecked;
                }

                if (isChecked)
                {
                    LevelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelectedAll");
                }
                else
                {
                    LevelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelected");
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        /// <summary>
        /// 筛选区域-等级-选择项
        /// </summary>
        /// <param name="isChecked"></param>
        private void OnLevelItemCheckedCmd(bool isChecked)
        {
            try
            {
                if (!isChecked)
                {
                    LevelIsSelectAll = false;
                    LevelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelected");
                }
                else
                {
                    var count = LevelCollection.Count(t => t.IsChecked);
                    if (count == LevelCollection.Count)
                    {
                        LevelIsSelectAll = true;
                        LevelSelectedDiscribe = Helpers.ResourceHelper.FindKey("MarksM_LabelSelectedAll");
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }

        private void OnTagsIsCheckedAllCommand(bool isChecked)
        {
            if (isChecked)
            {
                LabelCollection.ForEach(t => t.LabelIsSelected = true);
            }
            else
            {
                LabelCollection.ForEach(t => t.LabelIsSelected = false);
            }

            UpdateTagType(isChecked);
        }

        private void OnTagItemCheckedCommand(LabelInfoModel tag)
        {
            if (tag.LabelIsSelected)
            {
                if (LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count)
                {
                    TagsIsCheckedAll = true;
                }
            }
            else
            {
                TagsIsCheckedAll = false;
            }

            if (tag.LabelId != "-1")
            {
                UpdateTagType(tag.LabelIsSelected);
            }
        }

        private void OnTagTypeCheckedCommand(TagTypeModel tagItem)
        {
            if (tagItem.IsChecked)
            {
                foreach (var item in tagItem.tags)
                {
                    var temp = LabelCollection.FirstOrDefault(t => t.LabelId == item.LabelId);
                    if (temp != null)
                    {
                        temp.LabelIsSelected = true;
                        if (DicTagTypeCheckedCount.ContainsKey(temp.LabelId))
                        {
                            DicTagTypeCheckedCount[temp.LabelId]++;
                        }
                        else
                        {
                            DicTagTypeCheckedCount.Add(temp.LabelId, 1);
                        }
                    }
                }

                if (LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count)
                {
                    TagsIsCheckedAll = true;
                }
            }
            else
            {
                foreach (var item in tagItem.tags)
                {
                    var tag = LabelCollection.FirstOrDefault(t => t.LabelId == item.LabelId);

                    if (tag != null)
                    {
                        if (DicTagTypeCheckedCount.ContainsKey(tag.LabelId))
                        {
                            if (DicTagTypeCheckedCount[tag.LabelId] > 0)
                                DicTagTypeCheckedCount[tag.LabelId]--;
                            tag.LabelIsSelected = (DicTagTypeCheckedCount[tag.LabelId] > 0);
                        }
                        else
                        {
                            tag.LabelIsSelected = false;
                        }
                    }
                }

                TagsIsCheckedAll = LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count;
            }
        }

        /// <summary>
        /// 更新标签类型
        /// </summary>
        private void UpdateTagType(bool isChecked)
        {
            foreach (var tagItem in LabelCollection)
            {
                foreach (var tagType in TagTypes)
                {
                    if (tagType.tags.Any(t => t.LabelId == tagItem.LabelId))
                    {
                        if (isChecked)
                        {
                            int count = 0;
                            foreach (var i in tagType.tags)
                            {
                                var tagFound = LabelCollection.FirstOrDefault(t => t.LabelId == i.LabelId);
                                if (tagFound != null && tagFound.LabelIsSelected)
                                    count++;
                            }

                            if (count == tagType.tags.Count)
                            {
                                tagType.IsChecked = true;
                            }
                        }
                        else
                        {
                            tagType.IsChecked = false;
                        }
                    }
                }
            }
        }
    }

    class RowObject
    {
        public string FID
        {
            get;
            set;
        }
        public string FCGUID
        {
            get;
            set;
        }
        public string FCName
        {
            get;
            set;
        }
        public object FeatureClass
        {
            get;
            set;
        }
        public IEnvelope Envelop
        {
            get;
            set;
        }
    }
}
