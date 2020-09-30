using Mmc.Mspace.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class ImageAccountModel : BaseViewModel
    {

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        private bool _isContainsImage;
        public bool IsContainsImage
        {
            get { return _isContainsImage; }
            set
            {
                _isContainsImage = value;
                OnPropertyChanged("IsContainsImage");
            }
        }

        private Visibility _imageCloseBtnVisibility=Visibility.Visible;
        public Visibility ImageCloseBtnVisibility
        {
            get { return _imageCloseBtnVisibility; }
            set
            {
                _imageCloseBtnVisibility = value;
                OnPropertyChanged("ImageCloseBtnVisibility");
            }
        }
    }
}
