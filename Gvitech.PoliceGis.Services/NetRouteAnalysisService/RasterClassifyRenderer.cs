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
    public class RasterClassifyRenderer : RasterRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public string NormField
        {
            get
            {
                return this.normFieldField;
            }
            set
            {
                this.normFieldField = value;
                base.RaisePropertyChanged("NormField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
        public bool ClassificationComponent
        {
            get
            {
                return this.classificationComponentField;
            }
            set
            {
                this.classificationComponentField = value;
                base.RaisePropertyChanged("ClassificationComponent");
            }
        }

        [XmlIgnore]
        public bool ClassificationComponentSpecified
        {
            get
            {
                return this.classificationComponentFieldSpecified;
            }
            set
            {
                this.classificationComponentFieldSpecified = value;
                base.RaisePropertyChanged("ClassificationComponentSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public string Guid
        {
            get
            {
                return this.guidField;
            }
            set
            {
                this.guidField = value;
                base.RaisePropertyChanged("Guid");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
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

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 6)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public int BreakSize
        {
            get
            {
                return this.breakSizeField;
            }
            set
            {
                this.breakSizeField = value;
                base.RaisePropertyChanged("BreakSize");
            }
        }

        [XmlIgnore]
        public bool BreakSizeSpecified
        {
            get
            {
                return this.breakSizeFieldSpecified;
            }
            set
            {
                this.breakSizeFieldSpecified = value;
                base.RaisePropertyChanged("BreakSizeSpecified");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 8)]
        [XmlArrayItem("Double", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public double[] ArrayOfBreak
        {
            get
            {
                return this.arrayOfBreakField;
            }
            set
            {
                this.arrayOfBreakField = value;
                base.RaisePropertyChanged("ArrayOfBreak");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
        public bool Ascending
        {
            get
            {
                return this.ascendingField;
            }
            set
            {
                this.ascendingField = value;
                base.RaisePropertyChanged("Ascending");
            }
        }

        [XmlIgnore]
        public bool AscendingSpecified
        {
            get
            {
                return this.ascendingFieldSpecified;
            }
            set
            {
                this.ascendingFieldSpecified = value;
                base.RaisePropertyChanged("AscendingSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
        public NumericFormat NumberFormat
        {
            get
            {
                return this.numberFormatField;
            }
            set
            {
                this.numberFormatField = value;
                base.RaisePropertyChanged("NumberFormat");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
        public bool ShowClassGaps
        {
            get
            {
                return this.showClassGapsField;
            }
            set
            {
                this.showClassGapsField = value;
                base.RaisePropertyChanged("ShowClassGaps");
            }
        }

        [XmlIgnore]
        public bool ShowClassGapsSpecified
        {
            get
            {
                return this.showClassGapsFieldSpecified;
            }
            set
            {
                this.showClassGapsFieldSpecified = value;
                base.RaisePropertyChanged("ShowClassGapsSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
        public double DeviationInterval
        {
            get
            {
                return this.deviationIntervalField;
            }
            set
            {
                this.deviationIntervalField = value;
                base.RaisePropertyChanged("DeviationInterval");
            }
        }

        [XmlIgnore]
        public bool DeviationIntervalSpecified
        {
            get
            {
                return this.deviationIntervalFieldSpecified;
            }
            set
            {
                this.deviationIntervalFieldSpecified = value;
                base.RaisePropertyChanged("DeviationIntervalSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
        public object ExlusionValues
        {
            get
            {
                return this.exlusionValuesField;
            }
            set
            {
                this.exlusionValuesField = value;
                base.RaisePropertyChanged("ExlusionValues");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
        public object ExclusionRanges
        {
            get
            {
                return this.exclusionRangesField;
            }
            set
            {
                this.exclusionRangesField = value;
                base.RaisePropertyChanged("ExclusionRanges");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
        public bool ExclusionShowClass
        {
            get
            {
                return this.exclusionShowClassField;
            }
            set
            {
                this.exclusionShowClassField = value;
                base.RaisePropertyChanged("ExclusionShowClass");
            }
        }

        [XmlIgnore]
        public bool ExclusionShowClassSpecified
        {
            get
            {
                return this.exclusionShowClassFieldSpecified;
            }
            set
            {
                this.exclusionShowClassFieldSpecified = value;
                base.RaisePropertyChanged("ExclusionShowClassSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
        public LegendClass ExclusionLegendClass
        {
            get
            {
                return this.exclusionLegendClassField;
            }
            set
            {
                this.exclusionLegendClassField = value;
                base.RaisePropertyChanged("ExclusionLegendClass");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 17)]
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

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
        public bool UseHillShader
        {
            get
            {
                return this.useHillShaderField;
            }
            set
            {
                this.useHillShaderField = value;
                base.RaisePropertyChanged("UseHillShader");
            }
        }

        [XmlIgnore]
        public bool UseHillShaderSpecified
        {
            get
            {
                return this.useHillShaderFieldSpecified;
            }
            set
            {
                this.useHillShaderFieldSpecified = value;
                base.RaisePropertyChanged("UseHillShaderSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
        public double ZScale
        {
            get
            {
                return this.zScaleField;
            }
            set
            {
                this.zScaleField = value;
                base.RaisePropertyChanged("ZScale");
            }
        }

        [XmlIgnore]
        public bool ZScaleSpecified
        {
            get
            {
                return this.zScaleFieldSpecified;
            }
            set
            {
                this.zScaleFieldSpecified = value;
                base.RaisePropertyChanged("ZScaleSpecified");
            }
        }

        private string classFieldField;

        private string normFieldField;

        private bool classificationComponentField;

        private bool classificationComponentFieldSpecified;

        private string guidField;

        private string colorSchemaField;

        private int legendGroupsCountField;

        private bool legendGroupsCountFieldSpecified;

        private LegendGroup[] legendGroupsField;

        private int breakSizeField;

        private bool breakSizeFieldSpecified;

        private double[] arrayOfBreakField;

        private bool ascendingField;

        private bool ascendingFieldSpecified;

        private NumericFormat numberFormatField;

        private bool showClassGapsField;

        private bool showClassGapsFieldSpecified;

        private double deviationIntervalField;

        private bool deviationIntervalFieldSpecified;

        private object exlusionValuesField;

        private object exclusionRangesField;

        private bool exclusionShowClassField;

        private bool exclusionShowClassFieldSpecified;

        private LegendClass exclusionLegendClassField;

        private RasterUniqueValues uniqueValuesField;

        private bool useHillShaderField;

        private bool useHillShaderFieldSpecified;

        private double zScaleField;

        private bool zScaleFieldSpecified;
    }
}