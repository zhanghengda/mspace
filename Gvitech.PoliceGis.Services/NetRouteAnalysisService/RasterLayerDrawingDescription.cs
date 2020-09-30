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
    public class RasterLayerDrawingDescription : LayerDrawingDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public RasterRenderer RasterRenderer
        {
            get
            {
                return this.rasterRendererField;
            }
            set
            {
                this.rasterRendererField = value;
                base.RaisePropertyChanged("RasterRenderer");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public short Transparency
        {
            get
            {
                return this.transparencyField;
            }
            set
            {
                this.transparencyField = value;
                base.RaisePropertyChanged("Transparency");
            }
        }

        [XmlIgnore]
        public bool TransparencySpecified
        {
            get
            {
                return this.transparencyFieldSpecified;
            }
            set
            {
                this.transparencyFieldSpecified = value;
                base.RaisePropertyChanged("TransparencySpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public short Brightness
        {
            get
            {
                return this.brightnessField;
            }
            set
            {
                this.brightnessField = value;
                base.RaisePropertyChanged("Brightness");
            }
        }

        [XmlIgnore]
        public bool BrightnessSpecified
        {
            get
            {
                return this.brightnessFieldSpecified;
            }
            set
            {
                this.brightnessFieldSpecified = value;
                base.RaisePropertyChanged("BrightnessSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public short Contrast
        {
            get
            {
                return this.contrastField;
            }
            set
            {
                this.contrastField = value;
                base.RaisePropertyChanged("Contrast");
            }
        }

        [XmlIgnore]
        public bool ContrastSpecified
        {
            get
            {
                return this.contrastFieldSpecified;
            }
            set
            {
                this.contrastFieldSpecified = value;
                base.RaisePropertyChanged("ContrastSpecified");
            }
        }

        private RasterRenderer rasterRendererField;

        private short transparencyField;

        private bool transparencyFieldSpecified;

        private short brightnessField;

        private bool brightnessFieldSpecified;

        private short contrastField;

        private bool contrastFieldSpecified;
    }
}