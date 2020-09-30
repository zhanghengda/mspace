using Gvitech.CityMaker.RenderControl;
using Mmc.Windows.Design;
using Mmc.Windows.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Mmc.LayerSymbol
{
    public class RenderXmlParser : Singleton<RenderXmlParser>
    {
        private void LayerRender2Xml(IFeatureLayer layer, XmlWriter xw)
        {
            bool flag = layer == null || xw == null;
            if (!flag)
            {
                xw.WriteStartElement("FeatureLayer");
                xw.WriteAttributeString("FeatureClassId", layer.FeatureClassId.ToString());
                xw.WriteAttributeString("GeometryFieldName", layer.GeometryFieldName);
                xw.WriteAttributeString("MaxVisibleDistance", layer.MaxVisibleDistance.ToString());
                xw.WriteAttributeString("MinVisiblePixels", layer.MinVisiblePixels.ToString());
                xw.WriteAttributeString("EnableCullFace", layer.CullMode.ToString());
                IGeometryRender geometryRender = layer.GetGeometryRender();
                this.GeoRender2Xml(geometryRender, xw);
                ITextRender textRender = layer.GetTextRender();
                this.TextRender2Xml(textRender, xw);
                xw.WriteEndElement();
            }
        }

        private void GeoRender2Xml(IGeometryRender render, XmlWriter xw)
        {
            bool flag = render == null || xw == null;
            if (!flag)
            {
                xw.WriteStartElement("GeometryRender");
                xw.WriteAttributeString("HeightStyle", render.HeightStyle.ToString());
                xw.WriteAttributeString("GroupField", render.RenderGroupField);
                xw.WriteAttributeString("RenderType", render.RenderType.ToString());
                bool flag2 = render.RenderType == gviRenderType.gviRenderSimple;
                if (flag2)
                {
                    ISimpleGeometryRender simpleGeometryRender = render as ISimpleGeometryRender;
                    bool flag3 = simpleGeometryRender != null;
                    if (flag3)
                    {
                        IGeometrySymbol symbol = simpleGeometryRender.Symbol;
                        bool flag4 = symbol != null;
                        if (flag4)
                        {
                            this.GeoSymbol2Xml(xw, symbol);
                        }
                    }
                }
                else
                {
                    IValueMapGeometryRender valueMapGeometryRender = render as IValueMapGeometryRender;
                    bool flag5 = valueMapGeometryRender != null;
                    if (flag5)
                    {
                        xw.WriteStartElement("ValueMap");
                        int num;
                        for (int i = 0; i < valueMapGeometryRender.SchemeCount; i = num)
                        {
                            xw.WriteStartElement("RenderScheme");
                            IGeometryRenderScheme scheme = valueMapGeometryRender.GetScheme(i);
                            bool flag6 = scheme == null;
                            if (!flag6)
                            {
                                IRenderRule rule = scheme.GetRule(0);
                                bool flag7 = rule == null;
                                if (!flag7)
                                {
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
                                            this.GeoSymbol2Xml(xw, symbol2);
                                        }
                                    }
                                    xw.WriteEndElement();
                                }
                            }
                            num = i + 1;
                        }
                        xw.WriteEndElement();
                    }
                }
                xw.WriteEndElement();
            }
        }

        private void TextRender2Xml(ITextRender textRender, XmlWriter xw)
        {
            bool flag = textRender == null || xw == null;
            if (!flag)
            {
                xw.WriteStartElement("TextRender");
                xw.WriteAttributeString("Expression", textRender.Expression);
                xw.WriteAttributeString("TextRenderType", textRender.RenderType.ToString());
                bool flag2 = textRender.RenderType == gviRenderType.gviRenderSimple;
                if (flag2)
                {
                    ISimpleTextRender simpleTextRender = textRender as ISimpleTextRender;
                    bool flag3 = simpleTextRender != null && simpleTextRender.Symbol != null;
                    if (flag3)
                    {
                        ITextSymbol symbol = simpleTextRender.Symbol;
                        this.TextSymbol2Xml(symbol, xw);
                    }
                }
                else
                {
                    IValueMapTextRender valueMapTextRender = textRender as IValueMapTextRender;
                    bool flag4 = valueMapTextRender != null;
                    if (flag4)
                    {
                        xw.WriteStartElement("ValueMap");
                        int num;
                        for (int i = 0; i < valueMapTextRender.SchemeCount; i = num)
                        {
                            xw.WriteStartElement("TextScheme");
                            IRenderRule rule = valueMapTextRender.GetScheme(i).GetRule(0);
                            bool flag5 = rule != null;
                            if (flag5)
                            {
                                bool flag6 = rule.RuleType == gviRenderRuleType.gviRenderRuleRange;
                                if (flag6)
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
                            ITextSymbol symbol2 = valueMapTextRender.GetScheme(i).Symbol;
                            bool flag7 = symbol2 != null;
                            if (flag7)
                            {
                                this.TextSymbol2Xml(symbol2, xw);
                            }
                            xw.WriteEndElement();
                            num = i + 1;
                        }
                        xw.WriteEndElement();
                    }
                }
                xw.WriteEndElement();
            }
        }

        private void TextSymbol2Xml(ITextSymbol symbol, XmlWriter xw)
        {
            bool flag = symbol == null || xw == null;
            if (!flag)
            {
                xw.WriteStartElement("TextSymbol");
                xw.WriteAttributeString("DrawLine", symbol.DrawLine.ToString());
                xw.WriteAttributeString("MaxVisualDistance", symbol.MaxVisualDistance.ToString());
                xw.WriteAttributeString("MinVisualDistance", symbol.MinVisualDistance.ToString());
                xw.WriteAttributeString("Priority", symbol.Priority.ToString());
                xw.WriteAttributeString("VerticalOffset", symbol.VerticalOffset.ToString());
                xw.WriteAttributeString("PivotAlignment", symbol.PivotAlignment.ToString());
                xw.WriteStartElement("TextAttribute");
                xw.WriteAttributeString("TextColor", symbol.TextAttribute.TextColor.ToString());
                xw.WriteAttributeString("Font", symbol.TextAttribute.Font.ToString());
                xw.WriteAttributeString("TextSize", symbol.TextAttribute.TextSize.ToString());
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }

        public string LayerRender2Xml(IFeatureLayer layer)
        {
            string @string;
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
                this.LayerRender2Xml(layer, xmlWriter);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                byte[] bytes = memoryStream.ToArray();
                @string = Encoding.UTF8.GetString(bytes);
            }
            catch (Exception innerException)
            {
                throw new Exception("LayerRender2Xml Function Error", innerException);
            }
            return @string;
        }

        private void GeoSymbol2Xml(XmlWriter xw, IGeometrySymbol symbol)
        {
            bool flag = xw == null || symbol == null;
            if (!flag)
            {
                try
                {
                    switch (symbol.SymbolType)
                    {
                        case gviGeometrySymbolType.gviGeoSymbolPoint:
                            {
                                ISimplePointSymbol simplePointSymbol = symbol as ISimplePointSymbol;
                                xw.WriteStartElement("GeometrySymbol");
                                xw.WriteAttributeString("SymbolType", simplePointSymbol.SymbolType.ToString());
                                xw.WriteAttributeString("FillColor", simplePointSymbol.FillColor.ToString());
                                xw.WriteAttributeString("OutlineColor", simplePointSymbol.OutlineColor.ToString());
                                xw.WriteAttributeString("Style", simplePointSymbol.Style.ToString());
                                xw.WriteAttributeString("Size", simplePointSymbol.Size.ToString());
                                xw.WriteEndElement();
                                break;
                            }
                        case gviGeometrySymbolType.gviGeoSymbolModelPoint:
                            {
                                IModelPointSymbol modelPointSymbol = symbol as IModelPointSymbol;
                                xw.WriteStartElement("GeometrySymbol");
                                xw.WriteAttributeString("SymbolType", modelPointSymbol.SymbolType.ToString());
                                xw.WriteAttributeString("Color", modelPointSymbol.Color.ToString());
                                xw.WriteAttributeString("EnableColor", modelPointSymbol.EnableColor.ToString());
                                xw.WriteEndElement();
                                break;
                            }
                        case gviGeometrySymbolType.gviGeoSymbolCurve:
                            {
                                ICurveSymbol curveSymbol = symbol as ICurveSymbol;
                                xw.WriteStartElement("GeometrySymbol");
                                xw.WriteAttributeString("SymbolType", curveSymbol.SymbolType.ToString());
                                xw.WriteAttributeString("Color", curveSymbol.Color.ToString());
                                xw.WriteEndElement();
                                break;
                            }
                        case gviGeometrySymbolType.gviGeoSymbolSurface:
                            {
                                ISurfaceSymbol surfaceSymbol = symbol as ISurfaceSymbol;
                                xw.WriteStartElement("GeometrySymbol");
                                xw.WriteAttributeString("SymbolType", surfaceSymbol.SymbolType.ToString());
                                xw.WriteAttributeString("FillColor", surfaceSymbol.Color.ToString());
                                xw.WriteAttributeString("Color", surfaceSymbol.BoundarySymbol.Color.ToString());
                                xw.WriteEndElement();
                                break;
                            }
                    }
                }
                catch (Exception innerException)
                {
                    throw new Exception("GeoSymbol2Xml Function Error", innerException);
                }
            }
        }

        private IGeometrySymbol Xml2GeoSymbol(XmlDocument xmlDoc)
        {
            return null;
        }

        private ITextRender Xml2TextRender(XmlDocument xmlDoc)
        {
            XmlNode node = this.GetNode(xmlDoc, "LayerRender/FeatureLayer/TextRender");
            bool flag = node == null;
            ITextRender result;
            if (flag)
            {
                result = null;
            }
            else
            {
                ITextRender textRender = null;
                string value = node.Attributes["TextRenderType"].Value;
                bool flag2 = value.Equals("RenderSimple");
                if (flag2)
                {
                    ISimpleTextRender simpleTextRender = new SimpleTextRender();
                    ITextSymbol textSymbol = null;
                    XmlNode firstChild = node.FirstChild;
                    bool flag3 = firstChild != null;
                    if (flag3)
                    {
                        textSymbol = new TextSymbol();
                        this.SetAttr(firstChild, textSymbol);
                        XmlNode firstChild2 = firstChild.FirstChild;
                        bool flag4 = firstChild2 != null;
                        if (flag4)
                        {
                            TextAttribute textAttribute = new TextAttribute();
                            this.SetAttr(firstChild2, textAttribute);
                            textSymbol.TextAttribute = textAttribute;
                        }
                    }
                    bool flag5 = textSymbol != null;
                    if (flag5)
                    {
                        simpleTextRender.Symbol = textSymbol;
                    }
                    textRender = simpleTextRender;
                }
                bool flag6 = textRender != null;
                if (flag6)
                {
                    string value2 = node.Attributes["Expression"].Value;
                    textRender.Expression = value2;
                }
                result = textRender;
            }
            return result;
        }

        private ITextSymbol Xml2TextSymbol(XmlDocument xmlDoc)
        {
            return null;
        }

        public IGeometryRender Xml2GeoRender(XmlDocument xmlDoc)
        {
            XmlNode node = this.GetNode(xmlDoc, "//LayerRender/FeatureLayer/GeometryRender");
            bool flag = node == null;
            IGeometryRender result;
            if (flag)
            {
                result = null;
            }
            else
            {
                string value = node.Attributes["RenderType"].Value;
                bool flag2 = value.Equals("RenderSimple");
                IGeometryRender geometryRender;
                if (flag2)
                {
                    ISimpleGeometryRender simpleGeometryRender = new SimpleGeometryRender();
                    XmlNode firstChild = node.FirstChild;
                    IGeometrySymbol geometrySymbol = this.Xml2GeoSym(firstChild);
                    bool flag3 = geometrySymbol != null;
                    if (flag3)
                    {
                        simpleGeometryRender.Symbol = geometrySymbol;
                    }
                    geometryRender = simpleGeometryRender;
                }
                else
                {
                    XmlNode node2 = this.GetNode(xmlDoc, "//LayerRender/FeatureLayer/GeometryRender/ValueMap");
                    bool flag4 = node2 == null || node2.ChildNodes.Count <= 0;
                    if (flag4)
                    {
                        result = null;
                        return result;
                    }
                    IValueMapGeometryRender valueMapGeometryRender = new ValueMapGeometryRender();
                    string lookUpField = string.Empty;
                    int num;
                    for (int i = 0; i < node2.ChildNodes.Count; i = num + 1)
                    {
                        XmlNode xmlNode = node2.ChildNodes[i];
                        bool flag5 = xmlNode == null;
                        if (!flag5)
                        {
                            XmlNode xmlNode2 = xmlNode.ChildNodes[0];
                            bool flag6 = xmlNode2 == null;
                            if (!flag6)
                            {
                                gviRenderRuleType gviRenderRuleType = (gviRenderRuleType)Enum.Parse(typeof(gviRenderRuleType), xmlNode2.Attributes["RuleType"].Value);
                                bool flag7 = gviRenderRuleType == gviRenderRuleType.gviRenderRuleUniqueValues;
                                IRenderRule renderRule;
                                if (flag7)
                                {
                                    IUniqueValuesRenderRule uniqueValuesRenderRule = new UniqueValuesRenderRule();
                                    string value2 = xmlNode2.Attributes["UniqueValue"].Value;
                                    uniqueValuesRenderRule.AddValue(value2);
                                    renderRule = uniqueValuesRenderRule;
                                }
                                else
                                {
                                    renderRule = new RangeRenderRule();
                                }
                                this.SetAttr(xmlNode2, renderRule);
                                lookUpField = renderRule.LookUpField;
                                XmlNode xmlNode3 = xmlNode.ChildNodes[1];
                                bool flag8 = xmlNode3 == null;
                                if (!flag8)
                                {
                                    IGeometrySymbol geometrySymbol2 = this.Xml2GeoSym(xmlNode3);
                                    bool flag9 = geometrySymbol2 != null && renderRule != null;
                                    if (flag9)
                                    {
                                        IGeometryRenderScheme geometryRenderScheme = new GeometryRenderScheme();
                                        geometryRenderScheme.Symbol = geometrySymbol2;
                                        geometryRenderScheme.AddRule(renderRule);
                                        valueMapGeometryRender.AddScheme(geometryRenderScheme);
                                    }
                                }
                            }
                        }
                        num = i;
                    }
                    IGeometryRenderScheme geometryRenderScheme2 = new GeometryRenderScheme();
                    ISurfaceSymbol symbol = new SurfaceSymbol();
                    geometryRenderScheme2.Symbol = symbol;
                    geometryRenderScheme2.AddRule(new UniqueValuesRenderRule
                    {
                        LookUpField = lookUpField,
                        Otherwise = true
                    });
                    valueMapGeometryRender.AddScheme(geometryRenderScheme2);
                    geometryRender = valueMapGeometryRender;
                }
                bool flag10 = geometryRender != null;
                if (flag10)
                {
                    string value3 = node.Attributes["HeightStyle"].Value;
                    gviHeightStyle gviHeightStyle = (gviHeightStyle)Enum.Parse(typeof(gviHeightStyle), value3);
                    string value4 = node.Attributes["GroupField"].Value;
                    geometryRender.RenderGroupField = value4;
                }
                result = geometryRender;
            }
            return result;
        }

        private IGeometrySymbol Xml2GeoSym(XmlNode geoSymNode)
        {
            IGeometrySymbol geometrySymbol = null;
            bool flag = geoSymNode != null;
            if (flag)
            {
                string value = geoSymNode.Attributes["SymbolType"].Value;
                gviGeometrySymbolType gviGeometrySymbolType = (gviGeometrySymbolType)Enum.Parse(typeof(gviGeometrySymbolType), value);
                switch (gviGeometrySymbolType)
                {
                    case gviGeometrySymbolType.gviGeoSymbolPoint:
                        geometrySymbol = new SimplePointSymbol();
                        break;
                    case gviGeometrySymbolType.gviGeoSymbolImagePoint:
                        geometrySymbol = new ImagePointSymbol();
                        break;
                    case gviGeometrySymbolType.gviGeoSymbolModelPoint:
                        geometrySymbol = new ModelPointSymbol();
                        break;
                    case gviGeometrySymbolType.gviGeoSymbolCurve:
                        geometrySymbol = new CurveSymbol();
                        break;
                    case gviGeometrySymbolType.gviGeoSymbolSurface:
                        geometrySymbol = new SurfaceSymbol();
                        break;
                    case gviGeometrySymbolType.gviGeoSymbolSolid:
                        geometrySymbol = new SolidSymbol();
                        break;
                }
                bool flag2 = gviGeometrySymbolType == gviGeometrySymbolType.gviGeoSymbolSurface;
                if (flag2)
                {
                    this.SetAttr2(geoSymNode, (ISurfaceSymbol)geometrySymbol);
                }
                else
                {
                    this.SetAttr(geoSymNode, geometrySymbol);
                }
            }
            return geometrySymbol;
        }

        private void Xml2LayerRender(string xml, IFeatureLayer layer)
        {
        }

        private void SetAttr2(XmlNode node, ISurfaceSymbol syb)
        {
            int num;
            for (int i = 0; i < node.Attributes.Count; i = num + 1)
            {
                string name = node.Attributes[i].Name;
                bool flag = "FillColor".Equals(name);
                if (flag)
                {
                    try
                    {
                        syb.Color = ColorConvert.UintToColor(uint.Parse(node.Attributes[i].Value));

                    }
                    catch
                    {
                        string value = "0x" + node.Attributes[i].Value;
                        syb.Color = ColorConvert.UintToColor(Convert.ToUInt32(value, 16));
                    }
                }
                num = i;
            }
        }

        private void SetAttr(XmlNode node, object obj)
        {
            try
            {
                Type type = obj.GetType();
                int num;
                for (int i = 0; i < node.Attributes.Count; i = num + 1)
                {
                    string name = node.Attributes[i].Name;
                    bool flag = string.IsNullOrEmpty(name);
                    if (!flag)
                    {
                        PropertyInfo[] properties = type.GetProperties();
                        for (int j = 0; j < properties.Length; j = num + 1)
                        {
                            bool flag2 = !properties[j].CanWrite || !properties[j].Name.Equals(name);
                            if (!flag2)
                            {
                                Type propertyType = properties[j].PropertyType;
                                try
                                {
                                    bool flag3 = propertyType == typeof(uint);
                                    if (flag3)
                                    {
                                        properties[j].SetValue(obj, uint.Parse(node.Attributes[i].Value), null);
                                    }
                                    else
                                    {
                                        bool flag4 = propertyType == typeof(int);
                                        if (flag4)
                                        {
                                            properties[j].SetValue(obj, int.Parse(node.Attributes[i].Value), null);
                                        }
                                        else
                                        {
                                            bool flag5 = propertyType == typeof(double);
                                            if (flag5)
                                            {
                                                properties[j].SetValue(obj, double.Parse(node.Attributes[i].Value), null);
                                            }
                                            else
                                            {
                                                bool flag6 = propertyType == typeof(string);
                                                if (flag6)
                                                {
                                                    properties[j].SetValue(obj, node.Attributes[i].Value, null);
                                                }
                                                else
                                                {
                                                    bool flag7 = propertyType == typeof(bool);
                                                    if (flag7)
                                                    {
                                                        properties[j].SetValue(obj, bool.Parse(node.Attributes[i].Value), null);
                                                    }
                                                    else
                                                    {
                                                        bool flag8 = propertyType.BaseType == typeof(Enum);
                                                        if (flag8)
                                                        {
                                                            properties[j].SetValue(obj, Enum.Parse(propertyType, node.Attributes[i].Value), null);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                            num = j;
                        }
                    }
                    num = i;
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("SetAttr Function Error", innerException);
            }
        }

        private XmlNodeList GetNodeList(XmlDocument xmlDoc, string path)
        {
            XmlNodeList result;
            try
            {
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(path);
                bool flag = xmlNodeList == null;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    result = xmlNodeList;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        private XmlNode GetNode(XmlDocument xmlDoc, string path)
        {
            XmlNode result;
            try
            {
                XmlNode xmlNode = xmlDoc.SelectSingleNode(path);
                bool flag = xmlNode == null;
                if (flag)
                {
                    result = null;
                }
                else
                {
                    result = xmlNode;
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("GetNode Function Error", innerException);
            }
            return result;
        }

        public LayerStyleType GetType(XmlDocument xmlDoc)
        {
            bool flag = xmlDoc == null;
            LayerStyleType result;
            if (flag)
            {
                result = LayerStyleType.StyleUnkown;
            }
            else
            {
                XmlNode node = this.GetNode(xmlDoc, "//FeatureLayer/Visibility");
                bool flag2 = node != null;
                if (flag2)
                {
                    result = LayerStyleType.Style63;
                }
                else
                {
                    node = this.GetNode(xmlDoc, "//LayerRender/FeatureLayer");
                    bool flag3 = node != null;
                    if (flag3)
                    {
                        result = LayerStyleType.Style70;
                    }
                    else
                    {
                        node = this.GetNode(xmlDoc, "//FeatureLayer");
                        bool flag4 = node != null;
                        if (flag4)
                        {
                            result = LayerStyleType.Style70;
                        }
                        else
                        {
                            result = LayerStyleType.StyleUnkown;
                        }
                    }
                }
            }
            return result;
        }

        public uint ColorParse(string strColor)
        {
            uint result = 4294967295u;
            try
            {
                result = uint.Parse(strColor);
            }
            catch
            {
                string value = "0x" + strColor;
                result = Convert.ToUInt32(value, 16);
            }
            return result;
        }

        public XmlDocument LoadXmlDocument(string xmlInfo)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlInfo);
            MemoryStream memoryStream = new MemoryStream(bytes);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(memoryStream);
            memoryStream.Close();
            return xmlDocument;
        }

        public XmlDocument LoadXmlDocument(byte[] buf)
        {
            MemoryStream memoryStream = new MemoryStream(buf);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(memoryStream);
            memoryStream.Close();
            return xmlDocument;
        }
    }
}
