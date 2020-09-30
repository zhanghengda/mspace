using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Dto
{
    public class MarkerModel: BindableBase
    {

        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("id"); }
        }
        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; NotifyPropertyChanged("PoiTitle"); }
        }
        private string _img;
        public string img
        {
            get { return _img; }
            set { _img = value; NotifyPropertyChanged("img"); }
        }
        //public string img { get; set; }

        private string _detail;

        public string detail
        {
            get { return _detail; }
            set { _detail = value; NotifyPropertyChanged("detail"); }
        }

        public string _address;
        public string address
        {
            get { return _address; }
            set { _address = value; NotifyPropertyChanged("address"); }
        }
        public string style { get; set; }
        public int cat_id { get; set; }
        public string cat_Name { get; set; }
        public string cat_url { get; set; }
        public int type { get; set; }
        public string geom { get; set; }
        public string sr_proj4_text { get; set; }
        private string _lp_size;
        public string lp_size
        {
            get { return _lp_size; }
            set { _lp_size = value; NotifyPropertyChanged("Area"); }
        }

        private List<TagItem> _tags;
        
        public List<TagItem> tags
        {
            get { return _tags; }
            set { _tags = value; NotifyPropertyChanged("tags"); }
        }
        

        public PoiCategory category { get; set; }

        public string positionKey { get; set; }

        public string guid { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; NotifyPropertyChanged("IsSelected"); }
        }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; NotifyPropertyChanged("IsChecked"); }
        }
        

    }


}
