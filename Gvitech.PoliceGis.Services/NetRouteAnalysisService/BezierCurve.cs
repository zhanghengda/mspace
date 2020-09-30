using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class BezierCurve : Segment
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int Degree
        {
            get
            {
                return this.degreeField;
            }
            set
            {
                this.degreeField = value;
                base.RaisePropertyChanged("Degree");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Point[] ControlPointArray
        {
            get
            {
                return this.controlPointArrayField;
            }
            set
            {
                this.controlPointArrayField = value;
                base.RaisePropertyChanged("ControlPointArray");
            }
        }

        private int degreeField;

        private Point[] controlPointArrayField;
    }
}