using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule.BuildInfo
{
    public class ArchivesViewModel : CheckedToolItemModel
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
            this.TitleName = "建筑档案册";
            base.Command = new ShowArchivesCmd(_view);
            this.CloseCmd = new RelayCommand(() =>
            {
                if (this._view != null)
                    ((Window)this._view).Hide();
            });
        }

        public override FrameworkElement CreatedView()
        {
            return _view;
        }

        private string buildCode;

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        private string titleName;

        public string TitleName
        {
            get { return this.titleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.titleName, value, "TitleName"); }
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