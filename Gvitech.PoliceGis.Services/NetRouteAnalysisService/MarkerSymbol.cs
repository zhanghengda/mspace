using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(CartographicMarkerSymbol))]
    [XmlInclude(typeof(PictureMarkerSymbol))]
    [XmlInclude(typeof(CharacterMarkerSymbol))]
    [XmlInclude(typeof(SimpleMarkerSymbol))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class MarkerSymbol : Symbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double Angle
        {
            get
            {
                return this.angleField;
            }
            set
            {
                this.angleField = value;
                base.RaisePropertyChanged("Angle");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public Color Color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
                base.RaisePropertyChanged("Color");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
                base.RaisePropertyChanged("Size");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double XOffset
        {
            get
            {
                return this.xOffsetField;
            }
            set
            {
                this.xOffsetField = value;
                base.RaisePropertyChanged("XOffset");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public double YOffset
        {
            get
            {
                return this.yOffsetField;
            }
            set
            {
                this.yOffsetField = value;
                base.RaisePropertyChanged("YOffset");
            }
        }

        private double angleField;

        private Color colorField;

        private double sizeField;

        private double xOffsetField;

        private double yOffsetField;
    }
}