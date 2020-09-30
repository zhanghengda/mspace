using System;
using System.Configuration;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Services;

namespace MMC.MSpace.Views
{

    public class LicenseUtil
    {

        public static void ValidLicense(AxRenderControl axRenderControl, out int result)
        {
            result = -1;
            string text = StringExtension.ParseTo<string>(ConfigurationManager.AppSettings["LicenseServerIP"], null);
            uint port = StringExtension.ParseTo<uint>(ConfigurationManager.AppSettings["LicenseServerPort"], 0u);
            string password = StringExtension.ParseTo<string>(ConfigurationManager.AppSettings["LicenseServerPwd"], null);
            ILicenseServer licenseServer = new LicenseServer();
            bool flag = string.IsNullOrEmpty(text);
            if (!flag)
            {
                licenseServer.SetHost(text, port, password);
                IInternalLicenseKey internalLicenseKey = axRenderControl as IInternalLicenseKey;
                bool flag2 = internalLicenseKey != null;
                if (flag2)
                {
                    int num = internalLicenseKey.SetLicenseKey("C95A07F7-D73C-4102-B8DA-70F8AF591079");
                    result = ((num >= 0) ? 1 : 0);
                }
            }
        }

        public static bool ValidLicense(AxRenderControl axRenderControl, out string resMsg)
        {
            string hostIP = StringExtension.ParseTo<string>(ConfigurationManager.AppSettings["LicenseServerIP"], null);
            uint port = StringExtension.ParseTo<uint>(ConfigurationManager.AppSettings["LicenseServerPort"], 0u);
            string password = StringExtension.ParseTo<string>(ConfigurationManager.AppSettings["LicenseServerPwd"], null);
            //新建对象
            ILicenseServer licenseServer = new LicenseServer();
            if (string.IsNullOrEmpty(hostIP))
            {
                resMsg = "授权IP为空,请输入授权IP";
                SystemLog.Log(resMsg, LogMessageType.ERROR);
                return false;
            }
            else
            {
                //连接网络服务器或设置本地授权文件（授权服务器IP地址，端口，密码）
                licenseServer.SetHost(@hostIP, port, password);
                //验证服务器是否连接成功
                //bool bSucceed = licenseServer.HasRuntimeLicense();

                // resMsg = string.Format("IP:{0},端口:{1}的授权验证失败，请检测IP端口和密码是否正确", hostIP, port);
                resMsg = string.Format("IP:{0},端口:{1}的授权验证", hostIP, port);
                //return bSucceed;
                return true;
            }
        }




    }
}
