using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.PoiManagerModule.Models
{
    public class QueryWktGroup:BindableBase
    {
        private string id;
        private string name;
        public List<string> WktStringList;
        private string areaNum;
        public QueryWktGroup(string _id,string _name,List<string> _wktStringList)
        {
            id = _id;
            name = _name;
            WktStringList = _wktStringList;
            areaNum = Convert.ToString(_wktStringList.Count);
        }

        public void addWkt(string _wktString)
        {
            WktStringList.Add(_wktString);
            areaNum = Convert.ToString(WktStringList.Count);
        }
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }
        public string AreaNum
        {
            get { return areaNum; }
            set { areaNum = value; NotifyPropertyChanged("AreaNum"); }
        }
        public string ID
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged("ID"); }
        }
    }
}
