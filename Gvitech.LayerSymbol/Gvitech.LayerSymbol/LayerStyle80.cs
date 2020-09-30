using Gvitech.CityMaker.RenderControl;
using Gvitech.CityMaker.Resource;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace Mmc.LayerSymbol
{
	public class LayerStyle80 : ILayerStyleConvertor
	{
		public bool ImportXml(string fileName, out FlyrProperty xmlFcLyr)
		{
			xmlFcLyr = null;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(fileName);
			bool flag = xmlDocument == null;
			return !flag && this.Xml2LayerStyle(xmlDocument, null, out xmlFcLyr);
		}

		public bool ExportXml(string fileName, FlyrProperty layer)
		{
			bool flag = string.IsNullOrEmpty(fileName) || layer == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string text = this.LayerStyle2Xml(layer);
					bool flag2 = text == null;
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = string.IsNullOrEmpty(text);
						if (flag3)
						{
							result = false;
						}
						else
						{
							text = text.Trim();
							byte[] bytes = Encoding.UTF8.GetBytes(text);
							MemoryStream inStream = new MemoryStream(bytes);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(inStream);
							xmlDocument.Save(fileName);
							result = true;
						}
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("ExportXml Function Error", innerException);
				}
			}
			return result;
		}

		public bool ExportXmlToBuilder(string fileName, FlyrProperty layer)
		{
			bool flag = string.IsNullOrEmpty(fileName) || layer == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				try
				{
					string text = this.ExportToBuilder(layer);
					bool flag2 = text == null;
					if (flag2)
					{
						result = false;
					}
					else
					{
						bool flag3 = string.IsNullOrEmpty(text);
						if (flag3)
						{
							result = false;
						}
						else
						{
							text = text.Trim();
							byte[] bytes = Encoding.UTF8.GetBytes(text);
							MemoryStream inStream = new MemoryStream(bytes);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(inStream);
							xmlDocument.Save(fileName);
							result = true;
						}
					}
				}
				catch (Exception innerException)
				{
					throw new Exception("ExportXmlToBuilder Function Error", innerException);
				}
			}
			return result;
		}

		public bool LayerStyle2Xml(FlyrProperty layer, ref string xmlInfo)
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
						XmlDocument xmlDocument = Singleton<RenderXmlParser>.Instance.LoadXmlDocument(xmlInfo);
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
							bool flag4 = !xmlElement.HasAttribute("CurrentGeoFieldName");
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
							XmlDocument xmlDocument2 = Singleton<RenderXmlParser>.Instance.LoadXmlDocument(buf);
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
				catch (Exception innerException)
				{
					throw new Exception("LayerStyle2Xml Function Error", innerException);
				}
			}
			return result;
		}

		public bool Xml2LayerStyle(XmlDocument xmlDoc, string geoFieldName, out FlyrProperty xmlFcLyr)
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
					XmlNode node = LayerStyleHelper.GetNode(xmlDoc, "//FeatureLayer");
					bool flag2 = node != null && xmlDoc.FirstChild == node;
					if (flag2)
					{
						xmlFcLyr = this.CreateFcLyrFromBuilder(node);
					}
					else
					{
						XmlNode firstChild = xmlDoc.FirstChild;
						XmlElement xmlElement = firstChild as XmlElement;
						bool flag3 = xmlElement.HasAttribute("CurrentGeoFieldName");
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
								result = false;
								return result;
							}
							xmlFcLyr = this.CreateFcLyr(fcNode);
						}
						else
						{
							XmlNode xmlNode = xmlDoc.SelectSingleNode("//LayerRender/FeatureLayer");
							bool flag6 = xmlNode == null;
							if (flag6)
							{
								result = false;
								return result;
							}
							xmlFcLyr = this.CreateFcLyr(xmlNode);
						}
					}
					result = true;
				}
				catch (Exception innerException)
				{
					throw new Exception("Xml2LayerStyle Function Error", innerException);
				}
			}
			return result;
		}

		private string LayerStyle2Xml(FlyrProperty layer)
		{
			string text = null;
			bool flag = layer == null;
			string result;
			if (flag)
			{
				result = text;
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
					StringBuilder stringBuilder = new StringBuilder();
					MemoryStream memoryStream = new MemoryStream();
					XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
					xmlWriter.WriteStartElement("LayerRender");
					xmlWriter.WriteAttributeString("CurrentGeoFieldName", layer.GeometryFiledName);
					this.WriteCirecle(xmlWriter, layer, false);
					xmlWriter.WriteEndElement();
					xmlWriter.Close();
					byte[] bytes = memoryStream.ToArray();
					text = Encoding.UTF8.GetString(bytes);
					result = text;
				}
				catch (Exception innerException)
				{
					throw new Exception("LayerStyle2Xml Function Error", innerException);
				}
			}
			return result;
		}

		private string ExportToBuilder(FlyrProperty layer)
		{
			string text = null;
			bool flag = layer == null;
			string result;
			if (flag)
			{
				result = text;
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
					StringBuilder stringBuilder = new StringBuilder();
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
					text = Encoding.UTF8.GetString(bytes);
					result = text;
				}
				catch (Exception innerException)
				{
					throw new Exception("ExportToBuilder Function Error", innerException);
				}
			}
			return result;
		}

		private XmlNode GetFcNode(XmlDocument xmlDoc, string geoFieldName)
		{
			string xpath = string.Format("//LayerRender/FeatureLayer[@GeometryFieldName='{0}']", geoFieldName);
			return xmlDoc.SelectSingleNode(xpath);
		}

		private FlyrProperty CreateFcLyr(XmlNode fcNode)
		{
			bool flag = fcNode == null;
			FlyrProperty result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FlyrProperty flyrProperty = new FlyrProperty();
				double maxVisibleDistance = 0.0;
				float minVisiblePixels = 0f;
				bool forceCullMode = false;
				gviCullFaceMode cullFaceMode = gviCullFaceMode.gviCullFront;
				string empty = string.Empty;
				this.GetFeatureLayerAttribute(fcNode, out maxVisibleDistance, out minVisiblePixels, out forceCullMode, out cullFaceMode, out empty);
				flyrProperty.MaxVisibleDistance = maxVisibleDistance;
				flyrProperty.MinVisiblePixels = minVisiblePixels;
				flyrProperty.ForceCullMode = forceCullMode;
				flyrProperty.CullFaceMode = cullFaceMode;
				flyrProperty.GeometryFiledName = empty;
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
					flyrProperty.GeoRender = geoRender;
					ITextRender textRender = this.GetTextRender(textRenNode);
					flyrProperty.TextRender = textRender;
					result = flyrProperty;
				}
			}
			return result;
		}

		private FlyrProperty CreateFcLyrFromBuilder(XmlNode fcNode)
		{
			bool flag = fcNode == null;
			FlyrProperty result;
			if (flag)
			{
				result = null;
			}
			else
			{
				FlyrProperty flyrProperty = new FlyrProperty();
				double maxVisibleDistance = 0.0;
				float minVisiblePixels = 0f;
				bool forceCullMode = false;
				gviCullFaceMode cullFaceMode = gviCullFaceMode.gviCullFront;
				string empty = string.Empty;
				this.GetFeatureLayerAttribute(fcNode, out maxVisibleDistance, out minVisiblePixels, out forceCullMode, out cullFaceMode, out empty);
				flyrProperty.MaxVisibleDistance = maxVisibleDistance;
				flyrProperty.MinVisiblePixels = minVisiblePixels;
				flyrProperty.ForceCullMode = forceCullMode;
				flyrProperty.CullFaceMode = cullFaceMode;
				flyrProperty.GeometryFiledName = empty;
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
				flyrProperty.GeoRender = geoRender;
				ITextRender textRender = this.GetTextRender(textRenNode);
				flyrProperty.TextRender = textRender;
				result = flyrProperty;
			}
			return result;
		}

		private void WriteCirecle(XmlWriter xw, FlyrProperty layer, bool isForbuild)
		{
			bool flag = layer != null;
			if (flag)
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
									bool flag11 = symbol2 != null;
									if (flag11)
									{
										this.WriteSymbol2Xml(xw, symbol2);
									}
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
				bool flag12 = textRender != null;
				if (flag12)
				{
					xw.WriteStartElement("TextRender");
					xw.WriteAttributeString("Expression", textRender.Expression);
					xw.WriteAttributeString("DynamicPlacement", textRender.DynamicPlacement.ToString());
					xw.WriteAttributeString("MinimizeOverlap", textRender.MinimizeOverlap.ToString());
					xw.WriteAttributeString("TextRenderType", textRender.RenderType.ToString());
					bool flag13 = textRender.RenderType == gviRenderType.gviRenderSimple;
					if (flag13)
					{
						ISimpleTextRender simpleTextRender = textRender as ISimpleTextRender;
						bool flag14 = simpleTextRender != null && simpleTextRender.Symbol != null;
						if (flag14)
						{
							this.WriteTextSymbol(xw, simpleTextRender.Symbol);
						}
					}
					else
					{
						IValueMapTextRender valueMapTextRender = textRender as IValueMapTextRender;
						bool flag15 = valueMapTextRender != null;
						if (flag15)
						{
							xw.WriteStartElement("ValueMap");
							int num;
							for (int j = 0; j < valueMapTextRender.SchemeCount; j = num)
							{
								xw.WriteStartElement("TextScheme");
								IRenderRule rule2 = valueMapTextRender.GetScheme(j).GetRule(0);
								bool flag16 = rule2 != null;
								if (flag16)
								{
									bool flag17 = rule2.RuleType == gviRenderRuleType.gviRenderRuleRange;
									if (flag17)
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
				bool flag18 = !isForbuild;
				if (flag18)
				{
					xw.WriteEndElement();
				}
			}
		}

		private void WriteTextSymbol(XmlWriter xw, ITextSymbol symbol)
		{
			bool flag = symbol != null;
			if (flag)
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
				xw.WriteAttributeString("Font", symbol.TextAttribute.Font.ToString());
				xw.WriteAttributeString("TextSize", symbol.TextAttribute.TextSize.ToString());
				xw.WriteAttributeString("Bold", symbol.TextAttribute.Bold.ToString());
				xw.WriteAttributeString("Italic", symbol.TextAttribute.Italic.ToString());
				xw.WriteAttributeString("Underline", symbol.TextAttribute.Underline.ToString());
				xw.WriteEndElement();
				xw.WriteEndElement();
			}
		}

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
					xw.WriteStartElement("GeometrySymbol");
					xw.WriteAttributeString("GeometryType", "ModelPoint");
					xw.WriteAttributeString("Color", modelPointSymbol.Color.ToString());
					xw.WriteAttributeString("EnableColor", modelPointSymbol.EnableColor.ToString());
					xw.WriteEndElement();
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

		private bool GetFeatureLayerAttribute(XmlNode fcNode, out double dMaxVisibleDistance, out float fMinVisiblePixels, out bool bForceCullMode, out gviCullFaceMode gviCullMode, out string geoFieldName)
		{
			dMaxVisibleDistance = 0.0;
			fMinVisiblePixels = 0f;
			bForceCullMode = false;
			gviCullMode = gviCullFaceMode.gviCullFront;
			geoFieldName = string.Empty;
			bool flag = fcNode == null;
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
				catch (Exception innerException)
				{
					throw new Exception("GetFeatureLayerAttribute Function Error", innerException);
				}
			}
			return result;
		}        		

        private IGeometryRender GetGeoRender(XmlNode geoRenNode)
        {
            bool flag = geoRenNode == null;
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
                    bool flag3 = xmlNode != null;
                    if (flag3)
                    {
                        string value5 = xmlNode.Attributes["GeometryType"].Value;
                        simpleGeometryRender.Symbol = this.GetGeometrySymbol(value5, xmlNode);
                    }
                    bool flag4 = value == gviHeightStyle.gviHeightAbsolute.ToString();
                    if (flag4)
                    {
                        simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                    }
                    else
                    {
                        simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                    }
                    simpleGeometryRender.RenderGroupField = value2;
                    geometryRender = simpleGeometryRender;
                }
                else
                {
                    XmlNodeList xmlNodeList = geoRenNode.SelectNodes("ValueMap/RenderScheme");
                    bool flag5 = xmlNodeList != null && xmlNodeList.Count > 0;
                    if (flag5)
                    {
                        IValueMapGeometryRender valueMapGeometryRender = new ValueMapGeometryRender();
                        foreach (XmlNode xmlNode2 in xmlNodeList)
                        {
                            IGeometryRenderScheme geometryRenderScheme = new GeometryRenderScheme();
                            XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
                            bool flag6 = xmlNode3 != null;
                            if (flag6)
                            {
                                string value6 = xmlNode3.Attributes["RuleType"].Value;
                                string value7 = xmlNode3.Attributes["LookUpField"].Value;
                                bool flag7 = value6 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString();
                                if (flag7)
                                {
                                    IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRule();
                                    bool flag8 = xmlNode3.Attributes["UniqueValue"] == null;
                                    if (flag8)
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
                            bool flag9 = xmlNode4 != null;
                            if (flag9)
                            {
                                string value13 = xmlNode4.Attributes["GeometryType"].Value;
                                IGeometrySymbol geometrySymbol = this.GetGeometrySymbol(value13, xmlNode4);
                                geometryRenderScheme.Symbol = geometrySymbol;
                            }
                            bool flag10 = value == gviHeightStyle.gviHeightAbsolute.ToString();
                            if (flag10)
                            {
                                valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                            }
                            else
                            {
                                valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                            }
                            valueMapGeometryRender.RenderGroupField = value2;
                            valueMapGeometryRender.AddScheme(geometryRenderScheme);
                        }
                        geometryRender = valueMapGeometryRender;
                    }
                }
                bool flag11 = geometryRender != null;
                if (flag11)
                {
                    geometryRender.HeightOffset = double.Parse(value4);
                }
                result = geometryRender;
            }
            return result;
        }

        private IGeometryRender GetGeoRender(XmlNode geoRenNode,ref string heightStyle)
        {
            bool flag = geoRenNode == null;
            IGeometryRender result;
            if (flag)
            {
                result = null;
            }
            else
            {
                IGeometryRender geometryRender = null;
                string value = geoRenNode.Attributes["HeightStyle"].Value;
                heightStyle = value;
                string value2 = geoRenNode.Attributes["GroupField"].Value;
                string value3 = geoRenNode.Attributes["RenderType"].Value;
                string value4 = geoRenNode.Attributes["HeightOffset"].Value;
                bool flag2 = value3 == gviRenderType.gviRenderSimple.ToString();
                if (flag2)
                {
                    ISimpleGeometryRender simpleGeometryRender = new SimpleGeometryRender();
                    XmlNode xmlNode = geoRenNode.SelectSingleNode("GeometrySymbol");
                    bool flag3 = xmlNode != null;
                    if (flag3)
                    {
                        string value5 = xmlNode.Attributes["GeometryType"].Value;
                        simpleGeometryRender.Symbol = this.GetGeometrySymbol(value5, xmlNode);
                    }

                    switch (value)
                    {
                        case "gviHeightAbsolute":
                            simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                            break;
                        case "gviHeightRelative":
                            simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightRelative;
                            break;
                        case "gviHeightOnTerrain":
                            simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                            break;
                        case "gviHeightOnEverything":
                            simpleGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                            break;
                    }

                    simpleGeometryRender.RenderGroupField = value2;
                    geometryRender = simpleGeometryRender;
                }
                else
                {
                    XmlNodeList xmlNodeList = geoRenNode.SelectNodes("ValueMap/RenderScheme");
                    bool flag5 = xmlNodeList != null && xmlNodeList.Count > 0;
                    if (flag5)
                    {
                        IValueMapGeometryRender valueMapGeometryRender = new ValueMapGeometryRender();
                        foreach (XmlNode xmlNode2 in xmlNodeList)
                        {
                            IGeometryRenderScheme geometryRenderScheme = new GeometryRenderScheme();
                            XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
                            bool flag6 = xmlNode3 != null;
                            if (flag6)
                            {
                                string value6 = xmlNode3.Attributes["RuleType"].Value;
                                string value7 = xmlNode3.Attributes["LookUpField"].Value;
                                bool flag7 = value6 == gviRenderRuleType.gviRenderRuleUniqueValues.ToString();
                                if (flag7)
                                {
                                    IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRule();
                                    bool flag8 = xmlNode3.Attributes["UniqueValue"] == null;
                                    if (flag8)
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
                            bool flag9 = xmlNode4 != null;
                            if (flag9)
                            {
                                string value13 = xmlNode4.Attributes["GeometryType"].Value;
                                IGeometrySymbol geometrySymbol = this.GetGeometrySymbol(value13, xmlNode4);
                                geometryRenderScheme.Symbol = geometrySymbol;
                            }

                            switch (value)
                            {
                                case "gviHeightAbsolute":
                                    valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightAbsolute;
                                    break;
                                case "gviHeightRelative":
                                    valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightRelative;
                                    break;
                                case "gviHeightOnTerrain":
                                    valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnTerrain;
                                    break;
                                case "gviHeightOnEverything":
                                    valueMapGeometryRender.HeightStyle = gviHeightStyle.gviHeightOnEverything;
                                    break;
                            }

                            valueMapGeometryRender.RenderGroupField = value2;
                            valueMapGeometryRender.AddScheme(geometryRenderScheme);
                        }
                        geometryRender = valueMapGeometryRender;
                    }
                }
                bool flag11 = geometryRender != null;
                if (flag11)
                {
                    geometryRender.HeightOffset = double.Parse(value4);
                }
                result = geometryRender;
            }
            return result;
        }

        private ITextRender GetTextRender(XmlNode textRenNode)
		{
			bool flag = textRenNode == null;
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
						foreach (XmlNode xmlNode2 in xmlNodeList)
						{
							ITextRenderScheme textRenderScheme = new TextRenderScheme();
							XmlNode xmlNode3 = xmlNode2.SelectSingleNode("RenderRule");
							bool flag7 = xmlNode3 != null;
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

		private IGeometrySymbol GetGeometrySymbol(string GeoType, XmlNode symbolNode)
		{
			IGeometrySymbol result = null;
			if (!(GeoType == "ModelPoint"))
			{
				if (!(GeoType == "SimplePoint"))
				{
					if (!(GeoType == "ImagePoint"))
					{
						if (!(GeoType == "Polyline"))
						{
							if (GeoType == "Polygon")
							{
								ISurfaceSymbol surfaceSymbol = new SurfaceSymbol();

                                
								surfaceSymbol.Color = this.GetColor(symbolNode.Attributes["FillColor"].Value);
								surfaceSymbol.BoundarySymbol.Color = this.GetColor(symbolNode.Attributes["Color"].Value);
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
							curveSymbol.Color = this.GetColor(symbolNode.Attributes["Color"].Value);
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
                        ImagePointSymbol imageSymbol = new ImagePointSymbol();
                       
                        if (symbolNode.Attributes["Alignment"] != null)
                        {
                            imageSymbol.Alignment = (gviPivotAlignment)Enum.Parse(typeof(gviPivotAlignment), symbolNode.Attributes["Alignment"].Value);
                        }
                        if (symbolNode.Attributes["ImageName"] != null)
                        {
                            imageSymbol.ImageName = symbolNode.Attributes["ImageName"].Value;
                        }
                        if (symbolNode.Attributes["Script"] != null)
                        {
                            imageSymbol.Script = symbolNode.Attributes["Script"].Value;
                        }
                        if (symbolNode.Attributes["Size"] != null)
                        {
                            imageSymbol.Size = int.Parse(symbolNode.Attributes["Size"].Value);
                        }

                        result = imageSymbol;
					}
				}
				else
				{
                    SimplePointSymbol temp = new SimplePointSymbol();
                    if (symbolNode.Attributes["FillColor"] != null)
                    {
                        temp.FillColor = this.GetColor(symbolNode.Attributes["FillColor"].Value);
                    }
                    if (symbolNode.Attributes["OutlineColor"] != null)
                    {
                        temp.OutlineColor = this.GetColor(symbolNode.Attributes["OutlineColor"].Value);
                    }
                    if (symbolNode.Attributes["Size"] != null)
                    {
                        temp.Size = int.Parse(symbolNode.Attributes["Size"].Value);
                    }
                    if (symbolNode.Attributes["Style"] != null)
                    {
                        temp.Style = (gviSimplePointStyle)Enum.Parse(typeof(gviSimplePointStyle), symbolNode.Attributes["Style"].Value);
                    }
                    result = temp;
                }
			}
			else
			{
				result = new ModelPointSymbol
				{
					Color = this.GetColor(symbolNode.Attributes["Color"].Value),
					EnableColor = symbolNode.Attributes["EnableColor"].Value.ToLower() == "true"
				};
			}
			return result;
		}

		private ITextSymbol GetTextSymbol(XmlNode symbolNode)
		{
			bool flag = symbolNode == null;
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
				bool flag5 = xmlNode != null;
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
						BackgroundColor = this.GetColor(value11),
						TextColor = this.GetColor(value8),
						Font = value9,
						OutlineColor = this.GetColor(value12),
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

        public string getRenderXml(string fileName, string renderType,ref string heightStyle)
        {
            string xmlStr = string.Empty;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//FeatureLayer");
            bool flag = xmlNode != null && xmlDoc.FirstChild == xmlNode;
            if (flag)
            {
                XmlNode tempRenNode = null;
                int num;
                for (int i = 0; i < xmlNode.ChildNodes.Count; i = num + 1)
                {
                    XmlNode childNode = xmlNode.ChildNodes[i];
                    if (renderType == "GeometryRender" && childNode.Name == "GeometryRender")
                    {
                        tempRenNode = childNode;
                        IGeometryRender geoRender = this.GetGeoRender(tempRenNode, ref heightStyle);
                        if (geoRender != null)
                        {
                            xmlStr = geoRender.AsXml();
                        }
                        break;
                    }
                    else if (renderType == "TextRender" && childNode.Name == "TextRender")
                    {
                        tempRenNode = childNode;
                        ITextRender textRender = this.GetTextRender(tempRenNode);
                        if (textRender != null)
                        {
                            xmlStr = textRender.AsXml();
                        }
                        break;
                    }
                    num = i;
                }
            }
            return xmlStr;
        }


        private Color GetColor(string colorstr)
        {
            Color color = new System.Drawing.Color();
            string[] tempArr = colorstr.Replace("]", "").Split(',');
            int alpha = Convert.ToInt16(tempArr[0].Split('=')[1]);
            int red = Convert.ToInt16(tempArr[1].Split('=')[1]);
            int green = Convert.ToInt16(tempArr[2].Split('=')[1]);
            int blue = Convert.ToInt16(tempArr[3].Split('=')[1]);
            color = System.Drawing.Color.FromArgb(alpha, red, green, blue);
            return color;
        }
    }
}
