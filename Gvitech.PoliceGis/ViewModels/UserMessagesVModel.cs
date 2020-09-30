using Mmc.Wpf.Commands;
using Mmc.Mspace.Common.Cache;
using MMC.MSpace.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMC.MSpace.ViewModels
{
    public class UserMessagesVModel
    {
        UserMessagesView userMessageView = new UserMessagesView();
        public ICommand ReportCenterCmd { get; set; }

        private RelayCommand _reportSettingCommand;
        public RelayCommand ReportSettingCommand
        {
            get { return _reportSettingCommand ?? (_reportSettingCommand = new RelayCommand(OnReportSettingCommand)); }
            set { _reportSettingCommand = value; }
        } 
        public UserMessagesVModel()
        {
            userMessageView.DataContext = this;
            userMessageView.WindowStartupLocation = WindowStartupLocation.Manual;
            userMessageView.Owner = Application.Current.MainWindow;
            //userMessageView.Left = 500;
            //userMessageView.Topmost = true;
            //userMessageView.Top = 40;
            //userMessageView.Left = SystemParameters.WorkArea.Width - 260;
            GetWindowPosition();

            this.ReportCenterCmd = new RelayCommand(OpenUserCenter);
        }

        public void UserMessageShow()
        {
            GetWindowPosition();
            userMessageView.Show();
        }

        public void UserMessageHide()
        {
            userMessageView.Hide();
        }
        UserCenterVModel userCenterVModel = null;
        public void OpenUserCenter()
        {
            if (userCenterVModel == null)
            {
                userCenterVModel = new UserCenterVModel(CacheData.UserInfo.username);
            }
            userCenterVModel.OpenView();
            UserMessageHide();
        }

        private void OnReportSettingCommand()
        {
            ReportSettingView reportSettingView = new ReportSettingView();
            reportSettingView.Show();
        }

        public void GetWindowPosition()
        {
            userMessageView.Top = Application.Current.MainWindow.Top + 40;
            userMessageView.Left = Application.Current.MainWindow.Left + Application.Current.MainWindow.Width - 260;
        }
    }
}