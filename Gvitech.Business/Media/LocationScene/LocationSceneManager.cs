using System;
using System.Collections.ObjectModel;
using Mmc.Business.Utils;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Mmc.Windows.Utils;

namespace Mmc.Business.Media.LocationScene
{
	// Token: 0x0200000C RID: 12
	public sealed class LocationSceneManager
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static ObservableCollection<LocationScene> GetLocationScenes(IFeatureDataSet dataSet, string filterString = "")
		{
			ObservableCollection<LocationScene> str = new ObservableCollection<LocationScene>();
			try
			{
				IObjectClass a = dataSet.OpenObjectClass("UP_Location");
				bool r = a != null;
				if (r)
				{
					IQueryFilter g = QueryFilterFactory.CreateQueryFilter<QueryFilter>(filterString, null, null, null, null);
					IFdeCursor b = a.Search(g, true);
					bool flag = b == null;
					if (flag)
					{
						return str;
					}
					IRowBuffer rowBuffer;
					while ((rowBuffer = b.NextRow()) != null)
					{
						LocationScene locationScene = new LocationScene();
						locationScene.ID = (int)rowBuffer.GetValue(0);
						locationScene.Location = rowBuffer.GetValue(rowBuffer.FieldIndex("location")).ParseTo<string>(null);
						locationScene.Name = rowBuffer.GetValue(rowBuffer.FieldIndex("locationName")).ParseTo<string>(null);
						locationScene.Comment = rowBuffer.GetValue(rowBuffer.FieldIndex("comment")).ParseTo<string>(null);
						locationScene.Duration = rowBuffer.GetValue(rowBuffer.FieldIndex("duration")).ParseTo(0.0);
						locationScene.GroupID = rowBuffer.GetValue(rowBuffer.FieldIndex("locationGroupID")).ParseTo(0);
						locationScene.ConnectionType = (ConnectionType)rowBuffer.GetValue(rowBuffer.FieldIndex("ConnType")).ParseTo(0);
						locationScene.PlanID = rowBuffer.GetValue(rowBuffer.FieldIndex("PlanId")).ParseTo(0);
						locationScene.GroupType = (GroupType)rowBuffer.GetValue(rowBuffer.FieldIndex("PlanType")).ParseTo(0);
						locationScene.Index = rowBuffer.GetValue(rowBuffer.FieldIndex("LocationIndex")).ParseTo(0);
						IBinaryBuffer binaryBuffer = (IBinaryBuffer)rowBuffer.GetValue(rowBuffer.FieldIndex("image"));
						locationScene.Image = ((binaryBuffer != null) ? binaryBuffer.AsByteArray() : null);
						str.Add(locationScene);
					}
					ComFactory.ReleaseComObjects(new object[]
					{
						b,
						rowBuffer,
						g,
						a
					});
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return str;
		}
	}
}
