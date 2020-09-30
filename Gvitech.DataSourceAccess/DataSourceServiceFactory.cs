using System;
using System.IO;
using Gvitech.CityMaker.Controls;
using Gvitech.CityMaker.FdeCore;

namespace Mmc.DataSourceAccess
{
	
	public class DataSourceServiceFactory
	{
		
		public static IDataSourceService FdbDataSourceService<TFeatureDataSet>(string fdbFile, string pswd, AxRenderControl rc) where TFeatureDataSet : class, IGvitechFeatureDataSet
		{
			bool flag = string.IsNullOrEmpty(fdbFile);
			if (flag)
			{
				throw new ArgumentNullException("fdbFile");
			}
			bool flag2 = !File.Exists(fdbFile);
			if (flag2)
			{
				throw new FileNotFoundException(fdbFile);
			}
			return new DataSourceService<TFeatureDataSet>(rc, DataSourceServiceFactory.CreateFdbConnectionInfo(fdbFile, pswd));
		}

		
		private static IConnectionInfo CreateFdbConnectionInfo(string fdbFile, string pswd)
		{
			bool flag = string.IsNullOrEmpty(fdbFile);
			IConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ConnectionInfo
				{
					ConnectionType = gviConnectionType.gviConnectionFireBird2x,
					Database = fdbFile,
					Password = pswd
				};
			}
			return result;
		}
	}
}
