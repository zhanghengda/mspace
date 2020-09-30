using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(Polyline))]
    [XmlInclude(typeof(PolylineB))]
    [XmlInclude(typeof(PolylineN))]
    [XmlInclude(typeof(Polygon))]
    [XmlInclude(typeof(PolygonB))]
    [XmlInclude(typeof(PolygonN))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Polycurve : Curve
    {
    }
}