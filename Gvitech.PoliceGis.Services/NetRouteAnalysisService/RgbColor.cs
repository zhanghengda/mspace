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
    public class RgbColor : Color
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public byte Red
        {
            get
            {
                return this.redField;
            }
            set
            {
                this.redField = value;
                base.RaisePropertyChanged("Red");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public byte Green
        {
            get
            {
                return this.greenField;
            }
            set
            {
                this.greenField = value;
                base.RaisePropertyChanged("Green");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public byte Blue
        {
            get
            {
                return this.blueField;
            }
            set
            {
                this.blueField = value;
                base.RaisePropertyChanged("Blue");
            }
        }

        private byte redField;

        private byte greenField;

        private byte blueField;
    }
}