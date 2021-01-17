
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.RoutePlanning.Utils;
using Mmc.Mspace.RoutePlanning.ViewModels;
using Mmc.Mspace.RoutePlanning.Views;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.RoutePlanning
{
    /// <summary>
    /// 航线展示列表
    /// </summary>
     public  class RouteListViewModel : CheckedToolItemModel
    {
        //private RoutePlanView _routePlanView;
        private RoutePlanningView _newRoutePlanView;
        public Views.ConveyJson conveyJson3;
        public bool status = false;
        [XmlIgnore]
        public ICommand cmdCloseWindow { get; set; }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void OnChecked()
        {
            base.OnChecked();

            //cmdCloseWindow = new RelayCommand(() =>
            //{
            //    releaseWindow();
            //});
            //if (this._routePlanView == null)
            //{
            //    this._routePlanView = new RoutePlanView();
            //    this._routePlanView.Owner = Application.Current.MainWindow;
            //    _routePlanView.conveyJson2 += new ConveyJson(GetMissionJson);
            //}
            //if (!_routePlanView.IsVisible)
            //{
            //    _routePlanView.Show();
            //}
            //this._routePlanView.DataContext = this;

            cmdCloseWindow = new RelayCommand(() => {
                releaseWindow();
            });
            ShowNewRoutePlanView();
            IsSelected = true;
        }

        private RoutePlanningViewModel _newRoutePlanViewModel;
        private void ShowNewRoutePlanView()
        {

            if (_newRoutePlanViewModel == null)
            {
                _newRoutePlanViewModel = new RoutePlanningViewModel();

            }
            _newRoutePlanViewModel.CloseRoutePlanning += OnCloseRoutePlanning;
            _newRoutePlanViewModel.ShowWindow(status);
        }

        private void OnCloseRoutePlanning(string obj)
        {
            IsSelected = false;
        }

        /// <summary>
        /// 获取Mission格式Json
        /// </summary>
        /// <param name="arrString"></param>
        public void GetMissionJson(string[] arrString)
        {
            conveyJson3(arrString);
        }

        public override void OnUnchecked()
        {
            //if (_routePlanView != null)
            //{
            //    _routePlanView.Hide();
            //}
            if (_newRoutePlanView != null)
            {
                _newRoutePlanView.Hide();
            }

            if (_newRoutePlanViewModel != null)
            {
                _newRoutePlanViewModel.OnReleaseWindow();
            }
            IsSelected = false;
        }
        public void releaseWindow()
        {
            this.OnUnchecked();
            //_routePlanView = null;
            _newRoutePlanView = null;
            IsSelected = false;
            Console.WriteLine("-----------CloseWindow");
        }
    }
}