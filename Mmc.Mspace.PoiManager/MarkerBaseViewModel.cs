using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.RenderControl;
using Gvitech.Windows.Utils;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule
{
    public class MarkerBaseViewModel: CheckedToolItemModel
    {
        [XmlIgnore]
        public Action<MarkerModel> UpdatePoi;

        protected readonly ExportProgressView progressView= new ExportProgressView();
        protected AmapLocationService _amapLocationService;
        //protected MarkerHelper _markerHelper;


        private _IRenderControlEvents_RcPictureExportBeginEventHandler _rcPictureExportBegin;
        private _IRenderControlEvents_RcPictureExportEndEventHandler _rcPictureExportEnd;
        private _IRenderControlEvents_RcPictureExportingEventHandler _rcPictureExporting;


        //private MarkerModel _markerModel;
        //public MarkerModel MarkerModel { get { return _markerModel; } set { base.SetAndNotifyPropertyChanged<MarkerModel>(ref this._markerModel, value, "PoiInfo"); } }

        //private PoiPositon _poiPositon;
        //public PoiPositon PoiPositon { get { return _poiPositon; } set { base.SetAndNotifyPropertyChanged<PoiPositon>(ref this._poiPositon, value, "PoiInfo");  } }

        /*出图绝对路径*/
        private string ShootImgPath = @"C:\Users\Admin\AppData\Local\Temp\MSpace\shootImage\temp.bmp";
        public bool IsEdit { get; set; }
        protected bool IsCapturingImg;
        protected string _img_path;
        protected int _type;
        protected string MarkerTypeKey;
        protected string LabelKey;
        protected int _marker_id;

        private bool _isSave = true;
        public bool IsSave
        {
            get { return _isSave; }
            set { _isSave = value; base.NotifyPropertyChanged("IsSave"); }
        }

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

        private string _markerTitle;
        public string MarkerTitle
        {
            get { return this._markerTitle; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._markerTitle, value, "PoiTitle");
                if (GviMap.PoiManager.TempRPoi != null)
                {
                    var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                    poi.Name = this._markerTitle;
                    GviMap.PoiManager.TempRPoi.SetFdeGeometry(poi);
                }
                //this._markerModel.title = this._poiTitle;
            }
        }



        private string _address;
        public string Address
        {
            get { return this._address; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._address, value, "Address"); }
        }

        private Dictionary<string, object> _tagsDic;
        [XmlIgnore]
        public Dictionary<string, object> TagsDic
        {
            get { return this._tagsDic; }
            set
            {
                base.SetAndNotifyPropertyChanged<Dictionary<string, object>>(ref this._tagsDic, value, "TagItems");
            }
        }

        private string _detail;
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
        protected virtual void OnDetailChanged(string detail) { }


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
        protected virtual void OnSelectedHeightChange(HeightType heightType) { }

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

        private double _lng;
        [XmlIgnore]
        public double Lng
        {
            get { return this._lng; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._lng, value, "Lng");
            }
        }

        private double _lat;
        [XmlIgnore]
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
        [XmlIgnore]
        public double Alt
        {
            get { return this._alt; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._alt, value, "Alt");
            }
        }

        private string _imgUrl;
        public string ImgUrl
        {
            get { return this._imgUrl; }
            set
            {
                base.SetAndNotifyPropertyChanged<string>(ref this._imgUrl, value, "ImgUrl");
                OnImgUrlChanged(_imgUrl);
            }
        }

        protected virtual void OnImgUrlChanged(string url) { }



        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _amapLocationService = new AmapLocationService();
            this.TagsDic = new Dictionary<string, object>();
            this.SelectdTagItems = new Dictionary<string, object>();
            this.IsCapturingImg = false;
            //this._markerHelper = new MarkerHelper();

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
                this.toolStripCaptureScreen();//出图
            });

            this.CreateCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                IsSave = false;
                CreateMaker();
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
        }

        public override void OnChecked()
        {
            base.OnChecked();
            this._heightTypes = new ObservableCollection<HeightType>(){
                new HeightType(){HeighName=Helpers.ResourceHelper.FindKey("HeightAbsolute"), HeightStyle= gviHeightStyle.gviHeightAbsolute},
                new HeightType(){HeighName=Helpers.ResourceHelper.FindKey("HeightOnEverything"), HeightStyle= gviHeightStyle.gviHeightOnEverything}
                };
            if (IsEdit)
            {


            }
            else
            {
                this.SelectedHeightType = this._heightTypes[0];
                GetLocation();
                this.ImgUrl = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png";
                this.Detial = Helpers.ResourceHelper.FindKey("DefaultpointDetail");
                this.Address = "";
            }
        }

        protected virtual void SetTitle() { }

        protected virtual void CreateTempRobj() { }

        public virtual void UpdateRobj() { }

        private void SummitProcess()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Submitting"));
            });
        }

        private void FinishProcess()
        {
            progressView.ViewModel.ProgressValue = string.Empty;
            ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        }

        protected virtual void GetLocation() { }

        protected virtual void Flyto() { }

        protected virtual void GetAddress() { }

        private void toolStripCaptureScreen()
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo info = new DirectoryInfo(temp);
            ShootImgPath = info.ToString() + "\\MSpace\\shootImage\\" + GetTimeStamp() + ".jpg";

            RegisterExportImgEvent();
            bool b = GviMap.MapControl.ExportManager.ExportImage(ShootImgPath, 1920, 1080, true);
            //bool b = GviMap.AxMapControl.ExportManager.ExportImage(_shootImgPath, 256, 256, false);
            if (!b)
            {
                SystemLog.Log(string.Format("CityMaker错误码为：{0}", GviMap.MapControl.GetLastError().ToString()));
                UnRegisterExportImgEvent();
            }
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

        //@param bool IsAborted : true代表取消出图成功；false表示正常出图成功。
        private void AxMapControl_RcPictureExportEnd(double Time, bool IsAborted)
        {
            //GviMap.MapControl.ResumeRendering();
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
                        _img_path = ShootImgPath;
                        IsCapturingImg = true;
                    }
                    UnRegisterExportImgEvent();
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

        private void UnRegisterExportImgEvent()
        {
            //GviMap.MapControl.ResumeRendering();
            GviMap.AxMapControl.RcPictureExportBegin -= new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting -= new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd -= new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

        protected virtual PostMarkerNew GetMarkerInfoItem()
        {
            return null;
        }

        public virtual void CreateMaker()
        {
            try
            {
                SummitProcess();

                var api = string.Empty;
                if (IsEdit)
                    api = "syncmarker";
                else
                    api = "setmarker";


                if (IsCapturingImg)
                {
                    string imgurl = MarkerHelper.Instance.updateCaptureImg(ShootImgPath);
                    if (string.IsNullOrEmpty(imgurl))
                        _img_path = imgurl;
                    else
                        _img_path = "/no_photo_1200.png";
                }
                else
                {
                    if (!IsEdit)
                        _img_path = "/no_photo_1200.png";
                }

                //转换提交格式
                var poiItem = GetMarkerInfoItem();

                //int markerId = MarkerHelper.Instance.AddOrUpdateMarkerNew(poiItem, api, out string title);

                if (true)
                {
                    string newMarkId = Convert.ToString(1);


                    this.IsChecked = false;
                }
                else
                {
                    //unsuccess
                    //Messages.ShowMessage(postReturn.message.ToString());
                }
                IsSave = true;
            }
            catch (Exception ex)
            {
                if (ex is LoginExcetiop)
                    throw ex;
                SystemLog.Log(ex);
                IsSave = true;
            }
            finally
            {
                FinishProcess();
            }


            void PostTags(string newMarkId)
            {
                //var tagUrl = string.Format("{0}/addtag", _poiHost);
                //var postTags = new PostTag();

                //postTags.marker_id = int.Parse(newMarkId);
                //var list = new List<string>();
                //foreach (var item in this.SelectdTagItems)
                //    list.Add(item.Key.ToString());
                //postTags.tags = list.ToArray();
                //if (postTags.tags != null && postTags.tags.Length > 0)
                //    _httpService.PostJsonData(tagUrl, JsonUtil.SerializeToString(postTags));
            }

        }
    }
}
