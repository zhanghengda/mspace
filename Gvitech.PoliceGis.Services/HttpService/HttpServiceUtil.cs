using Mmc.Mspace.Models.HttpResult;
using Mmc.Windows.Design;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpServiceUtil
    {
        public static HttpResult<T> RequestResult<T>(string hsKey, string param, Dictionary<string, Type> typeDny) where T : class, new()
        {
            bool flag = string.IsNullOrEmpty(hsKey);
            HttpResult<T> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IHttpServiceConfigService service = ServiceManager.GetService<IHttpServiceConfigService>(null);
                HttpServiceParam serviceParam = service.GetServiceParam(hsKey);
                bool flag2 = serviceParam == null;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    bool unobstructed = serviceParam.Unobstructed;
                    bool enableCacheData = service.EnableCacheData;
                    string url = serviceParam.Url;
                    string text = string.Format("{0}{1}", Application.StartupPath, serviceParam.JsonFile);
                    try
                    {
                        SystemLog.Log(string.Format("开始请求{0}", hsKey), 0);
                        string text2 = (unobstructed && !string.IsNullOrEmpty(url)) ? HttpServiceUtil.httpService.HttpPost(url, param, 3000) : ((enableCacheData && !string.IsNullOrEmpty(text) && File.Exists(text)) ? StringExtension.ReadFile(text, Encoding.GetEncoding("gbk"), FileMode.Open) : string.Empty);
                        bool flag3 = string.IsNullOrEmpty(text2) || text2.Equals("null");
                        if (flag3)
                        {
                            return null;
                        }
                        SystemLog.Log(string.Format("已经获取到{0}请求结果字符串", hsKey), 0);
                        SystemLog.Log(string.Format("开始解析{0}", hsKey), 0);
                        return Singleton<RequestResultReSolve>.Instance.ReSolveRequestResult<T>(text2, typeDny);
                    }
                    catch (WebException ex)
                    {
                        bool flag4 = !string.IsNullOrEmpty(url);
                        if (flag4)
                        {
                            int num = url.IndexOf("://") + 3;
                            int num2 = url.IndexOf(':', num);
                            serviceParam.Unobstructed = ServiceManager.GetService<INetWorkCheckService>(null).IsUnobstructed(url.Substring(num, num2 - num));
                            SystemLog.Log(serviceParam.Unobstructed ? ex.StackTrace : "连接失败", 0);
                        }
                        SystemLog.Log(ex);
                    }
                    catch (Exception ex2)
                    {
                        SystemLog.Log(string.Format("解析{0}异常", hsKey), LogMessageType.ERROR);
                        SystemLog.Log(ex2);
                        throw ex2;
                    }
                    result = null;
                }
            }
            return result;
        }

        public static string Token = string.Empty;

        private static HttpService httpService = new HttpService();

        public static string MspaceVersion = string.Empty;

        public static string MspaceHostUrl = string.Empty;

    }
}