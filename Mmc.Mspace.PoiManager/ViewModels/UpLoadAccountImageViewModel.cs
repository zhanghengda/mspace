using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
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

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class UpLoadAccountImageViewModel :  Singleton<UpLoadAccountImageViewModel>
    {
        public Action<bool> OnViewVisible;
        #region 方法
        public UpLoadAccountImageViewModel()
        {
            this.CloseCommand = new RelayCommand(OnCloseWindow);
            this.UpLoadLocalImageCommand = new RelayCommand(OnUpLoadLocalImage);
            this.UpLoadScreenShotImageCommand = new RelayCommand(OnUpLoadScreenShotImage);
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            //OnViewVisible(true);
            SystemLog.Log("-----------show3------------------");
            if (_accountView != null)
            {
                _accountView.Show();
            }
            if (_accountListView != null)
            {
                _accountListView.Show();
            }
        }

        private void OnScreenCaputred(object sender, CaptureLib.ScreenCaputredEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
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
                    _accountImageUrl = _shootImgPath;
                    AccountImageUrl(_accountImageUrl);
                    //OnViewVisible(true);
                }
           
                SystemLog.Log("-----------show2------------------");
                if (_accountView != null)
                {
                    _accountView.Show();
                }
                if (_accountListView != null)
                {
                    _accountListView.Show();
                }
            });
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        private void OnUpLoadScreenShotImage()
        {
            try
            {
                //OnViewVisible(false);
                OnCloseWindow();
                screenCaputre.ScreenCaputred -= OnScreenCaputred;
                screenCaputre.ScreenCaputreCancelled -= OnScreenCaputreCancelled;
                Thread.Sleep(500);
                screenCaputre.ScreenCaputred += OnScreenCaputred;
                screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
                screenCaputre.StartCaputre(30, lastSize);
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnUpLoadLocalImage()
        {
            try
            {
                OnCloseWindow();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "jpg文件|*.jpg|png文件|*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(ofd.FileName))
                    {
                        _accountImageUrl = ofd.FileName;
                        AccountImageUrl(_accountImageUrl);
                    }
                }
                SystemLog.Log("-----------show1------------------");
                if (_accountView != null)
                {
                    _accountView.Show();
                }
                if (_accountListView != null)
                {
                    _accountListView.Show();
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnCloseWindow()
        {
            try
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    if (_upLoadAccountImageView != null)
                    {
                        SystemLog.Log("-----------hide   _upLoadAccountImageView!=null------------------");
                        _upLoadAccountImageView.Hide();
                    }
                    SystemLog.Log("-----------hide ------------------");
                    if (_accountView != null)
                    {
                        SystemLog.Log("-----------hide _accountView != null------------------");
                        _accountView.Hide();
                    }
                    if (_accountListView != null)
                    {
                        SystemLog.Log("-----------hide _accountListView != null------------------");
                        _accountListView.Hide();
                    }
                });
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void ShowView(AccountView _accountView,AccountListView  _accountListView)
        {
            try
            {
                if (_upLoadAccountImageView == null)
                {
                    _upLoadAccountImageView = new UpLoadAccountImageView();
                }
                this._accountView = _accountView;
                this._accountListView = _accountListView;
                _upLoadAccountImageView.DataContext = this;
                _upLoadAccountImageView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                _upLoadAccountImageView.Owner = _accountView;
                _upLoadAccountImageView.Show();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        #endregion

        #region 命令、属性
        private AccountView _accountView { get; set; }
        private AccountListView _accountListView { get; set; }
        public Action<string> AccountImageUrl;
        private UpLoadAccountImageView _upLoadAccountImageView;
        private readonly CaptureLib.ScreenCaputre screenCaputre = new CaptureLib.ScreenCaputre();
        private Size? lastSize;
        /*出图绝对路径*/
        private string _shootImgPath = @"C:\Users\Admin\AppData\Local\Temp\MSpace\shootImage\temp.bmp";

        private string _accountImageUrl = string.Empty;
        public ICommand CloseCommand { get; set; }
        public ICommand UpLoadLocalImageCommand { get; set; }
        public ICommand UpLoadScreenShotImageCommand { get; set; }
        
        #endregion
    }
}
