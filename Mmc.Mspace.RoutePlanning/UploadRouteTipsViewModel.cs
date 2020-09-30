using Mmc.Mspace.Common.Models;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.RoutePlanning
{
    public delegate void UploadRoute(string routeName);
   public  class UploadRouteTipsViewModel : CheckedToolItemModel
    {
        public UploadRoute _uploadRoute;
        private string _routeName;

        /// <summary>
        /// 上传航线绑定
        /// </summary>
        public ICommand cmdUploadRoute { get; set; }
        [XmlIgnore]
        public ICommand cmdCancel { get; set; }
        UploadRouteTipsView _uploadRouteTipsView;
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            cmdUploadRoute = new RelayCommand(() => 
            {
                _uploadRoute(RouteName);
            });
            cmdCancel = new RelayCommand(() =>
            {
                releaseWindow();
            });
        }
        public override void OnChecked()
        {
            base.OnChecked();
            if (_uploadRouteTipsView == null)
            {
                _uploadRouteTipsView = new UploadRouteTipsView();
                _uploadRouteTipsView.Owner = Application.Current.MainWindow;
                _uploadRouteTipsView.Closed += (sender, e) =>
                {
                    _uploadRouteTipsView = null;
                };
            }
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            int t = (int)cha.TotalSeconds;
            RouteName = t.ToString();
            _uploadRouteTipsView.DataContext = this;
            if (!_uploadRouteTipsView.IsVisible)
            {
                _uploadRouteTipsView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _uploadRouteTipsView.Show();
            }
            IsSelected = true;
        }
        public void releaseWindow()
        {
            IsSelected = false;
            this.OnUnchecked();
            _uploadRouteTipsView?.Hide();
            _uploadRouteTipsView = null;
            Console.WriteLine("-----------CloseWindow");
        }

        /// <summary>
        /// 航线名称绑定
        /// </summary>
        public string RouteName
        {
            get { return _routeName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref _routeName, value, "RouteName"); }
        }
    }
}
