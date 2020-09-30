using Mmc.Windows.Design;
using System;

namespace Mmc.Windows.Services
{
	public class SystemLog
	{
		public static void InitSysLog(string logDir)
		{
			Singleton<AppLogService>.Instance.InitSysLog(logDir);
		}

		public static void Log(string message, LogMessageType type = LogMessageType.INFO)
		{
			Singleton<AppLogService>.Instance.Log(message, type);
		}

		public static void Log(Exception message)
		{
			Singleton<AppLogService>.Instance.Log(message);
		}
        /// <summary>
        /// 日志信息
        /// </summary>
        /// <param name="title"></param>
        public static void WriteLog(string message)
        {
            Singleton<AppLogService>.Instance.WriteLog(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void WriteLog(string title, Exception message)
        {
            Singleton<AppLogService>.Instance.WriteLog(title, message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="message"></param>
        public static void WriteLog(string title, string message)
        {
            Singleton<AppLogService>.Instance.WriteLog(title, message);
        }
    }
}
