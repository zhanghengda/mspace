using LiveCharts;
using LiveCharts.Wpf;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    class AnalysisiChartVModel : BindableBase
    {
        private AnalysisiChartDisplayView _ChartDisplayView;
        private int _num;
        private List<string> _NameList;
        private List<string> _NumList;
        private string _geom;
        public string _currentFileName = null;
        public AnalysisiChartVModel(int num, List<string> NameList, List<string> NumList, string geom)
        {
            _num = num;
            _NameList = NameList;
            _NumList = NumList;
            _geom = geom;
            Load();
            _ChartDisplayView = new AnalysisiChartDisplayView();
            _ChartDisplayView.DataContext = this;
            _ChartDisplayView.Show();
        }



        private RelayCommand<object> _exportCommand;

        public RelayCommand<object> ExportCommand
        {
            get { return _exportCommand ?? (_exportCommand = new RelayCommand<object>(OnExportCommand)); }
            set { _exportCommand = value; }
        }


        private SeriesCollection _seriesCollections;

        public SeriesCollection SeriesCollections
        {
            get { return _seriesCollections; }
            set { _seriesCollections = value; NotifyPropertyChanged("SeriesCollections"); }
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
                        //httpDowLoadManager.DownloadMarkerStatisticsReport(_currentFileName, file_path, _geom, DowloadResult);

                        string downloadReport = string.Format("{0}?file_path={1}&geom={2}&name={3}", MarkInterface.MarksStatisticsReportInf, file_path, _geom, filename);
                        HttpServiceHelper.Instance.DownloadFile(downloadReport, _currentFileName, DownloadResult);
                    });
                }
                //Messages.ShowMessage("导出图片成功！");
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
                LiveCharts.Wpf.PieSeries ser = new LiveCharts.Wpf.PieSeries
                {
                    Values = new LiveCharts.ChartValues<decimal> { Convert.ToInt32(_NumList[i]) },
                    Title = _NameList[i],
                    DataLabels = true
                };
                series.Add(ser);
            }
            SeriesCollections = series; //
        }
    }
}
