using Mmc.Mspace.Models.HttpResult;
using Mmc.Mspace.Models.Video;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mmc.Mspace.Services.HttpService
{
    public class VideoHttpService : Singleton<VideoHttpService>, IVideoHttpService
    {
        public static VideoHttpService GetDefault(object obj)
        {
            return Singleton<VideoHttpService>.Instance;
        }

        public VideoHttpService()
        {
            this.httpService = new HttpService();
            VideoHttpService.VideoInfos = new List<VideoInfo>();
        }

        public List<VideoInfo> GetVideoInfos(params string[] ids)
        {
            bool flag = CollectionExtension.HasValues<string>(ids);
            List<VideoInfo> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = !IEnumerableExtension.HasValues<VideoInfo>(VideoHttpService.VideoInfos);
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    IEnumerable<VideoInfo> enumerable = from video in VideoHttpService.VideoInfos
                                                        where ids.Contains(video.BH)
                                                        select video;
                    result = ((enumerable == null) ? null : enumerable.ToList<VideoInfo>());
                }
            }
            return result;
        }

        private List<VideoInfo> GetVideoInfos(string[] sbbhs, int curPage, int pageCapacity)
        {
            List<VideoInfo> list = null;
            List<VideoInfo> result;
            try
            {
                SystemLog.Log("开始请视频信息", 0);
                string hbs = "[";
                bool flag = CollectionExtension.HasValues<string>(sbbhs);
                if (flag)
                {
                    IEnumerableExtension.ForEach<string>(sbbhs, delegate (string bh)
                    {
                        hbs = hbs + "\"" + bh + "\",";
                    });
                    hbs.Remove(hbs.Length - 1, 1);
                }
                hbs += "]";
                HttpResult<HttpVideoInfos> httpResult = HttpServiceUtil.RequestResult<HttpVideoInfos>("视频信息服务", string.Format("[{0},{1},{2}]", hbs, curPage, pageCapacity), VideoHttpService.typeDny);
                bool flag2 = httpResult == null || httpResult.RequestResult == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    httpResult.RequestResult.InfoSPJKs.ForEach(delegate (VideoInfo v)
                    {
                        CollectionExtension.AddEx<VideoInfo>(VideoHttpService.VideoInfos, v);
                    });
                    result = httpResult.RequestResult.InfoSPJKs;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("解析视频信息异常", LogMessageType.ERROR);
                SystemLog.Log(ex);
                result = list;
            }
            return result;
        }

        private void GetAllVideoInfo()
        {
            bool flag = IEnumerableExtension.HasValues<VideoInfo>(VideoHttpService.VideoInfos);
            if (!flag)
            {
                VideoHttpService.VideoInfos = new List<VideoInfo>();
                try
                {
                    int num = 1;
                    string[] sbbhs = null;
                    int num2 = num;
                    num = num2 + 1;
                    List<VideoInfo> videoInfos = this.GetVideoInfos(sbbhs, num2, 15000);
                    bool flag2 = IEnumerableExtension.HasValues<VideoInfo>(videoInfos);
                    if (flag2)
                    {
                        VideoHttpService.VideoInfos.AddRange(videoInfos);
                    }
                }
                catch (Exception ex)
                {
                    SystemLog.Log("解析案件信息异常", LogMessageType.ERROR);
                    SystemLog.Log(ex);
                }
            }
        }

        public VideoInfo GetVideoInfo(string id)
        {
            bool flag = string.IsNullOrEmpty(id);
            VideoInfo result;
            if (flag)
            {
                result = null;
            }
            else
            {
                VideoInfo videoInfo = null;
                bool flag2 = IEnumerableExtension.HasValues<VideoInfo>(VideoHttpService.VideoInfos);
                if (flag2)
                {
                    videoInfo = VideoHttpService.VideoInfos.Find((VideoInfo video) => id.Equals(video.BH));
                }
                bool flag3 = videoInfo == null;
                if (flag3)
                {
                    videoInfo = this.GetVideoInfos(new string[]
                    {
                        id
                    }, 1, 10).FirstOrDefault<VideoInfo>();
                }
                result = videoInfo;
            }
            return result;
        }

        private IHttpService httpService = null;

        private static string videoUrl = "http://10.197.7.33:20031/policeDataService-yc/BSV_YC_017";

        private static Dictionary<string, Type> typeDny = new Dictionary<string, Type>
        {
            {
                "infoSPJKs",
                typeof(VideoInfo)
            }
        };

        private static List<VideoInfo> VideoInfos;
    }
}