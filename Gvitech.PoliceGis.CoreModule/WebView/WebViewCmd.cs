using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.CoreModule
{
    public class WebViewCmd : SimpleCommand
    {
        private WebView view;

        public WebViewCmd(WebView view)
        {
            this.view = view;
        }

        public override void Execute(object parameter)
        {
            var url = parameter.ToString();
            view.Navigate(url);
            view.Show();
        }
    }

}
