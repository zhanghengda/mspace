using Mmc.Mspace.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Models.RoutePlanning
{
    public class RoutePlanModel: INotifyPropertyChanged
    {

        private string _routeID;
        public string RouteID
        {
            get { return _routeID; }
            set
            {
                _routeID = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RouteID"));
                }
            }
        }

        private string _routeName;
        public string RouteName
        {
            get { return _routeName; }
            set
            {
                _routeName = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RouteName"));
                }
            }
        }

        private string _routeAddTime;
        public string RouteAddTime
        {
            get { return _routeAddTime; }
            set
            {
                _routeAddTime = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RouteAddTime"));
                }
            }
        }

        private MeasurementAreaType _measurementAreaType=0;
        public MeasurementAreaType MeasurementAreaType
        {
            get { return _measurementAreaType; }
            set
            {
                _measurementAreaType = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("MeasurementAreaType"));
                }
            }
        }

        private RouteType _routeType = 0;
        public RouteType RouteType
        {
            get { return _routeType; }
            set
            {
                _routeType = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RouteType"));
                }
            }
        }

        private int _numberofWayPoints;
        public int NumberofWayPoints
        {
            get { return _numberofWayPoints; }
            set
            {
                _numberofWayPoints = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NumberofWayPoints"));
                }
            }
        }
        private float _workingArea;
        public float WorkingArea
        {
            get { return _workingArea; }
            set
            {
                _workingArea = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("WorkingArea"));
                }
            }
        }

        private float _estimatedTime;
        public float  EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                _estimatedTime = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EstimatedTime"));
                }
            }
        }

        private int _estimatedRange;
        public int EstimatedRange
        {
            get { return _estimatedRange; }
            set
            {
                _estimatedRange = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EstimatedRange"));
                }
            }
        }


        private string _routeCourseJson;
        public string RouteCourseJson
        {
            get { return _routeCourseJson; }
            set
            {
                _routeCourseJson = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RouteCourseJson"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
