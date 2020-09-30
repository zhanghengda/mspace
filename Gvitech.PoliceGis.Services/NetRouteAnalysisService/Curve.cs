using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(Path))]
    [XmlInclude(typeof(Ring))]
    [XmlInclude(typeof(Polycurve))]
    [XmlInclude(typeof(Polyline))]
    [XmlInclude(typeof(PolylineB))]
    [XmlInclude(typeof(PolylineN))]
    [XmlInclude(typeof(Polygon))]
    [XmlInclude(typeof(PolygonB))]
    [XmlInclude(typeof(PolygonN))]
    [XmlInclude(typeof(Segment))]
    [XmlInclude(typeof(BezierCurve))]
    [XmlInclude(typeof(CircularArc))]
    [XmlInclude(typeof(EllipticArc))]
    [XmlInclude(typeof(Line))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Curve : Geometry
    {
    }
}