using Gvitech.Windows.Utils;
using Microsoft.Win32;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.PoiManagerModule;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    /// <summary>
    /// 原导入数据方式，现已弃用；可删除
    /// </summary>
    public class RegInsImportDataVModel : BaseViewModel
    {
        public Action<InspectModel> UpdateDataList;
        public Action CloseWindow;
        //public Action ClearData;

        private readonly ExportProgressView progressView = new ExportProgressView();
        public int currentUnitId;
        public string currentUnitName;

        private TextItem _selectedItem;
        public TextItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private string _dataFileName;
        public string DataFileName
        {
            get { return _dataFileName; }
            set { _dataFileName = value; OnPropertyChanged("DataFileName"); }
        }

        private bool _isSave;
        public bool IsSave
        {
            get { return _isSave; }
            set { _isSave = value; OnPropertyChanged("IsSave"); }
        }


        private ObservableCollection<TextItem> _inspectionTypes;

        public ObservableCollection<TextItem> InspectionTypes
        {
            get { return _inspectionTypes; }
            set { _inspectionTypes = value; OnPropertyChanged("InspectionTypes"); }
        }


        [XmlIgnore]
        public ICommand CancelCmd { get; set; }
        [XmlIgnore]
        public ICommand SaveCmd { get; set; }
        [XmlIgnore]
        public ICommand ChooseFileCmd { get; set; }

        public RegInsImportDataVModel()
        {


            InspectionTypes = new ObservableCollection<TextItem>( CommonContract.GetInspectDataType());

            this.CancelCmd = new RelayCommand(() =>
            {
                CloseWindow();
            });

            this.SaveCmd = new RelayCommand(() =>
            {
                //CreatePhotoTrace();
                ImportData();
                //CloseWindow(false);
            });
            this.ChooseFileCmd = new RelayCommand(() =>
            {
                OpenDataFile();
            });

            IsSave = false;
        }

        private void OpenDataFile()
        {
            if (_selectedItem == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ChooseDataType"));
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            if (_selectedItem.Key == CommonContract.InspectDataType.Dom.ToString())
            {
                dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionDom") + Helpers.ResourceHelper.FindKey("File");
                dialog.Filter = FileFilterStrings.TIF;
            }
            else if (_selectedItem.Key == CommonContract.InspectDataType.Picture.ToString())
            {
                dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionPicture") + Helpers.ResourceHelper.FindKey("File");
                dialog.Filter = FileFilterStrings.IMAGE;
            }
            else if (_selectedItem.Key == CommonContract.InspectDataType.Video.ToString())
            {
                dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionVideo") + Helpers.ResourceHelper.FindKey("File");
                dialog.Filter = FileFilterStrings.Video;
            }
            else if (_selectedItem.Key == CommonContract.InspectDataType.Route.ToString())
            {
                dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionRoute") + Helpers.ResourceHelper.FindKey("File");
                dialog.Filter = FileFilterStrings.KML;
            }
            else if (_selectedItem.Key == CommonContract.InspectDataType.Report.ToString())
            {
                dialog.Title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionReport") + Helpers.ResourceHelper.FindKey("File");
                dialog.Filter = FileFilterStrings.WORD;
            }

            if (dialog.ShowDialog() == true)
            {
                DataFileName = dialog.FileName;
                IsSave = true;
            }
        }

        private void ImportData()
        {

            if (_selectedItem == null )
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("PleaseChoose")+ Helpers.ResourceHelper.FindKey("DataType"));
                return;
            }
            if (string.IsNullOrEmpty( DataFileName))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("File"));
                return;
            }

            string fileExtention = Path.GetExtension(DataFileName).ToUpper();

            Task.Run(() =>
            {
                try
                {
                    LoadingDsProcess(string.Format(Helpers.ResourceHelper.FindKey("UploadDataToUnit"), currentUnitName));
                    InspectModel inspectModel = new InspectModel();

                    //string fileMd5 = FileUtil.GetSHA1FromFile(DataFileName);

                    string fileName = Path.GetFileNameWithoutExtension(DataFileName);

                    if (_selectedItem.Key == CommonContract.InspectDataType.Dom.ToString())
                    {
                        if (!fileExtention.Contains("TIF"))
                        {
                            FinishLoadProcess();
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                            return;
                        }

                        string thumb = fileName + "_thumb.png";
                        string thumbPath = Path.GetDirectoryName(DataFileName) + "\\" + thumb;

                        var reclassify = new DomReclassify();
                        reclassify.PythonProcess.ImputFile = DataFileName;
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
                            Path = DataFileName,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5,
                            Thumbnail = thumbPath

                        };
                        Console.WriteLine(DateTime.Now);
                        InspectionService.Instance.AddDom(domCfg);

                        inspectModel = RegInsModelConvert.DomConvert(domCfg);
                        Console.WriteLine(DateTime.Now);
                        UpdateDataList(inspectModel);

                        ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
                        {
                            Messenger.Messengers.Notify("HistoryDomRefresh");
                        });
                    }
                    else if (_selectedItem.Key == CommonContract.InspectDataType.Picture.ToString())
                    {
                        if (!fileExtention.Contains("JPG") && !fileExtention.Contains("PNG") && !fileExtention.Contains("BMP"))
                        {
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                            FinishLoadProcess();
                            return;
                        }

                        //if (InspectionService.Instance.PictureItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}


                        var picCfg = new PictureItem()
                        {
                            Path = DataFileName,
                            Name = fileName,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5
                        };

                        InspectionService.Instance.PictureItemSet.Add(picCfg);

                        inspectModel = RegInsModelConvert.PictureConvert(picCfg);
                        UpdateDataList(inspectModel);
                    }
                    else if (_selectedItem.Key == CommonContract.InspectDataType.Video.ToString())
                    {
                        if (!fileExtention.Contains("MP4") && !fileExtention.Contains("AVI"))
                        {
                            FinishLoadProcess();
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                            return;
                        }

                        //if (InspectionService.Instance.VideoItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        var videoCfg = new VideoItem()
                        {
                            Path = DataFileName,
                            Name = fileName,
                            InspectUnitId = currentUnitId,
                            //Md5 = fileMd5
                        };

                        InspectionService.Instance.VideoItemSet.Add(videoCfg);

                        inspectModel = RegInsModelConvert.VideoConvert(videoCfg);
                        UpdateDataList(inspectModel);
                    }
                    else if (_selectedItem.Key == CommonContract.InspectDataType.Route.ToString())
                    {
                        if (!fileExtention.Contains("KML"))
                        {
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                            FinishLoadProcess();
                            return;
                        }

                        //if (InspectionService.Instance.RouteItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        string style = string.Empty;
                        var polyLine = GviMap.GeoFactory.CreateFromXml(DataFileName, GviMap.SpatialCrs);
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

                        UpdateDataList(inspectModel);
                    }
                    else if (_selectedItem.Key == CommonContract.InspectDataType.Report.ToString())
                    {
                        if (!fileExtention.Contains("DOC"))
                        {
                            FinishLoadProcess();
                            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("NotMatchDataType"));
                            return;
                        }

                        //if (InspectionService.Instance.ReportItemSet.Exist(p => p.Md5 == fileMd5))
                        //{
                        //    FinishLoadProcess();
                        //    ShowDataOpenStatus(CommonContract.OperateDataStatus.DATAEXISTED);
                        //    return;
                        //}

                        string resStr = RegInsDataRenderManager.Instance.UploadReportFile(DataFileName);
                        var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                        if (resDyn != null)
                        {
                            var reportCfg = new ReportItem()
                            {
                                Path = resDyn?.file_path,
                                ReportNum = resDyn?.id,
                                Name = fileName,
                                InspectUnitId = currentUnitId,
                                //Md5 = fileMd5
                            };

                            InspectionService.Instance.ReportItemSet.Add(reportCfg);

                            inspectModel = RegInsModelConvert.ReportConvert(reportCfg);
                            UpdateDataList(inspectModel);
                        }
                    }

                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.SAVESUCCESSED);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    FinishLoadProcess();
                    ShowDataOpenStatus(CommonContract.OperateDataStatus.LOADFAILED);
                }
                finally
                {
                    CloseWindow();
                }

            });

        }

        public void ClearData()
        {
            this.SelectedItem = null;
            this.DataFileName = null;
        }

        //private bool HasChinese(string str)
        //{
        //    return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        //}

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
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey(status.ToString()));
            });
        }
    }
}
