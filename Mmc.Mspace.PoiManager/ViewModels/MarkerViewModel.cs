using CaptureLib;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Windows.Utils;
using Mmc.Framework.Draw;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Mvvm;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class MarkerViewModel : BaseViewModel
    {
        #region varies

        [XmlIgnore] public Action<MarkerNew> OnRefreshPoiList;
        [XmlIgnore] public Action<bool> OnCloseMarkerView;
        protected DrawCustomerUC DrawCustomer;
        protected ICurveSymbol _surSym = null;
        protected bool IsEvenOn;
        protected string MarkerKey;
        protected readonly string LabelMarkerKey = "MmcLabelMarkerKey";
        private bool _isEdit;



        public MarkerView _markerView;

        public ILabel _label;

        private bool _isCapturedImg;
        private readonly CaptureLib.ScreenCaputre screenCaputre;
        private System.Windows.Size? lastSize;
        private readonly ExportProgressView progressView;
        private string _shootImgPath;

        protected HeightType SelectHeightType;
        private AmapLocationService _amapLocationService;

        #endregion

        #region binging varies and command

        private System.Windows.Media.Color? _color;

        public System.Windows.Media.Color? StyleColor
        {
            get { return this._color; }
            set
            {
                _color = value;
                OnPropertyChanged("StyleColor");
                var v = this._color.Value;
                OnColorChanged(Color.FromArgb(v.A, v.R, v.G, v.B));
            }
        }

        private Visibility _isSizeCtrVisibility;

        public Visibility IsSizeCtrVisibility
        {
            get { return this._isSizeCtrVisibility; }
            set
            {
                _isSizeCtrVisibility = value;
                OnPropertyChanged("IsSizeCtrVisibility");
            }
        }

        private Visibility _isLineCtrVisibility;

        public Visibility IsLineCtrVisibility
        {
            get { return this._isLineCtrVisibility; }
            set
            {
                _isLineCtrVisibility = value;
                OnPropertyChanged("IsLineCtrVisibility");
            }
        }

        private Visibility _isPoiCtrVisible;

        public Visibility IsPoiCtrVisible
        {
            get { return this._isPoiCtrVisible; }
            set
            {
                _isPoiCtrVisible = value;
                OnPropertyChanged("IsPoiCtrVisible");
            }
        }

        private Visibility _isFaceCtrVisibility;

        public Visibility IsFaceCtrVisibility
        {
            get { return this._isFaceCtrVisibility; }
            set
            {
                _isFaceCtrVisibility = value;
                OnPropertyChanged("IsFaceCtrVisibility");
            }
        }

        private MarkerNew _markerModel;

        public MarkerNew MarkerModel
        {
            get => _markerModel;
            set
            {
                _markerModel = value;
                OnPropertyChanged("MarkerModel");
            }
        }

        private string _viewTitle;

        public string ViewTitle
        {
            get => _viewTitle;
            set
            {
                _viewTitle = value;
                OnPropertyChanged("ViewTitle");
            }
        }

        private string _sizeName;

        public string SizeName
        {
            get => _sizeName;
            set
            {
                _sizeName = value;
                OnPropertyChanged("SizeName");
            }
        }

        private double _lon;

        public double Longitude
        {
            get => _lon;
            set
            {
                _lon = value;
                OnPropertyChanged("Longitude");
            }
        }

        private double _lat;

        public double Latitude
        {
            get => _lat;
            set
            {
                _lat = value;
                OnPropertyChanged("Latitude");
            }
        }
        public double Altitude;

        private string _coor;

        public string Coor
        {
            get => _coor;
            set
            {
                _coor = value;
                OnPropertyChanged("Coor");
            }
        }

        private ObservableCollection<PoiType> _poiTypes;

        [XmlIgnore]
        public ObservableCollection<PoiType> PoiTypes
        {
            get => _poiTypes;
            set
            {
                _poiTypes = value;
                OnPropertyChanged("PoiTypes");
            }
        }

        private ObservableCollection<HeightType> _heightTypes;

        [XmlIgnore]
        public ObservableCollection<HeightType> HeightTypes
        {
            get { return this._heightTypes; }
            set
            {
                _heightTypes = value;
                OnPropertyChanged("HeightTypes");
            }
        }

        private HeightType _selectedHeightType;

        [XmlIgnore]
        public HeightType SelectedHeightType
        {
            get { return this._selectedHeightType; }
            set
            {
                _selectedHeightType = value;
                OnPropertyChanged("SelectedHeightType");
            }
        }

        //private Dictionary<string, object> _tagItems;

        //[XmlIgnore]
        //public Dictionary<string, object> TagItems
        //{
        //    get { return this._tagItems; }
        //    set
        //    {
        //        _tagItems = value;
        //        OnPropertyChanged("TagItems");
        //    }
        //}

        private ObservableCollection<TagInfo> _tagItemsCollection;
        public ObservableCollection<TagInfo> TagItems
        {
            get { return _tagItemsCollection ?? (_tagItemsCollection = new ObservableCollection<TagInfo>()); }
            set
            {
                _tagItemsCollection = value;
                OnPropertyChanged("TagItems");
            }
        }

        private Dictionary<string, object> _selectdTagItems;

        private Dictionary<string, string> DicTags;

        [XmlIgnore]
        public Dictionary<string, object> SelectdTagItems
        {
            get { return this._selectdTagItems; }
            set
            {
                //TagItems = MarkerHelper.Instance.TagsDic;

                TagItems.Clear();
                TagItems.Add(new TagInfo { ItemTag = "Add" });
                foreach (var dicItem in MarkerHelper.Instance.TagsDic)
                {
                    TagItems.Add(new TagInfo
                    {
                        TagID = dicItem.Key,
                        TagName = dicItem.Value.ToString()
                    });
                }

                _selectdTagItems = value;
                OnPropertyChanged("SelectdTagItems");
            }
        }

        private bool _isOkBtnEnable;

        public bool IsOkBtnEnable
        {
            get => _isOkBtnEnable;
            set
            {
                _isOkBtnEnable = value;
                OnPropertyChanged("IsOkBtnEnable");
            }
        }

        private TextItem _selectedRank;
        public TextItem SelectedRank
        {
            get { return _selectedRank; }
            set { _selectedRank = value; OnPropertyChanged("SelectedRank"); }
        }
        private ObservableCollection<TextItem> _markRankTypes;

        public ObservableCollection<TextItem> MarkRankTypes
        {
            get { return _markRankTypes; }
            set { _markRankTypes = value; OnPropertyChanged("MarkRankTypes"); }
        }
        private TextItem _selectedMarkStatu;
        public TextItem SelectedMarkStatu
        {
            get { return _selectedMarkStatu; }
            set { _selectedMarkStatu = value; OnPropertyChanged("SelectedMarkStatu"); }
        }
        private ObservableCollection<TextItem> _markHandleStatus;

        public ObservableCollection<TextItem> MarkHandleStatus
        {
            get { return _markHandleStatus; }
            set { _markHandleStatus = value; OnPropertyChanged("MarkHandleStatus"); }
        }

        private bool _popupIsOpen;
        public bool PopupIsOpen
        {
            get { return _popupIsOpen; }
            set
            {
                _popupIsOpen = value;
                OnPropertyChanged("PopupIsOpen");
            }
        }

        private Dictionary<string, int> DicTagTypeCheckedCount;

        private bool _tagsIsCheckedAll;
        public bool TagsIsCheckedAll
        {
            get { return _tagsIsCheckedAll; }
            set
            {
                _tagsIsCheckedAll = value;
                OnPropertyChanged("TagsIsCheckedAll");
            }
        }

        private ObservableCollection<TagTypeModel> _tagTypes;
        public ObservableCollection<TagTypeModel> TagTypes
        {
            get { return _tagTypes ?? (_tagTypes = new ObservableCollection<TagTypeModel>()); }
            set { _tagTypes = value; OnPropertyChanged("TagTypes"); }
        }

        private ObservableCollection<LabelInfoModel> _labelCollection;
        public ObservableCollection<LabelInfoModel> LabelCollection
        {
            get { return _labelCollection ?? (_labelCollection = new ObservableCollection<LabelInfoModel>()); }
            set { _labelCollection = value; OnPropertyChanged("LabelCollection"); }
        }

        [XmlIgnore] public ICommand CancelCommand { get; set; }

        [XmlIgnore] public ICommand SaveCommand { get; set; }

        [XmlIgnore] public ICommand DisplayImgCommand { get; set; }

        [XmlIgnore] public ICommand FlytoCommand { get; set; }

        [XmlIgnore] public ICommand GetLocalCommand { get; set; }

        [XmlIgnore] public ICommand GetPictureCommand { get; set; }

        [XmlIgnore] public ICommand GetAddressCommand { get; set; }

        private RelayCommand<TagInfo> _tagCommand;
        public RelayCommand<TagInfo> TagCommand
        {
            get { return _tagCommand ?? (_tagCommand = new RelayCommand<TagInfo>(OnTagCommand)); }
            set
            {
                _tagCommand = value;
                OnPropertyChanged("DeleteCommand");
            }
        }

        private RelayCommand _cancelLabelFilterCmd;
        public RelayCommand CancelLabelFilterCmd
        {
            get { return _cancelLabelFilterCmd ?? (_cancelLabelFilterCmd = new RelayCommand(OnCancelLabelFilterCmd)); }
            set { _cancelLabelFilterCmd = value; }
        }

        private RelayCommand _confirmLabelFilterCmd;
        public RelayCommand ConfirmLabelFilterCmd
        {
            get { return _confirmLabelFilterCmd ?? (_confirmLabelFilterCmd = new RelayCommand(OnConfirmLabelFilterCmd)); }
            set { _confirmLabelFilterCmd = value; }
        }

        private RelayCommand<TagTypeModel> _tagTypeCheckedCommand;
        public RelayCommand<TagTypeModel> TagTypeCheckedCommand
        {
            get { return _tagTypeCheckedCommand ?? (_tagTypeCheckedCommand = new RelayCommand<TagTypeModel>(OnTagTypeCheckedCommand)); }
            set { _tagTypeCheckedCommand = value; }
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
        #endregion

        public MarkerViewModel( /*int  type*/)
        {
            if (_markerView == null) _markerView = new MarkerView();
            _markerView.DataContext = this;

            screenCaputre = new CaptureLib.ScreenCaputre();
            progressView = new ExportProgressView();
            _amapLocationService = new AmapLocationService();
            //_shootImgPath = @"C:\Users\Admin\AppData\Local\Temp\MSpace\shootImage\temp.bmp";
            _shootImgPath = string.Empty;
            screenCaputre.ScreenCaputred -= OnScreenCaputred;
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled -= OnScreenCaputreCancelled;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;

            this.CancelCommand = new RelayCommand(CloseView);
            this.SaveCommand = new RelayCommand(SaveMarkerData);
            this.GetLocalCommand = new RelayCommand(GetLocation);
            this.FlytoCommand = new RelayCommand(FlyToObject);
            this.GetPictureCommand = new RelayCommand(() =>
            {
                this.HideView();
                Thread.Sleep(600);
                screenCaputre.StartCaputre(30, lastSize);
            });
            this.GetAddressCommand = new RelayCommand(GetAddress);
            this.DisplayImgCommand = new RelayCommand(ShowImage);

            this.Initialize();

            IsSizeCtrVisibility = Visibility.Visible;

            this.HeightTypes = new ObservableCollection<HeightType>()
            {
                new HeightType()
                {
                    HeighName = Helpers.ResourceHelper.FindKey("HeightAbsolute"),
                    HeightStyle = gviHeightStyle.gviHeightAbsolute
                },
                new HeightType()
                {
                    HeighName = Helpers.ResourceHelper.FindKey("HeightOnEverything"),
                    HeightStyle = gviHeightStyle.gviHeightOnEverything
                }
            };


            this._markHandleStatus = new ObservableCollection<TextItem>(CommonContract.GetMarkHandleStatus());
            this._markRankTypes = new ObservableCollection<TextItem>(CommonContract.GetMarkRankType());
            this._selectdTagItems = new Dictionary<string, object>();
            this.SelectHeightType = this.HeightTypes[0];
            this.SelectedMarkStatu = MarkHandleStatus[0];
            this.SelectedRank = MarkRankTypes[0];
        }
        //private MarksManagementVModel marksManagementVModel;

        //public MarksManagementVModel MarksManagementVModel
        //{
        //    get { return marksManagementVModel ?? (marksManagementVModel = new MarksManagementVModel()); }
        //    set { marksManagementVModel = value; OnPropertyChanged("MarksManagementVModel"); }
        //}
        private void Initialize()
        {

            //_label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
            //_label.VisibleMask = gviViewportMask.gviViewNone;
            //var textSym = _label.TextSymbol;
            //textSym.PivotAlignment = gviPivotAlignment.gviPivotAlignTopRight;
            //_label.TextSymbol = textSym;

            _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
            _label.VisibleMask = gviViewportMask.gviViewNone;
            var textSym = _label.TextSymbol;
            textSym.PivotAlignment = gviPivotAlignment.gviPivotAlignTopRight;
            _label.TextSymbol = textSym;

            loadTagsItem();
            var rd = new Random();
            var num = rd.Next(1000, 10000).ToString();
            var str = num.Substring(1);

            var timestr = DateTime.Now.ToString("yyyyMMddHHmmss");

            MarkerModel = new MarkerNew()
            {
                Code = "BZ" + timestr + str,
                //ImgPath = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png",
                ImgPath = "/no_photo_1200.png",
                Operator = CacheData.UserInfo.name,
                Phone = CacheData.UserInfo.phone,
                Account_end_time = CacheData.UserInfo.account_end_time,
                 Life_day = CacheData.UserInfo.life_day
            };
            IsOkBtnEnable = true;
        }

        private void loadTagsItem()
        {
            if (MarkerHelper.Instance.GetTagsSource().Count > 0)
            {

                this.DicTags = new Dictionary<string, string>();
                foreach (var item in MarkerHelper.Instance.GetTagsSource())
                {
                    DicTags.Add(item.id.ToString(), item.name);
                }

                TagItems.Clear();
                TagItems.Add(new TagInfo{ItemTag = "Add"});
            }
        }

        public virtual void ReAssignData(MarkerNew marker, bool isEdit, bool isInputAdd = false)
        {
            _isEdit = isEdit;

            if (isEdit && marker != null)
            {
                MarkerModel = marker;

                TagItems.Clear();
                TagItems.Add(new TagInfo { ItemTag = "Add" });

                foreach (var tag in marker.Tags)
                {
                    string id = Convert.ToString(tag.id);

                    if (DicTags.ContainsKey(id))
                    {
                        TagItems.Add(new TagInfo
                        {
                            TagID = id,
                            TagName = DicTags[id]
                        });
                    }
                }


                this.SelectedMarkStatu = MarkHandleStatus[Convert.ToInt32(marker.Status)];
                this.SelectedRank = MarkRankTypes.FirstOrDefault(t => t.Value.Contains(marker.Level.ToString()));

                this.ShowView();
            }
        }

        protected void OnScreenCaputreCancelled(object sender, EventArgs e)
        {
            this.ShowView();
        }

        protected void OnScreenCaputred(object sender, ScreenCaputredEventArgs e)
        {
            this.ShowView();
            lastSize = new System.Windows.Size(e.Bmp.Width, e.Bmp.Height);
            var bmp = e.Bmp;
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo info = new DirectoryInfo(temp);
            string MSpace = info.ToString() + "\\MSpace";
            if (!Directory.Exists(MSpace))
            {
                Directory.CreateDirectory(MSpace);
            }

            string shootImage = MSpace + "\\shootImage";
            if (!Directory.Exists(shootImage))
            {
                Directory.CreateDirectory(shootImage);
            }

            _shootImgPath = shootImage + "\\" + GetTimeStamp() + ".png";
            var result = ImageHelper.SaveImage(_shootImgPath, bmp);
            if (result)
            {
                MarkerModel.ImgPath = _shootImgPath;
                _isCapturedImg = true;
                Clipboard.SetImage(bmp);
            }
        }

        private string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        private void ShowImage()
        {
            if (!string.IsNullOrEmpty(MarkerModel.ImgPath))
                ImageUtil.InvokeImageProcess(MarkerModel.ImgPath);
        }

        protected void GetAddress()
        {
            MarkerModel.Address = _amapLocationService.GetAddressByCoor(Longitude, Latitude);
        }

        protected virtual void FlyToObject()
        {
            if (GviMap.TempRObjectPool[MarkerKey] != null)
            {
                GviMap.Camera.LookAtEnvelope(((IRenderGeometry)GviMap.TempRObjectPool[MarkerKey]).Envelope);
                ((IRenderGeometry)GviMap.TempRObjectPool[MarkerKey]).Glow(2000);
            }
        }

        protected virtual void GetLocation()
        {
            IsEvenOn = true;
            this.HideView();
        }

        private void CreateMarker()
        {
            MarkerHelper.Instance.RenderMarker(MarkerModel);
        }

        protected virtual void SaveMarkerData()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                    this.progressView.ViewModel.ProgressValue =
                        string.Format(Helpers.ResourceHelper.FindKey("Submitting"));
                });
                string api = string.Empty;
                if (_isEdit) api = MarkInterface.UpdateMarkInf;
                else api = MarkInterface.AddMarkInf;
                if (string.IsNullOrEmpty(MarkerModel.ImgPath))
                {
                    MarkerModel.ImgPath = "/no_photo_1200.png";
                }

                if (_isCapturedImg) MarkerModel.ImgPath = MarkerHelper.Instance.updateCaptureImg(_shootImgPath);
                //_shootImgPath = string.Empty;


                //var levelm = MarkRankTypes.Select(p => p.Key.Equals(SelectedRank.Key));
                MarkerModel.Level = GetSelectRankNum();
                MarkerModel.Status = GetSelectStatusNum();
                //转换提交格式
                var marksConverter = new MarksModelsConverter();
                PostMarkerNew postMarker = marksConverter.MarkerConverting(MarkerModel);

                int markerId = MarkerHelper.Instance.AddOrUpdateMarkerNew(postMarker, api, out string title);

                if (markerId > 0)
                {
                    string newMarkId = Convert.ToString(markerId);

                    MarkerModel.MarkerId = markerId;

                    MarkerModel.Title = title ?? this.MarkerModel.Title;
                    MarkerHelper.Instance.UpdateMarksTages();
                    //提交标签
                    var tags = PostTags(newMarkId);
                    MarkerModel.Tags = new ObservableCollection<TagItem>(tags);

                    MarkerHelper.Instance.UpdateMarkerList(MarkerModel);

                    OnRefreshPoiList?.Invoke(MarkerModel);
                    //  MarkerHelper.Instance.MarkerDic.Add(newMarkId, MarkerModel);


                }
            }
            catch (HttpException httpEx)
            {
                HttpException.ShowHttpExcetion(httpEx.Message);
                SystemLog.Log(httpEx);
            }
            catch (Exception ex)
            {
                if (ex is LoginExcetiop)
                    throw ex;
                SystemLog.Log(ex);
            }
            finally
            {
                FinishProcess();
                IsOkBtnEnable = true;
                this.CloseView();
            }
        }


        private List<TagItem> PostTags(string newMarkId)
        {
            var tagList = new List<TagItem>();

            var postTags = new PostTag();

            postTags.marker_id = int.Parse(newMarkId);
            var list = new List<string>();
            //if (_markerView.MC.SelectedItems == null) return null;
            //foreach (var item in _markerView.MC.SelectedItems)
            //    list.Add(item.Key.ToString());

            if (TagItems.Count > 0)
            {
                foreach (var item in TagItems)
                {
                    if (!string.IsNullOrWhiteSpace(item.TagID))
                        list.Add(item.TagID);
                }
            }

            postTags.tags = list.ToArray();
            if (postTags.tags != null && postTags.tags.Length > 0)
            {
                string json = JsonUtil.SerializeToString(postTags);
                MarkerHelper.Instance.PostMarkerTags(json);
            }

            if (postTags.tags != null)
                foreach (var ii in postTags.tags)
                {
                    var item = TagItems.FirstOrDefault(t => t.TagID == ii);
                    var tag = new TagItem()
                    {
                        marker_id = postTags.marker_id,
                        id = Convert.ToInt32(ii),
                        //name = this.TagItems[ii].ToString(),
                        name = item?.TagName
                    };
                    tagList.Add(tag);
                }
            return tagList;
        }

        public void ShowView()
        {
            _markerView.Owner = Application.Current.MainWindow;
            _markerView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _markerView.Show();
        }

        public void CloseView()
        {
            if (GviMap.TempRObjectPool.ContainsKey(MarkerKey))
            {
                ((IRenderable)GviMap.TempRObjectPool[MarkerKey]).VisibleMask = gviViewportMask.gviViewNone;
                GviMap.TempRObjectPool.Remove(MarkerKey);
            }

            if (GviMap.TempRObjectPool.ContainsKey(LabelMarkerKey))
            {
                ((IRenderable)GviMap.TempRObjectPool[LabelMarkerKey]).VisibleMask = gviViewportMask.gviViewNone;
                GviMap.TempRObjectPool.Remove(LabelMarkerKey);
            }


            _markerView?.Close();
            _markerView = null;
            if (OnCloseMarkerView != null)
            {
                OnCloseMarkerView(true);
            }
        }

        private void HideView()
        {
            _markerView?.Hide();
        }

        private void FinishProcess()
        {
            progressView.ViewModel.ProgressValue = string.Empty;
            ServiceManager.GetService<IShellService>(null).ProgressView
                .ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        }

        protected virtual void OnColorChanged(Color color)
        {

        }

        protected virtual void CreateTempRObj()
        {
            ((IRenderable)GviMap.TempRObjectPool[MarkerKey]).VisibleMask = gviViewportMask.gviViewNone;
            _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);

            //创建多边形 label
            if (!GviMap.TempRObjectPool.ContainsKey(LabelMarkerKey) && MarkerModel.Type != 1)
            {
                GviMap.TempRObjectPool.Add(LabelMarkerKey, _label);
            }
            else
            {
                GviMap.TempRObjectPool[LabelMarkerKey] = _label;
            }

            ((IRenderable)GviMap.TempRObjectPool[LabelMarkerKey]).VisibleMask = gviViewportMask.gviViewNone;
        }

        protected void OnSelectedHeightChange(HeightType heightType)
        {
            if (GviMap.TempRObjectPool.ContainsKey(MarkerKey))
            {
                var rPolygon = GviMap.TempRObjectPool[MarkerKey] as IRenderPolyline;
                rPolygon.MaxVisibleDistance = 50000;
                rPolygon.HeightStyle = heightType.HeightStyle;
            }
        }

        protected void SetColor(Color c)
        {
            if (_markerView == null) _markerView = new MarkerView();
            _markerView.ColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        protected void SetOutlineColor(Color c)
        {
            if (_markerView == null) _markerView = new MarkerView();
            _markerView.OutlineColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(c.A, c.R, c.G, c.B);
        }


        private int GetSelectRankNum()
        {
            int num = 0;
            if (SelectedRank != null)
            {
                if (SelectedRank.Key == CommonContract.MarkRankType.First.ToString())
                {
                    num = 1;
                }
                else if (SelectedRank.Key == CommonContract.MarkRankType.Second.ToString())
                {
                    num = 2;
                }
                else if (SelectedRank.Key == CommonContract.MarkRankType.Third.ToString())
                {
                    num = 3;
                }

                else if (SelectedRank.Key == CommonContract.MarkRankType.Fourth.ToString())
                {
                    num = 4;
                }

                else if (SelectedRank.Key == CommonContract.MarkRankType.Fifth.ToString())
                {
                    num = 5;
                }
                else
                {
                    num = 0;
                }
            }
            return num;
        }


        private int GetSelectStatusNum()
        {
            int num = -1;

            if (SelectedMarkStatu.Key == CommonContract.MarkHandleStatus.WaitingForCheck.ToString())
            {
                num = 0;
            }
            else if (SelectedMarkStatu.Key == CommonContract.MarkHandleStatus.NotInProcess.ToString())
            {
                num = 1;
            }
            else if (SelectedMarkStatu.Key == CommonContract.MarkHandleStatus.Handling.ToString())
            {
                num = 2;
            }

            else if (SelectedMarkStatu.Key == CommonContract.MarkHandleStatus.Handled.ToString())
            {
                num = 3;
            }

            else if (SelectedMarkStatu.Key == CommonContract.MarkHandleStatus.NoNeedHandle.ToString())
            {
                num = 4;
            }
            else
            {
                num = -1;
            }
            return num;
        }

        private void OnTagCommand(TagInfo info)
        {
            if (info.ItemTag == "Add")
            {
                DicTagTypeCheckedCount = new Dictionary<string, int>();
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

                LabelCollection.Clear();
                foreach (var item in MarkerHelper.Instance.GetTagsSource())
                {
                    LabelInfoModel labelInfo = new LabelInfoModel();
                    labelInfo.LabelName = item.name;
                    labelInfo.LabelId = item.id.ToString();
                    if (TagItems.Any(t => t.TagID == item.id.ToString()))
                        labelInfo.LabelIsSelected = true;
                    LabelCollection.Add(labelInfo);
                }

                UpdateTagType(true);

                if (LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count)
                {
                    TagsIsCheckedAll = true;
                }

                PopupIsOpen = true;

            }
            else
            {
                if (Messages.ShowMessageDialog("删除标签", $"是否删除标签「{info?.TagName}」"))
                {
                    var item = TagItems.FirstOrDefault(t => t.TagID == info.TagID);
                    if (item != null)
                    {
                        TagItems.Remove(item);
                    }
                }
            }
        }

        private void OnCancelLabelFilterCmd()
        {
            PopupIsOpen = false;
        }

        private void OnConfirmLabelFilterCmd()
        {
            TagItems.Clear();

            TagItems.Add(new TagInfo { ItemTag = "Add" });

            foreach (var labelInfoModel in LabelCollection)
            {
                if (labelInfoModel.LabelIsSelected)
                {
                    TagItems.Add(new TagInfo
                    {
                        TagID = labelInfoModel.LabelId,
                        TagName = labelInfoModel.LabelName
                    });
                }
            }

            PopupIsOpen = false;
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
            }

            if (LabelCollection.Count(t => t.LabelIsSelected) == LabelCollection.Count)
            {
                TagsIsCheckedAll = true;
            }
            else
            {
                TagsIsCheckedAll = false;
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

    public class TagInfo : BindableBase
    {
        private string _tagName;
        public string TagName
        {
            get { return _tagName; }
            set { SetAndNotifyPropertyChanged(ref _tagName, value); }
        }

        private string _tagId;
        public string TagID
        {
            get { return _tagId; }
            set { SetAndNotifyPropertyChanged(ref _tagId, value); }
        }

        private string _itemTag = "Delete";
        public string ItemTag
        {
            get { return _itemTag; }
            set { SetAndNotifyPropertyChanged(ref _itemTag, value); }
        }
    }
}