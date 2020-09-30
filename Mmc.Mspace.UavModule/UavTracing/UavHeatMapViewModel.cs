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
    public class UavHeatMapViewModel : CheckedToolItemModel
    {
        private UavHeatMapView heatMapView;
        HttpService _httpService;
        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        public string WindowTitle { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _httpService = new HttpService();
            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
            });
        }

        public override void OnChecked()
        {
            base.OnChecked();
            _httpService.Token = HttpServiceUtil.Token;
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json1.uavHeatMapUrl + "?" + _httpService.Token;
            var shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            shell.Dispatcher.Invoke(() =>
            {
                if (heatMapView == null)
                    heatMapView = new UavHeatMapView();
                this.heatMapView.Owner = Application.Current.MainWindow;
                double dWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
                double dHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
                this.heatMapView.Width = dWidth;
                this.heatMapView.Height = dHeight;
                this.heatMapView.Left = 0;
                this.heatMapView.Top = 0;
                heatMapView.DataContext = this;
                heatMapView.webCtrl.Navigate(new Uri(url));
                heatMapView.Show();
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
                heatMapView?.webCtrl.Dispose();
                heatMapView?.Close();
                heatMapView = null;
            });
        }
    }
}
