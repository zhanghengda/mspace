using System;
using System.Collections.Generic;
using System.Xml;
using Gvitech.CityMaker.Common;
using Gvitech.CityMaker.FdeCore;
using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;

namespace Mmc.Business.Data
{
	// Token: 0x0200000F RID: 15
	public class PoiLayerHelper
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003394 File Offset: 0x00001594
		public static List<PoiDisplayLayer> LoadPoiLayers(IObjectManager objectManager, IConnectionInfo ci)
		{
			bool ex = objectManager == null || ci == null;
			List<PoiDisplayLayer> result;
			if (ex)
			{
				result = null;
			}
			else
			{
				IDataSourceFactory xmlInfo = new DataSourceFactory();
				IDataSource buffer = xmlInfo.OpenDataSource(ci);
				List<PoiDisplayLayer> stream = new List<PoiDisplayLayer>();
				string[] xmlDoc = buffer.GetFeatureDatasetNames();
				bool flag = xmlDoc == null || xmlDoc.Length == 0;
				if (flag)
				{
					result = null;
				}
				else
				{
					foreach (string featureDataSetName in xmlDoc)
					{
						IFeatureDataSet featureDataSet = buffer.OpenFeatureDataset(featureDataSetName);
						string[] namesByType = featureDataSet.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
						bool flag2 = namesByType == null || namesByType.Length == 0;
						if (flag2)
						{
							return null;
						}
						IObjectClass oc = featureDataSet.OpenObjectClass("UP_LogicLayer");
						foreach (string name in namesByType)
						{
							IFeatureClass featureClass = featureDataSet.OpenFeatureClass(name);
							IRowBuffer ocRowBuffer = PoiLayerHelper.GetOcRowBuffer(featureClass, oc);
							IBinaryBuffer layerStyleBuf = PoiLayerHelper.GetLayerStyleBuf(ocRowBuffer);
							PoiLayerProperty poiLayerProperty = PoiLayerHelper.GetPoiLayerProperty(layerStyleBuf);
							bool flag3 = poiLayerProperty != null;
							IFeatureLayer featureLayer;
							if (flag3)
							{
								featureLayer = objectManager.CreateFeatureLayer(featureClass, "Geometry", poiLayerProperty.TextRender, poiLayerProperty.GeoRender);
								featureLayer.MaxVisibleDistance = poiLayerProperty.MaxVisibleDistance;
								featureLayer.MinVisiblePixels = poiLayerProperty.MinVisiblePixels;
								featureLayer.ForceCullMode = poiLayerProperty.ForceCullMode;
							}
							else
							{
								featureLayer = objectManager.CreateFeatureLayer(featureClass, "Geometry", null, null);
							}
							bool flag4 = featureLayer != null;
							if (flag4)
							{
								stream.Add(new PoiDisplayLayer
								{
									Fc = featureClass,
									FeatureLayer = featureLayer
								});
							}
						}
					}
					result = stream;
				}
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003544 File Offset: 0x00001744
		public static void StorePoiRendStyle(IObjectClass oc, string guidString, IBinaryBuffer renderStyle)
		{
			IQueryFilter xmlInfo = new QueryFilter();
			xmlInfo.WhereClause = "Guid= '" + guidString + "'";
			IFdeCursor buffer = oc.Update(xmlInfo);
			IRowBuffer stream = buffer.NextRow();
			bool xmlDoc = stream != null;
			if (xmlDoc)
			{
				stream.SetValue(stream.FieldIndex("RenderStyle"), renderStyle);
				buffer.UpdateRow(stream);
				ComFactory.ReleaseComObject(buffer);
			}
			else
			{
				IFdeCursor ex = oc.Insert();
				IRowBuffer rowBuffer = oc.CreateRowBuffer();
				rowBuffer.SetValue(rowBuffer.FieldIndex("Guid"), guidString);
				rowBuffer.SetValue(rowBuffer.FieldIndex("RenderStyle"), renderStyle);
				ex.InsertRow(rowBuffer);
				ComFactory.ReleaseComObjects(new object[]
				{
					ex
				});
			}
			ComFactory.ReleaseComObject(xmlInfo);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000360C File Offset: 0x0000180C
		public static List<PoiDisplayLayer> LoadVectorLayers(IObjectManager objectManager, IConnectionInfo ci, gviHeightStyle gviHeightStyle, double heightOffset)
		{
			bool re = objectManager == null || ci == null;
			List<PoiDisplayLayer> xmlAttr;
			if (re)
			{
				xmlAttr = null;
			}
			else
			{
				IDataSourceFactory oldXmlDoc = new DataSourceFactory();
				IDataSource fcNode = oldXmlDoc.OpenDataSource(ci);
				List<PoiDisplayLayer> geoName = new List<PoiDisplayLayer>();
				string[] rootNode = fcNode.GetFeatureDatasetNames();
				bool setting = rootNode == null || rootNode.Length == 0;
				if (setting)
				{
					xmlAttr = null;
				}
				else
				{
					foreach (string buf in rootNode)
					{
						IFeatureDataSet fcDoc = fcNode.OpenFeatureDataset(buf);
						string[] newFcNode = fcDoc.GetNamesByType(gviDataSetType.gviDataSetFeatureClassTable);
						bool buf2 = newFcNode == null || newFcNode.Length == 0;
						if (buf2)
						{
							return null;
						}
						IObjectClass outMs = fcDoc.OpenObjectClass("UP_LogicLayer");
						foreach (string name in newFcNode)
						{
							IFeatureClass featureClass = fcDoc.OpenFeatureClass(name);
							IRowBuffer ocRowBuffer = PoiLayerHelper.GetOcRowBuffer(featureClass, outMs);
							IBinaryBuffer layerStyleBuf = PoiLayerHelper.GetLayerStyleBuf(ocRowBuffer);
							PoiLayerProperty poiLayerProperty = PoiLayerHelper.GetPoiLayerProperty(layerStyleBuf);
							bool flag = poiLayerProperty != null;
							IFeatureLayer featureLayer;
							if (flag)
							{
								featureLayer = objectManager.CreateFeatureLayer(featureClass, "Geometry", poiLayerProperty.TextRender, poiLayerProperty.GeoRender);
								featureLayer.MaxVisibleDistance = poiLayerProperty.MaxVisibleDistance;
								featureLayer.MinVisiblePixels = poiLayerProperty.MinVisiblePixels;
								featureLayer.ForceCullMode = poiLayerProperty.ForceCullMode;
							}
							else
							{
								featureLayer = objectManager.CreateFeatureLayer(featureClass, "Geometry", null, null);
							}
							bool flag2 = featureLayer == null;
							if (!flag2)
							{
								featureLayer.GetGeometryRender().HeightOffset = heightOffset;
								featureLayer.GetGeometryRender().HeightStyle = gviHeightStyle;
								geoName.Add(new PoiDisplayLayer
								{
									Fc = featureClass,
									FeatureLayer = featureLayer
								});
							}
						}
					}
					xmlAttr = geoName;
				}
			}
			return xmlAttr;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000037D8 File Offset: 0x000019D8
		private static IRowBuffer GetOcRowBuffer(IFeatureClass fc, IObjectClass oc)
		{
			IFdeCursor root = oc.Search(new QueryFilter
			{
				WhereClause = "Guid= '" + fc.Guid.ToString() + "'"
			}, false);
			return root.NextRow();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000381C File Offset: 0x00001A1C
		private static IBinaryBuffer GetLayerStyleBuf(IRowBuffer rowBuffer)
		{
			bool sb = rowBuffer == null;
			IBinaryBuffer ms;
			if (sb)
			{
				ms = null;
			}
			else
			{
				int xmlInfo = rowBuffer.FieldIndex("RenderStyle");
				IBinaryBuffer setting = rowBuffer.GetValue(xmlInfo) as IBinaryBuffer;
				ms = setting;
			}
			return ms;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003854 File Offset: 0x00001A54
		private static PoiLayerProperty GetPoiLayerProperty(IBinaryBuffer bufStyle)
		{
			bool sb = bufStyle == null;
			PoiLayerProperty ms;
			if (sb)
			{
				ms = null;
			}
			else
			{
				XmlDocument setting = Singleton<PoiLayerXmlParser>.Instance.LoadXmlDocument(bufStyle.AsByteArray());
				PoiLayerProperty xmlInfo;
				ms = (Singleton<LayerStyle70>.Instance.Xml2LayerStyle(setting, "Geometry", out xmlInfo) ? xmlInfo : null);
			}
			return ms;
		}
	}
}
