using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule.BuildInfo
{
    /// <summary>
    /// 单位列表
    /// </summary>
    public class UnitListViewModel : CheckedToolItemModel
    {
        public ICommand CloseCmd { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _view = new ArchivesView()
            {
                DataContext = this,
                Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
            };

            base.Command = new UnitListCmd(_view);
            this.CloseCmd = new RelayCommand(() =>
            {
                if (this._view != null)
                    ((Window)this._view).Hide();
            });
        }

        private string buildCode;

        private string requestUrl;

        public string RequestUrl
        {
            get { return this.requestUrl; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.requestUrl, value, "RequestUrl"); }
        }

        public override FrameworkElement CreatedView()
        {
            return _view;
        }

        private string titleName;

        public string TitleName
        {
            get { return this.titleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.titleName, value, "TitleName"); }
        }

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        private string unitId;

        public string UnitId
        {
            get { return this.unitId; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.unitId, value, "UnitId"); }
        }

        public override void Reset()
        {
            base.Reset();
            base.IsChecked = false;
        }

        private ArchivesView _view;

        public override void OnChecked()
        {
            base.OnChecked();
        }
    }
}