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
    public class HlsColor : Color
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public short Hue
        {
            get
            {
                return this.hueField;
            }
            set
            {
                this.hueField = value;
                base.RaisePropertyChanged("Hue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public byte Lightness
        {
            get
            {
                return this.lightnessField;
            }
            set
            {
                this.lightnessField = value;
                base.RaisePropertyChanged("Lightness");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public byte Saturation
        {
            get
            {
                return this.saturationField;
            }
            set
            {
                this.saturationField = value;
                base.RaisePropertyChanged("Saturation");
            }
        }

        private short hueField;

        private byte lightnessField;

        private byte saturationField;
    }
}