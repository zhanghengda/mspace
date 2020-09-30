using Mmc.Mspace.Common.Models;
using Mmc.Mspace.CoreModule.CoreModel;
using Mmc.Wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Mmc.Mspace.StatisticsModule
{
    public class StatisticsWebViewModel : CheckedToolItemModel
    {
        public override void Initialize()
        {
            base.Initialize();
            var cmd = new StatisticsWebCmd();
            cmd.View = new StatisticsWebView();
            cmd.View.DataContext = this;
            base.Command = cmd;
            base.ViewType = (ViewType)1;

        }

        public override void OnChecked()
        {
            base.OnChecked();
            var cmd = ((StatisticsWebCmd)base.Command);
            cmd.View = new StatisticsWebView();
            cmd.View.DataContext = this;
        }

        public override void OnUnchecked()
        {
            base.OnUnchecked();
          
        }


    }
}
