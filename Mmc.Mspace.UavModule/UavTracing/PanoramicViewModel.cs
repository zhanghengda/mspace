using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Mspace.Services.HttpService;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.UavModule.UavTracing
{
    class PanoramicViewModel : CheckedToolItemModel
    {
        private PanoramicView panoramicView;

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string WindowTitle { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
            OnChecked();
        }

        public override void OnChecked()
        {
            base.OnChecked();
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = string.Format("{0}/video/full-screen", json1.poiUrl);

            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Dispatcher.Invoke(() =>
            {
                if (panoramicView == null)
                    panoramicView = new PanoramicView();
                this.panoramicView.Owner = Application.Current.MainWindow;
                //this.panoramicView.Width = Width;
                //this.panoramicView.Height = 259;
                //this.panoramicView.Left = 10;
                //this.panoramicView.Top = this.OffsetHeight + 15;
                panoramicView.DataContext = this;
                panoramicView.webCtrl.Navigate(new Uri(url));
                panoramicView.Show();
            });
        }
        public double OffsetHeight { get; set; }
        public double Width { get; set; }

        public string ground_serial { get; set; }
        public override void OnUnchecked()
        {
            base.OnUnchecked();
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Dispatcher.Invoke(() =>
            {
                panoramicView?.webCtrl.Dispose();
                panoramicView?.Close();
                panoramicView = null;
            });
        }
    }
}
