using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.WireTowerModule.Models;
using Mmc.Mspace.WireTowerModule.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Mmc.Mspace.Theme.Pop;

namespace Mmc.Mspace.WireTowerModule.ViewModels
{
    public class SignViewModel : BaseViewModel
    {
        #region vary

        public Action<SignModel,bool, bool> OnAddOrUpdateSign;
        public bool IsEdit { get; set; }
        private SignView _signView;

        #endregion

        #region binging vary and command

        private string _viewTitle;

        public string ViewTitle
        {
            get { return _viewTitle; }
            set
            {
                _viewTitle = value;
                OnPropertyChanged("ViewTitle");
            }
        }
        private bool _isSerialEnabled;

        public bool IsSerialEnabled
        {
            get { return _isSerialEnabled; }
            set
            {
                _isSerialEnabled = value;
                OnPropertyChanged("IsSerialEnabled");
            }
        }

        
        private SignModel _sign;

        public SignModel Sign
        {
            get { return _sign ?? new SignModel(); }
            set
            {
                _sign = value;
                OnPropertyChanged("Sign");
            }
        }

        private TextItem _selectedType;

        public TextItem SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        private ObservableCollection<TextItem> _signTypeSet;

        public ObservableCollection<TextItem> SignTypeSet
        {
            get
            {
                return _signTypeSet ??
                       (_signTypeSet = new ObservableCollection<TextItem>(CommonContract.GetSignType()));
            }
            set
            {
                _signTypeSet = value;
                OnPropertyChanged("SignTypeSet");
            }
        }

        [XmlIgnore] public ICommand CancelCommand { get; set; }
        [XmlIgnore] public ICommand SaveCommand { get; set; }
        [XmlIgnore] public ICommand GetPositionCommand { get; set; }

        #endregion


        public SignViewModel()
        {
            _signView = new SignView();

            this.CancelCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                HideView();
                OnAddOrUpdateSign(Sign,IsEdit, false);
                MapControlEventManagement(false);
            });
            this.SaveCommand = new Mmc.Wpf.Commands.RelayCommand(() =>
            {
                if (!int.TryParse(Sign.serial, out int num))
                {
                    Messages.ShowMessage(Helpers.ResourceHelper.FindKey("WTSerialInt"));
                    return;
                }

                Sign.type = SelectedType?.Key ?? SignTypeSet.FirstOrDefault()?.Key;
                HideView();
                OnAddOrUpdateSign(Sign,IsEdit, true);
                MapControlEventManagement(false);
            });
            this.GetPositionCommand = new Mmc.Wpf.Commands.RelayCommand(() => { getPosition(); });
        }

        private void getPosition()
        {
            this.HideView();
            MapControlEventManagement(true);
        }

        private void OnUpdatePosition(string action, double x, double y, double z)
        {
            switch (action)
            {
                case "Click":
                    Sign.X = x;
                    Sign.Y = y;
                    Sign.Z = z;
                    this.ShowView();
                    break;
                case "Move":
                    Sign.X = x;
                    Sign.Y = y;
                    Sign.Z = z;
                    break;
            }
        }

        private void MapControlEventManagement(bool OnEvent)
        {
            WirTowRenderManagement.Instance.MapClickSelectEventManagement(OnEvent);
            WirTowRenderManagement.Instance.MapTransformMoveEventManagement(OnEvent);
            if (OnEvent)
            {
                WirTowRenderManagement.Instance.OnUpdatePointPosition -= OnUpdatePosition;
                WirTowRenderManagement.Instance.OnUpdatePointPosition += OnUpdatePosition;
            }
            else
            {
                WirTowRenderManagement.Instance.OnUpdatePointPosition -= OnUpdatePosition;
            }
        }

        public void ShowView()
        {
            ViewTitle = (IsEdit ? Helpers.ResourceHelper.FindKey("WTEdit") : Helpers.ResourceHelper.FindKey("WTAdd")) + Helpers.ResourceHelper.FindKey("WTSign");
            IsSerialEnabled = !IsEdit;
            MapControlEventManagement(true);
            if (IsEdit)
            {
                SelectedType =  SignTypeSet?.ToList().Find(p => p.Key.Equals(Sign.type))?? SelectedType ;
                WirTowRenderManagement.Instance.MapTransformMoveEventManagement(true);
                WirTowRenderManagement.Instance.SetTransformPoint(Sign.X, Sign.Y, Sign.Z);
            }
            else
            {
                SelectedType = SelectedType ?? SignTypeSet?[0];
            }

            if (_signView == null) _signView = new SignView();
            _signView.DataContext = this;
            _signView.Owner = Application.Current.MainWindow;
            _signView.Left = 100;
            _signView.Top = 100;
            _signView.Show();
        }

        public void HideView()
        {
            _signView?.Hide();
        }

        public void CloseView()
        {
            _signView?.Close();
            _signView = null;
        }
    }
}