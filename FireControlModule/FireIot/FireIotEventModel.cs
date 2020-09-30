using FireControlModule.InsideBuild;
using FireControlModule.VideoMonitor;
using Mmc.Mspace.ToolModule.Search;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using Mmc.Wpf.Toolkit.MarkupExtensions;
using System;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace FireControlModule.FireIot
{
    public class FireIotEventModel : BindableBase
    {
        private string address;
        private string code;
        private string createdate;
        private FloorMapViewModel floorMd;
        private Visibility isVisible;
        private KeyWordSearchViewModel kwMd;
        private string name;
        private string sensorType;
        private string status;
        private string telPepple;
        private string telPhone;
        private string value;
        private string valueRange;
        private VideoMonitorExViewModel videoMd;

        public FireIotEventModel()
        {
            videoMd = new VideoSingleViewModel() { Content = "视频监控" };
            floorMd = new FloorMapViewModel() { Content = "平面图" };
            kwMd = new KeyWordSearchViewModel() { Content = "周边设施" };
            kwMd.SetBufferGeoVisible(true);
            this.LocateCmd = new RelayCommand(delegate (object p)
            {
                var codeInfo = p as Tuple<string, string>;
                if (codeInfo != null)
                {
                    string code = string.Format("{0}@{1}", codeInfo.Item1, codeInfo.Item2);
                    InsideBuildCmd.OpenInside3D(code);
                }
            });

            this.ShowVideoCmd = new RelayCommand(delegate ()
            {
                if (videoMd != null)
                    videoMd.OnChecked();
            });

            this.ShowFloorMapCmd = new RelayCommand(delegate (object p)
            {
                var codeInfo = p as Tuple<string, string>;
                string filePathName = string.Empty;
                if (codeInfo != null && floorMd != null)
                {
                    if (CodeInfo.Item2 == "171002000017")
                    {
                        floorMd.ImgName = "pack://siteoforigin:,,,/Resources/FloorMap/金谷工业园二至七层.jpg";
                        filePathName = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\FloorMap\金谷工业园二至七层.jpg";
                    }
                    else if (CodeInfo.Item2 == "171002000016")
                    {
                        floorMd.ImgName = "pack://siteoforigin:,,,/Resources/FloorMap/金谷工业园一层.jpg";
                        filePathName = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\FloorMap\金谷工业园一层.jpg";
                    }
                    // floorMd.OnChecked();

                    //建立新的系统进程
                    System.Diagnostics.Process process = new System.Diagnostics.Process();

                    //设置图片的真实路径和文件名
                    process.StartInfo.FileName = filePathName;

                    //设置进程运行参数，这里以最大化窗口方法显示图片。
                    process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";

                    //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true
                    process.StartInfo.UseShellExecute = true;

                    //此处可以更改进程所打开窗体的显示样式，可以不设
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.Start();
                    process.Close();
                }
            });

            this.ShowFacilityCmd = new RelayCommand(delegate ()
            {
                if (kwMd != null)
                    kwMd.OnChecked();
            });
        }

        public string Address
        {
            get { return this.address; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.address, value, "Address"); }
        }

        public EnumProvider BuildInfo { get; set; }

        public string Code
        {
            get { return this.code; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.code, value, "Code"); }
        }

        public Tuple<string, string> CodeInfo { get; set; }

        public string Createdate
        {
            get { return this.createdate; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.createdate, value, "Createdate"); }
        }

        public Visibility IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                base.SetAndNotifyPropertyChanged<Visibility>(ref this.isVisible, value, "IsVisible");
            }
        }

        [XmlIgnore]
        public ICommand LocateCmd { get; set; }

        public string Name
        {
            get { return this.name; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.name, value, "Name"); }
        }

        //public FireIotInfo FireIotInfo { get; set; }
        public string SensorType
        {
            get { return this.sensorType; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.sensorType, value, "SensorType"); }
        }

        [XmlIgnore]
        public ICommand ShowFacilityCmd { get; set; }

        [XmlIgnore]
        public ICommand ShowFloorMapCmd { get; set; }

        [XmlIgnore]
        public ICommand ShowVideoCmd { get; set; }

        public string Status
        {
            get { return this.status; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.status, value, "Status"); }
        }

        public string TelPepple
        {
            get { return this.telPepple; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.telPepple, value, "TelPepple"); }
        }

        public string TelPhone
        {
            get { return this.telPhone; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.telPhone, value, "TelPhone"); }
        }

        public string Value
        {
            get { return this.value; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.value, value, "Value"); }
        }

        public string ValueRange
        {
            get { return this.valueRange; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.valueRange, value, "ValueRange"); }
        }
    }
}