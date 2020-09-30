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
    public class SymbolShadow : Shadow
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public LineSymbol Symbol
        {
            get
            {
                return this.symbolField;
            }
            set
            {
                this.symbolField = value;
                base.RaisePropertyChanged("Symbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public short CornerRounding
        {
            get
            {
                return this.cornerRoundingField;
            }
            set
            {
                this.cornerRoundingField = value;
                base.RaisePropertyChanged("CornerRounding");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public double HorizontalOffset
        {
            get
            {
                return this.horizontalOffsetField;
            }
            set
            {
                this.horizontalOffsetField = value;
                base.RaisePropertyChanged("HorizontalOffset");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public double VerticalOffset
        {
            get
            {
                return this.verticalOffsetField;
            }
            set
            {
                this.verticalOffsetField = value;
                base.RaisePropertyChanged("VerticalOffset");
            }
        }

        private LineSymbol symbolField;

        private short cornerRoundingField;

        private double horizontalOffsetField;

        private double verticalOffsetField;
    }
}