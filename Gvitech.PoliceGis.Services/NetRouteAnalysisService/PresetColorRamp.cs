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
    public class PresetColorRamp : ColorRamp
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int NumColors
        {
            get
            {
                return this.numColorsField;
            }
            set
            {
                this.numColorsField = value;
                base.RaisePropertyChanged("NumColors");
            }
        }

        [XmlIgnore]
        public bool NumColorsSpecified
        {
            get
            {
                return this.numColorsFieldSpecified;
            }
            set
            {
                this.numColorsFieldSpecified = value;
                base.RaisePropertyChanged("NumColorsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int PresetSize
        {
            get
            {
                return this.presetSizeField;
            }
            set
            {
                this.presetSizeField = value;
                base.RaisePropertyChanged("PresetSize");
            }
        }

        [XmlIgnore]
        public bool PresetSizeSpecified
        {
            get
            {
                return this.presetSizeFieldSpecified;
            }
            set
            {
                this.presetSizeFieldSpecified = value;
                base.RaisePropertyChanged("PresetSizeSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Color[] Colors
        {
            get
            {
                return this.colorsField;
            }
            set
            {
                this.colorsField = value;
                base.RaisePropertyChanged("Colors");
            }
        }

        private int numColorsField;

        private bool numColorsFieldSpecified;

        private int presetSizeField;

        private bool presetSizeFieldSpecified;

        private Color[] colorsField;
    }
}