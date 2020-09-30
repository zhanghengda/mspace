using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Services;
using Mmc.Windows.Utils;
using Mmc.Wpf.Commands;
using System;

namespace FireControlModule.BuildInfo
{
    public class UnitListCmd : SimpleCommand
    {
        private ArchivesView view;

        public UnitListCmd(ArchivesView view)
        {
            this.view = view;
        }

        public override void Execute(object parameter)
        {
            try
            {
                if (parameter == null)
                    return;
                if (parameter is bool)
                {
                    if (parameter.ParseTo<bool>())
                    {
                        var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
                        string url = json1.threeUnitUrl;
                        view.webCtrl.Navigate(new Uri(url));
                        view.Show();
                    }
                    else
                    {
                        view.Hide();
                    }
                }
                else
                {
                    var url = parameter.ToString();
                    view.webCtrl.Navigate(new Uri(url));
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log(ex);
            }
        }
    }
}