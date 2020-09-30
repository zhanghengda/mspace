using Mmc.Mspace.Common;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
   public   class EditAccountVModel:BindableBase
    {
        private int _markerid;

        public int MarkerId
        {
            get { return _markerid; }
            set { _markerid = value; NotifyPropertyChanged("MarkerId"); }
        }


        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand ?? (saveCommand = new RelayCommand(OnSaveCommand)); }
            set { saveCommand = value; }
        }

        private RelayCommand _checkedCommand;

        public RelayCommand CheckedCommand
        {
            get { return _checkedCommand ?? (_checkedCommand = new RelayCommand(OnCheckedCommand)); }
            set { _checkedCommand = value; }
        }


        private Dictionary<string, string> _accountStatusSource;

        public Dictionary<string, string> AccountStatusSource
        {
            get { return _accountStatusSource ?? (_accountStatusSource = new Dictionary<string, string>()); }
            set { _accountStatusSource = value; base.NotifyPropertyChanged("AccountStatusSource"); }
        }



        private AccountNew _accountModel;

        public AccountNew AccountModel
        {
            get { return _accountModel; }
            set { _accountModel = value; NotifyPropertyChanged("AccountModel"); }
        }
        public EditAccountVModel()
        {

        }
        public void LoadData(int markerid)
        {
            _markerid = markerid;
            AccountModel = new AccountNew();
            GetAccountStatus();
        }
        private void GetAccountStatus()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(((int)AccountStatus.Untreated).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Untreated.ToString()));
            dic.Add(((int)AccountStatus.Processing).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Processing.ToString()));
            dic.Add(((int)AccountStatus.Processed).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Processed.ToString()));
            AccountStatusSource = dic;
            //AccountModel = MarkerHelper.Instance.GetAccountListOfMark(_markerid,10,1);
        }


        private void OnSaveCommand()
        {
            if (string.IsNullOrEmpty(AccountModel.Title))
            {
                Messages.ShowMessage("标题不允许为空！");
                return;
            }
            //if (string.IsNullOrEmpty(AccountModel.OperatorPhone))
            //{
            //    Messages.ShowMessage("电话不允许为空！");
            //    return;
            //}
            //if (!Regex.Match(CommonRegex.TelRegex, AccountModel.OperatorPhone).Success)
            //{
            //    Messages.ShowMessage("电话格式不正确！");
            //    return;
            //}
            
            var result = MarkerHelper.Instance.GetAccountByMarkerId(_markerid);
          
        }

        private void OnCheckedCommand()
        {
            //AccountModel.Img = null;
        }
    }
}
