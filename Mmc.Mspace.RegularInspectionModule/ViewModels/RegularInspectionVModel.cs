using Helpers;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.PoiManagerModule.ViewModels;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.DataSourceServices;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Mmc.Mspace.Common.CommonContract;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    /// <summary>
    /// 常态化巡检列表构造器
    /// </summary>
    public class RegularInspectionVModel:BaseViewModel
    {

        //private RegInsImportDataView _regInsImportDataView;

        //private RegInsImportDataVModel _regInsImportDataVModel;
        private ObservableCollection<InspectModel> _inspectRegions;

        public ObservableCollection<InspectModel> InspectRegions
        {
            get { return _inspectRegions ?? (_inspectRegions = new ObservableCollection<InspectModel>()); }
            set
            {
                _inspectRegions = value;
                OnPropertyChanged("InspectRegions");
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged("SearchText"); }
        }


        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get { return _searchCommand??(_searchCommand =new RelayCommand(OnSearchCommand)); }
            set { _searchCommand = value; }
        }

        private RelayCommand<object> _selectChangedCommand;

        public RelayCommand<object> SelectChangedCommand
        {
            get { return _selectChangedCommand??(_selectChangedCommand=new RelayCommand<object>(OnSelectChangedCommand)); }
            set { _selectChangedCommand = value; }
        }


        private RelayCommand<object> _importCommand;

        public RelayCommand<object> ImportCommand
        {
            get { return _importCommand??(_importCommand=new RelayCommand<object>(OnImportCommand)); }
            set { _importCommand = value; }
        }
        private RelayCommand<object> _checkedCommand;

        public RelayCommand<object> CheckedCommand
        {
            get { return _checkedCommand ?? (_checkedCommand = new RelayCommand<object>(OnCheckedCommand)); }
            set { _checkedCommand = value; }
        }
        private RelayCommand<object> _downloadCommand;

        public RelayCommand<object> DownloadCommand
        {
            get { return _downloadCommand ?? (_checkedCommand = new RelayCommand<object>(OnDownloadCommand)); }
            set { _downloadCommand = value; }
        }
        

        private RelayCommand<object> _deleteCommand;

        public RelayCommand<object> DeleteCommand
        {
            get { return _deleteCommand??(_deleteCommand=new RelayCommand<object>(OnDeleteCommand)); }
            set { _deleteCommand = value; }
        }
        private RelayCommand<object> _reNameCmd;

        public RelayCommand<object> ReNameCmd
        {
            get { return _reNameCmd ?? (_reNameCmd = new RelayCommand<object>(OnReNameCommand)); }
            set { _reNameCmd = value; }
        }
        


        public RegularInspectionVModel()
        {
          Messenger.Messengers.Register<bool>("AddRegion", (t) =>
            {
                if (t)
                    UpdateData();
            });

            Messenger.Messengers.Register("LeftListRefresh", () =>
            {
                LoadData();
            });
        }

        public void MapControlEventManagement(bool OnEvent)
        {
            RegInsDataRenderManager.Instance.MapControlEventManagement(OnEvent);
        }


        private void OnSearchCommand()
        {
            InspectRegions = new ObservableCollection<InspectModel>(InspectionService.Instance.GetAllRegion(_searchText).Select(t => RegInsModelConvert.InspectRegionConvert(t)).ToList());
        }

        private void OnSelectChangedCommand(object parameter)
        {

        }

        private void OnImportCommand(object parameter)
        {
            //if (parameter == null) return;
            //InspectModel inspectModel = parameter as InspectModel;
            //if (_regInsImportDataView==null)
            //{
            //    _regInsImportDataView = new RegInsImportDataView();
            //    _regInsImportDataVModel = new RegInsImportDataVModel();
            //    _regInsImportDataVModel.CloseWindow = CloseWindow;
            //    _regInsImportDataVModel.UpdateDataList = UpdateData;
            //    _regInsImportDataView.DataContext = _regInsImportDataVModel;
            //}
            //_regInsImportDataVModel.currentUnitId = inspectModel.Id;
            //_regInsImportDataVModel.currentUnitName = inspectModel.Name;
            //_regInsImportDataView.Owner= Application.Current.MainWindow;
            //_regInsImportDataView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //_regInsImportDataView.Show();

            //if (_regInsImportDataView == null)
            //    _regInsImportDataVModel = new RegInsImportDataVModel();
            var inspect = parameter as InspectModel;
            if (inspect == null) return;
           RegInsDataRenderManager.Instance.ImportInspectData(inspect);
            //if (success)
            //    LoadData();
        }


        private void OnCheckedCommand(object parameter)
        {
            var inspect = parameter as InspectModel;
            if (inspect == null) return;
            RegInsDataRenderManager.Instance.OpenInspectData(inspect);

        }

        private void OnDownloadCommand(object parameter)
        {
            var inspect = parameter as InspectModel;
            if (inspect == null) return;
            RegInsDataRenderManager.Instance.OpenInspectData(inspect);

        }

        //private void CloseWindow()
        //{
        //    ServiceManager.GetService<IShellService>(null).ShellWindow.Dispatcher.Invoke(() =>
        //    {
        //        _regInsImportDataView.Hide();
        //        _regInsImportDataVModel.ClearData();
        //    });
        //}

        private void OnDeleteCommand(object parameter)
        {
            if (parameter == null)
            {
                Messages.ShowMessage(ResourceHelper.FindKey("SelectedDeleteItem"));
                return;
            }
            InspectModel inspectModel = parameter as InspectModel;
             
            var message = string.Format(Helpers.ResourceHelper.FindKey("ConfirmDeleteData"), ResourceHelper.FindKey("Inspection" + inspectModel.DataType.ToString()));
            if (!Messages.ShowMessageDialog(ResourceHelper.FindKey("DeleteTitle"), message)) return;
            bool result = false;
            result = RegInsDataRenderManager.Instance.DeleteInspectData(inspectModel);
            if (result)
            {
                Messages.ShowMessage(Helpers.ResourceHelper.FindKey("DeleteSuccess"));
                UpdateData();
            }
        }
        ChangeRegInsNameVModel ChangeRegInsNameVModel = null;
        private void OnReNameCommand(object parameter)
        {
            if (parameter == null)
            {
                Messages.ShowMessage("该数据不存在");//ResourceHelper.FindKey("SelectedDeleteItem")
                return;
            }
            InspectModel inspectModel = parameter as InspectModel;
            if (ChangeRegInsNameVModel != null)
            {
                ChangeRegInsNameVModel?.changeRegInsNameView.Close();
            }
            ChangeRegInsNameVModel = new ChangeRegInsNameVModel(inspectModel, inspectModel.Name);
            ChangeRegInsNameVModel.UpdateName -= EditQueryName;
            ChangeRegInsNameVModel.UpdateName += EditQueryName;
            ChangeRegInsNameVModel.OpenEditName();
            //tree1
            //int index = InspectRegions.IndexOf(inspectModel);
            //InspectRegions.Remove(inspectModel);
            //RegInsDataRenderManager.Instance.DeleteInspectData(inspectModel);
            //inspectModel.Name = "121";
            //InspectRegions.Insert(index,inspectModel);
          
          


        }
        public void EditQueryName(InspectModel _inspectModel,string _name)
        {
            RegInsDataRenderManager.Instance.ChangeInspectDataName(_inspectModel, _name);
            UpdateData();
            ChangeRegInsNameVModel?.CloseEditName();
        }
        public void UpdateData(InspectModel inspectModel)
        {
            UpdateData();
        }

        protected override void Loaded()
        {
            //InspectRegions.Add(MockNewInspectRegionData());
            LoadData();
            base.Loaded();
        }

        private void LoadData()
        {
            UpdateData();
            if (InspectRegions.Count == 0)
                Messenger.Messengers.Notify("CreateNewInspection", true);
        }

        private void UpdateData()
        {
            InspectRegions = new ObservableCollection<InspectModel>(InspectionService.Instance.GetAllRegion().Select(t => RegInsModelConvert.InspectRegionConvert(t)).ToList());
        }

        protected override void Unloaded()
        {
            base.Unloaded();
        }

    }
}
