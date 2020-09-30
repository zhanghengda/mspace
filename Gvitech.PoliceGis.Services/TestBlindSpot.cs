using Gvitech.CityMaker.RenderControl;
using Mmc.Framework.Services;
using Mmc.Mspace.Models.VideoMonitor;
using Mmc.Windows.Utils;
using System;

namespace Mmc.Mspace.Services
{
    public class TestBlindSpot
    {
        public void Clear()
        {
            bool flag = this.tr != null;
            if (flag)
            {
                GviMap.MapControl.ObjectManager.DeleteObject(this.tr.Guid);
            }
        }

        public void DrawBlindSpotArea()
        {
            CameraInfo testCameraInfo = CameraInfo.GetTestCameraInfo();
            IPosition position = new Position();
            position.Heading = testCameraInfo.Heading;
            position.Tilt = testCameraInfo.Tilt;
            position.X = testCameraInfo.PtCenter.X;
            position.Y = testCameraInfo.PtCenter.Y;
            position.Altitude = testCameraInfo.PtCenter.Z;
            this.tr = GviMap.MapControl.ObjectManager.CreatePyramid(position, testCameraInfo.HeadRange, testCameraInfo.Deepth, testCameraInfo.TiltRange, ColorConvert.UintToColor(4278255615u), ColorConvert.UintToColor(1157562368u), default(Guid));
            ICameraExtension.FlyToEnvelope(GviMap.MapControl.Camera, this.tr.Envelope, null, 2f, 0.5, null);
        }

        private ITerrain3DRectBase tr;
    }
}