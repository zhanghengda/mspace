using System;
using System.IO;
using System.Text;
using System.Xml;
using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;

namespace Mmc.Business.Data
{
	// Token: 0x02000013 RID: 19
	public class LayerStyle70 : Singleton<LayerStyle70>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public bool ImportXml(string fileName, out PoiLayerProperty xmlFcLyr)
		{
			xmlFcLyr = null;
			XmlDocument count = new XmlDocument();
			count.Load(fileName);
			return this.Xml2LayerStyle(count, null, out xmlFcLyr);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public bool ExportXml(string fileName, PoiLayerProperty layer)
		{
			bool propertyColumnName = string.IsNullOrEmpty(fileName) || layer == null;
			bool valueColumnName;
			if (propertyColumnName)
			{
				valueColumnName = false;
			}
			else
			{
				try
				{
					string table = this.LayerStyle2Xml(layer);
					bool i = string.IsNullOrEmpty(table);
					if (i)
					{
						valueColumnName = false;
					}
					else
					{
						table = table.Trim();
						byte[] dr = Encoding.UTF8.GetBytes(table);
						MemoryStream field = new MemoryStream(dr);
						XmlDocument fields = new XmlDocument();
						fields.Load(field);
						fields.Save(fileName);
						valueColumnName = true;
					}
				}
				catch (Exception ex)
				{
					valueColumnName = false;
				}
			}
			return valueColumnName;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B88 File Offset: 0x00001D88
		public bool ExportXmlToBuilder(string fileName, PoiLayerProperty layer)
		{
			bool shpDs = string.IsNullOrEmpty(fileName) || layer == null;
			bool conn;
			if (shpDs)
			{
				conn = false;
			}
			else
			{
				try
				{
					string dsFactory = this.ExportToBuilder(layer);
					bool shpFc = string.IsNullOrEmpty(dsFactory);
					if (shpFc)
					{
						conn = false;
					}
					else
					{
						dsFactory = dsFactory.Trim();
						byte[] dsNames = Encoding.UTF8.GetBytes(dsFactory);
						MemoryStream gviFDataset = new MemoryStream(dsNames);
						XmlDocument fcNames = new XmlDocument();
						fcNames.Load(gviFDataset);
						fcNames.Save(fileName);
						conn = true;
					}
				}
				catch (Exception ex)
				{
					conn = false;
				}
			}
			return conn;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003C1C File Offset: 0x00001E1C
		public bool LayerStyle2Xml(PoiLayerProperty layer, ref string xmlInfo)
		{
			bool flag = layer == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					bool flag2 = string.IsNullOrEmpty(xmlInfo);
					if (flag2)
					{
						xmlInfo = this.LayerStyle2Xml(layer);
						result = true;
					}
					else
					{
						XmlDocument xmlDocument = Singleton<PoiLayerXmlParser>.Instance.LoadXmlDocument(xmlInfo);
						bool flag3 = xmlDocument == null;
						if (flag3)
						{
							result = false;
						}
						else
						{
							string geometryFiledName = layer.GeometryFiledName;
							XmlNode firstChild = xmlDocument.FirstChild;
							XmlElement xmlElement = firstChild as XmlElement;
							bool flag4 = xmlElement != null && !xmlElement.HasAttribute("CurrentGeoFieldName");
							if (flag4)
							{
								XmlAttribute node = xmlDocument.CreateAttribute("CurrentGeoFieldName");
								xmlElement.Attributes.Append(node);
							}
							xmlElement.SetAttribute("CurrentGeoFieldName", geometryFiledName);
							XmlNode fcNode = this.GetFcNode(xmlDocument, geometryFiledName);
							bool flag5 = fcNode != null;
							if (flag5)
							{
								firstChild.RemoveChild(fcNode);
							}
							XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
							xmlWriterSettings.OmitXmlDeclaration = false;
							xmlWriterSettings.Indent = true;
							xmlWriterSettings.Encoding = Encoding.UTF8;
							xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;
							MemoryStream memoryStream = new MemoryStream();
							XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
							this.WriteCirecle(xmlWriter, layer, false);
							xmlWriter.Close();
							byte[] buf = memoryStream.ToArray();
							XmlDocument xmlDocument2 = Singleton<PoiLayerXmlParser>.Instance.LoadXmlDocument(buf);
							XmlNode newChild = xmlDocument.ImportNode(xmlDocument2.FirstChild, true);
							memoryStream.Close();
							firstChild.AppendChild(newChild);
							MemoryStream memoryStream2 = new MemoryStream();
							xmlDocument.Save(memoryStream2);
							byte[] bytes = memoryStream2.ToArray();
							xmlInfo = Encoding.UTF8.GetString(bytes);
							memoryStream2.Close();
							result = true;
						}
					}
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003DDC File Offset: 0x00001FDC
		public bool Xml2LayerStyle(XmlDocument xmlDoc, string geoFieldName, out PoiLayerProperty xmlFcLyr)
		{
			xmlFcLyr = null;
			bool flag = xmlDoc == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					XmlNode xmlNode = xmlDoc.SelectSingleNode("//FeatureLayer");
					bool flag2 = xmlNode != null && xmlDoc.FirstChild == xmlNode;
					if (flag2)
					{
						xmlFcLyr = this.CreateFcLyrFromBuilder(xmlNode);
					}
					else
					{
						XmlNode firstChild = xmlDoc.FirstChild;
						XmlElement xmlElement = firstChild as XmlElement;
						bool flag3 = xmlElement != null && xmlElement.HasAttribute("CurrentGeoFieldName");
						if (flag3)
						{
							bool flag4 = string.IsNullOrEmpty(geoFieldName);
							if (flag4)
							{
								geoFieldName = xmlElement.GetAttribute("CurrentGeoFieldName");
							}
							XmlNode fcNode = this.GetFcNode(xmlDoc, geoFieldName);
							bool flag5 = fcNode == null;
							if (flag5)
							{
								return false;
							}
							xmlFcLyr = this.CreateFcLyr(fcNode);
						}
						else
						{
							XmlNode xmlNode2 = xmlDoc.SelectSingleNode("//LayerRender/FeatureLayer");
							bool flag6 = xmlNode2 == null;
							if (flag6)
							{
								return false;
							}
							xmlFcLyr = this.CreateFcLyr(xmlNode2);
						}
					}
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003EE0 File Offset: 0x000020E0
		private string LayerStyle2Xml(PoiLayerProperty layer)
		{
			bool flag = layer == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.OmitXmlDeclaration = false;
					xmlWriterSettings.Indent = true;
					xmlWriterSettings.Encoding = Encoding.UTF8;
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;
					MemoryStream memoryStream = new MemoryStream();
					XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
					xmlWriter.WriteStartElement("LayerRender");
					xmlWriter.WriteAttributeString("CurrentGeoFieldName", layer.GeometryFiledName);
					this.WriteCirecle(xmlWriter, layer, false);
					xmlWriter.WriteEndElement();
					xmlWriter.Close();
					byte[] bytes = memoryStream.ToArray();
					string @string = Encoding.UTF8.GetString(bytes);
					result = @string;
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FA8 File Offset: 0x000021A8
		private string ExportToBuilder(PoiLayerProperty layer)
		{
			bool flag = layer == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.OmitXmlDeclaration = false;
					xmlWriterSettings.Indent = true;
					xmlWriterSettings.Encoding = Encoding.UTF8;
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Auto;
					MemoryStream memoryStream = new MemoryStream();
					XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
					xmlWriter.WriteStartElement("FeatureLayer");
					xmlWriter.WriteAttributeString("Name", layer.LayerName);
					xmlWriter.WriteAttributeString("GeometryFieldName", layer.GeometryFiledName);
					xmlWriter.WriteAttributeString("MaxVisibleDistance", layer.MaxVisibleDistance.ToString());
					xmlWriter.WriteAttributeString("MinVisiblePixels", layer.MinVisiblePixels.ToString());
					xmlWriter.WriteAttributeString("ForceCullMode", layer.ForceCullMode.ToString());
					xmlWriter.WriteAttributeString("CullMode", layer.CullFaceMode.ToString());
					this.WriteCirecle(xmlWriter, layer, true);
					xmlWriter.WriteEndElement();
					xmlWriter.Close();
					byte[] bytes = memoryStream.ToArray();
					string @string = Encoding.UTF8.GetString(bytes);
					result = @string;
				}
				catch
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004104 File Offset: 0x00002304
		private XmlNode GetFcNode(XmlDocument xmlDoc, string geoFieldName)
		{
			string xpath = string.Format("//LayerRender/FeatureLayer[@GeometryFieldName='{0}']", geoFieldName);
			return xmlDoc.SelectSingleNode(xpath);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000412C File Offset: 0x0000232C
		private PoiLayerProperty CreateFcLyr(XmlNode fcNode)
		{
			bool flag = fcNode == null;
			PoiLayerProperty result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PoiLayerProperty poiLayerProperty = new PoiLayerProperty();
				double maxVisibleDistance = 0.0;
				float minVisiblePixels = 0f;
				bool forceCullMode = false;
				gviCullFaceMode cullFaceMode = gviCullFaceMode.gviCullFront;
				string geometryFiledName;
				this.GetFeatureLayerAttribute(fcNode, out maxVisibleDistance, out minVisiblePixels, out forceCullMode, out cullFaceMode, out geometryFiledName);
				poiLayerProperty.MaxVisibleDistance = maxVisibleDistance;
				poiLayerProperty.MinVisiblePixels = minVisiblePixels;
				poiLayerProperty.ForceCullMode = forceCullMode;
				poiLayerProperty.CullFaceMode = cullFaceMode;
				poiLayerProperty.GeometryFiledName = geometryFiledName;
				XmlNode geoRenNode = null;
				XmlNode textRenNode = null;
				int num;
				for (int i = 0; i < fcNode.ChildNodes.Count; i = num + 1)
				{
					XmlNode xmlNode = fcNode.ChildNodes[i];
					bool flag2 = xmlNode.Name == "GeometryRender";
					if (flag2)
					{
						geoRenNode = xmlNode;
					}
					else
					{
						bool flag3 = xmlNode.Name == "TextRender";
						if (flag3)
						{
							textRenNode = xmlNode;
						}
					}
					num = i;
				}
				IGeometryRender geoRender = this.GetGeoRender(geoRenNode);
				bool flag4 = geoRender == null;
				if (flag4)
				{
					result = null;
				}
				else
				{
					poiLayerProperty.GeoRender = geoRender;
					ITextRender textRender = this.GetTextRender(textRenNode);
					poiLayerProperty.TextRender = textRender;
					result = poiLayerProperty;
				}
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004258 File Offset: 0x00002458
		private PoiLayerProperty CreateFcLyrFromBuilder(XmlNode fcNode)
		{
			bool flag = fcNode == null;
			PoiLayerProperty result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PoiLayerProperty poiLayerProperty = new PoiLayerProperty();
				gviCullFaceMode cullFaceMode = gviCullFaceMode.gviCullFront;
				double maxVisibleDistance;
				float minVisiblePixels;
				bool forceCullMode;
				string geometryFiledName;
				this.GetFeatureLayerAttribute(fcNode, out maxVisibleDistance, out minVisiblePixels, out forceCullMode, out cullFaceMode, out geometryFiledName);
				poiLayerProperty.MaxVisibleDistance = maxVisibleDistance;
				poiLayerProperty.MinVisiblePixels = minVisiblePixels;
				poiLayerProperty.ForceCullMode = forceCullMode;
				poiLayerProperty.CullFaceMode = cullFaceMode;
				poiLayerProperty.GeometryFiledName = geometryFiledName;
				XmlNode geoRenNode = null;
				XmlNode textRenNode = null;
				int num;
				for (int i = 0; i < fcNode.ChildNodes.Count; i = num + 1)
				{
					XmlNode xmlNode = fcNode.ChildNodes[i];
					bool flag2 = xmlNode.Name == "GeometryRender";
					if (flag2)
					{
						geoRenNode = xmlNode;
					}
					else
					{
						bool flag3 = xmlNode.Name == "TextRender";
						if (flag3)
						{
							textRenNode = xmlNode;
						}
					}
					num = i;
				}
				IGeometryRender geoRender = this.GetGeoRender(geoRenNode);
				poiLayerProperty.GeoRender = geoRender;
				ITextRender textRender = this.GetTextRender(textRenNode);
				poiLayerProperty.TextRender = textRender;
				result = poiLayerProperty;
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004360 File Offset: 0x00002560
		private void WriteCirecle(XmlWriter xw, PoiLayerProperty layer, bool isForbuild)
		{
			bool flag = layer == null;
			if (!flag)
			{
				bool flag2 = !isForbuild;
				if (flag2)
				{
					xw.WriteStartElement("FeatureLayer");
					xw.WriteAttributeString("Name", layer.LayerName);
					xw.WriteAttributeString("GeometryFieldName", layer.GeometryFiledName);
					xw.WriteAttributeString("MaxVisibleDistance", layer.MaxVisibleDistance.ToString());
					xw.WriteAttributeString("MinVisiblePixels", layer.MinVisiblePixels.ToString());
					xw.WriteAttributeString("ForceCullMode", layer.ForceCullMode.ToString());
					xw.WriteAttributeString("CullMode", layer.CullFaceMode.ToString());
				}
				IGeometryRender geoRender = layer.GeoRender;
				bool flag3 = geoRender != null;
				if (flag3)
				{
					xw.WriteStartElement("GeometryRender");
					xw.WriteAttributeString("HeightStyle", geoRender.HeightStyle.ToString());
					xw.WriteAttributeString("GroupField", geoRender.RenderGroupField);
					xw.WriteAttributeString("RenderType", geoRender.RenderType.ToString());
					xw.WriteAttributeString("HeightOffset", geoRender.HeightOffset.ToString());
					bool flag4 = geoRender.RenderType == gviRenderType.gviRenderSimple;
					if (flag4)
					{
						ISimpleGeometryRender simpleGeometryRender = geoRender as ISimpleGeometryRender;
						bool flag5 = simpleGeometryRender != null;
						if (flag5)
						{
							IGeometrySymbol symbol = simpleGeometryRender.Symbol;
							bool flag6 = symbol != null;
							if (flag6)
							{
								this.WriteSymbol2Xml(xw, symbol);
							}
						}
					}
					else
					{
						IValueMapGeometryRender valueMapGeometryRender = geoRender as IValueMapGeometryRender;
						bool flag7 = valueMapGeometryRender != null;
						if (flag7)
						{
							xw.WriteStartElement("ValueMap");
							int num;
							for (int i = 0; i < valueMapGeometryRender.SchemeCount; i = num)
							{
								xw.WriteStartElement("RenderScheme");
								IRenderRule rule = valueMapGeometryRender.GetScheme(i).GetRule(0);
								bool flag8 = rule != null;
								if (flag8)
								{
									bool flag9 = rule.RuleType == gviRenderRuleType.gviRenderRuleRange;
									if (flag9)
									{
										IRangeRenderRule rangeRenderRule = rule as IRangeRenderRule;
										xw.WriteStartElement("RenderRule");
										xw.WriteAttributeString("LookUpField", rule.LookUpField);
										xw.WriteAttributeString("RuleType", rule.RuleType.ToString());
										xw.WriteAttributeString("IncludeMax", rangeRenderRule.IncludeMax.ToString());
										xw.WriteAttributeString("IncludeMin", rangeRenderRule.IncludeMin.ToString());
										xw.WriteAttributeString("MaxValue", rangeRenderRule.MaxValue.ToString());
										xw.WriteAttributeString("MinValue", rangeRenderRule.MinValue.ToString());
										xw.WriteEndElement();
									}
									else
									{
										IUniqueValuesRenderRule uniqueValuesRenderRule = rule as IUniqueValuesRenderRule;
										xw.WriteStartElement("RenderRule");
										xw.WriteAttributeString("LookUpField", rule.LookUpField);
										xw.WriteAttributeString("RuleType", rule.RuleType.ToString());
										xw.WriteAttributeString("UniqueValue", uniqueValuesRenderRule.GetValue(0));
										xw.WriteEndElement();
									}
								}
								IGeometrySymbol symbol2 = valueMapGeometryRender.GetScheme(i).Symbol;
								bool flag10 = symbol2 != null;
								if (flag10)
								{
									this.WriteSymbol2Xml(xw, symbol2);
								}
								xw.WriteEndElement();
								num = i + 1;
							}
							xw.WriteEndElement();
						}
					}
					xw.WriteEndElement();
				}
				ITextRender textRender = layer.TextRender;
				bool flag11 = textRender != null;
				if (flag11)
				{
					xw.WriteStartElement("TextRender");
					xw.WriteAttributeString("Expression", textRender.Expression);
					xw.WriteAttributeString("DynamicPlacement", textRender.DynamicPlacement.ToString());
					xw.WriteAttributeString("MinimizeOverlap", textRender.MinimizeOverlap.ToString());
					xw.WriteAttributeString("TextRenderType", textRender.RenderType.ToString());
					bool flag12 = textRender.RenderType == gviRenderType.gviRenderSimple;
					if (flag12)
					{
						ISimpleTextRender simpleTextRender = textRender as ISimpleTextRender;
						bool flag13 = simpleTextRender != null && simpleTextRender.Symbol != null;
						if (flag13)
						{
							this.WriteTextSymbol(xw, simpleTextRender.Symbol);
						}
					}
					else
					{
						IValueMapTextRender valueMapTextRender = textRender as IValueMapTextRender;
						bool flag14 = valueMapTextRender != null;
						if (flag14)
						{
							xw.WriteStartElement("ValueMap");
							int num;
							for (int j = 0; j < valueMapTextRender.SchemeCount; j = num)
							{
								xw.WriteStartElement("TextScheme");
								IRenderRule rule2 = valueMapTextRender.GetScheme(j).GetRule(0);
								bool flag15 = rule2 != null;
								if (flag15)
								{
									bool flag16 = rule2.RuleType == gviRenderRuleType.gviRenderRuleRange;
									if (flag16)
									{
										IRangeRenderRule rangeRenderRule2 = rule2 as IRangeRenderRule;
										xw.WriteStartElement("RenderRule");
										xw.WriteAttributeString("LookUpField", rule2.LookUpField);
										xw.WriteAttributeString("RuleType", rule2.RuleType.ToString());
										xw.WriteAttributeString("IncludeMax", rangeRenderRule2.IncludeMax.ToString());
										xw.WriteAttributeString("IncludeMin", rangeRenderRule2.IncludeMin.ToString());
										xw.WriteAttributeString("MaxValue", rangeRenderRule2.MaxValue.ToString());
										xw.WriteAttributeString("MinValue", rangeRenderRule2.MinValue.ToString());
										xw.WriteEndElement();
									}
									else
									{
										IUniqueValuesRenderRule uniqueValuesRenderRule2 = rule2 as IUniqueValuesRenderRule;
										xw.WriteStartElement("RenderRule");
										xw.WriteAttributeString("LookUpField", rule2.LookUpField);
										xw.WriteAttributeString("RuleType", rule2.RuleType.ToString());
										xw.WriteAttributeString("UniqueValue", uniqueValuesRenderRule2.GetValue(0));
										xw.WriteEndElement();
									}
								}
								ITextSymbol symbol3 = valueMapTextRender.GetScheme(j).Symbol;
								this.WriteTextSymbol(xw, symbol3);
								xw.WriteEndElement();
								num = j + 1;
							}
							xw.WriteEndElement();
						}
					}
					xw.WriteEndElement();
				}
				bool flag17 = !isForbuild;
				if (flag17)
				{
					xw.WriteEndElement();
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000049D4 File Offset: 0x00002BD4
		private void WriteTextSymbol(XmlWriter xw, ITextSymbol symbol)
		{
			bool flag = symbol == null;
			if (!flag)
			{
				xw.WriteStartElement("TextSymbol");
				xw.WriteAttributeString("DrawLine", symbol.DrawLine.ToString());
				xw.WriteAttributeString("LockMode", symbol.LockMode.ToString());
				xw.WriteAttributeString("MaxVisualDistance", symbol.MaxVisualDistance.ToString());
				xw.WriteAttributeString("MinVisualDistance", symbol.MinVisualDistance.ToString());
				xw.WriteAttributeString("PivotOffsetX", symbol.MarginWidth.ToString());
				xw.WriteAttributeString("PivotOffsetY", symbol.MarginHeight.ToString());
				xw.WriteAttributeString("Priority", symbol.Priority.ToString());
				xw.WriteAttributeString("VerticalOffset", symbol.VerticalOffset.ToString());
				xw.WriteAttributeString("PivotAlignment", symbol.PivotAlignment.ToString());
				xw.WriteStartElement("TextAttribute");
				xw.WriteAttributeString("TextColor", symbol.TextAttribute.TextColor.ToString());
				xw.WriteAttributeString("BackgroundColor", symbol.TextAttribute.BackgroundColor.ToString());
				xw.WriteAttributeString("OutlineColor", symbol.TextAttribute.OutlineColor.ToString());
				xw.WriteAttributeString("Font", symbol.TextAttribute.Font);
				xw.WriteAttributeString("TextSize", symbol.TextAttribute.TextSize.ToString());
				xw.WriteAttributeString("Bold", symbol.TextAttribute.Bold.ToString());
				xw.WriteAttributeString("Italic", symbol.TextAttribute.Italic.ToString());
				xw.WriteAttributeString("Underline", symbol.TextAttribute.Underline.ToString());
				xw.WriteEndElement();
				xw.WriteEndElement();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004C04 File Offset: 0x00002E04
		private void WriteSymbol2Xml(XmlWriter xw, IGeometrySymbol symbol)
		{
			bool flag = xw == null || symbol == null;
			if (!flag)
			{
				switch (symbol.SymbolType)
				{
				case gviGeometrySymbolType.gviGeoSymbolPoint:
				{
					ISimplePointSymbol simplePointSymbol = symbol as ISimplePointSymbol;
					xw.WriteStartElement("GeometrySymbol");
					xw.WriteAttributeString("GeometryType", "SimplePoint");
					xw.WriteAttributeString("SymbolType", simplePointSymbol.SymbolType.ToString());
					xw.WriteAttributeString("FillColor", simplePointSymbol.FillColor.ToString());
					xw.WriteAttributeString("OutlineColor", simplePointSymbol.OutlineColor.ToString());
					xw.WriteAttributeString("Style", simplePointSymbol.Style.ToString());
					xw.WriteAttributeString("Size", simplePointSymbol.Size.ToString());
					xw.WriteEndElement();
					break;
				}
				case gviGeometrySymbolType.gviGeoSymbolImagePoint:
				{
					IImagePointSymbol imagePointSymbol = symbol as IImagePointSymbol;
					xw.WriteStartElement("GeometrySymbol");
					xw.WriteAttributeString("GeometryType", "ImagePoint");
					xw.WriteAttributeString("ImageName", imagePointSymbol.ImageName);
					xw.WriteAttributeString("Script", imagePointSymbol.Script);
					xw.WriteAttributeString("PivotAlignment", imagePointSymbol.Alignment.ToString());
					xw.WriteAttributeString("Size", imagePointSymbol.Size.ToString());
					xw.WriteEndElement();
					break;
				}
				case gviGeometrySymbolType.gviGeoSymbolModelPoint:
				{
					IModelPointSymbol modelPointSymbol = symbol as IModelPointSymbol;
					bool flag2 = modelPointSymbol != null;
					if (flag2)
					{
						xw.WriteStartElement("GeometrySymbol");
						xw.WriteAttributeString("GeometryType", "ModelPoint");
						xw.WriteAttributeString("Color", modelPointSymbol.Color.ToString());
						xw.WriteAttributeString("EnableColor", modelPointSymbol.EnableColor.ToString());
						xw.WriteEndElement();
					}
					break;
				}
				case gviGeometrySymbolType.gviGeoSymbolCurve:
				{
					ICurveSymbol curveSymbol = symbol as ICurveSymbol;
					xw.WriteStartElement("GeometrySymbol");
					xw.WriteAttributeString("GeometryType", "Polyline");
					xw.WriteAttributeString("Color", curveSymbol.Color.ToString());
					xw.WriteAttributeString("Width", curveSymbol.Width.ToString());
					xw.WriteAttributeString("ImageName", curveSymbol.ImageName);
					xw.WriteAttributeString("RepeatLength", curveSymbol.RepeatLength.ToString());
					xw.WriteEndElement();
					break;
				}
				case gviGeometrySymbolType.gviGeoSymbolSurface:
				{
					ISurfaceSymbol surfaceSymbol = symbol as ISurfaceSymbol;
					xw.WriteStartElement("GeometrySymbol");
					xw.WriteAttributeString("GeometryType", "Polygon");
					xw.WriteAttributeString("FillColor", surfaceSymbol.Color.ToString());
					xw.WriteAttributeString("EnableLight", surfaceSymbol.EnableLight.ToString());
					xw.WriteAttributeString("Color", surfaceSymbol.BoundarySymbol.Color.ToString());
					xw.WriteAttributeString("Width", surfaceSymbol.BoundarySymbol.Width.ToString());
					xw.WriteAttributeString("ImageName", surfaceSymbol.BoundarySymbol.ImageName);
					xw.WriteEndElement();
					break;
				}
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004F7C File Offset: 0x0000317C
		private bool GetFeatureLayerAttribute(XmlNode fcNode, out double dMaxVisibleDistance, out float fMinVisiblePixels, out bool bForceCullMode, out gviCullFaceMode gviCullMode, out string geoFieldName)
		{
			dMaxVisibleDistance = 0.0;
			fMinVisiblePixels = 0f;
			bForceCullMode = false;
			gviCullMode = gviCullFaceMode.gviCullFront;
			geoFieldName = string.Empty;
			bool flag = fcNode == null || fcNode.Attributes == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string value = fcNode.Attributes["MaxVisibleDistance"].Value;
					string value2 = fcNode.Attributes["MinVisiblePixels"].Value;
					string value3 = fcNode.Attributes["ForceCullMode"].Value;
					string value4 = fcNode.Attributes["CullMode"].Value;
					geoFieldName = fcNode.Attributes["GeometryFieldName"].Value;
					dMaxVisibleDistance = double.Parse(value);
					fMinVisiblePixels = float.Parse(value2);
					bForceCullMode = (value3.ToLower() == "true");
					gviCullMode = (gviCullFaceMode)Enum.Parse(typeof(gviCullFaceMode), value4);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005094 File Offset: 0x00003294
		private IGeometryRender GetGeoRender(XmlNode geoRenNode)
		{
			bool flag = geoRenNode == null || geoRenNode.Attributes == null;
			IGeometryRender result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IGeometryRender geometryRender = null;
				string value = geoRenNode.Attributes["HeightStyle"].Value;
				string value2 = geoRenNode.Attributes["GroupField"].Value;
				string value3 = geoRenNode.Attributes["RenderType"].Value;
				string value4 = geoRenNode.Attributes["HeightOffset"].Value;
				bool flag2 = value3 == gviRenderType.gviRenderSimple.ToString();
				if (flag2)
				{
					ISimpleGeometryRender simpleGeometryRender = new SimpleGeometryRender();
					XmlNode xmlNode = geoRenNode.SelectSingleNode("GeometrySymbol");
					bool flag3 = xmlNode != null && xmlNode.Attributes != null;
					if (flag3)
					{
						string value5 = xmlNode.Attributes["GeometryType"].Value;
						simpleGeometryRender.Symbol = this.GetGeometrySymbol(value5, xmlNode);
					}
					simpleGeometryRender.HeightStyle = ((value == gviHeightStyle.gviHeightAbsolute.ToString()) ? gviHeightStyle.gviHeightAbsolute : gviHeightStyle.gviHeightOnTerrain);
					simpleGeometryRender.RenderGroupField = value2;
					geometryRender = simpleGeometryRender;
				}
				else
				{
					XmlNodeList xmlNodeList = geoRenNode.SelectNodes("ValueMap/RenderScheme");
					bool flag4 = xmlNodeList != null && xmlNodeList.Count > 0;
					if (flag4)
					{
						IValueMapGeometryRender valueMapGeometryRender = new ValueMapGeometryRender();
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode2 = (XmlNode)obj;
							IGeometryRenderScheme geometryRenderScheme = new GeometryRenderScheme();
							XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
							bool flag5 = xmlNode3 != null && xmlNode3.Attributes != null;
							if (flag5)
							{
								string value6 = xmlNode3.Attributes["RuleType"].Value;
								string value7 = xmlNode3.Attributes["LookUpField"].Value;
								bool flag6 = value6 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString();
								if (flag6)
								{
									IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRule();
									bool flag7 = xmlNode3.Attributes["UniqueValue"] == null;
									if (flag7)
									{
										continue;
									}
									string value8 = xmlNode3.Attributes["UniqueValue"].Value;
									uniqueValuesRenderRule.LookUpField = value7;
									uniqueValuesRenderRule.AddValue(value8);
									geometryRenderScheme.AddRule(uniqueValuesRenderRule);
								}
								else
								{
									IRangeRenderRule rangeRenderRule = new RangeRenderRule();
									string value9 = xmlNode3.Attributes["IncludeMax"].Value;
									string value10 = xmlNode3.Attributes["IncludeMin"].Value;
									string value11 = xmlNode3.Attributes["MaxValue"].Value;
									string value12 = xmlNode3.Attributes["MinValue"].Value;
									rangeRenderRule.LookUpField = value7;
									rangeRenderRule.IncludeMax = (value9.ToLower() == "true");
									rangeRenderRule.IncludeMin = (value10.ToLower() == "true");
									rangeRenderRule.MaxValue = double.Parse(value11);
									rangeRenderRule.MinValue = double.Parse(value12);
									geometryRenderScheme.AddRule(rangeRenderRule);
								}
							}
							XmlNode xmlNode4 = xmlNode2.SelectSingleNode("GeometrySymbol");
							bool flag8 = xmlNode4 != null && xmlNode4.Attributes != null;
							if (flag8)
							{
								string value13 = xmlNode4.Attributes["GeometryType"].Value;
								IGeometrySymbol geometrySymbol = this.GetGeometrySymbol(value13, xmlNode4);
								geometryRenderScheme.Symbol = geometrySymbol;
							}
							valueMapGeometryRender.HeightStyle = ((value == gviHeightStyle.gviHeightAbsolute.ToString()) ? gviHeightStyle.gviHeightAbsolute : gviHeightStyle.gviHeightOnTerrain);
							valueMapGeometryRender.RenderGroupField = value2;
							valueMapGeometryRender.AddScheme(geometryRenderScheme);
						}
						geometryRender = valueMapGeometryRender;
					}
				}
				bool flag9 = geometryRender != null;
				if (flag9)
				{
					geometryRender.HeightOffset = double.Parse(value4);
				}
				result = geometryRender;
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000054B4 File Offset: 0x000036B4
		private ITextRender GetTextRender(XmlNode textRenNode)
		{
			bool flag = textRenNode == null || textRenNode.Attributes == null;
			ITextRender result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ITextRender textRender = null;
				bool dynamicPlacement = false;
				bool minimizeOverlap = false;
				bool flag2 = textRenNode.Attributes["DynamicPlacement"] != null;
				if (flag2)
				{
					dynamicPlacement = bool.Parse(textRenNode.Attributes["DynamicPlacement"].Value);
				}
				bool flag3 = textRenNode.Attributes["MinimizeOverlap"] != null;
				if (flag3)
				{
					minimizeOverlap = bool.Parse(textRenNode.Attributes["MinimizeOverlap"].Value);
				}
				string value = textRenNode.Attributes["Expression"].Value;
				string value2 = textRenNode.Attributes["TextRenderType"].Value;
				bool flag4 = value2 == gviRenderType.gviRenderSimple.ToString();
				if (flag4)
				{
					XmlNode xmlNode = textRenNode.SelectSingleNode("TextSymbol");
					bool flag5 = xmlNode != null;
					if (flag5)
					{
						textRender = new SimpleTextRender
						{
							Symbol = this.GetTextSymbol(xmlNode),
							Expression = value,
							MinimizeOverlap = minimizeOverlap,
							DynamicPlacement = dynamicPlacement
						};
					}
				}
				else
				{
					XmlNodeList xmlNodeList = textRenNode.SelectNodes("ValueMap/TextScheme");
					bool flag6 = xmlNodeList != null && xmlNodeList.Count > 0;
					if (flag6)
					{
						IValueMapTextRender valueMapTextRender = new ValueMapTextRender();
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode2 = (XmlNode)obj;
							ITextRenderScheme textRenderScheme = new TextRenderScheme();
							XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
							bool flag7 = xmlNode3 != null && xmlNode3.Attributes != null;
							if (flag7)
							{
								string value3 = xmlNode3.Attributes["RuleType"].Value;
								string value4 = xmlNode3.Attributes["LookUpField"].Value;
								bool flag8 = value3 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString();
								if (flag8)
								{
									IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRule();
									string value5 = xmlNode3.Attributes["UniqueValue"].Value;
									uniqueValuesRenderRule.LookUpField = value4;
									uniqueValuesRenderRule.AddValue(value5);
									textRenderScheme.AddRule(uniqueValuesRenderRule);
								}
								else
								{
									IRangeRenderRule rangeRenderRule = new RangeRenderRule();
									string value6 = xmlNode3.Attributes["IncludeMax"].Value;
									string value7 = xmlNode3.Attributes["IncludeMin"].Value;
									string value8 = xmlNode3.Attributes["MaxValue"].Value;
									string value9 = xmlNode3.Attributes["MinValue"].Value;
									rangeRenderRule.LookUpField = value4;
									rangeRenderRule.IncludeMax = (value6.ToLower() == "true");
									rangeRenderRule.IncludeMin = (value7.ToLower() == "true");
									rangeRenderRule.MaxValue = double.Parse(value8);
									rangeRenderRule.MinValue = double.Parse(value9);
									textRenderScheme.AddRule(rangeRenderRule);
								}
							}
							XmlNode xmlNode4 = xmlNode2.SelectSingleNode("TextSymbol");
							bool flag9 = xmlNode4 != null;
							if (flag9)
							{
								textRenderScheme.Symbol = this.GetTextSymbol(xmlNode4);
							}
							valueMapTextRender.AddScheme(textRenderScheme);
						}
						valueMapTextRender.Expression = value;
						valueMapTextRender.MinimizeOverlap = minimizeOverlap;
						valueMapTextRender.DynamicPlacement = dynamicPlacement;
						textRender = valueMapTextRender;
					}
				}
				result = textRender;
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000586C File Offset: 0x00003A6C
		private IGeometrySymbol GetGeometrySymbol(string geoType, XmlNode symbolNode)
		{
			IGeometrySymbol result = null;
			if (!(geoType == "ModelPoint"))
			{
				if (!(geoType == "SimplePoint"))
				{
					if (!(geoType == "ImagePoint"))
					{
						if (!(geoType == "Polyline"))
						{
							if (geoType == "Polygon")
							{
								ISurfaceSymbol surfaceSymbol = new SurfaceSymbol();
								surfaceSymbol.Color = ColorConvert.UintColorParse(symbolNode.Attributes["FillColor"].Value);
								surfaceSymbol.BoundarySymbol.Color = ColorConvert.UintColorParse(symbolNode.Attributes["Color"].Value);
								bool flag = symbolNode.Attributes["Width"] != null;
								if (flag)
								{
									surfaceSymbol.BoundarySymbol.Width = float.Parse(symbolNode.Attributes["Width"].Value);
								}
								surfaceSymbol.BoundarySymbol.ImageName = symbolNode.Attributes["ImageName"].Value;
								surfaceSymbol.EnableLight = bool.Parse(symbolNode.Attributes["EnableLight"].Value);
								result = surfaceSymbol;
							}
						}
						else
						{
							ICurveSymbol curveSymbol = new CurveSymbol();
							curveSymbol.Color = ColorConvert.UintColorParse(symbolNode.Attributes["Color"].Value);
							curveSymbol.Width = float.Parse(symbolNode.Attributes["Width"].Value);
							bool flag2 = symbolNode.Attributes["ImageName"] != null;
							if (flag2)
							{
								curveSymbol.ImageName = symbolNode.Attributes["ImageName"].Value;
							}
							bool flag3 = symbolNode.Attributes["RepeatLength"] != null;
							if (flag3)
							{
								curveSymbol.RepeatLength = float.Parse(symbolNode.Attributes["RepeatLength"].Value);
							}
							result = curveSymbol;
						}
					}
					else
					{
						result = new ImagePointSymbol
						{
							Alignment = (gviPivotAlignment)Enum.Parse(typeof(gviPivotAlignment), symbolNode.Attributes["PivotAlignment"].Value),
							ImageName = symbolNode.Attributes["ImageName"].Value,
							Script = symbolNode.Attributes["Script"].Value,
							Size = int.Parse(symbolNode.Attributes["Size"].Value)
						};
					}
				}
				else
				{
					result = new SimplePointSymbol
					{
						FillColor = ColorConvert.UintColorParse(symbolNode.Attributes["FillColor"].Value),
						OutlineColor = ColorConvert.UintColorParse(symbolNode.Attributes["OutlineColor"].Value),
						Size = int.Parse(symbolNode.Attributes["Size"].Value),
						Style = (gviSimplePointStyle)Enum.Parse(typeof(gviSimplePointStyle), symbolNode.Attributes["Style"].Value)
					};
				}
			}
			else
			{
				result = new ModelPointSymbol
				{
					Color = ColorConvert.UintColorParse(symbolNode.Attributes["Color"].Value),
					EnableColor = (symbolNode.Attributes["EnableColor"].Value.ToLower() == "true")
				};
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005C14 File Offset: 0x00003E14
		private ITextSymbol GetTextSymbol(XmlNode symbolNode)
		{
			bool flag = symbolNode == null || symbolNode.Attributes == null;
			ITextSymbol result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string value = symbolNode.Attributes["DrawLine"].Value;
				string value2 = symbolNode.Attributes["MaxVisualDistance"].Value;
				string value3 = symbolNode.Attributes["MinVisualDistance"].Value;
				string value4 = symbolNode.Attributes["Priority"].Value;
				string s = "0";
				string s2 = "0";
				bool flag2 = symbolNode.Attributes["PivotOffsetX"] != null;
				if (flag2)
				{
					s = symbolNode.Attributes["PivotOffsetX"].Value;
					s2 = symbolNode.Attributes["PivotOffsetY"].Value;
				}
				string value5 = symbolNode.Attributes["VerticalOffset"].Value;
				ITextSymbol textSymbol = new TextSymbol();
				textSymbol.DrawLine = (value.ToLower() == "true");
				textSymbol.MaxVisualDistance = double.Parse(value2);
				textSymbol.MinVisualDistance = double.Parse(value3);
				textSymbol.MarginWidth = int.Parse(s);
				textSymbol.MarginHeight = int.Parse(s2);
				textSymbol.Priority = int.Parse(value4);
				textSymbol.VerticalOffset = double.Parse(value5);
				bool flag3 = symbolNode.Attributes["PivotAlignment"] != null;
				if (flag3)
				{
					string value6 = symbolNode.Attributes["PivotAlignment"].Value;
					textSymbol.PivotAlignment = (gviPivotAlignment)Enum.Parse(typeof(gviPivotAlignment), value6);
				}
				bool flag4 = symbolNode.Attributes["LockMode"] != null;
				if (flag4)
				{
					string value7 = symbolNode.Attributes["LockMode"].Value;
					textSymbol.LockMode = (gviLockMode)Enum.Parse(typeof(gviLockMode), value7);
				}
				XmlNode xmlNode = symbolNode.SelectSingleNode("TextAttribute");
				bool flag5 = xmlNode != null && xmlNode.Attributes != null;
				if (flag5)
				{
					string value8 = xmlNode.Attributes["TextColor"].Value;
					string value9 = xmlNode.Attributes["Font"].Value;
					string value10 = xmlNode.Attributes["TextSize"].Value;
					string value11 = xmlNode.Attributes["BackgroundColor"].Value;
					string value12 = xmlNode.Attributes["OutlineColor"].Value;
					string value13 = xmlNode.Attributes["Bold"].Value;
					string value14 = xmlNode.Attributes["Italic"].Value;
					string value15 = xmlNode.Attributes["Underline"].Value;
					textSymbol.TextAttribute = new TextAttribute
					{
						BackgroundColor = ColorConvert.UintColorParse(value11),
						TextColor = ColorConvert.UintColorParse(value8),
						Font = value9,
						OutlineColor = ColorConvert.UintColorParse(value12),
						TextSize = int.Parse(value10),
						Bold = bool.Parse(value13),
						Italic = bool.Parse(value14),
						Underline = bool.Parse(value15)
					};
				}
				result = textSymbol;
			}
			return result;
		}
	}
}
