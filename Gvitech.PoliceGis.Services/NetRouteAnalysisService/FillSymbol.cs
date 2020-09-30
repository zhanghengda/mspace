using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [XmlInclude(typeof(XMLBinaryFillSymbol))]
    [XmlInclude(typeof(PictureFillSymbol))]
    [XmlInclude(typeof(SimpleFillSymbol))]
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public abstract class FillSymbol : Symbol
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public LineSymbol Outline
        {
            get
            {
                return this.outlineField;
            }
            set
            {
                this.outlineField = value;
                base.RaisePropertyChanged("Outline");
            }
        }

        private Color colorField;

        private LineSymbol outlineField;
    }
}