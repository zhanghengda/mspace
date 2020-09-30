using Microsoft.Win32;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class ReportRankingViewModel : BaseViewModel
    {
        
        private ReportRankingView _reportRankingView;

        private ObservableCollection<TextItem> _rankConditionSet;
        private ObservableCollection<TextItem> _rankConditionSet2;
        public ObservableCollection<TextItem> RankConditionSet
        {
            get => _rankConditionSet;
            set
            {
                _rankConditionSet = value;
                OnPropertyChanged("RankConditionSet");
            }
        }

        public ObservableCollection<TextItem> RankConditionSet2
        {
            get => _rankConditionSet2;
            set
            {
                _rankConditionSet2 = value;
                OnPropertyChanged("RankConditionSet2");
            }
        }
        private ObservableCollection<TextItem> _rankMethodSet;

        public ObservableCollection<TextItem> RankMethodSet
        {
            get => _rankMethodSet;
            set
            {
                _rankMethodSet = value;
                OnPropertyChanged("RankMethodSet");
            }
        }

        private TextItem _selectedCondFirst;

        public TextItem SelectedCondFirst
        {
            get => _selectedCondFirst;
            set
            {
                _selectedCondFirst = value;
                OnPropertyChanged("SelectedCondFirst");
            }
        }

        private TextItem _selectedCondSecond;

        public TextItem SelectedCondSecond
        {
            get => _selectedCondSecond;
            set
            {
                _selectedCondSecond = value;
                OnPropertyChanged("SelectedCondSecond");
            }
        }

        private TextItem _selectedMethodFirst;

        public TextItem SelectedMethodFirst
        {
            get => _selectedMethodFirst;
            set
            {
                _selectedMethodFirst = value;
                OnPropertyChanged("SelectedMethodFirst");
            }
        }

        private TextItem _selectedMethodSecond;
        public TextItem SelectedMethodSecond
        {
            get => _selectedMethodSecond;
            set
            {
                _selectedMethodSecond = value;
                OnPropertyChanged("SelectedMethodSecond");
            }
        }

        private List<MarkerNew> _selectPoiList;
        public List<MarkerNew> SelectPoiList
        {
            get => _selectPoiList;
            set
            {
                _selectPoiList = value;
                OnPropertyChanged("SelectPoiList");
            }
        }
        private ObservableCollection<MarkerNew> _previewCollection = new ObservableCollection<MarkerNew>();
        public ObservableCollection<MarkerNew> PreviewCollection
        {
            get { return _previewCollection; }
            set
            {
                _previewCollection = value;
                OnPropertyChanged("PreviewCollection");
               // base.SetAndNotifyPropertyChanged<ObservableCollection<MarkerNew>>(ref this._previewCollection, value, "PreviewCollection");
            }
        }
        [XmlIgnore] public ICommand CancelCommand { get; set; }

        [XmlIgnore] public ICommand SaveCommand { get; set; }
        [XmlIgnore] public ICommand SortChangedCmd { get; set; }        
        [XmlIgnore] public ICommand RankConditionSetChangedCmd { get; set; }
        [XmlIgnore] public ICommand RankConditionSet2ChangedCmd { get; set; }
       // ReportPreviewView tempReportPreviewView = null;
        List<string> Labels = new List<string>();
        List<string> Valuecount = new List<string>();
        List<MarkerNew> Selecttagelist = null;
        public ReportRankingViewModel(List<string> labels, List<string> values, List<MarkerNew> select)
        {
            Labels = labels;
            Valuecount = values;
            Selecttagelist = new List<MarkerNew>(select);
            // tempReportPreviewView = _reportPreviewView;
            SelectPoiList = new List<MarkerNew>(select);
            _reportRankingView = new ReportRankingView();
            _reportRankingView.DataContext = this;
            this.CancelCommand = new RelayCommand(CloseView);
            this.SaveCommand = new RelayCommand(SaveConditons);
            this.RankConditionSetChangedCmd = new RelayCommand(RankConditionSetChanged);
            this.SortChangedCmd = new RelayCommand(SortChangedChanged);
            this.RankConditionSet2ChangedCmd = new RelayCommand(RankConditionSet2Changed);
            this.RankConditionSet = new ObservableCollection<TextItem>(GetRankConditions());
            this.RankConditionSet2 = new ObservableCollection<TextItem>(GetRankConditions());

            this.RankMethodSet = new ObservableCollection<TextItem>(GetRankMethod());
            this.SelectedCondFirst = RankConditionSet[0];
            this.SelectedCondSecond = RankConditionSet2[0];
            this.SelectedMethodFirst = RankMethodSet[0];
            this.SelectedMethodSecond = RankMethodSet[0];

            this.ShowView();
            GetPreviewList();
        }

        private void SaveConditons()
        {
            var reportPreVM = new ReportPreViewModel(Labels, Valuecount, Selecttagelist,this.SelectedCondFirst.Key,this.SelectedCondSecond.Key, this.SelectedMethodFirst.Key, this.SelectedMethodSecond.Key);
            this.CloseView();
            //var a = this.SelectedCondFirst;
            //this.CloseView();
            //OnDownloadReport.Invoke();

            //var idList = SelectPoiList.Select(t => t.MarkerId).ToList();

            //string poiids = string.Join(",", idList);
            //System.Windows.Forms.SaveFileDialog saveDlg = new System.Windows.Forms.SaveFileDialog();
            //string mydocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //saveDlg.InitialDirectory = mydocPath/* + "\\"*/;
            //saveDlg.Filter = "word文档|*.doc";
            //saveDlg.FilterIndex = 2;
            //saveDlg.FileName = GetTimeStamp();
            //if (saveDlg.ShowDialog() == DialogResult.OK)
            //{
            //    string directPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\ReportImage";
            //    string directImgPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\ReportImage\img.png";
            //    string file_path = "";
            //    if (tempReportPreviewView != null)
            //    {
            //        var obj = tempReportPreviewView.reportCollection;
            //        if (obj != null)
            //        {
            //            SaveFrameworkElementToImage(obj as FrameworkElement, directImgPath);

            //            string img = string.Empty;
            //            string resStr = HttpServiceHelper.Instance.PostImageFile("/api/upload-form/getloadfile", directImgPath);
            //            var resDyn = JsonUtil.DeserializeFromString<dynamic>(resStr);
            //            file_path = resDyn.img_url;
                      
            //            //string api = RegularInspectInterface.UploadStatisticImgInf;

            //            //string result = HttpServiceHelper.Instance.PostImageFile(api, directImgPath);
            //            //var imgUploadDyn = JsonUtil.DeserializeFromString<dynamic>(result);
            //            //file_path = imgUploadDyn.file_path;
            //        }
            //    }
            //    Task.Run(() =>
            //    {
            //        string reportName = Path.GetFileName(saveDlg.FileName);
            //        string downloadReport = "";
            //        if (file_path != "")
            //        {
            //            downloadReport = string.Format("{0}?marker_id={1}&name={2}&img={3}", MarkInterface.DownloadMarksReportInf, poiids, reportName, file_path);//&name={2}，reportName
            //        }
            //        else
            //        {
            //            downloadReport = string.Format("{0}?marker_id={1}&name={2}", MarkInterface.DownloadMarksReportInf, poiids, reportName);//&name={2}，reportName
            //        }


            //        if (SelectedCondFirst.Key != "0")
            //        {
            //            downloadReport = downloadReport + string.Format("&sort[{0}]={1}", SelectedCondFirst.Key, SelectedMethodFirst.Key);
            //        }
            //        if (SelectedCondSecond.Key != "0")
            //        {
            //            downloadReport = downloadReport + string.Format("&sort[{0}]={1}", SelectedCondSecond.Key, SelectedMethodSecond.Key);
            //        }
            //        HttpServiceHelper.Instance.DownloadFile(downloadReport, saveDlg.FileName, DownloadResult);
            //    });
            //}
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

        private void RankConditionSetChanged()
        {
            GetPreviewList();
            //if (this.SelectedCondFirst== RankConditionSet[0]&&this.SelectedCondSecond!=RankConditionSet2[0])
            //{

            //    this.RankConditionSet2 = new ObservableCollection<TextItem>(GetRankConditions());
            //    this.SelectedCondSecond = RankConditionSet2[0];


            //}
            //else
            //{
            //    this.RankConditionSet2 = new ObservableCollection<TextItem>(GetRankConditions());
            //    for (int i =0;i< RankConditionSet2.Count;i++)
            //    {
            //        if(this.SelectedCondFirst.Key== RankConditionSet2[i].Key)
            //        {
            //            RankConditionSet2?.Remove(RankConditionSet2[i]);
            //        }
            //    }             
            //}
            // this.SelectedCondSecond = RankConditionSet2[3];
        }
        
        private void SortChangedChanged()
        {
            GetPreviewList();
            //List<MarkerNew> markerNews = new List<MarkerNew>();
            //if (PreviewCollection?.Count > 0)
            //{
            //    for (int i = PreviewCollection.Count; i > 0; i--)
            //    {
            //        markerNews.Add(PreviewCollection[i - 1]);
            //    }
            //}
            //PreviewCollection?.Clear();
            //for (int k = 0; k < markerNews.Count; k++)
            //{
            //    PreviewCollection.Add(markerNews[k]);
            //}



        }
        private void RankConditionSet2Changed()
        {
            List<MarkerNew> markerNews = new List<MarkerNew>();
            if(PreviewCollection?.Count>0)
            {
                for(int i = PreviewCollection.Count; i > 0;i--)
                {
                    markerNews.Add(PreviewCollection[i-1]);
                }
            }
            PreviewCollection?.Clear();
            for(int k=0;k<markerNews.Count;k++)
            {
                PreviewCollection.Add(markerNews[k]);
            }
            
           //if(this.SelectedCondFirst.Key=="0"&&this.SelectedCondSecond.Key!="0")
           // {

            //     Messages.ShowMessage("尚未选择一级查询条件");

            // }

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


        private List<TextItem> GetRankConditions()
        {
            var list = new List<TextItem>()
            {
                new TextItem()
                {
                  Key  = "0",Value = "无"
                },
                new TextItem()
                {
                    Key  = "accountime",Value = "最新台账日期"
                },
                //new TextItem()
                //{
                //    Key  = "type",Value = "标签"
                //},
                new TextItem()
                {
                    Key  = "letter",Value = "标注名称"
                },
                new TextItem()
                {
                    Key  = "level",Value = "等级"
                }
            };

            return list;
        }

        private List<TextItem> GetRankMethod()
        {
            var list = new List<TextItem>()
            {
                new TextItem()
                {
                    Key  = "ASC",Value = "升序"
                },
                new TextItem()
                {
                    Key  = "DESC",Value = "降序"
                }
            };

            return list;
        }

        public void ShowView()
        {
            //_reportRankingView.Owner = _reportPreviewView;
            _reportRankingView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _reportRankingView.Show();
            _reportRankingView.RankMethod.Visibility = Visibility.Hidden;
        }

        private void CloseView()
        {
            _reportRankingView?.Close();
            _reportRankingView = null;
        }
        private void GetPreviewList()
        {
            var idList = SelectPoiList.Select(t => t.MarkerId).ToList();
            string markList = string.Join(",", idList);
            string PreViewReport = PreViewReport = string.Format("{0}?marker_id={1}", MarkInterface.PreviewMarksReportInf, markList);
            if (SelectedCondFirst.Key != "0")
            {
                PreViewReport = PreViewReport + string.Format("&sort[{0}]={1}", SelectedCondFirst.Key, SelectedMethodFirst.Key);
                _reportRankingView.RankMethod.Visibility = Visibility.Visible;
            }
            else
            {
                _reportRankingView.RankMethod.Visibility = Visibility.Hidden;
            }
           
            string resStr = HttpServiceHelper.Instance.GetRequestAsync(PreViewReport);
            if (resStr != null && resStr != "")
            {
                PreViewMessage(resStr);
            }
        }
        public void PreViewMessage(string _input)
        {

            using (JsonTextReader reader = new JsonTextReader(new StringReader(_input)))
            {

                JArray jArray = (JArray)JToken.ReadFrom(reader);
                PreviewCollection?.Clear();
                foreach (JObject obj in jArray)
                {
                    
                    //int objID = Convert.ToInt32(obj["id"]);
                    string marker_id = Convert.ToInt32(obj["marker_id"]).ToString();
                    string title = obj["title"].ToString();
                    string operatorName = obj["operator"].ToString();
                    MarkerNew markerNew = new MarkerNew();
                    markerNew.Title = title;
                    markerNew.Operator = operatorName;
                    PreviewCollection.Add(markerNew);
                }
            }
        }
       
    }
}