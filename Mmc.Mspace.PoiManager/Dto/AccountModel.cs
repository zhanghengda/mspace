using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class AccountModel : BindableBase
    {
        private int id;
        private int marker_id;
        private string site;
        private string area;
        private string title;
        private string status;
        private string img;
        private string video;
        private string _operator;
        private string operator_phone;
        private string addedTime;

        public int Id
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged("Id"); }
        }
        public int MarkerId
        {
            get { return marker_id; }
            set { marker_id = value; NotifyPropertyChanged("MarkerId"); }
        }

        public string Site
        {
            get { return site; }
            set { site = value; NotifyPropertyChanged("Site"); }
        }

        public string Area
        {
            get { return area; }
            set { area = value; NotifyPropertyChanged("Area"); }
        }

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; NotifyPropertyChanged("Operator"); }
        }

        public string Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged("Status"); }
        }

        public string Img
        {
            get { return img; }
            set { img = value; NotifyPropertyChanged("Img"); }
        }
       
        public string Video
        {
            get { return video; }
            set { video = value; NotifyPropertyChanged("Video"); }
        }
        
        public string OperatorPhone
        {
            get { return operator_phone; }
            set { operator_phone = value; NotifyPropertyChanged("OperatorPhone"); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged("Title"); }
        }

        public string AddedTime
        {
            get { return addedTime; }
            set { addedTime = value; NotifyPropertyChanged("AddedTime"); }
        }
    }
}
