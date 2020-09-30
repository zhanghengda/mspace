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
    public class RasterRGBRenderer : RasterRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public int LayerIndex1
        {
            get
            {
                return this.layerIndex1Field;
            }
            set
            {
                this.layerIndex1Field = value;
                base.RaisePropertyChanged("LayerIndex1");
            }
        }

        [XmlIgnore]
        public bool LayerIndex1Specified
        {
            get
            {
                return this.layerIndex1FieldSpecified;
            }
            set
            {
                this.layerIndex1FieldSpecified = value;
                base.RaisePropertyChanged("LayerIndex1Specified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public int LayerIndex2
        {
            get
            {
                return this.layerIndex2Field;
            }
            set
            {
                this.layerIndex2Field = value;
                base.RaisePropertyChanged("LayerIndex2");
            }
        }

        [XmlIgnore]
        public bool LayerIndex2Specified
        {
            get
            {
                return this.layerIndex2FieldSpecified;
            }
            set
            {
                this.layerIndex2FieldSpecified = value;
                base.RaisePropertyChanged("LayerIndex2Specified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public int LayerIndex3
        {
            get
            {
                return this.layerIndex3Field;
            }
            set
            {
                this.layerIndex3Field = value;
                base.RaisePropertyChanged("LayerIndex3");
            }
        }

        [XmlIgnore]
        public bool LayerIndex3Specified
        {
            get
            {
                return this.layerIndex3FieldSpecified;
            }
            set
            {
                this.layerIndex3FieldSpecified = value;
                base.RaisePropertyChanged("LayerIndex3Specified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public byte UseRGBBand
        {
            get
            {
                return this.useRGBBandField;
            }
            set
            {
                this.useRGBBandField = value;
                base.RaisePropertyChanged("UseRGBBand");
            }
        }

        [XmlIgnore]
        public bool UseRGBBandSpecified
        {
            get
            {
                return this.useRGBBandFieldSpecified;
            }
            set
            {
                this.useRGBBandFieldSpecified = value;
                base.RaisePropertyChanged("UseRGBBandSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string StretchType
        {
            get
            {
                return this.stretchTypeField;
            }
            set
            {
                this.stretchTypeField = value;
                base.RaisePropertyChanged("StretchType");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double StandardDeviations
        {
            get
            {
                return this.standardDeviationsField;
            }
            set
            {
                this.standardDeviationsField = value;
                base.RaisePropertyChanged("StandardDeviations");
            }
        }

        [XmlIgnore]
        public bool StandardDeviationsSpecified
        {
            get
            {
                return this.standardDeviationsFieldSpecified;
            }
            set
            {
                this.standardDeviationsFieldSpecified = value;
                base.RaisePropertyChanged("StandardDeviationsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public bool IsInvert
        {
            get
            {
                return this.isInvertField;
            }
            set
            {
                this.isInvertField = value;
                base.RaisePropertyChanged("IsInvert");
            }
        }

        [XmlIgnore]
        public bool IsInvertSpecified
        {
            get
            {
                return this.isInvertFieldSpecified;
            }
            set
            {
                this.isInvertFieldSpecified = value;
                base.RaisePropertyChanged("IsInvertSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public bool DisplayBkValue
        {
            get
            {
                return this.displayBkValueField;
            }
            set
            {
                this.displayBkValueField = value;
                base.RaisePropertyChanged("DisplayBkValue");
            }
        }

        [XmlIgnore]
        public bool DisplayBkValueSpecified
        {
            get
            {
                return this.displayBkValueFieldSpecified;
            }
            set
            {
                this.displayBkValueFieldSpecified = value;
                base.RaisePropertyChanged("DisplayBkValueSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 8)]
        [XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public double[] BlackValue
        {
            get
            {
                return this.blackValueField;
            }
            set
            {
                this.blackValueField = value;
                base.RaisePropertyChanged("BlackValue");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool IsLegendExpand
        {
            get
            {
                return this.isLegendExpandField;
            }
            set
            {
                this.isLegendExpandField = value;
                base.RaisePropertyChanged("IsLegendExpand");
            }
        }

        [XmlIgnore]
        public bool IsLegendExpandSpecified
        {
            get
            {
                return this.isLegendExpandFieldSpecified;
            }
            set
            {
                this.isLegendExpandFieldSpecified = value;
                base.RaisePropertyChanged("IsLegendExpandSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public Color BkColor
        {
            get
            {
                return this.bkColorField;
            }
            set
            {
                this.bkColorField = value;
                base.RaisePropertyChanged("BkColor");
            }
        }

        private int layerIndex1Field;

        private bool layerIndex1FieldSpecified;

        private int layerIndex2Field;

        private bool layerIndex2FieldSpecified;

        private int layerIndex3Field;

        private bool layerIndex3FieldSpecified;

        private byte useRGBBandField;

        private bool useRGBBandFieldSpecified;

        private string stretchTypeField;

        private double standardDeviationsField;

        private bool standardDeviationsFieldSpecified;

        private bool isInvertField;

        private bool isInvertFieldSpecified;

        private bool displayBkValueField;

        private bool displayBkValueFieldSpecified;

        private double[] blackValueField;

        private bool isLegendExpandField;

        private bool isLegendExpandFieldSpecified;

        private Color bkColorField;
    }
}