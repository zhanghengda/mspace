using ApplicationConfig;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Microsoft.Win32;
using Mmc.DataSourceAccess;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.PoiManagerModule;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.ViewModels;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Services.MapHostService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.RegularInspectionModule.Views;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.RegularInspectionModule
{
    public class RegInsDataRenderManager : Singleton<RegInsDataRenderManager>
    {
        //public Action UpdateDataList;
        private ScreenHintVModel leftHintVModel;
        private ScreenHintVModel rightHintVModel;
        private readonly ExportProgressView progressView;
        private Dictionary<string, IRenderLayer> _renderLayers;
        private Dictionary<string, IRenderLayer> _renderDomLayer;
        private HttpService _httpService;
        private string _poiHost;
        private ImageDisplayVModel imageDisplayVModel;
        private readonly string TAG = "PhotoTrace";
        private List<InspectModel> currentLineModelList;
        public string _currentFileName = null;
        private Dictionary<string, gviViewportMask> _rLayersRenderableStatus;
        public RegInsDataRenderManager()
        {
            progressView = new ExportProgressView();

            _httpService = new HttpService();
            _httpService.Token = HttpServiceUtil.Token;
            var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            _poiHost = json.poiUrl;

            if (_renderLayers == null)
                _renderLayers = new Dictionary<string, IRenderLayer>();

            if (_renderDomLayer == null)
                _renderDomLayer = new Dictionary<string, IRenderLayer>();

            imageDisplayVModel = new ImageDisplayVModel();

            _rLayersRenderableStatus = new Dictionary<string, gviViewportMask>();

        }

        private bool WarnFileExist(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("FileNotExists"));
                return false;
            }
        }

        public void ImportInspectData(InspectModel inModel)
        {
            //bool success = false;
            OpenFileDialog dialog = new OpenFileDialog();
            string filePath = string.Empty;
            string[] filePathArr;
            dialog.Multiselect = false;
            switch (inModel.DataType)
            {
                case Common.CommonContract.InspectDataType.Dom:

                    dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionDom") + Helpers.ResourceHelper.FindKey("File");
                    dialog.Filter = FileFilterStrings.TIF;
                    if (dialog.ShowDialog() == true)
                    {
                        filePath = dialog.FileName;
                        updateDataToSave(inModel, filePath);
                    }
                    break;
                case Common.CommonContract.InspectDataType.Picture:

                    UploadVModel uploadPicture = new UploadVModel(inModel)
                    {
                        TitleName = "图片加载",
                    };

                    //dialog.Multiselect = true;
                    //dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionPicture") + Helpers.ResourceHelper.FindKey("File");
                    //dialog.Filter = FileFilterStrings.IMAGE;
                    //if (dialog.ShowDialog() == true)
                    //{
                    //    filePathArr = dialog.FileNames;
                    //    if (filePathArr.Length > 0)
                    //        foreach (var item in filePathArr)
                    //        {
                    //            updateDataToSave(inModel, item);
                    //            //success = true;
                    //        }
                    //}
                    break;
                case Common.CommonContract.InspectDataType.Video:
                    dialog.Multiselect = true;
                    dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionVideo") + Helpers.ResourceHelper.FindKey("File");
                    dialog.Filter = FileFilterStrings.Video;
                    if (dialog.ShowDialog() == true)
                    {
                        filePathArr = dialog.FileNames;
                        if (filePathArr.Length > 0)
                            foreach (var item in filePathArr)
                            {
                                updateDataToSave(inModel, item);
                                //success = true;
                            }
                    }
                    break;
                case Common.CommonContract.InspectDataType.Route:

                    UploadVModel uploadRoute = new UploadVModel(inModel)
                    {
                        TitleName = "航线加载"
                    };

                    //dialog.Multiselect = true;
                    //dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionRoute") + Helpers.ResourceHelper.FindKey("File");
                    //dialog.Filter = FileFilterStrings.KML;
                    //if (dialog.ShowDialog() == true)
                    //{
                    //    filePathArr = dialog.FileNames;
                    //    if (filePathArr.Length > 0)
                    //        foreach (var item in filePathArr)
                    //        {
                    //            updateDataToSave(inModel, item);
                    //            //success = true;
                    //        }
                    //}
                    break;
                case Common.CommonContract.InspectDataType.Report:

                    dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionReport") + Helpers.ResourceHelper.FindKey("File");
                    dialog.Filter = FileFilterStrings.WORD;
                    if (dialog.ShowDialog() == true)
                    {
                        filePath = dialog.FileName;
                        updateDataToSave(inModel, filePath);
                    }
                    break;
            }


            //return success;
        }

        private void updateDataToSave(InspectModel inModel, string inFilePath)
        {
            //bool success = false;
            int currentUnitId = inModel.InspectUnitId;
            string currentUnitName = inModel.Name;
            string fileExtention = Path.GetExtension(inFilePath).ToUpper();

            Task.Run(() =>
            {
                try
                {
                    LoadingDsProcess(string.Format(Helpers.ResourceHelper.FindKey("UploadDataToUnit"), currentUnitName));
                    InspectModel inspectModel = new InspectModel();

                    //string fileMd5 = FileUtil.GetSHA1FromFile(DataFileName);

                    string fileName = Path.GetFileNameWithoutExtension(inFilePath);

                    if (inModel.DataType == CommonContract.InspectDataType.Dom)
                    {
                        //if (!fileExtention.Contains("TIF"))
                        //{
                        //    FinishLoadProcess();
                        //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                        //    return;
                        //}

                        string thumb = fileName + "_thumb.png";
                        string thumbPath = Path.GetDirectoryName(inFilePath) + "\\" + thumb;

                        var reclassify = new DomReclassify();
                        reclassify.PythonProcess.ImputFile = inFilePath;
                        reclassify.PythonProcess.OutputFile = thumbPath;
                        reclassify.Analys();

                        Console.WriteLine(DateTime.Now);
                        //if (InspectionService.Instance.DomItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}
                        Console.WriteLine(DateTime.Now);
                        var domCfg = new DomItem()
                        {
                            Name = fileName,
                            Path = inFilePath,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5,
                            Thumbnail = thumbPath

                        };
                        Console.WriteLine(DateTime.Now);
                        InspectionService.Instance.AddDom(domCfg);

                        inspectModel = RegInsModelConvert.DomConvert(domCfg);
                        Console.WriteLine(DateTime.Now);
                        //UpdateDataList(inspectModel);
                        //success = true;
                        ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
                        {
                            Messenger.Messengers.Notify("HistoryDomRefresh");
                        });
                    }
                    else if (inModel.DataType == CommonContract.InspectDataType.Picture)
                    {
                        //if (!fileExtention.Contains("JPG") && !fileExtention.Contains("PNG") && !fileExtention.Contains("BMP"))
                        //{
                        //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                        //    FinishLoadProcess();
                        //    return;
                        //}

                        //if (InspectionService.Instance.PictureItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}


                        var picCfg = new PictureItem()
                        {
                            Path = inFilePath,
                            Name = fileName,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5
                        };

                        InspectionService.Instance.PictureItemSet.Add(picCfg);

                        inspectModel = RegInsModelConvert.PictureConvert(picCfg);
                        //UpdateDataList(inspectModel);
                        //success = true;
                    }
                    else if (inModel.DataType == CommonContract.InspectDataType.Video)
                    {
                        //if (!fileExtention.Contains("MP4") && !fileExtention.Contains("AVI"))
                        //{
                        //    FinishLoadProcess();
                        //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                        //    return;
                        //}

                        //if (InspectionService.Instance.VideoItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        var videoCfg = new VideoItem()
                        {
                            Path = inFilePath,
                            Name = fileName,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5
                        };

                        InspectionService.Instance.VideoItemSet.Add(videoCfg);

                        inspectModel = RegInsModelConvert.VideoConvert(videoCfg);
                        //UpdateDataList(inspectModel);
                        //success = true;
                    }
                    else if (inModel.DataType == CommonContract.InspectDataType.Route)
                    {
                        //if (!fileExtention.Contains("KML"))
                        //{
                        //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                        //    FinishLoadProcess();
                        //    return;
                        //}

                        //if (InspectionService.Instance.RouteItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        string style = string.Empty;
                        var polyLine = GviMap.GeoFactory.CreateFromXml(inFilePath, GviMap.SpatialCrs);
                        //RegInsDataManager.Instance.AddTraceLine(polyLine, style,fileMd5);

                        var routeCfg = new RouteItem()
                        {
                            Name = fileName,
                            InspectUnitId = currentUnitId,
                            Geom = polyLine.AsWKT(),
                            Style = style,
                            //Md5 = fileMd5
                        };

                        InspectionService.Instance.RouteItemSet.Add(routeCfg);

                        inspectModel = RegInsModelConvert.RouteConvert(routeCfg);
                        //success = true;
                        //UpdateDataList(inspectModel);
                    }
                    else if (inModel.DataType == CommonContract.InspectDataType.Report)
                    {
                        //if (!fileExtention.Contains("DOC"))
                        //{
                        //    FinishLoadProcess();
                        //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                        //    return;
                        //}

                        //if (InspectionService.Instance.ReportItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        string api = RegularInspectInterface.UploadReportInf;
                        string resStr = HttpServiceHelper.Instance.PostDocFile(api, inFilePath);

                        //string resStr = RegInsDataRenderManager.Instance.UploadReportFile(inFilePath);
                        
                        
                        if (!string.IsNullOrEmpty(resStr))
                        {
                            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                            var reportCfg = new ReportItem()
                            {
                                Path = resDyn.file_path,
                                ReportNum = resDyn.id,
                                Name = fileName,
                                InspectUnitId = currentUnitId,
                                //Md5 = fileMd5
                            };

                            InspectionService.Instance.ReportItemSet.Add(reportCfg);

                            inspectModel = RegInsModelConvert.ReportConvert(reportCfg);
                            //UpdateDataList(inspectModel);
                            //success = true;
                            ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVESUCCESSED);
                        }
                        else
                        {
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("UPLOADFAILED"));
                            //SystemLog.Log(string.Format("文件上传失败。错误代码：{0}", resDyn.status), LogMessageType.ERROR);
                        }
                    }

                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVESUCCESSED);
                    ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
                    {
                        Messenger.Messengers.Notify("HistoryDomRefresh");
                        Messenger.Messengers.Notify("LeftListRefresh");
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.LOADFAILED);

                }

                //return success;
            });
        }


        private void ShowDataOpenStatus(CommonContract.OperateDataStatus status)
        {
            ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey(status.ToString()));
            });
        }

        public void OpenInspectData(InspectModel inModel)
        {
            switch (inModel.DataType)
            {
                case Common.CommonContract.InspectDataType.Dom:
                    OpenDomData(inModel);
                    break;
                case Common.CommonContract.InspectDataType.Picture:
                    if (WarnFileExist(inModel.Path))
                        imageDisplayVModel.ShowImageView(inModel.Path);
                    imageDisplayVModel.BtnVisiable = Visibility.Hidden;
                    break;
                case Common.CommonContract.InspectDataType.Video:
                    VideoPlayViewVMdel videoPlayViewVMdel = new VideoPlayViewVMdel();
                    if (WarnFileExist(inModel.Path))
                        videoPlayViewVMdel.ShowVideoView(inModel.Path);
                    break;
                case Common.CommonContract.InspectDataType.Route:
                    var polyLine = GviMap.GeoFactory.CreatePolyline(inModel.Geom, GviMap.SpatialCrs);
                    OpenTraceLine(polyLine, inModel.Style, inModel.Id.ToString());

                    var photoList = InspectionService.Instance.PictureItemSet.Find(p => p.RouteName == inModel.Name);

                    if (photoList?.Count > 0)
                    {
                        var inspectList = new List<InspectModel>();

                        foreach (var item in photoList)
                        {
                            inspectList.Add(RegInsModelConvert.PictureConvert(item));
                        }

                        CreatePhotoPoints(inspectList);
                    }

                    break;
                case Common.CommonContract.InspectDataType.Report:

                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = FileFilterStrings.WORD;
                    string fileName = string.Empty;
                    dialog.AddExtension = true;
                    if (dialog.ShowDialog() == true)
                    {
                        fileName = dialog.FileName;
                        var httpDowLoadManager = new HttpDowLoadManager();
                        var json = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                        httpDowLoadManager._poiHost = json.poiUrl;
                        httpDowLoadManager.Token = HttpServiceUtil.Token;
                        Task.Run(() =>
                        {
                            _currentFileName = fileName;
                            //httpDowLoadManager.DownloadInspectReport(fileName, inModel.ReportNum, DownloadResult);
                            string downloadReport = string.Format("{0}?id={1}", RegularInspectInterface.DownloadReportInf, inModel.ReportNum);
                            HttpServiceHelper.Instance.DownloadFile(downloadReport, fileName, DownloadResult);
                        });
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DownLoadingReport"));
                    }

                    break;
            }
        }

        public void DownloadResult(bool result)
        {
            if (result)
            {
                if (File.Exists(_currentFileName))
                {
                    System.Diagnostics.Process.Start(_currentFileName);
                }
                //Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SAVESUCCESSED"));
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("SAVEFAILED"));
            }
        }

        public bool DeleteInspectData(InspectModel inModel)
        {
            bool result = false;
            if (inModel.DataType == InspectDataType.Region)
            {
                var units = InspectionService.Instance.FindUnits(p => p.InspectRegionId == inModel.Id);
                if(units!=null)
                    foreach(var unit in units)
                    {
                        ClearUnitRender(unit.Id);
                    }

                result = InspectionService.Instance.DeleteRegion(RegInsModelConvert.InspectRegionConvert(inModel));
                if (result)
                {
                    Messenger.Messengers.Notify("HistoryDomRefresh");
                    Messenger.Messengers.Notify("PhotoTraceRefresh");
                }
            }
            else if (inModel.DataType == InspectDataType.Unit)
            {
                ClearUnitRender(inModel.Id);

                result = InspectionService.Instance.DeleteUnit(RegInsModelConvert.InspectUnitConvert(inModel));
                if (result)
                {
                    Messenger.Messengers.Notify("HistoryDomRefresh");
                    Messenger.Messengers.Notify("PhotoTraceRefresh");
                }
            }
            else if (inModel.DataType == InspectDataType.Dom)
            {
                if (inModel.Id != 0)
                {
                    result = InspectionService.Instance.DeleteDomItem(RegInsModelConvert.DomConvert(inModel));
                }
                if (inModel.InspectList.Count > 0)
                {
                    var item = inModel.InspectList.FirstOrDefault();
                    result = InspectionService.Instance.DeleteDomItem(RegInsModelConvert.DomConvert(item));
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoDelete") + Helpers.ResourceHelper.FindKey("InspectionDom"));

                if (_renderLayers.Count > 0 && _renderLayers.ContainsKey(inModel.Id.ToString()))
                {
                    RemoveRenderLayer(inModel.Id.ToString());
                }
                if (result)
                    Messenger.Messengers.Notify("HistoryDomRefresh");
            }
            else if (inModel.DataType == InspectDataType.Picture || inModel.DataType == InspectDataType.Folder)
            {
                if (inModel.Id != 0)
                {
                    List<PictureItem> list = new List<PictureItem>();
                    list.Add(RegInsModelConvert.PictureConvert(inModel));
                    result = InspectionService.Instance.DeletePicLayer(list);
                }
                if (inModel.InspectList.Count > 0)
                {
                    result = InspectionService.Instance.DeletePicLayer(inModel.InspectList.Select(t => RegInsModelConvert.PictureConvert(t)).ToList());
                    //if(inModel)
                }
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoDelete") + Helpers.ResourceHelper.FindKey("InspectionPicture"));
            }
            else if (inModel.DataType == InspectDataType.Video)
            {
                if (inModel.Id != 0)
                {
                    List<VideoItem> list = new List<VideoItem>();
                    list.Add(RegInsModelConvert.VideoConvert(inModel));
                    result = InspectionService.Instance.DeleteVideo(list);
                }
                if (inModel.InspectList.Count > 0)
                    result = InspectionService.Instance.DeleteVideo(inModel.InspectList.Select(t => RegInsModelConvert.VideoConvert(t)).ToList());
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoDelete") + Helpers.ResourceHelper.FindKey("InspectionVideo"));
            }

            else if (inModel.DataType == InspectDataType.Report)
            {
                if (inModel.Id != 0)
                {
                    List<ReportItem> list = new List<ReportItem>();
                    list.Add(RegInsModelConvert.ReportConvert(inModel));
                    result = InspectionService.Instance.DeleteReportLayer(list);
                }
                if (inModel.InspectList.Count > 0)
                    result = InspectionService.Instance.DeleteReportLayer(inModel.InspectList.Select(t => RegInsModelConvert.ReportConvert(t)).ToList());
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoDelete") + Helpers.ResourceHelper.FindKey("InspectionReport"));
            }
            else if (inModel.DataType == InspectDataType.Route)
            {
                if (inModel.Id != 0)
                {
                    List<RouteItem> list = new List<RouteItem>();
                    list.Add(RegInsModelConvert.RouteConvert(inModel));
                    result = InspectionService.Instance.DeleteRouteLayer(list);
                }
                if (inModel.InspectList.Count > 0)
                    result = InspectionService.Instance.DeleteRouteLayer(inModel.InspectList.Select(t => RegInsModelConvert.RouteConvert(t)).ToList());
                else
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NoDelete") + Helpers.ResourceHelper.FindKey("InspectionRoute"));

                if (GviMap.TraceLinePolyManager.ContainsKey(inModel.Id.ToString()))
                {
                    ClearRouteRender();
                    ClearPoiRender();
                }
            }
            return result;
        }
        public bool ChangeInspectDataName(InspectModel inModel,string _name)
        {
            bool result = false;
            if (inModel.DataType == InspectDataType.Region)
            {
                var units = InspectionService.Instance.FindUnits(p => p.InspectRegionId == inModel.Id);
                if (units != null)
                    foreach (var unit in units)
                    {
                        ClearUnitRender(unit.Id);
                    }

                result = InspectionService.Instance.ChangeRegionName(RegInsModelConvert.InspectRegionConvert(inModel), _name);
                if (result)
                {
                    Messenger.Messengers.Notify("HistoryDomRefresh");
                    Messenger.Messengers.Notify("PhotoTraceRefresh");
                }
            }else
            {
                Messages.ShowMessage("该对象不支持修改名称");
            }
            return result;
        }
        private void ClearUnitRender(int itemId)
        {
            var dom = InspectionService.Instance.FindOneDom(itemId);
            if (dom!=null&&_renderLayers.ContainsKey(dom.Id.ToString()))
                RemoveRenderLayer(dom.Id.ToString());

            var route = InspectionService.Instance.FindOneRoute(itemId);
            if (route!=null&& GviMap.TraceLinePolyManager.ContainsKey(route.Id.ToString()))
            {
                ClearRouteRender();
                ClearPoiRender();
            }
        }

        private List<KeyValuePair<string, IRenderLayer>> RenderNumberInOneView(gviViewportMask viewportMask)
        {
            List<KeyValuePair<string, IRenderLayer>> temp = new List<KeyValuePair<string, IRenderLayer>>();

            return temp;
        }

        private void ShowHintView(gviViewportMask viewportMask, string name)
        {
            if (viewportMask == gviViewportMask.gviView0)
            {
                if (leftHintVModel == null)
                    leftHintVModel = new ScreenHintVModel();

                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                double width = mapView.Width / 2;
                double top = mapView.Top + 48;
                double left = mapView.Left;

                leftHintVModel.ShowViewOfName(width, top, left, name);
            }
            else if (viewportMask == gviViewportMask.gviView1)
            {
                if (rightHintVModel == null)
                    rightHintVModel = new ScreenHintVModel();

                var mapView = ServiceManager.GetService<IMaphostService>(null).MapWindow;
                double width = mapView.Width / 2;
                double top = mapView.Top + 48;
                double left = mapView.Width / 2 + mapView.Left;

                rightHintVModel.ShowViewOfName(width, top, left, name);
            }
        }

        private void HideHintView()
        {
            if (_renderLayers.Count < 0) return;
            if (_renderLayers.Count == 1)
            {
                var temp = _renderLayers.First().Value.Renderable.VisibleMask;
                if (temp == gviViewportMask.gviView0)
                    rightHintVModel?.HideView();
                if (temp == gviViewportMask.gviView1)
                    leftHintVModel?.HideView();
            }
            if (_renderLayers.Count == 2)
            {
                var first = _renderLayers.First().Value.Renderable.VisibleMask;
                var last = _renderLayers.Last().Value.Renderable.VisibleMask;
                if (first == last)
                {
                    if (first == gviViewportMask.gviView0)
                        rightHintVModel?.HideView();
                    if (first == gviViewportMask.gviView1)
                        leftHintVModel?.HideView();
                }
                //leftHintVModel?.HideView();
                //rightHintVModel?.HideView();
            }
        }


        public void OpenDomData(InspectModel inModel, bool isLocal = true, gviViewportMask viewportMask = gviViewportMask.gviViewAllNormalView)
        {
            if (!WarnFileExist(inModel.Path))
                return;

            try
            {
                Task.Run(() =>
                {
                    if (IsDomOpen(inModel))
                    {
                        var render = _renderLayers[inModel.Id.ToString()];

                        render.Renderable.VisibleMask = viewportMask;

                        var layer = _renderLayers?.ToList().FindAll(p => p.Value.Renderable.VisibleMask == viewportMask);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ShowHintView(viewportMask, inModel.Time.ToShortDateString());
                        });
                        if (layer.Count > 0)
                        {
                            foreach (var item in layer)
                            {
                                if (item.Key != inModel.Id.ToString())
                                    RemoveRenderLayer(item.Key);
                            }
                        }
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            HideHintView();
                        });
                        FlyToDom(render);
                        return;
                    }

                    var fileAddress = inModel.Path;
                    OperateDataStatus status = OperateDataStatus.LOADFAILED;
                    ImageLayerConfig layerConfig = new ImageLayerConfig()
                    {
                        ConnInfoString = fileAddress,
                        AlphaEnabled = "false",
                        IsLocal = isLocal
                    };
                    if (isLocal)
                    {
                        layerConfig.AliasName = Path.GetFileNameWithoutExtension(fileAddress);
                        layerConfig.ConType = "File";
                    }
                    else
                    {
                        layerConfig.ConType = "WMTS";
                    }


                    LoadingDsProcess(Helpers.ResourceHelper.FindKey("ReadDomData"));

                    var renderLayer = DataBaseService.Instance.OpenImageLayer(layerConfig, out status);
                    if (renderLayer != null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            renderLayer.Renderable.VisibleMask = viewportMask;
                            if (!_renderLayers.ContainsKey(inModel.Id.ToString()))
                                _renderLayers.Add(inModel.Id.ToString(), renderLayer);

                            ShowHintView(viewportMask, inModel.Time.ToShortDateString());
                            HideHintView();
                            FlyToDom(renderLayer);
                        });
                    }
                    FinishLoadProcess();

                });
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
                FinishLoadProcess();
                //ShowAddDataStatus(status);
            }
        }

        private IRenderLayer SetRenderLayerViewTaget(IRenderLayer renderLayer, int viewNum)
        {
            if (viewNum == 0)
                renderLayer.Renderable.VisibleMask = gviViewportMask.gviView0;
            else if (viewNum == 1)
                renderLayer.Renderable.VisibleMask = gviViewportMask.gviView1;
            else
                renderLayer.Renderable.VisibleMask = gviViewportMask.gviViewAllNormalView;
            return renderLayer;
        }

        private bool IsDomOpen(InspectModel inspectModel)
        {
            if (inspectModel == null || string.IsNullOrEmpty(inspectModel.Id.ToString()))
                return false;
            return _renderLayers.ContainsKey(inspectModel.Id.ToString());
        }

        private void FlyToDom(IRenderLayer renderLayer)
        {
            if (renderLayer == null || renderLayer.Renderable?.Guid == null)
                return;
            GviMap.Camera.FlyToObject(renderLayer.Renderable.Guid, gviActionCode.gviActionFlyTo);
        }

        public void FlyToPoint(IPoint poi)
        {
            if (poi == null)
                return;

            GviMap.Camera.GetCamera2(out IPoint Position, out IEulerAngle Angle);

            GviMap.Camera.LookAt2(poi, 1000, Angle);
        }


        public void AddPictures(List<IPoint> poiList, string _photosFolder)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < poiList.Count; i++)
                {
                    var photoCfg = new PictureItem()
                    {
                        X = poiList[i].X,
                        Y = poiList[i].Y,
                        Z = poiList[i].Z,
                        Path = _photosFolder,
                        InspectUnitId = 1
                    };
                    InspectionService.Instance.PictureItemSet.Add(photoCfg);

                    var inspectModel = RegInsModelConvert.PictureConvert(photoCfg);

                    //UpdateDataList(inspectModel);
                }
            });
        }


        public void OpenTraceLine(IPolyline polyLine, string style, string traceId)
        {
            GviMap.TraceLinePolyManager.Clear();
            GviMap.TracePoiManager.Clear();

            RenderGeometryStyle styleRender = null;
            ICurveSymbol CurveSym = null;
            if (string.IsNullOrEmpty(style))
                CurveSym = GviMap.TraceLinePolyManager.CurveSym;
            else
            {
                if (style.Contains("HeightStyle"))
                {
                    //styleRender = JsonUtil.DeserializeFromString<RenderGeometryStyle>(style);
                    //CurveSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(styleRender.GeoSymbolXml) as ICurveSymbol;
                }
                else
                {
                    CurveSym = GviMap.ObjectManager.CreateGeometrySymbolFromXML(style) as ICurveSymbol;
                }

            }
            var rLine = GviMap.ObjectManager.CreateRenderPolyline(polyLine, CurveSym);
            if (styleRender != null)
                rLine.HeightStyle = styleRender.HeightStyle;
            var label = GviMap.ObjectManager.CreateLabel(polyLine.Midpoint);
            //label.Text = title;
            GviMap.TraceLinePolyManager.AddPoi(traceId, 2, label, rLine);

            var route = GviMap.TraceLinePolyManager.GetItemByKey(traceId).Item3;// TempRObjectPool[fileMd5];
            if (route != null)
            {
                //GviMap.Camera.LookAtEnvelope(((IRenderGeometry)route).Envelope);
                ((IRenderGeometry)route).Glow(2000);
            }
        }

        public void CreatePhotoPoints(List<InspectModel> modelList)
        {
            if (modelList == null || modelList.Count <= 0) return;
            currentLineModelList = modelList;
            GviMap.TracePoiManager.Clear();

            for (var i = 0; i < currentLineModelList.Count; i++)
            {
                var rPoi = CreateRenderPoi(currentLineModelList[i]);
                GviMap.TracePoiManager.AddPoi(currentLineModelList[i].Id.ToString(), TAG, rPoi);
                currentLineModelList[i].Guid = rPoi.Guid.ToString();
            }

            GviMap.TracePoiManager.FlyTo(currentLineModelList[0].Id.ToString());
        }

        private IRenderPOI CreateRenderPoi(InspectModel model)
        {
            string thumb = string.Empty;
            if (File.Exists(model.Thumbnail))
            {
                string path = Path.GetDirectoryName(model.Thumbnail);
                string name = Path.GetFileNameWithoutExtension(model.Thumbnail);
                int index = name.LastIndexOf("_");

                var imgname = name.Substring(0, index);
                string ext = Path.GetExtension(model.Thumbnail);
                if (model.IsTroublePoi)
                    thumb = path + "\\" + imgname + "_red" + ext;
                else thumb = path + "\\" + imgname + "_gray" + ext;
            }
            else
                thumb = "项目数据\\shp\\IMG_POI\\默认.png";
            var poi = GviMap.TracePoiManager.CreatePoi(model.X, model.Y, model.Z, thumb, model.Name, 26);
            var rPoi = GviMap.TracePoiManager.CreateRPoi(poi);
            rPoi.MaxVisibleDistance = WebConfig.TracePoiMaxDistance;

            rPoi.SetClientData(TAG, model.Id.ToString());

            GviMap.MapControl.SetRenderParam(gviRenderControlParameters.gviRenderParamOutlineColor, Color.Red);
            rPoi.ShowOutline = true;

            return rPoi;
        }

        public void UpdatePhotoRenderPoint(InspectModel picModel)
        {
            var rPoi = GviMap.TracePoiManager.GetRPOI(TAG, picModel.Id.ToString());
            if (rPoi != null)
                GviMap.TracePoiManager.DeletePoi(picModel.Id.ToString());
            var newRPoi = CreateRenderPoi(picModel);
            GviMap.TracePoiManager.AddPoi(picModel.Id.ToString(), TAG, newRPoi);
            var markModel = currentLineModelList.Find(p => p.Id == picModel.Id);
            markModel.Guid = newRPoi.Guid.ToString();

            var updatePicItem = RegInsModelConvert.PictureConvert(markModel);

            InspectionService.Instance.UpdatePicItem(updatePicItem);
        }


        public string UploadReportFile(string fileName)
        {
            //string url = string.Format("{0}/api/inspection-file/upload", _poiHost);
            //string resStr = this._httpService.PostReportFileData(url, fileName);

            string api = RegularInspectInterface.UploadReportInf;
            string resStr = HttpServiceHelper.Instance.PostDocFile(api, fileName);

            return resStr;
        }


        public void MapControlEventManagement(bool OnEvent)
        {
            if (OnEvent)
            {
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                GviMap.AxMapControl.RcMouseClickSelect += AxMapControl_RcMouseClickSelect;
            }
            else
            {
                GviMap.AxMapControl.RcMouseClickSelect -= AxMapControl_RcMouseClickSelect;
                GviMap.InteractMode = gviInteractMode.gviInteractNormal;
                GviMap.MouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectNone;
                GviMap.MouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
            }
        }

        private void AxMapControl_RcMouseClickSelect(IPickResult PickResult, IPoint IntersectPoint, gviModKeyMask Mask, gviMouseSelectMode EventSender)
        {
            if (EventSender == gviMouseSelectMode.gviMouseSelectMove)
                return;

            var pickedRPoi = PickResult as IRenderPOIPickResult;

            var selectedPoi = currentLineModelList?.Find(p => p.Guid == pickedRPoi?.RenderPOI?.Guid.ToString());

            if (selectedPoi == null || string.IsNullOrEmpty(selectedPoi.Path))
                return;

            imageDisplayVModel.BtnVisiable = Visibility.Visible;
            imageDisplayVModel.CurrentPicPoiModel = selectedPoi;
            imageDisplayVModel.ShowImageView(selectedPoi.Path);
        }


        //public void ClearRenderObj()
        //{
        //    if (_renderLayers.Count > 0)
        //        foreach (var item in _renderLayers.ToList())
        //            RemoveRenderLayer(item.Key);

        //    ClearRouteRender();
        //    ClearPoiRender();
        //}

        public void HideRenderLayer()
        {
            if (_renderLayers?.Count < 0)
                return;
            foreach (var layer in _renderLayers.Values)
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
            foreach (var layer in _renderLayers.Values)
            {
                if (_rLayersRenderableStatus.ContainsKey(layer.Guid.ToString()))
                    layer.Renderable.VisibleMask = _rLayersRenderableStatus[layer.Guid.ToString()];
            }

            if (currentLineModelList != null) CreatePhotoPoints(currentLineModelList);
        }


        public void SaveRenderLayersStatus()
        {
            _rLayersRenderableStatus?.Clear();
            if (_renderLayers?.Count < 0)
                return;
            foreach (var layer in _renderLayers.Values)
            {
                if (layer.Guid != null && layer.Renderable != null && !_rLayersRenderableStatus.ContainsKey(layer.Guid.ToString()))
                    _rLayersRenderableStatus.Add(layer.Guid.ToString(), layer.Renderable.VisibleMask);
            }

            ClearRouteRender();
            ClearPoiRender();
        }

        public void CloseHintView()
        {
            leftHintVModel?.CloseView();
            rightHintVModel?.CloseView();
            leftHintVModel = null;
            rightHintVModel = null;
        }

        private void RemoveRenderLayer(string key)
        {
            if (_renderLayers.Count > 0 && _renderLayers.ContainsKey(key))
            {
                DataBaseService.Instance.RenderControl.ObjectManager.DeleteObject(_renderLayers[key].Renderable.Guid);
                _renderLayers.Remove(key);
            }
        }

        private void ClearRouteRender()
        {
            GviMap.TraceLinePolyManager.Clear();
        }

        private void ClearPoiRender()
        {
            GviMap.TracePoiManager.Clear();
        }


        private void LoadingDsProcess(string msg)
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
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
    }
}
