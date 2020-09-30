using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class NewInspectionVModel : CheckedToolItemModel
    {
        private NewInspectionView _newInspectionView;

        private InspectRegion _selectedItem;

        public InspectRegion SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }

        private string _newName;

        public string NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                NotifyPropertyChanged("NewName");
            }
        }

        private DateTime _inspectionDate=DateTime.Now;

        public DateTime InspectionDate
        {
            get { return _inspectionDate; }
            set { _inspectionDate = value; NotifyPropertyChanged("InspectionDate"); }
        }



        private ObservableCollection<InspectRegion> _inspectRegions;

        public ObservableCollection<InspectRegion> InspectRegions
        {
            get { return _inspectRegions; }
            set { _inspectRegions = value; NotifyPropertyChanged("InspectRegions"); }
        }

        private RelayCommand _cancelCommand;
        [XmlIgnore]
        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(OnCancelCommand)); }
            set { _cancelCommand = value; }
        }

        private RelayCommand _createCommand;
        [XmlIgnore]
        public RelayCommand CreateCommand
        {
            get { return _createCommand ?? (_createCommand = new RelayCommand(OnCreateCommand)); }
            set { _createCommand = value; }
        }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            Messenger.Messengers.Register<bool>("CreateNewInspection", (t) =>
            {
                IsChecked = true;
            });
        }



        private void OnCancelCommand()
        {
            IsChecked = false;
        }
        public override void OnChecked()
        {
            base.OnChecked();
            if (_newInspectionView == null)
            {
                _newInspectionView = new NewInspectionView();
                _newInspectionView.DataContext = this;
            }
            _newInspectionView.Owner = Application.Current.MainWindow;
            LoadData();
            _newInspectionView.Show();
        }
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            _newInspectionView.Hide();
        }
        public void LoadData()
        {
            NewName = string.Empty;
            InspectionDate = DateTime.Now;
            InspectRegions = new ObservableCollection<InspectRegion>(InspectionService.Instance.GetAllRegion());

        }
        private void OnCreateCommand()
        {
            if (string.IsNullOrEmpty(_newName) && _selectedItem == null)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("InputName"));
                return;
            }
            if (_newName.Length>21)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("InputNameLength"));
                return;
            }
            if(_selectedItem==null|| _selectedItem.Name!=_newName)
            {
              var regions=  _inspectRegions.Where(t => t.Name == _newName).ToList();
                if (regions.Count > 0)
                    SelectedItem = regions.SingleOrDefault();
                else
                {
                    _selectedItem = new InspectRegion();
                    _selectedItem.Name = _newName;
                }
            }
            _selectedItem.InspectUnits = new List<InspectUnit>();
            _selectedItem.InspectUnits.Add(new InspectUnit() { Name= _inspectionDate.ToLongDateString(),Time=_inspectionDate});
            if (InspectionService.Instance.IsExistsRegion(_selectedItem.Id,_newName, _inspectionDate.ToLongDateString()))
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("ReSaveName"));
                return;
            }
            var result = InspectionService.Instance.AddRegion(_selectedItem);
            if (result != null)
            {
                NewName = null;
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savesuccess"));
                Messenger.Messengers.Notify("AddRegion", true);
                IsChecked = false;

                Messenger.Messengers.Notify("HistoryDomRefresh");
                Messenger.Messengers.Notify("PhotoTraceRefresh");
            }
            else
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("Savefailed"));
            }
        }
    }
}
