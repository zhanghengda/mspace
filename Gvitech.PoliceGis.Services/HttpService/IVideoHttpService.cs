using Mmc.Mspace.Models.Video;
using System.Collections.Generic;

namespace Mmc.Mspace.Services.HttpService
{
    public interface IVideoHttpService
    {
        VideoInfo GetVideoInfo(string id);

        List<VideoInfo> GetVideoInfos(params string[] ids);
    }
}