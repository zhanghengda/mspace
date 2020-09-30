using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
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
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule
{
    public class PoiDetailViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public Action<MarkerModel> RefreshPoiList;

        public bool IsInputAdd = false;

        public string PoiId { get; set; }

        public int status;

        private readonly CaptureLib.ScreenCaputre screenCaputre = new CaptureLib.ScreenCaputre();
        private Size? lastSize;
        private readonly ExportProgressView progressView = new ExportProgressView();
        private bool _isCapturedImg;
        private ILabel _label;
        private MarkerModel _poiInfo;
        private string _poiTitle;
        private ObservableCollection<PoiType> _poiTypes;

        private _IRenderControlEvents_RcPictureExportBeginEventHandler _rcPictureExportBegin;
        private _IRenderControlEvents_RcPictureExportEndEventHandler _rcPictureExportEnd;
        private _IRenderControlEvents_RcPictureExportingEventHandler _rcPictureExporting;
        private PoiType _selectPoiTypes;
        private HeightType _selectHeightType;
        private AmapLocationService _amapLocationService;

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
        public ICommand GetPictureCommand { get; set; }

        [XmlIgnore]
        public ICommand GetAddressCommand { get; set; }



        [XmlIgnore]
        public MarkerModel PoiInfo
        {
            get { return this._poiInfo; }
            set { base.NotifyPropertyChanged("PoiInfo"); }
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
                if (GviMap.PoiManager.TempRPoi != null)
                {
                    var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                    poi.Name = this._poiTitle;
                    GviMap.PoiManager.TempRPoi.SetFdeGeometry(poi);
                }
                this._poiInfo.title = this._poiTitle;
            }
        }

        private string _address;
        public string Address
        {
            get { return this._address; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._address, value, "Address"); }
        }

        [XmlIgnore]
        public ObservableCollection<PoiType> PoiTypes
        {
            get { return this._poiTypes; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<PoiType>>(ref this._poiTypes, value, "PoiTypes");
            }
        }



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

        [XmlIgnore]
        public PoiType SelectedPoiType
        {
            get { return this._selectPoiTypes; }
            set
            {
                base.SetAndNotifyPropertyChanged<PoiType>(ref this._selectPoiTypes, value, "SelectedPoiType");
                if (GviMap.PoiManager.TempRPoi != null && this.SelectedPoiType != null)
                {
                    var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                    poi.ImageName = this.SelectedPoiType.cat_url;
                    GviMap.PoiManager.TempRPoi.SetFdeGeometry(poi);
                }
                if (this.SelectedPoiType != null)
                {
                    this._poiInfo.cat_id = this.SelectedPoiType.cat_id;
                    this._poiInfo.cat_Name = this.SelectedPoiType.cat_name;
                }
            }
        }

        [XmlIgnore]
        public HeightType SelectedHeightType
        {
            get { return this._selectHeightType; }
            set
            {
                base.SetAndNotifyPropertyChanged<HeightType>(ref this._selectHeightType, value, "SelectedHeightType");

            }
        }

        public String WinTitle { get; set; }


        private double _lng;
        public double Lng
        {
            get { return this._lng; }
            set
            {
                base.SetAndNotifyPropertyChanged<double>(ref this._lng, value, "Lng");
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
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public override FrameworkElement CreatedView()
        {
            return new PoiDetailView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        public override void Initialize()
        {

            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _isCapturedImg = false;
            _amapLocationService = new AmapLocationService();

            this.TagItems = new Dictionary<string, object>();
            this.SelectdTagItems = new Dictionary<string, object>();

            _poiTypes = new ObservableCollection<PoiType>();


            //创建poi的初始化状态
            //_poi = GviMap.GeoFactory.CreateGeometry(gviGeometryType.gviGeometryPOI, gviVertexAttribute.gviVertexAttributeZ) as IPOI;
            _label = GviMap.ObjectManager.CreateLabel(GviMap.ProjectTree.RootID);
            _label.VisibleMask = gviViewportMask.gviViewNone;
            var textSym = _label.TextSymbol;
            textSym.PivotAlignment = gviPivotAlignment.gviPivotAlignTopRight;
            _label.TextSymbol = textSym;

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
                //飞入
                GviMap.Camera.FlyTime = 1;
                var rPoi = GviMap.PoiManager.TempRPoi;
                if (rPoi != null)
                {
                    //GviMap.Camera.FlyToObject(rpoi.Guid, gviActionCode.gviActionFlyTo);
                    //rpoi.Glow(2000);


                    var poi = rPoi.GetFdeGeometry() as IPoint;

                    GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);

                    GviMap.Camera.LookAt2(poi, 200, Angle);
                    ((IRenderGeometry)rPoi).Glow(1500);
                }

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
                saveMarkerToServer();
            });

            //打开图片
            this.DisplayImgCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(_poiInfo.img))
                    ImageUtil.InvokeImageProcess(_poiInfo.img);
            });

            this.GetAddressCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                this.Address = _amapLocationService.GetAddressByCoor(poi.X, poi.Y);
            });

            //this.SelectedTags = new List<TagItem>() { new TagItem("news"), new TagItem("priority") };
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
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);
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
            if(result)
            {
                PoiInfo.img = _shootImgPath;
                _isCapturedImg = true;
                Clipboard.SetImage(bmp);
            }
           ((Window)base.View).Show();
            ((Window)base.View).Focus();
        }
        public override void OnChecked()
        {
            base.OnChecked();

            //获取所有标签
            this.TagItems = MarkerHelper.Instance.GetTagsList();
            foreach (var item in MarkerHelper.Instance.PoiTypeDic.Values)
                this._poiTypes.Add(item);

            FillMarkerDataInView();
        }


        private void FillMarkerDataInView()
        {
            if (status == (int)OperationStatus.EDIT) //编辑
            {
                //_poiInfo = MarkerHelper.Instance.MarkerDic[PoiId];

                
                this.PoiTitle = PoiInfo.title;
                this.Address = PoiInfo.address;

                if (_poiInfo.type == 1 && _poiInfo.geom.Contains("POINT"))
                {
                    var position = _poiInfo.geom.Split('(')[1].Split(')')[0].Split(' ');
                    this.Lng = Convert.ToDouble(position[0]);
                    this.Lat = Convert.ToDouble(position[1]);
                    this.Alt = Convert.ToDouble(position[2]);
                }
                this.WinTitle = Helpers.ResourceHelper.FindKey("Editpoint");
                this.PoiInfo.img = _poiInfo.img;
                if (GviMap.PoiManager.TempRPoi == null)
                    GviMap.PoiManager.CreateTempRPoi();

                var rPOi = GviMap.PoiManager.GetRPOI(_poiInfo.cat_Name, _poiInfo.id.ToString());
                rPOi.VisibleMask = gviViewportMask.gviViewNone;
                var poi = rPOi.GetFdeGeometry() as IPOI;
                GviMap.PoiManager.TempRPoi.SetFdeGeometry(poi);
                GviMap.PoiManager.TempRPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                this.SelectedPoiType = MarkerHelper.Instance.PoiTypeDic[_poiInfo.cat_id];

                //删除原来poi
                GviMap.PoiManager.DeletePoi(_poiInfo.id.ToString());

                //获取标注所属标签                
                foreach (var tag in _poiInfo.tags)
                {
                    string id = Convert.ToString(tag.id);
                    if (this.TagItems.ContainsKey(id))
                        this.SelectdTagItems.Add(id, this.TagItems[id]);
                }
                base.NotifyPropertyChanged("PoiInfo");
                IsSave = true;
                //显示窗体
                var view = (Window)base.View;
                view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                view.Show();
            }
            else if(status == (int)OperationStatus.ADD)
            {
                _poiInfo = new MarkerModel()
                {
                    title = Helpers.ResourceHelper.FindKey("Defaultpoint"),
                    detail = Helpers.ResourceHelper.FindKey("DefaultpointDetail")
                };

                var textSym = _label.TextSymbol;
                textSym.PivotAlignment = gviPivotAlignment.gviPivotAlignBottomLeft;
                _label.TextSymbol = textSym;

                this.PoiTitle = PoiInfo.title;
                this.Address = PoiInfo.address;
                this.WinTitle = Helpers.ResourceHelper.FindKey("Addpoint");
                this.PoiInfo.img = "pack://siteoforigin:,,,/Resources/BuildInfo/BuildImage.png";
                this.SelectedPoiType = this._poiTypes[0];
                if (IsInputAdd)
                {
                    CreatePoint();
                    var view = (Window)base.View;
                    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    view.Show();
                }
                else
                {
                    GetLocation();
                }

            }
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();

            this.Alt = 0;
            this.Lat = 0;
            this.Lng = 0;
            this.PoiInfo = null;
            this.Address = null;
            this.PoiInfo = null;
            this.PoiTitle = null;
            //this.PoiTypes = null;
            //this.TagItems = null;
            this.SelectdTagItems.Clear();
            //this.SelectedPoiType



            if (GviMap.PoiManager.TempRPoi != null)
                GviMap.PoiManager.TempRPoi.VisibleMask = gviViewportMask.gviViewNone;

            if (status == (int)OperationStatus.EDIT)
                CreateNewRpoi();

            GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            UnRegisterExportImgEvent();
            ((Window)base.View).Hide();

        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            try
            {
                if (EventSender == gviMouseSelectMode.gviMouseSelectClick)
                {
                    var pt = IntersectPoint;
                    if (pt == null)
                        return;

                    this.Lng = pt.X;
                    this.Lat = pt.Y;
                    this.Alt = pt.Z;

                    CreatePoint();

                    //if (GviMap.PoiManager.TempRPoi == null)
                    //    GviMap.PoiManager.CreateTempRPoi();
                    //var _poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
                    ////创建POI
                    //_poi.X = pt.X;
                    //_poi.Y = pt.Y;
                    //_poi.Z = pt.Z;


                    //_poi.ShowName = true;
                    //_poi.Name = _poiInfo.title;
                    //_poi.Size = 32;



                    //var localurl = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, this.SelectedPoiType.cat_url);
                    //_poi.ImageName = localurl;
                    //_poi.SpatialCRS = GviMap.SpatialCrs;

                    ////加入缓存管理
                    //if (_poiInfo.id > 0 && GviMap.PoiManager.ContainsKey(_poiTypes[_poiInfo.cat_id].cat_name, _poiInfo.id.ToString()))
                    //{
                    //    GviMap.PoiManager.UpdatePoi(_poiInfo.id.ToString(), _poiTypes[_poiInfo.cat_id].cat_name, _poi);
                    //}
                    //else
                    //{
                    //    GviMap.PoiManager.TempRPoi.SetFdeGeometry(_poi);
                    //    GviMap.PoiManager.TempRPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
                    //}

                    _label.VisibleMask = gviViewportMask.gviViewNone;
                    //取消事件注册
                    GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                    GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                    GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                    GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

                    var view = (Window)base.View;
                    view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    view.Show();
                }
                else if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                {
                    var pt = IntersectPoint;
                    _label.Text = Helpers.ResourceHelper.FindKey("Markedlocation");
                    _label.Position = pt;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
            finally
            {
            }
        }


        private void CreatePoint()
        {
            if (GviMap.PoiManager.TempRPoi == null)
                GviMap.PoiManager.CreateTempRPoi();
            var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
            //创建POI
            poi.X = this.Lng;
            poi.Y = this.Lat;
            poi.Z = this.Alt;

            poi.ShowName = true;
            poi.Name = _poiInfo.title;
            poi.Size = 32;

            var localurl = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, this.SelectedPoiType.cat_url);
            poi.ImageName = localurl;
            poi.SpatialCRS = GviMap.SpatialCrs;

            //加入缓存管理
            if (_poiInfo.id > 0 && GviMap.PoiManager.ContainsKey(_poiTypes[_poiInfo.cat_id].cat_name, _poiInfo.id.ToString()))
            {
                GviMap.PoiManager.UpdatePoi(_poiInfo.id.ToString(), _poiTypes[_poiInfo.cat_id].cat_name, poi);
            }
            else
            {
                GviMap.PoiManager.TempRPoi.SetFdeGeometry(poi);
                GviMap.PoiManager.TempRPoi.VisibleMask = gviViewportMask.gviViewAllNormalView;
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
                        PoiInfo.img = _shootImgPath;
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

        private void CreateNewRpoi()
        {

            PostMarkerNew postMarker = GetPostMarkerModel();
            var marksModelConvter = new MarksModelsConverter();
            var markerModel = marksModelConvter.MarkerConverting(postMarker);
            MarkerHelper.Instance.RenderMarker(markerModel);

        }

        private void saveMarkerToServer()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                    this.progressView.ViewModel.ProgressValue = string.Format(Helpers.ResourceHelper.FindKey("Submitting"));
                });
                string api = string.Empty;
                if (status == (int)OperationStatus.ADD) api = MarkInterface.AddMarkInf;
                else api = MarkInterface.UpdateMarkInf;

                if (status == (int)OperationStatus.ADD) PoiInfo.img = "/no_photo_1200.png";
                if (_isCapturedImg)  PoiInfo.img = MarkerHelper.Instance.updateCaptureImg(_shootImgPath);

                //转换提交格式
                PostMarkerNew postMarker = GetPostMarkerModel();

                int markerId = MarkerHelper.Instance.AddOrUpdateMarkerNew(postMarker, api, out string title);
                if (!string.IsNullOrEmpty(title))
                    this.PoiTitle = title;

                if (markerId > 0)
                {
                    string newMarkId = Convert.ToString(markerId);

                    //提交标签
                    var tags = PostTags(newMarkId);

                    _poiInfo.id = markerId;

                    var marksModelConvter = new MarksModelsConverter();
                    //var markerModel = marksModelConvter.MarkerConverting(postMarker);

                    //markerModel.id = markerId;
                    //markerModel.title = title ?? this.PoiTitle;
                    //markerModel.positionKey = this.Lng.ToString() + this.Lat.ToString();
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
            var view = this.View as PoiDetailView;
            var list = new List<string>();
            foreach (var item in view.MC.SelectedItems)
                list.Add(item.Key.ToString());
            postTags.tags = list.ToArray();
            if (postTags.tags != null && postTags.tags.Length > 0)
            {
                string json = JsonUtil.SerializeToString(postTags);
                MarkerHelper.Instance.PostMarkerTags(json);
            }
            return postTags.tags;
        }

        protected PostMarkerNew GetPostMarkerModel()
        {
            var postMarker = new PostMarkerNew();

            if (int.TryParse(PoiInfo.id.ToString(), out int makerId))
                postMarker.marker_id = makerId;
            postMarker.type = 1;
            postMarker.detail = PoiInfo.detail;

            if (PoiInfo.img.ToLower().Contains("http"))
                postMarker.img = PoiInfo.img.Substring(PoiInfo.img.LastIndexOf("resource") + 1 + "resouce".Length);
            else
                postMarker.img = PoiInfo.img;

            postMarker.address = this.Address;
            postMarker.cat_id = PoiInfo.cat_id;
            var poi = GviMap.PoiManager.TempRPoi.GetFdeGeometry() as IPOI;
            postMarker.geom = poi.AsWKT().ToUpper();

            postMarker.title = PoiTitle;
            return postMarker;
        }

        private void GetLocation()
        {
            GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
            GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;
            GviMap.InteractMode = gviInteractMode.gviInteractSelect;
            GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
            GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick | gviMouseSelectMode.gviMouseSelectMove;
            _label.VisibleMask = gviViewportMask.gviViewAllNormalView;
            var view = (Window)base.View;
            //view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.Hide();
        }

        private void RegisterExportImgEvent()
        {
            // GviMap.MapControl.PauseRendering(false);//先暂停渲染
            UnRegisterExportImgEvent();
            GviMap.AxMapControl.RcPictureExportBegin += new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting += new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd += new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

        private void toolStripCaptureScreen()
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo info = new DirectoryInfo(temp);
            _shootImgPath = info.ToString() + "\\MSpace\\shootImage\\" + GetTimeStamp() + ".jpg";

            RegisterExportImgEvent();
            bool b = GviMap.MapControl.ExportManager.ExportImage(_shootImgPath, 1920, 1080, true);
            //bool b = GviMap.AxMapControl.ExportManager.ExportImage(_shootImgPath, 256, 256, false);
            if (!b)
            {
                SystemLog.Log(string.Format("CityMaker错误码为：{0}", GviMap.MapControl.GetLastError().ToString()));
                UnRegisterExportImgEvent();
            }
        }

        private void UnRegisterExportImgEvent()
        {
            //GviMap.MapControl.ResumeRendering();
            GviMap.AxMapControl.RcPictureExportBegin -= new _IRenderControlEvents_RcPictureExportBeginEventHandler(this.AxMapControl_RcPictureExportBegin);
            GviMap.AxMapControl.RcPictureExporting -= new _IRenderControlEvents_RcPictureExportingEventHandler(this.AxMapControl_RcPictureExporting);
            GviMap.AxMapControl.RcPictureExportEnd -= new _IRenderControlEvents_RcPictureExportEndEventHandler(this.AxMapControl_RcPictureExportEnd);
        }

    }
}