using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(BezierCurve))]
    [XmlInclude(typeof(CircularArc))]
    [XmlInclude(typeof(EllipticArc))]
    [XmlInclude(typeof(Line))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class Segment : Curve
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Point FromPoint
        {
            get
            {
                return this.fromPointField;
            }
            set
            {
                this.fromPointField = value;
                base.RaisePropertyChanged("FromPoint");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Point ToPoint
        {
            get
            {
                return this.toPointField;
            }
            set
            {
                this.toPointField = value;
                base.RaisePropertyChanged("ToPoint");
            }
        }

        private Point fromPointField;

        private Point toPointField;
    }
}