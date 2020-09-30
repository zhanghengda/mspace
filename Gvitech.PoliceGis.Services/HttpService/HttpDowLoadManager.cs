using Mmc.Mspace.Const.ConstPath;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpDowLoadManager
    {
        public string Token { get; set; }
        public string _poiHost { get; set; }
        public string MspaceVersion { get; set; }

        const int bytebuff = 1024;
        const int ReadWriteTimeOut = 2 * 1000;//超时等待时间
        const int TimeOutWait = 5 * 1000;//超时等待时间
        const int MaxTryTime = 12;
        static Dictionary<string, int> TryNumDic = new Dictionary<string, int>();

        public HttpDowLoadManager()
        {
            var json1 = JsonUtil.DeserializeFromFile<dynamic>(AppDomain.CurrentDomain.BaseDirectory + ConfigPath.WebConnectConfig);
            MspaceVersion = json1.mspaceVersion;
            _poiHost = json1.poiUrl;
        }

        private void SetToken(ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                request.Headers.Add("token", this.Token);
            }
        }

        private void SetVersion(ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(this.MspaceVersion))
            {
                request.Headers.Add("mspace-version", this.MspaceVersion);
            }
        }

        public void DownloadMarkerStatisticsReport(string strFileName, string imgpath,string geom, Action<bool> OnFinsh)
        {
            try
            {
                string url = string.Format("{0}/api/marker/download-report?file_path={1}&geom={2}", _poiHost,imgpath,geom);

                if (!string.IsNullOrEmpty(Token))
                {
                    url = string.Format("{0}&token={1}", url, Token);
                }
                DownloadFile(strFileName, url, OnFinsh);
            }
            catch (Exception ex)
            {
                OnFinsh(false);
            }
        }

        public void DownloadInspectReport( string strFileName, int reportId, Action<bool> OnFinsh)
        {
            try
            {
                string url = string.Format("{0}/api/inspection-file/download?id={1}", _poiHost, reportId);
                if (!string.IsNullOrEmpty(Token))
                {
                    url = string.Format("{0}&token={1}", url, Token);
                }
                DownloadFile(strFileName, url, OnFinsh);
            }
            catch (Exception ex)
            {
                OnFinsh(false);
            }
        }

        public void DownloadReport(string strFileName, string poiid, Action<bool> OnFinsh)
        {
            try
            {
                string url = string.Format("{0}/api/account/exportword?marker_id={1}", _poiHost, poiid);
                if (!string.IsNullOrEmpty(Token))
                {
                    url = string.Format("{0}&token={1}", url, Token);
                }
                DownloadFile(strFileName, url, OnFinsh);
            }
            catch (Exception ex)
            {
                OnFinsh(false);
            }
        }

        #region 以断点续传方式下载文件
        /// <summary>
        /// 以断点续传方式下载文件
        /// </summary>
        /// <param name="strFileName">下载文件的保存路径</param>
        /// <param name="strUrl">文件下载地址</param>
        /// <param name="OnFinsh">回调函数</param>
        private void DownloadFile(string strFileName, string strUrl,Action<bool> OnFinsh)
        {
            //打开上次下载的文件或新建文件
            long SPosition = 0;
            FileStream FStream;
            if (File.Exists(strFileName))
            {
                FStream = File.OpenWrite(strFileName);
                SPosition = FStream.Length;
                FStream.Seek(SPosition, SeekOrigin.Current); //移动文件流中的当前指针
            }
            else
            {
                FStream = new FileStream(strFileName, FileMode.Create);
                SPosition = 0;
            }
            //打开网络连接
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                SetToken(ref myRequest);
                SetVersion(ref myRequest);

                if (SPosition > 0)
                    myRequest.AddRange((int)SPosition);//设置Range值
                //向服务器请求，获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                FStream.Close();
                myStream.Close();
                OnFinsh(true);
            }
            catch
            {
                FStream.Close();
                OnFinsh(false);
            }
        }
        #endregion

    }
}
