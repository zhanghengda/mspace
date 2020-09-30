using Microsoft.Win32;
using Mmc.Mspace.Common;
using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class AddAccountVModel : BindableBase
    {
        public Action AddAccount;
        public Action Close;
        public Action ShowWindow;
        public Action CloseAccount;
        public Action ShowAccount;
        public Action<int> Updatedata;
        private int _markerid;
        private readonly CaptureLib.ScreenCaputre screenCaputre = new CaptureLib.ScreenCaputre();
        private Size? lastSize;
        public int MarkerId
        {
            get { return _markerid; }
            set { _markerid = value;NotifyPropertyChanged("MarkerId"); }
        }

        private RelayCommand _closeCommand;

        public RelayCommand CloseCommand
        {
            get { return _closeCommand??(_closeCommand=new RelayCommand(()=> { Close(); })); }
            set { _closeCommand = value; }
        }


        private RelayCommand  createCommand;

        public RelayCommand CreateCommand
        {
            get { return createCommand??(createCommand=new RelayCommand(OnCreateCommand)); }
            set { createCommand = value; }
        }

        private RelayCommand _checkedCommand;

        public RelayCommand CheckedCommand
        {
            get { return _checkedCommand ?? (_checkedCommand = new RelayCommand(OnCheckedCommand)); }
            set { _checkedCommand = value; }
        }

        private RelayCommand _screenCaputredCommand;

        public RelayCommand ScreenCaputredCommand
        {
            get { return _screenCaputredCommand??(_screenCaputredCommand=new RelayCommand (OnScreenCaputredCommand)); }
            set { _screenCaputredCommand = value; }
        }   

        private RelayCommand _uploadCommand;

        public RelayCommand UploadCommand
        {
            get { return _uploadCommand??(_uploadCommand=new RelayCommand (OnUploadCommand)); }
            set { _uploadCommand = value; }
        }


        private KeyValuePair<string,string> _selectStatus;

        public KeyValuePair<string,string> SelectStatus
        {
            get { return _selectStatus; }
            set { _selectStatus = value; }
        }


        private Dictionary<string,string> _accountStatusSource;

        public Dictionary<string,string> AccountStatusSource
        {
            get { return _accountStatusSource ?? (_accountStatusSource = new Dictionary<string, string> ()); }
            set { _accountStatusSource = value;base.NotifyPropertyChanged("AccountStatusSource"); }
        }



        private AccountNew accountModel;

        public AccountNew AccountModel
        {
            get { return accountModel; }
            set { accountModel = value;NotifyPropertyChanged("AccountModel"); }
        }

        public AddAccountVModel()
        {
            screenCaputre.ScreenCaputred -= OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled -= OnScreenCaputreCancelled;
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
        }

        public void LoadData(int markerid)
        {
            _markerid = markerid;
            AccountModel = new AccountNew();
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            ShowWindow();
            ShowAccount();
        }
        /*出图绝对路径*/
        private string _shootImgPath = @"C:\Users\Admin\AppData\Local\Temp\MSpace\shootImage\temp.bmp";

        private void OnScreenCaputred(object sender, CaptureLib.ScreenCaputredEventArgs e)
        {
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);
            var bmp = e.Bmp;
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            DirectoryInfo info = new DirectoryInfo(temp);
            string MSpace = info.ToString() + "\\MSpace";
            if (!Directory.Exists(MSpace))
            {
                Directory.CreateDirectory(MSpace);
            }
            string shootImage = MSpace + "\\shootImage";
            if (!Directory.Exists(shootImage))
            {
                Directory.CreateDirectory(shootImage);
            }
            _shootImgPath = shootImage + "\\" + GetTimeStamp() + ".png";
            var result = ImageHelper.SaveImage(_shootImgPath, bmp);
            if (result)
            {
                AccountModel.ImgPathList[0] = _shootImgPath;
                Clipboard.SetImage(bmp);
            }
            ShowWindow();
            ShowAccount();
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

        private void OnUploadCommand()
        {
            OpenFileDialog _fdialog = new OpenFileDialog()
            {
                Filter = "Bitmap files (*.jpg)|*.jpg",
            };
            _fdialog.ShowDialog();
        }

        private void GetAccountStatus()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(((int)AccountStatus.Untreated).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Untreated.ToString()));
            dic.Add(((int)AccountStatus.Processing).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Processing.ToString()));
            dic.Add(((int)AccountStatus.Processed).ToString(), Helpers.ResourceHelper.FindKey(AccountStatus.Processed.ToString()));
            AccountStatusSource = dic;
            SelectStatus = dic.First();
        }


        private void OnCreateCommand()
        {
            if (string.IsNullOrEmpty(accountModel.Title))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Titlenotnull"));
                return;
            }
            //if (string.IsNullOrEmpty(accountModel.OperatorPhone))
            //{
            //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Phonenotnull"));
            //    return;
            //}
            //Regex regex = new Regex(CommonRegex.TelRegex);
            //if (!regex.IsMatch(accountModel.OperatorPhone))
            //{
            //    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Phonefailed"));
            //    return;
            //}

            //1:未处理  2：处理中  3：已处理
            //accountModel.Status = SelectStatus.Key;
            //accountModel.MarkerId = _markerid;
            //if (!string.IsNullOrEmpty(accountModel.Img))
            //{
            //    if (!File.Exists(accountModel.Img))
            //    {
            //        Messages.ShowMessage("图片路径有误！");
            //        return;
            //    }
            //    AccountModel.Img = MarkerHelper.Instance.updateCaptureImg(_shootImgPath);
            //}
            var result = MarkerHelper.Instance.AddAccount(accountModel);
            if (result)
            {
                if (Close != null)
                    Close();
                //Updatedata(accountModel.MarkerId);
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
            }
            else
            {
                //AccountModel.Img = _shootImgPath;
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savefailed"));
            }

            if (AddAccount != null)
            {
                AddAccount();
            }
        }

        private void OnScreenCaputredCommand()
        {
            Close();
            CloseAccount();
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }
        private void OnCheckedCommand()
        {
            //AccountModel.Img = null;
        }
    }
}
