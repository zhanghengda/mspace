using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule.FireIot
{
    public class FloorMapViewModel : CheckedToolItemModel
    {
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public override FrameworkElement CreatedView()
        {
            return new FloorMapView()
            {
                Owner = Application.Current.MainWindow
            };
        }

        private string imgName;

        public string ImgName
        {
            get { return this.imgName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.imgName, value, "ImgName"); }
        }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = (ViewType)1;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                ((Window)this.View).Hide();
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();
            ((Window)this.View).Show();
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();

            ((Window)this.View).Hide();
        }
    }
}