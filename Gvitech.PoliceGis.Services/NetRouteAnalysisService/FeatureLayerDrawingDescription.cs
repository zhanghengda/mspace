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
    public class FeatureLayerDrawingDescription : LayerDrawingDescription
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public FeatureRenderer FeatureRenderer
        {
            get
            {
                return this.featureRendererField;
            }
            set
            {
                this.featureRendererField = value;
                base.RaisePropertyChanged("FeatureRenderer");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public bool ScaleSymbols
        {
            get
            {
                return this.scaleSymbolsField;
            }
            set
            {
                this.scaleSymbolsField = value;
                base.RaisePropertyChanged("ScaleSymbols");
            }
        }

        [XmlIgnore]
        public bool ScaleSymbolsSpecified
        {
            get
            {
                return this.scaleSymbolsFieldSpecified;
            }
            set
            {
                this.scaleSymbolsFieldSpecified = value;
                base.RaisePropertyChanged("ScaleSymbolsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public LabelingDescription LabelingDescription
        {
            get
            {
                return this.labelingDescriptionField;
            }
            set
            {
                this.labelingDescriptionField = value;
                base.RaisePropertyChanged("LabelingDescription");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public int SourceLayerID
        {
            get
            {
                return this.sourceLayerIDField;
            }
            set
            {
                this.sourceLayerIDField = value;
                base.RaisePropertyChanged("SourceLayerID");
            }
        }

        [XmlIgnore]
        public bool SourceLayerIDSpecified
        {
            get
            {
                return this.sourceLayerIDFieldSpecified;
            }
            set
            {
                this.sourceLayerIDFieldSpecified = value;
                base.RaisePropertyChanged("SourceLayerIDSpecified");
            }
        }

        private FeatureRenderer featureRendererField;

        private bool scaleSymbolsField;

        private bool scaleSymbolsFieldSpecified;

        private short transparencyField;

        private bool transparencyFieldSpecified;

        private short brightnessField;

        private bool brightnessFieldSpecified;

        private short contrastField;

        private bool contrastFieldSpecified;

        private LabelingDescription labelingDescriptionField;

        private int sourceLayerIDField;

        private bool sourceLayerIDFieldSpecified;
    }
}