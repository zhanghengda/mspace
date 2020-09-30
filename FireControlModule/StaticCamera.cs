using Gvitech.CityMaker.FdeGeometry;
using Gvitech.CityMaker.Math;
using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;

namespace FireControlModule
{
    public class StaticCamera
    {
        public static void SetCamera(double x = 496525.62, double y = 2496395.54, double height = 1917.13,double tilt=-90)
        {
            GviMap.Camera.FlyTime = 0.5;
            IPoint position = null; IEulerAngle eulerAngle = null;
            GviMap.Camera.GetCamera2(out position, out eulerAngle);
            position.SetPostion(x, y, height);
            eulerAngle.Tilt = tilt;
            eulerAngle.Heading = 0.0;
            GviMap.Camera.SetCamera2(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
        }

        //public static void SetCamera1()
        //{
        //    GviMap.Camera.FlyTime = 0.5;
        //    IPoint position = null; IEulerAngle eulerAngle = null;
        //    GviMap.Camera.GetCamera2(out position, out eulerAngle);
        //    position.SetPostion(496609.62, 2496750.54, 7000.13);
        //    eulerAngle.Tilt = -90.0;
        //    eulerAngle.Heading = 0.0;
        //    GviMap.Camera.SetCamera2(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
        //}

        public static void RestoreCamera()
        {
            IPoint position = null; IEulerAngle eulerAngle = null;
            GviMap.Camera.GetCamera2(out position, out eulerAngle);
            eulerAngle.Tilt = -45.0;
            if (position.Z > 2000)
                position.Z = 2000;
            GviMap.Camera.SetCamera2(position, eulerAngle, gviSetCameraFlags.gviSetCameraNoFlags);
        }
    }
}