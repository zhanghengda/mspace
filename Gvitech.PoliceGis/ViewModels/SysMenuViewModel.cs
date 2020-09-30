using Mmc.Mspace.Common.Dto;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using MMC.MSpace.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Mmc.Mspace.Common.ShellService;

namespace MMC.MSpace.ViewModels
{
    public class SysMenuViewModel : BindableBase
    {
        List<string> _noticeList;
        public SysMenuViewModel()
        {
            if (_noticeList == null) _noticeList = new List<string>();
            _noticeList = DataBaseService.Instance.GetLayersOutCycle();
            //_noticeList = test();
            if (_noticeList != null && _noticeList.Count > 0) IsNoticeVisiable = Visibility.Visible;
            else IsNoticeVisiable = Visibility.Hidden;
           
        }

        private List<string> test()
        {
            return new List<string>()
            {
                "101",
                "102",
                "103",
                "104",
                "105",
                "106",
                "107",
                "108",
            };
        }

        private NoticeVModel _noticeVModel;
        private Visibility _isNoticeVisiable;
        public Visibility IsNoticeVisiable
        {
            get { return _isNoticeVisiable; }
            set { _isNoticeVisiable = value; base.NotifyPropertyChanged("IsNoticeVisiable"); }
        }

        private RelayCommand _checkNoticeCommand;
        public RelayCommand CheckNoticeCommand
        {
            get { return _checkNoticeCommand ?? (_checkNoticeCommand = new RelayCommand(CheckNoticeMessage)); }
            set { _checkNoticeCommand = value; }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; base.NotifyPropertyChanged("UserName"); }
        }


        private RelayCommand _SelMenuCommand;
        public RelayCommand SelMenuCommand { get { return _SelMenuCommand ?? (_SelMenuCommand = new RelayCommand(DealWithSelectedMenu)); } }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(CloseWindow)); }
            set { _closeCommand = value; }
        }
        private RelayCommand _minCommand;

        public RelayCommand MinCommand
        {
            get { return _minCommand ?? (_minCommand = new RelayCommand(MinWindow)); }
            set { _minCommand = value; }
        }

        private RelayCommand _setCommand;

        public RelayCommand SetCommand
        {
            get { return _setCommand ?? (_setCommand = new RelayCommand(ShowSetView)); }
            set { _setCommand = value; }
        }
        private void DealWithSelectedMenu(object para)
        {
            MmcComboboxData Data = (MmcComboboxData)para;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("navid", Data.NavId);
            MethodInfo method = this.GetType().GetMethod(Data.NavMethodName);
            if (method != null)
            {
                method.Invoke(this, new object[] { dic });
            }
        }


        public void CloseWindow()
        {
            if (Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("Close"), Helpers.ResourceHelper.FindKey("ConfirmClose")))
            {
                try
                {
                    Application.Current.Shutdown(0);
                }
                catch(Exception e)
                {
                    SystemLog.Log(e);
                }
            }
        }

        public void MinWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }


        public void ShowSetView()
        {
            var view = new SettingView();
            view.Owner = Application.Current.MainWindow;
            view.ShowDialog();
        }

        public void CheckNoticeMessage()
        {
            if(_noticeVModel==null)  _noticeVModel = new NoticeVModel();
            _noticeVModel.ShowView(_noticeList);
            _noticeVModel.IsNoticeClear -= OnNoticeClear;
            _noticeVModel.IsNoticeClear += OnNoticeClear;
        }

        private void OnNoticeClear(bool isClear)
        {
            if (isClear)
            {
                IsNoticeVisiable = Visibility.Hidden;
            }
            else
            {
                IsNoticeVisiable = Visibility.Visible;
            }
        }
    }
}
