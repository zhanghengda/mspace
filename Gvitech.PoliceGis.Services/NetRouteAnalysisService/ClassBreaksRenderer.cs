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
    public class ClassBreaksRenderer : FeatureRenderer
    {
        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
        public string Field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
                base.RaisePropertyChanged("Field");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
        public double MinimumValue
        {
            get
            {
                return this.minimumValueField;
            }
            set
            {
                this.minimumValueField = value;
                base.RaisePropertyChanged("MinimumValue");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 2)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public ClassBreakInfo[] ClassBreakInfos
        {
            get
            {
                return this.classBreakInfosField;
            }
            set
            {
                this.classBreakInfosField = value;
                base.RaisePropertyChanged("ClassBreakInfos");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
        public FillSymbol BackgroundSymbol
        {
            get
            {
                return this.backgroundSymbolField;
            }
            set
            {
                this.backgroundSymbolField = value;
                base.RaisePropertyChanged("BackgroundSymbol");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
        public string NormalizationField
        {
            get
            {
                return this.normalizationFieldField;
            }
            set
            {
                this.normalizationFieldField = value;
                base.RaisePropertyChanged("NormalizationField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
        public esriNormalizationType NormalizationType
        {
            get
            {
                return this.normalizationTypeField;
            }
            set
            {
                this.normalizationTypeField = value;
                base.RaisePropertyChanged("NormalizationType");
            }
        }

        [XmlIgnore]
        public bool NormalizationTypeSpecified
        {
            get
            {
                return this.normalizationTypeFieldSpecified;
            }
            set
            {
                this.normalizationTypeFieldSpecified = value;
                base.RaisePropertyChanged("NormalizationTypeSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
        public double NormalizationTotal
        {
            get
            {
                return this.normalizationTotalField;
            }
            set
            {
                this.normalizationTotalField = value;
                base.RaisePropertyChanged("NormalizationTotal");
            }
        }

        [XmlIgnore]
        public bool NormalizationTotalSpecified
        {
            get
            {
                return this.normalizationTotalFieldSpecified;
            }
            set
            {
                this.normalizationTotalFieldSpecified = value;
                base.RaisePropertyChanged("NormalizationTotalSpecified");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
        public string RotationField
        {
            get
            {
                return this.rotationFieldField;
            }
            set
            {
                this.rotationFieldField = value;
                base.RaisePropertyChanged("RotationField");
            }
        }

        [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
        public esriRotationType RotationType
        {
            get
            {
                return this.rotationTypeField;
            }
            set
            {
                this.rotationTypeField = value;
                base.RaisePropertyChanged("RotationType");
            }
        }

        [XmlIgnore]
        public bool RotationTypeSpecified
        {
            get
            {
                return this.rotationTypeFieldSpecified;
            }
            set
            {
                this.rotationTypeFieldSpecified = value;
                base.RaisePropertyChanged("RotationTypeSpecified");
            }
        }

        private string fieldField;

        private double minimumValueField;

        private ClassBreakInfo[] classBreakInfosField;

        private FillSymbol backgroundSymbolField;

        private string normalizationFieldField;

        private esriNormalizationType normalizationTypeField;

        private bool normalizationTypeFieldSpecified;

        private double normalizationTotalField;

        private bool normalizationTotalFieldSpecified;

        private string rotationFieldField;

        private esriRotationType rotationTypeField;

        private bool rotationTypeFieldSpecified;
    }
}