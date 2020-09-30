using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using LiveCharts;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using Application = System.Windows.Application;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class ReportPreViewModel : BaseViewModel
    {
        //public Action OnDownloadReport;
        private ReportPreviewView _reportPreviewView;

        //private ReportRankingViewModel _reportRankingViewModel;
        private string _reportHead;

        public string ReportHead
        {
            get => _reportHead;
            set
            {
                _reportHead = value;
              OnPropertyChanged("ReportHead");
            }
        }

        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        private string _firstPart;

        public string FirstPart
        {
            get => _firstPart;
            set
            {
                _firstPart = value;
                OnPropertyChanged("FirstPart");
            }
        }

        private string _lastPart;

        public string LastPart
        {
            get => _lastPart;
            set
            {
                _lastPart = value;
                OnPropertyChanged("LastPart");
            }
        }

        private List<MarkerNew> _selectPoiList ;

        public List<MarkerNew> SelectPoiList
        {
            get => _selectPoiList;
            set
            {
                _selectPoiList = value;
                OnPropertyChanged("SelectPoiList");
            }
        }

        private Dictionary<string, string> _selectPoiDic;
        public Dictionary<string, string> SelectPoiDic
        {
            get => _selectPoiDic;
            set
            {
                _selectPoiDic = value;
                OnPropertyChanged("SelectPoiDic");
            }
        }


        [XmlIgnore] public ICommand CancelCommand { get; set; }
        [XmlIgnore] public ICommand OutputCommand { get; set; }
        string SelectedCondFirst;
        string SelectedCondSecond;
        string SelectedMethodFirst;
        string SelectedMethodSecond;
        public ReportPreViewModel(List<string> labels, List<string> values,List<MarkerNew> select,string _selectedCondFirst, string _selectedCondSecond,string _selectedMethodFirst,string _SelectedMethodSecond)
        {
            SelectedCondFirst = _selectedCondFirst;
            SelectedCondSecond = _selectedCondSecond;
            SelectedMethodFirst = _selectedMethodFirst;
            SelectedMethodSecond = _SelectedMethodSecond;
            SelectPoiDic = new Dictionary<string, string>();
            for (int i=0;i<labels.Count;i++)
            {
                if(!SelectPoiDic.Keys.Contains(labels[i]))
                {
                    SelectPoiDic.Add(labels[i], values[i]);
                }
                
            }

            // var dicSort= SelectPoiDic.OrderByDescending(t => t.Value).Select(e => e.Key).ToList();
            //var dicNew = SelectPoiDic.First(a => a.Value == SelectPoiDic.Values.Max());
            int Max = 0;
            string MaxName = "";
            foreach (var item in SelectPoiDic)
            {
                if(Convert.ToInt32(item.Value)> Max)
                {
                    Max = Convert.ToInt32(item.Value);
                    MaxName = item.Key;
                }
            }
            SelectPoiList = new List<MarkerNew>(select);
            _reportPreviewView = new ReportPreviewView();
            _reportPreviewView.DataContext = this;

            this.CancelCommand =new RelayCommand(this.CloseView);
            this.OutputCommand = new RelayCommand(this.OutputReport);
            ReportHead = "巡检报告";
            string dateTime = DateTime.Now.ToString("D");
            string poiCount = select.Count().ToString();
            int num = 0;
            for (int i =0;i<values.Count;i++)
            {
                num = num + Convert.ToInt32(values[i]);
            }
            string numCount = num.ToString();
            FirstPart = string.Format("本次巡检报告发布日期为{0},共有标注点{1}个，标签{2}种，统计情况如下图所示：", dateTime, poiCount, values.Count.ToString());
            if(SelectPoiDic.Count ==0)
            {
                LastPart = string.Format("从图中可以看出，不包含有标签的标注，标注详情请见报告正文。");
            }
            else if(SelectPoiDic.Count>0)
            {
                LastPart = string.Format("从图中可以看出，标签含{0}的标注数量最多，共有{1}处，详情请见报告正文。", MaxName, Max);
            }
            

            this.ShowView();

            _reportPreviewView.Owner = Application.Current.MainWindow;
            _reportPreviewView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _reportPreviewView.Show();

            AnalysisiBarChartVModel(labels, values);
            //this.ImagePath = barViewModel.OutputImagePath();

        }

        private void OutputReport()
        {
            
            var a = SelectedCondFirst;
           // this.CloseView();
            //OnDownloadReport.Invoke();

            var idList = SelectPoiList.Select(t => t.MarkerId).ToList();

            string poiids = string.Join(",", idList);
            System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
            string mydocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveDlg.InitialDirectory = mydocPath/* + "\\"*/;
            saveDlg.Filter = "word文档|*.doc";
            saveDlg.FilterIndex = 2;
            saveDlg.FileName = GetTimeStamp();
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string directPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\ReportImage";
                string directImgPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\ReportImage\img.png";
                string file_path = "";
                if (_reportPreviewView != null)
                {
                    _reportPreviewView.DownLoadBtn.Visibility = Visibility.Hidden;
                    _reportPreviewView.Titles.Visibility = Visibility.Hidden;

                    try
                    {
                        var obj = _reportPreviewView.reportCollection;
                        if (obj != null)
                        {
                            SaveFrameworkElementToImage(obj as FrameworkElement, directImgPath);
                            _reportPreviewView.DownLoadBtn.Visibility = Visibility.Visible;
                            _reportPreviewView.Titles.Visibility = Visibility.Visible;
                            string img = string.Empty;
                            string resStr = HttpServiceHelper.Instance.PostImageFile("/api/upload-form/getloadfile", directImgPath);
                            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
                            file_path = resDyn.img_url;

                            //string api = RegularInspectInterface.UploadStatisticImgInf;

                            //string result = HttpServiceHelper.Instance.PostImageFile(api, directImgPath);
                            //var imgUploadDyn = JsonUtil.DeserializeFromString<dynamic>(result);
                            //file_path = imgUploadDyn.file_path;
                        }
                    }
                    catch { }                    
                    _reportPreviewView.DownLoadBtn.Visibility = Visibility.Visible;
                    _reportPreviewView.Titles.Visibility = Visibility.Visible;
                }
                Task.Run(() =>
                {
                    string reportName = Path.GetFileName(saveDlg.FileName);
                    string downloadReport = "";
                    if (File.Exists(saveDlg.FileName))
                    {
                        File.Delete(saveDlg.FileName);
                    }
                    if (file_path != "")
                    {
                        downloadReport = string.Format("{0}?marker_id={1}&name={2}&img={3}", MarkInterface.DownloadMarksReportInf, poiids, reportName, file_path);//&name={2}，reportName
                    }
                    else
                    {
                        downloadReport = string.Format("{0}?marker_id={1}&name={2}", MarkInterface.DownloadMarksReportInf, poiids, reportName);//&name={2}，reportName
                    }


                    if (SelectedCondFirst != "0")
                    {
                        downloadReport = downloadReport + string.Format("&sort[{0}]={1}", SelectedCondFirst, SelectedMethodFirst);
                    }
                    if (SelectedCondSecond != "0")
                    {
                        downloadReport = downloadReport + string.Format("&sort[{0}]={1}", SelectedCondSecond, SelectedMethodSecond);
                    }
                    HttpServiceHelper.Instance.DownloadFile(downloadReport, saveDlg.FileName, DownloadResult);
                    Messages.ShowMessage("生成完成");
                    System.Diagnostics.Process.Start("explorer.exe", saveDlg.FileName);
                });
              
            }
            //_reportRankingViewModel = new ReportRankingViewModel(SelectPoiList,  _reportPreviewView);
            // _reportRankingViewModel.OnDownloadReport += DownloadReport;
        }

   

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        private void ShowView()
        {

        }

        private void CloseView()
        {
            _reportPreviewView?.Close();
            _reportPreviewView = null;
        }
















        private AnalysisiBarChartDisplayView analysisiBarChartView;
        private int _num;
        private List<string> _NameList;
        private List<string> _NumList;
        private string _geom;
        public string _currentFileName = null;
        public void AnalysisiBarChartVModel(int num, List<string> NameList, List<string> NumList, string geom)
        {
            _num = num;
            _NameList = NameList;
            _NumList = NumList;
            _geom = geom;
            Load();
            //analysisiBarChartView = new AnalysisiBarChartDisplayView();
            //analysisiBarChartView.DataContext = this;

        }

        public void AnalysisiBarChartVModel(List<string> NameList, List<string> NumList)
        {
            _num = NameList.Count;
            _NameList = NameList;
            _NumList = NumList;
            Load();
            //analysisiBarChartView = new AnalysisiBarChartDisplayView();
            //analysisiBarChartView.DataContext = this;

            Console.WriteLine(string.Format("1:{0}", DateTime.Now));
            Thread.Sleep(3000);
            Console.WriteLine(string.Format("2:{0}", DateTime.Now));
        }



        public string OutputImagePath()
        {
            string path = string.Empty;

            path = OnExportImageCommand(this.analysisiBarChartView.Bartotal as FrameworkElement);
            //analysisiBarChartView.Hide();
            return path;
        }

        private RelayCommand<object> _exportCommand;

        public RelayCommand<object> ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new RelayCommand<object>(OnExportCommand)); }
            set { _exportCommand = value; }
        }
        private SeriesCollection _barseriesCollections;

        public SeriesCollection BarSeriesCollections
        {

            get => _barseriesCollections;
            set
            {
                _barseriesCollections = value;
                OnPropertyChanged("BarSeriesCollections");
            }
        }



        private void OnExportCommand(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = FileFilterStrings.WORD;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFileName = saveFileDialog.FileName;
                string filename = System.IO.Path.GetFileName(saveFileDialog.FileName); 
                if (obj == null) return;

                string cacheImgFile = Path.GetDirectoryName(_currentFileName) + "\\" + "img.png";
                SaveFrameworkElementToImage(obj as FrameworkElement, cacheImgFile);

                string api = RegularInspectInterface.UploadStatisticImgInf;

                string result = HttpServiceHelper.Instance.PostImageFile(api, cacheImgFile);
                var imgUploadDyn = JsonUtil.DeserializeFromString<dynamic>(result);
                string file_path = imgUploadDyn.file_path;
                if (!string.IsNullOrEmpty(file_path))
                {
                    var httpDowLoadManager = new HttpDowLoadManager();
                    httpDowLoadManager.Token = HttpServiceUtil.Token;

                    Task.Run(() =>
                    {
                        //httpDowLoadManager.DownloadMarkerStatisticsReport(_currentFileName, file_path, _geom, DownloadResult);
                        string downloadReport = string.Format("{0}?file_path={1}&geom={2}&name={3}", MarkInterface.MarksStatisticsReportInf, file_path, _geom, filename);
                        HttpServiceHelper.Instance.DownloadFile(downloadReport, _currentFileName, DownloadResult);
                    });
                }
                //Messages.ShowMessage("导出图片成功！");
            }
        }

        private string OnExportImageCommand(object obj)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = FileFilterStrings.WORD;
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //_currentFileName = saveFileDialog.FileName;
            _currentFileName = "G:\\hello\\testimg.png";
            if (obj == null) return "";

            string cacheImgFile = Path.GetDirectoryName(_currentFileName) + "\\" + DateTime.Now.ToString("yyyyMMddHHMMss") + ".png";
            SaveFrameworkElementToImage(obj as FrameworkElement, cacheImgFile);

            return cacheImgFile;
            //string api = RegularInspectInterface.UploadStatisticImgInf;

            //string result = HttpServiceHelper.Instance.PostImageFile(api, cacheImgFile);
            //var imgUploadDyn = JsonUtil.DeserializeFromString<dynamic>(result);
            //string file_path = imgUploadDyn.file_path;
            //if (!string.IsNullOrEmpty(file_path))
            //{
            //    var httpDowLoadManager = new HttpDowLoadManager();
            //    httpDowLoadManager.Token = HttpServiceUtil.Token;

            //    Task.Run(() =>
            //    {
            //        //httpDowLoadManager.DownloadMarkerStatisticsReport(_currentFileName, file_path, _geom, DownloadResult);
            //        string downloadReport = string.Format("{0}?file_path={1}&geom={2}", MarkInterface.MarksStatisticsReportInf, file_path, _geom);
            //        HttpServiceHelper.Instance.DownloadFile(downloadReport, _currentFileName, DownloadResult);
            //    });
            //}
            //Messages.ShowMessage("导出图片成功！");
            //}
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

        private void SaveFrameworkElementToImage(FrameworkElement ui, string filename)
        {

            FileStream ms = new FileStream(filename, FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            bmp.Render(ui);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(ms);
            ms.Close();
        }
        private void Load()
        {

            SeriesCollection series = new SeriesCollection();
            for (int i = 0; i < _num; i++)
            {
                LiveCharts.Wpf.ColumnSeries ser = new LiveCharts.Wpf.ColumnSeries();
                ser.Values = new LiveCharts.ChartValues<decimal> { Convert.ToInt32(_NumList[i]) };
                ser.Title = _NameList[i];
                ser.DataLabels = false ;
                //if(_num<20)
                //{
                //    ser.Title = _NameList[i];
                //    ser.DataLabels = false ;
                //}
                //else
                //{
                //    //ser
                //    ser.DataLabels = false;
                //}


                //    {
                //    Values = new LiveCharts.ChartValues<decimal> { Convert.ToInt32(_NumList[i]) },
                //    ,
                //    DataLabels = true

                //};



                series.Add(ser);
            }
            BarSeriesCollections = series; //
        }
    }
}
