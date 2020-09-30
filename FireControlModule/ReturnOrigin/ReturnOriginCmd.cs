using FireControlModule.UnitInfo;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Common.Commands;
using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using System;
using System.Configuration;

namespace FireControlModule.ReturnOrigin
{
    public class ReturnOriginCmd : BarCmd
    {
        public override void Execute(object parameter)
        {
            string OriginCamera = ConfigurationManager.AppSettings["OriginCamera"];
            string[] array = OriginCamera.Split(new char[] { ';' });
            if (!(array == null || array.Length != 6))
            {
                double num = StringExtension.ParseTo<double>(array[0], 0.0);
                double num2 = StringExtension.ParseTo<double>(array[1], 0.0);
                double num3 = StringExtension.ParseTo<double>(array[2], 0.0);
                double num4 = StringExtension.ParseTo<double>(array[3], 0.0);
                double num5 = StringExtension.ParseTo<double>(array[4], 0.0);
                double num6 = StringExtension.ParseTo<double>(array[5], 0.0);
                GviMap.AxMapControl.Camera.FlyTime = 1;
                GviMap.AxMapControl.Camera.SetCamera(num, num2, num3, num4, num5, num6, GviMap.SpatialCrs, gviSetCameraFlags.gviSetCameraNoFlags);
            }
        }
    }
}