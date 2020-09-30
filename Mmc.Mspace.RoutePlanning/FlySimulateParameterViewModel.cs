using Gvitech.CityMaker.FdeGeometry;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mmc.Mspace.RoutePlanning
{
    class FlySimulateParameterViewModel : CheckedToolItemModel
    {
        public Action ObjectStartFly;
        public Action ObjectPauseFly;
        public Action ObjectReSetFly;
        public Action<List<IPoint>, bool> ObjecReStartFly;
        public Action ObjectFollowerPer;
        public Action ObjectOverLook;
        public Action ObjectRemove;
        public Action <double> ObjectReSetSpeed;
        public ICommand StartFlyCmd { get; set; }
        public ICommand PauseFlyCmd { get; set; }
        public ICommand ReSetFlyCmd { get; set; }
        public ICommand ReStartFlyCmd { get; set; }
        public ICommand FollowerPerCmd { get; set; }
        public ICommand OverLookCmd { get; set; }
        public ICommand FlightCloseCmd { get; set; }
        
        public  List<IPoint> ReFlyPointList = new List<IPoint>();       

        FlySimulateParameterView flySimulateParameterView = null;
       
        public override void Initialize()
        {
            this.StartFlyCmd = new Mmc.Wpf.Commands.RelayCommand<bool>((parament) =>
            {
                if ( _flyIsChecked == false )
                {
                    ObjectStartFly();
                }
                else
                {
                    ObjectPauseFly();
                }                
            });
            this.PauseFlyCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjectPauseFly();
            });
            this.ReSetFlyCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjectReSetFly();
            });
            this.ReStartFlyCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjecReStartFly(ReFlyPointList, ViewIsChecked);
                FlyIsChecked = false;
            });  
            this.FollowerPerCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjectFollowerPer();
            });
            this.OverLookCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjectOverLook();
            });
            this.FlightCloseCmd = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                ObjectRemove();
            });

        }
            public void ShowParameterView(List<IPoint> PointList)
        {
            flySimulateParameterView = new FlySimulateParameterView();
            flySimulateParameterView.DataContext = this;
            flySimulateParameterView.Show();
            ReFlyPointList = PointList;
            FlySpeed = 5;
            //_flyIsChecked = true;
            ViewIsChecked = true;

        }
        public void HideParameterView()
        {
            flySimulateParameterView?.Hide();

        }
        public double _flyspeed;
        public double FlySpeed
        {
            get { return this._flyspeed; }
            set
            {

                _flyspeed = value; NotifyPropertyChanged("FlySpeed");
                ObjectReSetSpeed(FlySpeed);

            }
        }

        private bool _flyIsChecked;

        public bool FlyIsChecked
        {
            get { return _flyIsChecked; }
            set { _flyIsChecked = value; NotifyPropertyChanged("FlyIsChecked"); }
        }

        private bool _viewIsChecked;

        public bool ViewIsChecked
        {
            get { return _viewIsChecked; }
            set { _viewIsChecked = value; NotifyPropertyChanged("ViewIsChecked"); }
        }
      
    }
}
