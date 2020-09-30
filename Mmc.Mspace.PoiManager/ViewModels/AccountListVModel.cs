using Microsoft.Office.Interop.Excel;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstDataInterface;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;


namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class AccountListVModel: CheckedToolItemModel
    {
        private int _currMarkerId;

        public override void Initialize()
        {
            this.AddNewAccountCmd = new RelayCommand(OnAddNewAccount);
            this.ExportAcountCmd = new RelayCommand(OnExportAcount);
            this.LastPageCmd = new RelayCommand(OnLastPage);
            this.NextPageCmd  = new RelayCommand(OnNextPage);
            this.EndPageCmd = new RelayCommand(OnEndPage);
            this.FirstPageCmd = new RelayCommand(OnFirstPage);
            this.CloseWindowCmd = new RelayCommand(OnCloseWindow);
            this.DeleteAccountCmd = new RelayCommand<AccountNew>((AccountNew)=>OnDeleteAccount(AccountNew));
            this.LeftDoubleClickCmd = new RelayCommand<AccountNew>((AccountNew) => LeftDoubleClick(AccountNew));
            this.EditAccountCmd= new RelayCommand<AccountNew>((AccountNew) => OnEditAccount(AccountNew));
            MarkerHelper.Instance.AccountCount += OnAccountCount;

        }

        private void OnEditAccount(AccountNew accountNew)
        {
            try
            {
                _accountViewModel = new AccountViewModel(_currMarkerId, true);
                AccountListIsEnable = Visibility.Visible;
                _accountViewModel.ShowEditView(accountNew, _accountListView);

                _accountViewModel.OnRefreshList -= RefreshListCallback;
                _accountViewModel.OnRefreshList += RefreshListCallback;
                _accountViewModel.CloseAccountWindow += accountViewModel_CloseAccountWindow;

                _accountViewModel.OnViewVisible += IsViewVisible;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void IsViewVisible(bool isVisible)
        {
            if (isVisible)
            {
                _accountListView?.Show();
            }
            else
            {
                _accountListView?.Hide();
            }
        }

        public void OnCloseWindow()
        {
            try
            {
                if (_accountListView != null)
                {
                    _accountListView.Close();
                    _accountListView = null;
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void LeftDoubleClick(AccountNew accountNew)
        {
            //try
            //{
            //    if(string.IsNullOrEmpty(accountNew.ImgNum))
            //    {
            //        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Account_ImageIsNull"));
            //        return;
            //    }
            //    if(_imageShowViewModel==null)
            //    {
            //        _imageShowViewModel = new ImageShowViewModel();
            //    }
            //    _imageShowViewModel.ImageShowWindow(accountNew);
            //}
            //catch(Exception e)
            //{
            //    SystemLog.Log(e);
            //}
        }

        private void OnDeleteAccount(AccountNew accountNew)
        {
            try
            {
                if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Account_MesTip"), Helpers.ResourceHelper.FindKey("Account_ConfirmDelete") + accountNew.Title+ "?"))
                {
                    var result = MarkerHelper.Instance.DeleteAccount(accountNew.Id);

                    if (result)
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Deletesuccess"));
                    }
                    else
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Deletefailed"));
                    }

                    int markId = Convert.ToInt32(PoiId);
                    if (PageCount == 0)
                    {
                        PageNum = 1;
                    }
                    List<AccountNew> accoutList = new List<AccountNew>();
                    accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                    if (accoutList.Count == 0 && PageNum > 1 && PageNum >= PageCount)
                    {
                        PageNum--;
                        accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                    }
                    AccountCollection.Clear();
                    foreach (var item in accoutList)
                    {
                        AccountCollection.Add(item);
                    }
                }
            }catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }


        /// <summary>
        /// 首页
        /// </summary>
        private void OnFirstPage()
        {
            try
            {
                int markId = Convert.ToInt32(PoiId);
                _currMarkerId = markId;

                PageNum = 1;
                var accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                AccountCollection.Clear();
                foreach (var item in accoutList)
                {
                    AccountCollection.Add(item);
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }
        /// <summary>
        /// 尾页
        /// </summary>
        private void OnEndPage()
        {
            try
            {
                int markId = Convert.ToInt32(PoiId);
                PageNum = PageCount;
                if(PageCount == 0)
                {
                    PageNum = 1;
                }
                    var accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                AccountCollection.Clear();
                foreach (var item in accoutList)
                {
                    AccountCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void OnNextPage()
        {
            try
            {
                int markId = Convert.ToInt32(PoiId);
                PageNum++;
                var accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                AccountCollection.Clear();
                foreach (var item in accoutList)
                {
                    AccountCollection.Add(item);
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void OnLastPage()
        {
            try
            {
                int markId = Convert.ToInt32(PoiId);
                PageNum--;
                var accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, PageNum);
                AccountCollection.Clear();
                foreach (var item in accoutList)
                {
                    AccountCollection.Add(item);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnAddNewAccount()
        {
            try
            {
                _accountViewModel = new AccountViewModel(_currMarkerId, false);
                AddAccountWindowFlag = true;
                AccountListIsEnable = Visibility.Visible;
                _accountViewModel.ShowView(_accountListView);
                _accountViewModel.OnRefreshList -= RefreshListCallback;
                _accountViewModel.OnRefreshList += RefreshListCallback;
                _accountViewModel.CloseAccountWindow += accountViewModel_CloseAccountWindow;
                _accountViewModel.OnViewVisible += IsViewVisible;
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void accountViewModel_CloseAccountWindow(bool obj)
        {
            AddAccountWindowFlag = false;
            AccountListIsEnable = Visibility.Collapsed;
        }

        private void RefreshListCallback(AccountNew obj, bool isEdit)
        {
            
            if (isEdit)
            {
                this.OnCurrentPage(obj);
            }
            else
            {
                this.OnFirstPage();
            }
        }

        private void OnCurrentPage(AccountNew obj)
        {
            this.RefleshCurrentList(obj,_curPageNum);
        }

        private void RefleshCurrentList(AccountNew obj,int pageNum)
        {
            try
            {
                if (pageNum <= 0) pageNum = 1;
                int markId = Convert.ToInt32(PoiId);
                var accoutList = MarkerHelper.Instance.GetAccountListOfMark(markId, pageSize, pageNum);
                AccountCollection.Clear();
                AccountCollection = new ObservableCollection<AccountNew>(accoutList);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnAccountCount(int obj)
        {
            try
            {
                PageCount = (obj % pageSize) > 0 ? (obj / pageSize) + 1 : obj / pageSize;
                if (PageNum == PageCount || obj == 0)
                {
                    NextPageBtnEnable = false;
                }
                else
                {
                    NextPageBtnEnable = true;
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        /// <summary>
        /// 添加台账后获取数据
        /// </summary>
        private void OnAddedAccount()
        {
            try
            {
                OnEndPage();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private PropertyInfo[] GetPropertyInfoArray()
        {
            PropertyInfo[] props = null;
            try
            {
                Type type = typeof(AccountNew);
                object obj = Activator.CreateInstance(type);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            catch (Exception ex)
            { }
            return props;
        }

        private void OnExportAcount()
        {
            int markId = Convert.ToInt32(PoiId);
            SaveFileDialog saveDlg = new SaveFileDialog();
            string mydocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveDlg.InitialDirectory = mydocPath;
            saveDlg.Filter = FileFilterStrings.EXCEL;
            saveDlg.FilterIndex = 2;
            saveDlg.FileName = GetTimeStamp();
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                Task.Run(() =>
                {
                    string downloadReport = string.Format("{0}?marker_id={1}", MarkInterface.DownloadExcelAccountReportInf, markId);
                    HttpServiceHelper.Instance.DownloadFile(downloadReport, saveDlg.FileName, DownloadResult);
                });
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

        public void DownloadResult(bool result)
        {
            if (result)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ReportSuccess"));
                return;
            }
            Messages.ShowMessage(Helpers.ResourceHelper.FindKey("AccountReportFaild"));
        }


        /// <summary>
        /// 导出到账数据到Excel
        /// </summary>

        private void FinishProcess()
        {
            ServiceManager.GetService<IShellService>(null).ProgressView.Dispatcher.Invoke(() =>
            {
                progressView.ViewModel.ProgressValue = string.Empty;
                ServiceManager.GetService<IShellService>(null).ProgressView.ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
            });
        }

        public void ShowWindow(string poiId/*, string geotype*/)
        {
            try
            {
                if(_accountViewModel!=null)
                {
                    _accountViewModel.CloseView();
                }
                if (_accountListView == null)
                {
                    _accountListView = new AccountListView();
                }
                AccountListIsEnable = Visibility.Collapsed;
                PoiId = poiId;
                _currMarkerId = Convert.ToInt32(poiId);
                _accountListView.DataContext = this;
                _accountListView.Owner = System.Windows.Application.Current.MainWindow;
                _accountListView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                _accountListView.Show();
                
                OnFirstPage();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }
        #region 属性 命令
        private AddAccountView _addAccountView;
        private AddAccountVModel _addAccountVModel;
        private AccountViewModel _accountViewModel;
        
        private AccountListView _accountListView { get; set; }
        private ImageShowViewModel _imageShowViewModel;

        private int _curPageNum;
        private bool AddAccountWindowFlag=false;
        public readonly int pageSize = 10;
        private readonly ExportProgressView progressView = new ExportProgressView();
        public ICommand AddNewAccountCmd { get; set; }
        public ICommand ExportAcountCmd { get; set; }
        public ICommand LastPageCmd { get; set; }
        public ICommand NextPageCmd { get; set; }
        public ICommand EndPageCmd { get; set; }
        public ICommand FirstPageCmd { get; set; }
        public ICommand DeleteAccountCmd { get; set; }
        public ICommand LeftDoubleClickCmd { get; set; }
        public ICommand CloseWindowCmd { get; set; }

        public ICommand EditAccountCmd { get; set; }
        

        private string _poiId;
        public string PoiId
        {
            get { return _poiId; }
            set { _poiId = value; NotifyPropertyChanged("PoiId"); }
        }

        //private string _geoType;
        //public string GeoType
        //{
        //    get { return _geoType; }
        //    set { _geoType = value; NotifyPropertyChanged("GeoType"); }
        //}

        private int _pageCount=1;
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                if(_pageCount==0)
                {
                    _pageCount = 1;
                }
                NotifyPropertyChanged("PageCount");
            }
        }

        private int _pageNum=1;
        public int PageNum
        {
            get { return _pageNum; }
            set
            {
                _pageNum = value;
               
                
                if (PageNum == 1)
                {
                    LastPageBtnEnable = false;
                }if (PageNum > 1)
                {
                    LastPageBtnEnable = true;
                }
                if(PageNum==PageCount)
                {
                    NextPageBtnEnable = false;
                }
                NotifyPropertyChanged("PageNum");
            }
        }
        private bool _lastPageBtnEnable;
        public bool LastPageBtnEnable
        {
            get { return _lastPageBtnEnable; }
            set { _lastPageBtnEnable = value; NotifyPropertyChanged("LastPageBtnEnable"); }
        }

        private bool _nextPageBtnEnable= true;
        public bool NextPageBtnEnable
        {
            get { return _nextPageBtnEnable; }
            set { _nextPageBtnEnable = value; NotifyPropertyChanged("NextPageBtnEnable"); }
        }

        private ObservableCollection<AccountNew> _accountCollection = new ObservableCollection<AccountNew>();
        public ObservableCollection<AccountNew> AccountCollection
        {
            get { return _accountCollection; }
            set
            {
                _accountCollection = value;
                NotifyPropertyChanged("AccountCollection");
            }
        }
        private Visibility _accountListIsEnable = Visibility.Collapsed;
        public Visibility AccountListIsEnable
        {
            get { return _accountListIsEnable; }
            set { _accountListIsEnable = value; NotifyPropertyChanged("AccountListIsEnable"); }
        }

        
        #endregion
    }
}
