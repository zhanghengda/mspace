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
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Services.HttpService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        #region varies

        public Action<bool> OnViewVisible;

        public Action<AccountNew,bool> OnRefreshList;

        public Action<bool> CloseAccountWindow;

        private bool _isEdit;

        private UpLoadAccountImageViewModel _upLoadAccountImageViewModel;
        #endregion
        private AccountView _accountView;
        private readonly ExportProgressView progressView;
        private AccountListView _accountListView;

        #region binging varies

        private ObservableCollection<ImageAccountModel> _imgItemList = new ObservableCollection<ImageAccountModel>();
        public ObservableCollection<ImageAccountModel> ImgItemList
        {
            get { return _imgItemList; }
            set
            {
                _imgItemList = value;
                OnPropertyChanged("ImgItemList");
            }
        }

        private AccountNew _account;
        public AccountNew Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged("Account");
            }
        }

        private string _viewTitle;
        public string ViewTitle
        {
            get => _viewTitle;
            set
            {
                _viewTitle = value;
                OnPropertyChanged("ViewTitle");
            }
        }

        private string _accountTitle;
        public string AccountTitle
        {
            get => _accountTitle;
            set
            {
                _accountTitle = value;
                OnPropertyChanged("AccountTitle");
            }
        }
        private int _accountId;
        public int AccountId
        {
            get => _accountId;
            set
            {
                _accountId = value;
                OnPropertyChanged("AccountId");
            }
        }
        private bool _isShowReport;
        public bool IsShowReport
        {
            get => _isShowReport;
            set
            {
                _isShowReport = value;
                OnPropertyChanged("IsShowReport");
            }
        }
        private string _accountProblemTime;
        public string AccountProblemTime
        {
            get { return _accountProblemTime; }
            set
            {
                _accountProblemTime = value;
                if(!string.IsNullOrEmpty(_accountProblemTime)&& _accountProblemTime.Contains(":"))
                {
                    _accountProblemTime= _accountProblemTime.Substring(0, _accountProblemTime.IndexOf(":") - 1);
                }
                OnPropertyChanged("AccountProblemTime");
            }
        }


        [XmlIgnore] public ICommand CancelCommand { get; set; }
        [XmlIgnore] public ICommand SaveCommand { get; set; }
        [XmlIgnore]
        public ICommand ImportAccountImageCmd { get; set; }
        [XmlIgnore]
        public ICommand DeleteAccountImageCmd { get; set; }
        [XmlIgnore]
        public ICommand VedioPreviewCommand { get; set; }
        
        //[XmlIgnore] public ICommand DisplayImgCommand { get; set; }
        #endregion

        public AccountViewModel(int markerId, bool isEdit = false)
        {
            progressView = new ExportProgressView();
            _accountView = new AccountView();
            _account = new AccountNew()
            {
                MarkerId = markerId
            };
            _isShowReport = true;
            _isEdit = isEdit;

            this.CancelCommand = new RelayCommand(CloseView);
            this.SaveCommand = new RelayCommand(SaveAccoutData);
            this.ImportAccountImageCmd = new RelayCommand<ImageAccountModel>((imageAccountModel)=>OnImportAccountImage(imageAccountModel));
            this.DeleteAccountImageCmd = new RelayCommand<ImageAccountModel>((imageAccountModel) => OnDeleteAccountImage(imageAccountModel));
            this.VedioPreviewCommand = new RelayCommand(OnVedioPreview);
            //this.DisplayImgCommand = new RelayCommand(ShowImage);
            if (_isEdit)
            {
                this.ViewTitle = "编辑台账";
            }
            else
            {
                this.ViewTitle = "新增台账";
            }
            System.Windows.Application.Current.MainWindow.StateChanged += MainWindow_StateChanged;

        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if(Application.Current.MainWindow.WindowState ==WindowState.Minimized )
            {
                if(_accountView!=null)
                {
                    _accountView.Hide();
                }
            }
            if(Application.Current.MainWindow.WindowState == WindowState.Maximized|| Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                if (_accountView != null)
                {
                    _accountView.Show();
                }
            }
        }

        private void OnVedioPreview()
        {
            if (File.Exists(Account.Video))
            {
                VideoPlayViewVMdel videoPlay = new VideoPlayViewVMdel();
                videoPlay.ShowVideoView(Account.Video);
            }else
            {
                Messages.ShowMessage("视频文件不存在！");
            }
        }

        private void OnDeleteAccountImage(ImageAccountModel imageAccountModel)
        {
            try
            {
                if(ImgItemList.Contains(imageAccountModel))
                {
                    ImgItemList.Remove(imageAccountModel);
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnImportAccountImage(ImageAccountModel imageAccountModel)
        {
            try
            {
                if(imageAccountModel.IsContainsImage)
                {
                    ImageShowViewModel.Instance.AccountImageShowWindow(_accountView,ImgItemList, imageAccountModel);
                }
                else
                {
                    if (_upLoadAccountImageViewModel == null)
                    {
                        _upLoadAccountImageViewModel = new UpLoadAccountImageViewModel();
                    }
                    _upLoadAccountImageViewModel.ShowView(_accountView, this._accountListView);
                    _upLoadAccountImageViewModel.AccountImageUrl -= _upLoadAccountImageViewModel_AccountImageUrl;
                    _upLoadAccountImageViewModel.AccountImageUrl += _upLoadAccountImageViewModel_AccountImageUrl;
                    _upLoadAccountImageViewModel.OnViewVisible += IsViewVisible;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void _upLoadAccountImageViewModel_AccountImageUrl(string obj)
        {
            try
            {

                _upLoadAccountImageViewModel.AccountImageUrl -= _upLoadAccountImageViewModel_AccountImageUrl;
                ImageAccountModel imageAccountModel = new ImageAccountModel();
                imageAccountModel.ImageUrl = obj;
                imageAccountModel.IsContainsImage = true;
                ImgItemList.Insert(0,imageAccountModel);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void ShowImage()
        {
            //throw new NotImplementedException();
        }

        private void SaveAccoutData()
        {
            if (string.IsNullOrEmpty(AccountTitle))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("AccountTitleNotNull"));
                return;
            }
            SubmitAccountData();
        }

        private void SubmitAccountData()
        {
            try
            {
                
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ServiceManager.GetService<IShellService>(null).ProgressView.Content = this.progressView;
                    this.progressView.ViewModel.ProgressValue =
                        string.Format(Helpers.ResourceHelper.FindKey("Submitting"));
                });
                Account.ImgPathList.Clear();
                foreach (var item in ImgItemList)
                {
                    if (item.ImageUrl.Contains(
                        "pack://application:,,,/Mmc.Mspace.Theme;component/Images/Account/Account_Addphoto.png"))
                        continue;
                    if (item.ImageUrl.Contains(WebConfig.MspaceHostUrl)) Account.ImgPathList.Add(item.ImageUrl);
                    else Account.ImgPathList.Add(MarkerHelper.Instance.updateCaptureImg(item.ImageUrl));
                }

                Account.Id = AccountId;
                Account.Title = AccountTitle;
                Account.IsShowInReport = IsShowReport ? "显示" : "不显示";
                Account.ProblemTime = AccountProblemTime;
                Account.ImgNum = Account.ImgPathList.Count;
                if (_isEdit)
                {
                    if (MarkerHelper.Instance.UpdateAccountListOfMark(Account.Id, Account))
                    {
                        RefreshData(Account);
                    }
                }
                else
                {
                    if (MarkerHelper.Instance.AddAccount(Account))
                    {
                        RefreshData(Account);
                    }
                }

                //this.CloseView();
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
                //IsOkBtnEnable = true;
                this.CloseView();
            }
        }


        private void FinishProcess()
        {
            progressView.ViewModel.ProgressValue = string.Empty;
            ServiceManager.GetService<IShellService>(null).ProgressView
                .ClearValue(System.Windows.Controls.ContentControl.ContentProperty);
        }

        private void RefreshData(AccountNew accout)
        {
            if (accout == null) return;
            List<string> temp = new List<string>();
            if (accout.ImgPathList.Count > 0)
            {
                foreach (var path in accout.ImgPathList)
                {
                    if ((bool)path.ToLower().Contains(WebConfig.MspaceHostUrl))
                        temp.Add(path);
                    else
                        temp.Add(string.Format("{0}/resource{1}", WebConfig.MspaceHostUrl, path));
                }
                accout.ImgPathList = new ObservableCollection<string>(temp);
            }

            OnRefreshList?.Invoke(accout, _isEdit);
            
        }


        public void ShowEditView(AccountNew accountNew, AccountListView _accountListView)
        {
            if (_accountView == null) _accountView = new AccountView();
            this._accountListView = _accountListView;
            _accountView.DataContext = this;
            _accountView.Owner = _accountListView;
            _accountView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _accountView.Show();

            foreach (var item in accountNew.ImgPathList)
            {
                ImageAccountModel imageAccount = new ImageAccountModel();
                imageAccount.ImageUrl = item;
                imageAccount.IsContainsImage = true;
                ImgItemList.Add(imageAccount);
            }
            AccountId = accountNew.Id;
            IsShowReport = accountNew.IsShowInReport.Equals("显示") ? true : false;
            AccountProblemTime = accountNew.ProblemTime.ToString(); ;
            AccountTitle = accountNew.Title;
            ImageAccountModel imageAccountModel = new ImageAccountModel();
            imageAccountModel.ImageUrl = "pack://application:,,,/Mmc.Mspace.Theme;component/Images/Account/Account_Addphoto.png";
            imageAccountModel.IsContainsImage = false;
            imageAccountModel.ImageCloseBtnVisibility = Visibility.Collapsed;
            ImgItemList.Add(imageAccountModel);
        }

        public void ShowView(AccountListView _accountListView)
        {
            if (_accountView == null) _accountView = new AccountView();
            _accountView.DataContext = this;
            this._accountListView = _accountListView;
            _accountView.Owner = _accountListView;
            _accountView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _accountView.Show();
            if(ImgItemList.Count==0)
            {
                ImageAccountModel imageAccountModel = new ImageAccountModel();
                imageAccountModel.ImageUrl = "pack://application:,,,/Mmc.Mspace.Theme;component/Images/Account/Account_Addphoto.png";
                imageAccountModel.IsContainsImage = false;
                imageAccountModel.ImageCloseBtnVisibility = Visibility.Collapsed;
                ImgItemList.Add(imageAccountModel);
            }
        }


        private void IsViewVisible(bool isVisible)
        {
            //if (isVisible)
            //{
            //    _accountView?.Show();
            //}
            //else
            //{
            //    _accountView?.Hide();
            //}
            //OnViewVisible?.Invoke(isVisible);
        }

        public void CloseView()
        {
            _accountView?.Close();
            _accountView = null;
            CloseAccountWindow(true);
        }
    }
}