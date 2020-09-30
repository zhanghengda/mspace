using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Wpf.Commands;
using System.Windows;
using Mmc.Mspace.Common.Models;
using System.Windows.Media;
using Mmc.Mspace.RegularInspectionModule.Dto;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class ImageDisplayVModel : BaseViewModel
    {
        public InspectModel CurrentPicPoiModel;
        public static ScaleTransform _scaleTransform;
        private ImageDisplayView _imageDisplayView;
        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged("ImagePath"); }
        }
        private Visibility _btnVisiable;
        public Visibility BtnVisiable
        {
            get { return _btnVisiable; }
            set { _btnVisiable = value; OnPropertyChanged("BtnVisiable"); }
        }
        private bool _isTroublePoi;
        public bool IsTroublePoi
        {
            get { return _isTroublePoi; }
            set { _isTroublePoi = value; OnPropertyChanged("IsTroublePoi"); }
        }
        private string _markBtnContent;
        public string MarkBtnContent
        {
            get { return _markBtnContent; }
            set { _markBtnContent = value; OnPropertyChanged("MarkBtnContent"); }
        }


        [XmlIgnore]
        public ICommand CancelCmd { get; set; }
        [XmlIgnore]
        public ICommand ImageLoadedCommand { get; set; }
        [XmlIgnore]
        public ICommand MarkTroublePoiCmd { get; set; }

        public ImageDisplayVModel()
        {
            _imageDisplayView = new ImageDisplayView();
            _imageDisplayView.DataContext = this;

            this.CancelCmd = new RelayCommand(() =>
            {
                CloseImageView(); 
            });

            ImageLoadedCommand = new RelayCommand<object>(OnImageLoadedCommand);
            this.MarkTroublePoiCmd = new RelayCommand(() =>
            {
                MarkTroublePoi();
            });
        }

        private void MarkTroublePoi()
        {
            if (CurrentPicPoiModel == null)
                return;

            if (IsTroublePoi)
            {
                CurrentPicPoiModel.IsTroublePoi = false;
                MarkBtnContent = Helpers.ResourceHelper.FindKey("MarkBtnTxt");
                IsTroublePoi = false;
            }
            else
            {
                CurrentPicPoiModel.IsTroublePoi = true;
                MarkBtnContent = Helpers.ResourceHelper.FindKey("ClearBtnTxt");
                IsTroublePoi = true;
            }
            RegInsDataRenderManager.Instance.UpdatePhotoRenderPoint(CurrentPicPoiModel);
        }

        private void OnImageLoadedCommand(object obj)
        {
            _scaleTransform = obj as ScaleTransform;
        }

        protected override void Loaded()
        {
            base.Loaded();
        }
        public void ShowImageView(string imgPath)
        {
            ImagePath = imgPath;
            if (CurrentPicPoiModel != null)
            {
                IsTroublePoi = CurrentPicPoiModel.IsTroublePoi;
                if (IsTroublePoi)
                {
                    MarkBtnContent = Helpers.ResourceHelper.FindKey("ClearBtnTxt");
                }
                else
                {
                    MarkBtnContent = Helpers.ResourceHelper.FindKey("MarkBtnTxt");
                }
            }

            if (_imageDisplayView != null)
            {
                _imageDisplayView.Close();
                _imageDisplayView = null;
                _imageDisplayView = new ImageDisplayView();
                _imageDisplayView.DataContext = this;
            }

            _imageDisplayView.Owner = Application.Current.MainWindow;
            _imageDisplayView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _imageDisplayView.Show();
        }

        private void CloseImageView()
        {
            _imageDisplayView.Hide();
        }
    }
}
