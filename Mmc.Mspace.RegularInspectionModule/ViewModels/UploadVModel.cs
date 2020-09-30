using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Mmc.Framework.Services;
using Mmc.Mspace.Common;
using Mmc.Mspace.Common.Messenger;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Models.Inspection;
using Mmc.Mspace.RegularInspectionModule.Dto;
using Mmc.Mspace.RegularInspectionModule.model;
using Mmc.Mspace.RegularInspectionModule.Views;
using Mmc.Mspace.Services.LocalConfigService;
using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Mmc.Mspace.RegularInspectionModule.ViewModels
{
    public class UploadVModel : BindableBase
    {
        private bool _isLoading = false;
        private UploadView uploadView;
        private InspectModel inspectModel;

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                NotifyPropertyChanged("TitleName");
            }
        }

        private string _currentFilePath;
        public string CurrentFilePath
        {
            get { return _currentFilePath; }
            set
            {
                _currentFilePath = value;
                NotifyPropertyChanged("CurrentFilePath");
            }
        }

        private ObservableCollection<FileModel> _dtSource;
        public ObservableCollection<FileModel> dtSource
        {
            get { return _dtSource ?? (_dtSource = new ObservableCollection<FileModel>()); }
            set
            {
                _dtSource = value;
                NotifyPropertyChanged("dtSource");
            }
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(OnCloseCommand)); }
            set { _closeCommand = value; }
        }

        private RelayCommand _confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get { return _confirmCommand ?? (_confirmCommand = new RelayCommand(OnConfirmCommand)); }
            set { _confirmCommand = value; }
        }

        private RelayCommand _selectFileCommand;
        public RelayCommand SelectFileCommand
        {
            get { return _selectFileCommand ?? (_selectFileCommand = new RelayCommand(OnSelectFileCommand)); }
            set { _selectFileCommand = value; }
        }

        private RelayCommand<FileModel> _deleteCommand;
        public RelayCommand<FileModel> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<FileModel>(OnDeleteCommand)); }
            set { _deleteCommand = value; }
        }

        public UploadVModel(InspectModel inspectModel)
        {
            this.inspectModel = inspectModel;

            if (uploadView is null)
            {
                uploadView = new UploadView();
            }

            uploadView.DataContext = this;
            uploadView.Owner = Application.Current.MainWindow;
            uploadView.Show();
        }

        private void OnCloseCommand()
        {
            uploadView.Close();
        }

        private void OnConfirmCommand()
        {
            Task.Run(() =>
            {
                try
                {
                    _isLoading = true;
                    uploadView.ChangeButtonEnable(false);

                    foreach (var fileModel in dtSource)
                    {
                        UploadFile(fileModel);
                    }

                    _isLoading = false;
                    uploadView.ChangeButtonEnable(true);

                    ServiceManager.GetService<IShellService>().ShellWindow.Dispatcher?.Invoke(() =>
                    {
                        Messenger.Messengers.Notify("HistoryDomRefresh");
                        Messenger.Messengers.Notify("LeftListRefresh");
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey(CommonContract.OperateDataStatus.LOADSUCCESSED.ToString()));
                    });
                }
                catch (Exception ex)
                {
                    ServiceManager.GetService<IShellService>().ShellWindow.Dispatcher?.Invoke(() =>
                    {
                        Messenger.Messengers.Notify("HistoryDomRefresh");
                        Messenger.Messengers.Notify("LeftListRefresh");
                        Messages.ShowMessage(Helpers.ResourceHelper.FindKey(CommonContract.OperateDataStatus.LOADFAILED.ToString()));
                    });
                }
            });
        }


        private void OnSelectFileCommand()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            string title = string.Empty;
            string filter = string.Empty;

            if (inspectModel.DataType == Common.CommonContract.InspectDataType.Picture)
            {
                title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionPicture") + Helpers.ResourceHelper.FindKey("File");
                filter = FileFilterStrings.IMAGE;
            }
            else if(inspectModel.DataType==Common.CommonContract.InspectDataType.Route)
            {
                title = Helpers.ResourceHelper.FindKey("PleaseChoose") + Helpers.ResourceHelper.FindKey("InspectionRoute") + Helpers.ResourceHelper.FindKey("File");
                filter = FileFilterStrings.KML;
            }

            dialog.Title = title;
            dialog.Filter = filter;

            if (dialog.ShowDialog() == true)
            {
                var pathList = dialog.FileNames.ToList();

                if (pathList.Count > 0)
                {
                    dtSource.Clear();

                    foreach (var path in pathList)
                    {
                        string name = Path.GetFileNameWithoutExtension(path);

                        FileModel model = new FileModel
                        {
                            Path = path,
                            Name = name,
                            Status = "未完成"
                        };

                        dtSource.Add(model);
                    }

                    CurrentFilePath = dtSource[0].Path;
                }
            }
        }

        private void OnDeleteCommand(FileModel model)
        {
            if (!_isLoading)
            {
                if (Messages.ShowMessageDialog("删除任务", "确定要删除这条任务吗?"))
                {
                    dtSource.Remove(model);
                }
            }
        }

        private void UploadFile(FileModel fileModel)
        {
            if (inspectModel.DataType == CommonContract.InspectDataType.Picture)
            {
                var picCfg = new PictureItem
                {
                    Path = fileModel.Path,
                    Name = fileModel.Name,
                    InspectUnitId = inspectModel.InspectUnitId
                };

                InspectionService.Instance.PictureItemSet.Add(picCfg);

                fileModel.Status = "已完成";
            }
            else if (inspectModel.DataType == CommonContract.InspectDataType.Route)
            {
                string style = string.Empty;
                var polyLine = GviMap.GeoFactory.CreateFromXml(fileModel.Path, GviMap.SpatialCrs);

                var routeCfg = new RouteItem
                {
                    Name = fileModel.Name,
                    InspectUnitId = inspectModel.InspectUnitId,
                    Geom = polyLine.AsWKT(),
                    Style = style
                };

                InspectionService.Instance.RouteItemSet.Add(routeCfg);

                fileModel.Status = "已完成";
            }

        }
    }
}
