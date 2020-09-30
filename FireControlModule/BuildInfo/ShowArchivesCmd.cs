using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;

namespace FireControlModule.BuildInfo
{
    public class ShowArchivesCmd : SimpleCommand
    {
        private ArchivesView view;

        public ShowArchivesCmd(ArchivesView view)
        {
            this.view = view;
        }

        public override void Execute(object parameter)
        {
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            string url = json1.buildArchivesUrl + parameter; ;
            view.Navigate(url);
            view.Show();
        }
    }
}