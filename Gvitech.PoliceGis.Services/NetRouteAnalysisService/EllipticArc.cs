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
    public class EllipticArc : Segment
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public bool EllipseStd
        {
            get
            {
                return this.ellipseStdField;
            }
            set
            {
                this.ellipseStdField = value;
                base.RaisePropertyChanged("EllipseStd");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double Rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
                base.RaisePropertyChanged("Rotation");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double MinorMajorRatio
        {
            get
            {
                return this.minorMajorRatioField;
            }
            set
            {
                this.minorMajorRatioField = value;
                base.RaisePropertyChanged("MinorMajorRatio");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        private bool ellipseStdField;

        private Point centerPointField;

        private double rotationField;

        private double minorMajorRatioField;

        private bool isCounterClockwiseField;

        private bool isMinorField;
    }
}