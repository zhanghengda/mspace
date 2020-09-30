using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using System.Windows;
using System.Windows.Input;

namespace FireControlModule
{
    public class UnitVideoViewModel : CheckedToolItemModel
    {
        private VideoMonitorView _view;
        public ICommand CloseCmd { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            _view = new VideoMonitorView
            {
                Owner = Application.Current.MainWindow
            };
            base.Command = new VideoMonitorCmd(_view);
            this.CloseCmd = new RelayCommand(() =>
            {
                if (this._view != null)
                    ((Window)this._view).Hide();
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();

            //Window window = (Window)base.View;
            //window.Hide();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
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
    }
}