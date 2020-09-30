using Mmc.Mspace.Theme.Pop;
using Mmc.Windows.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mmc.Mspace.Services.HttpService
{
    public class HttpException :Exception
    {
        public HttpException(string code) : base(code)
        {
            if (string.IsNullOrEmpty(code)) return;
            throw new Exception(code);
        }

        public static void ShowHttpExcetion(string code)
        {
            if (string.IsNullOrEmpty(code)) return;
            SystemLog.Log(string.Format("网络错误：{0}\n", code));
            switch (code)
            {
                case "-1":
                    SystemLog.Log(string.Format("未知网络错误：{0}\n", code));
                    break;
                case "14":
                    Messages.ShowMessage("网络链接超时");
                    break;
                case "401":
                    Messages.ShowMessage("未授权");
                    break;
                case "408":
                    Messages.ShowMessage("请求超时");
                    break;
                case "504":
                    Messages.ShowMessage("服务器超时");
                    break;
            }
        }
    }
}
