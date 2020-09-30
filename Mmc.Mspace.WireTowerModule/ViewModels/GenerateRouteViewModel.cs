using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class GenerateRouteViewModel : BaseViewModel
    {
        #region vary
        public List<TowerForClient> CurrentTowerList { get; set; }

        private CalculateAndOutputRoute _calculateAndOutputRoute;
        private GenerateRouteView _generateRouteView;
        #endregion

        #region binging varies and commands
        private string _routeName;
        public string RouteName
        {
            get => _routeName;
            set
            {
                _routeName = value;
                OnPropertyChanged("RouteName");
            }
        }

        private ObservableCollection<TowerForClient> _towerSet;

        public ObservableCollection<TowerForClient> TowerSet
        {
            get => _towerSet ?? new ObservableCollection<TowerForClient>();
            set
            {
                _towerSet = value;
                OnPropertyChanged("TowerSet");
            }
        }

        [XmlIgnore] public ICommand CancelCommand { get; set; }

        [XmlIgnore] public ICommand SaveCommand { get; set; }

        #endregion

        public GenerateRouteViewModel()
        {
            _calculateAndOutputRoute = new CalculateAndOutputRoute();
            CurrentTowerList = new List<TowerForClient>();
            this.CancelCommand = new Mmc.Wpf.Commands.RelayCommand(CloseView);
            this.SaveCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(RouteName))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTRoute")+ Helpers.ResourceHelper.FindKey("WTListName") + Helpers.ResourceHelper.FindKey("WTNotEmpty"));
                    return;
                }

                GenerateRoute(RouteName);
                this.CloseView();
            });
        }

        private void GenerateRoute(string routeName)
        {
            if(_calculateAndOutputRoute==null) _calculateAndOutputRoute = new CalculateAndOutputRoute();
            _calculateAndOutputRoute.GenerateRoute(routeName, CurrentTowerList);
        }

        public void ShowView()
        {
            if (_generateRouteView == null) _generateRouteView = new GenerateRouteView();
            _generateRouteView.DataContext = this;
            _generateRouteView.Owner = Application.Current.MainWindow;
            _generateRouteView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            TowerSet = new ObservableCollection<TowerForClient>(CurrentTowerList);
            _generateRouteView.Show();
        }

        public void CloseView()
        {
            if (_generateRouteView == null) _generateRouteView = new GenerateRouteView();
            _generateRouteView.Close();
            _generateRouteView = null;
        }
    }
}