using Gvitech.CityMaker.FdeCore;
using System;
using System.Collections.Generic;

public static class IDataSourceExtension
{
	public static List<IFeatureDataSet> OpenAllFeatureDataSet(this IDataSource @this)
	{
		List<IFeatureDataSet> list = new List<IFeatureDataSet>();
		bool flag = @this == null;
		List<IFeatureDataSet> result;
		if (flag)
		{
			result = list;
		}
		else
		{
			string[] featureDatasetNames = @this.GetFeatureDatasetNames();
			bool flag2 = !featureDatasetNames.HasValues<string>();
			if (flag2)
			{
				result = list;
			}
			else
			{
				string[] array = featureDatasetNames;
				for (int i = 0; i < array.Length; i++)
				{
					string featureDataSetName = array[i];
					IFeatureDataSet item = @this.OpenFeatureDataset(featureDataSetName);
					list.Add(item);
				}
				result = list;
			}
		}
		return result;
	}

	public static List<IFeatureClass> OpenAllFeatureClass(this IDataSource @this)
	{
		List<IFeatureClass> list = new List<IFeatureClass>();
		bool flag = @this == null;
		List<IFeatureClass> result;
		if (flag)
		{
			result = list;
		}
		else
		{
			List<IFeatureDataSet> list2 = @this.OpenAllFeatureDataSet();
			foreach (IFeatureDataSet current in list2)
			{
				List<IFeatureClass> list3 = current.OpenAllFeatureClass();
				bool flag2 = !list3.HasValues<IFeatureClass>();
				if (!flag2)
				{
					list.AddRange(list3);
				}
			}
			list2.Clear();
			result = list;
		}
		return result;
	}

	public static int RemoveFeatureDataset(this IDataSource @this)
	{
		bool flag = @this == null;
		int result;
		if (flag)
		{
			result = -1;
		}
		else
		{
			string[] featureDatasetNames = @this.GetFeatureDatasetNames();
			bool flag2 = featureDatasetNames == null || featureDatasetNames.Length == 0;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				string[] array = featureDatasetNames;
				for (int i = 0; i < array.Length; i++)
				{
					string featureDataSetName = array[i];
					IFeatureDataSet featureDataSet = @this.OpenFeatureDataset(featureDataSetName);
					string[] namesByType = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
					bool flag3 = namesByType != null && namesByType.Length != 0;
					if (flag3)
					{
						string[] array2 = namesByType;
						for (int j = 0; j < array2.Length; j++)
						{
							string name = array2[j];
							featureDataSet.DeleteByName(name);
						}
					}
					string[] namesByType2 = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetObjectClassTable);
					bool flag4 = namesByType2 != null && namesByType2.Length != 0;
					if (flag4)
					{
						string[] array3 = namesByType2;
						for (int k = 0; k < array3.Length; k++)
						{
							string name2 = array3[k];
							featureDataSet.DeleteByName(name2);
						}
					}
					@this.DeleteFeatureDataset(featureDataSetName);
				}
				result = @this.GetFeatureDatasetNames().Length;
			}
		}
		return result;
	}

    public static bool IsNetServer(this IDataSource @this)
    {
        IConnectionInfo ci = @this.ConnectionInfo;
        bool result = false;
        gviConnectionType connectionType = ci.ConnectionType;
        switch (connectionType)
        {
            case gviConnectionType.gviConnectionMySql5x:
            case gviConnectionType.gviConnectionOCI11:
            case gviConnectionType.gviConnectionPg9:
            case gviConnectionType.gviConnectionMSClient:
            case gviConnectionType.gviConnectionSQLite3:
            case gviConnectionType.gviConnectionArcGISServer10:
            case gviConnectionType.gviConnectionArcSDE9x:
            case gviConnectionType.gviConnectionArcSDE10x:
            case gviConnectionType.gviConnectionWFS:
                break;
            case gviConnectionType.gviConnectionFireBird2x:
            case (gviConnectionType)7:
            case (gviConnectionType)8:
            case (gviConnectionType)9:
            case gviConnectionType.gviConnectionGBase8t:
            case gviConnectionType.gviConnectionShapeFile:
                return result;
            default:
                if (connectionType != gviConnectionType.gviConnectionCms7Http && connectionType != gviConnectionType.gviConnectionCms7Https)
                {
                    return result;
                }
                break;
        }
        result = true;
        return result;
    }
}
