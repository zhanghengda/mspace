using Mmc.Mspace.Common.Models;
using System.Windows.Input;

namespace FireControlModule.InsideBuild
{
    public class InsideBuildViewModel : ToolItemModel
    {
        public ICommand CloseCmd { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;

            base.Command = new InsideBuildCmd();
            //this.CloseCmd = new RelayCommand(() =>
            //{
            //    if (this._view != null)
            //        ((Window)this._view).Hide();
            //});
        }

        private string buildCode;

        public string BuildCode
        {
            get { return this.buildCode; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.buildCode, value, "BuildCode"); }
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}