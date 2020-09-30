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
    public class CircularArc : Segment
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public Point CenterPoint
        {
            get
            {
                return this.centerPointField;
            }
            set
            {
                this.centerPointField = value;
                base.RaisePropertyChanged("CenterPoint");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double FromAngle
        {
            get
            {
                return this.fromAngleField;
            }
            set
            {
                this.fromAngleField = value;
                base.RaisePropertyChanged("FromAngle");
            }
        }

        [XmlIgnore]
        public bool FromAngleSpecified
        {
            get
            {
                return this.fromAngleFieldSpecified;
            }
            set
            {
                this.fromAngleFieldSpecified = value;
                base.RaisePropertyChanged("FromAngleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double ToAngle
        {
            get
            {
                return this.toAngleField;
            }
            set
            {
                this.toAngleField = value;
                base.RaisePropertyChanged("ToAngle");
            }
        }

        [XmlIgnore]
        public bool ToAngleSpecified
        {
            get
            {
                return this.toAngleFieldSpecified;
            }
            set
            {
                this.toAngleFieldSpecified = value;
                base.RaisePropertyChanged("ToAngleSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool IsCounterClockwise
        {
            get
            {
                return this.isCounterClockwiseField;
            }
            set
            {
                this.isCounterClockwiseField = value;
                base.RaisePropertyChanged("IsCounterClockwise");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public bool IsMinor
        {
            get
            {
                return this.isMinorField;
            }
            set
            {
                this.isMinorField = value;
                base.RaisePropertyChanged("IsMinor");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public bool IsLine
        {
            get
            {
                return this.isLineField;
            }
            set
            {
                this.isLineField = value;
                base.RaisePropertyChanged("IsLine");
            }
        }

        private Point centerPointField;

        private double fromAngleField;

        private bool fromAngleFieldSpecified;

        private double toAngleField;

        private bool toAngleFieldSpecified;

        private bool isCounterClockwiseField;

        private bool isMinorField;

        private bool isLineField;
    }
}