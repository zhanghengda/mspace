using Gvitech.CityMaker.FdeGeometry;
using Mmc.Mspace.WireTowerModule.Tools;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.WireTowerModule.Models
{
    public class RouteForClient : BindableBase
    {
        private string _userName;
        private int _id;
        private int _isAdministrator;
        private string _routeName;
        private string _serial;
        private string _pid;
        private double _distance;
        private int _towerCount;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; NotifyPropertyChanged("UserName"); }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged("Id"); }
        }
        public int IsAdministrator
        {
            get { return _isAdministrator; }
            set { _isAdministrator = value; NotifyPropertyChanged("IsAdministrator"); }
        }
        public string Pid
        {
            get { return _pid; }
            set { _pid = value; NotifyPropertyChanged("Pid"); }
        }

        public string RouteName
        {
            get { return _routeName; }
            set { _routeName = value; NotifyPropertyChanged("RouteName"); }
        }
        public string Serial
        {
            get { return _serial; }
            set { _serial = value; NotifyPropertyChanged("Serial"); }
        }
        public double Distance
        {
            get { return _distance; }
            set { _distance = Math.Round(value, 2); NotifyPropertyChanged("Distance"); }
        }
        public int TowerCount
        {
            get { return _towerCount; }
            set { _towerCount = value; NotifyPropertyChanged("TowerCount"); }
        }

        public List<TowerForClient> Towers { get; set; }
        public List<Placemark> TowerList { get; set; }
        public List<IPolyline> LeftLineList { get; set; }
        public List<IPolyline> RightLineList { get; set; }

        public List<FlightWay> LeftWay { get; set; }
        public List<FlightWay> RightWay { get; set; }
        public RouteForClient()
        {
            TowerList = new List<Placemark>();
            LeftLineList = new List<IPolyline>();
            RightLineList = new List<IPolyline>();
            LeftWay = new List<FlightWay>();
            RightWay = new List<FlightWay>();
        }
    }
}
