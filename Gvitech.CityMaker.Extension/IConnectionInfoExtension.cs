using Gvitech.CityMaker.FdeCore;
using System;
using System.IO;

public static class IConnectionInfoExtension
{
	public static string GetDataSourceName(this IConnectionInfo @this)
	{
		bool flag = @this == null;
		if (flag)
		{
			throw new ArgumentNullException("this");
		}
		gviConnectionType connectionType = @this.ConnectionType;
		if (connectionType != gviConnectionType.gviConnectionFireBird2x && connectionType != gviConnectionType.gviConnectionShapeFile && connectionType != gviConnectionType.gviConnectionCms7Http)
		{
			throw new Exception("conn的类型未做处理");
		}
		return Path.GetFileNameWithoutExtension(@this.Database);
	}
}
