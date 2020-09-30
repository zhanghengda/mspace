using Mmc.Mspace.PoiManagerModule.Dto;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class ImageShowViewModel: Singleton<ImageShowViewModel>,INotifyPropertyChanged
    {
        public ImageShowViewModel()
        {
            this.CloseAccountImageShowCmd = new RelayCommand(OnCloseAccountImageShow);
            this.NextAccountImageCmd = new RelayCommand(OnNextAccountImage);
            this.PreAccountImageCmd = new RelayCommand(OnPreAccountImage);
            
        }

        private void OnPreAccountImage()
        {
            try
            {
                if (ImgItemShowList.Count-1 > 1)
                {
                    ImageAccountModel imageAccountModel = ImgItemShowList.FirstOrDefault(t => t.ImageUrl == ImageUrl);
                    CurrentImageIndex = ImgItemShowList.IndexOf(imageAccountModel);
                    if (CurrentImageIndex == 0)
                    {
                        CurrentImageIndex = ImgItemShowList.Count - 2;
                    }else
                    {
                        CurrentImageIndex--;
                    }
                    ImageUrl = ImgItemShowList[CurrentImageIndex].ImageUrl;
                }
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnNextAccountImage()
        {
            try
            {
                if(ImgItemShowList.Count-1>1)
                {
                    ImageAccountModel imageAccountModel=ImgItemShowList.FirstOrDefault(t=>t.ImageUrl == ImageUrl);
                    CurrentImageIndex = ImgItemShowList.IndexOf(imageAccountModel);
                    if(CurrentImageIndex== ImgItemShowList.Count-2)
                    {
                        CurrentImageIndex = 0;
                    }else
                    {
                        CurrentImageIndex++;
                    }
                    ImageUrl = ImgItemShowList[CurrentImageIndex].ImageUrl;
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void OnCloseAccountImageShow()
        {
            if (_imageShowView != null)
            {
                _imageShowView.Hide();
            }
        }

        public void ImageShowWindow(AccountModel accountModel)
        {
            if(_imageShowView==null)
            {
                _imageShowView = new ImageShowView();
            }
            ImageUrl = accountModel.Img ;
            AccountName = accountModel.Title;
            _imageShowView.DataContext = this;
            _imageShowView.Owner = System.Windows.Application.Current.MainWindow;
            _imageShowView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            _imageShowView.Show();
        }

        public void AccountImageShowWindow(AccountView _accountView,ObservableCollection<ImageAccountModel> ImgItemList, ImageAccountModel currentImage)
        {
            if (_imageShowView == null)
            {
                _imageShowView = new ImageShowView();
            }
            ImgItemShowList = new ObservableCollection<ImageAccountModel>(ImgItemList);
            ImageUrl = currentImage.ImageUrl;
            //AccountName = accountModel.Title;
            _imageShowView.DataContext = this;
            _imageShowView.Owner = _accountView;
            _imageShowView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            _imageShowView.Show();
        }


        #region 命令属性
        public ICommand CloseAccountImageShowCmd { get; set; }
        public ICommand PreAccountImageCmd { get; set; }
        public ICommand NextAccountImageCmd { get; set; }
        private ImageShowView _imageShowView;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ImageAccountModel> _imgItemShowList = new ObservableCollection<ImageAccountModel>();
        public ObservableCollection<ImageAccountModel> ImgItemShowList
        {
            get { return _imgItemShowList; }
            set
            {
                _imgItemShowList = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ImgItemShowList"));
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ImageUrl"));
                }
            }
        }

        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get { return _currentImageIndex; }
            set
            {
                _currentImageIndex = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CurrentImageIndex"));
                }
            }
        }

        private string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                _accountName = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AccountName"));
                }
            }
        }
        #endregion
    }
}
