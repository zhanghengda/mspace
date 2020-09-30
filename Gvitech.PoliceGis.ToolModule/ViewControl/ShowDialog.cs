using Gvitech.CityMaker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class ShowDialog : ICoordSysDialog
    {
        string ICoordSysDialog.ShowDialog(gviLanguage Language)
        {
            return "0";
        }
    }
}
