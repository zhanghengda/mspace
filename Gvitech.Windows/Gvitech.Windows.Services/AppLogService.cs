using Mmc.Windows.Design;
using log4net;
using log4net.Appender;
using log4net.Repository;
using System;
using System.IO;

namespace Mmc.Windows.Services
{
	public class AppLogService : Singleton<AppLogService>, IAppLogService
	{
		private static readonly ILog _errorLog = LogManager.GetLogger("errorLogger");

		private static readonly ILog _infoLog = LogManager.GetLogger("infoLogger");

		public void InitSysLog(string logDir)
		{
			try
			{
				bool flag = !Directory.Exists(logDir);
				if (flag)
				{
					Directory.CreateDirectory(logDir);
				}
				ILoggerRepository repository = LogManager.GetRepository();
				IAppender[] appenders = repository.GetAppenders();
				bool flag2 = appenders != null && appenders.Length != 0;
				if (flag2)
				{
					int i = 0;
					int num = appenders.Length;
					while (i < num)
					{
						FileAppender fileAppender = appenders[i] as FileAppender;
						bool flag3 = fileAppender != null;
						if (flag3)
						{
							bool flag4 = fileAppender.Name == "errorAppender";
							if (flag4)
							{
								string file = Path.Combine(logDir, "log_error");
								fileAppender.File = file;
								fileAppender.ActivateOptions();
							}
							bool flag5 = fileAppender.Name == "infoAppender";
							if (flag5)
							{
								string file2 = Path.Combine(logDir, "log_info");
								fileAppender.File = file2;
								fileAppender.ActivateOptions();
							}
						}
						int num2 = i;
						i = num2 + 1;
					}
				}
			}
			catch (Exception ex)
			{
				Singleton<AppLogService>.Instance.Log(ex.Message, LogMessageType.ERROR);
				bool flag6 = ex.InnerException != null;
				if (flag6)
				{
					Singleton<AppLogService>.Instance.Log(ex.InnerException.Message, LogMessageType.ERROR);
				}
			}
		}

		public void Log(string message, LogMessageType type = LogMessageType.INFO)
		{
			try
			{
				if (type != LogMessageType.INFO)
				{
					if (type == LogMessageType.ERROR)
					{
						AppLogService._errorLog.Error(message);
					}
				}
				else
				{
					AppLogService._infoLog.Info(message);
				}
			}
			catch (Exception ex)
			{
				this.Log(ex);
			}
		}

		public void Log(Exception ex)
		{
			Singleton<AppLogService>.Instance.Log(ex.Message, LogMessageType.ERROR);
			Singleton<AppLogService>.Instance.Log(string.Format("异常信息:{0}\n 调用堆栈:{1}", ex.Message, ex.StackTrace), LogMessageType.ERROR);
		}

		public static IAppLogService GetDefault(object args = null)
		{
			return Singleton<AppLogService>.Instance;
		}


        public void WriteLog(string info)
        {
            if (_infoLog.IsInfoEnabled)
            {
                _infoLog.Info(info);
            }
        }

        public void WriteLog(string info, Exception se)
        {
            if (_errorLog.IsErrorEnabled)
            {
                _errorLog.Error(info, se);
            }
        }
        public void WriteLog(string info, string se)
        {
            if (_errorLog.IsErrorEnabled)
            {
                _errorLog.Error(info+"+"+se);
            }
        }
    }
}
