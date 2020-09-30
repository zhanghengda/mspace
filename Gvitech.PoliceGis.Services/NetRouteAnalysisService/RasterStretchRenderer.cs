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
    public class RasterStretchRenderer : RasterRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string ColorSchema
        {
            get
            {
                return this.colorSchemaField;
            }
            set
            {
                this.colorSchemaField = value;
                base.RaisePropertyChanged("ColorSchema");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public double BlackValue
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

        [XmlIgnore]
        public bool BlackValueSpecified
        {
            get
            {
                return this.blackValueFieldSpecified;
            }
            set
            {
                this.blackValueFieldSpecified = value;
                base.RaisePropertyChanged("BlackValueSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public ColorRamp ColorRamp
        {
            get
            {
                return this.colorRampField;
            }
            set
            {
                this.colorRampField = value;
                base.RaisePropertyChanged("ColorRamp");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public LegendGroup LegendGroup
        {
            get
            {
                return this.legendGroupField;
            }
            set
            {
                this.legendGroupField = value;
                base.RaisePropertyChanged("LegendGroup");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public bool InitCustomMinMax
        {
            get
            {
                return this.initCustomMinMaxField;
            }
            set
            {
                this.initCustomMinMaxField = value;
                base.RaisePropertyChanged("InitCustomMinMax");
            }
        }

        [XmlIgnore]
        public bool InitCustomMinMaxSpecified
        {
            get
            {
                return this.initCustomMinMaxFieldSpecified;
            }
            set
            {
                this.initCustomMinMaxFieldSpecified = value;
                base.RaisePropertyChanged("InitCustomMinMaxSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool UseCustomMinMax
        {
            get
            {
                return this.useCustomMinMaxField;
            }
            set
            {
                this.useCustomMinMaxField = value;
                base.RaisePropertyChanged("UseCustomMinMax");
            }
        }

        [XmlIgnore]
        public bool UseCustomMinMaxSpecified
        {
            get
            {
                return this.useCustomMinMaxFieldSpecified;
            }
            set
            {
                this.useCustomMinMaxFieldSpecified = value;
                base.RaisePropertyChanged("UseCustomMinMaxSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public double CustomMin
        {
            get
            {
                return this.customMinField;
            }
            set
            {
                this.customMinField = value;
                base.RaisePropertyChanged("CustomMin");
            }
        }

        [XmlIgnore]
        public bool CustomMinSpecified
        {
            get
            {
                return this.customMinFieldSpecified;
            }
            set
            {
                this.customMinFieldSpecified = value;
                base.RaisePropertyChanged("CustomMinSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public double CustomMax
        {
            get
            {
                return this.customMaxField;
            }
            set
            {
                this.customMaxField = value;
                base.RaisePropertyChanged("CustomMax");
            }
        }

        [XmlIgnore]
        public bool CustomMaxSpecified
        {
            get
            {
                return this.customMaxFieldSpecified;
            }
            set
            {
                this.customMaxFieldSpecified = value;
                base.RaisePropertyChanged("CustomMaxSpecified");
            }
        }

        private string colorSchemaField;

        private int layerIndex1Field;

        private bool layerIndex1FieldSpecified;

        private string stretchTypeField;

        private double standardDeviationsField;

        private bool standardDeviationsFieldSpecified;

        private bool isInvertField;

        private bool isInvertFieldSpecified;

        private double blackValueField;

        private bool blackValueFieldSpecified;

        private ColorRamp colorRampField;

        private Color bkColorField;

        private LegendGroup legendGroupField;

        private bool displayBkValueField;

        private bool displayBkValueFieldSpecified;

        private bool initCustomMinMaxField;

        private bool initCustomMinMaxFieldSpecified;

        private bool useCustomMinMaxField;

        private bool useCustomMinMaxFieldSpecified;

        private double customMinField;

        private bool customMinFieldSpecified;

        private double customMaxField;

        private bool customMaxFieldSpecified;
    }
}