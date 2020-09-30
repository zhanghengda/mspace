using Mmc.Mspace.Common.Models;
using Mmc.Mspace.Common.ShellService;
using Mmc.Windows.Services;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Mmc.Mspace.CoreModule
{
    public class WebViewModel : CheckedToolItemModel
    {
        private WebView _view;
        public ICommand CloseCmd { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            base.ViewType = ViewType.CheckedIcon;
            _view = new WebView()
            {
                DataContext = this,
                Owner = ServiceManager.GetService<IShellService>(null).ShellWindow,
            };

            base.Command = new WebViewCmd(_view);
            this.CloseCmd = new RelayCommand(() =>
            {
                if (this._view != null)
                    ((Window)this._view).Hide();
            });
        }

        public virtual void Navigate()
        {
            Navigate(this.requestUrl);
        }

        public virtual void Navigate(string url)
        {
            var cmd = base.Command as WebViewCmd;
            this.requestUrl = url;
            cmd.Execute(this.RequestUrl);
        }


        private string titleName;

        public string TitleName
        {
            get { return this.titleName; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.titleName, value, "TitleName"); }
        }

        private string requestUrl;

        public string RequestUrl
        {
            get { return this.requestUrl; }
            set { base.SetAndNotifyPropertyChanged<string>(ref this.requestUrl, value, "RequestUrl"); }
        }

        public WebView WebView { get => _view; }
    }
}
