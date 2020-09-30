using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Theme.Pop;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Views;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    class WirTowManageViewModel : CheckedToolItemModel
    {
        #region binging varies and command

        private WireTowManageView _wirTowManageView;
        private LineManageViewModel _lineManageViewModel;

        public LineManageViewModel LineManageViewModel
        {
            get { return _lineManageViewModel ?? (_lineManageViewModel =new LineManageViewModel()); }
            set { _lineManageViewModel = value; NotifyPropertyChanged("LineManageViewModel"); }
        }

        private TowerManageViewModel _towerManageViewModel;

        public TowerManageViewModel TowerManageViewModel
        {
            get { return _towerManageViewModel ?? (_towerManageViewModel = new TowerManageViewModel()); }
            set { _towerManageViewModel = value; NotifyPropertyChanged("TowerManageViewModel"); }
        }

        private RouteManageViewModel _routeManageViewModel;

        public RouteManageViewModel RouteManageViewModel
        {
            get { return _routeManageViewModel ?? (_routeManageViewModel = new RouteManageViewModel()); }
            set { _routeManageViewModel = value; NotifyPropertyChanged("RouteManageViewModel"); }
        }

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                //OnSelectCommand(value);
                _selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        [XmlIgnore]
        public ICommand CancelCommand { get; set; }
        #endregion

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _wirTowManageView = new WireTowManageView();
        }

        public override void OnChecked()
        {
            base.OnChecked();

            this.CancelCommand = new Mmc.Wpf.Commands.RelayCommand(() => { base.IsChecked = false; });

            ShowView();
            Messenger.Messengers.Notify("ShowHiddenMenu", true);
            if (_lineManageViewModel.OnLineChanged == null) _lineManageViewModel.OnLineChanged += LineChangedCallback;
            if (_towerManageViewModel.OnManagerViewShow == null) _towerManageViewModel.OnManagerViewShow += ViewShowCallback;
        }

        private void ViewShowCallback(bool obj)
        {
            if (obj)
            {
                this.ShowView();
            }
            else
            {
                this.HideView();
            }
        }

        private void LineChangedCallback(List<LineForClient> obj)
        {
            if(obj== null) return;
            _towerManageViewModel.UpdateLineSetValue(obj);
            _routeManageViewModel.UpdateLineSetValue(obj);
        }

        public override void OnUnchecked()
        {
            if (_wirTowManageView.IsActive && Messages.ShowMessageDialog(Helpers.ResourceHelper.FindKey("WTItemHint"), Helpers.ResourceHelper.FindKey("WTSaveData")))
            {
                _towerManageViewModel.SaveTowerSet();
                _lineManageViewModel.SaveData();
            }
            base.OnUnchecked();
            this.HideView();
            Messenger.Messengers.Notify("ShowHiddenMenu", false);
            WirTowRenderManagement.Instance.ClearRenderObj();
        }

        public void ShowView()
        {
            if(_wirTowManageView==null) _wirTowManageView = new WireTowManageView();
            _wirTowManageView.DataContext = this;
            if (Application.Current.MainWindow != null)
            {
                _wirTowManageView.Owner = Application.Current.MainWindow;
                _wirTowManageView.Left = Application.Current.MainWindow.Width - _wirTowManageView.Width - 60;
                _wirTowManageView.Top = Application.Current.MainWindow.Top + 60;
            }

            _wirTowManageView.Show();
        }

        public void HideView()
        {
            if (_wirTowManageView == null) _wirTowManageView = new WireTowManageView();
            _wirTowManageView.Hide();
        }
    }
}
