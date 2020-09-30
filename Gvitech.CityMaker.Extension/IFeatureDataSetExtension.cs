using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.Resource;
using Mmc.Windows.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

public static class IFeatureDataSetExtension
{
	public static List<IFeatureClass> OpenAllFeatureClass(this IFeatureDataSet @this)
	{
		List<IFeatureClass> list = new List<IFeatureClass>();
		string[] namesByType = @this.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
		bool flag = !namesByType.HasValues<string>();
		List<IFeatureClass> result;
		if (flag)
		{
			result = list;
		}
		else
		{
			string[] array = namesByType;
			for (int i = 0; i < array.Length; i++)
			{
				string name = array[i];
				IFeatureClass item = @this.OpenFeatureClass(name);
				list.Add(item);
			}
			result = list;
		}
		return result;
	}

	public static bool AddImageResouce(this IFeatureDataSet @this, string imagePath, bool isOverride = true)
	{
		bool flag = @this == null || string.IsNullOrEmpty(imagePath) || !Directory.Exists(imagePath);
		bool result;
		if (flag)
		{
			result = false;
		}
		else
		{
			IEnumerable<string> enumerable = Directory.EnumerateFiles(imagePath);
			IResourceFactory resourceFactory = new ResourceFactory();
			IResourceManager resourceManager = @this as IResourceManager;
			bool flag2 = resourceManager == null;
			if (flag2)
			{
				result = false;
			}
			else
			{
				int num = 0;
				foreach (string current in enumerable)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(current);
					IImage image = resourceFactory.CreateImageFromFile(current);
					bool flag3 = image == null;
					if (!flag3)
					{
						bool flag4 = resourceManager.ImageExist(fileNameWithoutExtension);
						if (flag4)
						{
							if (!isOverride)
							{
								continue;
							}
							resourceManager.DeleteImage(fileNameWithoutExtension);
						}
						bool flag5 = !resourceManager.AddImage(fileNameWithoutExtension, image);
						if (flag5)
						{
							int num2 = num;
							num = num2 + 1;
						}
					}
				}
				ComFactory.ReleaseComObjects(new object[]
				{
					resourceFactory
				});
				result = (num <= 0);
			}
		}
		return result;
	}

	public static DataSet OpenModelLib(this IFeatureDataSet @this)
	{
		bool flag = @this == null;
		DataSet result;
		if (flag)
		{
			result = null;
		}
		else
		{
			DataSet dataSet = new DataSet();
			string[] namesByType = @this.GetNamesByType(gviDataSetType.gviDataSetObjectClassTable);
			bool flag2 = namesByType == null || namesByType.Length < 1;
			if (flag2)
			{
				result = null;
			}
			else
			{
				List<string> list = namesByType.ToList<string>();
				List<string> list2 = new List<string>();
				bool flag3 = !list.Contains("ModelLib");
				if (flag3)
				{
					result = null;
				}
				else
				{
					IObjectClass objectClass = @this.OpenObjectClass("ModelLib");
					bool flag4 = objectClass == null;
					if (flag4)
					{
						result = null;
					}
					else
					{
						IFieldInfoCollection fields = objectClass.GetFields();
						int num = fields.IndexOf("name");
						int num2 = fields.IndexOf("namealias");
						int num3 = fields.IndexOf("namealiasindex");
						bool flag5 = num < 0 || num2 < 0 || num3 < 0;
						if (flag5)
						{
							result = null;
						}
						else
						{
							IQueryFilter filter = new QueryFilter();
							IFdeCursor fdeCursor = objectClass.Search(filter, false);
							IRowBuffer rowBuffer;
							while ((rowBuffer = fdeCursor.NextRow()) != null)
							{
								string item = rowBuffer.GetValue(num2).ToString();
								list2.Add(item);
							}
							foreach (string current in list2)
							{
								IObjectClass objectClass2 = @this.OpenObjectClass(current);
								bool flag6 = !objectClass2.HasSubTypes;
								if (flag6)
								{
									result = null;
									return result;
								}
								int subTypeCount = objectClass2.SubTypeCount;
								int num4;
								for (int i = subTypeCount - 1; i > -1; i = num4 - 1)
								{
									DataTable dataTable = new DataTable();
									dataTable.Columns.Add("Image", typeof(byte[]));
									dataTable.Columns.Add("ImageName", typeof(string));
									string name = objectClass2.GetSubType(i).Name;
									dataTable.TableName = (dataSet.Tables.Contains(name) ? (name + "1") : name);
									dataSet.Tables.Add(dataTable);
									num4 = i;
								}
								IFieldInfoCollection fields2 = objectClass2.GetFields();
								int position = fields2.IndexOf("name");
								int position2 = fields2.IndexOf("groupid");
								int position3 = fields2.IndexOf("thumbpic");
								IFdeCursor fdeCursor2 = objectClass2.Search(filter, false);
								IRowBuffer rowBuffer2;
								while ((rowBuffer2 = fdeCursor2.NextRow()) != null)
								{
									string text = rowBuffer2.GetValue(position).ToString();
									int num5 = int.Parse(rowBuffer2.GetValue(position2).ToString());
									string name2 = objectClass2.GetSubType(num5).Name;
									IBinaryBuffer binaryBuffer = rowBuffer2.GetValue(position3) as IBinaryBuffer;
									byte[] array = (binaryBuffer != null) ? binaryBuffer.AsByteArray() : null;
									bool flag7 = string.IsNullOrEmpty(text) || num5 <= -1 || num5 > subTypeCount;
									if (!flag7)
									{
										DataTable dataTable2 = dataSet.Tables[name2];
										DataRow dataRow = dataTable2.NewRow();
										dataRow.ItemArray = new object[]
										{
											array,
											text
										};
										dataTable2.Rows.Add(dataRow);
									}
								}
							}
							result = dataSet;
						}
					}
				}
			}
		}
		return result;
	}
}
