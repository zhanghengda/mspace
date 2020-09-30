using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.IotModule.Models
{
    public class GridInfoModel : BindableBase
    {



        private string _grid_id;

        public string grid_id
        {
            get { return _grid_id; }
            set { _grid_id = value; NotifyPropertyChanged("grid_id"); }
        }
        private string _village_num;

        public string village_num
        {
            get { return _village_num; }
            set { _village_num = value; NotifyPropertyChanged("village_num"); }
        }
        private string _grid_num;

        public string grid_num
        {
            get { return _grid_num; }
            set { _grid_num = value; NotifyPropertyChanged("grid_num"); }
        }
        private string _gridman;

        public string gridman
        {
            get { return _gridman; }
            set { _gridman = value; NotifyPropertyChanged("gridman"); }
        }

        private string _gridman_parttime;

        public string gridman_parttime
        {
            get { return _gridman_parttime; }
            set { _gridman_parttime = value; NotifyPropertyChanged("gridman_parttime"); }
        }

        private string _app_num;

        public string app_num
        {
            get { return _app_num; }
            set { _app_num = value; NotifyPropertyChanged("app_num"); }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("name"); }
        }
        private int _index;

        public int Index
        {
            get { return _index; }
            set { _index = value; NotifyPropertyChanged("Index"); }
        }

        
    }
}
