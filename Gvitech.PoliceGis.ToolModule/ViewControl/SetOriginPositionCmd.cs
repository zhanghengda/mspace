using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Framework.Wpf.Core;
using Mmc.Mspace.Common.Cache;
using Mmc.Mspace.Const.ConstDataBase;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Mmc.Mspace.ToolModule.ViewControl
{
    public class SetOriginPositionCmd : MapCommand
    {
        public override void Execute(object parameter)
        {
            //throw new NotImplementedException();
            this.SetOriginPosition();
        }

        private void SetOriginPosition()
        {
            IPoint state;
            IEulerAngle eulerAngle;
            base.MapControl.Camera.GetCamera2(out state, out eulerAngle);
            string path = AppDomain.CurrentDomain.BaseDirectory + ConfigPath.ShellConfig;
            string x = state.X.ToString();
            string y = state.Y.ToString();
            string z = state.Z.ToString();
            string heading = eulerAngle.Heading.ToString();
            string roll = eulerAngle.Roll.ToString();
            string tilt = eulerAngle.Tilt.ToString();
            string content = x + ";" + y + ";" + z + ";" + heading + ";" + tilt + ";" + roll;
            var result = CacheData.SaveConfig("OriginCamera", content);
            ICoordSysDialog sysDialog = new CoordSysDialog();
            string strWKT = sysDialog.ShowDialog(gviLanguage.gviLanguageChineseSimple);
        }
    }
}
