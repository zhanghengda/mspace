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

        private int _declarationStatus=0;
        /// <summary>
        /// 申报状态 0 待审核 1 通过 2 未通过
        /// </summary>
        public int DeclarationStatus
        {
            get { return _declarationStatus; }
            set
            { 
                _declarationStatus = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeclarationStatus"));
                }
            }
        }
        private string _declarationText= "待审核";
        /// <summary>
        /// 申报状态 0 待审核 1 通过 2 未通过
        /// </summary>
        public string DeclarationText
        {
            get 
            {
                if(DeclarationStatus==0)
                {
                    return _declarationText="待审核";
                }
                if (DeclarationStatus == 1)
                {
                    return _declarationText = "通过";
                }
                if (DeclarationStatus == 2)
                {
                    return _declarationText = "未通过";
                }
                return _declarationText;
            }
            set
            {
                _declarationText = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeclarationText"));
                }
            }
        }
        private string _declarationForeground = "待审核";
        /// <summary>
        /// 申报状态 0 待审核 1 通过 2 未通过
        /// </summary>
        public string DeclarationForeground
        {
            get
            {
                if (DeclarationStatus == 0)
                {
                    return _declarationForeground = "#FF7943";
                }
                if (DeclarationStatus == 1)
                {
                    return _declarationForeground = "#669633";
                }
                if (DeclarationStatus == 2)
                {
                    return _declarationForeground = "#EE8F8F";
                }
                return _declarationForeground;
            }
            set
            {
                _declarationForeground = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeclarationForeground"));
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
