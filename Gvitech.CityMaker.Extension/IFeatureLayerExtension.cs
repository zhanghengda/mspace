using Gvitech.CityMaker.Models;
using Gvitech.CityMaker.RenderControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

public static class IFeatureLayerExtension
{
	//[CompilerGenerated]
	//[Serializable]
	//private sealed class <>c
	//{
	//	public static readonly IFeatureLayerExtension.<>c <>9 = new IFeatureLayerExtension.<>c();

	//	public static Func<XmlNode, bool> <>9__1_0;

	//	internal bool <GetGeometryRenderMetadata>b__1_0(XmlNode i)
	//	{
	//		return i.Name.Contains("Symbol");
	//	}
	//}

	public static void SetVisibleMask(this IFeatureLayer @this, bool isVisible, gviViewportMask gviViewportMask = gviViewportMask.gviViewAllNormalView)
	{
		bool flag = @this == null;
		if (!flag)
		{
			@this.VisibleMask = (isVisible ? gviViewportMask : gviViewportMask.gviViewNone);
		}
	}

	public static GeometryRenderMetadata GetGeometryRenderMetadata(this IFeatureLayer @this)
	{
		bool flag = @this == null || @this.GetGeometryRender() == null;
		GeometryRenderMetadata result;
		if (flag)
		{
			result = null;
		}
		else
		{
			GeometryRenderMetadata geometryRenderMetadata = new GeometryRenderMetadata();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(@this.GetGeometryRender().AsXml());
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("ValueMapGeometryRender//GeometryRenderScheme");
			bool flag2 = xmlNodeList != null;
			if (flag2)
			{
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					try
					{
						geometryRenderMetadata.LookUpField = xmlNode.SelectSingleNode("Unique").Attributes["LookUpField"].Value;
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("Unique//Value");
						bool flag3 = xmlNode2 == null;
						if (!flag3)
						{
							string innerText = xmlNode2.InnerText;
							bool flag4 = string.IsNullOrEmpty(innerText);
							if (!flag4)
							{
								bool flag5 = !geometryRenderMetadata.TypeValues.Contains(innerText);
								if (flag5)
								{
									geometryRenderMetadata.TypeValues.Add(innerText);
								}
								bool flag6 = !geometryRenderMetadata.Legend.ContainsKey(innerText);
								if (flag6)
								{
									//IEnumerable<XmlNode> arg_138_0 = xmlNode.ChildNodes.Cast<XmlNode>();
									//Func<XmlNode, bool> arg_138_1;
									//if ((arg_138_1 = IFeatureLayerExtension.<>c.<>9__1_0) == null)
									//{
									//	arg_138_1 = (IFeatureLayerExtension.<>c.<>9__1_0 = new Func<XmlNode, bool>(IFeatureLayerExtension.<>c.<>9.<GetGeometryRenderMetadata>b__1_0));
									//}
									//XmlNode xmlNode3 = arg_138_0.FirstOrDefault(arg_138_1);
									//bool flag7 = xmlNode3 != null;
									//if (flag7)
									//{
									//	string value = xmlNode3.Attributes["Color"].Value;
									//	geometryRenderMetadata.Legend.Add(innerText, value);
									//}
								}
							}
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
			result = geometryRenderMetadata;
		}
		return result;
	}

	public static void SetVisibleByValue(this IFeatureLayer @this, IObjectManager iObjectManager, string lookUpField, Dictionary<string, string> visibleController)
	{
		bool flag = @this == null || iObjectManager == null || !visibleController.HasValues<string, string>();
		if (!flag)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(@this.GetGeometryRender().AsXml());
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("ValueMapGeometryRender//GeometryRenderScheme");
			bool flag2 = xmlNodeList != null;
			if (flag2)
			{
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					try
					{
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("Unique//Value");
						bool flag3 = xmlNode2 == null;
						if (!flag3)
						{
							string innerText = xmlNode2.InnerText;
							bool flag4 = string.IsNullOrEmpty(innerText);
							if (!flag4)
							{
								bool flag5 = visibleController.ContainsKey(innerText);
								if (flag5)
								{
									xmlNode.Attributes["VisibleMask"].Value = visibleController[innerText];
								}
							}
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
			@this.SetGeometryRender(iObjectManager.CreateGeometryRenderFromXML(xmlDocument.InnerXml));
		}
	}
}
