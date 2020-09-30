using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(TriangleStrip))]
    [XmlInclude(typeof(TriangleFan))]
    [XmlInclude(typeof(Triangles))]
    [XmlInclude(typeof(MultiPatch))]
    [XmlInclude(typeof(MultiPatchN))]
    [XmlInclude(typeof(MultiPatchB))]
    [XmlInclude(typeof(Multipoint))]
    [XmlInclude(typeof(MultipointN))]
    [XmlInclude(typeof(MultipointB))]
    [XmlInclude(typeof(Envelope))]
    [XmlInclude(typeof(EnvelopeB))]
    [XmlInclude(typeof(EnvelopeN))]
    [XmlInclude(typeof(Point))]
    [XmlInclude(typeof(PointB))]
    [XmlInclude(typeof(PointN))]
    [XmlInclude(typeof(Curve))]
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
    public class Geometry : INotifyPropertyChanged
    {
        // (add) Token: 0x060001AA RID: 426 RVA: 0x0000748C File Offset: 0x0000568C
        // (remove) Token: 0x060001AB RID: 427 RVA: 0x000074C4 File Offset: 0x000056C4
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}