using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gvitech.CityMaker.FdeCore;
using Mmc.Framework.Services;
using Mmc.Windows.Utils;

namespace Mmc.Business.IO
{
	// Token: 0x0200000D RID: 13
	public class FileOp
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000030F0 File Offset: 0x000012F0
		public static IFeatureClass OpenShpFile(string shpPath)
		{
			bool flag = !File.Exists(shpPath);
			IFeatureClass result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IDataSource colorUint = null;
				IConnectionInfo connectionInfo = new ConnectionInfo();
				connectionInfo.ConnectionType = gviConnectionType.gviConnectionShapeFile;
				connectionInfo.Database = shpPath;
				connectionInfo.Password = "";
				connectionInfo.Port = 0u;
				bool flag2 = GviMap.DataSourceFactory.HasDataSource(connectionInfo);
				if (flag2)
				{
					colorUint = GviMap.DataSourceFactory.OpenDataSource(connectionInfo);
				}
				bool flag3 = colorUint == null;
				if (flag3)
				{
					result = null;
				}
				else
				{
					ComFactory.ReleaseComObjects(new object[]
					{
						connectionInfo
					});
					string[] featureDatasetNames = colorUint.GetFeatureDatasetNames();
					bool flag4 = featureDatasetNames == null || featureDatasetNames.Length == 0;
					if (flag4)
					{
						result = null;
					}
					else
					{
						IFeatureDataSet featureDataSet = colorUint.OpenFeatureDataset(featureDatasetNames[0]);
						bool flag5 = featureDataSet == null;
						if (flag5)
						{
							result = null;
						}
						else
						{
							string[] namesByType = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
							bool flag6 = namesByType == null || namesByType.Length == 0;
							if (flag6)
							{
								result = null;
							}
							else
							{
								IFeatureClass featureClass = featureDataSet.OpenFeatureClass(namesByType[0]);
								result = featureClass;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000031EC File Offset: 0x000013EC
		public static List<IFeatureDataSet> OpenFdbFile(string fdbPath)
		{
			bool g = !File.Exists(fdbPath);
			List<IFeatureDataSet> b;
			if (g)
			{
				b = null;
			}
			else
			{
				IDataSource fdbDs = null;
				IConnectionInfo a = new ConnectionInfo();
				a.ConnectionType = gviConnectionType.gviConnectionFireBird2x;
				a.Database = fdbPath;
				a.Password = "";
				a.Port = 0u;
				bool flag = GviMap.DataSourceFactory.HasDataSource(a);
				if (flag)
				{
					fdbDs = GviMap.DataSourceFactory.OpenDataSource(a);
				}
				bool flag2 = fdbDs == null;
				if (flag2)
				{
					b = null;
				}
				else
				{
					ComFactory.ReleaseComObjects(new object[]
					{
						a
					});
					string[] r = fdbDs.GetFeatureDatasetNames();
					bool flag3 = r == null || r.Length == 0;
					if (flag3)
					{
						b = null;
					}
					else
					{
						b = (from name in r
						select fdbDs.OpenFeatureDataset(name)).ToList<IFeatureDataSet>();
					}
				}
			}
			return b;
		}
	}
}
