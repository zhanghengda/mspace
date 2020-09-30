using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using MMC.MSpace.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Reflection;
namespace MMC.MSpace.ViewModels
{
    public class UserCenterVModel: CheckedToolItemModel//Rone
    {
        public ICommand CloseCmd { get; set; }
        public ICommand DeleteReportCmd { get; set; }
        public ICommand ChangeLabelCmd { get; set; }
        public ICommand FirstPageCmd { get; set; }
        public ICommand EndPageCmd { get; set; }
        public ICommand LastPageCmd { get; set; }
        public ICommand NextPageCmd { get; set; }
        public ICommand ReportSearchCmd { get; set; }
        public ICommand ViewCmd { get; set; }
        public ICommand IsOpenCmd { get; set; }

       // public ICommand CloseCmd { get; set; }
        public ICommand RoneDeleteReportCmd { get; set; }
        //public ICommand ChangeLabelCmd { get; set; }
        public ICommand RoneFirstPageCmd { get; set; }
        public ICommand RoneEndPageCmd { get; set; }
        public ICommand RoneLastPageCmd { get; set; }
        public ICommand RoneNextPageCmd { get; set; }
        public ICommand RoneReportSearchCmd { get; set; }
        public ICommand RoneViewCmd { get; set; }
        public ICommand RoneIsOpenCmd { get; set; }
        UserCenterView userCenterView = new UserCenterView();
        readonly int PageSizeInt = 10;
        private string username;
        int MaxPageNum;
        int RoneMaxPageNum;
        public  UserCenterVModel(string _username)
        {
            userCenterView.DataContext = this;
            username = _username;
            this.CloseCmd = new RelayCommand(() =>
            {
                CloseView();
            });
            this.DeleteReportCmd = new RelayCommand<ReportType>((_report) => DeleteReport(_report));
            this.ChangeLabelCmd = new RelayCommand<ReportType>((_report) => ChangeLabelText(_report));
            this.FirstPageCmd = new RelayCommand(OnFirstPage);          
            this.EndPageCmd = new RelayCommand(OnEndPage);
            this.LastPageCmd = new RelayCommand(OnLastPage);
            this.NextPageCmd = new RelayCommand(OnNextPage);

            this.ReportSearchCmd = new RelayCommand(OnReportSearch);
            this.ViewCmd = new RelayCommand<ReportType>((_report) => ExportWord(_report));
            this.IsOpenCmd = new RelayCommand<ReportType>((_report) => OnIsOpen(_report));

            this.RoneDeleteReportCmd = new RelayCommand<ReportType>((_report) => RoneDeleteReport(_report));
            //this.RoneChangeLabelCmd = new RelayCommand<ReportType>((_report) => ChangeLabelText(_report));
            this.RoneFirstPageCmd = new RelayCommand(OnRoneFirstPage);
            this.RoneEndPageCmd = new RelayCommand(OnRoneEndPage);
            this.RoneLastPageCmd = new RelayCommand(OnRoneLastPage);
            this.RoneNextPageCmd = new RelayCommand(OnRoneNextPage);
            this.RoneReportSearchCmd = new RelayCommand(OnRoneReportSearch);
            this.RoneViewCmd = new RelayCommand<ReportType>((_report) => ExportWord(_report));
            this.RoneIsOpenCmd = new RelayCommand<ReportType>((_report) => OnIsOpen(_report));

        }
        public void UpdateCollection()
        {
           
        }
        /// <summary>
        /// 组织请求报告列表的json字符串
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        public void GetReport(int pageSize, int page)
        {            
            List<ReportType> result = new List<ReportType>();
            string url = "";
            if (ReportSearchText == "")
            {
                url = string.Format("{0}?page_size={1}&page={2}", MarkInterface.Getbglist, pageSize, page);
            }
            else
            {
                url = string.Format("{0}?page_size={1}&page={2}&keyword={3}", MarkInterface.Getbglist, pageSize, page, ReportSearchText);
            }
            //string url = MarkInterface.Getbglist;
            //var json = @"{""page_size"":"+ pageSize + @",""page"":" + page + "}";         
            string resStr = HttpServiceHelper.Instance.GetRequest(url);         
            GetReportMessage(resStr, result);
          //  var list = JsonUtil.DeserializeFromString<List<ReportType>>(resStr);          
            if (result?.Count > 0)
            {
                ReportCollection.Clear();
                foreach (var account in result)
                {
                    account.UseName = username;
                    ReportCollection.Add(account);
                }
            }
        }
        public void GetRoneReport(int pageSize, int page)
        {
            List<ReportType> result = new List<ReportType>();
            string url = "";
            if (ReportSearchText == "")
            {
                url = string.Format("{0}?page_size={1}&page={2}&is_area=1", MarkInterface.Getbglist, pageSize, page);
            }
            else
            {
                url = string.Format("{0}?page_size={1}&page={2}&is_area=1&keyword={3}", MarkInterface.Getbglist, pageSize, page, RoneReportSearchText);
            }
            //string url = MarkInterface.Getbglist;
            //var json = @"{""page_size"":"+ pageSize + @",""page"":" + page + "}";         
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            GetReportMessage(resStr, result);
            //  var list = JsonUtil.DeserializeFromString<List<ReportType>>(resStr);          
            if (result?.Count > 0)
            {
                RoneReportCollection.Clear();
                foreach (var account in result)
                {
                    account.UseName = username;
                    RoneReportCollection.Add(account);
                }
            }
        }
        private string _reportSearchText;

        public string ReportSearchText
        {
            get { return _reportSearchText; }
            set { _reportSearchText = value; NotifyPropertyChanged("ReportSearchText"); }
        }
        private string _roneReportSearchText;

        public string RoneReportSearchText
        {
            get { return _roneReportSearchText; }
            set { _roneReportSearchText = value; NotifyPropertyChanged("RoneReportSearchText"); }
        }
        private string _pageCount;
         public string PageCount
        {
        get { return _pageCount; }
        set { _pageCount = value; NotifyPropertyChanged("PageCount"); }
        }

        private string _ronePageCount;
        public string RonePageCount
        {
            get { return _ronePageCount; }
            set { _ronePageCount = value; NotifyPropertyChanged("RonePageCount"); }
        }

        private string _pageNum = "1";
        public string PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; NotifyPropertyChanged("PageNum"); }
        }

        private string _ronePageNum = "1";
        public string RonePageNum
        {
            get { return _ronePageNum; }
            set { _ronePageNum = value; NotifyPropertyChanged("RonePageNum"); }
        }

        public void OpenView()
        {
            GetMaxPageNum();
            GetRoneMaxPageNum();
            OnFirstPage();
            OnRoneFirstPage();
            userCenterView.WindowStartupLocation = WindowStartupLocation.Manual;
            userCenterView.Top = 300;
            userCenterView.Left = 400;
            userCenterView.Show();
        }
        public void CloseView()
        {
            userCenterView.Hide();
        }
        public void AddReport(string _id,string _name, bool _isOpen)
        {
            ReportType report = new ReportType(_id, _name,_isOpen);
            ReportCollection.Add(report);
        }
        public void DeleteReport(string _id)
        {
            foreach(var item in ReportCollection)
            {
                if (item.ID == _id)
                {
                    ReportCollection.Remove(item);
                }
            }
        }
        public void DeleteReport(ReportType _report)
        {
            ReportCollection.Remove(_report);
            DeleteReportOnServer(_report);
        }
        public void RoneDeleteReport(ReportType _report)
        {
            RoneReportCollection.Remove(_report);
            DeleteReportOnServer(_report);
        }

        public void DeleteReportOnServer(ReportType _report)
        {
            // _report.ID;
            string url = MarkInterface.DelMyReport;
            var json = @"{""id"":" + _report.ID.ToString() + "}";
            string resStr = HttpServiceHelper.Instance.PostRequestForData(url, json);
            using (JsonTextReader reader = new JsonTextReader(new StringReader(resStr)))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                if (obj["msg"].ToString() == "成功")
                {
                    Messages.ShowMessage("删除成功！");
                }
                else
                {
                    Messages.ShowMessage("删除失败！");
                }
               
            }          
        }
       
        public void ChangeLabelText(ReportType _report)
        {
            _report.IsEdit = !_report.IsEdit;           
            var index = ReportCollection.IndexOf(_report);
            ReportType temp = userCenterView.Reportdg.Items.GetItemAt(index) as ReportType;
         
        }

        // ObservableCollection ReportCollection
        private ObservableCollection<ReportType> _reportCollection = new ObservableCollection<ReportType>();
        public ObservableCollection<ReportType> ReportCollection
        {
            get { return _reportCollection; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ReportType>>(ref this._reportCollection, value, "ReportCollection");
            }
        }
        private ObservableCollection<ReportType> _roneReportCollection = new ObservableCollection<ReportType>();
        public ObservableCollection<ReportType> RoneReportCollection
        {
            get { return _roneReportCollection; }
            set
            {
                base.SetAndNotifyPropertyChanged<ObservableCollection<ReportType>>(ref this._roneReportCollection, value, "RoneReportCollection");
            }
        }
        
        private void OnFirstPage()
        {
            try
            {
                GetReport(PageSizeInt,1);
                PageNum = "1";
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnRoneFirstPage()
        {
            try
            {
                GetRoneReport(PageSizeInt, 1);
                RonePageNum = "1";
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnEndPage()
        {
            try
            {
                if (MaxPageNum != 0)
                {
                    GetReport(PageSizeInt, MaxPageNum);
                    PageNum = MaxPageNum.ToString();
                }               
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnRoneEndPage()
        {
            try
            {
                if (RoneMaxPageNum != 0)
                {
                    GetRoneReport(PageSizeInt, RoneMaxPageNum);
                    RonePageNum = RoneMaxPageNum.ToString();
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnLastPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(PageNum);
                if (pageNow > 1)
                {
                    GetReport(PageSizeInt, pageNow - 1);
                    PageNum = Convert.ToString(pageNow - 1);
                }              
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnRoneLastPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(RonePageNum);
                if (pageNow > 1)
                {
                    GetRoneReport(PageSizeInt, pageNow - 1);
                    RonePageNum = Convert.ToString(pageNow - 1);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnNextPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(PageNum);
                if (pageNow < RoneMaxPageNum)
                {
                    GetReport(PageSizeInt, pageNow + 1);
                    PageNum = Convert.ToString(pageNow + 1);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnRoneNextPage()
        {
            try
            {
                int pageNow = Convert.ToInt32(RonePageNum);
                if (pageNow < RoneMaxPageNum)
                {
                    GetRoneReport(PageSizeInt, pageNow + 1);
                    RonePageNum = Convert.ToString(pageNow + 1);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }
        private void OnReportSearch()
        {
            GetMaxPageNum();
            OnFirstPage();
            List<ReportType> result = new List<ReportType>();
            //string url = string.Format("{0}?page_size={1}&page={2}", MarkInterface.Getbglist,pageSize, page);                    
            string url = string.Format("{0}?keyword={1}", MarkInterface.Getbglist,ReportSearchText);
            //string url = MarkInterface.Getbglist;
            //var json = @"{""page_size"":"+ pageSize + @",""page"":" + page + "}";         
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            ReportCollection.Clear();
            GetReportMessage(resStr, result);
            //  var list = JsonUtil.DeserializeFromString<List<ReportType>>(resStr);          
            if (result?.Count > 0)
            {
                ReportCollection.Clear();
                foreach (var account in result)
                {
                    account.UseName = username;
                    ReportCollection.Add(account);
                }
            }
        }
        private void OnRoneReportSearch()
        {
            GetRoneMaxPageNum();
            OnRoneFirstPage();
            List<ReportType> result = new List<ReportType>();
            //string url = string.Format("{0}?page_size={1}&page={2}", MarkInterface.Getbglist,pageSize, page);                    
            string url = string.Format("{0}?is_area=1&keyword={1}", MarkInterface.Getbglist, RoneReportSearchText);
            //string url = MarkInterface.Getbglist;
            //var json = @"{""page_size"":"+ pageSize + @",""page"":" + page + "}";         
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            RoneReportCollection.Clear();
            GetReportMessage(resStr, result);           
            //  var list = JsonUtil.DeserializeFromString<List<ReportType>>(resStr);          
            if (result?.Count > 0)
            {
                
                foreach (var account in result)
                {
                    account.UseName = username;
                    RoneReportCollection.Add(account);
                }
            }
        }
        private void OnIsOpen(ReportType _report)
        {
            string url = MarkInterface.UserReportCenterUpdatebg;

            string postStr = JsonUtil.SerializeToString(new { id = _report.ID, is_open = !_report.Is_Open });
            string resStr = HttpServiceHelper.Instance.PostRequestForData(url, postStr);

          
        }

        /// <summary>
        /// 导出报告
        /// </summary>
        /// <param name="_report"></param>
        private void ExportWord(ReportType _report)
        {
            string directPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\Report";
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + @"MMC\MMC.MSpace\1.0.0.0\Report\" + _report.Name ;
            if (!Directory.Exists(directPath))
            {
                Directory.CreateDirectory(directPath); 
            }
            if (File.Exists(filePath))
            {
                 //System.Diagnostics.Process.Start("ExpLore", filePath);
                System.Diagnostics.Process.Start("explorer.exe", filePath);
            }
            else
            {
                Task.Run(() =>
                {
                    //string downloadReport = string.Format("{0}?marker_id={1}", MarkInterface.DownloadMarksReportInf, _report.idList);
                    //HttpServiceHelper.Instance.DownloadFile(downloadReport, filePath, DownloadResult);
                    string downloadReport = string.Format("{0}?id={1}", MarkInterface.DownloadOriReport, _report.ID);

                    HttpServiceHelper.Instance.DownloadWithVersion(downloadReport, filePath, DownloadResult);
                });
                System.Diagnostics.Process.Start("explorer.exe", filePath);
            }
            
            
        }
        private void GetMaxPageNum()
        {
            string url = "";
         //   string url = MarkInterface.GetUserReportCenterListNum;
         //  var json = @"{""page_size"":" + PageSizeInt+"}";
            if (ReportSearchText == "")
            {
                url = string.Format("{0}?page_size={1}", MarkInterface.GetUserReportCenterListNum, PageSizeInt);
            }
            else
            {
                url = string.Format("{0}?page_size={1}&keyword={2}", MarkInterface.GetUserReportCenterListNum,PageSizeInt,ReportSearchText);
            }
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            using (JsonTextReader reader = new JsonTextReader(new StringReader(resStr)))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                MaxPageNum = Convert.ToInt32(obj["pageNum"]);
                PageCount = Convert.ToString(MaxPageNum);
            }
        }
        private void GetRoneMaxPageNum()
        {
            string url = "";
            //   string url = MarkInterface.GetUserReportCenterListNum;
            //  var json = @"{""page_size"":" + PageSizeInt+"}";
            if (RoneReportSearchText == "")
            {
                url = string.Format("{0}?page_size={1}&is_area=1", MarkInterface.GetUserReportCenterListNum, PageSizeInt);
            }
            else
            {
                url = string.Format("{0}?page_size={1}&is_area=1&keyword={2}", MarkInterface.GetUserReportCenterListNum, PageSizeInt, RoneReportSearchText);
            }
            string resStr = HttpServiceHelper.Instance.GetRequest(url);
            using (JsonTextReader reader = new JsonTextReader(new StringReader(resStr)))
            {
                JObject obj = (JObject)JToken.ReadFrom(reader);
                RoneMaxPageNum = Convert.ToInt32(obj["pageNum"]);
                RonePageCount = Convert.ToString(RoneMaxPageNum);
            }
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
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)//请求报头设置
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        /// <summary>
        /// 拿到解析后的json数组，并保存标注序列方便二次导出
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="ReportList"></param>
        public void GetReportMessage(string _input, List<ReportType> ReportList)
        {

            using (JsonTextReader reader = new JsonTextReader(new StringReader(_input)))
            {
                
                JArray jArray = (JArray)JToken.ReadFrom(reader);
                List<string> MarkList = new List<string>();
               
                foreach ( JObject obj in jArray)
                {
                    MarkList?.Clear();
                    //int objID = Convert.ToInt32(obj["id"]);
                    string objID = Convert.ToInt32(obj["id"]).ToString();
                    string objName = obj["name"].ToString();
                    string objCode = obj["code"].ToString();
                    bool isOpen = (bool)obj["is_open"];
                    var data = obj["data"];
                    foreach (JObject enm in data)
                    {
                        var marker_id = enm["marker_id"];
                        MarkList.Add(marker_id.ToString());
                    }
                    ReportType report = new ReportType(objID, objName, isOpen);                   
                    report.idList = string.Join(",",MarkList);
                    report.Code = objCode;
                    ReportList.Add(report);
                }             
            }
        }
    }
}
