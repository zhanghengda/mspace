using Mmc.Mspace.CoreModule;
using Mmc.Wpf.Commands;
using Mmc.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.PoliceResourceModule.VideoMonitor
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class WebVideoViewModel : BindableBase
    {
        private WebView webView;
        public WebVideoViewModel()
        {
            webView = new WebView();
            webView.DataContext = this;
            webView.WebCtrl.ObjectForScripting = this;
            string url = AppDomain.CurrentDomain.BaseDirectory + @"data\html\VideoOne.html";
            Uri uri = new Uri(url);
            webView.WebCtrl.Navigate(url);
            webView.Show();
            this.CloseCmd = new RelayCommand(() =>
            {
                webView.Hide();
            });
        }

        [XmlIgnore]
        public ICommand CloseCmd { get; set; }

        private string webViewHeight = "500";
        private string webViewWidth = "500";
        private string titleName = "视频监控";
        public string WebViewHeight
        {
            get { return this.webViewHeight; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.webViewHeight, value, "WebViewHeight"); }
        }

        public string WebViewWidth
        {
            get { return this.webViewWidth; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.webViewWidth, value, "WebViewWidth"); }
        }


        public string TitleName
        {
            get { return this.titleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.titleName, value, "TitleName"); }
        }


        public void SetVideoPath(int videoIndex)
        {
            try
            {
               
               

                //object[] objects = new object[1];
                //objects[0] = "C#访问JavaScript脚本";
                //webView.WebCtrl.InvokeScript("messageBox", objects);
                webView.WebCtrl.InvokeScript("openOneVideo", 4);

            }
            catch (Exception ex)
            {
                Mmc.Windows.Services.SystemLog.Log(ex);
            }

        }

    }
}
