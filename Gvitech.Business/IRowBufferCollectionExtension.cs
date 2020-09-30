using System;
using System.Data;
using Gvitech.CityMaker.FdeCore;

// Token: 0x02000006 RID: 6
public static class IRowBufferCollectionExtension
{
	// Token: 0x0600000A RID: 10 RVA: 0x000028E4 File Offset: 0x00000AE4
	public static DataTable ToDataTable(this IRowBufferCollection rowCollection, string[] showFields = null)
	{
		bool listPoiLayer = rowCollection == null;
		if (listPoiLayer)
		{
			throw new NullReferenceException("rowCollection");
		}
		int tempFDataset;
		bool poiLayerProperty = (tempFDataset = rowCollection.Count) < 1;
		DataTable poiDisplayLayer;
		if (poiLayerProperty)
		{
			poiDisplayLayer = null;
		}
		else
		{
			IFieldInfoCollection tempFeatureLayer = null;
			DataTable tempOC = null;
			int dataSource = 0;
			int num;
			for (int rowBuffer = 0; rowBuffer < tempFDataset; rowBuffer = num + 1)
			{
				IRowBuffer tempFC = rowCollection.Get(rowBuffer);
				bool bufStyle = tempFC == null;
				if (!bufStyle)
				{
					bool dataSetNames = tempOC == null;
					if (dataSetNames)
					{
						tempFeatureLayer = tempFC.Fields;
						tempOC = tempFeatureLayer.CreateEmptyTableWithFields(showFields);
						bool fcNames = tempOC == null;
						if (fcNames)
						{
							return null;
						}
						dataSource = tempOC.Columns.Count;
						bool dataSetName = dataSource < 1;
						if (dataSetName)
						{
							return null;
						}
					}
					DataRow dsF = tempOC.NewRow();
					for (int fcName = 0; fcName < dataSource; fcName = num + 1)
					{
						dsF[fcName] = tempFC.GetValue(tempFeatureLayer.FieldIndexOf(tempOC.Columns[fcName].ColumnName));
						num = fcName;
					}
					tempOC.Rows.Add(dsF);
				}
				num = rowBuffer;
			}
			poiDisplayLayer = tempOC;
		}
		return poiDisplayLayer;
	}
}
