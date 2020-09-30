using Mmc.Framework.Services;
using Mmc.Mspace.PoiManagerModule.Models;
using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.PoiManagerModule.ViewModels
{
    public class FeatureSelectVModel : Singleton<FeatureSelectVModel>, INotifyPropertyChanged
    {
        public FeatureSelectVModel()
        {
            this.CloseWindowCmd = new RelayCommand(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            CloseWindow();
        }

        public void ShowWindow(FeatureSelectedModel featureSelectedModel,double x,double y)
        {
            try
            {
                if (_featureSelectView == null)
                {
                    _featureSelectView = new FeatureSelectView();
                }
                _featureSelectView.DataContext = this;

                double dWidth = SystemParameters.PrimaryScreenWidth;
                double dHeight = SystemParameters.PrimaryScreenHeight;
                //Height = "340" Width = "600"
                if (dWidth - x < 600)
                {
                    _featureSelectView.Left = x - 600;
                }
                else
                {
                    _featureSelectView.Left = x;
                }
                if (dHeight - y < 340)
                {
                    _featureSelectView.Top = y - 340;
                }
                else
                {
                    _featureSelectView.Top = y;
                }
                _featureSelectView.Owner = Application.Current.MainWindow;
                ConvertVariable(featureSelectedModel);
                _featureSelectView.Show();
            }
            catch(Exception e)
            {
                SystemLog.Log(e);
            }
        }

        private void ConvertVariable(FeatureSelectedModel featureSelectedModel)
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
                if (_featureSelectView != null)
                {
                    _featureSelectView.Hide();
                    GviMap.AxMapControl.HighlightHelper.SetRegion(null);
                }
            }
            catch(Exception e)
            {

            }
        }
        #region 命令、属性

        public FeatureSelectView _featureSelectView = null;

        public ICommand CloseWindowCmd { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _selectedInfo;
        public string SelectedInfo
        {
            get { return _selectedInfo; }
            set {
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
                if(this.PropertyChanged !=null)
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
