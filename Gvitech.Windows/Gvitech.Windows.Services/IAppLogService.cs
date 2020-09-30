using System;

namespace Mmc.Windows.Services
{
	public interface IAppLogService
	{
		void InitSysLog(string logDir);

		void Log(string message, LogMessageType type = LogMessageType.INFO);

		void Log(Exception message);
	}
}
