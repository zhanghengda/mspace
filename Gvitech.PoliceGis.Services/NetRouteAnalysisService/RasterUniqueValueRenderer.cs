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
    public class RasterUniqueValueRenderer : RasterRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string ValueField
        {
            get
            {
                return this.valueFieldField;
            }
            set
            {
                this.valueFieldField = value;
                base.RaisePropertyChanged("ValueField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string ClassField
        {
            get
            {
                return this.classFieldField;
            }
            set
            {
                this.classFieldField = value;
                base.RaisePropertyChanged("ClassField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public bool UseDefaultSymbol
        {
            get
            {
                return this.useDefaultSymbolField;
            }
            set
            {
                this.useDefaultSymbolField = value;
                base.RaisePropertyChanged("UseDefaultSymbol");
            }
        }

        [XmlIgnore]
        public bool UseDefaultSymbolSpecified
        {
            get
            {
                return this.useDefaultSymbolFieldSpecified;
            }
            set
            {
                this.useDefaultSymbolFieldSpecified = value;
                base.RaisePropertyChanged("UseDefaultSymbolSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public Symbol DefaultSymbol
        {
            get
            {
                return this.defaultSymbolField;
            }
            set
            {
                this.defaultSymbolField = value;
                base.RaisePropertyChanged("DefaultSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public string DefaultLabel
        {
            get
            {
                return this.defaultLabelField;
            }
            set
            {
                this.defaultLabelField = value;
                base.RaisePropertyChanged("DefaultLabel");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public int LegendGroupsCount
        {
            get
            {
                return this.legendGroupsCountField;
            }
            set
            {
                this.legendGroupsCountField = value;
                base.RaisePropertyChanged("LegendGroupsCount");
            }
        }

        [XmlIgnore]
        public bool LegendGroupsCountSpecified
        {
            get
            {
                return this.legendGroupsCountFieldSpecified;
            }
            set
            {
                this.legendGroupsCountFieldSpecified = value;
                base.RaisePropertyChanged("LegendGroupsCountSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 7)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public LegendGroup[] LegendGroups
        {
            get
            {
                return this.legendGroupsField;
            }
            set
            {
                this.legendGroupsField = value;
                base.RaisePropertyChanged("LegendGroups");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public int ClassValuesCount
        {
            get
            {
                return this.classValuesCountField;
            }
            set
            {
                this.classValuesCountField = value;
                base.RaisePropertyChanged("ClassValuesCount");
            }
        }

        [XmlIgnore]
        public bool ClassValuesCountSpecified
        {
            get
            {
                return this.classValuesCountFieldSpecified;
            }
            set
            {
                this.classValuesCountFieldSpecified = value;
                base.RaisePropertyChanged("ClassValuesCountSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 9)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] ClassesInLegend
        {
            get
            {
                return this.classesInLegendField;
            }
            set
            {
                this.classesInLegendField = value;
                base.RaisePropertyChanged("ClassesInLegend");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 10)]
        [XmlArrayItem("Int", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public int[] ClassesInLegendSize
        {
            get
            {
                return this.classesInLegendSizeField;
            }
            set
            {
                this.classesInLegendSizeField = value;
                base.RaisePropertyChanged("ClassesInLegendSize");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 11)]
        [XmlArrayItem("Value", Form = XmlSchemaForm.Unqualified)]
        public object[] UniqueValueVariants
        {
            get
            {
                return this.uniqueValueVariantsField;
            }
            set
            {
                this.uniqueValueVariantsField = value;
                base.RaisePropertyChanged("UniqueValueVariants");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public bool Global
        {
            get
            {
                return this.globalField;
            }
            set
            {
                this.globalField = value;
                base.RaisePropertyChanged("Global");
            }
        }

        [XmlIgnore]
        public bool GlobalSpecified
        {
            get
            {
                return this.globalFieldSpecified;
            }
            set
            {
                this.globalFieldSpecified = value;
                base.RaisePropertyChanged("GlobalSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public RasterUniqueValues UniqueValues
        {
            get
            {
                return this.uniqueValuesField;
            }
            set
            {
                this.uniqueValuesField = value;
                base.RaisePropertyChanged("UniqueValues");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
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

        private string valueFieldField;

        private string classFieldField;

        private string colorSchemaField;

        private bool useDefaultSymbolField;

        private bool useDefaultSymbolFieldSpecified;

        private Symbol defaultSymbolField;

        private string defaultLabelField;

        private int legendGroupsCountField;

        private bool legendGroupsCountFieldSpecified;

        private LegendGroup[] legendGroupsField;

        private int classValuesCountField;

        private bool classValuesCountFieldSpecified;

        private int[] classesInLegendField;

        private int[] classesInLegendSizeField;

        private object[] uniqueValueVariantsField;

        private bool globalField;

        private bool globalFieldSpecified;

        private RasterUniqueValues uniqueValuesField;

        private ColorRamp colorRampField;
    }
}