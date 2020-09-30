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
    public class SymbolBackground : Background
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public double HorizontalGap
        {
            get
            {
                return this.horizontalGapField;
            }
            set
            {
                this.horizontalGapField = value;
                base.RaisePropertyChanged("HorizontalGap");
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
        public double VerticalGap
        {
            get
            {
                return this.verticalGapField;
            }
            set
            {
                this.verticalGapField = value;
                base.RaisePropertyChanged("VerticalGap");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public FillSymbol Symbol
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

        private double horizontalGapField;

        private short cornerRoundingField;

        private double verticalGapField;

        private FillSymbol symbolField;
    }
}