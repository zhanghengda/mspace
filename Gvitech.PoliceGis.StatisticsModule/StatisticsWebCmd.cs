using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;

namespace Mmc.Mspace.StatisticsModule
{
    public class StatisticsWebCmd : SimpleCommand
    {
        StatisticsWebView view = null;
        private string url;
        public StatisticsWebCmd()
        {
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            url = json1.staticUrl;
        }
        public StatisticsWebView View
        {
            get
            {
                return view;
            }
            set
            { view = value; }
        }

        public override void Execute(object parameter)
        {

            bool position = parameter.ParseTo(false);
            if (position)
            {
                view.webCtrl.Navigate(new Uri(url));
                view.Show();
            }
            else
            {
                CloseforRestore();
            }
        }

        public void CloseforRestore()
        {
            if (View != null)
            {
                //  ((StatisticsWebViewModel)view.DataContext).OnUnchecked();
                View.Close();
                View = null;
            }
        }
    }
}
