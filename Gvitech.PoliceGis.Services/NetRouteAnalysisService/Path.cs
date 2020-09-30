using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(Ring))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class Path : Curve
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Point[] PointArray
        {
            get
            {
                return this.pointArrayField;
            }
            set
            {
                this.pointArrayField = value;
                base.RaisePropertyChanged("PointArray");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Segment[] SegmentArray
        {
            get
            {
                return this.segmentArrayField;
            }
            set
            {
                this.segmentArrayField = value;
                base.RaisePropertyChanged("SegmentArray");
            }
        }

        private Point[] pointArrayField;

        private Segment[] segmentArrayField;
    }
}