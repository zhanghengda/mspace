using Mmc.Mspace.Common.Enum;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.RoutePlanning;
using Mmc.Mspace.RoutePlanning.Utils;
using Mmc.Mspace.RoutePlanning.Views;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.RoutePlanning.ViewModels
{
    public class RoutePlanDetailVModel : Singleton<RoutePlanDetailVModel>,INotifyPropertyChanged
    {
        public RoutePlanDetailVModel()
        {
            this.CloseDetailCmd = new RelayCommand(OnCloseDetail);
            this.SendEmailCmd = new RelayCommand(OnSendEmail);
        }


        private void OnSendEmail()
        {
            try
            {
                Regex regex = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

                if (regex.IsMatch(EmailAddress))
                {
                    var result = RoutePlanHelper.Instance.RoutePlanSendEmail(RouteID, EmailAddress);
                    if(result.Equals("ok"))
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("RoutePlanningView_SendSuccess"));
                    }else
                    {
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey("RoutePlanningView_SendFail"));
                    }
                }
                else
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("RoutePlanningView_EmailNotExist"));
                }
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        public void OnCloseDetail()
        {
            if (_routePlanDetailView != null)
            {
                _routePlanDetailView.Hide();
            }
        }

        public void ShowView(RoutePlanModel routePlanModel)
        {
            try
            {
                if(_routePlanDetailView == null)
                {
                    _routePlanDetailView = new RoutePlanDetailView();
                }

                RouteID = routePlanModel.RouteID;
                RouteName = routePlanModel.RouteName;
                RouteAddTime = routePlanModel.RouteAddTime;
                NumberofWayPoints = routePlanModel.NumberofWayPoints;
                WorkingArea = routePlanModel.WorkingArea;
                EstimatedTime = routePlanModel.EstimatedTime;
                EstimatedRange = routePlanModel.EstimatedRange;

                _routePlanDetailView.DataContext = this;
                _routePlanDetailView.Owner = Application.Current.MainWindow;
                _routePlanDetailView.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                _routePlanDetailView.Show();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }


        #region 属性、命令

        public ICommand CloseDetailCmd { get; set; }
        public ICommand SendEmailCmd { get; set; }

        public RoutePlanDetailView _routePlanDetailView;

        

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("EmailAddress"));
                }
            }
        }

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

        private MeasurementAreaType _measurementAreaType;
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

        private RouteType _routeType;
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
        public float EstimatedTime
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

        public event PropertyChangedEventHandler PropertyChanged;

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

        #endregion
    }
}
