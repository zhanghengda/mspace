using Microsoft.Win32;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections.ObjectModel;
using Mmc.Framework.Services;
using Gvitech.CityMaker.FdeGeometry;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Models.Inspection;
using Gvitech.Windows.Utils;
using Mmc.Windows.Services;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.PoiManagerModule;
using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.RoutePlanning;
using System.Device.Location;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class PhotosToTraceVModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public Action<InspectModel> UpdateDataList;

        private readonly string TAG = "PhotoTrace";

        private List<IPoint> poiList;
        private readonly ExportProgressView progressView = new ExportProgressView();
        private string _photosFolder;
        public string PhotosFolder
        {
            get { return _photosFolder; }
            set { _photosFolder = value; NotifyPropertyChanged("PhotosFolder"); }
        }


        private string _traceFile;
        public string TraceFile
        {
            get { return _traceFile; }
            set { _traceFile = value; NotifyPropertyChanged("TraceFile"); }
        }

        private List<InspectModel> photoPosList;

        private ObservableCollection<InspectModel> _photoPosModelList;
        public ObservableCollection<InspectModel> PhotoPosModelList
        {
            get { return _photoPosModelList??(_photoPosModelList=new ObservableCollection<InspectModel> ()); }
            set { _photoPosModelList = value; NotifyPropertyChanged("PhotoPosModelList"); }
        }

        private bool _thumbnailChecked;
        public bool ThumbnailChecked
        {
            get { return _thumbnailChecked; }
            set { _thumbnailChecked = value; NotifyPropertyChanged("ThumbnailChecked"); }
        }

        private bool _traceLineChecked;
        public bool TraceLineChecked
        {
            get { return _traceLineChecked; }
            set { _traceLineChecked = value; NotifyPropertyChanged("TraceLineChecked"); }
        }
        private bool _isUnitCbxEnable;
        public bool IsUnitCbxEnable
        {
            get { return _isUnitCbxEnable; }
            set { _isUnitCbxEnable = value; NotifyPropertyChanged("IsUnitCbxEnable"); }
        }

        private bool _isOkBtnEnable;
        public bool IsOkBtnEnable
        {
            get { return _isOkBtnEnable; }
            set { _isOkBtnEnable = value; NotifyPropertyChanged("IsOkBtnEnable"); }
        }
        private InspectModel _selectedPhotoPoint;
        public InspectModel SelectedPhotoPoint
        {
            get { return _selectedPhotoPoint; }
            set { _selectedPhotoPoint = value; NotifyPropertyChanged("SelectedPhotoPoint"); }
        }

        private ObservableCollection<InspectRegion> _inspectRegions;
        public ObservableCollection<InspectRegion> InspectRegions
        {
            get { return _inspectRegions; }
            set { _inspectRegions = value; NotifyPropertyChanged("InspectRegions"); }
        }

        private InspectRegion _selectedRegion;
        public InspectRegion SelectedRegion
        {
            get { return _selectedRegion; }
            set { _selectedRegion = value; NotifyPropertyChanged("SelectedRegion"); }
        }
        
        private ObservableCollection<TextItem> _inspectTimeRange;

        public ObservableCollection<TextItem> InspectTimeRange
        {
            get { return _inspectTimeRange ?? (_inspectTimeRange = new ObservableCollection<TextItem>()); }
            set { _inspectTimeRange = value; NotifyPropertyChanged("InspectTimeRange"); }
        }

        private TextItem _selectedUnit;

        public TextItem SelectedUnit
        {
            get { return _selectedUnit; }
            set { _selectedUnit = value; NotifyPropertyChanged("SelectedUnit"); }
        }

        [XmlIgnore]
        public ICommand CancelCmd { get; set; }
        [XmlIgnore]
        public ICommand SaveCmd { get; set; }
        [XmlIgnore]
        public ICommand ChoosePhotosFolderCmd { get; set; }
        [XmlIgnore]
        public ICommand ChooseTraceFileCmd { get; set; }
        [XmlIgnore]
        public ICommand FlyToCmd { get; set; }
        [XmlIgnore]
        public ICommand SelectInspectCommand { get; set; }

        public override FrameworkElement CreatedView()
        {
            return new PhotosToTraceView() { Owner = Application.Current.MainWindow };
        }


        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            poiList = new List<IPoint>();
            photoPosList = new List<InspectModel>();
            IsOkBtnEnable = false;

            this.CancelCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });

            this.SaveCmd = new RelayCommand(() =>
            {
                CreatePhotoTrace();
            });

            this.ChoosePhotosFolderCmd = new RelayCommand(() =>
            {
                OpenPhotoPath();
            });
            this.ChooseTraceFileCmd = new RelayCommand(() =>
            {
                SaveTraceFile();
            });
            this.FlyToCmd = new RelayCommand(() =>
             {
                 FlyToPhotoPoint();
             });
            this.SelectInspectCommand = new RelayCommand<InspectRegion>(OnSelectInspectCommand);
            Messenger.Messengers.Register("PhotoTraceRefresh", () =>
            {
                LoadRegionsData();
            });

        }

        private void LoadRegionsData()
        {
            this.SelectedRegion = null;
            this.SelectedUnit = null;
            List<InspectRegion> lists = new List<InspectRegion>();
            lists.AddRange( InspectionService.Instance.GetAllRegion());
            this.InspectRegions = new ObservableCollection<InspectRegion>(lists);

        }

        private void OnSelectInspectCommand(InspectRegion parameter)
        {
            if (parameter == null) return;

            IsUnitCbxEnable = true;
            List<TextItem> lists = new List<TextItem>();
            var unit = InspectionService.Instance.GetUnitsByRegionId(parameter.Id);

            if (unit == null) return;

            lists.AddRange(unit.Select(z => new TextItem() { Key = z.Id.ToString(), Value = z.Name.ToString() }).ToList());
            InspectTimeRange = new ObservableCollection<TextItem>(lists);
            SelectedUnit = lists[0];
        }

        

        private void FlyToPhotoPoint()
        {
            var selected = SelectedPhotoPoint;
            var crs = GviMap.SpatialCrs;
            var poi = GviMap.GeoFactory.CreatePoint(selected.X, selected.Y, selected.Z, crs);
            RegInsDataRenderManager.Instance.FlyToPoint(poi);
            var rpoi = GviMap.TracePoiManager.GetRPOI(TAG, selected.Id.ToString());
            ((IRenderGeometry)rpoi)?.Glow(2000);
        }

        /// <summary>
        /// create and save photo trace
        /// </summary>
        private void CreatePhotoTrace()
        {
            try
            {
                Task.Run(() =>
                {
                    if (string.IsNullOrEmpty(this.TraceFile))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("OutputFile"));
                        return;
                    }

                    LoadingDsProcess(string.Format(Helpers.ResourceHelper.FindKey("UploadDataToUnit"),SelectedUnit.Value));
                    HideView();

                    string routeName = Path.GetFileNameWithoutExtension(this.TraceFile);
                    string fileMd5 = CreateRouteKmlAndMd5();
                    InspectModel inspectModel = null;
                    if (TraceLineChecked)
                    {
                        if (poiList.Count > 1)
                        {
                            string style = string.Empty;

                            var polyLine = GviMap.GeoFactory.CreatePolyline(poiList, GviMap.SpatialCrs);
                            RegInsDataRenderManager.Instance.OpenTraceLine(polyLine, style, fileMd5);


                            var routeCfg = new RouteItem()
                            {
                                Path = this.TraceFile,
                                Name = routeName,
                                Geom = polyLine.AsWKT(),
                                Style = style,
                                Md5 = fileMd5,
                                InspectUnitId = Convert.ToInt32(SelectedUnit.Key)
                            };

                            InspectionService.Instance.RouteItemSet.Add(routeCfg);

                            inspectModel = RegInsModelConvert.RouteConvert(routeCfg);
                            //UpdateDataList(inspectModel);
                            ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVESUCCESSED);
                        }
                    }

                    List<InspectModel> modelList = new List<InspectModel>();

                    for (int i = 0; i < photoPosList.Count; i++)
                    {
                        //if (InspectionService.Instance.PictureItemSet.Exist(p => p.Md5 == photoPosList[i].Md5))
                        //    continue;
                        var photoCfg = new PictureItem()
                        {
                            Name = photoPosList[i].Name,
                            //Md5 = photoPosList[i].Md5,
                            X = photoPosList[i].X,
                            Y = photoPosList[i].Y,
                            Z = photoPosList[i].Z,
                            Path = photoPosList[i].Path,
                            InspectUnitId = Convert.ToInt32(SelectedUnit.Key),
                            RouteName = routeName
                        };

                        if (this._thumbnailChecked)
                            photoCfg.Thumbnail = CreateThumbnail(photoPosList[i].Path);

                        InspectionService.Instance.PictureItemSet.Add(photoCfg);
                        inspectModel = RegInsModelConvert.PictureConvert(photoCfg);
                        modelList.Add(inspectModel);
                    }

                    RegInsDataRenderManager.Instance.CreatePhotoPoints(modelList);

                    //var inspectModel = RegInsModelConvert.RouteConvert(routeCfg);
                    //UpdateDataList(inspectModel);

                    Messenger.Messengers.Notify("AddRegion", true);
                    base.IsChecked = false;

                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVESUCCESSED);
                });
            }
            catch
            {
                FinishLoadProcess();
                ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVEFAILED);
            }
        }
        /// <summary>
        /// create thumbnail
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private string CreateThumbnail(string filepath)
        {
            
            string subPath = Path.GetDirectoryName(filepath) + "\\thumb";
            if (false == Directory.Exists(subPath))
                Directory.CreateDirectory(subPath);

            string imgName = Path.GetFileNameWithoutExtension(filepath);

            string grayName = imgName + "_gray.png";
            string grayThumbPath = subPath + "\\" + grayName;
            string redName = imgName + "_red.png";
            string redThumbPath = subPath + "\\" + redName;

            Image thumb = ImageUtil.CreateThumbnail(filepath);
            Image grayThumb = ImageUtil.CutEllipse(thumb, Color.Gray);
            Image redThumb = ImageUtil.CutEllipse(thumb,Color.Red);

            grayThumb.Save(grayThumbPath);
            redThumb.Save(redThumbPath);
            return grayThumbPath;
        }

        private string CreateRouteKmlAndMd5()
        {
            string hashCode = string.Empty;

            kml loadkml = new kml();

            List<GeoCoordinate> coordinates = new List<GeoCoordinate>();

            for (int i = 0; i < photoPosList.Count; i++)
            {
                GeoCoordinate coord = new GeoCoordinate();
                coord.Longitude = photoPosList[i].X;
                coord.Latitude = photoPosList[i].Y;
                coord.Altitude = photoPosList[i].Z;
                coordinates.Add(coord);
            }
            loadkml.Document.Add(new Placemark("", "", "colorID", coordinates));
            loadkml.SaveToFile(this._traceFile);
            hashCode = FileUtil.GetSHA1FromFile(this._traceFile);
            
            return hashCode;
        }


        private void SaveTraceFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("TraceFile");
            dialog.Filter = FileFilterStrings.KML;
            dialog.DefaultExt = "kml";
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == true)
            {
                TraceFile = dialog.FileName;
            }
        }

        private void OpenPhotoPath()
        {
            PhotoPosModelList.Clear();
            photoPosList.Clear();

            if (this.SelectedRegion == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectRegion"));
                return;
            }

            if (this.SelectedUnit == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectTimeUnit"));
                return;
            }
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PhotosFolder = dialog.SelectedPath;
                string[] files;

                try
                {
                    Task.Run(() =>
                    {
                        LoadingDsProcess(Helpers.ResourceHelper.FindKey("ReadImageData"));
                        HideView();

                        files = Directory.GetFiles(PhotosFolder, "*.jpg", SearchOption.TopDirectoryOnly);

                        var photoModel = new InspectModel();

                        IPoint point;
                        var crs = GviMap.SpatialCrs;

                        if (files.Length < 0)
                        {
                            FinishLoadProcess();
                            ShowDataOpenStatus(CommonContract.OperateDataStatus.LOADFAILED);
                            return;
                        }

                        for (int i = 0; i < files.Length; i++)
                        {
                            //string fileMd5 = FileUtil.GetSHA1FromFile(files[i]);
                            //if (InspectionService.Instance.PictureItemSet.Exist(p => p.Md5 == fileMd5))
                            //    continue;

                            Image img = Image.FromFile(files[i]);

                            photoModel = GetImageInfo(img);
                            photoModel.Id = i;
                            photoModel.Path = files[i];
                            photoModel.Name = Path.GetFileNameWithoutExtension(files[i]);//.Substring(files[i].LastIndexOf('\\') + 1);
                                //photoModel.Md5 = fileMd5;
                            point = GviMap.GeoFactory.CreatePoint(photoModel.X, photoModel.Y, photoModel.Z, crs);
                            if (photoModel.X != 0 && photoModel.Y != 0 && photoModel.Z != 0)
                            {
                                poiList.Add(point);

                                photoPosList.Add(photoModel);

                            }
                            LoadingDsProcess(string.Format(Helpers.ResourceHelper.FindKey("ReadImageStatus"),i,files.Length));
                        }

                        PhotoPosModelList = new ObservableCollection<InspectModel>(photoPosList);

                        FinishLoadProcess();
                        ShowView();

                        if (photoPosList.Count <= 0)
                        {
                            this.IsOkBtnEnable = false;
                            ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        }
                        else
                        {
                            this.IsOkBtnEnable = true;
                            ShowDataOpenStatus(CommonContract.OperateDataStatus.LOADSUCCESSED);
                        }
                    });
                }
                catch
                {
                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.LOADFAILED);
                }
            }
        }

        /// <summary>
        /// read  gps data of image
        /// </summary>
        /// <param name="objImage"></param>
        /// <returns></returns>
        private InspectModel GetImageInfo(Image objImage)
        {
            var model = new InspectModel();
            try
            {
                //取得所有的屬性(以PropertyId做排序)   
                var propertyItems = objImage.PropertyItems.OrderBy(x => x.Id);
                char chrGPSLatitudeRef = 'N';
                char chrGPSLongitudeRef = 'E';
                foreach (var objItem in propertyItems)
                {
                    if ((objItem.Id >= 0x0000 && objItem.Id <= 0x001e) || objItem.Id == 0x0132)
                    {
                        switch (objItem.Id)
                        {
                            case 0x0000:
                                var query = from tmpb in objItem.Value select tmpb.ToString();
                                string sreVersion = string.Join(".", query.ToArray());
                                //richTextBox1.Text = sreVersion;
                                break;
                            case 0x0001:
                                chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0);
                                break;
                            case 0x0002:
                                if (objItem.Value.Length == 24)
                                {
                                    //degrees(將byte[0]~byte[3]轉成uint, 除以byte[4]~byte[7]轉成的uint)   
                                    double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                    //minutes(將byte[8]~byte[11]轉成uint, 除以byte[12]~byte[15]轉成的uint)   
                                    double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                    //seconds(將byte[16]~byte[19]轉成uint, 除以byte[20]~byte[23]轉成的uint)   
                                    double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                    //計算緯度的數值, 如果是南緯, 要乘上(-1)   
                                    double dblGPSLatitude = (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
                                    model.Y = Math.Round(dblGPSLatitude, 6);
                                    string strLatitude = string.Format("{0:#} deg {1:#}' {2:#.00}\" {3}", d, m, s, chrGPSLatitudeRef);
                                }
                                break;
                            case 0x0003:
                                //透過BitConverter, 將Value轉成Char('E' / 'W')   
                                //此值在後續的Longitude計算上會用到   
                                chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0);
                                break;
                            case 0x0004:
                                if (objItem.Value.Length == 24)
                                {
                                    //degrees(將byte[0]~byte[3]轉成uint, 除以byte[4]~byte[7]轉成的uint)   
                                    double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                    //minutes(將byte[8]~byte[11]轉成uint, 除以byte[12]~byte[15]轉成的uint)   
                                    double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                    //seconds(將byte[16]~byte[19]轉成uint, 除以byte[20]~byte[23]轉成的uint)   
                                    double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                    //計算緯度的數值, 如果是西經, 要乘上(-1)   
                                    double dblGPSLongitude = (((s / 60 + m) / 60) + d) * (chrGPSLongitudeRef.Equals('E') ? 1 : -1);
                                    model.X = Math.Round(dblGPSLongitude, 6);
                                    //Console.WriteLine("{0:#} deg {1:#}' {2:#.00}\" {3}", d, m, s, chrGPSLongitudeRef);
                                }
                                break;
                            case 0x0005:
                                string strAltitude = BitConverter.ToBoolean(objItem.Value, 0) ? "0" : "1";
                                break;
                            case 0x0006:
                                if (objItem.Value.Length == 8)
                                {
                                    //將byte[0]~byte[3]轉成uint, 除以byte[4]~byte[7]轉成的uint   
                                    double dblAltitude = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                    model.Z = Math.Round(dblAltitude, 1);
                                }
                                break;
                            case 0x0132:
                                try
                                {
                                    string dateStr = Encoding.ASCII.GetString(objItem.Value);
                                    string temp = dateStr.Split(' ')[0].Replace(":", "");
                                    DateTime date = DateTime.ParseExact(temp, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                    model.Time = date;
                                }
                                catch { }
                                break;
                        }
                    }
                }

            }
            catch { }
            return model;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            var view = (PhotosToTraceView)base.View;
            view.DataContext = this;

            ThumbnailChecked = true;
            TraceLineChecked = true;

            LoadRegionsData();
            
            view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            view.Show();
        }



        public override void OnUnchecked()
        {
            base.OnUnchecked();
            HideView();
            ClearData();
        }

        private void ClearData()
        {
            this.PhotosFolder = null;
            this.TraceFile = null;
            this.PhotoPosModelList = null;
            this.ThumbnailChecked = false;
            this.TraceLineChecked = false;
            this.IsUnitCbxEnable = false;
            this.SelectedUnit = null;
            this.SelectedUnit = null;
            this.IsOkBtnEnable = false;
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        private void ShowView()
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {
                var view = (PhotosToTraceView)base.View;
                view.Show();
            });
        }

        private void HideView()
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {
                var view = (PhotosToTraceView)base.View;
                view.Hide();
            });

        }

        private void LoadingDsProcess(string msg)
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {
                this.progressView.ViewModel.ProgressValue = msg;
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
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

        private void ShowDataOpenStatus(CommonContract.OperateDataStatus status)
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {
                if(status== CommonContract.OperateDataStatus.LOADSUCCESSED|| status == CommonContract.OperateDataStatus.SAVESUCCESSED)
                {

                }
                else
                {
                    Messages.ShowMessage("数据重复或者无包含Pos的照片数据！");//Helpers.ResourceHelper.FindKey( status.ToString()
                }                
            });
        }

    }
}
