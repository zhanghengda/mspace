using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(TextElement))]
    [XmlInclude(typeof(RectangleElement))]
    [XmlInclude(typeof(PolygonElement))]
    [XmlInclude(typeof(ParagraphTextElement))]
    [XmlInclude(typeof(MarkerElement))]
    [XmlInclude(typeof(EllipseElement))]
    [XmlInclude(typeof(CircleElement))]
    [XmlInclude(typeof(LineElement))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class GraphicElement : Element
    {
    }
}