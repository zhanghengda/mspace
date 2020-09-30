using Gvitech.CityMaker.RenderControl;
using Mmc.Mspace.Models.VideoMonitor;
using System.Collections.Generic;

namespace Mmc.Mspace.Services
{
    public interface ICameraInfoService
    {
        void LoadData();

        List<CameraInfo> GetCameraInfos();

        CameraInfo GetCameraInfo(string cId);

        string GetVideoPath(string cId);

        List<IRenderable> GetDrawCameraViews();

        string GetDeviceId(string fcGuid, int fid);
    }
}