using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Windows.Utils;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule
{
    public class PoiBaseViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public Action<MarkerModel> RefreshPoiList;

        private readonly ExportProgressView progressView = new ExportProgressView();
        private string _imgUrl;
        private bool _isCapturedImg;
        private string _poiTitle;
        private string _detail;
        protected int _type;
        private readonly CaptureLib.ScreenCaputre screenCaputre = new CaptureLib.ScreenCaputre();
        private System.Windows.Size? lastSize;
        private _IRenderControlEvents_RcPictureExportBeginEventHandler _rcPictureExportBegin;
        private _IRenderControlEvents_RcPictureExportEndEventHandler _rcPictureExportEnd;
        private _IRenderControlEvents_RcPictureExportingEventHandler _rcPictureExporting;
        protected string marker_id;
        protected System.Windows.Media.Color? _color;
        protected System.Windows.Media.Color? _slColor;
        //protected string img_server_url;
        protected AmapLocationService amapLocationService;
        protected string _labelKey;
        protected string _polyMakerKey;

        private bool _IsSave = true;

        public bool IsSave
        {
            get { return _IsSave; }
            set { _IsSave = value; base.NotifyPropertyChanged("IsSave"); }
        }

        /*出图绝对路径*/
        private string _shootImgPath = @"C:\Users\Admin\AppData\Local\Temp\MSpace\shootImage\temp.bmp";

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        [XmlIgnore]
        public ICommand CreateCmd { get; set; }

        [XmlIgnore]
        public ICommand DisplayImgCommand { get; set; }

        [XmlIgnore]
        public ICommand FlytoCommand { get; set; }

        [XmlIgnore]
        public ICommand GetLocalCommand { get; set; }

        [XmlIgnore]
        public ICommand GetAddressCommand { get; set; }


        [XmlIgnore]
        public ICommand GetPictureCommand { get; set; }

        private Dictionary<string, object> _tagItems;
        [XmlIgnore]
        public Dictionary<string, object> TagItems
        {
            get { return this._tagItems; }
            set
            {
                base.SetAndNotifyPropertyChanged<Dictionary<string, object>>(ref this._tagItems, value, "TagItems");
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl
        {
            get { return this._imgUrl; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._imgUrl, value, "ImgUrl");
                OnImgUrlChanged(_imgUrl);
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public System.Windows.Media.Color? StyleColor
        {
            get { return this._color; }
            set
            {
                base.SetAndNotifyPropertyChanged<System.Windows.Media.Color?>(ref this._color, value, "StyleColor");
                var v = this._color.Value;
                OnColorChanged(Color.FromArgb(v.A, v.R, v.G, v.B));
            }
        }
        /// <summary>
        /// 面外边颜色
        /// </summary>
        public System.Windows.Media.Color? SLStyleColor
        {
            get { return this._slColor; }
            set
            {
                base.SetAndNotifyPropertyChanged<System.Windows.Media.Color?>(ref this._slColor, value, "SLStyleColor");
                var v = this._slColor.Value;
                OnSLColorChanged(Color.FromArgb(v.A, v.R, v.G, v.B));
            }
        }

        /// <summary>
        /// 详情
        /// </summary>
        public string Detial
        {
            get { return this._detail; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._detail, value, "Detial");
                OnDetailChanged(_detail);
            }
        }

        private string _adress;
        public string Address
        {
            get { return this._adress; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._adress, value, "Address");
                OnDetailChanged(_adress);
            }
        }

        private double _lng;
        public double Lng
        {
            get { return this._lng; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._lng, value, "Lng");
            }
        }
        /// <summary>
        /// 标注线长度
        /// </summary>
        private double _len;
        public double Len
        {
            get { return this._len; }
            set
            {
                
                base.SetAndNotifyPropertyChanged<double>(ref this._len, value, "Len");
                LineNumTransfer(_len);
            }
        }

        private string _lenTranfer;
        public string LenTranfer
        {
            get { return this._lenTranfer; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._lenTranfer, value, "LenTranfer");
            }
        }

        private void LineNumTransfer(double num)
        {
            if (num > 100)
            {
                double hh = Math.Round(num / 1000, 1);
                LenTranfer = string.Format("{0} {1}", hh, Helpers.ResourceHelper.FindKey("PoiBaseVM_Kilometer"));
            }
            else
            {
                LenTranfer = string.Format("{0} {1}", Math.Round(num, 1), Helpers.ResourceHelper.FindKey("PoiBaseVM_Meter"));
            }
        }
        /// <summary>
        /// 标注区域面积
        /// </summary>
        private double _area;
        public double Area
        {
            get { return this._area; }
            set
            {
                
                base.SetAndNotifyPropertyChanged<double>(ref this._area, value, "Area");
                NumTransfer(_area);
            }
        }

        private string _areaTranfer;
        public string AreaTranfer
        {
            get { return this._areaTranfer; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._areaTranfer, value, "AreaTranfer");
            }
        }

        private void NumTransfer(double num)
        {
            if(num>10000)
            {
                double hh = Math.Round(num / 1000000, 2);
                AreaTranfer = string.Format("{0} {1}", hh, Helpers.ResourceHelper.FindKey("PoiBaseVM_SquareKilometer"));
            }
            else
            {
                AreaTranfer = string.Format("{0} {1}", Math.Round(num,2), Helpers.ResourceHelper.FindKey("PoiBaseVM_SquareMeter"));
            }
        }

        private double _lat;
        public double Lat
        {
            get { return this._lat; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._lat, value, "Lat");
            }
        }

        private double _alt;
        /// <summary>
        /// 海拔高度
        /// </summary>
        public double Alt
        {
            get { return this._alt; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._alt, value, "Alt");
            }
        }


        public bool IsEdit { get; set; }
        public string PoiId { get; set; }


        protected virtual void OnDetailChanged(string detail)
        {

        }

        protected virtual void OnColorChanged(Color color)
        {

        }
        protected virtual void OnSLColorChanged(Color color)
        {

        }

        protected virtual void CreateNewRPoi()
        {
            PostMarkerNew postMarker = GetPostMarkerItem();
            var marksModelConvter = new MarksModelsConverter();
            var markerModel = marksModelConvter.MarkerConverting(postMarker);
            MarkerHelper.Instance.RenderMarker(markerModel);
        }

        protected virtual PostMarkerNew GetPostMarkerItem()
        {
            return null;
        }

        /// <summary>
        /// 标签类型
        /// </summary>
        public string PoiTitle
        {
            get { return this._poiTitle; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._poiTitle, value, "PoiTitle");
                OnPoiTitleChanged(_poiTitle);
                if (GviMap.TempRObjectPool.ContainsKey(_labelKey))
                {
                    var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
                    if (label != null)
                        label.Text = value;
                }
            }
        }

        public String WinTitle { get; set; }

        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        public virtual void SaveMarkerToServer()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                    this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Submitting"));
                });
                var api = string.Empty;
                if (IsEdit) { api = MarkInterface.UpdateMarkInf; }
                else { api = MarkInterface.AddMarkInf; }

                if (!IsEdit) ImgUrl = "/no_photo_1200.png";
                if (_isCapturedImg) ImgUrl = MarkerHelper.Instance.updateCaptureImg(_shootImgPath);

                //转换提交格式
                PostMarkerNew postMarker = GetPostMarkerItem();

                int markerId = MarkerHelper.Instance.AddOrUpdateMarkerNew(postMarker, api, out string title);

                if (!string.IsNullOrEmpty(title))
                    this.PoiTitle = title;
                if (markerId > 0)
                {
                    string newMarkId = Convert.ToString(markerId);
                    marker_id = newMarkId;

                    //提交标签
                    var tags = PostTags(newMarkId);

                    //var markerModel = MarkerHelper.Instance.PostMarkerToRenderMarker(postMarker);

                    var marksModelConvter = new MarksModelsConverter();
                    var markerModel = marksModelConvter.MarkerConverting(postMarker);

                    //markerModel.id = markerId;
                    //markerModel.title = title ?? this.PoiTitle;
                    //markerModel.tags = new List<TagItem>();
                    //foreach (var ii in tags)
                    //{
                    //    var tag = new TagItem()
                    //    {
                    //        id = Convert.ToInt32(ii),
                    //        name = this.TagItems[ii].ToString(),
                    //    };
                    //    markerModel.tags.Add(tag);
                    //}

                    //MarkerHelper.Instance.UpdateMarkerList(markerModel);
                    //RefreshPoiList?.Invoke(markerModel);
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
                IsSave = true;
                base.IsChecked = false;
            }
        }

        private string[] PostTags(string newMarkId)
        {
            var postTags = new PostTag();

            postTags.marker_id = int.Parse(newMarkId);
            var list = new List<string>();
            foreach (var item in this.SelectdTagItems)
                list.Add(item.Key.ToString());
            postTags.tags = list.ToArray();
            if (postTags.tags != null && postTags.tags.Length > 0)
            {
                string json = JsonUtil.SerializeToString(postTags);
                MarkerHelper.Instance.PostMarkerTags(json);
            }
            return postTags.tags;
        }

        public void Flyto()
        {
            if (GviMap.TempRObjectPool[_polyMakerKey] != null)
            {
                GviMap.Camera.LookAtEnvelope(((IRenderGeometry)GviMap.TempRObjectPool[_polyMakerKey]).Envelope);
                ((IRenderGeometry)GviMap.TempRObjectPool[_polyMakerKey]).Glow(2000);
            }
        }


        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;

            amapLocationService = new AmapLocationService();
            marker_id = "-1";
            _isCapturedImg = false;
            this.TagItems = new Dictionary<string, object>();
            this.SelectdTagItems = new Dictionary<string, object>();
            this._rcPictureExportBegin = new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            this._rcPictureExporting = new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            this._rcPictureExportEnd = new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
            //IsEdit = false;

            this.CloseCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                base.IsChecked = false;
                FinishProcess();
            });
            this.GetLocalCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                GetLocation();
            });
            this.FlytoCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                this.Flyto();
            });

            this.GetPictureCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ((Window)base.View).Hide();
                Thread.Sleep(600);
                screenCaputre.StartCaputre(30, lastSize);
                // this.toolStripCaptureScreen();//出图
            });

            this.CreateCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                IsSave = false;
                SaveMarkerToServer();
            });

            //打开图片
            this.DisplayImgCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(_imgUrl))
                    ImageUtil.InvokeImageProcess(_imgUrl);
            });

            //获取地址
            this.GetAddressCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                GetAddress();
            });

            screenCaputre.ScreenCaputred -= OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled -= OnScreenCaputreCancelled;
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            ((Window)base.View).Show();
            ((Window)base.View).Focus();
        }

        private void OnScreenCaputred(object sender, CaptureLib.ScreenCaputredEventArgs e)
        {
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
            var result = ImageHelper.SaveImage(_shootImgPath, e.Bmp);
            if (result)
            {
                ImgUrl = _shootImgPath;
                _isCapturedImg = true;
                Clipboard.SetImage(e.Bmp);
            }
            ((Window)base.View).Show();
            ((Window)base.View).Focus();
        }
        protected void GetAddress()
        {
            var label = GviMap.TempRObjectPool[_labelKey] as ILabel;

            var address = amapLocationService.GetAddressByCoor(label.Position.X, label.Position.Y);
            this.Address = address;
        }

        protected virtual void SetTitle()
        {

        }

        /// <summary>
        /// 创建临时交换的渲染对象
        /// </summary>
        protected virtual void CreateTempRobj()
        { }

        public override void OnChecked()
        {
            try
            {
                base.OnChecked();
                this._heightTypes = new ObservableCollection<HeightType>(){
                new HeightType(){HeighName=Helpers.ResourceHelper.FindKey("HeightAbsolute"), HeightStyle= gviHeightStyle.gviHeightAbsolute},
                new HeightType(){HeighName=Helpers.ResourceHelper.FindKey("HeightOnEverything"), HeightStyle= gviHeightStyle.gviHeightOnEverything}
                };

                //获取所有标签
                this.TagItems = MarkerHelper.Instance.GetTagsList();

                //设置窗口名称
                SetTitle();
                //创建临时交换的渲染对象
                CreateTempRobj();
                if (IsEdit)
                {
                    //var marker = MarkerHelper.Instance.MarkerDic[PoiId];

                    //this.PoiTitle = marker.Title;
                    //this.Detial = marker.Detail;
                    //this.marker_id = PoiId;
                    //this.Address = marker.Address;
                    //this.ImgUrl = marker.ImgPath;
                    ////if (double.TryParse(marker.Size, out double lenghtOrArea))
                    ////    this.Area = lenghtOrArea;

                    ////设置已选标签
                    //foreach (var tag in marker.Tags)
                    //{
                    //    string id = Convert.ToString(tag.id);
                    //    if (this.TagItems.ContainsKey(id))
                    //        this.SelectdTagItems.Add(id, this.TagItems[id]);
                    //}

                    //更新几何渲染对象
                    UpdateRobj();
                    //显示窗体
                    var view = (Window)base.View;
                    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    view.Show();
                    // CreateNewRPoi();
                }
                else
                {
                    //this.SelectedHeightType = this._heightTypes[0];
                    GetLocation();

                    this.ImgUrl = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png";
                    this.Detial = Helpers.ResourceHelper.FindKey("DefaultpointDetail");
                    this.Address = "";
                }
            }
            catch (Exception ex)
            {
                Mmc.Windows.Services.SystemLog.Log(ex);
            }
        }

        public virtual void UpdateRobj()
        { }

        public override void OnUnchecked()
        {
            try
            {
                base.OnUnchecked();

                if (IsEdit)
                    CreateNewRPoi();

                if (GviMap.TempRObjectPool.ContainsKey(_polyMakerKey))
                {
                    var label = GviMap.TempRObjectPool[_labelKey] as ILabel;
                    var rpolygonOld = GviMap.TempRObjectPool[_polyMakerKey] as IRenderable;
                    label.VisibleMask = gviViewportMask.gviViewNone;
                    rpolygonOld.VisibleMask = gviViewportMask.gviViewNone;
                }

            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            UnRegisterExportImgEvent();
            ((Window)base.View).Close();
        }

        private Dictionary<string, object> _selectdTagItems;
        [XmlIgnore]
        public Dictionary<string, object> SelectdTagItems
        {
            get { return this._selectdTagItems; }
            set
            {
                base.SetAndNotifyPropertyChanged<Dictionary<string, object>>(ref this._selectdTagItems, value, "SelectdTagItems");
            }
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        protected virtual void GetLocation()
        {
            var view = (Window)base.View;
            view.Hide();
        }

        protected virtual void OnImgUrlChanged(string url)
        {

        }

        protected virtual void OnPoiTitleChanged(string title)
        {

        }
        private void AxMapControl_RcPictureExportBegin(int NumberOfWidth, int NumberOfHeight)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExportBegin);
                return;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                    this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Compositing"));
                });
            }
        }

        //@param bool IsAborted : true代表取消出图成功；false表示正常出图成功。
        private void AxMapControl_RcPictureExportEnd(double Time, bool IsAborted)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExportEnd);
                return;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    FinishProcess();
                    if (!IsAborted)
                    {
                        ImgUrl = _shootImgPath;
                        _isCapturedImg = true;
                    }
                    UnRegisterExportImgEvent();
                });
            }
        }

        private void FinishProcess()
        {
            progressView.ViewModel.ProgressValue = string.Empty;
            ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        }

        private void AxMapControl_RcPictureExporting(int Index, float Percentage)
        {
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            if (Application.Current.Dispatcher.Thread.ManagedThreadId != shell.Dispatcher.Thread.ManagedThreadId)
            {
                shell.Dispatcher.BeginInvoke(this._rcPictureExporting);
                return;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Compositingpicture"), Percentage * 100);
                });
            }
        }

        private void RegisterExportImgEvent()
        {
            UnRegisterExportImgEvent();
            GviMap.AxMapControl.RcPictureExportBegin += new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting += new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd += new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

        private void toolStripCaptureScreen()
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo info = new DirectoryInfo(temp);
            _shootImgPath = info.ToString() + "\\MSpace\\shootImage\\" + GetTimeStamp() + ".png";

            RegisterExportImgEvent();
            bool b = GviMap.MapControl.ExportManager.ExportImage(_shootImgPath, 1920, 1080, true);
            if (!b)
            {
                SystemLog.Log(string.Format("CityMaker错误码为：{0}", GviMap.MapControl.GetLastError().ToString()));
                UnRegisterExportImgEvent();
            }
        }

        private void UnRegisterExportImgEvent()
        {
            GviMap.AxMapControl.RcPictureExportBegin -= new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting -= new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd -= new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }
        private ObservableCollection<HeightType> _heightTypes;

        [XmlIgnore]
        public ObservableCollection<HeightType> HeightTypes
        {
            get { return this._heightTypes; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<HeightType>>(ref this._heightTypes, value, "HeightTypes");
            }
        }

        private HeightType _selectedHeightType;

        [XmlIgnore]
        public HeightType SelectedHeightType
        {
            get { return this._selectedHeightType; }
            set
            {
                base.SetAndNotifyPropertyChanged<HeightType>(ref this._selectedHeightType, value, "SelectedHeightType");
                OnSelectedHeightChange(_selectedHeightType);
            }
        }
        protected virtual void OnSelectedHeightChange(HeightType heightType)
        {

        }
    }
}