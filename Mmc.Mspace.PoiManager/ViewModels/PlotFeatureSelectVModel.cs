using Mmc.Framework.Services;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class PlotFeatureSelectVModel : Singleton<FeatureSelectVModel>, INotifyPropertyChanged
    {
        public PlotFeatureSelectVModel()
        {
            this.CloseWindowCmd = new RelayCommand(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            CloseWindow();
        }

        public void ShowWindow(PlotFeatureSelectedModel featureSelectedModel, double x, double y)
        {
            try
            {
                if (_plotFeatureSelectView == null)
                {
                    _plotFeatureSelectView = new PlotFeatureSelectView();
                }
                _plotFeatureSelectView.DataContext = this;

                double dWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double dHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                //Height = "340" Width = "600"
                if (dWidth - x < 600)
                {
                    _plotFeatureSelectView.Left = x - 600;
                }
                else
                {
                    _plotFeatureSelectView.Left = x;
                }
                if (dHeight - y < 340)
                {
                    _plotFeatureSelectView.Top = y - 340;
                }
                else
                {
                    _plotFeatureSelectView.Top = y;
                }
                _plotFeatureSelectView.Owner = Application.Current.MainWindow;
                ConvertVariable(featureSelectedModel);
                _plotFeatureSelectView.Show();
            }
            catch (Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void ConvertVariable(PlotFeatureSelectedModel featureSelectedModel)
        {
            SelectedInfo = featureSelectedModel.SelectedInfo;
            SelectedFID = featureSelectedModel.SelectedFID;
            SelectedAddress = featureSelectedModel.SelectedAddress;
            SelectedPrincipal = featureSelectedModel.SelectedPrincipal;
            SelectedPPhone = featureSelectedModel.SelectedPPhone;
            SelectedManager = featureSelectedModel.SelectedManager;
            SelectedMPhone = featureSelectedModel.SelectedMPhone;
        }

        public void CloseWindow()
        {
            try
            {
                if (_plotFeatureSelectView != null)
                {
                    _plotFeatureSelectView.Hide();
                    GviMap.AxMapControl.HighlightHelper.SetRegion(null);
                }
            }
            catch (Exception e)
            {

            }
        }
        #region 命令、属性

        public PlotFeatureSelectView _plotFeatureSelectView = null;

        public ICommand CloseWindowCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedInfo;
        public string SelectedInfo
        {
            get { return _selectedInfo; }
            set
            {
                _selectedInfo = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedInfo"));
                }
            }
        }
        private int _selectedFID;
        public int SelectedFID
        {
            get { return _selectedFID; }
            set
            {
                _selectedFID = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedFID"));
                }
            }
        }
        private string _selectedAddress;
        public string SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedAddress"));
                }
            }
        }
        private string _selectedPrincipal;
        public string SelectedPrincipal
        {
            get { return _selectedPrincipal; }
            set
            {
                _selectedPrincipal = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedPrincipal"));
                }
            }
        }
        private string _selectedPPhone;
        public string SelectedPPhone
        {
            get { return _selectedPPhone; }
            set
            {
                _selectedPPhone = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedPPhone"));
                }
            }
        }
        private string _selectedManager;
        public string SelectedManager
        {
            get { return _selectedManager; }
            set
            {
                _selectedManager = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedManager"));
                }
            }
        }
        private string _selectedMPhone;
        public string SelectedMPhone
        {
            get { return _selectedMPhone; }
            set
            {
                _selectedMPhone = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedMPhone"));
                }
            }
        }
        #endregion
    }
}

