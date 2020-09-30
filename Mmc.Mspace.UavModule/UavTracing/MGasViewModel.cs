using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System.Diagnostics;

using Mmc.Windows.Utils;
using Mmc.Mspace.Common.Models;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using Mmc.Mspace.UavModule.Dto;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

using Newtonsoft.Json;
using Mmc.Mspace.Common.ShellService;
using System.Device.Location;

namespace Mmc.Mspace.UavModule.UavTracing
{
    public class MGasViewModel : CheckedToolItemModel
    {
        public string ground_serial { get; set; }
        public string ground_state { get; set; }

        private MGasView _mountGasView;
        private Window shell;

        private int _mountType;
        private string _camCamPitchAngle;
        private string _camCamHeadAngle;
        private string _camCamRoolAngle;
        private string _camCamZoom;

        [XmlIgnore]
        public int mountType
        {
            get { return this._mountType; }
            set { base.SetAndNotifyPropertyChanged<int>(ref this._mountType, value, "mountType"); }
        }

        [XmlIgnore]
        public string camCamPitchAngle
        {
            get { return this._camCamPitchAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamPitchAngle, value, "camCamPitchAngle"); }
        }

        [XmlIgnore]
        public string camCamHeadAngle
        {
            get { return this._camCamHeadAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamHeadAngle, value, "camCamHeadAngle"); }
        }
        [XmlIgnore]
        public string camCamRoolAngle
        {
            get { return this._camCamRoolAngle; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamRoolAngle, value, "camCamRoolAngle"); }
        }
        [XmlIgnore]
        public string camCamZoom
        {
            get { return this._camCamZoom; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this._camCamZoom, value, "camCamZoom"); }
        }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }
        public string WindowTitle { get; set; }
        public void releaseWindow()
        {
            _mountGasView = null;
        }

        public override void OnChecked()
        {
            base.OnChecked();
            shell = ServiceManager.GetService<IShellService>(null).ShellWindow;
            WindowTitle = "气体检测";
            if (_mountGasView == null)
                _mountGasView = new MGasView();
            this._mountGasView.Owner = Application.Current.MainWindow;
            //this._mountControlView.Width = Width;
            //this._mountControlView.Height = 350;
            //this._mountControlView.Left = 10;
            //this._mountControlView.Top = this.OffsetHeight + 15;
            _mountGasView.DataContext = this;
            _mountGasView.Show();
        }
        public override void Initialize()
        {
            base.Initialize();
            this.camCamHeadAngle = "1.06502184401";
            this.camCamPitchAngle = "0.8829054252";
            this.camCamRoolAngle = "0.000124858";
            this.camCamZoom = "0";

            this.CloseCmd = new RelayCommand(() =>
            {
                base.IsChecked = false;
                _mountGasView?.Close();
                this.releaseWindow();

            });
        }


    }
}
